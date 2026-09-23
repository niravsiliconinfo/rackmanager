// ============================================================
// salesInternalInspectionCtrl.js - final
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
} catch (e) { }

angular.module('myApp')
    .controller('salesInternalInspectionCtrl', function ($scope, $http, $window, $filter, sharedFilterService) {
        var path = window.location.pathname.toLowerCase();
        $scope.getAllInternalInspectionBySales = [];

        $scope.viewby = 50;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.totalItems = 0;

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1;
        };

        init();

        function init() {

            var filters = sharedFilterService.getSalesInternalFilters();

            if (window.location.pathname === "/SalesManager/ManageInternalInspectionSales") {

                if (filters && Object.keys(filters).length > 0) {
                    loadSalesInternalInspections(filters);
                }
                else {
                    loadSalesInternalInspections();
                }
            }
        }

        $scope.$on("salesInternalFiltersUpdated", function (event, filters) {

            console.log("Sales Internal Filters", filters);

            loadSalesInternalInspections(filters);

        });

        function loadSalesInternalInspections(filter) {

            console.log("Loading Sales Internal", filter);

            $http.post("/api/pageview/getSalesInternalInspections", filter)
                .then(function (response) {

                    $scope.getAllInternalInspectionBySales = response.data || [];

                    $scope.totalItems =
                        $scope.getAllInternalInspectionBySales.length;

                    $scope.currentPage = 1;

                }, function (error) {

                    console.log(error);

                });
        }

        $scope.refreshInternalInspections = function () {
            loadSalesInternalInspections();
        };     
        $scope.viewInternalInspection = function (id) {
            $window.location.href =
                "/SalesManager/ViewInternalIncidentReportView?id=" + id;
        };
        // ============================================================
        // Sales Manager - VIEW
        // ============================================================
        var path = window.location.pathname.toLowerCase();

        if (path.indexOf('/salesmanager/viewinternalincidentreportview') !== -1) {

            $scope.inspection = null;
            $scope.newStatus = '';
            $scope.updatingStatus = false;
            $scope.statusSuccess = '';
            $scope.statusError = '';
            $scope.downloading = false;
            $scope.downloadError = '';

            var inspId = new URLSearchParams(window.location.search).get('id');

            if (inspId && inspId !== '0') {

                $http.get('/api/pageview/getInternalInspectionById', {
                    params: { id: parseInt(inspId) }
                }).then(function (res) {

                    $scope.inspection = res.data;
                    $scope.newStatus = res.data.Status;
                    $scope.inspectionId = parseInt(inspId);
                    console.log(res.data);  
                }, function () {

                    $scope.inspection = null;

                });
            }

            $scope.DownloadPdf = function () {

                $scope.downloading = true;

                $http.get('/api/pageview/getInternalInspectionPdfHtml', {
                    params: { id: parseInt(inspId) }
                }).then(function (res) {

                    $scope.downloading = false;

                    var win = window.open('', '_blank');
                    win.document.open();
                    win.document.write(res.data.html);
                    win.document.close();

                    win.onload = function () {
                        win.print();
                    };

                }, function () {

                    $scope.downloading = false;
                    $scope.downloadError = 'Failed to generate report.';

                });
            };
        }

    });
