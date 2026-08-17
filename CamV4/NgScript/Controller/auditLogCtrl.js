// NgScript/Controller/auditLogCtrl.js
// Admin / internal-staff only &mdash; server enforces via IsInternal().

var app = angular.module('myApp');

app.controller('auditLogCtrl', ['$scope', '$http', function ($scope, $http) {
    console.log('----------- auditLogCtrl loaded -----------');

    $scope.viewby       = '10';
    $scope.currentPage  = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize      = '10';
    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num; $scope.currentPage = 1;
    };

    $scope.search     = '';
    $scope.auditList  = [];
    $scope.filter     = { fromDate: '', toDate: '', locationId: '', actionType: '' };
    $scope.locationList = [];

    function loadLocations() {
        $http.get('/api/pageview/getInventoryLocations')
            .then(function (r) { $scope.locationList = r.data; })
            .catch(function () {});
    }

    $scope.ApplyFilter = function () {
        var params = {};
        if ($scope.filter.fromDate)   params.fromDate   = $scope.filter.fromDate;
        if ($scope.filter.toDate)     params.toDate     = $scope.filter.toDate;
        if ($scope.filter.locationId) params.locationId = $scope.filter.locationId;
        if ($scope.filter.actionType) params.actionType = $scope.filter.actionType;

        $http.get('/api/pageview/getInventoryAuditLog', { params: params })
            .then(function (r) {
                $scope.auditList   = r.data;
                $scope.currentPage = 1;
            })
            .catch(function (err) {
                $scope.auditList = [];
                console.error('Audit log error:', err);
            });
    };

    $scope.ClearFilter = function () {
        $scope.filter = { fromDate: '', toDate: '', locationId: '', actionType: '' };
        $scope.ApplyFilter();
    };

    loadLocations();
    $scope.ApplyFilter();

}]);
