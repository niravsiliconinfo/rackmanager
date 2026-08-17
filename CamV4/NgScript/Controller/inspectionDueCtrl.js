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

        // Function to get customer locations by customer ID
        $scope.GetCustomerLocationByCustomerIdDrpd = function (custid) {
            console.log('GetLocationbyCustomerDrpd', custid);
            if (custid) {
                $http.get('/api/pageview/getCustomerLocationByCustomerId', { params: { id: custid } }).then(function (response) {
                    $scope.getCustomerLocationByCustomerIdDrpd = response.data;
                    console.log('Customer Locations loaded:', $scope.getCustomerLocationByCustomerIdDrpd);
                }, function (response) {
                    $scope.waiting = false;
                });
            } else {
                $scope.getCustomerLocationByCustomerIdDrpd = [];
                $scope.getAreaDetailsByLocationId = [];
            }
        };

        // Function to get areas by location ID
        //$scope.GetAreaByLocationIdDrpd = function (locationId) {
        //    console.log('GetAreaByLocationIdDrpd', locationId);
        //    if (locationId) {
        //        $http.get('/api/pageview/getAreaDetailsByLocationId', { params: { LocationId: locationId } }).then(function (response) {
        //            $scope.getAreaDetailsByLocationId = response.data;
        //            console.log('Areas loaded:', $scope.getAreaDetailsByLocationId);
        //        }, function (response) {
        //            $scope.waiting = false;
        //        });
        //    } else {
        //        $scope.getAreaDetailsByLocationId = [];
        //    }
        //};
        $scope.GetAreaByLocationIdDrpd = function (id) {

            return $http.get('/api/pageview/getAreaDetailsByLocationId',
                { params: { id: id } })
                .then(function (response) {
                    $scope.getAreaDetailsByLocationId = response.data;
                });
        };
        $scope.GetAreaByFacilityIdDrpd = function (id) {

            return $http.get('/api/pageview/getAreaDetailsByFacilityId',
                { params: { id: id } })
                .then(function (response) {
                    $scope.getAreaDetailsByLocationId = response.data;
                });
        };

        

        if (window.location.pathname === "/Admin/EditInspectionDue") {

            $scope.ShowDatepicker = false;
            $scope.ReadOnlyDatePicker = true;

            var inspectionId = window.location.search.replace('?id=', '');

            // Load all inspection types
            $http.get('/api/pageview/getAllInspectionType').then(function (response) {
                $scope.getAllInspectionType = response.data;
                console.log('$scope.getAllInspectionType', $scope.getAllInspectionType);
                if ($scope.getAllInspectionType != null) { $scope.getAllInspectionTypecount = $scope.getAllInspectionType.length; }
                else { $scope.getAllInspectionTypecount = 0; }
            }, function (response) {
                $scope.waiting = false;
            });

            // Load all customers
            $http.get('/api/pageview/getAllCustomers').then(function (response) {
                $scope.getAllCustomers = response.data;
                console.log('$scope.getAllCustomers', $scope.getAllCustomers);
                if ($scope.getAllCustomers != null) { $scope.getAllCustomerscount = $scope.getAllCustomers.length; }
                else { $scope.getAllCustomerscount = 0; }
                $scope.totalgetAllCustomers = $scope.getAllCustomerscount;
            }, function (response) {
                $scope.waiting = false;
            });




            // Load NON-dependent lists + inspection data
            var facilitiesPromise = $http.get('/api/pageview/getAllFacilitiesArea');
            var processPromise = $http.get('/api/pageview/getAllProcessOverview');
            var documentPromise = $http.get('/api/pageview/getAllDocumentTitle');

            var inspectionPromise = $http.get('/api/pageview/getInspectionById', {
                params: { InspectionId: inspectionId }
            });

            $q.all([
                facilitiesPromise,
                processPromise,
                documentPromise,
                inspectionPromise
            ]).then(function (responses) {

                $scope.getAllFacilitiesArea = responses[0].data;
                $scope.getAllProcessOverview = responses[1].data;
                $scope.getAllDocumentTitle = responses[2].data;
                $scope.getInspectionById = responses[3].data;

                var d = $scope.getInspectionById;

                console.log('Inspection data loaded:', d);

                // Set CAD documents
                $scope.cADDocuments = d.CADDocuments;

                // Set the inspection type - keep as-is from API
                //$scope.inspectionType = d.InspectionType;
                $scope.inspectionType = d.InspectionType
                    ? d.InspectionType.trim()
                    : '';
                console.log('Set inspectionType:', $scope.inspectionType);

                // Set employee ID - keep as-is from API

                // Load all employees
                $http.get('/api/pageview/getAllEmployee').then(function (response) {
                    $scope.getAllEmployee = response.data;
                    $scope.employeeId = d.EmployeeId;
                    console.log('Set employeeId:', $scope.employeeId);
                }, function (response) {
                    $scope.waiting = false;
                });
                // Set inspection date
                /*$scope.inspectionDate = d.InspectionDate;*/
                // Set inspection date - format as yyyy-MM-dd for HTML date input
                if (d.InspectionDate) {
                    var date = new Date(d.InspectionDate);
                    var year = date.getFullYear();
                    var month = ('0' + (date.getMonth() + 1)).slice(-2);
                    var day = ('0' + date.getDate()).slice(-2);
                    $scope.inspectionDate = year + '-' + month + '-' + day;
                }

                // Set inspection status
                //$scope.inspectionStatus = d.InspectionStatus;
                // Set inspection status - convert to string to match option values
                $scope.inspectionStatus = d.InspectionStatus ? d.InspectionStatus.toString() : '';
                console.log('Set inspectionStatus:', $scope.inspectionStatus);

                // Load customer locations based on customer ID, then load areas
                //if (d.CustomerId) {
                //    $http.get('/api/pageview/getCustomerLocationByCustomerId', { params: { id: d.CustomerId } }).then(function (response) {
                //        $scope.getCustomerLocationByCustomerIdDrpd = response.data;
                //        console.log('Customer locations loaded for edit:', $scope.getCustomerLocationByCustomerIdDrpd);

                //        // Set customer ID after locations are loaded
                //        $scope.customerId = d.CustomerId;
                //        console.log('Set customerId:', $scope.customerId);

                //        // Load areas based on location ID
                //        if (d.CustomerLocationId) {
                //            $http.get('/api/pageview/getAreaDetailsByLocationId', { params: { LocationId: d.CustomerLocationId } }).then(function (response) {
                //                $scope.getAreaDetailsByLocationId = response.data;
                //                console.log('Areas loaded for edit:', $scope.getAreaDetailsByLocationId);

                //                // Set the customer location ID after locations are loaded
                //                $scope.customerLocationId = d.CustomerLocationId;
                //                console.log('Set customerLocationId:', $scope.customerLocationId);

                //                // Set the customer area ID after areas are loaded
                //                if (d.CustomerAreaID) {
                //                    $scope.CustomerAreaID = d.CustomerAreaID;
                //                    console.log('Set CustomerAreaID:', $scope.CustomerAreaID);
                //                }
                //            }, function (response) {
                //                console.error('Error loading areas:', response);
                //            });
                //        } else {
                //            // Set location ID even if no area
                //            $scope.customerLocationId = d.CustomerLocationId;
                //            console.log('Set customerLocationId (no area):', $scope.customerLocationId);
                //        }
                //    }, function (response) {
                //        console.error('Error loading customer locations:', response);
                //    });
                //}

                if (d.CustomerId) {
                    $http.get('/api/pageview/getCustomerLocationByCustomerId', { params: { id: d.CustomerId } }).then(function (response) {
                        $scope.getCustomerLocationByCustomerIdDrpd = response.data;
                        console.log('Customer locations loaded for edit:', $scope.getCustomerLocationByCustomerIdDrpd);

                        // Set customer ID after locations are loaded
                        $scope.customerId = d.CustomerId;
                        console.log('Set customerId:', $scope.customerId);

                        // Set location ID RIGHT HERE after locations are loaded
                        $scope.customerLocationId = d.CustomerLocationId;
                        console.log('Set customerLocationId:', $scope.customerLocationId);

                        //$scope.GetFacilityByLocationIdDrpd(d.CustomerLocationId, d.customerFacilityId);                        
                        $scope.GetFacilityByLocationIdDrpd(d.CustomerLocationId,d.customerFacilityId,d.CustomerAreaID);

                        // Load areas based on location ID
                        if (d.CustomerLocationId) {
                            $http.get('/api/pageview/getAreaDetailsByLocationId', { params: { LocationId: d.CustomerLocationId } }).then(function (response) {
                                $scope.getAreaDetailsByLocationId = response.data;
                                console.log('Areas loaded for edit:', $scope.getAreaDetailsByLocationId);

                                // Set the customer area ID after areas are loaded
                                if (d.CustomerAreaID) {
                                    $scope.CustomerAreaID = d.CustomerAreaID;
                                    console.log('Set CustomerAreaID:', $scope.CustomerAreaID);
                                }
                            }, function (response) {
                                console.error('Error loading areas:', response);
                            });
                        }
                    }, function (response) {
                        console.error('Error loading customer locations:', response);
                    });
                }

                // Set selected facilities areas
                if (d.FacilitiesAreasIds) {
                    var selectedFacilities = d.FacilitiesAreasIds.split(',');
                    $scope.getAllFacilitiesArea.forEach(function (f) {
                        f.selected = selectedFacilities.indexOf(f.FacilitiesAreaId.toString()) !== -1;
                    });
                }

                // Set selected process overviews
                if (d.ProcessOverviewIds) {
                    var selectedProcess = d.ProcessOverviewIds.split(',');
                    $scope.getAllProcessOverview.forEach(function (p) {
                        p.selected = selectedProcess.indexOf(p.ProcessOverviewId.toString()) !== -1;
                    });
                }

                // Set selected document titles
                if (d.ReferenceDocumentIds) {
                    var selectedDocuments = d.ReferenceDocumentIds.split(',');
                    $scope.getAllDocumentTitle.forEach(function (t) {
                        t.selected = selectedDocuments.indexOf(t.DocumentId.toString()) !== -1;
                    });
                }

            }, function (error) {
                console.error('Error loading inspection data:', error);
            });
        }

        //$scope.GetFacilityByLocationIdDrpd = function (id, selectedFacilityId) {

        //    $http.get('/api/pageview/getFacilityByLocationId',
        //        { params: { id: id } })
        //        .then(function (response) {

        //            $scope.getFacilityDetailsByLocationId = response.data;

        //            $scope.customerFacilityId = selectedFacilityId;

        //            if (selectedFacilityId) {
        //                $scope.GetAreaByFacilityIdDrpd(selectedFacilityId);
        //            }
        //        });
        //}

        $scope.GetFacilityByLocationIdDrpd = function (locationId, selectedFacilityId, selectedAreaId) {

            console.log('getFacilityByLocationId', locationId);

            return $http.get('/api/pageview/getFacilityByLocationId', {
                params: { id: locationId }
            }).then(function (response) {

                $scope.getFacilityDetailsByLocationId = response.data;

                // Select the saved facility
                $scope.customerFacilityId = selectedFacilityId;

                // Load Area based on whether a Facility exists
                //if (selectedFacilityId) {

                //    return $scope.GetAreaByFacilityIdDrpd(selectedFacilityId)
                //        .then(function () {
                //            $scope.CustomerAreaID = selectedAreaId;
                //        });

                //} else {

                //    return $scope.GetAreaByLocationIdDrpd(locationId)
                //        .then(function () {
                //            $scope.CustomerAreaID = selectedAreaId;
                //        });
                //}

                $scope.LoadAreaDropdown(locationId, selectedFacilityId)
                    .then(function () {
                        $scope.CustomerAreaID = selectedAreaId;
                    });

            });
        };

        $scope.LoadAreaDropdown = function (locationId, facilityId) {

            if (facilityId) {
                return $scope.GetAreaByFacilityIdDrpd(facilityId);
            }

            return $scope.GetAreaByLocationIdDrpd(locationId);
        };
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
                CustomerId: $scope.customerId, CustomerLocationId: $scope.customerLocationId, CustomerAreaID: $scope.CustomerAreaID, CustomerFacilityID: $scope.customerFacilityId,
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
                CustomerId: $scope.customerId, CustomerLocationId: $scope.customerLocationId, CustomerAreaID: $scope.CustomerAreaID,
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
