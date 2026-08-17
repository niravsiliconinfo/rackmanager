// NgScript/Controller/customerInventoryGridCtrl.js  — CUSTOMER
// Luckysheet 2.1.13. Customer can EDIT cells + Save + Export.
// Customer CANNOT Add Rows or Delete Rows.

var app = angular.module('myApp');

app.controller('customerInventoryGridCtrl',
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
                    .catch(function (err) {
                        if (err.status === 403)
                            $scope.gridError = 'Access Denied. You do not have access to this file.';
                    });

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
                        $scope.gridError = err.status === 403
                            ? 'Access Denied. You do not have access to this file.'
                            : 'Could not load grid: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data)) : err.statusText);
                    });
            }

            function InitSheet(rows) {
                if (sheetReady) {
                    try { window.luckysheet.destroy(); } catch (e) { }
                    sheetReady = false;
                }

                var celldata = [];

                $scope.headers.forEach(function (h, c) {
                    celldata.push({
                        r: 0, c: c,
                        v: {
                            v: h.Label, m: h.Label, bl: 1, fc: '#ffffff', bg: '#1a237e',
                            ct: { fa: 'General', t: 'g' }
                        }
                    });
                });

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

            //  SAVE — uses getAllSheets()[0].data (correct Luckysheet 2.1.13 API) 
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

                if (!rowsToSave.length) {
                    $scope.hasUnsavedChanges = false;
                    return;
                }

                $scope.saving = true;
                $scope.gridError = '';

                $http.post('/api/pageview/bulkSaveInventoryRows',
                    { Rows: rowsToSave }, { params: { fileId: $scope.fileId } })
                    .then(function () {
                        $scope.saving = false;
                        $scope.hasUnsavedChanges = false;
                        $scope.gridSuccess = rowsToSave.length + ' row(s) saved.';
                        $timeout(function () { $scope.gridSuccess = ''; }, 3000);
                        LoadFile($scope.fileId);
                    })
                    .catch(function (err) {
                        $scope.saving = false;
                        $scope.gridError = 'Save failed: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data)) : err.statusText);
                    });
            };

            $scope.DiscardChanges = function () {
                if (!confirm('Discard all unsaved changes and reload?')) return;
                $scope.hasUnsavedChanges = false;
                LoadFile($scope.fileId);
            };

            $scope.ExportGrid = function () {
                window.location.href = '/api/pageview/exportInventoryFile?fileId=' + $scope.fileId;
            };

            // NOTE: No AddRow or DeleteSelectedRows — customers cannot add or delete rows.

        }]);