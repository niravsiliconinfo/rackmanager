var app = angular.module('myApp');

app.controller('salesSpareMaterialCtrl', salesSpareMaterialCtrl);

salesSpareMaterialCtrl.$inject = [
    '$scope',
    '$http',
    'sharedFilterService',
    '$rootScope'
];

function salesSpareMaterialCtrl($scope, $http, sharedFilterService, $rootScope)
{

    console.log("----------- Sales Spare Material -----------");

    $scope.getAllInventoryFilesBySales = [];

    $scope.viewby = 50;
    $scope.currentPage = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 10;
    $scope.totalItems = 0;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () { };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1;
    };

    init();

    function init() {

        var filters = sharedFilterService.getSalesInventoryFilters();

        if (window.location.pathname === "/SalesManager/ManageSpareMaterialSales") {

            if (filters && Object.keys(filters).length > 0) {
                loadSalesInventory(filters);
            }
            else {
                loadSalesInventory();
            }
        }
    }

    // Sidebar Search button
    $scope.$on("salesInventoryFiltersUpdated", function (event, filters) {

        console.log("Sales Inventory Filters", filters);

        loadSalesInventory(filters);

    });

    function loadSalesInventory(filter) {

        console.log("Loading Sales Inventory", filter);

        $http.post("/api/pageview/getSalesInventoryFiles", filter)
            .then(function (response) {

                $scope.getAllInventoryFilesBySales = response.data || [];

                $scope.totalItems =
                    $scope.getAllInventoryFilesBySales.length;

                $scope.currentPage = 1;

            }, function (error) {

                console.log(error);

            });
    }

    $scope.refreshInventory = function () {
        loadSalesInventory();
    };

}