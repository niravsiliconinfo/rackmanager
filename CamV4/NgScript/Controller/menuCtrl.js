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

        $scope.filterDocs = {
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

        // ---- Bootstrap ----
        init();

        function init() {
            loadInspectionTypes();
            loadInspectionStatuses();

            loadRegions();
            loadProvinces();

            loadCitiesAll();
            loadLocationsAll();

            loadFacilitiesAll();
            loadAreasAll();
            $scope.filterStatus.facility = '';
            $scope.filterStatus.location = '';
            $scope.filterStatus.area = '';

            $scope.filterDocs.facility = '';
            $scope.filterDocs.location = '';
            $scope.filterDocs.area = '';

            $scope.filterIncident.facility = '';
            $scope.filterIncident.location = '';
            $scope.filterIncident.area = '';

            $scope.filterInternal.facility = '';
            $scope.filterInternal.location = '';
            $scope.filterInternal.area = '';

            $scope.filterInventory.facility = '';
            $scope.filterInventory.location = '';
            $scope.filterInventory.area = '';

            //$scope.filterInventory.facility = '';
            //$scope.filterInventory.location = '';
            //$scope.filterInventory.area = '';            
            
            //$scope.filterStatus = {

            //    InspectionTypeId: '',
            //    Region: '',
            //    province: '',
            //    city: '',
            //    location: '',
            //    facility: '',
            //    area: ''

            //};
            //loadInspectionTypes();
            //loadInspectionStatuses();
            //loadRegions();
            //loadProvinces();
            //loadCityAll();
            //loadLocationAll();
            //loadFacilitiesAll();
            // NOTE: Facilities and Areas are NOT pre-loaded globally anymore.
            // They are loaded on demand when a Location is selected via
            // loadFacilitiesAndAreas(section). This prevents showing all
            // facilities/areas before a location context is chosen.
        }

        function loadCitiesAll() {
            myService.getCitiesAll()
                .then(function (res) {                    
                    $scope.citiesStatus = angular.copy(res.data);
                    $scope.citiesDocs = angular.copy(res.data);
                    $scope.citiesIncident = angular.copy(res.data);
                    $scope.citiesInternal = angular.copy(res.data);          
                    $scope.citiesInventory = angular.copy(res.data);          
                });
        }

        function loadLocationsAll() {
            myService.getLocationsAll()
                .then(function (res) {
                    $scope.locationsStatus = angular.copy(res.data);
                    $scope.locationsDocs = angular.copy(res.data);
                    $scope.locationsIncident = angular.copy(res.data);
                    $scope.locationsInternal = angular.copy(res.data);
                    $scope.locationsInventory = angular.copy(res.data);
                });
        }
        function loadFacilitiesAll() {
            myService.getFacilitiesAll()
                .then(function (res) {
                    $scope.facilitiesStatus = angular.copy(res.data);
                    $scope.facilitiesDocs = angular.copy(res.data);
                    $scope.facilitiesIncident = angular.copy(res.data);
                    $scope.facilitiesInternal = angular.copy(res.data);
                    $scope.facilitiesInventory = angular.copy(res.data);
                    $scope.filterStatus.facility = "";
                    $scope.filterDocs.facility = "";
                    $scope.filterIncident.facility = "";                    
                    $scope.filterInternal.facility = "";
                    $scope.facilitiesInventory.facility = "";
                });
        }
        function loadAreasAll() {
            myService.getAreasAll()
                .then(function (res) {                    
                    $scope.areasStatus = angular.copy(res.data);
                    $scope.areasDocs = angular.copy(res.data);
                    $scope.areasIncident = angular.copy(res.data);
                    $scope.areasInternal = angular.copy(res.data);
                    $scope.areasInventory = angular.copy(res.data);
                });
        }
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

        function loadRegions() {
            console.log('loadRegions');
            myService.getRegions().then(function (res) {
                console.log('Regions Response:', res.data);
                $scope.regions = res.data;
                console.log('Load Regions');
            });
        }

        function loadProvinces() {
            console.log('loadProvinces');
            myService.getProvinces().then(function (res) {
                console.log('Provinces Response:', res.data);
                $scope.provinces = res.data;
            });
        }

        function loadLocationAll() {
            //myService.getLocationsAll().then(function (res) {
            //    $scope.locationsStatus = res.data;
            //    $scope.locationsDocs = res.data;
            //    $scope.locationsIncident = res.data;
            //    $scope.locationsInternal = res.data;                
            //});
        }

        function loadFacilitiesAll() {
            myService.getFacilitiesAll().then(function (res) {
                $scope.facilitiesStatus = res.data;
                $scope.facilitiesDocs = res.data;
                $scope.facilitiesIncident = res.data;
                $scope.facilitiesInternal = res.data;                  
                $scope.facilitiesInventory = res.data;
                $scope.filterStatus.facility = "";
                $scope.filterDocs.facility = "";
                $scope.filterIncident.facility = "";
                $scope.filterInternal.facility = "";
                $scope.facilitiesInventory.facility = "";
            });
        }       

        function loadCityAll() {
            //myService.getCityAll().then(function (res) {
            //    $scope.citiesStatus = res.data;
            //    $scope.citiesDocs = res.data;
            //    $scope.citiesIncident = res.data;
            //    $scope.citiesInternal = res.data;  
            //});
        }

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
            console.log('On click applyStatusFilters');

            $scope.filterStatus.SelectedStatusIds =
                $scope.InspectionStatusLayout
                    .filter(function (x) { return x.selected; })
                    .map(function (x) { return x.InspectionStatusId; });

            // CONVERSIONS: Convert raw dropdown values to proper property names
            $scope.filterStatus.ProvinceId =
                $scope.filterStatus.province ? parseInt($scope.filterStatus.province) : null;

            $scope.filterStatus.CityId =
                $scope.filterStatus.city ? parseInt($scope.filterStatus.city) : null;

            $scope.filterStatus.CustomerLocationId =
                $scope.filterStatus.location ? parseInt($scope.filterStatus.location) : null;

            $scope.filterStatus.CustomerFacilityId =
                $scope.filterStatus.facility ? parseInt($scope.filterStatus.facility) : null;

            $scope.filterStatus.CustomerAreaId =
                $scope.filterStatus.area ? parseInt($scope.filterStatus.area) : null;

            sharedFilterService.setInspectionFilters($scope.filterStatus);
        };

        

        $scope.applyDocFilters = function () {
            var selectedInspection =
                ($scope.documenttypelistingInspection || [])
                    .filter(function (s) { return s.selected; })
                    .map(function (s) { return s.documenttype; });

            var selectedHistorical =
                ($scope.documenttypelistingHistory || [])
                    .filter(function (s) { return s.selected; })
                    .map(function (s) { return s.documenttype; });

            $scope.filterDocs.DocumentTypeList =
                selectedInspection.concat(selectedHistorical);

            // CONVERSIONS: Convert raw dropdown values to proper property names
            $scope.filterDocs.ProvinceId =
                $scope.filterDocs.province ? parseInt($scope.filterDocs.province) : null;

            $scope.filterDocs.CityId =
                $scope.filterDocs.city ? parseInt($scope.filterDocs.city) : null;

            $scope.filterDocs.CustomerLocationId =
                $scope.filterDocs.location ? parseInt($scope.filterDocs.location) : null;

            $scope.filterDocs.CustomerFacilityId =
                $scope.filterDocs.facility ? parseInt($scope.filterDocs.facility) : null;

            $scope.filterDocs.CustomerAreaId =
                $scope.filterDocs.area ? parseInt($scope.filterDocs.area) : null;

            sharedFilterService.setDocumentFilters($scope.filterDocs);
        };

        
        $scope.applyIncidentFilters = function () {
            console.log('On click applyIncidentFilters');

            // CONVERSIONS: Convert raw dropdown values to proper property names
            $scope.filterIncident.ProvinceId =
                $scope.filterIncident.province ? parseInt($scope.filterIncident.province) : null;

            $scope.filterIncident.CityId =
                $scope.filterIncident.city ? parseInt($scope.filterIncident.city) : null;

            $scope.filterIncident.CustomerLocationId =
                $scope.filterIncident.location ? parseInt($scope.filterIncident.location) : null;

            $scope.filterIncident.CustomerFacilityId =
                $scope.filterIncident.facility ? parseInt($scope.filterIncident.facility) : null;

            $scope.filterIncident.CustomerAreaId =
                $scope.filterIncident.area ? parseInt($scope.filterIncident.area) : null;

            sharedFilterService.setIncidentFilters($scope.filterIncident);
        };
      
        $scope.applyInternalFilters = function () {
            console.log('On click applyInternalFilters');

            // CONVERSIONS: Convert raw dropdown values to proper property names
            $scope.filterInternal.ProvinceId =
                $scope.filterInternal.province ? parseInt($scope.filterInternal.province) : null;

            $scope.filterInternal.CityId =
                $scope.filterInternal.city ? parseInt($scope.filterInternal.city) : null;

            $scope.filterInternal.CustomerLocationId =
                $scope.filterInternal.location ? parseInt($scope.filterInternal.location) : null;

            $scope.filterInternal.CustomerFacilityId =
                $scope.filterInternal.facility ? parseInt($scope.filterInternal.facility) : null;

            $scope.filterInternal.CustomerAreaId =
                $scope.filterInternal.area ? parseInt($scope.filterInternal.area) : null;

            sharedFilterService.setInternalInspectionFilters($scope.filterInternal);
        };

        $scope.applyInventoryFilters = function () {
            console.log('On click applyInventoryFilters');

            // CONVERSIONS: Convert raw dropdown values to proper property names
            $scope.filterInventory.ProvinceId =
                $scope.filterInventory.province ? parseInt($scope.filterInventory.province) : null;

            $scope.filterInventory.CityId =
                $scope.filterInventory.city ? parseInt($scope.filterInventory.city) : null;

            $scope.filterInventory.CustomerLocationId =
                $scope.filterInventory.location ? parseInt($scope.filterInventory.location) : null;

            $scope.filterInventory.CustomerFacilityId =
                $scope.filterInventory.facility ? parseInt($scope.filterInventory.facility) : null;

            $scope.filterInventory.CustomerAreaId =
                $scope.filterInventory.area ? parseInt($scope.filterInventory.area) : null;

            sharedFilterService.setInternalInventoryFilters($scope.filterInventory);
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


    }

})();

