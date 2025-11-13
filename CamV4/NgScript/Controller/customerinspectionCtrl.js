(function () {
    'use strict';

    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp')
        .controller('customerinspectionCtrl', customerinspectionCtrl);

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

    app.controller('customerinspectionCtrl', customerinspectionCtrl);

    customerinspectionCtrl.$inject = ['$scope', '$http', 'sharedFilterService','$rootScope'];

    function customerinspectionCtrl($scope, $http, sharedFilterService, $rootScope) {
        console.log('-----------customerinspectionCtrl--------------');
        $scope.getAllInspectionDueByCustomerId = [];
        $scope.getAllInspectionByCustomerId = [];        

        $scope.scheduleFilter = sharedFilterService.getScheduleFilter() || {};
        $scope.statusFilter = sharedFilterService.getStatusFilter() || {};

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

       
        // 2. Set selected status from shared filter
        var savedStatusFilter = sharedFilterService.getStatusFilter();
        if (savedStatusFilter && savedStatusFilter.selectedIDs) {
            $scope.setSelectedInspectionStatusIDs(savedStatusFilter.selectedIDs);
        }

        //// 3. Get selected InspectionStatusId values
        //$scope.getSelectedInspectionStatusIDs = function () {
        //    return $scope.InspectionStatusLayout
        //        .filter(function (s) { return s.selected; })
        //        .map(function (s) { return s.InspectionStatusId; });
        //};

        $scope.isEditingLabour = false;

        $scope.isValid = function () {
            //console.log('------------$scope.btnAddItemComponentPrice---------', $scope.newquotationItem.quantity);
            return $scope.newquotationItem.quantity && $scope.newquotationItem.unitPrice && $scope.newquotationItem.weight &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.quantity) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.unitPrice) &&
                /^\d+(\.\d+)?$/.test($scope.newquotationItem.weight);
        };
        
        init();

        function init() {
            // Get filters from shared memory
            var schedule = sharedFilterService.getScheduleFilter();
            var status = sharedFilterService.getStatusFilter();            

            if (window.location.pathname == "/Customer/ManageInspectionDue") {
                //loadDueInspections();
                if (schedule && Object.keys(schedule).length > 0) {
                    console.log('from init for LoadInpsectionsDue');
                    $scope.scheduleFilter = schedule;
                    loadInspectionDue(schedule);
                } else {
                    console.log('from init for LoadInpsectionsDue');
                    loadInspectionDue(); // Load default data
                }
            } else if (window.location.pathname == "/Customer/ManageInspection") {
                //loadAllInspections();
                if (status && Object.keys(status).length > 0) {
                    console.log('from init for LoadInpsections');
                    $scope.statusFilter = status;
                    loadInspections(status);
                } else {
                    console.log('from init for LoadInpsections');
                    loadInspections(); // Load default data
                }
            }            
        }

        function loadInspectionDue(filter) {
            console.log('Calling ----> loadInspectionDue');
            const filters = filter;
            console.log("------XXXXXXXXXXXXXXXXXXXX------ CALL ------- -----getDueInspectionsCustomerFilters");
            $http.post('/api/pageview/getDueInspectionsCustomerFilters', filters)
                .then(function (response) {
                    $scope.getAllInspectionDueByCustomerId = response.data;
                }, function (error) {
                    console.error("Error loading due inspections:", error);
                });
        }

        function loadInspections(filter) {            
            const filters = filter;
            console.log('Calling ----> loadInspections---------------------------', filters);
            $http.post('/api/pageview/getAllInspectionsCustomerFilters', filters)
                .then(function (response) {
                    $scope.getAllInspectionByCustomerId = response.data;
                }, function (error) {
                    console.error("Error loading all inspections:", error);
                });
        }

        // Expose refresh methods for button calls
        $scope.refreshDueInspections = function () {
            loadDueInspections();
        };

        $scope.refreshAllInspections = function () {
            loadAllInspections();
        };

        $rootScope.$on('scheduleFilterUpdated', function () {
            console.error("Load function when click on button for scheduleFilterUpdated");
            const filters = sharedFilterService.getScheduleFilter();
            console.error("Load function when click on button for scheduleFilterUpdated");
            loadInspectionDue(filters);
        });

        $rootScope.$on('statusFilterUpdated', function () {
            const filters = sharedFilterService.getStatusFilter();
            console.log("Load function when click on button for statusFilterUpdated", filters);            
            loadInspections(filters);
        });
     
        $scope.InspectionDetailClickByCustomer = function (id) {
            $http.get('/api/pageview/getInspectionById', { params: { InspectionId: id } }).then(function (response) {
                $scope.getInspectionById = response.data;
                if ($scope.getInspectionById.InspectionStatus > 3) //|| $scope.getInspectionById.InspectionStatus == 4
                {
                    var url = '/Customer/InspectionDetails?id=' + id; //InspectionId
                    window.location = url;
                }
            }, function (response) {
                $scope.waiting = false;
            });
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

        if (window.location.pathname == "/Admin/EditInspectionDue") {

            $http.get('/api/pageview/getAllFacilitiesArea').then(function (response) {
                $scope.getAllFacilitiesArea = response.data;
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getAllProcessOverview').then(function (response) {
                $scope.getAllProcessOverview = response.data;
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getAllDocumentTitle').then(function (response) {
                $scope.getAllDocumentTitle = response.data;
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getAllCustomerLocations').then(function (response) {
                $scope.getCustomerLocationByCustomerIdDrpd = response.data;
                console.log('getAllCustomerLocations0000--', $scope.getCustomerLocationByCustomerIdDrpd);
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getAllCustomerArea').then(function (response) {
                $scope.getAreaDetailsByLocationId = response.data;
                console.log('getAllCustomerArea0000--', $scope.getAreaDetailsByLocationId);
            }, function (response) {
                $scope.waiting = false;
            });

            var para = window.location.search;
            para = para.replace('?id=', '');
            $http.get('/api/pageview/getInspectionById', { params: { InspectionId: para } }).then(function (response) {
                $scope.getInspectionById = response.data;
                var process = $scope.getInspectionById.ProcessOverviewIds;
                var document = $scope.getInspectionById.ReferenceDocumentIds;
                var facilities = $scope.getInspectionById.FacilitiesAreasIds;

                angular.forEach($scope.getAllFacilitiesArea, function (f) {
                    if (facilities.indexOf(f.FacilitiesAreaId) > -1) {
                        f.selected = true;
                    }
                });

                angular.forEach($scope.getAllProcessOverview, function (p) {
                    if (process.indexOf(p.ProcessOverviewId) > -1) {
                        p.selected = true;
                    }
                });
                angular.forEach($scope.getAllDocumentTitle, function (t) {
                    if (document.indexOf(t.DocumentId) > -1) {
                        t.selected = true;
                    }
                });
                console.log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX checkedDocument || XXXXXXXXXXXXXXXXXXXXXXXXXXX", document);
            }, function (response) {
                $scope.waiting = false;
            });

            $scope.ShowDatepicker = false;
            $scope.ReadOnlyDatePicker = true;
        }

        if (window.location.pathname == "/CustomerLocationContact/ManageInspection") {
            $http.get('/api/pageview/getInspectionByContactId').then(function (response) {
                $scope.getInspectionByContactId = response.data;
                console.log('$scope.getInspectionByContactId123123', $scope.getInspectionByContactId);
                if ($scope.getInspectionByContactId != null) { $scope.getInspectionByContactIdcount = $scope.getInspectionByContactId.length; }
                else { $scope.getInspectionByContactIdcount = 0; }
                $scope.totalgetInspectionByContactId = $scope.getInspectionByContactIdcount;
            }, function (response) {
                $scope.waiting = false;
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

        if (window.location.pathname == "/Admin/InspectionSheet" || window.location.pathname == "/Customer/InspectionDetails" || window.location.pathname == "/Employee/InspectionDetail" || window.location.pathname == "/Customer/GenerateQuotation") {

            var para = window.location.search;
            console.log('para inspection sheet', para);
            para = para.replace('?id=', '');
            console.log('para inspection sheet2', para);
            $http.get('/api/pageview/getInspectionDetailsForSheet', { params: { id: para } }).then(function (response) {
                $scope.getInspectionDetailsForSheet = response.data;

                console.log('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*************XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX', $scope.getInspectionDetailsForSheet);


                if ($scope.getInspectionDetailsForSheet.objQuotation != null) {
                    $scope.quotationsalespersonid = $scope.getInspectionDetailsForSheet.objQuotation.QuotationSalesPersonId;
                }
                $scope.FacilitiesAreas = $scope.getInspectionDetailsForSheet.FacilitiesAreas;
                $scope.facilitiesAreasList = new Array();
                $scope.facilitiesAreasList = $scope.FacilitiesAreas.substring(0, $scope.FacilitiesAreas.length - 1);
                $scope.facilitiesAreasList = $scope.facilitiesAreasList.split(',');
                console.log('$scope.facilitiesAreasList', $scope.facilitiesAreasList);

                $scope.ProcessOverview = $scope.getInspectionDetailsForSheet.ProcessOverviews;
                $scope.processOverviewList = new Array();
                $scope.processOverviewList = $scope.ProcessOverview.substring(0, $scope.ProcessOverview.length - 1);
                $scope.processOverviewList = $scope.processOverviewList.split(';');
                console.log('$scope.ProcessOverviewList', $scope.processOverviewList);

                var contacts = $scope.getInspectionDetailsForSheet.CustomerContactIds;
                console.log('contactsId', contacts);
                console.log('2222', $scope.getInspectionDetailsForSheet.ListCustomerLocationContacts);
                angular.forEach($scope.getInspectionDetailsForSheet.ListCustomerLocationContacts, function (Contact) {
                    if (contacts != null) {
                        if (contacts.indexOf(Contact.LocationContactId) > -1) {
                            Contact.selected = true;
                        }
                    }
                });

                $http.get('/api/pageview/getAllDocumentTitle').then(function (response) {
                    $scope.getAllDocumentTitleInspection = response.data;
                    console.log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX checkedDocument---- XXXXXXXXXXXXXXXXXXXXXXXXXXX", $scope.getAllDocumentTitleInspection);
                    var document = $scope.getInspectionDetailsForSheet.ReferenceDocumentIds;

                    angular.forEach($scope.getAllDocumentTitleInspection, function (t) {
                        if (document.indexOf(t.DocumentId) > -1) {
                            console.log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX t.DocumentId--- XXXXXXXXXXXXXXXXXXXXXXXXXXX", t.DocumentId);
                            t.selected = true;
                        }
                    });
                    console.log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX checkedDocument XXXXXXXXXXXXXXXXXXXXXXXXXXX", document);
                }, function (response) {
                    $scope.waiting = false;
                });

                if (window.location.pathname == "/Customer/InspectionDetails") {

                    console.log('Selected Year:', para);
                    //var selectedYear = $('#selectedYearCustomer').val();
                    console.log('Selected Year:', '2024');
                    $http.get('/api/pageview/getDeficienciesBreakdownCategoriesInspection', { params: { InspectionId: para } }).then(function (response) {
                        $scope.getDeficienciesBreakdownCategorieslistInspection = response.data;
                        console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslistInspection);
                    }, function (response) {
                        $scope.waiting = false;
                    });


                    $http.get('/api/pageview/getDeficienciesTrendFromPreviousYearsForCustomerLocation', { params: { customerLocationid: response.data.CustomerLocationId } })
                        .then(function (response) {
                            const dataFromApi = response.data; // Assuming response.data contains the array
                            const years = [...new Set(dataFromApi.map(item => item.Years))];
                            console.log('Unique Years:', years);

                            // Prepare data for Chart.js
                            const data = {
                                labels: years,
                                datasets: [
                                    {
                                        label: 'Minor',
                                        data: years.map(year => {
                                            const entry = dataFromApi.find(item => item.Years == year && item.Classifications === 'Minor');
                                            return entry ? entry.InspectionDeficiencyCnt : 0;
                                        }),
                                        backgroundColor: '#00CC00'
                                    },
                                    {
                                        label: 'Intermediate',
                                        data: years.map(year => {
                                            const entry = dataFromApi.find(item => item.Years == year && item.Classifications === 'Intermediate');
                                            return entry ? entry.InspectionDeficiencyCnt : 0;
                                        }),
                                        backgroundColor: '#FFFF00'
                                    },
                                    {
                                        label: 'Major',
                                        data: years.map(year => {
                                            const entry = dataFromApi.find(item => item.Years == year && item.Classifications === 'Major');
                                            return entry ? entry.InspectionDeficiencyCnt : 0;
                                        }),
                                        backgroundColor: '#FF0000'
                                    }
                                ]
                            };

                            const ctx = document.getElementById('deficiencyChart').getContext('2d');
                            const deficiencyChart = new Chart(ctx, {
                                type: 'bar',
                                data: data,
                                options: {
                                    scales: {
                                        y: {
                                            beginAtZero: true,
                                            title: {
                                                display: true,
                                                text: 'Inspection Deficiency Count'
                                            }
                                        },
                                        x: {
                                            title: {
                                                display: true,
                                                text: 'Years'
                                            }
                                        }
                                    },
                                    plugins: {
                                        legend: {
                                            display: true,
                                            position: 'top'
                                        }
                                    },
                                    responsive: true,
                                    maintainAspectRatio: false
                                }
                            });
                        })
                        .catch(function (error) {
                            console.log("Error fetching data:", error);
                        });

                    console.log('Before getDeficienciesbySeverityCustomerInspection------------------------------------------------', para);
                    $http.get('/api/pageview/getDeficienciesbySeverityCustomerInspection', { params: { inspectionid: para } })
                        .then(function (response) {

                            var labels = [];
                            var data = [];
                            var backgroundColors = [];

                            var deficiencies = response.data;

                            deficiencies.forEach(function (deficiency) {
                                labels.push(deficiency.Classifications);
                                data.push(deficiency.InspectionDeficiencyCnt);
                                backgroundColors.push(deficiency.ClassificationsColor);
                            });
                            console.log('Response getDeficienciesbySeverityCustomerInspection------------------------------------------------', labels);
                            console.log('Response getDeficienciesbySeverityCustomerInspection------------------------------------------------', data);
                            console.log('Response getDeficienciesbySeverityCustomerInspection------------------------------------------------', backgroundColors);

                            var ctx = document.getElementById('pie-chart-customer-inspection').getContext('2d');
                            var deficiencyChart = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: labels,
                                    datasets: [{
                                        data: data,
                                        backgroundColor: backgroundColors
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    plugins: {
                                        legend: {
                                            position: 'top'
                                        },
                                        tooltip: {
                                            callbacks: {
                                                label: function (tooltipItem) {
                                                    return tooltipItem.label + ': ' + tooltipItem.raw;
                                                }
                                            }
                                        }
                                    }
                                }
                            });
                        })
                        .catch(function (error) {
                            alert(error);
                            console.log("Error fetching data:", error);
                        });                    
                }
                
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.fetchItemDetails = function () {
            var Surcharge = $scope.getInspectionDetailsForSheet.objQuotation.QuotationSurcharge;
            var Markup = $scope.getInspectionDetailsForSheet.objQuotation.QuotationMarkup;
            console.log("$scope.getInspectionDetailsForSheet.objQuotation.Surcharge", $scope.getInspectionDetailsForSheet.objQuotation.Surcharge);
            console.log("$scope.getInspectionDetailsForSheet.objQuotation.Markup", $scope.getInspectionDetailsForSheet.objQuotation.Markup);
            const itemPartNo = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo;
            $http.get('/api/pageview/getComponentItemDetails', { params: { ItemPartNo: itemPartNo } }).then(function (response) {
                console.log(response.data);
                const quantity = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity) || 0;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemDescription = response.data.ComponentPriceDescription;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = response.data.ComponentPrice;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight = response.data.ComponentWeight;
                var ItemPrice = ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice * Surcharge * Markup).toFixed(2);
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPrice = ItemPrice;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabour = response.data.ComponentLabourTime;
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.fetchItemDetailsChild = function (item) {
            var Surcharge = item.ItemSurcharge;
            var Markup = item.ItemMarkup;
            const itemPartNo = item.ItemPartNo;
            $http.get('/api/pageview/getComponentItemDetails', { params: { ItemPartNo: itemPartNo } }).then(function (response) {
                console.log(response.data);
                var labourTime = 0;
                var ComponentPrice = 0;
                if (response.data.ComponentLabourTime != "") {
                    labourTime = response.data.ComponentLabourTime;
                }
                if (response.data.ComponentPrice != "") {
                    ComponentPrice = response.data.ComponentPrice;
                }
                item.ItemDescription = response.data.ComponentPriceDescription;
                item.ItemUnitPrice = response.data.ComponentPrice;
                item.ItemWeight = response.data.ComponentWeight;
                var ItemPrice = (ComponentPrice * Surcharge * Markup).toFixed(2);
                item.ItemPrice = ItemPrice;
                item.ItemLabour = response.data.ComponentLabourTime;
                item.ItemLabourTotal = item.ItemQuantity * labourTime;
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.editItem = function (item) {
            item.isEditing = true;
        };

        $scope.saveItem = function (item) {
            // Call your API to save the data
            //console.clear;
            console.log(item);
            var isTBD = item.IsTBD !== undefined
                ? item.IsTBD
                : false;
            const quantity = parseFloat(item.ItemQuantity) || 0;
            let Surcharge = parseFloat(item.ItemSurcharge) || 0;
            let Markup = parseFloat(item.ItemMarkup) || 0;
            let ItemUnitPrice = parseFloat(item.ItemUnitPrice) || 0;
            let ItemPrice = (ItemUnitPrice * Surcharge * Markup).toFixed(2);
            let LineTotal = (ItemPrice * quantity).toFixed(2);
            const ItemWeight = parseFloat(item.ItemWeight) || 0;


            var InspectionId = item.InspectionId;
            var QuotationId = item.QuotationId;
            var ItemPartNo = item.ItemPartNo;
            var ItemDescription = item.ItemDescription;
            const ItemWeightTotal = quantity * ItemWeight;

            var ItemLabour = item.ItemLabour;

            //if (ItemLabour === "") {
            //    ItemLabour = 0;
            //}
            console.log('ItemLabour - $scope.saveItem', ItemLabour);
            const ItemLabourTotal = parseFloat(ItemLabour) * quantity;
            console.log('ItemLabourTotal - $scope.saveItem', ItemLabourTotal);

            /* $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabourTotal = ItemLabourTotal;*/
            var config = {
                QuotationInspectionItemId: item.QuotationInspectionItemId,
                InspectionId: InspectionId,
                QuotationId: QuotationId,
                ItemPartNo: ItemPartNo,
                ItemDescription: ItemDescription,
                ItemUnitPrice: ItemUnitPrice,
                ItemSurcharge: Surcharge,
                ItemMarkup: Markup,
                ItemPrice: ItemPrice,
                ItemWeight: ItemWeight,
                ItemQuantity: String(quantity),
                ItemWeightTotal: ItemWeightTotal,
                LineTotal: LineTotal,
                isTBD: isTBD,
                ItemLabour: String(ItemLabour),
                ItemLabourTotal: String(ItemLabourTotal),
                inline: true
            };
            console.log('------------------------config---------------:$scope.saveItem', config);

            return $http({
                url: '/api/pageview/saveQuotationItemsByAdmin',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                item.isEditing = false;
                console.log('------------------------Success---------------:$scope.saveItem', response);
                console.log('Remove Quotation Items By Admin Success --', response.data);

                $scope.getInspectionDetailsForSheet.objQuotation = response.data;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response.data.objQuotationItems;

                //let subtotal = 0;                        
                //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                //    console.log('------------------------YYYYYYY---------------', item);
                //    subtotal += parseFloat(item.LineTotal);
                //});
                console.log('------------------------YYYYYYY---------------', response.data.Subtotal);
                $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (response.data.Subtotal).toFixed(2);

                //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                //let GSTVal = parseFloat(subtotal * gstRate);
                $scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (response.data.GSTValue).toFixed(2);
                $scope.getInspectionDetailsForSheet.objQuotation.Total = (response.data.Total).toFixed(2);
                /*$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response.data;*/

                //let subtotal = 0;
                //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                //    subtotal += parseFloat(item.LineTotal);
                //});

                //$scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);

                //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                //let GSTVal = parseFloat(subtotal * gstRate);
                //$scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
                //$scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);

            }, function (error) {
                console.log('error ----------$scope.saveItem ', error);
                alert(error);
            });
        };

        $scope.editItemlabour = function (isEditingLabour) {
            $scope.isEditingLabour = true;
        };

        $scope.saveItemlabour = function (isEditingLabour) {
            if (isEditingLabour) {

                var dataQuotation = {
                    QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId,
                    LabourUnitPrice: $scope.getInspectionDetailsForSheet.objQuotation.LabourUnitPrice,
                    TotalLabour: $scope.getInspectionDetailsForSheet.objQuotation.TotalLabour,
                    QuotationNotes: $scope.getInspectionDetailsForSheet.objQuotation.QuotationNotes,
                };

                console.log('dataQuotation', dataQuotation);

                $http.get($http({
                    url: '/api/pageview/updateLabourByAdmin',
                    method: "POST",
                    data: dataQuotation,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    $scope.isEditingLabour = false;
                    console.log('------------------------Success---------------:', response.data);
                    $scope.getInspectionDetailsForSheet.objQuotation = response.data;
                    $scope.getInspectionDetailsForSheet.objQuotation.TotalUnitPrice = (response.data.TotalUnitPrice).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (response.data.Subtotal).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (response.data.GSTValue).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.Total = (response.data.Total).toFixed(2);
                }).catch(function (error) {
                    console.error('Error in first API call', error);
                }));


                console.log('On save click labour', $scope.getInspectionDetailsForSheet.objQuotation.TotalLabour);

            }
        };

        $scope.calculateAllSurchargeMarkupItemsTotal = function () {
            const surcharge = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.Surcharge) || 0;
            const markup = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.Markup) || 0;

            console.log('surcharge change', surcharge);
            console.log('markup change', markup);


        };

        $scope.calculateTotals = function () {

            let ItemUnitPrice = 0;
            let ItemPrice = 0;
            const surcharge = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.QuotationSurcharge) || 0;
            const markup = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.QuotationMarkup) || 0;

            if ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.IsTBD) {
                ItemUnitPrice = 0;
                const LineTotal = 0;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = "0";
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.LineTotal = 0;
                const iLabourTime = 0;
                const iLabourTimeTotal = 0;
                if ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabour != '') {
                    iLabourTime = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabour;
                    iLabourTimeTotal = quantity * iLabourTime;
                }
                const ItemWeight = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight) || 0;
                const quantity = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity) || 0;
                const ItemWeightTotal = quantity * ItemWeight;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeightTotal = (ItemWeightTotal).toFixed(2);
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.LineTotal = LineTotal;//).toFixed(2);
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabourTotal = (iLabourTimeTotal).toFixed(2);
            }
            else {
                //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = "0.0";
                const itemPartNo = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo;
                $http.get('/api/pageview/getComponentItemDetails', { params: { ItemPartNo: itemPartNo } }).then(function (response) {
                    console.log(response.data);
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = response.data.ComponentPrice;
                    ItemUnitPrice = response.data.ComponentPrice; //parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice) || 0;

                    const ItemWeight = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight) || 0;
                    const quantity = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity) || 0;
                    ItemPrice = ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice * surcharge * markup).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPrice = ItemPrice;
                    /*ItemPrice = ItemUnitPrice * surcharge * markup;*/
                    //const iLabourTime = 0;
                    var iLabourTimeTotal = 0;
                    //console.log('in labour calc-------', $.trim(response.data.ComponentLabourTime));
                    //iLabourTime = parseFloat($.trim(response.data.ComponentLabourTime));

                    iLabourTimeTotal = quantity * response.data.ComponentLabourTime;
                    console.log('in labour calc', iLabourTimeTotal);
                    //if (iLabourTime != '') {
                    //    console.log('in labour calc');                        

                    //}
                    const LineTotal = (ItemPrice * $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity).toFixed(2);
                    const ItemWeightTotal = quantity * ItemWeight;
                    console.log('quantity - LineTotal', ItemUnitPrice + '||' + surcharge + '||' + markup + ' ||' + LineTotal);
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeightTotal = (ItemWeightTotal).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.LineTotal = LineTotal;//).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabourTotal = iLabourTimeTotal;// (iLabourTimeTotal).toFixed(2);
                }, function (response) {
                    $scope.waiting = false;
                });
            }

            //ItemUnitPrice = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice) || 0;

            /*const ItemUnitPrice = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice) || 0;*/


        };

        $scope.updateGSTTotals = function () {
            let subtotal = 0;
            let TotalUnitPrice = 0;
            $scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                subtotal += parseFloat(item.LineTotal);
            });

            $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);
            //console.log("$scope.getInspectionDetailsForSheet.objQuotation.TotalUnitPrice", $scope.getInspectionDetailsForSheet.objQuotation.TotalUnitPrice);
            TotalUnitPrice = $scope.getInspectionDetailsForSheet.objQuotation.TotalUnitPrice;
            //console.log("$scope.getInspectionDetailsForSheet.objQuotation.TotalUnitPrice", TotalUnitPrice);
            subtotal = subtotal + TotalUnitPrice;
            console.log("subtotal after total", subtotal);
            $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);
            const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
            let GSTVal = parseFloat(subtotal * gstRate);
            $scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
            $scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);

            if ($scope.getInspectionDetailsForSheet.objQuotation.QuotationNo != null) {
                var dataQuotation = {
                    QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId,
                    GSTPer: $scope.getInspectionDetailsForSheet.objQuotation.GSTPer,
                    Subtotal: $scope.getInspectionDetailsForSheet.objQuotation.Subtotal,
                    GSTValue: $scope.getInspectionDetailsForSheet.objQuotation.GSTValue,
                    Total: $scope.getInspectionDetailsForSheet.objQuotation.Total
                };

                console.log('dataQuotation', dataQuotation);

                return $http({
                    url: '/api/pageview/updateQuotationGSTAdmin',
                    method: "POST",
                    data: dataQuotation,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    console.log('------------------------Success---------------:', response);
                }, function (error) {
                    alert(error);
                });
            }

        };

        $scope.btnAddItemComponentPrice = function () {
            if ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity && $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice) {

                let ItemUnitPrice = 0;
                if ($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.IsTBD) {
                    ItemUnitPrice = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = "TBD";
                }
                else {
                    ItemUnitPrice = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice) || 0;
                }
                let Surcharge = $scope.getInspectionDetailsForSheet.objQuotation.QuotationSurcharge;
                let Markup = $scope.getInspectionDetailsForSheet.objQuotation.QuotationMarkup;
                let ItemPrice = (ItemUnitPrice * Surcharge * Markup).toFixed(2);
                let LineTotal = (ItemPrice * $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity).toFixed(2);
                const ItemWeight = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight) || 0;
                const quantity = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity) || 0;
                //let GSTVal = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer;
                let subtotal = 0;
                //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.push({
                //    QuotationInspectionItemId: 0,
                //    InspectionId: $scope.getInspectionDetailsForSheet.InspectionId,
                //    QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId,
                //    ItemPartNo: $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo,
                //    ItemDescription: $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemDescription,
                //    ItemUnitPrice: parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice),
                //    ItemSurcharge: parseFloat(Surcharge),
                //    ItemMarkup: parseFloat(Markup),
                //    ItemPrice: parseFloat(ItemPrice),
                //    ItemQuantity: parseInt($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity),
                //    ItemWeight: parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight),
                //    LineTotal: parseFloat(LineTotal)
                //});

                //var quotationItems = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems;
                //var itemsArray = [];


                //var itemData = {
                //    InspectionId: $scope.getInspectionDetailsForSheet.InspectionId,
                //    QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId,
                //    ItemPartNo: $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo,
                //    ItemDescription: $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemDescription,
                //    ItemUnitPrice: parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice),
                //    ItemSurcharge: parseFloat(Surcharge),
                //    ItemMarkup: parseFloat(Markup),
                //    ItemPrice: parseFloat(ItemPrice),
                //    ItemQuantity: parseInt($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity),
                //    ItemWeight: parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight),
                //    LineTotal: parseFloat(LineTotal)
                //};
                //var objSaveQuotationRequest = {
                //    QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId,
                //    QuotationComponentList: itemData
                //}

                var InspectionId = $scope.getInspectionDetailsForSheet.InspectionId;
                var QuotationId = $scope.getInspectionDetailsForSheet.objQuotation.QuotationId;
                var ItemPartNo = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo;
                var ItemDescription = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemDescription;
                /*var ItemUnitPrice = parseFloat($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice);*/
                var ItemSurcharge = parseFloat(Surcharge);
                var ItemMarkup = parseFloat(Markup);
                ItemPrice = parseFloat(ItemPrice);
                var ItemQuantity = parseInt($scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity);
                var isTBD = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.IsTBD !== undefined
                    ? $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.IsTBD
                    : false; // Default to false if undefined
                var ItemLabour = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemLabour;
                const ItemLabourTotal = parseFloat(ItemLabour) * quantity;

                LineTotal = parseFloat(LineTotal);
                const ItemWeightTotal = quantity * ItemWeight;
                var config = {
                    QuotationInspectionItemId: 0,
                    QuotationId: QuotationId,
                    InspectionId: InspectionId,
                    QuotationId: QuotationId,
                    ItemPartNo: ItemPartNo,
                    ItemDescription: ItemDescription,
                    ItemUnitPrice: ItemUnitPrice,
                    ItemSurcharge: ItemSurcharge,
                    ItemMarkup: ItemMarkup,
                    ItemPrice: ItemPrice,
                    ItemWeight: ItemWeight,
                    ItemQuantity: ItemQuantity,
                    ItemWeightTotal: ItemWeightTotal,
                    LineTotal: LineTotal,
                    isTBD: isTBD,
                    ItemLabour: ItemLabour,
                    ItemLabourTotal: ItemLabourTotal,
                    inline: false
                };

                console.log('------------------------config---------------:btnAddItemComponentPrice', config);

                return $http({
                    url: '/api/pageview/saveQuotationItemsByAdmin',
                    method: "POST",
                    params: config,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {

                    console.log('------------------------Success---------------:', response);
                    console.log('Remove Quotation Items By Admin Success --btnAddItemComponentPrice', response.data);

                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPartNo = '';
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemDescription = '';
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeight = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemUnitPrice = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemPrice = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemWeightTotal = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.ItemQuantity = 0;
                    $scope.getInspectionDetailsForSheet.objQuotation.objQuotationComponent.LineTotal = 0;


                    $scope.getInspectionDetailsForSheet.objQuotation = response.data;
                    /*$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response.data;*/

                    //let subtotal = 0;
                    //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                    //    subtotal += parseFloat(item.LineTotal);
                    //});

                    $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (response.data.Subtotal).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (response.data.GSTValue).toFixed(2);
                    $scope.getInspectionDetailsForSheet.objQuotation.Total = (response.data.Total).toFixed(2);
                    //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                    //let GSTVal = parseFloat(subtotal * gstRate);
                    //$scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
                    //$scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);

                }, function (error) {
                    alert(error);
                });

                //$.ajax({
                //    url: '', // Replace with your actual API URL
                //    type: 'POST',
                //    contentType: 'application/json',
                //    data: JSON.stringify({
                //        QuotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId, // Replace with your actual QuotationId
                //        QuotationComponentList: itemData // Replace with your actual data
                //    }),
                //    success: function (response) {



                //        //console.log('Remove Quotation Items By Admin Success --', response);
                //        //$scope.getInspectionDetailsForSheet.objQuotation = response.data;
                //        ///*$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response.data;*/

                //        //let subtotal = 0;
                //        //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                //        //    subtotal += parseFloat(item.LineTotal);
                //        //});

                //        //$scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);

                //        //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                //        //let GSTVal = parseFloat(subtotal * gstRate);
                //        //$scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
                //        //$scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);

                //        //if (!$scope.getInspectionDetailsForSheet.objQuotation) {
                //        //    $scope.getInspectionDetailsForSheet.objQuotation = {};
                //        //}

                //        //if ($scope.getInspectionDetailsForSheet && $scope.getInspectionDetailsForSheet.objQuotation) {
                //        //    $scope.getInspectionDetailsForSheet.objQuotation = response.data;
                //        //} else {
                //        //    console.error("objQuotation is not defined");
                //        //}

                //        //let subtotal = 0;
                //        //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                //        //    subtotal += parseFloat(item.LineTotal);
                //        //});

                //        //$scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);

                //        //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                //        //let GSTVal = parseFloat(subtotal * gstRate);
                //        //$scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
                //        //$scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);
                //        //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response;


                //        //$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {

                //        //    subtotal += parseFloat(item.LineTotal);                            
                //        //});

                //        //$scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);
                //        //console.log('xxxxxxx', subtotal);
                //        //const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                //        //let GSTVal = parseFloat(subtotal * gstRate);
                //        ////$scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (objQuotation.GSTVal).toFixed(2);
                //        ////$scope.getInspectionDetailsForSheet.objQuotation.Total = (objQuotation.Total).toFixed(2);

                //    },
                //    error: function (xhr, status, error) {
                //        console.error('Error:', xhr.responseText || 'An error occurred');
                //    }
                //});
            }
        }

        $scope.removeQuotaionItem = function (item) {
            console.log('delete called', item);
            var index = $scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.indexOf(item);
            console.log('Remove Quotataion line 2--', item);
            var config = { id: item, quotationId: $scope.getInspectionDetailsForSheet.objQuotation.QuotationId }
            console.log('Remove Quotataion line --', config);
            return $http({
                url: '/api/pageview/removeQuotationItemsByAdmin',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                console.log('Remove Quotation Items By Admin Success --', response.data);
                $scope.getInspectionDetailsForSheet.objQuotation = response.data;
                /*$scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems = response.data;*/

                let subtotal = 0;
                $scope.getInspectionDetailsForSheet.objQuotation.objQuotationItems.forEach(function (item) {
                    subtotal += parseFloat(item.LineTotal);
                });

                $scope.getInspectionDetailsForSheet.objQuotation.Subtotal = (subtotal).toFixed(2);

                const gstRate = $scope.getInspectionDetailsForSheet.objQuotation.GSTPer / 100;
                let GSTVal = parseFloat(subtotal * gstRate);
                $scope.getInspectionDetailsForSheet.objQuotation.GSTValue = (subtotal * gstRate).toFixed(2);
                $scope.getInspectionDetailsForSheet.objQuotation.Total = (subtotal + GSTVal).toFixed(2);

            }, function (error) {
                alert(error);
            });
        }
    }
})();