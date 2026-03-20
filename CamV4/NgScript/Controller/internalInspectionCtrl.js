// ============================================================
// internalInspectionCtrl.js
// Internal Inspection Module — separate controller
// Retrieves existing 'myApp' module — does NOT re-declare it
// ============================================================

// ---- Register startFrom filter safely ----
// Guard prevents duplicate registration error if customerCtrl.js
// is also loaded on the same page
try {
    angular.module('myApp').filter('startFrom', function () {
        return function (input, start) {
            if (input) { start = +start; return input.slice(start); }
            return [];
        };
    });
} catch (e) { /* already registered by customerCtrl.js */ }

angular.module('myApp')
    .controller('internalInspectionCtrl', function ($scope, $http, $window, sharedFilterService) {

        var path = window.location.pathname.toLowerCase();

        // ---- Shared paging helpers ----
        $scope.liPaging = { current: 1, size: 10 };

        $scope.getPages = function (total, pageSize) {
            var pages = [];
            var count = Math.ceil(total / pageSize);
            for (var i = 1; i <= count; i++) { pages.push(i); }
            return pages;
        };
        $scope.setPage = function (paging, p) {
            paging.current = p;
        };
        $scope.prevPage = function (paging) {
            if (paging.current > 1) paging.current--;
        };
        $scope.nextPage = function (paging, total) {
            var max = Math.ceil(total / paging.size);
            if (paging.current < max) paging.current++;
        };

        // ============================================================
        // CUSTOMER — LISTING PAGE
        // /Customer/customerInternalInspectionListing
        // ============================================================
        if (path.indexOf('customerinternalinspectionlisting') !== -1) {

            $scope.inspectionList = [];
            $scope.activeFilter = {};  // currently applied filter — shown in UI
            $scope.filterApplied = false;

            // ---- Load inspections — optionally with filter ----
            var loadInspections = function (filter) {
                var params = {};

                if (filter) {
                    if (filter.location) params.CustomerLocationID = filter.location;
                    if (filter.facility) params.CustomerFacilityID = filter.facility;
                    if (filter.area) params.AreaID = filter.area;
                    if (filter.Region) params.Region = filter.Region;
                }

                $http.get('/api/pageview/getMyInternalInspections', { params: params })
                    .then(function (res) {
                        $scope.inspectionList = res.data || [];
                        $scope.liPaging.current = 1;
                    }, function () {
                        $scope.inspectionList = [];
                    });
            };

            // ---- Initial load — no filter ----
            loadInspections(null);

            // ---- Listen for filter broadcast from menuCtrl ----
            $scope.$on('internalFilterUpdated', function () {
                var filter = sharedFilterService.getInternalFilter();
                $scope.activeFilter = filter;
                $scope.filterApplied = !!(filter.location || filter.facility
                    || filter.area || filter.Region);
                loadInspections(filter);
            });

            // ---- Clear filter ----
            $scope.ClearInternalFilter = function () {
                $scope.activeFilter = {};
                $scope.filterApplied = false;
                loadInspections(null);
            };
        }

        // ============================================================
        // CUSTOMER — ADD/EDIT PAGE
        // /Customer/customerInternalInspectionAddEdit
        // ============================================================
        if (path.indexOf('customerinternalinspectionaddedit') !== -1) {

            $scope.form = {};
            $scope.deficiencies = [];
            $scope.selectedLevels = {};
            $scope.levelOptions = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            $scope.filteredFacilities = [];
            $scope.filteredAreas = [];
            $scope.allFacilities = [];
            $scope.allAreas = [];
            $scope.locationList = [];
            $scope.engineerReviewCount = 0;
            $scope.hierarchyReady = false;
            $scope.saving = false;
            $scope.formError = '';
            $scope.formSuccess = '';

            // ---- Load hierarchy for current customer (session-resolved) ----
            $http.get('/api/pageview/getMyCustomerHierarchy')
                .then(function (res) {
                    $scope.locationList = res.data.Locations || [];
                    $scope.allFacilities = res.data.Facilities || [];
                    $scope.allAreas = res.data.Areas || [];
                    $scope.hierarchyReady = true;

                    // If ng-init already set inspectionId before hierarchy finished
                    if ($scope.inspectionId && $scope.inspectionId !== ''
                        && $scope.inspectionId !== '0') {
                        LoadInspectionForEdit();
                    }
                }, function () {
                    $scope.hierarchyReady = true; // unblock watcher even on error
                });

            // ---- Watch for inspectionId in case ng-init fires after hierarchy ----
            $scope.$watch('inspectionId', function (val) {
                if (val && val !== '' && val !== '0' && $scope.hierarchyReady) {
                    LoadInspectionForEdit();
                }
            });

            // ---- Location change — filter facilities ----
            $scope.onLocationChange = function () {
                $scope.form.customerFacilityId = '';
                $scope.form.areaId = '';

                $scope.filteredFacilities = $scope.allFacilities.filter(function (f) {
                    return f.CustomerLocationID == $scope.form.customerLocationId;
                });
                $scope.filteredAreas = [];
            };

            // ---- Facility change — filter areas ----
            $scope.onFacilityChange = function () {
                $scope.form.areaId = '';
                var locId = $scope.form.customerLocationId;
                var facId = $scope.form.customerFacilityId;

                $scope.filteredAreas = $scope.allAreas.filter(function (a) {
                    return a.CustomerLocationID == locId
                        && (a.CustomerFacilityID == null
                            || a.CustomerFacilityID == 0
                            || a.CustomerFacilityID == facId);
                });
            };

            // ---- Deficiency helpers ----
            $scope.AddDeficiency = function () {
                $scope.deficiencies.push({
                    DeficiencyID: 0,
                    DeficiencyDescription: '',
                    Severity: '',
                    RecommendedAction: '',
                    IsEngineerReviewRequested: false,
                    Status: 'Open'
                });
            };

            $scope.RemoveDeficiency = function (index) {
                $scope.deficiencies.splice(index, 1);
                $scope.UpdateEngineerCost();
            };

            $scope.UpdateEngineerCost = function () {
                $scope.engineerReviewCount = $scope.deficiencies.filter(function (d) {
                    return d.IsEngineerReviewRequested;
                }).length;
            };

            // ---- Build levels comma string from selectedLevels object ----
            var BuildLevelsString = function () {
                var levels = [];
                $scope.levelOptions.forEach(function (lvl) {
                    if ($scope.selectedLevels[lvl]) levels.push(lvl);
                });
                return levels.join(',');
            };

            // ---- Load existing inspection for edit ----
            var editLoaded = false;
            var LoadInspectionForEdit = function () {
                if (editLoaded) return; // prevent double-load
                editLoaded = true;

                $http.get('/api/pageview/getInternalInspectionById', {
                    params: { id: $scope.inspectionId }
                }).then(function (res) {
                    var d = res.data;

                    $scope.form = {
                        inspectionNumber: d.InspectionNumber,
                        customerLocationId: String(d.CustomerLocationID),
                        customerFacilityId: d.CustomerFacilityID
                            ? String(d.CustomerFacilityID) : '',
                        areaId: d.AreaID ? String(d.AreaID) : '',
                        typeOfRack: d.TypeOfRack,
                        inspectionDate: d.InspectionDate
                            ? d.InspectionDate.substring(0, 10) : '',
                        area: d.Area,
                        row: d.Row,
                        aisle: d.Aisle,
                        bay: d.Bay,
                        beamLocation: d.BeamLocation,
                        frameSide: d.FrameSide,
                        reportedBy: d.ReportedBy,
                        inspectionSummary: d.InspectionSummary
                    };

                    // Pre-populate levels
                    $scope.selectedLevels = {};
                    (d.Levels || []).forEach(function (lvl) {
                        $scope.selectedLevels[lvl] = true;
                    });

                    // Pre-populate facility/area dropdowns
                    $scope.filteredFacilities = $scope.allFacilities.filter(function (f) {
                        return f.CustomerLocationID == d.CustomerLocationID;
                    });
                    $scope.filteredAreas = $scope.allAreas.filter(function (a) {
                        return a.CustomerLocationID == d.CustomerLocationID;
                    });

                    // Pre-populate deficiencies
                    $scope.deficiencies = d.Deficiencies || [];
                    $scope.UpdateEngineerCost();

                }, function () {
                    $scope.formError = 'Failed to load inspection details.';
                });
            };

            // ---- Save ----
            $scope.SaveInspection = function () {
                $scope.formError = '';
                $scope.formSuccess = '';

                if (!$scope.form.customerLocationId) {
                    $scope.formError = 'Location is required.'; return;
                }
                if (!$scope.form.typeOfRack) {
                    $scope.formError = 'Type of Rack is required.'; return;
                }

                // Validate deficiency descriptions
                var defError = '';
                $scope.deficiencies.forEach(function (d, i) {
                    if (!d.DeficiencyDescription || d.DeficiencyDescription.trim() === '') {
                        defError = 'Deficiency #' + (i + 1) + ' description is required.';
                    }
                });
                if (defError) { $scope.formError = defError; return; }

                $scope.saving = true;

                var payload = {
                    InternalInspectionID: $scope.inspectionId || 0,
                    CustomerLocationID: $scope.form.customerLocationId,
                    CustomerFacilityID: $scope.form.customerFacilityId || null,
                    AreaID: $scope.form.areaId || null,
                    TypeOfRack: $scope.form.typeOfRack,
                    InspectionDate: $scope.form.inspectionDate || '',
                    Area: $scope.form.area || '',
                    Row: $scope.form.row || '',
                    Aisle: $scope.form.aisle || '',
                    Bay: $scope.form.bay || '',
                    BeamLocation: $scope.form.beamLocation || '',
                    FrameSide: $scope.form.frameSide || '',
                    ReportedBy: $scope.form.reportedBy || '',
                    InspectionSummary: $scope.form.inspectionSummary || '',
                    Levels: BuildLevelsString(),
                    Deficiencies: $scope.deficiencies
                };

                $http({
                    url: '/api/pageview/saveInternalInspection',
                    method: 'POST',
                    data: payload,
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (res) {
                    var savedId = res.data;

                    if (!savedId || savedId <= 0) {
                        $scope.formError = 'Failed to save inspection.';
                        $scope.saving = false;
                        return;
                    }

                    // Upload photos if any selected
                    var fileInput = document.getElementById('inspectionPhotos');
                    if (fileInput && fileInput.files.length > 0) {
                        var formData = new FormData();
                        formData.append('inspectionId', savedId);
                        formData.append('customerId', 0); // resolved server-side from session
                        for (var i = 0; i < fileInput.files.length; i++) {
                            formData.append('files', fileInput.files[i]);
                        }
                        $http({
                            url: '/api/pageview/saveInternalInspectionPhotos',
                            method: 'POST',
                            data: formData,
                            transformRequest: angular.identity,
                            headers: { 'Content-Type': undefined }
                        }).then(function () {
                            $window.location.href =
                                '/Customer/customerInternalInspectionView?id=' + savedId;
                        }, function () {
                            // Photos failed but inspection saved — still redirect
                            $window.location.href =
                                '/Customer/customerInternalInspectionView?id=' + savedId;
                        });
                    } else {
                        $window.location.href =
                            '/Customer/customerInternalInspectionView?id=' + savedId;
                    }

                }, function (err) {
                    $scope.formError = err.data || 'An error occurred while saving.';
                    $scope.saving = false;
                });
            };
        }

        // ============================================================
        // CUSTOMER — VIEW PAGE
        // /Customer/customerInternalInspectionView
        // ============================================================
        if (path.indexOf('customerinternalinspectionview') !== -1) {

            $scope.inspection = null;

            $scope.$watch('inspectionId', function (val) {
                if (val && val !== '' && val !== '0') {
                    $http.get('/api/pageview/getInternalInspectionById', {
                        params: { id: val }
                    }).then(function (res) {
                        $scope.inspection = res.data;
                    }, function () {
                        $scope.inspection = null;
                    });
                }
            });
        }

        // ============================================================
        // ADMIN — LISTING PAGE
        // /Admin/adminInternalInspectionListing
        // ============================================================
        if (path.indexOf('admininternalinspectionlisting') !== -1) {

            $scope.inspectionList = [];
            $scope.adminFilter = {};
            $scope.adminLocationList = [];
            $scope.allCustomers = [];

            // Load customer dropdown
            $http.get('/api/pageview/getAllCustomers')
                .then(function (res) {
                    $scope.allCustomers = res.data || [];

                    // If ?id= passed from customer detail page — pre-fill and auto-search
                    $scope.$watch('adminCustomerId', function (val) {
                        if (val && val !== '' && val !== '0') {
                            $scope.adminFilter.CustomerID = String(val);
                            $scope.onAdminCustomerChange();
                            $scope.SearchInspections();
                        }
                    });
                });

            // ---- Customer change — reload location dropdown ----
            $scope.onAdminCustomerChange = function () {
                $scope.adminFilter.CustomerLocationID = '';
                $scope.adminLocationList = [];

                if (!$scope.adminFilter.CustomerID) return;

                $http.get('/api/pageview/getCustomerLocationByCustomerId', {
                    params: { id: $scope.adminFilter.CustomerID }
                }).then(function (res) {
                    $scope.adminLocationList = res.data || [];
                }, function () { });
            };

            // ---- Search ----
            $scope.SearchInspections = function () {
                $http({
                    url: '/api/pageview/getInternalInspectionsForAdmin',
                    method: 'POST',
                    data: $scope.adminFilter,
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (res) {
                    $scope.inspectionList = res.data || [];
                    $scope.liPaging.current = 1;
                }, function () {
                    $scope.inspectionList = [];
                });
            };

            // ---- Clear filter ----
            $scope.ClearAdminFilter = function () {
                $scope.adminFilter = {};
                $scope.adminLocationList = [];
                $scope.inspectionList = [];
                $scope.liPaging.current = 1;
            };
        }

        // ============================================================
        // ADMIN — VIEW PAGE
        // /Admin/adminInternalInspectionView
        // ============================================================
        if (path.indexOf('admininternalinspectionview') !== -1) {

            $scope.inspection = null;
            $scope.newStatus = '';
            $scope.updatingStatus = false;
            $scope.statusSuccess = '';
            $scope.statusError = '';

            $scope.$watch('inspectionId', function (val) {
                if (val && val !== '' && val !== '0') {
                    $http.get('/api/pageview/getInternalInspectionById', {
                        params: { id: val }
                    }).then(function (res) {
                        $scope.inspection = res.data;
                        $scope.newStatus = res.data.Status;
                    }, function () {
                        $scope.inspection = null;
                    });
                }
            });

            // ---- Update status ----
            $scope.UpdateStatus = function () {
                $scope.statusSuccess = '';
                $scope.statusError = '';
                $scope.updatingStatus = true;

                $http({
                    url: '/api/pageview/updateInternalInspectionStatus',
                    method: 'POST',
                    data: { id: $scope.inspectionId, status: $scope.newStatus },
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (res) {
                    $scope.updatingStatus = false;
                    if (res.data === true) {
                        $scope.statusSuccess = 'Status updated to "' + $scope.newStatus + '" successfully.';
                        $scope.inspection.Status = $scope.newStatus;
                    } else {
                        $scope.statusError = 'Failed to update status.';
                    }
                }, function () {
                    $scope.updatingStatus = false;
                    $scope.statusError = 'An error occurred.';
                });
            };

            // ---- Delete inspection ----
            $scope.DeleteInspection = function (id) {
                if (!confirm('Are you sure you want to delete this inspection? This cannot be undone.')) return;

                $http({
                    url: '/api/pageview/deleteInternalInspection',
                    method: 'POST',
                    data: { id: id },
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (res) {
                    if (res.data === true) {
                        $window.location.href = '/Admin/adminInternalInspectionListing';
                    } else {
                        alert('Failed to delete inspection.');
                    }
                }, function () {
                    alert('An error occurred.');
                });
            };
        }

    });