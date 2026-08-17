(function () {
    'use strict';

    var app = angular.module('myApp');

    //  startFrom filter (re-declared safely) 
    if (!app._startFromRegistered) {
        app.filter('startFrom', function () {
            return function (input, start) {
                if (input) { start = +start; return input.slice(start); }
                return [];
            };
        });
        app._startFromRegistered = true;
    }

    // 
    //  adminInternalInspectionCtrl
    // 
    app.controller('adminInternalInspectionCtrl', ['$scope', '$http',function ($scope, $http) {
        
            //  Shared pagination state 
            $scope.viewby      = '10';
            $scope.currentPage = 1;
            $scope.itemsPerPage = 10;
            $scope.maxSize     = 10;
            $scope.totalItems  = 0;

            $scope.setItemsPerPage = function (num) {
                $scope.itemsPerPage = +num;
                $scope.currentPage  = 1;
            };

            //  Route detection 
            var pathname = window.location.pathname.toLowerCase();
        
        if (pathname === '/admin/internalinspectionlisting') {
                initList();
            } else if (pathname === '/admin/internalinspectionview') {
                initView();
            }

            // 
            //  LIST PAGE
            // 
        function initList() {
            console.log('initList');
                $scope.allInspections = [];
                $scope.loading        = false;
                $scope.filterStatus   = '';
                $scope.filterDateFrom = '';
                $scope.filterDateTo   = '';
                $scope.customerName   = '';

                // Read optional ?id= (customerId) from query string
                var customerId = getParam('id');
                console.log(customerId);
                loadInspections(customerId);

                $scope.applyFilter = function () {
                    loadInspections(customerId);
                };

                // Filter by status client-side
                $scope.filterByStatus = function (item) {
                    if (!$scope.filterStatus) return true;
                    return item.Status === $scope.filterStatus;
                };

                $scope.downloadPdf = function (id) {
                    openPdf(id);
                };
            }

            function loadInspections(customerId) {
                $scope.loading = true;
                var url = customerId
                    ? '/api/pageview/getInternalInspectionsByCustomerId?customerId=' + customerId
                    : '/api/pageview/getMyInternalInspections';

                $http.get(url)
                    .then(function (res) {
                        var data = res.data || [];

                        // Client-side date filter
                        if ($scope.filterDateFrom) {
                            var from = new Date($scope.filterDateFrom);
                            data = data.filter(function (d) {
                                return new Date(d.InternalInspectionDate) >= from;
                            });
                        }
                        if ($scope.filterDateTo) {
                            var to = new Date($scope.filterDateTo);
                            to.setHours(23, 59, 59);
                            data = data.filter(function (d) {
                                return new Date(d.InternalInspectionDate) <= to;
                            });
                        }

                        $scope.allInspections = data;
                        $scope.totalItems     = data.length;

                        if (data.length > 0 && data[0].CustomerName) {
                            $scope.customerName = data[0].CustomerName;
                        }
                        $scope.loading = false;
                    }, function (err) {
                        console.error('Error loading internal inspections:', err);
                        $scope.loading = false;
                    });
            }

            // 
            //  VIEW PAGE
            // 
            function initView() {
                $scope.inspection     = null;
                $scope.loading        = true;
                $scope.loadError      = null;
                $scope.statusMessage  = '';
                $scope.statusError    = '';
                $scope.statusUpdating = false;

                var id = getParam('id');
                if (!id) {
                    $scope.loadError = 'No inspection ID specified in URL.';
                    $scope.loading   = false;
                    return;
                }

                $http.get('/api/pageview/getInternalInspectionById?id=' + id)
                    .then(function (res) {
                        if (!res.data) {
                            $scope.loadError = 'Inspection not found.';
                        } else {
                            $scope.inspection = res.data;
                        }
                        $scope.loading = false;
                    }, function (err) {
                        console.error('Error loading inspection:', err);
                        $scope.loadError = 'Failed to load inspection. Please try again.';
                        $scope.loading   = false;
                    });
            }

            //  Update whole inspection status 
            $scope.updateInspectionStatus = function (newStatus) {
                if (!$scope.inspection) return;
                $scope.statusUpdating = true;
                $scope.statusMessage  = '';
                $scope.statusError    = '';

                $http.post('/api/pageview/updateInternalInspectionStatus',
                    { id: $scope.inspection.InternalInspectionID, status: newStatus })
                    .then(function (res) {
                        $scope.statusUpdating = false;
                        if (res.data === true || res.data === 'true' || res.data) {
                            $scope.inspection.Status = newStatus;
                            $scope.statusMessage = 'Inspection status updated to "' + newStatus + '".';
                        } else {
                            $scope.statusError = 'Could not update status. Please try again.';
                        }
                    }, function (err) {
                        $scope.statusUpdating = false;
                        $scope.statusError    = 'Server error updating status.';
                        console.error(err);
                    });
            };

            //  Approve / Reject individual deficiency 
            $scope.updateDeficiencyStatus = function (def, newStatus) {
                def.updating = true;
                def.feedback = '';

                $http.post('/api/pageview/updateInternalDeficiencyStatus',
                    { id: def.InternalInspectionDeficiencyID, status: newStatus })
                    .then(function (res) {
                        def.updating = false;
                        if (res.data === true || res.data === 'true' || res.data) {
                            def.Status      = newStatus;
                            def.feedbackOk  = true;
                            def.feedback    = newStatus === 'Approved' ? '✓ Approved' : '✗ Rejected';
                        } else {
                            def.feedbackOk = false;
                            def.feedback   = 'Update failed.';
                        }
                    }, function (err) {
                        def.updating   = false;
                        def.feedbackOk = false;
                        def.feedback   = 'Server error.';
                        console.error(err);
                    });
            };

            //  PDF download 
            $scope.downloadPdf = function (id) {
                openPdf(id);
            };

            //  Helpers 
            function openPdf(id) {
                $http.get('/api/pageview/getInternalInspectionPdfHtml?id=' + id)
                    .then(function (res) {
                        if (!res.data || !res.data.html) {
                            alert('PDF content could not be generated.');
                            return;
                        }
                        var win = window.open('', '_blank', 'width=900,height=700');
                        if (!win) { alert('Please allow pop-ups to download the PDF.'); return; }
                        win.document.open();
                        win.document.write(res.data.html);
                        win.document.close();
                        // Trigger print dialog (browser saves as PDF)
                        win.onload = function () { win.focus(); win.print(); };
                    }, function (err) {
                        console.error('PDF generation error:', err);
                        alert('Failed to generate PDF.');
                    });
            }

            function getParam(name) {
                var url = window.location.href;
                name = name.replace(/[\[\]]/g, '\\$&');
                var regex   = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
                var results = regex.exec(url);
                if (!results)    return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, ' '));
            }
        }
    ]);

})();
