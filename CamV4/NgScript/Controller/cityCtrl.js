(function () {
    'use strict';

    angular.module('myApp')
        .controller('cityCtrl', cityCtrl);

    app.filter('startFrom', function () {
        return function (input, start) {
            start = +start;
            return input ? input.slice(start) : [];
        };
    });

    cityCtrl.$inject = ['$scope', '$http', '$timeout', '$compile'];

    function cityCtrl($scope, $http, $timeout, $compile) {
        $scope.city = {};
        $scope.cities = [];
        $scope.provinces = [];
        $scope.validationMessage = "";

        $scope.viewby = 50;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10; //Number of pager buttons to show

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () { };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        $scope.loadCities = function () {
            $http.get('/api/pageview/getAllCities').then(function (response) {
                $scope.getAllCities = response.data;
                console.log('$scope.getAllCities', $scope.getAllCities);
                if ($scope.getAllCities != null) { $scope.getAllCitiescount = $scope.getAllCities.length; }
                else { $scope.getAllCitiescount = 0; }
                $scope.totalgetAllCities = $scope.getAllCitiescount;
            }, function (response) {
                $scope.waiting = false;
            });
        };


        $scope.loadProvinces = function () {
            $http.get("/Cities/GetProvinces").then(function (response) {
                $scope.provinces = response.data;
            });
        };

        $scope.saveCity = function () {
            if (!$scope.city.CityName || !$scope.city.ProvinceID) {
                $scope.validationMessage = "Please enter all required fields.";
                return;
            }
            let url = $scope.city.CityID ? "/Cities/UpdateCity" : "/Cities/AddCity";
            $http.post(url, $scope.city).then(function () {
                $scope.loadCities();
                $scope.city = {};
                $scope.validationMessage = "";
            });
        };

        $scope.editCity = function (city) {
            $scope.city = angular.copy(city);
        };

        $scope.deleteCity = function (id) {
            if (confirm("Are you sure you want to delete this city?")) {
                $http.post("/Cities/DeleteCity", { id: id }).then(function () {
                    $scope.loadCities();
                });
            }
        };

        // Init
        $scope.loadCities();
        $scope.loadProvinces();
    }
})();
