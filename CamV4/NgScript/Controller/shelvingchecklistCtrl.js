(function () {
    'use strict';

    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp')
        .controller('shelvingchecklistCtrl', shelvingchecklistCtrl);

    app.filter('startFrom', function () {
        return function (input, start) {
            if (input) {
                start = +start;
                return input.slice(start);
            }
            return [];
        };
    });

    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];
            if (angular.isArray(items)) {
                items.forEach(function (item) {
                    var itemMatches = false;

                    var keys = Object.keys(props);
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            return out;
        };
    });

    var PDFFileList = [];

    app.controller('ImageUploadMultipleCtrl', function ($scope) {

        $scope.fileList = [];
        $scope.curFile;
        $scope.FileProperty = {
            FileDrawingNamePath: ''
        }

        $scope.setFile = function (element) {
            $scope.fileList = [];
            // get the files
            var files = element.files;
            for (var i = 0; i < files.length; i++) {
                $scope.FileProperty.file = files[i];

                $scope.fileList.push($scope.FileProperty);
                $scope.FileProperty = {};
                $scope.$apply();

            }
        }
        PDFFileList = $scope.fileList;
        console.log('--------------------File list----------------', PDFFileList);
    });

    app.controller('shelvingchecklistCtrl', shelvingchecklistCtrl);

    shelvingchecklistCtrl.$inject = ['$scope', '$http'];

    function shelvingchecklistCtrl($scope, $http) {
        $scope.isProcessing = false;
        $scope.getInspectionClone = [];
        $scope.viewby = '50';
        $scope.currentPage = '1';
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = '10'; //Number of pager buttons to show
        $scope.isLoading = false;
        $scope.isLoadingButton = false;
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () { };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        $scope.inspectionDate = new Date();
        $scope.selectedDeficiencyCustomerQuotation = '';

        $scope.ItemDescription = '';
        $scope.suggestions = [];

        $scope.isEditingLabour = false;

        $http.get('/api/pageview/getAllCustomers').then(function (response) {
            $scope.getAllCustomers = response.data;
            console.log('$scope.getAllCustomers', $scope.getAllCustomers);
            if ($scope.getAllCustomers != null) { $scope.getAllCustomerscount = $scope.getAllCustomers.length; }
            else { $scope.getAllCustomerscount = 0; }
            $scope.totalgetAllCustomers = $scope.getAllCustomerscount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllEmployee').then(function (response) {
            $scope.getAllEmployee = response.data;
            console.log('$scope.getAllEmployee', $scope.getAllEmployee);
            if ($scope.getAllEmployee != null) { $scope.getAllEmployeecount = $scope.getAllEmployee.length; }
            else { $scope.getAllEmployeecount = 0; }
            $scope.totalemployee = $scope.getAllEmployeecount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllStampingEmployee').then(function (response) {
            $scope.getAllStampingEmployee = response.data;
            console.log('$scope.getAllStampingEmployee', $scope.getAllStampingEmployee);
        }, function (response) {
            $scope.waiting = true;
        });

        $http.get('/api/pageview/getAllSalesPerson').then(function (response) {
            $scope.getAllSalesPerson = response.data;
            console.log('------------$scope.getAllSalesPerson---------', $scope.getAllSalesPerson);
        }, function (response) {
            $scope.waiting = true;
        });

        $scope.isValid = function () {
            //console.log('------------$scope.btnAddItemComponentPrice---------', $scope.newquotationItem.quantity);
            return $scope.newquotationItem.quantity && $scope.newquotationItem.unitPrice && $scope.newquotationItem.weight &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.quantity) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.unitPrice) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.weight);
        };

        if (window.location.pathname == "/Customer/ManageInspectionDue") {
            $http.get('/api/pageview/getAllInspectionDueByCustomerId').then(function (response) {
                $scope.getAllInspectionDueByCustomerId = response.data;
                if ($scope.getAllInspectionDueByCustomerId != null) {
                    $scope.getAllInspectionDueByCustomerIdcount = $scope.getAllInspectionDueByCustomerId.length;
                }
                else {
                    $scope.getAllInspectionDueByCustomerIdcount = 0;
                }
                $scope.totalgetAllInspectionDueByCustomerId = $scope.getAllInspectionDueByCustomerIdcount;
            }, function (response) {
                $scope.waiting = false;
            });
        };   
    }
})();