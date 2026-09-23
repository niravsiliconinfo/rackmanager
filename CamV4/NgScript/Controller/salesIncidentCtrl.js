(function () {
    'use strict';

    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp')
        .controller('salesIncidentCtrl', salesIncidentCtrl);

    app.filter('startFrom', function () {
        return function (input, start) {
            if (input) {
                start = +start;
                return input.slice(start);
            }
            return [];
        };
    });

    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];
            if (angular.isArray(items)) {
                items.forEach(function (item) {
                    var itemMatches = false;

                    var keys = Object.keys(props);
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            return out;
        };
    });

    app.controller('salesIncidentCtrl', salesIncidentCtrl);

    salesIncidentCtrl.$inject = ['$scope', '$http', 'sharedFilterService', '$rootScope'];

    function salesIncidentCtrl($scope, $http, sharedFilterService, $rootScope) {
        console.log('-----------salesIncidentCtrl--------------');
        
        $scope.getAllIncidentBySales = [];
        $scope.totalItems = 0;
        
        $scope.salesIncidentFilter = sharedFilterService.getSalesIncidentFilters() || {};

        $scope.viewby = '50';
        $scope.currentPage = '1';
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = '10'; //Number of pager buttons to show

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () { };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        init();

        function init() {
            var filters = sharedFilterService.getSalesIncidentFilters();
            if (window.location.pathname ==
                "/SalesManager/ManageIncidentReportSales") {
                if (filters && Object.keys(filters).length > 0) {
                    loadSalesIncident(filters);
                } else {
                    loadSalesIncident();

                }
            }
        }

        //$scope.$on(
        //    'incidentFiltersUpdated',
        //    function (event, filters) {
        //        loadIncidents(filters);
        //    });
        $scope.$on("salesIncidentFiltersUpdated", function (event, filters) {
            console.log("Received Sales Filters", filters);
            loadSalesIncident(filters);
        });

        function loadSalesIncident(filter) {

            console.log("Sales Incident Filter", filter);

            $http.post("/api/pageview/getSalesIncidentListing", filter)
                .then(function (response) {

                    $scope.getAllIncidentBySales = response.data || [];

                    $scope.totalItems = $scope.getAllIncidentBySales.length;
                    $scope.currentPage = 1;

                    console.log("Sales Incidents Loaded:",
                        $scope.totalItems);

                }, function (error) {

                    console.log(error);

                });
        }

        // Expose refresh methods for button calls
        $scope.refreshIncidents = function () {
            loadSalesIncident();
        };       
    }
})();