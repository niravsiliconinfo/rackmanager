angular.module('myApp')
    .controller('customerinspectionDueCtrl', function ($scope, $http, sharedFilterService) {

        $scope.scheduleFilter = sharedFilterService.getScheduleFilter();

        console.log("Loaded Schedule Filter:", $scope.scheduleFilter);

        // Automatically fetch data with this filter when view loads
        if ($scope.scheduleFilter && Object.keys($scope.scheduleFilter).length > 0) {
            loadFilteredInspectionDue($scope.scheduleFilter);
        }

        function loadFilteredInspectionDue(filter) {
            // Use $http or your API service here
            $http.post('/api/pageview/getDueInspectionsCustomerFilters', filter).then(function (res) {
                $scope.inspectionResults = res.data;
            });
        }
    });
