(function () {
    'use strict';

    angular.module('myApp')
        .controller('menuCtrl', menuCtrl)
        .factory('sharedInspectionDueFilterService', function () {
            var filterData = {};
            return {
                get: function () { return filterData; },
                set: function (data) { filterData = data; },
                reset: function () { filterData = {}; }
            };
        })
        .factory('sharedinsepctionFilterService', function () {
            var filterData = {};
            return {
                get: function () { return filterData; },
                set: function (data) { filterData = data; },
                reset: function () { filterData = {}; }
            };
        });

    app.controller('menuCtrl', menuCtrl);
    menuCtrl.$inject = ['$scope', '$http', 'sharedInspectionDueFilterService', 'sharedinsepctionFilterService'];


    function menuCtrl($scope, $http, sharedInspectionDueFilterService, sharedinsepctionFilterService) {
        console.log('----------- menuCtrl loaded -----------');

        // Default filter models
        $scope.inspectionDueFilter = {};
        $scope.insepctionFilter = {};



        $scope.SearchInspectionDueMenu = function () {
            sharedInspectionDueFilterService.set({
                InspectionTypeId: $scope.inspectionDueFilter.InspectionTypeCode,
                province: $scope.inspectionDueFilter.province,
                Region: $scope.inspectionDueFilter.Region,
                city: $scope.inspectionDueFilter.city,
                location: $scope.inspectionDueFilter.location
            });

            // Manually trigger reload in customerinspectionCtrl
            var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
            if (scope && scope.refreshDueInspections) {
                scope.refreshDueInspections();
            }
        };

        $scope.applyInspectionFilters = function () {
            sharedinsepctionFilterService.set({
                InspectionStatusId: getSelectedInspectionStatusIds(),
                InspectionTypeId: $scope.inspectionFilter.InspectionTypeCode,
                province: $scope.inspectionFilter.province,
                Region: $scope.inspectionFilter.Region,
                city: $scope.inspectionFilter.city,
                location: $scope.inspectionFilter.location
            });

            var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
            if (scope && scope.refreshAllInspections) {
                scope.refreshAllInspections();
            }
        };

        function getSelectedInspectionStatusIds() {
            // TODO: implement based on your UI
            return [1, 2, 3, 4, 5, 6, 7, 8, 9]; // sample static
        }
        if (window.location.pathname == "/Customer/ManageInspectionDue") {
            $http.get('/api/pageview/getAllInspectionType').then(function (response) {
                $scope.InspectionTypeLayoutDue = response.data;
            });

            $http.get('/api/pageview/getProvincebyCountryId', { params: { id: 32 } }).then(function (response) {
                $scope.getProvincebyCountryIdLayout = response.data;
            });

            $http.get('/api/pageview/getProvincebyCountryIdByCustomer').then(function (response) {
                $scope.getProvincebyCountryIdByCustomer = response.data;
            });
            $http.get('/api/pageview/getProvincebyCountryIdByCustomer').then(function (response) {
                $scope.getProvincebyCountryIdByCustomerInspection = response.data;
            });

            $scope.getCitybyProvinceIdByCustomerDue = function () {
                $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.inspectionDueFilter.province } }).then(function (response) {
                    $scope.cityListDue = response.data;
                });
            };
            $scope.getLocationbyCityIdByCustomerDue = function () {
                $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.inspectionDueFilter.city } }).then(function (response) {
                    $scope.locationListDue = response.data;
                });
            };
        }
        else if (window.location.pathname == "/Customer/ManageInspection")
        {
            console.log('----------------------------------------------xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx---------------/Customer/ManageInspection--------');
            $http.get('/api/pageview/getAllInspectionType').then(function (response) {
                $scope.InspectionTypeLayout = response.data;
            });

            $http.get('/api/pageview/getAllInspectionStatus').then(function (response) {
                console.log('asdfasfasdfasdfas', response.data);
                $scope.InspectionStatusLayout = response.data;
            });

            $scope.getCitybyProvinceIdByCustomer = function () {
                $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.insepctionFilter.province } }).then(function (response) {
                    $scope.getCitybyProvinceIdByCustomer = response.data;
                });
            };
            $scope.getLocationbyCityIdByCustomer = function () {
                $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.insepctionFilter.city } }).then(function (response) {
                    $scope.getLocationbyCityIdByCustomer = response.data;
                });
            };

            $scope.GetCitybyProvinceId = function () {
                $scope.strProvince = document.getElementById("drpprovinceLayout").value;
                $http.get('/api/pageview/getCitybyProvinceId', { params: { id: $scope.strProvince } }).then(function (response) {
                    $scope.getCitybyProvinceIdLayout = response.data;
                });
            };
        }
        

        // ---- BUTTON EVENTS ----

        $scope.applyInspectionDueFilter = function () {
            console.log('Set filter for inspection due');
            sharedInspectionDueFilterService.set(angular.copy($scope.inspectionDueFilter));
        };

        $scope.applyInspectionFilters = function () {
            // Extract selected inspection statuses
            var selectedStatuses = [];
            angular.forEach($scope.InspectionStatusLayout, function (status) {
                if (status.selected) {
                    selectedStatuses.push(status.InspectionStatusId);
                }
            });

            $scope.inspectionStatusFilter.InspectionStatusIds = selectedStatuses;

            console.log('Set filter for all inspections');
            sharedinsepctionFilterService.set(angular.copy($scope.inspectionStatusFilter));
        };
    }

})();
