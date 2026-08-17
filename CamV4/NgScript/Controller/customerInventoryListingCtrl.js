// NgScript/Controller/customerInventoryListingCtrl.js
// Customer Spare Material Listing.
// Loads all files for the logged-in customer by default.
// Supports sidebar filter via sessionStorage + broadcast.

var app = angular.module('myApp');

app.controller('customerInventoryListingCtrl',
    ['$scope', '$http', '$timeout',
        function ($scope, $http, $timeout) {

            //  Pagination 
            $scope.viewby = '10';
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = '10';
            $scope.setItemsPerPage = function (num) {
                $scope.itemsPerPage = num;
                $scope.currentPage = 1;
            };

            //  State 
            $scope.fileList = [];
            $scope.search = '';
            $scope.loading = false;
            $scope.pageError = '';

            //  Load files 
            // No filter = GET getInventoryFiles (server scopes to customer automatically)
            // With filter = POST getInventoryFilesFiltered
            $scope.LoadFiles = function (filter) {
                $scope.loading = true;
                $scope.pageError = '';

                var hasFilter = filter && (
                    filter.Region || filter.ProvinceID || filter.CityID ||
                    filter.LocationID || filter.FacilityID || filter.AreaID ||
                    filter.Status || filter.Search
                );

                var request = hasFilter
                    ? $http.post('/api/pageview/getInventoryFilesFiltered', filter)
                    : $http.get('/api/pageview/getInventoryFiles');

                request
                    .then(function (res) {
                        $scope.fileList = Array.isArray(res.data) ? res.data : [];
                        $scope.loading = false;
                        $scope.currentPage = 1;
                    })
                    .catch(function (err) {
                        $scope.loading = false;
                        $scope.fileList = [];
                        $scope.pageError = 'Could not load files: ' +
                            (err.data ? (err.data.Message || JSON.stringify(err.data))
                                : err.statusText);
                    });
            };

            //  Sidebar filter broadcast 
            //$scope.$on('inventoryFiltersApplied', function (e, filter) {
            //    $scope.LoadFiles(filter);
            //});
            $scope.$on('internalInventoryFiltersUpdated',
                function (event, filters) {
                    console.log('internalInventoryFiltersUpdated received', filters);
                    $scope.LoadFiles(filters);  // Your load function
                });

            //  Export single file 
            $scope.ExportFile = function (fileId) {
                window.location.href =
                    '/api/pageview/exportInventoryFile?fileId=' + fileId;
            };

            //  Init — read sessionStorage filter or load all 
            (function init() {
                try {
                    var raw = sessionStorage.getItem('inventoryFilter');
                    var filter = raw ? JSON.parse(raw) : null;
                    $scope.LoadFiles(filter);
                } catch (e) {
                    $scope.LoadFiles(null);
                }
            })();

        }]);
