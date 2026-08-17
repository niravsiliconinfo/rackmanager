(function () {
    'use strict';

    angular.module('myApp')
        .controller('customerdashboardCtrl', customerdashboardCtrl);

    customerdashboardCtrl.$inject = [
        '$scope', '$http', '$filter', '$window', '$timeout', '$q',
        'Upload', '$document', '$location', '$interval'
    ];

    function customerdashboardCtrl(
        $scope, $http, $filter, $window, $timeout, $q,
        Upload, $document, $location, $interval
    ) {

        // 
        //  SECTION A – EXISTING FEATURES  (untouched)
        // 

        if (window.location.pathname === "/Customer/Index") {

            $http.get('/api/pageview/getRecentCompletedInspectionbyCustomerId').then(function (res) {
                $scope.getRecentCompletedInspectionbyCustomerId = res.data;
            }, function () { $scope.waiting = false; });

            $http.get('/api/pageview/getRecentInspectionbyCustomerId').then(function (res) {
                $scope.getRecentInspectionbyCustomerId = res.data;
            }, function () { $scope.waiting = false; });

            $http.get('/api/pageview/getDueInspectionByCustomerId').then(function (res) {
                $scope.getDueInspectionByCustomerId = res.data;
                $scope.getDueInspectionByCustomerIdcount = ($scope.getDueInspectionByCustomerId || []).length;
                $scope.totalgetDueInspectionByCustomerId = $scope.getDueInspectionByCustomerIdcount;
            }, function () { $scope.waiting = false; });

            // existing year dropdown population
            var minYear = 2022, maxYear = new Date().getFullYear();
            for (var y = maxYear; y >= minYear; y--) {
                $('#selectedYearCustomer').append($('<option />').val(y).html(y));
            }

            // 
            //  SECTION B – FILTER STATE  (all vars prefixed custDash_)
            // 

            $scope.custDash_selectedYear = maxYear;
            $scope.custDash_selectedLocationId = null;
            $scope.custDash_selectedFacilityId = null;

            $scope.custDash_locationList = [];
            $scope.custDash_facilityList = [];

            //  Loader state 
            $scope.custDash_isLoading = false;   // drives the progress bar
            $scope.custDash_loadError = false;   // drives the error banner

            //  Live status counters (replace @Model.* static values) 
            $scope.custDash_cnt = {
                InspectionsDue: 0,
                InProgress: 0,
                SentForApproval: 0,
                ReportComplete: 0,
                QuotationRequested: 0,
                AwaitingApproval: 0,
                QuotationApproved: 0,
                RepairCompleted: 0,
                Finished: 0
            };

            //  Chart instances 
            var custDash_pieChartInstance = null;
            var custDash_trendChartInstance = null;

            //  In-flight request canceller (prevents stale responses) 
            var custDash_pendingCanceller = null;

            //  Debounce timer 
            var custDash_debounceTimer = null;
            var CUSTDASH_DEBOUNCE_MS = 300;   // wait 300 ms after last change

            // 
            //  LOADER helpers
            // 

            function custDash_ShowLoader() {
                $scope.custDash_isLoading = true;
                $scope.custDash_loadError = false;
                if (typeof window.custDash_uiShowLoader === 'function') {
                    window.custDash_uiShowLoader();
                }
            }

            function custDash_HideLoader() {
                $scope.custDash_isLoading = false;
                if (typeof window.custDash_uiHideLoader === 'function') {
                    window.custDash_uiHideLoader();
                }
            }

            function custDash_ShowError() {
                $scope.custDash_isLoading = false;
                $scope.custDash_loadError = true;
                if (typeof window.custDash_uiShowError === 'function') {
                    window.custDash_uiShowError();   // handles toast + auto-hide
                } else {
                    $timeout(function () {
                        $scope.custDash_loadError = false;
                    }, 5000);
                }
            }

            // 
            //  B1: Load Locations
            // 
            $scope.custDash_LoadLocations = function () {
                $http.get('/api/pageview/getLocationByCustomer').then(function (res) {
                    $scope.custDash_locationList = res.data || [];
                }, function (err) {
                    console.error('CustDash: locations error', err);
                });
            };

            // 
            //  B2: Load Facilities
            // 
            $scope.custDash_LoadFacilities = function (locationId) {
                var request = locationId
                    ? $http.get('/api/pageview/getFacilityByLocationId', { params: { id: locationId } })
                    : $http.get('/api/pageview/getFacilityByCustomer');

                request.then(function (res) {
                    $scope.custDash_facilityList = res.data || [];
                    if (locationId) {
                        $scope.custDash_selectedFacilityId = null;
                    }
                }, function (err) {
                    console.error('CustDash: error loading facilities', err);
                });
            };

            // 
            //  B3: Load Dashboard Data  (debounced + cancellable)
            // 
            $scope.custDash_LoadDashboardData = function () {

                // Cancel any pending debounce
                if (custDash_debounceTimer) {
                    $timeout.cancel(custDash_debounceTimer);
                }
                
                custDash_debounceTimer = $timeout(function () {

                    // Abort previous in-flight HTTP request if still pending
                    if (custDash_pendingCanceller) {
                        custDash_pendingCanceller.resolve('cancelled');
                    }
                    custDash_pendingCanceller = $q.defer();

                    custDash_ShowLoader();

                    var params = { year: $scope.custDash_selectedYear };
                    if ($scope.custDash_selectedLocationId) {
                        params.locationId = $scope.custDash_selectedLocationId;
                    }
                    if ($scope.custDash_selectedFacilityId) {
                        params.facilityId = $scope.custDash_selectedFacilityId;
                    }

                    $http.get('/api/pageview/GetCustomerDashboardData', {
                        params: params,
                        timeout: custDash_pendingCanceller.promise   // cancellation hook
                    }).then(function (res) {

                        custDash_pendingCanceller = null;
                        var data = res.data;

                        //  Counters (live, replaces @Model.* razor values) 
                        if (data.StatusCounts) {
                            $scope.custDash_cnt = {
                                InspectionsDue: data.StatusCounts.InspectionsDue || 0,
                                InProgress: data.StatusCounts.InProgress || 0,
                                SentForApproval: data.StatusCounts.SentForApproval || 0,
                                ReportComplete: data.StatusCounts.ReportComplete || 0,
                                QuotationRequested: data.StatusCounts.QuotationRequested || 0,
                                AwaitingApproval: data.StatusCounts.AwaitingApproval || 0,
                                QuotationApproved: data.StatusCounts.QuotationApproved || 0,
                                RepairCompleted: data.StatusCounts.RepairCompleted || 0,
                                Finished: data.StatusCounts.Finished || 0
                            };
                        }

                        //  Category table 
                        $scope.custDash_categoryBreakdownList = data.CategoryBreakdown || [];

                        //  Charts 
                        custDash_RenderPieChart(data.PieData || []);
                        custDash_RenderTrendChart(data.TrendData || []);

                        custDash_HideLoader();

                    }, function (err) {
                        // Ignore intentional cancellations
                        if (err && (err.status === -1 || err.data === 'cancelled')) {
                            return;
                        }
                        console.error('CustDash: dashboard data error', err);
                        custDash_pendingCanceller = null;
                        custDash_ShowError();
                    });

                }, CUSTDASH_DEBOUNCE_MS);
            };

            // 
            //  B4: Event Handlers
            // 

            $scope.custDash_OnLocationChange = function () {
                $scope.custDash_LoadFacilities($scope.custDash_selectedLocationId);
                $scope.custDash_LoadDashboardData();
            };

            $scope.custDash_OnFacilityChange = function () {
                $scope.custDash_LoadDashboardData();
            };

            $scope.custDash_OnYearChange = function () {
                $scope.custDash_LoadDashboardData();
            };

            // 
            //  B5: Chart Renderers
            // 

            function custDash_RenderPieChart(pieData) {
                var canvas = document.getElementById('custdash-pie-chart');
                if (!canvas) return;

                if (custDash_pieChartInstance) {
                    custDash_pieChartInstance.destroy();
                    custDash_pieChartInstance = null;
                }

                if (!pieData.length) return;

                custDash_pieChartInstance = new Chart(canvas.getContext('2d'), {
                    type: 'pie',
                    data: {
                        labels: pieData.map(function (d) { return d.Classifications; }),
                        datasets: [{
                            label: 'Deficiencies',
                            backgroundColor: pieData.map(function (d) { return d.ClassificationsColor; }),
                            data: pieData.map(function (d) { return d.InspectionDeficiencyCnt; })
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: { duration: 400 },
                        plugins: { legend: { display: true, position: 'bottom' } }
                    }
                });
            }

            function custDash_RenderTrendChart(trendData) {
                var canvas = document.getElementById('custdash-trend-chart');
                if (!canvas) return;

                if (custDash_trendChartInstance) {
                    custDash_trendChartInstance.destroy();
                    custDash_trendChartInstance = null;
                }

                if (!trendData.length) return;

                custDash_trendChartInstance = new Chart(canvas.getContext('2d'), {
                    type: 'bar',
                    data: {
                        labels: trendData.map(function (d) { return d.Year; }),
                        datasets: [
                            { label: 'Minor', data: trendData.map(function (d) { return d.Minor; }), backgroundColor: '#00CC00' },
                            { label: 'Intermediate', data: trendData.map(function (d) { return d.Intermediate; }), backgroundColor: '#FFFF00' },
                            { label: 'Major', data: trendData.map(function (d) { return d.Major; }), backgroundColor: '#FF0000' }
                        ]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: { duration: 400 },
                        scales: {
                            y: { beginAtZero: true, title: { display: true, text: 'Deficiency Count' } },
                            x: { title: { display: true, text: 'Year' } }
                        },
                        plugins: { legend: { display: true, position: 'top' } }
                    }
                });
            }

            // 
            //  B6: Initialise
            // 
            $scope.custDash_LoadLocations();
            $scope.custDash_LoadFacilities(null);
            $scope.custDash_LoadDashboardData();

            // Clean up on scope destroy to prevent memory leaks
            $scope.$on('$destroy', function () {
                if (custDash_debounceTimer) { $timeout.cancel(custDash_debounceTimer); }
                if (custDash_pendingCanceller) { custDash_pendingCanceller.resolve('cancelled'); }
                if (custDash_pieChartInstance) { custDash_pieChartInstance.destroy(); }
                if (custDash_trendChartInstance) { custDash_trendChartInstance.destroy(); }
            });
        }

        // 
        //  SECTION C – EXISTING year-change handler (backward compat)
        // 

        $('#selectedYearCustomer').change(function () {
            var selectedYear = $(this).val();

            $scope.$apply(function () {
                $scope.custDash_selectedYear = parseInt(selectedYear, 10);
                $scope.custDash_LoadDashboardData();
            });

            // existing getDeficienciesBreakdownCategories call (unchanged)
            $http.get('/api/pageview/getDeficienciesBreakdownCategories', { params: { year: selectedYear } }).then(function (res) {
                $scope.getDeficienciesBreakdownCategorieslist = res.data;
            }, function () { $scope.waiting = false; });
        });

        $scope.chartDataByYearCustomerAngular = function () {
            var selectedYear = $('#selectedYear').val();
            $http.get('/api/pageview/getDeficienciesBreakdownCategories?year=' + selectedYear)
                .then(function (res) {
                    $scope.getDeficienciesBreakdownCategorieslist = res.data;
                }, function () { $scope.waiting = false; });
        };

        $scope.filterInspectionsByStatus = function () {
            var selected = ($scope.InspectionStatusLayout || [])
                .filter(function (s) { return s.selected; })
                .map(function (s) { return s.InspectionStatusId; });

            if (typeof inspectionFilterService !== 'undefined') {
                inspectionFilterService.setSelectedStatuses(selected);
            }
        };
    }
})();