//(function () {
//    'use strict';

//    angular.module('myApp')
//        .controller('menuCtrl', menuCtrl);

//    menuCtrl.$inject = ['$scope', '$http', 'myService', 'sharedFilterService'];

//    function menuCtrl($scope, $http, myService, sharedFilterService) {
//        console.log('----------- menuCtrl loaded -----------');

//        //  Lookup / dropdown lists 
//        $scope.InspectionTypeLayout = [];
//        $scope.InspectionStatusLayout = [];
//        $scope.regions = [];
//        $scope.provinces = [];

//        // Cities per section
//        $scope.citiesSchedule = [];
//        $scope.citiesStatus = [];
//        $scope.citiesDocs = [];
//        $scope.citiesIncident = [];

//        // Locations per section
//        $scope.locationsSchedule = [];
//        $scope.locationsStatus = [];
//        $scope.locationsDocs = [];
//        $scope.locationsIncident = [];

//        // Facilities per section  (populated when Location is chosen)
//        $scope.facilitiesSchedule = [];
//        $scope.facilitiesStatus = [];
//        $scope.facilitiesDocs = [];
//        $scope.facilitiesIncident = [];

//        // Areas per section  (populated when Location is chosen, re-filtered when Facility is chosen)
//        $scope.areasSchedule = [];
//        $scope.areasStatus = [];
//        $scope.areasDocs = [];
//        $scope.areasIncident = [];

