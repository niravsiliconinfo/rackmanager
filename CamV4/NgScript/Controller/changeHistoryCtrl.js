// NgScript/Controller/changeHistoryCtrl.js
// Customer-side users (UserType 4 or 9) &mdash; own history only.
// Server scopes via GetCustomerSpare MaterialHistory.

var app = angular.module('myApp');

app.controller('changeHistoryCtrl', ['$scope', '$http', function ($scope, $http) {
    console.log('----------- changeHistoryCtrl loaded -----------');

    $scope.viewby       = '10';
    $scope.currentPage  = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize      = '10';
    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num; $scope.currentPage = 1;
    };

    $scope.search        = '';
    $scope.historyList   = [];
    $scope.myLocationList = [];
    $scope.filter = { fromDate: '', toDate: '', locationId: '', actionType: '' };

    function loadLocations() {
        $http.get('/api/pageview/getInventoryLocations')
            .then(function (r) { $scope.myLocationList = r.data; })
            .catch(function () {});
    }

    $scope.ApplyFilter = function () {
        var params = {};
        if ($scope.filter.fromDate)   params.fromDate   = $scope.filter.fromDate;
        if ($scope.filter.toDate)     params.toDate     = $scope.filter.toDate;
        if ($scope.filter.locationId) params.fileId     = $scope.filter.locationId;
        if ($scope.filter.actionType) params.actionType = $scope.filter.actionType;

        $http.get('/api/pageview/getCustomerSpare MaterialHistory', { params: params })
            .then(function (r) {
                $scope.historyList = r.data;
                $scope.currentPage = 1;
            })
            .catch(function (err) {
                $scope.historyList = [];
                console.error('History error:', err);
            });
    };

    $scope.ClearFilter = function () {
        $scope.filter = { fromDate: '', toDate: '', locationId: '', actionType: '' };
        $scope.ApplyFilter();
    };

    loadLocations();
    $scope.ApplyFilter();

}]);
