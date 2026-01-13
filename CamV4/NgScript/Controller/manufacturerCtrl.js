(function () {
    'use strict';

    angular.module('myApp').controller('manufacturerCtrl', manufacturerCtrl);

    manufacturerCtrl.$inject = ['$scope', '$http', '$timeout'];

    function manufacturerCtrl($scope, $http, $timeout) {

        console.log('manufacturerCtrl initialized');

        // Initialize variables
        $scope.ManufacturerId = 0;
        $scope.manufacturerName = '';
        $scope.ManufacturerType = '';
        $scope.getAllManufacturer = [];

        // Manufacturer types dropdown
        $scope.manufacturerTypes = [
            { id: 0, name: 'For Racking & Shelving' },
            { id: 1, name: 'For Racking' },
            { id: 2, name: 'For Shelving' }
        ];

        // Get manufacturer type text for display
        $scope.getManufacturerTypeText = function (type) {
            var t = $scope.manufacturerTypes.find(function (x) {
                return x.id === type;
            });
            return t ? t.name : '';
        };

        // Initialize DataTable
        $scope.initManufacturerTable = function () {

            $timeout(function () {

                // Destroy existing DataTable if it exists
                if ($.fn.DataTable.isDataTable('#manufacturerTable')) {
                    $('#manufacturerTable').DataTable().destroy();
                }

                // Check if we have data
                if (!$scope.getAllManufacturer || $scope.getAllManufacturer.length === 0) {
                    console.log('No manufacturer data to display');
                    return;
                }

                console.log('Initializing DataTable with', $scope.getAllManufacturer.length, 'records');

                // Initialize DataTable
                $('#manufacturerTable').DataTable({
                    pageLength: 10,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    ordering: true,
                    searching: true,
                    destroy: true,
                    language: {
                        emptyTable: "No manufacturers found",
                        search: "Search:",
                        lengthMenu: "Show _MENU_ entries",
                        info: "Showing _START_ to _END_ of _TOTAL_ entries",
                        paginate: {
                            first: "First",
                            last: "Last",
                            next: "Next",
                            previous: "Previous"
                        }
                    }
                });

            }, 300);
        };

        // Load all manufacturers
        $scope.loadManufacturers = function () {

            console.log('Loading manufacturers...');

            $http.get('/api/pageview/getAllManufacturer')
                .then(function (response) {
                    console.log('Manufacturers loaded successfully:', response.data);
                    $scope.getAllManufacturer = response.data;

                    // Initialize DataTable after data loads
                    $scope.initManufacturerTable();

                }, function (error) {
                    console.error('Error loading manufacturers:', error);
                    alert('Failed to load manufacturers. Please refresh the page.');
                });
        };

        // Reset form
        $scope.resetManufacturer = function () {
            console.log('Resetting form');
            $scope.ManufacturerId = 0;
            $scope.manufacturerName = '';
            $scope.ManufacturerType = '';
        };

        // Edit manufacturer
        $scope.editManufacturer = function (manufacturer) {
            console.log('Editing manufacturer:', manufacturer);
            $scope.ManufacturerId = manufacturer.ManufacturerId;
            $scope.manufacturerName = manufacturer.ManufacturerName;
            $scope.ManufacturerType = manufacturer.ManufacturerType;
        };

        // Save or Update manufacturer
        $scope.saveOrUpdateManufacturer = function () {

            console.log('Save/Update called');

            // Validation
            if (!$scope.manufacturerName || $scope.manufacturerName.trim() === '') {
                alert('Please enter manufacturer name');
                return;
            }

            if ($scope.ManufacturerType === '' || $scope.ManufacturerType === null || $scope.ManufacturerType === undefined) {
                alert('Please select manufacturer type');
                return;
            }

            var obj = {
                ManufacturerId: $scope.ManufacturerId || 0,
                ManufacturerName: $scope.manufacturerName.trim(),
                ManufacturerType: parseInt($scope.ManufacturerType)
            };

            console.log('Saving manufacturer:', obj);

            var url = $scope.ManufacturerId > 0
                ? '/api/pageview/editManufacturer'
                : '/api/pageview/saveManufacturer';

            $http.post(url, obj)
                .then(function (response) {
                    console.log('Save response:', response.data);

                    if (response.data === "Ok") {
                        // Close modal
                        $('#manufacturerModal').modal('hide');

                        // Reset form
                        $scope.resetManufacturer();

                        // Reload data
                        $scope.loadManufacturers();

                        // Show success message
                        var message = $scope.ManufacturerId > 0
                            ? 'Manufacturer updated successfully'
                            : 'Manufacturer added successfully';

                        $timeout(function () {
                            alert(message);
                        }, 500);

                    } else {
                        alert('Failed to save manufacturer. Response: ' + response.data);
                    }

                }, function (error) {
                    console.error('Error saving manufacturer:', error);
                    alert('Error saving manufacturer. Please try again.');
                });
        };

        // Delete manufacturer
        $scope.DeleteManufacturer = function (id) {

            console.log('Deleting manufacturer ID:', id);

            if (!id || id <= 0) {
                alert('Invalid manufacturer ID');
                return;
            }

            $http.post('/api/pageview/removeManufacturer', null, { params: { id: id } })
                .then(function (response) {
                    console.log('Delete response:', response.data);

                    if (response.data === "Ok") {
                        // Close modal
                        $('#deleteModal').modal('hide');

                        // Reload data
                        $scope.loadManufacturers();

                        // Show success message
                        $timeout(function () {
                            alert('Manufacturer deleted successfully');
                        }, 500);

                    } else {
                        alert('Failed to delete manufacturer. Response: ' + response.data);
                    }

                }, function (error) {
                    console.error('Error deleting manufacturer:', error);
                    alert('Error deleting manufacturer. Please try again.');
                });
        };

        // Initialize - Load data on page load
        $scope.loadManufacturers();

    }
})();