//        //  Filter models 
//        $scope.filterSchedule = {};
//        $scope.filterStatus = {};
//        $scope.filterDocs = { InspectionDocs: true, HistoricalDocs: true };
//        $scope.filterIncident = {};

//        //  Document type lists 
//        $scope.documenttypelistingInspection = [
//            { documenttype: 'Inspection Drawings', selected: true },
//            { documenttype: 'Deficiency Drawings', selected: true },
//            { documenttype: 'Shelving Checklist', selected: true },
//            { documenttype: 'Quotation', selected: true },
//            { documenttype: 'Stamped Report', selected: true },
//            { documenttype: 'Capacity Table', selected: true },
//            { documenttype: 'Permit Documents', selected: true },
//            { documenttype: 'Others', selected: true }
//        ];

//        $scope.documenttypelistingHistory = [
//            { documenttype: 'Third Party Report', selected: true },
//            { documenttype: 'Capacity Plaques', selected: true },
//            { documenttype: 'Building Drawings(Architecture/Structural/Mechanical)', selected: true },
//            { documenttype: 'Municipality/OHS Report', selected: true },
//            { documenttype: 'Quoation/Proposal', selected: true },
//            { documenttype: 'Slab Letter', selected: true },
//            { documenttype: 'Fire Letter', selected: true },
//            { documenttype: 'Permit Schedules', selected: true },
//            { documenttype: 'Others', selected: true }
//        ];

