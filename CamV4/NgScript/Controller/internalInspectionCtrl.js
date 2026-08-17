// ============================================================
// internalInspectionCtrl.js - final
// Fixes: redirect to listing after save, admin listing direct load,
//        edit mode date/reportedBy, PDF download button
// ============================================================

try {
    angular.module('myApp').filter('startFrom', function () {
        return function (input, start) {
            if (input) { start = +start; return input.slice(start); }
            return [];
        };
    });
} catch (e) {}

angular.module('myApp')
    .controller('internalInspectionCtrl', function ($scope, $http, $window, $filter, sharedFilterService) {

        var path = window.location.pathname.toLowerCase();
        console.log('path-----------------',path);
        // ---- Paging ----
        $scope.liPaging = { current: 1, size: 10 };
        $scope.getPages = function (total, size) {
            var p = []; for (var i = 1; i <= Math.ceil(total / size); i++) p.push(i); return p;
        };
        $scope.setPage  = function (pg, p)    { pg.current = p; };
        $scope.prevPage = function (pg)        { if (pg.current > 1) pg.current--; };
        $scope.nextPage = function (pg, total) { if (pg.current < Math.ceil(total / pg.size)) pg.current++; };

        // ---- CSV checkbox helpers ----
        $scope.IsCSVSelected = function (csv, val) {
            if (!csv) return false;
            return csv.split(',').map(function (v) { return v.trim(); }).indexOf(String(val)) !== -1;
        };
        $scope.ToggleCSV = function (def, field, val) {
            var arr = def[field] ? def[field].split(',').map(function (v) { return v.trim(); }).filter(Boolean) : [];
            var idx = arr.indexOf(String(val));
            if (idx === -1) arr.push(String(val)); else arr.splice(idx, 1);
            def[field] = arr.join(',');
        };

        // ---- Fixed option lists ----
        $scope.levelOptions = [];
        for (var lv = 1; lv <= 25; lv++) $scope.levelOptions.push(String(lv));
        $scope.levelOptions.push('All'); $scope.levelOptions.push('Several');
        $scope.levelOptions.push('Various'); $scope.levelOptions.push('None');

        $scope.beamLocationOptions = ['Front', 'Rear', 'Both'];
        $scope.frameSideOptions    = ['Left', 'Right', 'Left & Right', 'None'];
        $scope.assessmentOptions   = ['Minor', 'Moderate', 'Severe'];
        $scope.actionOptions       = ['Monitor', 'Repair', 'Replace', 'Unload', 'Review Requested'];

       


        // ============================================================
        // CUSTOMER - LISTING
        // ============================================================
        if (path.indexOf('customerinternalinspectionlisting') !== -1) {

            $scope.inspectionList = [];
            $scope.activeFilter   = {};
            $scope.filterApplied  = false;

            var loadInspections = function (filter) {
                var params = {};
                if (filter) {
                    if (filter.location) params.CustomerLocationID = filter.location;
                    if (filter.facility) params.CustomerFacilityID = filter.facility;
                    if (filter.area)     params.CustomerAreaID     = filter.area;
                    if (filter.Region)   params.Region             = filter.Region;
                }
                $http.get('/api/pageview/getMyInternalInspections', { params: params })
                    .then(function (res) {
                        $scope.inspectionList   = res.data || [];
                        $scope.liPaging.current = 1;
                    }, function () { $scope.inspectionList = []; });
            };

            loadInspections(null);

            //$scope.$on('internalInspectionFiltersUpdated', function () {
            //    var f = sharedFilterService.getInternalFilter();
            //    $scope.activeFilter  = f;
            //    $scope.filterApplied = !!(f.location || f.facility || f.area || f.Region);
            //    loadInspections(f);
            //});

            $scope.$on('internalInspectionFiltersUpdated',function (event, filters) {
                    console.log('internalInspectionFiltersUpdated received', filters);
                    loadInspections(filters);  // Your load function
                });

            //$scope.$on('internalInspectionFiltersUpdated', function (event, filters) {
            //    console.log('internalInspectionFiltersUpdated received', filters);
            //    loadInternalInspections(filters);
            //});

            $scope.ClearInternalFilter = function () {
                $scope.activeFilter  = {};
                $scope.filterApplied = false;
                loadInspections(null);
            };

            $http.get('/api/pageview/getEngineerReviewCost')
                .then(function (res) {
                    $scope.engineerReviewCost = (res.data && res.data.cost) ? res.data.cost : 20;
                }, function () {
                    $scope.engineerReviewCost = 20; // safe fallback
                });

        }

        // ============================================================
        // CUSTOMER - ADD/EDIT
        // ============================================================
        if (path.indexOf('customerinternalinspectionaddedit') !== -1) {

            $scope.form               = {};
            $scope.deficiencies       = [];
            $scope.locationList       = [];
            $scope.allFacilities      = [];
            $scope.allAreas           = [];
            $scope.filteredFacilities = [];
            $scope.filteredAreas      = [];
            $scope.saving             = false;
            $scope.formError          = '';
            $scope.formSuccess        = '';

            function getIdFromUrl() {
                var m = window.location.search.match(/[?&]id=([^&]*)/);
                return m ? m[1] : null;
            }
            var editId = getIdFromUrl();
            $scope.inspectionId = editId || '0';

            $http.get('/api/pageview/getMyCustomerHierarchy')
                .then(function (res) {
                    $scope.locationList  = res.data.Locations  || [];
                    $scope.allFacilities = res.data.Facilities || [];
                    $scope.allAreas      = res.data.Areas      || [];
                    if (editId && editId !== '0') loadForEdit(editId);
                }, function () { });

            $http.get('/api/pageview/getEngineerReviewCost')
                .then(function (res) {
                    $scope.engineerReviewCost = (res.data && res.data.cost) ? res.data.cost : 20;
                }, function () {
                    $scope.engineerReviewCost = 20; // safe fallback
                });


            $scope.onLocationChange = function () {
                $scope.form.customerFacilityId = '';
                $scope.form.customerAreaId     = '';
                $scope.filteredFacilities = $scope.allFacilities.filter(function (f) {
                    return String(f.CustomerLocationID) === String($scope.form.customerLocationId);
                });
                $scope.filteredAreas = [];
            };

            $scope.onFacilityChange = function () {
                $scope.form.customerAreaId = '';
                var locId = String($scope.form.customerLocationId);
                var facId = String($scope.form.customerFacilityId);
                $scope.filteredAreas = $scope.allAreas.filter(function (a) {
                    return String(a.CustomerLocationID) === locId
                        && (!a.CustomerFacilityID || a.CustomerFacilityID == 0
                            || String(a.CustomerFacilityID) === facId);
                });
            };

            var newDef = function () {
                return {
                    InternalInspectionDeficiencyID: 0,
                    Area: '', Row: '', Aisle: '', Bay: '',
                    BeamFrameLevel: '', BeamLocation: '', FrameSide: '',
                    InternalAssessment: '', InternalAction: '',
                    RecommendedAction: '',
                    IsEngineerReviewRequested: false,
                    Status: 'Open',
                    previewPhotos: [], newFiles: []
                };
            };

            $scope.AddDeficiency    = function ()      { $scope.deficiencies.push(newDef()); };
            $scope.RemoveDeficiency = function (index) { $scope.deficiencies.splice(index, 1); };

            $scope.onDeficiencyPhotosSelected = function (files, inputEl) {
                var idx = parseInt(inputEl.id.replace('defPhoto_', ''));
                var def = $scope.deficiencies[idx];
                if (!def) return;
                for (var i = 0; i < files.length; i++) {
                    (function (file) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $scope.$apply(function () {
                                def.previewPhotos.push({ src: e.target.result });
                                def.newFiles.push(file);
                            });
                        };
                        reader.readAsDataURL(file);
                    })(files[i]);
                }
                inputEl.value = '';
            };

            $scope.RemoveDeficiencyPhoto = function (def, index) {
                def.previewPhotos.splice(index, 1);
                def.newFiles.splice(index, 1);
            };

            var editLoaded = false;
            var loadForEdit = function (id) {
                if (editLoaded) return;
                editLoaded = true;
                $http.get('/api/pageview/getInternalInspectionById', { params: { id: id } })
                    .then(function (res) {
                        var d = res.data;

                        // Parse date - API returns ISO string e.g. "/Date(1234567890)/" or "2026-03-25T00:00:00"
                        var parsedDate = '';
                        if (d.InternalInspectionDate) {
                            var raw = d.InternalInspectionDate;
                            // Handle /Date(...)/ format
                            var msMatch = String(raw).match(/\/Date\((\d+)\)\//);
                            if (msMatch) {
                                var dt = new Date(parseInt(msMatch[1]));
                                parsedDate = dt.toISOString().substring(0, 10);
                            } else {
                                parsedDate = String(raw).substring(0, 10);
                            }
                        }

                        $scope.form = {
                            inspectionNumber:    d.InternalInspectionNumber,
                            customerLocationId:  String(d.CustomerLocationID),
                            customerFacilityId:  d.CustomerFacilityID ? String(d.CustomerFacilityID) : '',
                            customerAreaId:      d.CustomerAreaID     ? String(d.CustomerAreaID)     : '',
                            inspectionDate:      parsedDate,
                            reportedBy:          d.ReportedByName || d.ReportedBy || ''
                        };

                        $scope.filteredFacilities = $scope.allFacilities.filter(function (f) {
                            return String(f.CustomerLocationID) === String(d.CustomerLocationID);
                        });
                        $scope.filteredAreas = $scope.allAreas.filter(function (a) {
                            return String(a.CustomerLocationID) === String(d.CustomerLocationID);
                        });

                        $scope.deficiencies = (d.Deficiencies || []).map(function (def) {
                            def.previewPhotos = [];
                            def.newFiles      = [];
                            return def;
                        });
                    }, function () { $scope.formError = 'Failed to load inspection.'; });
            };

            $scope.SaveInspection = function () {
                $scope.formError = $scope.formSuccess = '';
                if (!$scope.form.customerLocationId) { $scope.formError = 'Location is required.'; return; }
                $scope.saving = true;

                var fd = new FormData();
                fd.append('InternalInspectionID',   $scope.inspectionId || 0);
                fd.append('CustomerLocationID',     $scope.form.customerLocationId);
                fd.append('CustomerFacilityID',     $scope.form.customerFacilityId || '');
                fd.append('CustomerAreaID',         $scope.form.customerAreaId     || '');
                // Format date as yyyy-MM-dd
                var formattedDate = $filter('date')($scope.form.inspectionDate, 'yyyy-MM-dd') || $scope.form.inspectionDate || '';
                fd.append('InternalInspectionDate', formattedDate);
                fd.append('ReportedBy',             $scope.form.reportedBy || '');
                fd.append('DeficiencyCount',        $scope.deficiencies.length);

                $scope.deficiencies.forEach(function (def, i) {
                    var p = 'Deficiency[' + i + '].';
                    fd.append(p + 'DeficiencyID',               def.InternalInspectionDeficiencyID || 0);
                    fd.append(p + 'Area',                       def.Area               || '');
                    fd.append(p + 'Row',                        def.Row                || '');
                    fd.append(p + 'Aisle',                      def.Aisle              || '');
                    fd.append(p + 'Bay',                        def.Bay                || '');
                    fd.append(p + 'BeamFrameLevel',             def.BeamFrameLevel     || '');
                    fd.append(p + 'BeamLocation',               def.BeamLocation       || '');
                    fd.append(p + 'FrameSide',                  def.FrameSide          || '');
                    fd.append(p + 'InternalAssessment',         def.InternalAssessment || '');
                    fd.append(p + 'InternalAction',             def.InternalAction     || '');
                    fd.append(p + 'RecommendedAction',          def.RecommendedAction  || '');
                    fd.append(p + 'IsEngineerReviewRequested',  def.IsEngineerReviewRequested ? 'true' : 'false');
                    (def.newFiles || []).forEach(function (file) {
                        fd.append('DeficiencyPhotos_' + i, file);
                    });
                });

                $http({
                    url: '/api/pageview/saveInternalInspection', method: 'POST',
                    data: fd, transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (res) {
                    $scope.saving = false;
                    if (res.data && res.data.success) {
                        // FIXED: redirect to listing, not view page
                        $window.location.href = '/Customer/customerInternalInspectionListing';
                    } else {
                        $scope.formError = (res.data && res.data.message) || 'Failed to save.';
                    }
                }, function () { $scope.saving = false; $scope.formError = 'An error occurred.'; });
            };
        }

        // ============================================================
        // CUSTOMER - VIEW
        // ============================================================
        if (path.indexOf('customerinternalinspectionview') !== -1) {
            $scope.inspection    = null;
            $scope.downloading   = false;
            $scope.downloadError = '';

            var id = new URLSearchParams(window.location.search).get('id');
            if (id && id !== '0') {
                $http.get('/api/pageview/getInternalInspectionById', { params: { id: id } })
                    .then(function (res) { $scope.inspection = res.data; },
                          function ()    { $scope.inspection = null; });
            }

            // Download PDF - fetches HTML from server, opens print dialog
            $scope.DownloadPdf = function () {
                $scope.downloading   = true;
                $scope.downloadError = '';
                $http.get('/api/pageview/getInternalInspectionPdfHtml', { params: { id: id } })
                    .then(function (res) {
                        $scope.downloading = false;
                        var html = res.data.html;
                        var win  = window.open('', '_blank');
                        win.document.open();
                        win.document.write(html);
                        win.document.close();
                        win.focus();
                        // Trigger print after content loads
                        win.onload = function () { win.print(); };
                    }, function () {
                        $scope.downloading   = false;
                        $scope.downloadError = 'Failed to generate report.';
                    });
            };
        }

        // ============================================================
        // ADMIN - LISTING
        // FIXED: reads customerId directly from URL, no ng-init race
        // ============================================================
        if (path.indexOf('/admin/internalinspectionlisting') !== -1) {

            $scope.inspectionList    = [];
            $scope.filteredList      = [];
            $scope.adminCustomerName = '';
            $scope.adminStatusFilter = '';

            // Read customerId directly from URL
            var custId = new URLSearchParams(window.location.search).get('id');
            console.log(custId);            
            if (custId && custId !== '' && custId !== '0') {
                $http.get('/api/pageview/getInternalInspectionsByCustomerId', {
                    params: { customerId: custId }
                }).then(function (res) {
                    $scope.inspectionList   = res.data || [];
                    $scope.filteredList     = $scope.inspectionList;
                    $scope.liPaging.current = 1;
                    if ($scope.inspectionList.length > 0)
                        $scope.adminCustomerName = $scope.inspectionList[0].CustomerName;
                }, function () { $scope.inspectionList = []; $scope.filteredList = []; });
            }

            $scope.FilterByStatus = function () {
                $scope.filteredList = !$scope.adminStatusFilter
                    ? $scope.inspectionList
                    : $scope.inspectionList.filter(function (d) { return d.Status === $scope.adminStatusFilter; });
                $scope.liPaging.current = 1;
            };
        }

        // ============================================================
        // ADMIN - VIEW
        // ============================================================
        if (path.indexOf('/admin/IncidentReportView') !== -1) {

            $scope.inspection     = null;
            $scope.newStatus      = '';
            $scope.updatingStatus = false;
            $scope.statusSuccess  = '';
            $scope.statusError    = '';
            $scope.downloading    = false;
            $scope.downloadError  = '';

            var inspId = new URLSearchParams(window.location.search).get('id');
            if (inspId && inspId !== '0') {
                $http.get('/api/pageview/getInternalInspectionById', { params: { id: inspId } })
                    .then(function (res) {
                        $scope.inspection = res.data;
                        $scope.newStatus  = res.data.Status;
                        $scope.inspectionId = inspId;
                    }, function () { $scope.inspection = null; });
            }

            $scope.UpdateStatus = function () {
                $scope.statusSuccess = ''; $scope.statusError = ''; $scope.updatingStatus = true;
                $http({ url: '/api/pageview/updateInternalInspectionStatus', method: 'POST',
                        data: { id: $scope.inspectionId, status: $scope.newStatus },
                        headers: { 'Content-Type': 'application/json',
                                   'RequestVerificationToken': $scope.antiForgeryToken }
                }).then(function (res) {
                    $scope.updatingStatus = false;
                    if (res.data === true) {
                        $scope.statusSuccess     = 'Status updated to "' + $scope.newStatus + '".';
                        $scope.inspection.Status = $scope.newStatus;
                    } else { $scope.statusError = 'Failed to update status.'; }
                }, function () { $scope.updatingStatus = false; $scope.statusError = 'An error occurred.'; });
            };

            $scope.DeleteInspection = function (id) {
                if (!confirm('Delete this inspection? This cannot be undone.')) return;
                $http({ url: '/api/pageview/deleteInternalInspection', method: 'POST',
                        data: { id: id },
                        headers: { 'Content-Type': 'application/json',
                                   'RequestVerificationToken': $scope.antiForgeryToken }
                }).then(function (res) {
                    if (res.data === true) history.back();
                    else alert('Failed to delete.');
                }, function () { alert('An error occurred.'); });
            };

            $scope.DownloadPdf = function () {
                $scope.downloading   = true;
                $scope.downloadError = '';
                $http.get('/api/pageview/getInternalInspectionPdfHtml', { params: { id: inspId } })
                    .then(function (res) {
                        $scope.downloading = false;
                        var win = window.open('', '_blank');
                        win.document.open();
                        win.document.write(res.data.html);
                        win.document.close();
                        win.focus();
                        win.onload = function () { win.print(); };
                    }, function () {
                        $scope.downloading   = false;
                        $scope.downloadError = 'Failed to generate report.';
                    });
            };
        }

    });
