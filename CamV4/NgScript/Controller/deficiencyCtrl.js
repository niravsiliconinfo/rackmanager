(function () {
    'use strict';

    //var app = angular.module('myApp')
    angular.module('myApp')
        .controller('deficiencyCtrl', deficiencyCtrl);
    //app.controller('deficiencyCtrl', deficiencyCtrl);

    deficiencyCtrl.$inject = ['$scope', '$http'];

    function deficiencyCtrl($scope, $http) {

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

        $http.get('/api/pageview/getAllDeficiency').then(function (response) {
            $scope.getAllDeficiency = response.data;
            console.log('$scope.getAllDeficiency', $scope.getAllDeficiency);
            if ($scope.getAllDeficiency != null) { $scope.getAllDeficiencycount = $scope.getAllDeficiency.length; }
            else { $scope.getAllDeficiencycount = 0; }
            $scope.totalgetAllDeficiency = $scope.getAllDeficiencycount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllDeficiencyCategory').then(function (response) {
            $scope.getAllDeficiencyCategory = response.data;
            console.log('$scope.getAllDeficiencyCategory', $scope.getAllDeficiencyCategory);
            if ($scope.getAllDeficiencyCategory != null) { $scope.getAllDeficiencyCategorycount = $scope.getAllDeficiencyCategory.length; }
            else { $scope.getAllDeficiencyCategorycount = 0; }
            $scope.totalgetAllDeficiencyCategory = $scope.getAllDeficiencyCategorycount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllDeficiencySummary').then(function (response) {
            $scope.getAllDeficiencySummary = response.data;
            console.log('$scope.getAllDeficiencySummary', $scope.getAllDeficiencySummary);
            if ($scope.getAllDeficiencySummary != null) { $scope.getAllDeficiencySummarycount = $scope.getAllDeficiencySummary.length; }
            else { $scope.getAllDeficiencySummarycount = 0; }
            $scope.totalgetAllDeficiencySummary = $scope.getAllDeficiencySummarycount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllComponent').then(function (response) {
            $scope.getAllComponent = response.data;
            console.log('$scope.getAllComponent', $scope.getAllComponent);
            if ($scope.getAllComponent != null) { $scope.getAllComponentcount = $scope.getAllComponent.length; }
            else { $scope.getAllComponentcount = 0; }
            $scope.totalgetAllComponent = $scope.getAllComponentcount;
        }, function (response) {
            $scope.waiting = false;
            });

        $scope.SaveDeficiency = function () {
            var config = {
                DeficiencyCategoryId: $scope.deficiencyCategoryId, DeficiencyInfo: $scope.deficiencyInfo, DeficiencyDescription: $scope.deficiencyDescription,
                ComponentDesc: $scope.ComponentDesc, ComponentId: $scope.componentId, ComponentDescShort: $scope.ComponentDescShort
            }
            console.log('SaveDeficiency', config);
            return $http({
                url: '/api/pageview/saveDeficiency',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiency';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditDeficiency = function (Id) {
            var config = {
                DeficiencyID: Id,
                DeficiencyCategoryId: $scope.deficiencyCategoryId,
                DeficiencyInfo: $scope.deficiencyInfo,
                DeficiencyDescription: $scope.deficiencyDescription,
                ComponentDesc: $scope.ComponentDesc,
                ComponentId: $scope.componentId,
                ComponentDescShort: $scope.ComponentDescShort
            }
            console.log('editDeficiency', config);
            return $http({
                url: '/api/pageview/editDeficiency',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiency';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveDeficiency = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeDeficiency',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiency';
                    window.location = url;
                }
            });
        }

        $scope.SaveDeficiencyCategory = function () {
            var config = {
                DeficiencyCategoryName: $scope.deficiencyCategoryName
            }
            console.log('saveDeficiencyCategory', config);
            return $http({
                url: '/api/pageview/saveDeficiencyCategory',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencyCategory';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditDeficiencyCategory = function (Id) {
            var config = {
                DeficiencyCategoryId: Id, DeficiencyCategoryName: $scope.deficiencyCategoryName
            }
            console.log('editDeficiencyCategory', config);
            return $http({
                url: '/api/pageview/editDeficiencyCategory',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencyCategory';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveDeficiencyCategory = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeDeficiencyCategory',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencyCategory';
                    window.location = url;
                }
            });
        }

        $scope.SaveDeficiencySummary = function () {
            var config = {
                DeficiencySummaryDesc: $scope.deficiencysummarydesc
            }
            console.log('saveDeficiencySummary', config);
            return $http({
                url: '/api/pageview/saveDeficiencySummary',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencySummary';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditDeficiencySummary = function (Id) {
            var config = {
                DeficiencySummaryId: Id, DeficiencySummaryDesc: $scope.deficiencysummarydesc
            }
            console.log('editDeficiencySummary', config);
            return $http({
                url: '/api/pageview/editDeficiencySummary',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencySummary';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveDeficiencySummary = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeDeficiencySummary',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDeficiencySummary';
                    window.location = url;
                }
            });
        }
    }
})();