//        //  Bootstrap 
//        init();

//        function init() {
//            loadInspectionTypes();
//            loadInspectionStatuses();
//            loadRegions();
//            loadProvinces();
//            loadCityAll();
//            loadLocationAll();
//            loadfacilityAll();
//            loadareaAll();
//        }

//        //  Private data-load helpers 
//        function loadInspectionTypes() {
//            myService.getInspectionTypes().then(function (res) {
//                $scope.InspectionTypeLayout = res.data;
//            });
//        }

//        function loadInspectionStatuses() {
//            myService.getInspectionStatuses().then(function (res) {
//                $scope.InspectionStatusLayout = res.data.map(function (s) {
//                    s.selected = false;
//                    return s;
//                });
//            });
//        }

//        function loadRegions() {
//            myService.getRegions().then(function (res) {
//                $scope.regions = res.data;
//            });
//        }

//        function loadProvinces() {
//            myService.getProvinces().then(function (res) {
//                $scope.provinces = res.data;
//            });
//        }

//        function loadLocationAll() {
//            myService.getLocationsAll().then(function (res) {
//                $scope.locationsStatus = res.data;
//                $scope.locationsDocs = res.data;
//                $scope.locationsIncident = res.data;
//            });
//        }

//        function loadCityAll() {
//            myService.getCityAll().then(function (res) {
//                $scope.citiesStatus = res.data;
//                $scope.citiesDocs = res.data;
//                $scope.citiesIncident = res.data;
//            });
//        }
//        function loadfacilityAll() {
//            myService.getFacilityAll().then(function (res) {
//                $scope.facilitiesSchedule = res.data;
//                $scope.facilitiesStatus = res.data;
//                $scope.facilitiesDocs = res.data;
//                $scope.facilitiesIncident = res.data;
//            });
//        }

//        function loadareaAll() {
//            myService.getAreaAll().then(function (res) {
//                $scope.areasSchedule = res.data;
//                $scope.areasStatus = res.data;
//                $scope.areasDocs = res.data;
//                $scope.areasIncident = res.data;
//            });
//        }

//        // Private reset helper 
//        // Clears all dropdowns and filter values below the given level for a section.
//        // level: 'province' | 'city' | 'location' | 'facility'
//        function resetBelow(section, level) {
//            var levels = ['city', 'location', 'facility', 'area'];
//            var start = levels.indexOf(level);
//            if (start === -1) return;

//            for (var i = start; i < levels.length; i++) {
//                var key = levels[i];
//                $scope['filter' + section][key] = '';

//                // Clear the corresponding list array (cities don't have a list key here)
//                if (key !== 'city') {
//                    var listKey = key === 'location' ? 'locations' : (key + 's');
//                    $scope[listKey + section] = [];
//                }
//            }

//            // Also clear the cities list when resetting from province level
//            if (level === 'city') {
//                $scope['cities' + section] = [];
//            }
//        }

//        //  Cascade: Province -> Cities 
//        $scope.loadCities = function (section) {
//            var provinceId = $scope['filter' + section].province;
//            resetBelow(section, 'city');
//            if (!provinceId) return;

//            myService.getCities(provinceId).then(function (res) {
//                $scope['cities' + section] = res.data;
//            });
//        };

//        //  Cascade: City -> Locations 
//        $scope.loadLocations = function (section) {
//            var cityId = $scope['filter' + section].city;
//            resetBelow(section, 'location');
//            if (!cityId) return;

//            myService.getLocations(cityId).then(function (res) {
//                $scope['locations' + section] = res.data;
//            });
//        };

//        //  Cascade: Region -> Locations 
//        $scope.loadLocationsRegion = function (section) {
//            var region = $scope['filter' + section].Region;
//            resetBelow(section, 'location');
//            if (!region) return;

//            myService.getLocationsRegion(region).then(function (res) {
//                $scope['locations' + section] = res.data;
//            });
//        };

//        //  Cascade: Location -> Facilities + Areas 
//        // When a location is selected we:
//        //   1. Load facilities for that location.
//        //   2. Load ALL areas for that location (so Area is usable even without
//        //      picking a facility first).
//        $scope.loadFacilitiesAndAreas = function (section) {
//            var locationId = $scope['filter' + section].location;

