// ===================================================================
// ShelvingCheckListDeficiency Management Controller
// ===================================================================

(function () {
    'use strict';

    angular.module('myApp')
        .controller('ShelvingCheckListDeficiencyCtrl', ShelvingCheckListDeficiencyCtrl);

    ShelvingCheckListDeficiencyCtrl.$inject = ['$scope', '$http', '$timeout'];

    function ShelvingCheckListDeficiencyCtrl($scope, $http, $timeout) {

        // ============== INITIALIZATION ==============
        $scope.deficiencyList = [];
        $scope.currentDeficiency = {};
        $scope.isEditMode = false;
        $scope.isProcessing = false;
        $scope.ShelvingCheckListdeficiencyDataTable = null;
        $scope.validationErrors = {};

        // Get logged in user from session
        $scope.loggedInUserId = '@Session["LoggedInUserId"]';
        if (!$scope.loggedInUserId || $scope.loggedInUserId === '') {
            $scope.loggedInUserId = 'admin_user';
        }

        // ============== LOAD DATA ==============
        $scope.loadDeficiencies = function () {
            $http.get('/api/pageview/ShelvingCheckList_GetAllActiveShelvingCheckListDeficiencies')
                .then(function (response) {
                    $scope.deficiencyList = response.data;
                    console.log('Deficiencies loaded:', $scope.deficiencyList);

                    // Initialize or refresh DataTable
                    $timeout(function () {
                        if ($scope.ShelvingCheckListdeficiencyDataTable) {
                            $scope.ShelvingCheckListdeficiencyDataTable.destroy();
                        }
                        $scope.initializeDataTable();
                    }, 100);
                })
                .catch(function (error) {
                    console.error('Error loading deficiencies:', error);
                    alert('Failed to load Shelving CheckList Deficiencies');
                });
        };

        // ============== DATATABLE INITIALIZATION ==============
        $scope.initializeDataTable = function () {
            $scope.ShelvingCheckListdeficiencyDataTable = $('#ShelvingCheckListdeficiencyDataTable').DataTable({
                data: $scope.deficiencyList,
                columns: [
                    {
                        title: '<span class="text-dark text-uppercase text-secondary text-sm font-weight-bolder opacity-7 ps-2">ID</span>',
                        data: 'ShelvingCheckListDeficiencyID',
                        width: '5%',
                        className: 'text-sm'
                    },
                    {
                        title: '<span class="text-dark text-uppercase text-secondary text-sm font-weight-bolder opacity-7 ps-2">Shelving Deficiency Description</span>',
                        data: 'ShelvingCheckListDeficiencyInfo',
                        width: '50%',
                        className: 'text-sm'
                    },
                    {
                        title: '<span class="text-dark text-uppercase text-secondary text-sm font-weight-bolder opacity-7 ps-2">Comment</span>',
                        data: 'ShelvingCheckListDeficiencyComment',
                        width: '25%',
                        className: 'text-sm',
                        render: function (data) {
                            if (data && data.length > 50) {
                                return '<span title="' + data + '">' + data.substring(0, 50) + '...</span>';
                            }
                            return data || '<span class="text-muted">-</span>';
                        }
                    },
                    {
                        title: '<span class="text-dark text-uppercase text-secondary text-sm font-weight-bolder opacity-7 ps-2">Status</span>',
                        data: 'IsActive',
                        width: '8%',
                        className: 'text-sm',
                        render: function (data) {
                            if (data === true) {
                                return '<span class="badge badge-sm bg-gradient-success">Active</span>';
                            } else {
                                return '<span class="badge badge-sm bg-gradient-secondary">Inactive</span>';
                            }
                        }
                    },
                    {
                        title: '<span class="text-secondary opacity-7"></span>',
                        data: null,
                        width: '12%',
                        orderable: false,
                        className: 'text-sm align-middle',
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="edit-deficiency-btn mx-2" data-id="' + row.ShelvingCheckListDeficiencyID + '" title="Edit">' +
                                '<i class="material-icons text-sm">edit</i></a>' +
                                '<a href="javascript:void(0);" class="delete-deficiency-btn" data-id="' + row.ShelvingCheckListDeficiencyID + '" title="Delete">' +
                                '<i class="material-icons text-sm">delete</i></a>';
                        }
                    }
                ],
                pageLength: 25,
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                order: [[0, 'asc']],
                responsive: true,
                dom: '<"row"<"col-sm-6"l><"col-sm-6"f>>rtip',
                language: {
                    search: "_INPUT_",
                    searchPlaceholder: "Search deficiencies...",
                    lengthMenu: "Show _MENU_ entries"
                }
            });

            // Attach event handlers for Edit and Delete buttons
            $('#ShelvingCheckListdeficiencyDataTable tbody').on('click', '.edit-deficiency-btn', function () {
                var deficiencyId = $(this).data('id');
                $scope.$apply(function () {
                    $scope.editDeficiency(deficiencyId);
                });
            });

            $('#ShelvingCheckListdeficiencyDataTable tbody').on('click', '.delete-deficiency-btn', function () {
                var deficiencyId = $(this).data('id');
                $scope.$apply(function () {
                    $scope.deleteDeficiency(deficiencyId);
                });
            });
        };

        // ============== OPEN ADD MODAL ==============
        $scope.openAddDeficiencyModal = function () {
            $scope.isEditMode = false;
            $scope.currentDeficiency = {
                ShelvingCheckListDeficiencyInfo: '',
                ShelvingCheckListDeficiencyComment: '',
                IsActive: true,
                CreatedBy: $scope.loggedInUserId
            };
            $scope.validationErrors = {};
            $('#deficiencyModal').modal('show');
        };

        // ============== EDIT DEFICIENCY ==============
        $scope.editDeficiency = function (deficiencyId) {
            var deficiency = $scope.deficiencyList.find(function (d) {
                return d.ShelvingCheckListDeficiencyID === deficiencyId;
            });

            if (deficiency) {
                $scope.isEditMode = true;
                $scope.currentDeficiency = angular.copy(deficiency);
                $scope.currentDeficiency.ModifiedBy = $scope.loggedInUserId;
                $scope.validationErrors = {};
                $('#deficiencyModal').modal('show');
            }
        };

        // ============== VALIDATE FORM ==============
        $scope.validateDeficiency = function () {
            $scope.validationErrors = {};
            var isValid = true;

            if (!$scope.currentDeficiency.ShelvingCheckListDeficiencyInfo ||
                $scope.currentDeficiency.ShelvingCheckListDeficiencyInfo.trim() === '') {
                $scope.validationErrors.info = 'Deficiency Description is required';
                isValid = false;
            }

            return isValid;
        };

        // ============== SAVE DEFICIENCY (Create/Update) ==============
        $scope.saveDeficiency = function () {
            if (!$scope.validateDeficiency()) {
                return;
            }

            $scope.isProcessing = true;

            var url = '/api/pageview/ShelvingCheckList_CreateShelvingCheckListDeficiency';
            if ($scope.isEditMode) {
                url = '/api/pageview/ShelvingCheckList_UpdateShelvingCheckListDeficiency';
            }

            $http.post(url, $scope.currentDeficiency)
                .then(function (response) {
                    $scope.isProcessing = false;

                    if (response.data.success) {
                        $('#deficiencyModal').modal('hide');

                        if ($scope.isEditMode) {
                            alert('Deficiency updated successfully!');
                        } else {
                            alert('Deficiency created successfully!');
                        }

                        $scope.loadDeficiencies(); // Reload data
                    } else {
                        alert('Operation failed: ' + response.data.message);
                    }
                })
                .catch(function (error) {
                    $scope.isProcessing = false;
                    console.error('Error saving deficiency:', error);
                    alert('Failed to save deficiency. Please try again.');
                });
        };

        // ============== DELETE DEFICIENCY ==============
        $scope.deleteDeficiency = function (deficiencyId) {
            var deficiency = $scope.deficiencyList.find(function (d) {
                return d.ShelvingCheckListDeficiencyID === deficiencyId;
            });

            if (!deficiency) return;

            var confirmMsg = 'Are you sure you want to delete this deficiency?\n\n"' +
                deficiency.ShelvingCheckListDeficiencyInfo + '"';

            if (!confirm(confirmMsg)) {
                return;
            }

            $scope.isProcessing = true;

            var deleteData = {
                id: deficiencyId,
                loggedInUserId: $scope.loggedInUserId
            };

            $http.post('/api/pageview/ShelvingCheckList_DeleteShelvingCheckListDeficiency', deleteData)
                .then(function (response) {
                    $scope.isProcessing = false;

                    if (response.data.success) {
                        alert('Deficiency deleted successfully!');
                        $scope.loadDeficiencies(); // Reload data
                    } else {
                        alert('Delete failed: ' + response.data.message);
                    }
                })
                .catch(function (error) {
                    $scope.isProcessing = false;
                    console.error('Error deleting deficiency:', error);
                    alert('Failed to delete deficiency. Please try again.');
                });
        };

        // ============== CANCEL MODAL ==============
        $scope.cancelDeficiencyModal = function () {
            $('#deficiencyModal').modal('hide');
        };
        // Auto-initialize after Angular is ready
       
        // ============== INITIALIZE ON PAGE LOAD ==============
        $scope.init = function () {
            console.log('Initializing ShelvingCheckListDeficiency controller...');
            $scope.loadDeficiencies();
        };

        // Auto-initialize - call init immediately
        $timeout(function () {
            $scope.init();
        }, 0);
    }
})();