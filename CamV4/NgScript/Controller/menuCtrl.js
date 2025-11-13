(function () {
    'use strict';

    angular.module('myApp')
        .controller('menuCtrl', menuCtrl);


    app.controller('menuCtrl', menuCtrl);
    menuCtrl.$inject = ['$scope', '$http', 'myService', 'sharedFilterService'];


    function menuCtrl($scope, $http, myService, sharedFilterService) {
        console.log('----------- menuCtrl loaded -----------');
        // Initialize dropdown models
        $scope.InspectionTypeLayout = [];
        $scope.InspectionStatusLayout = [];
        $scope.regions = [];
        $scope.provinces = [];
        

        $scope.citiesSchedule = [];
        $scope.citiesStatus = [];
        $scope.citiesDocs = [];

        $scope.locationsSchedule = [];
        $scope.locationsStatus = [];
        $scope.locationsDocs = [];

        $scope.filterSchedule = {};
        $scope.filterStatus = {};
        $scope.filterDocs = {
            InspectionDocs: true,
            HistoricalDocs: true
        };

        $scope.filterIncident = {};

        $scope.documenttypelistingInspection = [
            { documenttype: "Inspection Drawings", selected: true },
            { documenttype: "Deficiency Drawings", selected: true },
            { documenttype: "Shelving Checklist", selected: true },
            { documenttype: "Quotation", selected: true },
            { documenttype: "Stamped Report", selected: true },
            { documenttype: "Capacity Table", selected: true },
            { documenttype: "Permit Documents", selected: true },
            { documenttype: "Others", selected: true }
        ];

        $scope.documenttypelistingHistory = [
            { documenttype: "Third Party Report", selected: true },
            { documenttype: "Capacity Plaques", selected: true },
            { documenttype: "Building Drawings(Architecture/Structural/Mechanical)", selected: true },
            { documenttype: "Municipality/OHS Report", selected: true },
            { documenttype: "Quoation/Proposal", selected: true },
            { documenttype: "Slab Letter", selected: true },
            { documenttype: "Fire Letter", selected: true },
            { documenttype: "Permit Schedules", selected: true },
            { documenttype: "Others", selected: true }
        ];

        init();

        function init() {
            loadInspectionTypes();
            loadInspectionStatuses();
            loadRegions();
            loadProvinces();
            loadCityAll();
            loadLocationAll();
        }

        function loadInspectionTypes() {
            myService.getInspectionTypes().then(function (res) {
                $scope.InspectionTypeLayout = res.data;
            });
        }

        function loadInspectionStatuses() {
            myService.getInspectionStatuses().then(function (res) {
                $scope.InspectionStatusLayout = res.data.map(function (status) {
                    status.selected = false;
                    return status;
                });
            });
        }

        function loadRegions() {
            myService.getRegions().then(function (res) {
                $scope.regions = res.data;
            });
        }

        function loadProvinces() {
            myService.getProvinces().then(function (res) {
                $scope.provinces = res.data;
            });
        }

        function loadLocationAll() {
            myService.getLocationsAll().then(function (res) {                
                $scope.locationsStatus = res.data;
                $scope.locationsDocs = res.data;
            });
        }

        function loadCityAll() {
            myService.getCityAll().then(function (res) {                
                $scope.citiesStatus = res.data;
                $scope.citiesDocs = res.data;
            });
        }
        
        $scope.loadCities = function (section) {
            var provinceId = $scope["filter" + section].province;
            myService.getCities(provinceId).then(function (res) {
                $scope["cities" + section] = res.data;
                $scope["filter" + section].city = "";
                $scope["filter" + section].location = "";
                $scope["locations" + section] = [];
            });
        };

        $scope.loadLocations = function (section) {
            console.log('On load call------------------', section);
            var cityId = $scope["filter" + section].city;
            myService.getLocations(cityId).then(function (res) {
                $scope["locations" + section] = res.data;
                $scope["filter" + section].location = "";                
            });
        };

        $scope.loadLocationsRegion = function (section) {
            var Region = $scope["filter" + section].Region;
            console.log('selected Region', Region);
            myService.getLocationsRegion(Region).then(function (res) {
                $scope["locations" + section] = res.data;
                $scope["filter" + section].location = "";
            });
        };

        $scope.applyScheduleFilters = function () {
            sharedFilterService.setScheduleFilter($scope.filterSchedule);
            console.log("Schedule Filter:", $scope.filterSchedule);
            // You can trigger reload via event or state here
        };

        $scope.applyStatusFilters = function () {
            $scope.filterStatus.selectedStatuses = $scope.InspectionStatusLayout
                .filter(s => s.selected)
                .map(s => s.InspectionStatus);
            console.log('$scope.filterStatus.selectedStatuses', $scope.filterStatus.selectedStatuses);
            sharedFilterService.setStatusFilter($scope.filterStatus);
            console.log("Status Filter for inspection:", $scope.filterStatus);
        };

        $scope.getSelectedDocumentTypes = function () {
            return $scope.documenttypelisting
                .filter(function (item) { return item.selected; })
                .map(function (item) { return item.documenttype; });
        };

        $scope.applyDocFilters = function () {

            var selectedInspection = ($scope.documenttypelistingInspection || [])
                .filter(s => s.selected)
                .map(s => s.documenttype); // Assuming you want `documenttype`, not `InspectionStatus`

            var selectedHistorical = ($scope.documenttypelistingHistory || [])
                .filter(s => s.selected)
                .map(s => s.documenttype);

            $scope.filterDocs.DocumentTypeList = selectedInspection.concat(selectedHistorical);

            // Optional: Log for debugging
            console.log('Selected Document Types:', $scope.filterDocs.DocumentTypeList);
            console.log('XXXXXXXXXXXXXX----$scope.filterDocs-----------XXXX:', $scope.filterDocs);
            sharedFilterService.setDocsFilter($scope.filterDocs);
            console.log("Docs Filter:", $scope.filterDocs);
        };

        $scope.applyIncidentFilters = function () {
            // Optional: Log for debugging            
            console.log('XXXXXXXXXXXXXX----$scope.filterIncident-----------XXXX:', $scope.filterIncident);
            sharedFilterService.setIncidentFilter($scope.filterIncident);
            console.log("Docs Filter:", $scope.filterIncident);
        };
        
        $scope.$watch('filterDocs.InspectionDocs', function (newVal) {
            if (angular.isArray($scope.documenttypelistingInspection)) {
                $scope.documenttypelistingInspection.forEach(function (item) {
                    item.selected = newVal;  // true if checked, false if unchecked
                });
            }
        });

        $scope.$watch('filterDocs.HistoricalDocs', function (newVal) {
            if (angular.isArray($scope.documenttypelistingHistory)) {
                $scope.documenttypelistingHistory.forEach(function (item) {
                    item.selected = newVal;  // true if checked, false if unchecked
                });
            }
        });

    }

})();
        //// Default filter models
        //$scope.inspectionDueFilter = {};
        //$scope.insepctionFilter = {};



        //$scope.SearchInspectionDueMenu = function () {
        //    sharedInspectionDueFilterService.set({
        //        InspectionTypeId: $scope.inspectionDueFilter.InspectionTypeCode,
        //        province: $scope.inspectionDueFilter.province,
        //        Region: $scope.inspectionDueFilter.Region,
        //        city: $scope.inspectionDueFilter.city,
        //        location: $scope.inspectionDueFilter.location
        //    });

        //    // Manually trigger reload in customerinspectionCtrl
        //    var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
        //    if (scope && scope.refreshDueInspections) {
        //        scope.refreshDueInspections();
        //    }
        //};

        //$scope.applyInspectionFilters = function () {
        //    sharedinsepctionFilterService.set({
        //        InspectionStatusId: getSelectedInspectionStatusIds(),
        //        InspectionTypeId: $scope.inspectionFilter.InspectionTypeCode,
        //        province: $scope.inspectionFilter.province,
        //        Region: $scope.inspectionFilter.Region,
        //        city: $scope.inspectionFilter.city,
        //        location: $scope.inspectionFilter.location
        //    });

        //    var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
        //    if (scope && scope.refreshAllInspections) {
        //        scope.refreshAllInspections();
        //    }
        //};

        //function getSelectedInspectionStatusIds() {
        //    // TODO: implement based on your UI
        //    return [1, 2, 3, 4, 5, 6, 7, 8, 9]; // sample static
        //}
        //if (window.location.pathname == "/Customer/ManageInspectionDue") {
        //    $http.get('/api/pageview/getAllInspectionType').then(function (response) {
        //        $scope.InspectionTypeLayoutDue = response.data;
        //    });

        //    $http.get('/api/pageview/getProvincebyCountryId', { params: { id: 32 } }).then(function (response) {
        //        $scope.getProvincebyCountryIdLayout = response.data;
        //    });

        //    $http.get('/api/pageview/getProvincebyCountryIdByCustomer').then(function (response) {
        //        $scope.getProvincebyCountryIdByCustomer = response.data;
        //        $scope.getProvincebyCountryIdByCustomerInspection = response.data;
        //    });            

        //    $http.get('/api/pageview/getRegionbyCustomer').then(function (response) {
        //        $scope.getRegionByCustomer = response.data;
        //    });

        //    $scope.getCitybyProvinceIdByCustomerDue = function () {
        //        $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.inspectionDueFilter.province } }).then(function (response) {
        //            $scope.cityListDue = response.data;
        //        });
        //    };
        //    $scope.getLocationbyCityIdByCustomerDue = function () {
        //        $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.inspectionDueFilter.city } }).then(function (response) {
        //            $scope.locationListDue = response.data;
        //        });
        //    };
        //}
        //else if (window.location.pathname == "/Customer/ManageInspection")
        //{
        //    console.log('----------------------------------------------xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx---------------/Customer/ManageInspection--------');
        //    $http.get('/api/pageview/getAllInspectionType').then(function (response) {
        //        $scope.InspectionTypeLayout = response.data;
        //    });

        //    $http.get('/api/pageview/getAllInspectionStatus').then(function (response) {
        //        console.log('asdfasfasdfasdfas', response.data);
        //        $scope.InspectionStatusLayout = response.data;
        //    });

        //    $scope.getCitybyProvinceIdByCustomer = function () {
        //        $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.insepctionFilter.province } }).then(function (response) {
        //            $scope.getCitybyProvinceIdByCustomer = response.data;
        //        });
        //    };
        //    $scope.getLocationbyCityIdByCustomer = function () {
        //        $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.insepctionFilter.city } }).then(function (response) {
        //            $scope.getLocationbyCityIdByCustomer = response.data;
        //        });
        //    };

        //    $scope.GetCitybyProvinceId = function () {
        //        $scope.strProvince = document.getElementById("drpprovinceLayout").value;
        //        $http.get('/api/pageview/getCitybyProvinceId', { params: { id: $scope.strProvince } }).then(function (response) {
        //            $scope.getCitybyProvinceIdLayout = response.data;
        //        });
        //    };
        //}


        //// ---- BUTTON EVENTS ----

        //$scope.applyInspectionDueFilter = function () {
        //    console.log('Set filter for inspection due');
        //    sharedInspectionDueFilterService.set(angular.copy($scope.inspectionDueFilter));
        //};

        //$scope.applyInspectionFilters = function () {
        //    console.log('Set filter for inspection search');
        //    // Extract selected inspection statuses
        //    var selectedStatuses = [];
        //    angular.forEach($scope.InspectionStatusLayout, function (status) {
        //        if (status.selected) {
        //            selectedStatuses.push(status.InspectionStatusId);
        //        }
        //    });

        //    $scope.insepctionFilter.InspectionStatusIds = selectedStatuses;

        //    console.log('Set filter for all inspections');
        //    sharedinsepctionFilterService.set(angular.copy($scope.insepctionFilter));
        //};