//            // Reset everything below location
//            $scope['filter' + section].facility = '';
//            $scope['filter' + section].area = '';
//            $scope['facilities' + section] = [];
//            $scope['areas' + section] = [];

//            if (!locationId) return;

//            // Load facilities
//            myService.getFacilitiesByLocationId(locationId).then(function (res) {
//                $scope['facilities' + section] = res.data;
//            });

//            // Load all areas for this location (pre-populate before facility is chosen)
//            myService.getAreasByLocationId(locationId).then(function (res) {
//                $scope['areas' + section] = res.data;
//            });
//        };

//        //  Cascade: Facility -> Areas (re-filter) 
//        // When a facility is selected we reload areas scoped to that facility.
//        // If facility is cleared we fall back to all areas for the current location.
//        $scope.loadAreasByFacility = function (section) {
//            var facilityId = $scope['filter' + section].facility;
//            $scope['filter' + section].area = '';
//            $scope['areas' + section] = [];

//            if (facilityId) {
//                // Facility chosen -> filter areas by facility
//                myService.getAreasByFacilityId(facilityId).then(function (res) {
//                    $scope['areas' + section] = res.data;
//                });
//            } else {
//                // Facility cleared -> fall back to all areas for the current location
//                var locationId = $scope['filter' + section].location;
//                if (locationId) {
//                    myService.getAreasByLocationId(locationId).then(function (res) {
//                        $scope['areas' + section] = res.data;
//                    });
//                }
//            }
//        };

//        //  Apply filters 
//        $scope.applyScheduleFilters = function () {
//            sharedFilterService.setScheduleFilter($scope.filterSchedule);
//            console.log('Schedule Filter:', $scope.filterSchedule);
//        };

//        $scope.applyStatusFilters = function () {
//            $scope.filterStatus.selectedStatuses = $scope.InspectionStatusLayout
//                .filter(function (s) { return s.selected; })
//                .map(function (s) { return s.InspectionStatus; });
//            sharedFilterService.setStatusFilter($scope.filterStatus);
//            console.log('Status Filter:', $scope.filterStatus);
//        };

//        $scope.applyDocFilters = function () {
//            var selectedInspection = ($scope.documenttypelistingInspection || [])
//                .filter(function (s) { return s.selected; })
//                .map(function (s) { return s.documenttype; });

//            var selectedHistorical = ($scope.documenttypelistingHistory || [])
//                .filter(function (s) { return s.selected; })
//                .map(function (s) { return s.documenttype; });

//            $scope.filterDocs.DocumentTypeList = selectedInspection.concat(selectedHistorical);
//            sharedFilterService.setDocsFilter($scope.filterDocs);
//            console.log('Docs Filter:', $scope.filterDocs);
//        };

//        $scope.applyIncidentFilters = function () {
//            sharedFilterService.setIncidentFilter($scope.filterIncident);
//            console.log('Incident Filter:', $scope.filterIncident);
//        };

//        //  Watchers: master checkboxes sync child lists 
//        $scope.$watch('filterDocs.InspectionDocs', function (newVal) {
//            if (angular.isArray($scope.documenttypelistingInspection)) {
//                $scope.documenttypelistingInspection.forEach(function (item) {
//                    item.selected = newVal;
//                });
//            }
//        });

//        $scope.$watch('filterDocs.HistoricalDocs', function (newVal) {
//            if (angular.isArray($scope.documenttypelistingHistory)) {
//                $scope.documenttypelistingHistory.forEach(function (item) {
//                    item.selected = newVal;
//                });
//            }
//        });
//    }

//})();

////// Default filter models
////$scope.inspectionDueFilter = {};
////$scope.insepctionFilter = {};



////$scope.SearchInspectionDueMenu = function () {
////    sharedInspectionDueFilterService.set({
////       InspectionTypeId: $scope.inspectionDueFilter.InspectionTypeCode,
////       province: $scope.inspectionDueFilter.province,
////       Region: $scope.inspectionDueFilter.Region,
////       city: $scope.inspectionDueFilter.city,
////       location: $scope.inspectionDueFilter.location
////    });

////    // Manually trigger reload in customerinspectionCtrl
////    var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
////    if (scope && scope.refreshDueInspections) {
////       scope.refreshDueInspections();
////    }
////};

