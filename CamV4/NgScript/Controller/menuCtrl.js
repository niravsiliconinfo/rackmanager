(function () {
    'use strict';

    angular.module('myApp')
        .controller('menuCtrl', menuCtrl);

    menuCtrl.$inject = ['$scope', '$http', 'myService', 'sharedFilterService'];

    function menuCtrl($scope, $http, myService, sharedFilterService) {
        console.log('----------- menuCtrl loaded -----------');

        // ---- Lookup / dropdown lists ----
        $scope.InspectionTypeLayout = [];
        //$scope.InspectionStatusLayout = [];
        angular.forEach(
            $scope.InspectionStatusLayout,
            function (item) {
                item.selected = true;
            });
        $scope.regions = [];
        $scope.provinces = [];

        // Cities per section
        $scope.citiesSchedule = [];
        $scope.citiesStatus = [];
        $scope.citiesDocs = [];
        $scope.citiesIncident = [];
        $scope.citiesInternal = [];
        $scope.citiesInventory = [];

        // Locations per section
        $scope.locationsSchedule = [];
        $scope.locationsStatus = [];
        $scope.locationsDocs = [];
        $scope.locationsIncident = [];
        $scope.locationsInternal = [];
        $scope.locationsInventory = [];

        // Facilities per section
        // NOTE: start EMPTY - populated only when a Location is selected
        $scope.facilitiesSchedule = [];
        $scope.facilitiesStatus = [];
        $scope.facilitiesDocs = [];
        $scope.facilitiesIncident = [];
        $scope.facilitiesInternal = [];
        $scope.facilitiesInventory = [];


        // Areas per section
        // NOTE: start EMPTY - populated only when a Location is selected
        $scope.areasSchedule = [];
        $scope.areasStatus = [];
        $scope.areasDocs = [];
        $scope.areasIncident = [];
        $scope.areasInternal = [];
        $scope.areasInventory = [];


        // ---- Filter models ----
        //$scope.filterSchedule = {};
        //$scope.filterStatus = {};
        //$scope.filterDocs = { InspectionDocs: true, HistoricalDocs: true };
        //$scope.filterIncident = {};
        //$scope.filterInternal = {};

        $scope.filterSchedule = {
            InspectionTypeId: '',
            SelectedStatusIds: [],
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };
        // Loading indicator flag
        $scope.loadingMaster = false;

        $scope.filterStatus = {
            InspectionTypeId: '',
            SelectedStatusIds: [],
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };


        //$scope.filterDocs = {
        //    Region: '',
        //    ProvinceId: null,
        //    CityId: null,
        //    CustomerLocationId: null,
        //    CustomerFacilityId: null,
        //    CustomerAreaId: null,

        //    IncludeInspectionDocuments: true,
        //    IncludeHistoricalDocuments: true,

        //    InspectionCategories: [],
        //    HistoricalCategories: []
        //};

        $scope.filterDocs = {
            Region: "",
            Province: 0,
            City: 0,

            CustomerLocationId: 0,
            CustomerFacilityId: 0,
            CustomerAreaId: 0,

            InspectionDocs: false,
            HistoricalDocs: false,

            DocumentTypeList: []
        };

        $scope.filterIncident = {
            IncidentType: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        $scope.filterInternal = {
            Status: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        $scope.filterInventory = {
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        // ---- Sales filter models ----
        $scope.filterSalesStatus = {
            InspectionTypeId: '',
            SelectedStatusIds: [],
            Region: '',
            province: null,
            city: null,
            location: null,
            facility: null,
            area: null,
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };
        $scope.filterSalesDocs = {
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null,
            IncludeInspectionDocuments: true,
            IncludeHistoricalDocuments: true,
            InspectionCategories: [],
            HistoricalCategories: []
        };
        $scope.filterSalesIncident = {
            IncidentType: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };
        $scope.filterSalesInternal = {
            Status: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };
        $scope.filterSalesInventory = {
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        // ---- Document type lists ----
        $scope.documenttypelistingInspection = [
            { documenttype: 'Inspection Drawings', selected: true },
            { documenttype: 'Deficiency Drawings', selected: true },
            { documenttype: 'Shelving Checklist', selected: true },
            { documenttype: 'Quotation', selected: true },
            { documenttype: 'Stamped Report', selected: true },
            { documenttype: 'Capacity Table', selected: true },
            { documenttype: 'Permit Documents', selected: true },
            { documenttype: 'Others', selected: true }
        ];

        $scope.documenttypelistingHistory = [
            { documenttype: 'Third Party Report', selected: true },
            { documenttype: 'Capacity Plaques', selected: true },
            { documenttype: 'Building Drawings(Architecture/Structural/Mechanical)', selected: true },
            { documenttype: 'Municipality/OHS Report', selected: true },
            { documenttype: 'Quoation/Proposal', selected: true },
            { documenttype: 'Slab Letter', selected: true },
            { documenttype: 'Fire Letter', selected: true },
            { documenttype: 'Permit Schedules', selected: true },
            { documenttype: 'Others', selected: true }
        ];

        //$scope.loadMasterDataOnce = function () {

        //    if ($scope.masterLoaded) return;

        //    $scope.loadingMaster = true;

        //    $http.get('/api/pageview/getCustomerMasterData')
        //        .then(function (res) {

        //            var data = res.data;

        //            $scope.regions = data.Regions;
        //            $scope.provinces = data.Provinces;

        //            $scope.citiesStatus = data.Cities;
        //            $scope.locationsStatus = data.Locations;
        //            $scope.facilitiesStatus = data.Facilities;
        //            $scope.areasStatus = data.Areas;

        //            $scope.masterLoaded = true;
        //        })
        //        .finally(function () {
        //            $scope.loadingMaster = false;
        //        });
        //};       
        // ---- Bootstrap ----
        init();

        function init() {
            loadInspectionTypes();
            loadInspectionStatuses();

            // If a filter accordion is open on page load (server-rendered), load the data
            setTimeout(function() {
                var openAccordion = document.querySelector('.accordion-collapse.show');
                if (openAccordion) {
                    $scope.$apply(function() {
                        $scope.loadMasterDataOnce();
                    });
                }
            }, 100);          
        }

        //function loadCitiesAll() {
        //    myService.getCitiesAll()
        //        .then(function (res) {
        //            $scope.citiesStatus = angular.copy(res.data);
        //            $scope.citiesDocs = angular.copy(res.data);
        //            $scope.citiesIncident = angular.copy(res.data);
        //            $scope.citiesInternal = angular.copy(res.data);
        //            $scope.citiesInventory = angular.copy(res.data);
        //        });
        //}

        //function loadLocationsAll() {
        //    myService.getLocationsAll()
        //        .then(function (res) {
        //            $scope.locationsStatus = angular.copy(res.data);
        //            $scope.locationsDocs = angular.copy(res.data);
        //            $scope.locationsIncident = angular.copy(res.data);
        //            $scope.locationsInternal = angular.copy(res.data);
        //            $scope.locationsInventory = angular.copy(res.data);
        //        });
        //}
        //function loadFacilitiesAll() {
        //    myService.getFacilitiesAll()
        //        .then(function (res) {
        //            $scope.facilitiesStatus = angular.copy(res.data);
        //            $scope.facilitiesDocs = angular.copy(res.data);
        //            $scope.facilitiesIncident = angular.copy(res.data);
        //            $scope.facilitiesInternal = angular.copy(res.data);
        //            $scope.facilitiesInventory = angular.copy(res.data);
        //            $scope.filterStatus.facility = "";
        //            $scope.filterDocs.facility = "";
        //            $scope.filterIncident.facility = "";
        //            $scope.filterInternal.facility = "";
        //            $scope.facilitiesInventory.facility = "";
        //        });
        //}
        //function loadAreasAll() {
        //    myService.getAreasAll()
        //        .then(function (res) {
        //            $scope.areasStatus = angular.copy(res.data);
        //            $scope.areasDocs = angular.copy(res.data);
        //            $scope.areasIncident = angular.copy(res.data);
        //            $scope.areasInternal = angular.copy(res.data);
        //            $scope.areasInventory = angular.copy(res.data);
        //        });
        //}
        // ---- Private data-load helpers ----
        function loadInspectionTypes() {
            myService.getInspectionTypes().then(function (res) {
                $scope.InspectionTypeLayout = res.data;
            });
        }

        //function loadInspectionStatuses() {
        //    console.log('loadInspectionStatuses');
        //    myService.getInspectionStatuses().then(function (res) {
        //        $scope.InspectionStatusLayout = res.data.map(function (s) {
        //            s.selected = false;
        //            return s;
        //        });
        //    });
        //}

        function loadInspectionStatuses() {
            myService.getInspectionStatuses()
                .then(function (res) {
                    $scope.InspectionStatusLayout = res.data;
                    angular.forEach(
                        $scope.InspectionStatusLayout,
                        function (s) {
                            s.selected = true;
                        });
                });
        }

        //function loadRegions() {
        //    console.log('loadRegions');
        //    myService.getRegions().then(function (res) {
        //        console.log('Regions Response:', res.data);
        //        $scope.regions = res.data;
        //        console.log('Load Regions');
        //    });
        //}

        //function loadProvinces() {
        //    console.log('loadProvinces');
        //    myService.getProvinces().then(function (res) {
        //        console.log('Provinces Response:', res.data);
        //        $scope.provinces = res.data;
        //    });
        //}

        //function loadLocationAll() {
        //    //myService.getLocationsAll().then(function (res) {
        //    //    $scope.locationsStatus = res.data;
        //    //    $scope.locationsDocs = res.data;
        //    //    $scope.locationsIncident = res.data;
        //    //    $scope.locationsInternal = res.data;                
        //    //});
        //}

        //function loadFacilitiesAll() {
        //    myService.getFacilitiesAll().then(function (res) {
        //        $scope.facilitiesStatus = res.data;
        //        $scope.facilitiesDocs = res.data;
        //        $scope.facilitiesIncident = res.data;
        //        $scope.facilitiesInternal = res.data;
        //        $scope.facilitiesInventory = res.data;
        //        $scope.filterStatus.facility = "";
        //        $scope.filterDocs.facility = "";
        //        $scope.filterIncident.facility = "";
        //        $scope.filterInternal.facility = "";
        //        //$scope.facilitiesInventory.facility = "";
        //    });
        //}

        //function loadCityAll() {
        //    //myService.getCityAll().then(function (res) {
        //    //    $scope.citiesStatus = res.data;
        //    //    $scope.citiesDocs = res.data;
        //    //    $scope.citiesIncident = res.data;
        //    //    $scope.citiesInternal = res.data;  
        //    //});
        //}

        // ---- Private reset helper ----
        // Clears all dropdowns and filter values below the given level for a section.
        // level: 'city' | 'location' | 'facility' | 'area'
        function resetBelow(section, level) {
            var levels = ['city', 'location', 'facility', 'area'];
            var start = levels.indexOf(level);
            if (start === -1) return;

            for (var i = start; i < levels.length; i++) {
                var key = levels[i];
                $scope['filter' + section][key] = '';

                if (key !== 'city') {
                    var listKey = key === 'location' ? 'locations' : (key + 's');
                    $scope[listKey + section] = [];
                }
            }

            // Also clear the cities list when resetting from province level
            if (level === 'city') {
                $scope['cities' + section] = [];
            }
        }

        // ---- Cascade: Province -> Cities ----
        $scope.loadCities = function (section) {
            var provinceId = $scope['filter' + section].province;
            resetBelow(section, 'city');
            if (!provinceId) return;

            myService.getCities(provinceId).then(function (res) {
                $scope['cities' + section] = res.data;
            });
        };

        // ---- Cascade: City -> Locations ----
        $scope.loadLocations = function (section) {
            var cityId = $scope['filter' + section].city;
            resetBelow(section, 'location');
            if (!cityId) return;

            myService.getLocations(cityId).then(function (res) {
                $scope['locations' + section] = res.data;
            });
        };

        // ---- Cascade: Region -> Locations ----
        $scope.loadLocationsRegion = function (section) {
            var region = $scope['filter' + section].Region;
            resetBelow(section, 'location');
            if (!region) return;

            myService.getLocationsRegion(region).then(function (res) {
                $scope['locations' + section] = res.data;
            });
        };

        // ---- Cascade: Location -> Facilities + Areas ----
        // When a location is selected:
        //   1. Load facilities for that location
        //   2. Load ALL areas for that location so Area is usable
        //      even without picking a facility first
        $scope.loadFacilitiesAndAreas = function (section) {
            var locationId = $scope['filter' + section].location;

            // Reset everything below location
            $scope['filter' + section].facility = '';
            $scope['filter' + section].area = '';
            $scope['facilities' + section] = [];
            $scope['areas' + section] = [];

            if (!locationId) return;

            // Load facilities for this location
            myService.getFacilitiesByLocationId(locationId).then(function (res) {
                $scope['facilities' + section] = res.data;
            });

            // Load all areas for this location
            // (pre-populate before facility is chosen)
            myService.getAreasByLocationId(locationId).then(function (res) {
                $scope['areas' + section] = res.data;
            });
        };

        // ---- Cascade: Facility -> Areas (re-filter) ----
        // When a facility is selected: reload areas scoped to that facility.
        // When facility is cleared: fall back to all areas for current location.
        $scope.loadAreasByFacility = function (section) {
            var facilityId = $scope['filter' + section].facility;
            $scope['filter' + section].area = '';
            $scope['areas' + section] = [];

            if (facilityId) {
                // Facility chosen - filter areas by facility
                myService.getAreasByFacilityId(facilityId).then(function (res) {
                    $scope['areas' + section] = res.data;
                });
            } else {
                // Facility cleared - fall back to all areas for current location
                var locationId = $scope['filter' + section].location;
                if (locationId) {
                    myService.getAreasByLocationId(locationId).then(function (res) {
                        $scope['areas' + section] = res.data;
                    });
                }
            }
        };

        // ---- Apply filters ----
        $scope.applyScheduleFilters = function () {
            sharedFilterService.setScheduleFilter($scope.filterSchedule);
            console.log('Schedule Filter:', $scope.filterSchedule);
        };

        $scope.applyStatusFilters = function () {

            $scope.filterStatus.ProvinceId = parseInt($scope.filterStatus.province || 0);
            $scope.filterStatus.CityId = parseInt($scope.filterStatus.city || 0);
            $scope.filterStatus.CustomerLocationId = parseInt($scope.filterStatus.location || 0);
            $scope.filterStatus.CustomerFacilityId = parseInt($scope.filterStatus.facility || 0);
            $scope.filterStatus.CustomerAreaId = parseInt($scope.filterStatus.area || 0);

            $scope.filterStatus.SelectedStatusIds = [];

            angular.forEach($scope.InspectionStatusLayout, function (s) {
                if (s.selected)
                    $scope.filterStatus.SelectedStatusIds.push(s.InspectionStatusId);
            });

            sharedFilterService.setInspectionFilters(angular.copy($scope.filterStatus));
        };

        $scope.toggleInspectionDocs = function () {
            if (!$scope.filterDocs.InspectionDocs) {
                angular.forEach($scope.documenttypelistingInspection, function (d) {
                    d.selected = false;
                });
            } else {
                angular.forEach($scope.documenttypelistingInspection, function (d) {
                    d.selected = true;
                });
            }
        };

        $scope.toggleHistoricalDocs = function () {
            if (!$scope.filterDocs.HistoricalDocs) {
                angular.forEach($scope.documenttypelistingHistory, function (d) {
                    d.selected = false;
                });
            } else {
                angular.forEach($scope.documenttypelistingHistory, function (d) {
                    d.selected = true;
                });
            }
        };

        $scope.applyDocumentFilters = function () {

            $scope.filterDocs.Province = parseInt($scope.filterDocs.province || 0);
            $scope.filterDocs.City = parseInt($scope.filterDocs.city || 0);
            $scope.filterDocs.CustomerLocationId = parseInt($scope.filterDocs.location || 0);
            $scope.filterDocs.CustomerFacilityId = parseInt($scope.filterDocs.facility || 0);
            $scope.filterDocs.CustomerAreaId = parseInt($scope.filterDocs.area || 0);

            $scope.filterDocs.DocumentTypeList = [];

            if ($scope.filterDocs.InspectionDocs) {
                angular.forEach($scope.documenttypelistingInspection, function (d) {
                    if (d.selected)
                        $scope.filterDocs.DocumentTypeList.push(d.documenttype);
                });
            }

            if ($scope.filterDocs.HistoricalDocs) {
                angular.forEach($scope.documenttypelistingHistory, function (d) {
                    if (d.selected)
                        $scope.filterDocs.DocumentTypeList.push(d.documenttype);
                });
            }

            sharedFilterService.setDocumentFilters(angular.copy($scope.filterDocs));
        };

        $scope.applyIncidentFilters = function () {

            $scope.filterIncident.Province = parseInt($scope.filterIncident.province || 0);
            $scope.filterIncident.City = parseInt($scope.filterIncident.city || 0);
            $scope.filterIncident.CustomerLocationId = parseInt($scope.filterIncident.location || 0);
            $scope.filterIncident.CustomerFacilityId = parseInt($scope.filterIncident.facility || 0);
            $scope.filterIncident.CustomerAreaId = parseInt($scope.filterIncident.area || 0);

            sharedFilterService.setIncidentFilters(angular.copy($scope.filterIncident));
        };

        $scope.applyInternalFilters = function () {

            $scope.filterInternal.Province = parseInt($scope.filterInternal.province || 0);
            $scope.filterInternal.City = parseInt($scope.filterInternal.city || 0);
            $scope.filterInternal.CustomerLocationId = parseInt($scope.filterInternal.location || 0);
            $scope.filterInternal.CustomerFacilityId = parseInt($scope.filterInternal.facility || 0);
            $scope.filterInternal.CustomerAreaId = parseInt($scope.filterInternal.area || 0);

            sharedFilterService.setInternalInspectionFilters(angular.copy($scope.filterInternal));
        };

        $scope.applyInventoryFilters = function () {

            $scope.filterInventory.Province = parseInt($scope.filterInventory.province || 0);
            $scope.filterInventory.City = parseInt($scope.filterInventory.city || 0);
            $scope.filterInventory.CustomerLocationId = parseInt($scope.filterInventory.location || 0);
            $scope.filterInventory.CustomerFacilityId = parseInt($scope.filterInventory.facility || 0);
            $scope.filterInventory.CustomerAreaId = parseInt($scope.filterInventory.area || 0);

            sharedFilterService.setInventoryFilters(angular.copy($scope.filterInventory));
        };

        $scope.applySalesStatusFilters = function () {

            $scope.filterSalesStatus.SelectedStatusIds =
                $scope.InspectionStatusLayout
                    .filter(function (x) { return x.selected; })
                    .map(function (x) { return x.InspectionStatusId; });

            $scope.filterSalesStatus.ProvinceId =
                parseInt($scope.filterSalesStatus.province || 0);

            $scope.filterSalesStatus.CityId =
                parseInt($scope.filterSalesStatus.city || 0);

            $scope.filterSalesStatus.CustomerLocationId =
                parseInt($scope.filterSalesStatus.location || 0);

            $scope.filterSalesStatus.CustomerFacilityId =
                parseInt($scope.filterSalesStatus.facility || 0);

            $scope.filterSalesStatus.CustomerAreaId =
                parseInt($scope.filterSalesStatus.area || 0);

            sharedFilterService.setSalesInspectionFilters(
                angular.copy($scope.filterSalesStatus)
            );
        };

        //====================================================
        // SALES DOCUMENT FILTER
        //====================================================
        $scope.applySalesDocumentFilters = function () {

            $scope.filterSalesDocs.ProvinceId =
                parseInt($scope.filterSalesDocs.province || 0);

            $scope.filterSalesDocs.CityId =
                parseInt($scope.filterSalesDocs.city || 0);

            $scope.filterSalesDocs.CustomerLocationId =
                parseInt($scope.filterSalesDocs.location || 0);

            $scope.filterSalesDocs.CustomerFacilityId =
                parseInt($scope.filterSalesDocs.facility || 0);

            $scope.filterSalesDocs.CustomerAreaId =
                parseInt($scope.filterSalesDocs.area || 0);

            // Inspection document categories
            $scope.filterSalesDocs.InspectionCategories =
                $scope.documenttypelistingInspection
                    .filter(function (x) { return x.selected; })
                    .map(function (x) { return x.documenttype; });

            // Historical document categories
            $scope.filterSalesDocs.HistoricalCategories =
                $scope.documenttypelistingHistory
                    .filter(function (x) { return x.selected; })
                    .map(function (x) { return x.documenttype; });

            sharedFilterService.setSalesDocumentFilters(
                angular.copy($scope.filterSalesDocs)
            );
        };

        $scope.applySalesIncidentFilters = function () {

            $scope.filterSalesIncident.Province =
                parseInt($scope.filterSalesIncident.province || 0);

            $scope.filterSalesIncident.City =
                parseInt($scope.filterSalesIncident.city || 0);

            $scope.filterSalesIncident.CustomerLocationId =
                parseInt($scope.filterSalesIncident.location || 0);

            $scope.filterSalesIncident.CustomerFacilityId =
                parseInt($scope.filterSalesIncident.facility || 0);

            $scope.filterSalesIncident.CustomerAreaId =
                parseInt($scope.filterSalesIncident.area || 0);

            sharedFilterService.setSalesIncidentFilters(
                angular.copy($scope.filterSalesIncident)
            );
        };

        //==================================================
        // SALES INTERNAL INSPECTION FILTER
        //==================================================

        $scope.applySalesInternalFilters = function () {

            $scope.filterSalesInternal.Province =
                parseInt($scope.filterSalesInternal.province || 0);

            $scope.filterSalesInternal.City =
                parseInt($scope.filterSalesInternal.city || 0);

            $scope.filterSalesInternal.CustomerLocationId =
                parseInt($scope.filterSalesInternal.location || 0);

            $scope.filterSalesInternal.CustomerFacilityId =
                parseInt($scope.filterSalesInternal.facility || 0);

            $scope.filterSalesInternal.CustomerAreaId =
                parseInt($scope.filterSalesInternal.area || 0);

            sharedFilterService.setSalesInternalFilters(
                angular.copy($scope.filterSalesInternal)
            );
        };

        $scope.applySalesInventoryFilters = function () {

            $scope.filterSalesInventory.ProvinceID =
                parseInt($scope.filterSalesInventory.province || 0);

            $scope.filterSalesInventory.CityID =
                parseInt($scope.filterSalesInventory.city || 0);

            $scope.filterSalesInventory.LocationID =
                parseInt($scope.filterSalesInventory.location || 0);

            $scope.filterSalesInventory.FacilityID =
                parseInt($scope.filterSalesInventory.facility || 0);

            $scope.filterSalesInventory.AreaID =
                parseInt($scope.filterSalesInventory.area || 0);

            sharedFilterService.setSalesInventoryFilters(
                angular.copy($scope.filterSalesInventory)
            );

        };

        //$scope.applyDocFilters = function () {
        //    var selectedInspection =
        //        ($scope.documenttypelistingInspection || [])
        //            .filter(function (s) { return s.selected; })
        //            .map(function (s) { return s.documenttype; });

        //    var selectedHistorical =
        //        ($scope.documenttypelistingHistory || [])
        //            .filter(function (s) { return s.selected; })
        //            .map(function (s) { return s.documenttype; });

        //    $scope.filterDocs.DocumentTypeList =
        //        selectedInspection.concat(selectedHistorical);

        //    // Clean mapped object for backend
        //    var fDocs = {
        //        InspectionDocs: $scope.filterDocs.InspectionDocs,
        //        HistoricalDocs: $scope.filterDocs.HistoricalDocs,
        //        Region: $scope.filterDocs.Region || '',
        //        Province: $scope.filterDocs.province ? parseInt($scope.filterDocs.province) : 0,
        //        City: $scope.filterDocs.city ? parseInt($scope.filterDocs.city) : 0,
        //        Location: $scope.filterDocs.location || '',
        //        Facility: $scope.filterDocs.facility || '',
        //        DocumentTypeList: $scope.filterDocs.DocumentTypeList
        //    };
        //    console.log('In applyDocFilters');
        //    sharedFilterService.setDocumentFilters(fDocs);
        //};
 
        $scope.applyDocFilters = function () {

            var selectedTypes = [];

            if ($scope.filterDocs.InspectionDocs) {
                angular.forEach($scope.documenttypelistingInspection, function (d) {
                    if (d.selected)
                        selectedTypes.push(d.documenttype);
                });
            }

            if ($scope.filterDocs.HistoricalDocs) {
                angular.forEach($scope.documenttypelistingHistory, function (d) {
                    if (d.selected)
                        selectedTypes.push(d.documenttype);
                });
            }

            var filters = {
                InspectionDocs: $scope.filterDocs.InspectionDocs,
                HistoricalDocs: $scope.filterDocs.HistoricalDocs,

                Region: $scope.filterDocs.Region,
                Province: parseInt($scope.filterDocs.Province || 0),
                City: parseInt($scope.filterDocs.City || 0),

                CustomerLocationId: parseInt($scope.filterDocs.CustomerLocationId || 0),
                CustomerFacilityId: parseInt($scope.filterDocs.CustomerFacilityId || 0),
                CustomerAreaId: parseInt($scope.filterDocs.CustomerAreaId || 0),

                DocumentTypeList: selectedTypes
            };

            //sharedFilterService.applyDocFilters(filters);
            sharedFilterService.setDocumentFilters(filters);
        };

        $scope.resetDocumentFilters = function () {
            $scope.filterDocs = {
                Region: "",
                Province: "",
                City: "",
                CustomerLocationId: "",
                CustomerFacilityId: "",
                CustomerAreaId: "",
                InspectionDocs: false,
                HistoricalDocs: false,
                DocumentTypeList: []
            };
            $scope.applyDocFilters();
        };
       
        $scope.masterDataLoaded = false;
        $scope.loadMasterDataOnce = function () {
            if ($scope.masterDataLoaded || $scope.loadingMaster) return;

            $scope.loadingMaster = true;
            myService.getDropdownMaster()
                .then(function (res) {
                    var data = res.data;
                    console.log('data for all dropdowns', data);

                    // Regions
                    var regions = (data.Regions || []).map(function (r) { return r.Name; });
                    $scope.regions = regions;

                    // Provinces
                    $scope.provinces = (data.Provinces || []).map(function (p) { return { ProvinceID: p.Id, ProvinceName: p.Name }; });

                    // Cities
                    var cities = (data.Cities || []).map(function (c) { return { CityID: c.Id, CityName: c.Name }; });
                    $scope.citiesSchedule = cities;
                    $scope.citiesStatus = cities;
                    $scope.citiesDocs = cities;
                    $scope.citiesIncident = cities;
                    $scope.citiesInternal = cities;
                    $scope.citiesInventory = cities;

                    // Locations
                    var locations = (data.Locations || []).map(function (l) { return { CustomerLocationID: l.Id, LocationName: l.Name }; });
                    $scope.locationsSchedule = locations;
                    $scope.locationsStatus = locations;
                    $scope.locationsDocs = locations;
                    $scope.locationsIncident = locations;
                    $scope.locationsInternal = locations;
                    $scope.locationsInventory = locations;

                    // Facilities
                    var facilities = (data.Facilities || []).map(function (f) { return { FacilityID: f.Id, FacilityName: f.Name }; });
                    $scope.facilitiesSchedule = facilities;
                    $scope.facilitiesStatus = facilities;
                    $scope.facilitiesDocs = facilities;
                    $scope.facilitiesIncident = facilities;
                    $scope.facilitiesInternal = facilities;
                    $scope.facilitiesInventory = facilities;

                    // Areas
                    var areas = (data.Areas || []).map(function (a) { return { AreaID: a.Id, AreaName: a.Name }; });
                    $scope.areasSchedule = areas;
                    $scope.areasStatus = areas;
                    $scope.areasDocs = areas;
                    $scope.areasIncident = areas;
                    $scope.areasInternal = areas;
                    $scope.areasInventory = areas;

                    $scope.initializeSalesDocumentCheckboxes();

                    $scope.masterDataLoaded = true;

                    console.log('Dropdown arrays assigned – regions:', regions.length,
                        'provinces:', $scope.provinces.length,
                        'cities:', cities.length,
                        'locations:', locations.length,
                        'facilities:', facilities.length,
                        'areas:', areas.length);
                })
                .catch(function (err) {
                    console.error('Failed to load master dropdown data', err);
                })
                .finally(function () {
                    $scope.loadingMaster = false;
                    //$scope.applyInternalFilters = function () {
                    //    console.log('On click applyInternalFilters');
                    //    var fInt = {
                    //        Status: $scope.filterInternal.Status || '',
                    //        Region: $scope.filterInternal.Region || '',
                    //        location: $scope.filterInternal.location || '',
                    //        facility: $scope.filterInternal.facility || '',
                    //        area: $scope.filterInternal.area || ''
                    //    };
                    //    sharedFilterService.setInternalInspectionFilters(fInt);
                    //};

                    //$scope.applyInventoryFilters = function () {
                    //    console.log('On click applyInventoryFilters');

                    //    var fInv = {
                    //        Region: $scope.filterInventory.Region || '',
                    //        ProvinceID: $scope.filterInventory.province ? parseInt($scope.filterInventory.province) : null,
                    //        CityID: $scope.filterInventory.city ? parseInt($scope.filterInventory.city) : null,
                    //        LocationID: $scope.filterInventory.location ? parseInt($scope.filterInventory.location) : null,
                    //        FacilityID: $scope.filterInventory.facility ? parseInt($scope.filterInventory.facility) : null,
                    //        AreaID: $scope.filterInventory.area ? parseInt($scope.filterInventory.area) : null
                    //    };

                    //    sharedFilterService.setInternalInventoryFilters(fInv);
                    //};
                });
        };

        // ---- Watchers: master checkboxes sync child lists ----
        $scope.$watch('filterDocs.InspectionDocs', function (newVal) {
            if (angular.isArray($scope.documenttypelistingInspection)) {
                $scope.documenttypelistingInspection.forEach(function (item) {
                    item.selected = newVal;
                });
            }
        });

        $scope.$watch('filterDocs.HistoricalDocs', function (newVal) {
            if (angular.isArray($scope.documenttypelistingHistory)) {
                $scope.documenttypelistingHistory.forEach(function (item) {
                    item.selected = newVal;
                });
            }
        });


        // SALES - Inspection Documents
        $scope.$watch('filterSalesDocs.InspectionDocs', function (newVal) {

            if (angular.isArray($scope.documenttypelistingInspection)) {

                angular.forEach($scope.documenttypelistingInspection, function (item) {
                    item.selected = newVal;
                });

            }

        });

        // SALES - Historical Documents
        $scope.$watch('filterSalesDocs.HistoricalDocs', function (newVal) {

            if (angular.isArray($scope.documenttypelistingHistory)) {

                angular.forEach($scope.documenttypelistingHistory, function (item) {
                    item.selected = newVal;
                });

            }

        });

        //=========================================
        // SALES DOCUMENT CHECKBOXES
        //=========================================

        // Select/Deselect all Inspection document types
        $scope.toggleSalesInspectionDocs = function () {

            angular.forEach($scope.documenttypelistingInspection, function (item) {
                item.selected = $scope.filterSalesDocs.InspectionDocs;
            });

        };

        // Select/Deselect all Historical document types
        $scope.toggleSalesHistoricalDocs = function () {

            angular.forEach($scope.documenttypelistingHistory, function (item) {
                item.selected = $scope.filterSalesDocs.HistoricalDocs;
            });

        };

        // Initialize default selection
        $scope.initializeSalesDocumentCheckboxes = function () {

            $scope.filterSalesDocs.InspectionDocs = true;
            $scope.filterSalesDocs.HistoricalDocs = true;

            $scope.toggleSalesInspectionDocs();
            $scope.toggleSalesHistoricalDocs();

        };

    }

})();