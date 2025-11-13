(function () {
    'use strict';

    //var app = angular.module('myApp', ['ui.bootstrap']);
    //app.controller('inspectionDueCtrl', inspectionDueCtrl);
    angular.module('myApp')
        .controller('inspectionDueCtrl', inspectionDueCtrl);

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

   

    //inspectionDueCtrl.$inject = ['$scope', '$http'];
    inspectionDueCtrl.$inject = ['$scope', '$http', '$q'];

    function inspectionDueCtrl($scope, $http, $q) {

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

        $scope.inspectionDate = new Date();
        $scope.selectedDeficiencyCustomerQuotation = '';

        $scope.ItemDescription = '';
        $scope.suggestions = [];

        $scope.isEditingLabour = false;

        $scope.isValid = function () {
            //console.log('------------$scope.btnAddItemComponentPrice---------', $scope.newquotationItem.quantity);
            return $scope.newquotationItem.quantity && $scope.newquotationItem.unitPrice && $scope.newquotationItem.weight &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.quantity) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.unitPrice) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.weight);
        };
        
        $scope.InspectionsheetClick = function (id) {
            var url = '/Admin/InspectionSheet?id=' + id;
            window.location = url;
        };

        $scope.InspectionDetailByEmployeeClick = function (id) {
            var url = '/Employee/InspectionDetail?id=' + id;
            window.location = url;
        };

        $http.get('/api/pageview/getAllDueInspection').then(function (response) {
            $scope.getAllDueInspection = response.data;
            console.log('$scope.getAllDueInspection', $scope.getAllDueInspection);
            if ($scope.getAllDueInspection != null) { $scope.getAllDueInspectioncount = $scope.getAllDueInspection.length; }
            else { $scope.getAllDueInspectioncount = 0; }
            $scope.totalgetAllDueInspection = $scope.getAllDueInspectioncount;
        }, function (response) {
            $scope.waiting = false;
        });

        
        
        if (window.location.pathname == "/Admin/EditInspectionDue") {
            //var para = window.location.search;
            //para = para.replace('?id=', '');
            $scope.ShowDatepicker = false;
            $scope.ReadOnlyDatePicker = true;

            var para = window.location.search.replace('?id=', '');

            // Call all list-loading APIs
            var facilitiesPromise = $http.get('/api/pageview/getAllFacilitiesArea');
            var processPromise = $http.get('/api/pageview/getAllProcessOverview');
            var documentPromise = $http.get('/api/pageview/getAllDocumentTitle');
            var locationPromise = $http.get('/api/pageview/getAllCustomerLocations');
            var areaPromise = $http.get('/api/pageview/getAllCustomerArea');
            var inspectionPromise = $http.get('/api/pageview/getInspectionById', { params: { InspectionId: para } });

            // Use $q.all to wait for all of them
            $q.all([facilitiesPromise, processPromise, documentPromise, inspectionPromise, locationPromise, areaPromise])
                .then(function (responses) {
                    // Unpack the responses
                    $scope.getAllFacilitiesArea = responses[0].data;
                    $scope.getAllProcessOverview = responses[1].data;
                    $scope.getAllDocumentTitle = responses[2].data;
                    $scope.getInspectionById = responses[3].data;
                    $scope.getCustomerLocationByCustomerIdDrpd = responses[4].data;
                    $scope.getAreaDetailsByLocationId = responses[5].data;

                    var facilities = $scope.getInspectionById.FacilitiesAreasIds || [];
                    var process = $scope.getInspectionById.ProcessOverviewIds || [];
                    var document = $scope.getInspectionById.ReferenceDocumentIds || [];

                    // Now we can safely map selected = true
                    $scope.getAllFacilitiesArea.forEach(function (f) {
                        f.selected = facilities.includes(f.FacilitiesAreaId);
                    });

                    $scope.getAllProcessOverview.forEach(function (p) {
                        p.selected = process.includes(p.ProcessOverviewId);
                    });

                    $scope.getAllDocumentTitle.forEach(function (d) {
                        d.selected = document.includes(d.DocumentId);
                    });

                    console.log("Document Checked IDs:", document);
                })
                .catch(function (error) {
                    console.error("Error loading data:", error);
                });
        }

        $scope.GetCheckedFacilitiesAndProcess = function () {
            var checkedFacilities = '';
            $scope.getAllFacilitiesArea.forEach(function (f) {
                if (f.selected) {
                    if (checkedFacilities != '') {
                        checkedFacilities += ",";
                    }
                    checkedFacilities += f.FacilitiesAreaId;
                }
            });
            $scope.checkedFacilitiesId = checkedFacilities;

            var checkedProcess = '';
            $scope.getAllProcessOverview.forEach(function (p) {
                if (p.selected) {
                    if (checkedProcess != '') {
                        checkedProcess += ",";
                    }
                    checkedProcess += p.ProcessOverviewId;
                }
            });
            $scope.checkedProcessId = checkedProcess;

            var checkedDocument = '';
            $scope.getAllDocumentTitle.forEach(function (t) {
                if (t.selected) {
                    if (checkedDocument != '') {
                        checkedDocument += ",";
                    }
                    checkedDocument += t.DocumentId;
                }
            });
            $scope.checkedDocumentId = checkedDocument;
        }

        $scope.ShowDatepickerClick = function () {
            $scope.ShowDatepicker = true;
            $scope.ReadOnlyDatePicker = false;
        }

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

        $scope.removeSelectedPic = function (filename) {
            console.log('file name--', filename);
            var i = $scope.fileList.indexOf('filename', filename);
            console.log('i--', i);
            $scope.fileList.splice(i, 1);
        }

        $scope.SaveInspectionDue = function () {
            $scope.GetCheckedFacilitiesAndProcess();
            var PdfList = [];
            var CapacityTable = 0;
            var PlanElevationDrawing = 0;
            if ($scope.capacitytable === true) {
                CapacityTable = 1;
            }

            if ($scope.planelevationdrawing) {
                PlanElevationDrawing = 1;
            }
            for (var i = 0; i < $scope.fileList.length; i++) {

                $scope.UploadFileIndividual($scope.fileList[i].file,
                    $scope.fileList[i].file.name,
                    $scope.fileList[i].file.type,
                    $scope.fileList[i].file.size,
                    i);
                PdfList[i] = $scope.fileList[i].file.name;
            }
            console.log('-----File Information-------', PdfList);
            if (true) {

            }
            var config = {
                CustomerId: $scope.customerId, CustomerLocationId: $scope.customerLocationId, CustomerAreaID: $scope.customerAreaId,
                EmployeeId: $scope.employeeId, InspectionDate: $scope.inspectionDate, InspectionType: $scope.inspectionType,
                CADDocuments: $scope.cADDocuments, FacilitiesAreasIds: $scope.checkedFacilitiesId, ProcessOverviewIds: $scope.checkedProcessId,
                ReferenceDocumentIds: $scope.checkedDocumentId, inspectionFileDrawing: PdfList
            }
            console.log('SaveInspectionDue', config);

            return $http({
                url: '/api/pageview/saveInspectionDue',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageInspectionNew';
                    window.location = url;
                }
                else {
                    $scope.errorNot = response.data;
                }
            }, function (error) {
                alert(error);
            });
        };


        $scope.UploadFileIndividual = function (fileToUpload, name, type, size, index) {
            //Create XMLHttpRequest Object
            var reqObj = new XMLHttpRequest();

            //open the object and set method of call(get/post), url to call, isAsynchronous(true/False)
            reqObj.open("POST", "/UploadDrawingFiles", true);

            //set Content-Type at request header.for file upload it's value must be multipart/form-data
            reqObj.setRequestHeader("Content-Type", "multipart/form-data");

            //Set Other header like file name,size and type
            reqObj.setRequestHeader('X-File-Name', name);
            reqObj.setRequestHeader('X-File-Type', type);
            reqObj.setRequestHeader('X-File-Size', size);

            // send the file
            reqObj.send(fileToUpload);


        }

        $scope.EditInspectionDue = function (Id) {
            $scope.GetCheckedFacilitiesAndProcess();
            var PdfList = [];

            for (var i = 0; i < $scope.fileList.length; i++) {

                $scope.UploadFileIndividual($scope.fileList[i].file,
                    $scope.fileList[i].file.name,
                    $scope.fileList[i].file.type,
                    $scope.fileList[i].file.size,
                    i);
                PdfList[i] = $scope.fileList[i].file.name;
            }

            var config = {
                CustomerId: $scope.customerId, CustomerLocationId: $scope.customerLocationId, CustomerAreaID: $scope.customerAreaId,
                EmployeeId: $scope.employeeId, InspectionDate: $scope.inspectionDate, InspectionType: $scope.inspectionType, InspectionId: Id,
                CADDocuments: $scope.cADDocuments, FacilitiesAreasIds: $scope.checkedFacilitiesId, ProcessOverviewIds: $scope.checkedProcessId,
                InspectionStatus: $scope.inspectionStatus, ReferenceDocumentIds: $scope.checkedDocumentId, inspectionFileDrawing: PdfList
            }
            console.log('editInspectionDue', config);
            return $http({
                url: '/api/pageview/editInspectionDue',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageInspectionDue';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveInspectionDue = function (id) {
            var config = { id: id }
            console.log('Remove Inspection --', config);
            return $http({
                url: '/api/pageview/removeInspectionDue',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                console.log('Remove Inspection Success --', response);
                if (response.data === "Ok") {
                    var url = '/Admin/ManageInspectionNew';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };
    }
})();