////$scope.applyInspectionFilters = function () {
////    sharedinsepctionFilterService.set({
////       InspectionStatusId: getSelectedInspectionStatusIds(),
////       InspectionTypeId: $scope.inspectionFilter.InspectionTypeCode,
////       province: $scope.inspectionFilter.province,
////       Region: $scope.inspectionFilter.Region,
////       city: $scope.inspectionFilter.city,
////       location: $scope.inspectionFilter.location
////    });

////    var scope = angular.element(document.querySelector('[ng-controller=customerinspectionCtrl]')).scope();
////    if (scope && scope.refreshAllInspections) {
////       scope.refreshAllInspections();
////    }
////};

////function getSelectedInspectionStatusIds() {
////    // TODO: implement based on your UI
////    return [1, 2, 3, 4, 5, 6, 7, 8, 9]; // sample static
////}
////if (window.location.pathname == "/Customer/ManageInspectionDue") {
////    $http.get('/api/pageview/getAllInspectionType').then(function (response) {
////       $scope.InspectionTypeLayoutDue = response.data;
////    });

////    $http.get('/api/pageview/getProvincebyCountryId', { params: { id: 32 } }).then(function (response) {
////       $scope.getProvincebyCountryIdLayout = response.data;
////    });

////    $http.get('/api/pageview/getProvincebyCountryIdByCustomer').then(function (response) {
////       $scope.getProvincebyCountryIdByCustomer = response.data;
////       $scope.getProvincebyCountryIdByCustomerInspection = response.data;
////    });

////    $http.get('/api/pageview/getRegionbyCustomer').then(function (response) {
////       $scope.getRegionByCustomer = response.data;
////    });

////    $scope.getCitybyProvinceIdByCustomerDue = function () {
////       $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.inspectionDueFilter.province } }).then(function (response) {
////           $scope.cityListDue = response.data;
////       });
////    };
////    $scope.getLocationbyCityIdByCustomerDue = function () {
////       $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.inspectionDueFilter.city } }).then(function (response) {
////           $scope.locationListDue = response.data;
////       });
////    };
////}
////else if (window.location.pathname == "/Customer/ManageInspection")
////{
////    console.log('----------------------------------------------xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx---------------/Customer/ManageInspection--------');
////    $http.get('/api/pageview/getAllInspectionType').then(function (response) {
////       $scope.InspectionTypeLayout = response.data;
////    });

////    $http.get('/api/pageview/getAllInspectionStatus').then(function (response) {
////       console.log('asdfasfasdfasdfas', response.data);
////       $scope.InspectionStatusLayout = response.data;
////    });

////    $scope.getCitybyProvinceIdByCustomer = function () {
////       $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.insepctionFilter.province } }).then(function (response) {
////           $scope.getCitybyProvinceIdByCustomer = response.data;
////       });
////    };
////    $scope.getLocationbyCityIdByCustomer = function () {
////       $http.get('/api/pageview/getLocationbyCityIdByCustomer', { params: { id: $scope.insepctionFilter.city } }).then(function (response) {
////           $scope.getLocationbyCityIdByCustomer = response.data;
////       });
////    };

////    $scope.GetCitybyProvinceId = function () {
////       $scope.strProvince = document.getElementById("drpprovinceLayout").value;
////       $http.get('/api/pageview/getCitybyProvinceId', { params: { id: $scope.strProvince } }).then(function (response) {
////           $scope.getCitybyProvinceIdLayout = response.data;
////       });
////    };
////}


////// ---- BUTTON EVENTS ----

////$scope.applyInspectionDueFilter = function () {
////    console.log('Set filter for inspection due');
////    sharedInspectionDueFilterService.set(angular.copy($scope.inspectionDueFilter));
////};

////$scope.applyInspectionFilters = function () {
////    console.log('Set filter for inspection search');
////    // Extract selected inspection statuses
////    var selectedStatuses = [];
////    angular.forEach($scope.InspectionStatusLayout, function (status) {
////       if (status.selected) {
////           selectedStatuses.push(status.InspectionStatusId);
////       }
////    });

////    $scope.insepctionFilter.InspectionStatusIds = selectedStatuses;

////    console.log('Set filter for all inspections');
////    sharedinsepctionFilterService.set(angular.copy($scope.insepctionFilter));
////};

