(function () {
    'use strict';

    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp')
        .controller('incidentListCtrl', incidentListCtrl);

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

    app.controller('incidentListCtrl', incidentListCtrl);

    incidentListCtrl.$inject = ['$scope', '$http', 'sharedFilterService', '$rootScope'];

    function incidentListCtrl($scope, $http, sharedFilterService, $rootScope) {
        console.log('-----------incidentListCtrl--------------');
        $scope.getAllIncidentByCustomerId = [];

       

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
                        
           
            var filters = sharedFilterService.getIncidentFilters();

            if (window.location.pathname == "/Customer/IncidentReport") {

                if (filters && Object.keys(filters).length > 0) {
                    loadIncident(filters);
                } else {
                    loadIncident();
                }
            }

            if (window.location.pathname == "/Admin/IncidentReportList") {
                console.log('Calling ----> loadIncidentAdminList');
                loadIncidentAdminList();
            } 
        }

   
        $scope.$on("incidentFiltersUpdated", function (event, filters) {
            console.log("Received Customer Filters", filters);
            loadIncident(filters);
        });

        function loadIncidentAdminList()
        {
            var para = window.location.search;
            console.log('para loadIncidentAdminList', para);
            para = para.replace('?id=', '');
            console.log('Calling ----> loadIncidentAdminList');
            $http.get('/api/pageview/getAllIncidentAdminByCustomerId', { params: { id: para } }).then(function (response){
                $scope.getAllIncidentByCustomerId = response.data;
                console.log('$scope.getAllIncidentByCustomerId', $scope.getAllIncidentByCustomerId);
                if ($scope.getAllIncidentByCustomerId != null) { $scope.getAllIncidentByCustomerIdcount = $scope.getAllIncidentByCustomerId.length; }
                else { $scope.getAllIncidentByCustomerIdcount = 0; }
                $scope.totalgetAllIncidentByCustomerId = $scope.getAllIncidentByCustomerIdcount;;
                }, function (error) {
                    console.error("Error loading due inspections:", error);
                });
        }
       

        //function loadIncident(filter) {
        //    console.log('Calling ----> loadIncident');
        //    const filters = filter;
        //    console.log("------XXXXXXXXXXXXXXXXXXXX------ CALL ------- -----getDueInspectionsCustomerFilters");
        //    $http.post('/api/pageview/getAllIncidentByCustomerId', filters)
        //        .then(function (response) {
        //            $scope.getAllIncidentByCustomerId = response.data;
        //            console.log('$scope.getAllIncidentByCustomerId', $scope.getAllIncidentByCustomerId);
        //            if ($scope.getAllIncidentByCustomerId != null) { $scope.getAllIncidentByCustomerIdcount = $scope.getAllIncidentByCustomerId.length; }
        //            else { $scope.getAllIncidentByCustomerIdcount = 0; }
        //            $scope.totalgetAllIncidentByCustomerId = $scope.getAllIncidentByCustomerIdcount;
        //        }, function (error) {
        //            console.error("Error loading due inspections:", error);
        //        });
        //}
        function loadIncident(filter) {

            filter = filter || {};

            console.log('Calling ----> loadIncident', filter);
            console.log(JSON.stringify(filter));
            $http.post('/api/pageview/getAllIncidentByCustomerId', filter)
                .then(function (response) {

                    $scope.getAllIncidentByCustomerId = response.data || [];

                    $scope.getAllIncidentByCustomerIdcount =
                        $scope.getAllIncidentByCustomerId.length;

                    $scope.totalgetAllIncidentByCustomerId =
                        $scope.getAllIncidentByCustomerIdcount;

                    // Reset paging
                    $scope.currentPage = 1;
                    $scope.pageChanged();

                }, function (error) {
                    console.error("Error loading incidents:", error);
                });
        }
       

        // Expose refresh methods for button calls
        $scope.refreshIncidents = function () {
            loadIncident();
        };

        //$rootScope.$on('incidentFilterUpdated', function () {
        //    console.error("Load function when click on button for incidentFilterUpdated");
        //    const filters = sharedFilterService.getIncidentFilters();
        //    console.error("Load function when click on button for incidentFilterUpdated");
        //    loadIncident(filters);
        //});       
       

        $http.get('/api/pageview/getAllIncidentByCustomerId').then(function (response) {
            $scope.getAllIncidentByCustomerId = response.data;
            console.log('$scope.getAllIncidentByCustomerId', $scope.getAllIncidentByCustomerId);
            if ($scope.getAllIncidentByCustomerId != null) { $scope.getAllIncidentByCustomerIdcount = $scope.getAllIncidentByCustomerId.length; }
            else { $scope.getAllIncidentByCustomerIdcount = 0; }
            $scope.totalgetAllIncidentByCustomerId = $scope.getAllIncidentByCustomerIdcount;
        }, function (response) {
            $scope.waiting = false;
        });
    }
})();