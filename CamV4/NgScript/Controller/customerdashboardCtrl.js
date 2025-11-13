(function () {
    'use strict';

    angular.module('myApp')
        .controller('customerdashboardCtrl', customerdashboardCtrl);

    app.controller('customerdashboardCtrl', customerdashboardCtrl);

    customerdashboardCtrl.$inject = ['$scope', '$http', '$filter', '$window', '$timeout', 'Upload', '$document', '$location', '$interval'];

    function customerdashboardCtrl($scope, $http, $filter, $window, $timeout, Upload, $document, $location, $interval, $rootScope) {

        if (window.location.pathname == "/Customer/Index") {

            $http.get('/api/pageview/getRecentCompletedInspectionbyCustomerId').then(function (response) {
                $scope.getRecentCompletedInspectionbyCustomerId = response.data;
                console.log('$scope.getRecentCompletedInspectionbyCustomerId', $scope.getRecentCompletedInspectionbyCustomerId);
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getRecentInspectionbyCustomerId').then(function (response) {
                $scope.getRecentInspectionbyCustomerId = response.data;
                console.log('$scope.getRecentInspectionbyCustomerId', $scope.getRecentInspectionbyCustomerId);
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getDueInspectionByCustomerId').then(function (response) {
                $scope.getDueInspectionByCustomerId = response.data;
                console.log('$scope.getDueInspectionByCustomerId', $scope.getDueInspectionByCustomerId);
                if ($scope.getDueInspectionByCustomerId != null) { $scope.getDueInspectionByCustomerIdcount = $scope.getDueInspectionByCustomerId.length; }
                else { $scope.getDueInspectionByCustomerIdcount = 0; }
                $scope.totalgetDueInspectionByCustomerId = $scope.getDueInspectionByCustomerIdcount;
            }, function (response) {
                $scope.waiting = false;
            });

            var min = 2022,
                max = new Date().getFullYear();
            var range = [];

            for (var i = max; i >= min; i--) {
                $('#selectedYearCustomer').append($('<option />').val(i).html(i));
            }

            $scope.years = range;

            console.log('Selected Year:', '');
            var selectedYear = $('#selectedYearCustomer').val();
            console.log('Selected Year:', selectedYear);
            $http.get('/api/pageview/getDeficienciesBreakdownCategories', { params: { year: selectedYear } }).then(function (response) {
                $scope.getDeficienciesBreakdownCategorieslist = response.data;
                console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
            }, function (response) {
                $scope.waiting = false;
            });
        }
        

        $scope.filterInspectionsByStatus = function () {
            const selected = $scope.InspectionStatusLayout
                .filter(s => s.selected)
                .map(s => s.InspectionStatusId);

            // Update shared service
            inspectionFilterService.setSelectedStatuses(selected);

            // Broadcast change
            $rootScope.$broadcast('inspectionFilterChanged', selected);
        };
       
        $('#selectedYearCustomer').change(function () {
            var selectedYear = $(this).val();
            console.log('Selected Year:', '');
            /*var selectedYear = $('#selectedYear').val();*/
            console.log('Selected Year:', selectedYear);
            $http.get('/api/pageview/getDeficienciesBreakdownCategories', { params: { year: selectedYear } }).then(function (response) {
                $scope.getDeficienciesBreakdownCategorieslist = response.data;
                console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
            }, function (response) {
                $scope.waiting = false;
            });
        });
        
        $scope.chartDataByYearCustomerAngular = function (selectedValue) {
            // Your logic here, e.g., call your API with the selected value
            //var year = $scope.selectedYear;
            var selectedYear = $('#selectedYear').val();
            console.log('Selected Year:', selectedYear);
            // Make your API call here
            $http.get('/api/pageview/getDeficienciesBreakdownCategories?year=' + selectedYear)
                .then(function (response) {
                    $scope.getDeficienciesBreakdownCategorieslist = response.data;
                    console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
                }, function (response) {
                    $scope.waiting = false;
                });
        };
    }
})();