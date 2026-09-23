(function () {
    'use strict';

    angular.module('myApp')
        .controller('salesDocumentsCtrl', salesDocumentsCtrl);

    app.filter('startFrom', function () {
        return function (input, start) {
            if (input) {
                start = +start;
                return input.slice(start);
            }
            return [];
        };
    });    

    salesDocumentsCtrl.$inject = [
        '$scope',
        '$http',
        'sharedFilterService'
    ];

    function salesDocumentsCtrl($scope, $http, sharedFilterService) {

        $scope.viewby = 50;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1;
        };

        $scope.getAllDocumentsBySales = [];

        init();

        function init() {

            var filters =
                sharedFilterService.getSalesDocumentFilters();

            if (filters &&
                Object.keys(filters).length > 0) {

                loadSalesDocuments(filters);

            } else {

                loadSalesDocuments();
            }
        }

        $scope.$on(
            'salesDocumentFiltersUpdated',
            function (event, filters) {

                loadSalesDocuments(filters);

            });

        function loadSalesDocuments(filter) {

            $http.post('/api/pageview/getSalesDocumentsWithFilters', filter)
                .then(function (response) {

                    $scope.getAllHistoryDocumentBySales = response.data || [];

                    $scope.currentPage = 1;
                    $scope.totalItems = $scope.getAllHistoryDocumentBySales.length;

                    console.log("Sales Documents:", $scope.totalItems);

                }, function (error) {

                    console.log(error);

                });
        }
    }

})();

