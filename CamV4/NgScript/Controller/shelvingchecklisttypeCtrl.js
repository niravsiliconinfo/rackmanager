// ===================================================================
// ShelvingCheckListType Management Controller
// ===================================================================

(function () {
    'use strict';

    angular.module('myApp')
        .controller('ShelvingCheckListTypeCtrl', ShelvingCheckListTypeCtrl);

    ShelvingCheckListTypeCtrl.$inject = ['$scope', '$http', '$timeout'];

    function ShelvingCheckListTypeCtrl($scope, $http, $timeout) {

        // ============== INITIALIZATION ==============
        $scope.typeList = [];
        $scope.currentType = {};
        $scope.isEditMode = false;
        $scope.isProcessing = false;
        $scope.typeDataTable = null;
        $scope.validationErrors = {};

        // Get logged in user from session
        $scope.loggedInUserId = '@Session["LoggedInUserId"]';
        if (!$scope.loggedInUserId || $scope.loggedInUserId === '') {
            $scope.loggedInUserId = 'admin_user';
        }

        // ============== LOAD DATA ==============
        $scope.loadTypes = function () {
            $http.get('/api/pageview/ShelvingCheckList_GetAllShelvingCheckListTypes')
                .then(function (response) {
                    $scope.typeList = response.data;
                    console.log('Types loaded:', $scope.typeList);

                    // Initialize or refresh DataTable
                    $timeout(function () {
                        if ($scope.typeDataTable) {
                            $scope.typeDataTable.destroy();
                        }
                        $scope.initializeDataTable();
                    }, 100);
                })
                .catch(function (error) {
                    console.error('Error loading types:', error);
                    alert('Failed to load Shelving CheckList Types');
                });
        };

        // ============== DATATABLE INITIALIZATION ==============
        $scope.initializeDataTable = function () {
            $scope.typeDataTable = $('#typeDataTable').DataTable({
                data: $scope.typeList,
                columns: [
                    {
                        title: '<span class="text-uppercase text-secondary text-xs font-weight-bolder opacity-7">ID</span>',
                        data: 'ShelvingCheckListTypeId',
                        width: '10%',
                        className: 'text-sm'
                    },
                    {
                        title: '<span class="text-uppercase text-secondary text-xs font-weight-bolder opacity-7">Type Name</span>',
                        data: 'ShelvingCheckListTypeName',
                        width: '60%',
                        className: 'text-sm'
                    },
                    {
                        title: '<span class="text-uppercase text-secondary text-xs font-weight-bolder opacity-7">Created Date</span>',
                        data: 'CreatedDate',
                        width: '15%',
                        className: 'text-sm',
                        render: function (data) {
                            if (data) {
                                var date = new Date(data);
                                return date.toLocaleDateString();
                            }
                            return '';
                        }
                    },
                    {
                        title: '<span class="text-secondary opacity-7"></span>',
                        data: null,
                        width: '15%',
                        orderable: false,
                        className: 'text-sm align-middle',
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="edit-type-btn mx-2" data-id="' + row.ShelvingCheckListTypeId + '" title="Edit">' +
                                '<i class="material-icons text-sm">edit</i></a>' +
                                '<a href="javascript:void(0);" class="delete-type-btn" data-id="' + row.ShelvingCheckListTypeId + '" title="Delete">' +
                                '<i class="material-icons text-sm">delete</i></a>';
                        }
                    }
                ],
                pageLength: 10,
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                order: [[0, 'desc']],
                responsive: true,
                dom: '<"row"<"col-sm-6"l><"col-sm-6"f>>rtip',
                language: {
                    search: "_INPUT_",
                    searchPlaceholder: "Search types...",
                    lengthMenu: "Show _MENU_ entries"
                }
            });

            // Attach event handlers for Edit and Delete buttons
            $('#typeDataTable tbody').on('click', '.edit-type-btn', function () {
                var typeId = $(this).data('id');
                $scope.$apply(function () {
                    $scope.editType(typeId);
                });
            });

            $('#typeDataTable tbody').on('click', '.delete-type-btn', function () {
                var typeId = $(this).data('id');
                $scope.$apply(function () {
                    $scope.deleteType(typeId);
                });
            });
        };

        // ============== OPEN ADD MODAL ==============
        $scope.openAddTypeModal = function () {
            $scope.isEditMode = false;
            $scope.currentType = {
                ShelvingCheckListTypeName: '',
                CreatedBy: $scope.loggedInUserId
            };
            $scope.validationErrors = {};
            $('#typeModal').modal('show');
        };

        // ============== EDIT TYPE ==============
        $scope.editType = function (typeId) {
            var type = $scope.typeList.find(function (t) {
                return t.ShelvingCheckListTypeId === typeId;
            });

            if (type) {
                $scope.isEditMode = true;
                $scope.currentType = angular.copy(type);
                $scope.currentType.ModifiedBy = $scope.loggedInUserId;
                $scope.validationErrors = {};
                $('#typeModal').modal('show');
            }
        };

        // ============== VALIDATE FORM ==============
        $scope.validateType = function () {
            $scope.validationErrors = {};
            var isValid = true;

            if (!$scope.currentType.ShelvingCheckListTypeName ||
                $scope.currentType.ShelvingCheckListTypeName.trim() === '') {
                $scope.validationErrors.name = 'Type Name is required';
                isValid = false;
            }

            return isValid;
        };

        // ============== SAVE TYPE (Create/Update) ==============
        $scope.saveType = function () {
            if (!$scope.validateType()) {
                return;
            }

            $scope.isProcessing = true;

            var url = '/api/pageview/ShelvingCheckList_CreateShelvingCheckListType';
            if ($scope.isEditMode) {
                url = '/api/pageview/ShelvingCheckList_UpdateShelvingCheckListType';
            }

            $http.post(url, $scope.currentType)
                .then(function (response) {
                    $scope.isProcessing = false;

                    if (response.data.success) {
                        $('#typeModal').modal('hide');

                        if ($scope.isEditMode) {
                            alert('Type updated successfully!');
                        } else {
                            alert('Type created successfully!');
                        }

                        $scope.loadTypes(); // Reload data
                    } else {
                        alert('Operation failed: ' + response.data.message);
                    }
                })
                .catch(function (error) {
                    $scope.isProcessing = false;
                    console.error('Error saving type:', error);
                    alert('Failed to save type. Please try again.');
                });
        };

        // ============== DELETE TYPE ==============
        $scope.deleteType = function (typeId) {
            var type = $scope.typeList.find(function (t) {
                return t.ShelvingCheckListTypeId === typeId;
            });

            if (!type) return;

            if (!confirm('Are you sure you want to delete "' + type.ShelvingCheckListTypeName + '"?')) {
                return;
            }

            $scope.isProcessing = true;

            var deleteData = {
                id: typeId,
                loggedInUserId: $scope.loggedInUserId
            };

            $http.post('/api/pageview/ShelvingCheckList_DeleteShelvingCheckListType', deleteData)
                .then(function (response) {
                    $scope.isProcessing = false;

                    if (response.data.success) {
                        alert('Type deleted successfully!');
                        $scope.loadTypes(); // Reload data
                    } else {
                        alert('Delete failed: ' + response.data.message);
                    }
                })
                .catch(function (error) {
                    $scope.isProcessing = false;
                    console.error('Error deleting type:', error);
                    alert('Failed to delete type. Please try again.');
                });
        };

        // ============== CANCEL MODAL ==============
        $scope.cancelTypeModal = function () {
            $('#typeModal').modal('hide');
        };

        // ============== INITIALIZE ON PAGE LOAD ==============
        $scope.init = function () {
            console.log('Initializing ShelvingCheckListType controller...');
            $scope.loadTypes();
        };

        // Auto-initialize - call init immediately
        $scope.init();
    }
})();
