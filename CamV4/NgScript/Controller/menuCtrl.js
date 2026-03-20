(function () {
    'use strict';

    angular.module('myApp')
        .controller('menuCtrl', menuCtrl);

    menuCtrl.$inject = ['$scope', '$http', 'myService', 'sharedFilterService'];

    function menuCtrl($scope, $http, myService, sharedFilterService) {
        console.log('----------- menuCtrl loaded -----------');

        // ---- Lookup / dropdown lists ----
        $scope.InspectionTypeLayout = [];
        $scope.InspectionStatusLayout = [];
        $scope.regions = [];
        $scope.provinces = [];

        // Cities per section
        $scope.citiesSchedule = [];
        $scope.citiesStatus = [];
        $scope.citiesDocs = [];
        $scope.citiesIncident = [];
        $scope.citiesInternal = [];  // NEW

        // Locations per section
        $scope.locationsSchedule = [];
        $scope.locationsStatus = [];
        $scope.locationsDocs = [];
        $scope.locationsIncident = [];
        $scope.locationsInternal = [];  // NEW

        // Facilities per section
        // NOTE: start EMPTY — populated only when a Location is selected
        $scope.facilitiesSchedule = [];
        $scope.facilitiesStatus = [];
        $scope.facilitiesDocs = [];
        $scope.facilitiesIncident = [];
        $scope.facilitiesInternal = [];  // NEW

        // Areas per section
        // NOTE: start EMPTY — populated only when a Location is selected
        $scope.areasSchedule = [];
        $scope.areasStatus = [];
        $scope.areasDocs = [];
        $scope.areasIncident = [];
        $scope.areasInternal = [];  // NEW

        // ---- Filter models ----
        $scope.filterSchedule = {};
        $scope.filterStatus = {};
        $scope.filterDocs = { InspectionDocs: true, HistoricalDocs: true };
        $scope.filterIncident = {};
        $scope.filterInternal = {};  // NEW

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
            loadCityAll();
            loadLocationAll();
            // NOTE: Facilities and Areas are NOT pre-loaded globally anymore.
            // They are loaded on demand when a Location is selected via
            // loadFacilitiesAndAreas(section). This prevents showing all
            // facilities/areas before a location context is chosen.
        }

        // ---- Private data-load helpers ----
        function loadInspectionTypes() {
            myService.getInspectionTypes().then(function (res) {
                $scope.InspectionTypeLayout = res.data;
            });
        }

        function loadInspectionStatuses() {
            myService.getInspectionStatuses().then(function (res) {
                $scope.InspectionStatusLayout = res.data.map(function (s) {
                    s.selected = false;
                    return s;
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
                $scope.locationsIncident = res.data;
                $scope.locationsInternal = res.data;  // NEW
            });
        }

        function loadCityAll() {
            myService.getCityAll().then(function (res) {
                $scope.citiesStatus = res.data;
                $scope.citiesDocs = res.data;
                $scope.citiesIncident = res.data;
                $scope.citiesInternal = res.data;  // NEW
            });
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
                // Facility chosen — filter areas by facility
                myService.getAreasByFacilityId(facilityId).then(function (res) {
                    $scope['areas' + section] = res.data;
                });
            } else {
                // Facility cleared — fall back to all areas for current location
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
            $scope.filterStatus.selectedStatuses = $scope.InspectionStatusLayout
                .filter(function (s) { return s.selected; })
                .map(function (s) { return s.InspectionStatus; });
            sharedFilterService.setStatusFilter($scope.filterStatus);
            console.log('Status Filter:', $scope.filterStatus);
        };

        $scope.applyDocFilters = function () {
            var selectedInspection = ($scope.documenttypelistingInspection || [])
                .filter(function (s) { return s.selected; })
                .map(function (s) { return s.documenttype; });

            var selectedHistorical = ($scope.documenttypelistingHistory || [])
                .filter(function (s) { return s.selected; })
                .map(function (s) { return s.documenttype; });

            $scope.filterDocs.DocumentTypeList = selectedInspection.concat(selectedHistorical);
            sharedFilterService.setDocsFilter($scope.filterDocs);
            console.log('Docs Filter:', $scope.filterDocs);
        };

        $scope.applyIncidentFilters = function () {
            sharedFilterService.setIncidentFilter($scope.filterIncident);
            console.log('Incident Filter:', $scope.filterIncident);
        };

        // NEW — Internal Inspections filter apply
        $scope.applyInternalFilters = function () {
            sharedFilterService.setInternalFilter($scope.filterInternal);
            console.log('Internal Filter:', $scope.filterInternal);
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

