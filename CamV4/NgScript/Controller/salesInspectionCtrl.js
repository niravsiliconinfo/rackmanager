(function () {
    'use strict';

    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp')
        .controller('salesInspectionCtrl', salesInspectionCtrl);

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

    app.controller('salesInspectionCtrl', salesInspectionCtrl);

    salesInspectionCtrl.$inject = ['$scope', '$http', 'sharedFilterService', '$rootScope'];

    function salesInspectionCtrl($scope, $http, sharedFilterService, $rootScope) {
        console.log('-----------salesInspectionCtrl--------------');
        //$scope.getAllInspectionDueByCustomerId = [];
        $scope.getAllInspectionBySales = [];

        //$scope.scheduleFilter = sharedFilterService.getScheduleFilter() || {};
        $scope.salesStatusFilter = sharedFilterService.getSalesInspectionFilters() || {};

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
        //       .filter(function (s) { return s.selected; })
        //       .map(function (s) { return s.InspectionStatusId; });
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
            var salesFilter = sharedFilterService.getSalesInspectionFilters();
            if (window.location.pathname == "/SalesManager/ManageInspectionSales") {
                if (salesFilter && Object.keys(salesFilter).length > 0) {
                    $scope.salesStatusFilter = salesFilter;
                    loadSalesInspections(salesFilter);
                } else {
                    loadSalesInspections({});
                }
            }
        }

        $scope.$on('salesInspectionFiltersUpdated', function (event, filters) {

            console.log("Sales filters received", filters);

            loadSalesInspections(filters);

        });  
        $rootScope.$on('salesInspectionFiltersUpdated', function () {
            var filters = sharedFilterService.getSalesInspectionFilters();
            console.log("Reload Sales Inspection", filters);
            loadSalesInspections(filters);
        });
        function loadSalesInspections(filter) {
            const filters = filter;
            console.log('Calling ----> loadSalesInspections', filters);
            $http.post('/api/pageview/getSalesInspectionListing', filters)            
                .then(function (response) {
                    console.log("SUCCESS");
                    console.log(response);
                    $scope.getAllInspectionBySales = response.data;
                })
                .catch(function (error) {
                    console.log("ERROR");
                    console.log(error);
                });
        }        

        $scope.refreshAllInspections = function () {
            loadSalesInspections();
        };      

   

        $scope.InspectionDetailClickBySales = function (id) {
            $http.get('/api/pageview/getInspectionById', { params: { InspectionId: id } }).then(function (response) {
                $scope.getInspectionById = response.data;
                var url = '/Admin/InspectionSheet?id=' + id; //InspectionId
                window.location = url;
            }, function (response) {
                $scope.waiting = false;
            });
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

      

        if (window.location.pathname == "/Admin/InspectionSheet" || window.location.pathname == "/Admin/InspectionSheet" || window.location.pathname == "/Employee/InspectionDetail" || window.location.pathname == "/Customer/GenerateQuotation") {

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

                if (window.location.pathname == "/Admin/InspectionSheet") {

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

           
    }
})();