(function () {
    'use strict';

    angular.module('myApp')
        .controller('userCtrl', userCtrl);

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

    app.controller('userCtrl', userCtrl);

    userCtrl.$inject = ['$scope', '$http'];

    function userCtrl($scope, $http) {
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

       
        if (window.location.pathname == "/Admin/ManageHistoryLegacyDocuments") {
            function loadAllCustomerContacts() {
                $http.post('/api/pageview/getAllCustomerDocumentsWithFilters')
                    .then(function (response) {
                        $scope.getAllHistoryDocument = response.data;
                    }, function (error) {
                        console.error("Error loading all inspections:", error);
                    });
            }

        }

        if (window.location.pathname == "/Customer/ManageAllUserContacts") {
            $http.get('/api/pageview/getLocationContactByCustomer').then(function (response) {
                $scope.getAllCustomerLocationUsers = response.data;                
                if ($scope.getAllCustomerLocationUsers != null) { $scope.getAllCustomerLocationUserscount = $scope.getAllCustomerLocationUsers.length; }
                else { $scope.getAllCustomerLocationUserscount = 0; }
                $scope.totalgetAllCustomerLocationUsers = $scope.getAllCustomerLocationUserscount;
            }, function (response) {
                $scope.waiting = false;
            });
        }
        

        $scope.GetCustomerLocationByCustomerIdDrpd = function (id) {
            console.log('GetLocationbyCustomerDrpd', id);
            $http.get('/api/pageview/getCustomerLocationByCustomerId', { params: { id: id } }).then(function (response) {
                $scope.getCustomerLocationByCustomerIdDrpd = response.data;
                console.log('getCustomerLocationByCustomerIdDrpd--', $scope.getCustomerLocationByCustomerIdDrpd);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $scope.GetAreaByLocationIdDrpd = function (id) {
            console.log('getAreaDetailsByLocationId', id);
            $http.get('/api/pageview/getAreaDetailsByLocationId', { params: { id: id } }).then(function (response) {
                $scope.getAreaDetailsByLocationId = response.data;
                console.log('getAreaDetailsByLocationId--', $scope.getAreaDetailsByLocationId);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        if (window.location.pathname == "/Admin/ManageHistoryLegacyDocuments") {
            console.log('--------------------$scope.getAllCustomerLocationHistoryDocument------------------------');
            $http.get('/api/pageview/getCustomerLocationHistoryLegacyFiles').then(function (response) {
                $scope.getAllCustomerLocationHistoryDocument = response.data;
                console.log('$scope.getAllCustomerLocationHistoryDocument', $scope.getAllCustomerLocationHistoryDocument);
                if ($scope.getInspectionFileDrawing != null) { $scope.getInspectionFileDrawingcount = $scope.getInspectionFileDrawing.length; }
                else { $scope.getInspectionFileDrawingcount = 0; }
                $scope.totalgetInspectionFileDrawing = $scope.getInspectionFileDrawingcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }
        if (window.location.pathname == "/Admin/AddHistoryLegacyDocuments") {
            $http.get('/api/pageview/getAllCustomers').then(function (response) {
                $scope.getAllCustomers = response.data;
                console.log('$scope.getAllCustomers', $scope.getAllCustomers);
                if ($scope.getAllCustomers != null) { $scope.getAllCustomerscount = $scope.getAllCustomers.length; }
                else { $scope.getAllCustomerscount = 0; }
                $scope.totalgetAllCustomers = $scope.getAllCustomerscount;
            }, function (response) {
                $scope.waiting = false;
            });

            $scope.fileList = [];
            $scope.FileUploadMultiple = function () {
                $scope.fileList = [];
                $scope.curFile;
                $scope.FileProperty = {
                    file: ''
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
            }

            $scope.UploadHistoryLegacyFile = function () {

                var customerid = $scope.customerId;
                var customerLocationID = $scope.customerLocationId;
                var list = '';
                var PdfList = [];

                for (var i = 0; i < $scope.fileList.length; i++) {

                    $scope.UploadFileIndividualHistory($scope.fileList[i].file,
                        $scope.fileList[i].file.name,
                        $scope.fileList[i].file.type,
                        $scope.fileList[i].file.size,
                        i);
                    PdfList[i] = $scope.fileList[i].file.name;

                }
                list = PdfList.toString();
                console.log('-----File Information-------', list);
                var config = { CustomerId: customerid, CustomerLocationID: customerLocationID, FileCategory: $scope.fileCategory, FileDrawingPath: list }
                console.log('00000000000--UploadHistoryLegacyFile ', config);

                return $http({
                    url: '/api/pageview/uploadHistoryLegacyFile',
                    method: "POST",
                    data: config,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    if (response.data != 0) {
                        var url = '/Admin/ManageHistoryLegacyDocuments';
                        window.location = url;
                    }
                });
            };

            $scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index) {
                //Create XMLHttpRequest Object
                var reqObj = new XMLHttpRequest();

                //open the object and set method of call(get/post), url to call, isAsynchronous(true/False)
                reqObj.open("POST", "/UploadCustHistoryFile", true);

                //set Content-Type at request header.for file upload it's value must be multipart/form-data
                reqObj.setRequestHeader("Content-Type", "multipart/form-data");

                //Set Other header like file name,size and type
                reqObj.setRequestHeader('X-File-Name', name);
                reqObj.setRequestHeader('X-File-Type', type);
                reqObj.setRequestHeader('X-File-Size', size);

                // send the file
                reqObj.send(fileToUpload);
            }
        }
    }
})();