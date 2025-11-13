(function () {
    'use strict';

    angular.module('myApp')
        .controller('documentsCtrl', documentsCtrl);

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

    app.controller('documentsCtrl', documentsCtrl);

    documentsCtrl.$inject = ['$scope', '$http', 'sharedFilterService', '$rootScope'];

    function documentsCtrl($scope, $http, sharedFilterService, $rootScope) {
        console.log('-----------documentsCtrl--------------');
        $scope.getAllInspectionDueByCustomerId = [];
        $scope.getAllInspectionByCustomerId = [];
        $scope.getAllHistoryDocument = [];

        $scope.scheduleFilter = sharedFilterService.getScheduleFilter() || {};
        $scope.statusFilter = sharedFilterService.getStatusFilter() || {};
        $scope.docsFilter = sharedFilterService.getDocsFilter() || {};


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

        init();

        function init() {
            var docs = sharedFilterService.getDocsFilter();
            if (window.location.pathname == "/Customer/ManageHistoryLegacyDocuments") {
                {
                    if (docs && Object.keys(docs).length > 0) {
                        console.log('from init for LoadDocument', docs);
                        $scope.docsFilter = docs;
                        loadDocuments(docs);
                    } else {
                        console.log('from init for LoadDocument by default from init');
                        loadDocuments(); // Load default data
                    }
                }
            }
        }

        function loadDocuments(filter) {
            const filters = filter;
            console.log('Calling ----> loadDocuments---------------------------', filters);
            $http.post('/api/pageview/getAllCustomerDocumentsWithFilters', filters)
                .then(function (response) {
                    $scope.getAllHistoryDocument = response.data;                                        
                    if ($scope.getAllHistoryDocument != null) { $scope.getAllHistoryDocumentcount = $scope.getAllHistoryDocument.length; }
                    else { $scope.getAllHistoryDocumentcount = 0; }
                    $scope.totalgetAllHistoryDocument = $scope.getAllHistoryDocumentcount;
                }, function (error) {
                    console.error("Error loading all inspections:", error);
                });
        }



        $scope.refreshAllDocuments = function () {
            loadDocuments();
        };

        $rootScope.$on('docsFilterUpdated', function () {
            const filters = sharedFilterService.getDocsFilter();
            console.log("Load function when click on button for statusFilterUpdated on getDocsFilter", filters);
            loadDocuments(filters);
        });




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
            var para = window.location.search;
            para = para.replace('?id=', '');
            $scope.companyId = para;
            console.log("ID on ManageHistoryLegacyDocuments", para);
            $http.get('/api/pageview/getCustomerLocationHistoryLegacyFiles', {
                params: { id: para }
            }).then(function (response) {
                $scope.getAllCustomerLocationHistoryDocument = response.data;
                console.log('$scope.getAllCustomerLocationHistoryDocument', $scope.getAllCustomerLocationHistoryDocument);
                if ($scope.getAllCustomerLocationHistoryDocument != null) { $scope.getAllCustomerLocationHistoryDocumentcount = $scope.getAllCustomerLocationHistoryDocument.length; }
                else { $scope.getAllCustomerLocationHistoryDocumentcount = 0; }
                $scope.totalgetAllCustomerLocationHistoryDocument = $scope.getAllCustomerLocationHistoryDocumentcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }
        //if (window.location.pathname == "/Admin/DeleteHistoryLegacyDocuments") {
        //    //var params = new URLSearchParams(window.location.search);            
        //    //var paraMain = params.get('custid');
        //    //console.log('paramain', paramain);
        //    $scope.RemoveHistoryLegacyFile = function (id) {
        //        //var paraMain = custid;
        //        var config = { CustomerLocationHistoryLegacyFileId: id }
        //        console.log('return config--', config);
        //        return $http({
        //            url: '/api/pageview/deleteHistoryLegacyFile',
        //            method: "POST",
        //            params: config,
        //            headers: {
        //                "Content-Type": "application/json"
        //            }
        //        }).then(function (response) {
        //            if (response.data === "File deleted successfully.") {
        //                var url = '/Admin/ManageHistoryLegacyDocuments?id=' + paraMain;
        //                window.location = url;
        //            }
        //        });
        //    }
        //}

        if (window.location.pathname == "/Admin/DeleteHistoryLegacyDocuments") {
            $scope.RemoveHistoryLegacyFile = function (id) {
                var params = new URLSearchParams(window.location.search);
                var custid = params.get('custid');

                if (!custid) {
                    toastr.error("Customer ID is missing. Cannot proceed.");
                    return;
                }

                var config = {
                    CustomerLocationHistoryLegacyFileId: id
                };
                console.log('return config--', config);

                return $http({
                    url: '/api/pageview/deleteHistoryLegacyFile',
                    method: "POST",
                    params: config,
                    headers: { "Content-Type": "application/json" }
                }).then(function (response) {
                    if (response.data === "File deleted successfully.") {
                        toastr.success("File deleted successfully!");
                        window.location = '/Admin/ManageHistoryLegacyDocuments?id=' + custid;
                    } else {
                        toastr.error(response.data);
                        window.location = '/Admin/ManageHistoryLegacyDocuments?id=' + custid;
                    }
                }, function (error) {
                    toastr.error("Error deleting file: " + (error.data || "Unknown error occurred."));
                    window.location = '/Admin/ManageHistoryLegacyDocuments?id=' + custid;
                });
            };
        }
        if (window.location.pathname == "/Admin/AddHistoryLegacyDocuments") {

            var paraMain = window.location.search;
            paraMain = paraMain.replace('?id=', '');
            //para = parseInt(para.replace('?id=', ''), 10); // Converts to number 5

            $http.get('/api/pageview/getAllCustomers').then(function (response) {
                $scope.getAllCustomers = response.data;
                //$scope.customerId = parseInt(para);
                var para = window.location.search;
                para = parseInt(para.replace('?id=', ''), 10);
                $scope.customerId = para;
                $scope.GetCustomerLocationByCustomerIdDrpd($scope.customerId);

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

            //$scope.UploadHistoryLegacyFile = function () {

            //    var customerid = $scope.customerId;
            //    var customerLocationID = $scope.customerLocationId;
            //    var list = '';
            //    var PdfList = [];

            //    for (var i = 0; i < $scope.fileList.length; i++) {

            //        $scope.UploadFileIndividualHistory($scope.fileList[i].file,
            //            $scope.fileList[i].file.name,
            //            $scope.fileList[i].file.type,
            //            $scope.fileList[i].file.size,
            //            i);
            //        PdfList[i] = $scope.fileList[i].file.name;

            //    }
            //    list = PdfList.toString();
            //    console.log('-----File Information-------', list);
            //    var config = { CustomerId: customerid, CustomerLocationID: customerLocationID, FileCategory: $scope.fileCategory, FileDrawingPath: list }
            //    console.log('00000000000--UploadHistoryLegacyFile ', config);

            //    return $http({
            //        url: '/api/pageview/uploadHistoryLegacyFile',
            //        method: "POST",
            //        data: config,
            //        headers: {
            //            "Content-Type": "application/json"
            //        }
            //    }).then(function (response) {
            //        if (response.data != 0) {
            //            var url = '/Admin/ManageHistoryLegacyDocuments?id='+paraMain;
            //            window.location = url;                           
            //        }
            //    });
            //};

            //$scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index) {
            //    //Create XMLHttpRequest Object
            //    var reqObj = new XMLHttpRequest();

            //    //open the object and set method of call(get/post), url to call, isAsynchronous(true/False)
            //    reqObj.open("POST", "/UploadCustHistoryFile", true);

            //    //set Content-Type at request header.for file upload it's value must be multipart/form-data
            //    reqObj.setRequestHeader("Content-Type", "multipart/form-data");

            //    //Set Other header like file name,size and type
            //    reqObj.setRequestHeader('X-File-Name', name);
            //    reqObj.setRequestHeader('X-File-Type', type);
            //    reqObj.setRequestHeader('X-File-Size', size);

            //    // send the file
            //    reqObj.send(fileToUpload);
            //}

            //$scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index, customerID, CustomerLocation) {
            //    var formData = new FormData();
            //    formData.append('file', fileToUpload);
            //    formData.append('antiForgeryToken', $scope.antiForgeryToken);
            //    formData.append('FileName', name);
            //    formData.append('customerID', customerID);
            //    formData.append('CustomerLocation', CustomerLocation);
            //    console.log('-----File Information-------fileToUpload', fileToUpload);
            //    console.log('-----File Information-------name', name);                    
            //    return $http({
            //        url: '/UploadCustHistoryFile',
            //        method: 'POST',
            //        data: formData,
            //        headers: {
            //            'Content-Type': undefined
            //        },
            //        transformRequest: angular.identity
            //    }).then(function (response) {
            //        console.log('File uploaded successfully:', name);
            //    }, function (error) {
            //        console.error('File upload failed:', name, error);
            //        $scope.validationShow = 'Failed to upload file: ' + name;
            //    });
            //};

            //$scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index, customerID, CustomerLocation) {
            //    var formData = new FormData();

            //    var fileNameWithoutExt = name.substring(0, name.lastIndexOf('.')) || name;
            //    var extension = name.substring(name.lastIndexOf('.'));
            //    var timestamp = new Date().toISOString().replace(/[-:.TZ]/g, "").substring(0, 14); // yyyyMMddHHmmss

            //    var safeName = fileNameWithoutExt.replace(/\s+/g, "_");
            //    var safeLocation = (CustomerLocation || "Unknown").replace(/\s+/g, "_");

            //    var finalFileName = customerID + "_" + safeLocation + "_" + safeName + "_" + timestamp + extension;
            //    fileToUpload.finalFileName = finalFileName;
            //    formData.append('file', fileToUpload);
            //    formData.append('antiForgeryToken', $scope.antiForgeryToken);
            //    //formData.append('X-File-Name', finalFileName);                
            //    console.log('-----File Information-------fileToUpload', fileToUpload);
            //    console.log('-----File Information-------name', finalFileName);
            //    return;
            //    return $http({
            //        url: '/UploadCustHistoryFile',
            //        method: 'POST',
            //        data: formData,
            //        headers: {
            //            'Content-Type': undefined
            //        },
            //        transformRequest: angular.identity
            //    }).then(function (response) {                    
            //        $scope.fileList[index].originalName = name;
            //        $scope.fileList[index].savedName = finalFileName;
            //        $scope.validationShow = 'File uploaded successfully : ' + name;
            //        console.log('File uploaded successfully:', name);
            //    }, function (error) {
            //        console.error('File upload failed:', name, error);
            //        $scope.validationShow = 'Failed to upload file: ' + name + error;
            //    });
            //};

            //$scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index, customerID, CustomerLocation) {
            //    var formData = new FormData();

            //    // Build final file name in Angular
            //    var fileNameWithoutExt = name.substring(0, name.lastIndexOf('.')) || name;
            //    var extension = name.substring(name.lastIndexOf('.'));
            //    var timestamp = new Date().toISOString().replace(/[-:.TZ]/g, "").substring(0, 14); // yyyyMMddHHmmss

            //    var safeName = fileNameWithoutExt.replace(/\s+/g, "_");
            //    var safeLocation = (CustomerLocation || "Unknown").replace(/\s+/g, "_");

            //    var finalFileName = customerID + "_" + safeLocation + "_" + safeName + "_" + timestamp + extension;

            //    formData.append('file', fileToUpload);           // actual file
            //    formData.append('FileName', finalFileName);      // final name
            //    formData.append('OriginalFileName', name);       // original name
            //    formData.append('customerID', customerID);
            //    formData.append('CustomerLocation', CustomerLocation);
            //    formData.append('antiForgeryToken', $scope.antiForgeryToken);

            //    return $http.post('/UploadCustHistoryFile', formData, {
            //        headers: { 'Content-Type': undefined },
            //        transformRequest: angular.identity
            //    }).then(function (response) {
            //        var data = response.data;
            //        $scope.fileList[index].originalName = data.OriginalFileName;
            //        $scope.fileList[index].savedName = data.SavedFileName;
            //    }, function (error) {
            //        console.log(error);
            //        $scope.validationShow = 'Failed to upload file: ' + name;
            //    });
            //};

            $scope.UploadFileIndividualHistory = function (fileToUpload, name, type, size, index, customerID, CustomerLocation) {
                var formData = new FormData();
                var fileNameWithoutExt = name.substring(0, name.lastIndexOf('.')) || name;
                var extension = name.substring(name.lastIndexOf('.'));
                var timestamp = new Date().toISOString().replace(/[-:.TZ]/g, "").substring(0, 14); // yyyyMMddHHmmss
                var safeName = fileNameWithoutExt.replace(/\s+/g, "_");
                var safeLocation = (CustomerLocation || "Unknown").replace(/\s+/g, "_");
                var finalFileName = customerID + "_" + safeLocation + "_" + safeName + "_" + timestamp + extension;

                // Append the file and file name to FormData
                formData.append('file', fileToUpload, finalFileName); // Attach file with the final file name
                formData.append('antiForgeryToken', $scope.antiForgeryToken);

                console.log('-----File Information-------fileToUpload', fileToUpload);
                console.log('-----File Information-------name', finalFileName);

                return $http({
                    url: '/UploadCustHistoryFile',
                    method: 'POST',
                    data: formData,
                    headers: {
                        'Content-Type': undefined // Let AngularJS set the correct multipart/form-data boundary
                    },
                    transformRequest: angular.identity
                }).then(function (response) {
                    $scope.fileList[index].originalName = name;
                    $scope.fileList[index].savedName = finalFileName;
                    $scope.validationShow = 'File uploaded successfully: ' + name;
                    console.log('File uploaded successfully:', name);
                }, function (error) {
                    console.error('File upload failed:', name, error);
                    $scope.validationShow = 'Failed to upload file: ' + name + ' - ' + error.statusText;
                });
            };

            $scope.UploadHistoryLegacyFile = function () {
                var customerid = $scope.customerId;
                var customerLocationID = $scope.customerLocationId;

                if (!customerLocationID) {
                    $scope.validationShow = 'Please select Customer Location.';
                    return;
                }

                if (!$scope.fileList.length) {
                    $scope.validationShow = 'Please select at least one file to upload.';
                    return;
                }

                var uploadPromises = [];
                for (var i = 0; i < $scope.fileList.length; i++) {
                    uploadPromises.push($scope.UploadFileIndividualHistory(
                        $scope.fileList[i].file,
                        $scope.fileList[i].file.name,
                        $scope.fileList[i].file.type,
                        $scope.fileList[i].file.size,
                        i,
                        customerid,
                        customerLocationID
                    ));
                }
                console.log('$scope.fileList', $scope.fileList);
                Promise.all(uploadPromises).then(function () {
                    var PdfList = [];      // renamed files
                    var OriginalList = []; // original names

                    for (var i = 0; i < $scope.fileList.length; i++) {
                        PdfList[i] = $scope.fileList[i].savedName;
                        OriginalList[i] = $scope.fileList[i].originalName;
                    }

                    var config = {
                        CustomerId: customerid,
                        CustomerLocationID: customerLocationID,
                        FileCategory: $scope.fileCategory,
                        FileDrawingPath: PdfList.toString(),
                        FileDrawingName: OriginalList.toString()
                    };
                    console.log('File listing', config);
                    return $http.post('/api/pageview/uploadHistoryLegacyFile', config, {
                        headers: {
                            "Content-Type": "application/json",
                            "RequestVerificationToken": $scope.antiForgeryToken
                        }
                    }).then(function (response) {
                        console.log('on file save in database :..................' , response);
                        if (response.data != 0) {
                            var url = '/Admin/ManageHistoryLegacyDocuments?id=' + paraMain;
                            window.location = url;
                        } else {
                            $scope.validationShow = 'Failed to save file information to database.';
                        }
                    }, function (error) {
                        $scope.validationShow = 'Error saving file information: ' + error.data;
                    });
                }, function (error) {
                    $scope.validationShow = 'One or more file uploads failed.';
                });
            };
            //$scope.UploadHistoryLegacyFile = function () {
            //    var customerid = $scope.customerId;
            //    var customerLocationID = $scope.customerLocationId;

            //    if (!customerLocationID) {
            //        $scope.validationShow = 'Please select Customer Location.';
            //        return;
            //    }

            //    if (!$scope.fileList.length) {
            //        $scope.validationShow = 'Please select at least one file to upload.';
            //        return;
            //    }

            //    var uploadPromises = [];
            //    for (var i = 0; i < $scope.fileList.length; i++) {
            //        uploadPromises.push($scope.UploadFileIndividualHistory(
            //            $scope.fileList[i].file,
            //            $scope.fileList[i].file.name,
            //            $scope.fileList[i].file.type,
            //            $scope.fileList[i].file.size,
            //            i,
            //            customerid,
            //            customerLocationID
            //        ));
            //    }
            //    console.log('$scope.fileList[i] : ',$scope.fileList[i]);
            //    return;
            //    // wait for all uploads to finish
            //    Promise.all(uploadPromises).then(function () {
            //        var PdfList = [];      // renamed files (with timestamp)
            //        var OriginalList = []; // original user file names

            //        for (var i = 0; i < $scope.fileList.length; i++) {
            //            PdfList[i] = $scope.fileList[i].savedName;
            //            OriginalList[i] = $scope.fileList[i].originalName;
            //        }

            //        var config = {
            //            CustomerId: customerid,
            //            CustomerLocationID: customerLocationID,
            //            FileCategory: $scope.fileCategory,
            //            FileDrawingPath: PdfList.toString(),   // renamed files
            //            FileDrawingName: OriginalList.toString() // originals
            //        };

            //        console.log('Final config before save:', config);

            //        return $http({
            //            url: '/api/pageview/uploadHistoryLegacyFile',
            //            method: "POST",
            //            data: config,
            //            headers: {
            //                "Content-Type": "application/json",
            //                "RequestVerificationToken": $scope.antiForgeryToken
            //            }
            //        }).then(function (response) {
            //            if (response.data != 0) {
            //                var url = '/Admin/ManageHistoryLegacyDocuments?id=' + paraMain;
            //                window.location = url;
            //            } else {
            //                $scope.validationShow = 'Failed to save file information to database.';
            //            }
            //        }, function (error) {
            //            $scope.validationShow = 'Error saving file information: ' + error.data;
            //        });
            //    }, function (error) {
            //        $scope.validationShow = 'One or more file uploads failed.';
            //    });
            //};

            //$scope.UploadHistoryLegacyFile = function () {
            //    var customerid = $scope.customerId;
            //    var customerLocationID = $scope.customerLocationId;
            //    var list = '';
            //    var PdfList = [];

            //    if (customerLocationID == "") {                        
            //        $scope.validationShow = 'Please select Customer Location.';
            //        return;
            //    }

            //    if (!$scope.fileList.length) {
            //        $scope.validationShow = 'Please select at least one file to upload.';
            //        return;
            //    }

            //    var uploadPromises = [];                    
            //    for (var i = 0; i < $scope.fileList.length; i++) {
            //        uploadPromises.push($scope.UploadFileIndividualHistory(
            //            $scope.fileList[i].file,
            //            $scope.fileList[i].file.name,
            //            $scope.fileList[i].file.type,
            //            $scope.fileList[i].file.size,
            //            i,
            //            customerid,
            //            customerLocationID
            //        ));
            //        PdfList[i] = $scope.fileList[i].file.name;
            //    }
            //    list = PdfList.toString();
            //    console.log('-----File Information-------', list);                    
            //    Promise.all(uploadPromises).then(function () {
            //        var config = {
            //            CustomerId: customerid,
            //            CustomerLocationID: customerLocationID,
            //            FileCategory: $scope.fileCategory,
            //            FileDrawingPath: list
            //        };
            //        console.log('00000000000--UploadHistoryLegacyFile ', config);

            //        return $http({
            //            url: '/api/pageview/uploadHistoryLegacyFile',
            //            method: "POST",
            //            data: config,
            //            headers: {
            //                "Content-Type": "application/json",
            //                "RequestVerificationToken": $scope.antiForgeryToken
            //            }
            //        }).then(function (response) {
            //            if (response.data != 0) {
            //                var url = '/Admin/ManageHistoryLegacyDocuments?id=' + paraMain;
            //                window.location = url;
            //            } else {
            //                $scope.validationShow = 'Failed to save file information to database.';
            //            }
            //        }, function (error) {
            //            $scope.validationShow = 'Error saving file information: ' + error.data;
            //        });
            //    }, function (error) {
            //        $scope.validationShow = 'One or more file uploads failed.';
            //    });
            //};

        }
    }
})();