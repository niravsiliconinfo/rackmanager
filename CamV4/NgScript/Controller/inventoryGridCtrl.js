// NgScript/Controller/inventoryGridCtrl.js  — ADMIN
// Luckysheet 2.1.13 grid editor for InventoryFile CRUD.

var app = angular.module('myApp');

app.controller('inventoryGridCtrl',
    ['$scope', '$http', '$timeout',
        function ($scope, $http, $timeout) {

            $scope.currentFile = {};
            $scope.headers = [];
            $scope.rowItemIds = [];
            $scope.rowCount = 0;
            $scope.loading = true;
            $scope.saving = false;
            $scope.hasUnsavedChanges = false;
            $scope.gridError = '';
            $scope.gridSuccess = '';

            var sheetReady = false;

            $scope.$watch('fileId', function (val) {
                if (!val) return;
                LoadFile(val);
            });

            function LoadFile(fileId) {
                $scope.loading = true;
                $scope.hasUnsavedChanges = false;
                $scope.gridError = '';
                sheetReady = false;

                $http.get('/api/pageview/getInventoryFileById', { params: { fileId: fileId } })
                    .then(function (r) { $scope.currentFile = r.data || {}; })
                    .catch(function () { });

                $http.get('/api/pageview/getInventoryHeaders', { params: { fileId: fileId } })
                    .then(function (rh) {
                        $scope.headers = Array.isArray(rh.data) ? rh.data : [];
                        return $http.get('/api/pageview/getInventoryGridData', { params: { fileId: fileId } });
                    })
                    .then(function (rd) {
                        var rows = Array.isArray(rd.data) ? rd.data : [];
                        $scope.rowItemIds = rows.map(function (r) { return r.ItemID; });
                        $scope.rowCount = rows.length;
                        $timeout(function () {
                            $scope.loading = false;
                            $timeout(function () { InitSheet(rows); }, 100);
                        });
                    })
                    .catch(function (err) {
                        $scope.loading = false;
                        $scope.gridError = 'Could not load grid: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data)) : err.statusText);
                    });
            }

            function InitSheet(rows) {
                if (sheetReady) {
                    try { window.luckysheet.destroy(); } catch (e) { }
                    sheetReady = false;
                }

                var celldata = [];

                // Row 0 — header
                $scope.headers.forEach(function (h, c) {
                    celldata.push({
                        r: 0, c: c,
                        v: {
                            v: h.Label, m: h.Label, bl: 1, fc: '#ffffff', bg: '#217346',
                            ct: { fa: 'General', t: 'g' }
                        }
                    });
                });

                // Rows 1+ — data
                rows.forEach(function (row, ri) {
                    $scope.headers.forEach(function (h, c) {
                        var raw = (row.Values && row.Values[h.Key] != null) ? String(row.Values[h.Key]) : '';
                        var isNum = h.Type === 'number';
                        var numVal = isNum ? (parseFloat(raw) || 0) : null;
                        celldata.push({
                            r: ri + 1, c: c,
                            v: {
                                v: isNum ? numVal : raw,
                                m: isNum ? String(numVal) : raw,
                                ct: { fa: 'General', t: isNum ? 'n' : 's' },
                                bg: h.Locked ? '#f6f8fa' : null,
                                fc: h.Locked ? '#555555' : null
                            }
                        });
                    });
                });

                var colWidths = {};
                $scope.headers.forEach(function (h, i) { colWidths[i] = h.Width || 130; });

                window.luckysheet.create({
                    container: 'luckysheet-container',
                    title: $scope.currentFile.FileName || 'Spare Materials',
                    lang: 'en',
                    showinfobar: false,
                    showsheetbar: false,
                    showstatisticBar: false,
                    enableAddRow: false,
                    enableAddBackTop: false,
                    userInfo: false,
                    freezeRow: 1,
                    freezeColumn: 0,
                    hook: {
                        cellUpdated: function (r, c, oldVal, newVal) {
                            $timeout(function () { $scope.hasUnsavedChanges = true; });
                        }
                    },
                    data: [{
                        name: 'Spare Materials',
                        index: 0,
                        status: 1,
                        order: 0,
                        celldata: celldata,
                        config: { columnlen: colWidths }
                    }]
                });

                sheetReady = true;
            }

            //  SAVE 
            $scope.SaveAll = function () {
                if ($scope.saving || !sheetReady) return;

                var sheets = window.luckysheet.getAllSheets();
                if (!sheets || !sheets[0] || !sheets[0].data) {
                    $scope.gridError = 'Could not read sheet data. Please try again.';
                    return;
                }

                var grid2d = sheets[0].data;
                var headers = $scope.headers;
                var rowsToSave = [];

                for (var ri = 1; ri < grid2d.length; ri++) {
                    var rowData = grid2d[ri];
                    if (!rowData) continue;
                    var values = {};
                    var hasData = false;
                    headers.forEach(function (h, ci) {
                        var cell = rowData[ci];
                        var v = (cell && cell.v !== null && cell.v !== undefined) ? String(cell.v) : '';
                        values[h.Key] = v;
                        if (v.trim() !== '') hasData = true;
                    });
                    var itemId = $scope.rowItemIds[ri - 1] || 0;
                    if (!hasData && itemId === 0) continue;
                    rowsToSave.push({ ItemID: itemId, Values: values });
                }

                // ── Row 0: column label updates ───────────────────────────
                var colUpdates = {};
                var row0 = grid2d[0];
                if (row0) {
                    headers.forEach(function (h, ci) {
                        var cell = row0[ci];
                        var newLabel = (cell && cell.v != null) ? String(cell.v) : '';
                        if (newLabel && newLabel !== h.Label) {
                            colUpdates[h.Key] = newLabel;
                        }
                    });
                }

                // ── Nothing changed at all ────────────────────────────────
                if (!rowsToSave.length && !Object.keys(colUpdates).length) {
                    $scope.hasUnsavedChanges = false;
                    return;
                }

                $scope.saving = true;
                $scope.gridError = '';

                // ── Build promise chain so LoadFile only runs once at end ─
                var savePromise;

                if (rowsToSave.length) {
                    savePromise = $http.post('/api/pageview/bulkSaveInventoryRows',
                        { Rows: rowsToSave },
                        { params: { fileId: $scope.fileId } });
                } else {
                    // No data rows to save — create a resolved promise to chain from
                    savePromise = $q.when(null);
                }

                savePromise
                    .then(function () {
                        if (Object.keys(colUpdates).length) {
                            return $http.post('/api/pageview/updateInventoryColumnLabels',
                                colUpdates,
                                { params: { fileId: $scope.fileId } });
                        }
                    })
                    .then(function () {
                        $scope.saving = false;
                        $scope.hasUnsavedChanges = false;
                        $scope.gridSuccess = 'Changes saved successfully.';
                        $timeout(function () { $scope.gridSuccess = ''; }, 3000);
                        LoadFile($scope.fileId); // single reload after both saves complete
                    })
                    .catch(function (err) {
                        $scope.saving = false;
                        $scope.gridError = 'Save failed: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data)) : err.statusText);
                    });
            };

            //  ADD ROW 
            $scope.AddRow = function () {
                if (!sheetReady) return;
                var sheets = window.luckysheet.getAllSheets();
                var data = sheets[0].data || [];
                var lastRow = data.length; // insert after last row
                window.luckysheet.insertRowOrColumn('row', lastRow, 1, 'rightbottom');
                $scope.rowCount++;
                $timeout(function () { $scope.hasUnsavedChanges = true; });
            };

            //  DELETE SELECTED ROWS 
            $scope.DeleteSelectedRows = function () {
                if (!sheetReady) return;
                var range = window.luckysheet.getRange();
                if (!range || !range.length) {
                    alert('Select one or more rows in the grid first, then click Delete Row(s).');
                    return;
                }

                var rowSet = {};
                range.forEach(function (sel) {
                    for (var r = sel.row[0]; r <= sel.row[1]; r++) {
                        if (r > 0) rowSet[r] = true;
                    }
                });

                var rowIndices = Object.keys(rowSet).map(Number).sort(function (a, b) { return b - a; });
                if (!rowIndices.length) {
                    alert('Please select data rows — the header row cannot be deleted.');
                    return;
                }

                if (!confirm('Delete ' + rowIndices.length + ' row(s)? This cannot be undone.')) return;

                var idsToDelete = [];
                rowIndices.forEach(function (ri) {
                    var itemId = $scope.rowItemIds[ri - 1];
                    if (itemId) idsToDelete.push(itemId);
                });

                rowIndices.forEach(function (ri) {
                    try { window.luckysheet.deleteRowOrColumn('row', ri, ri); } catch (e) { }
                });

                var deletedSet = {};
                rowIndices.forEach(function (ri) { deletedSet[ri - 1] = true; });
                $scope.rowItemIds = $scope.rowItemIds.filter(function (id, idx) { return !deletedSet[idx]; });
                $scope.rowCount = Math.max(0, $scope.rowCount - rowIndices.length);

                if (!idsToDelete.length) {
                    $timeout(function () { $scope.hasUnsavedChanges = true; });
                    return;
                }

                $http.post('/api/pageview/deleteInventoryRows',
                    { ItemIds: idsToDelete }, { params: { fileId: $scope.fileId } })
                    .then(function () {
                        $scope.gridSuccess = idsToDelete.length + ' row(s) deleted.';
                        $timeout(function () { $scope.gridSuccess = ''; }, 2500);
                    })
                    .catch(function (err) {
                        $scope.gridError = 'Delete failed: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data)) : err.statusText);
                        LoadFile($scope.fileId);
                    });
            };

            //  DISCARD 
            $scope.DiscardChanges = function () {
                if (!confirm('Discard all unsaved changes and reload?')) return;
                $scope.hasUnsavedChanges = false;
                LoadFile($scope.fileId);
            };

            //  EXPORT 
            $scope.ExportGrid = function () {
                window.location.href = '/api/pageview/exportInventoryFile?fileId=' + $scope.fileId;
            };

        }]);