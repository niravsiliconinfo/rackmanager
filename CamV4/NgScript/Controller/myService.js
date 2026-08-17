app.service('myService', function ($http) {

    // ===========================
    // LOOKUPS
    // ===========================

    this.getRegions = function () {
        return $http.get('/api/pageview/getRegions');
    };

    this.getProvinces = function (region) {
        console.log('Load getProvinces');
        return $http.get('/api/pageview/getProvinces', {
            params: {
                region: region
            }
        });
    };

    this.getCities = function (provinceId, region) {
        return $http.get('/api/pageview/getCities', {
            params: {
                provinceId: provinceId,
                region: region
            }
        });
    };

    this.getCitiesAll = function () {
        return $http.get('/api/pageview/getCitybyByCustomer');
    };

    this.getLocationsAll = function () {
        return $http.get('/api/pageview/getLocationByCustomer');
    };

    this.getFacilitiesAll = function () {
        return $http.get('/api/pageview/getFacilityByCustomer');
    };

    this.getLocations = function (region, provinceId, cityId) {
        return $http.get('/api/pageview/getLocations', {
            params: {
                region: region,
                provinceId: provinceId,
                cityId: cityId
            }
        });
    };

    this.getFacilities = function (locationId) {
        return $http.get('/api/pageview/getFacilities', {
            params: {
                locationId: locationId
            }
        });
    };

    this.getAreasAll = function () {
        return $http.get('/api/pageview/getAreaByCustomer');
    };

    this.getAreas = function (locationId, facilityId) {
        return $http.get('/api/pageview/getAreas', {
            params: {
                locationId: locationId,
                facilityId: facilityId
            }
        });
    };

    // ===========================
    // INSPECTIONS
    // ===========================

    this.getInspectionListing = function (filters) {
        return $http.post(
            '/api/pageview/getInspectionListing',
            filters
        );
    };

    // ===========================
    // DOCUMENTS
    // ===========================

    this.getDocumentListing = function (filters) {
        return $http.post(
            '/api/pageview/getDocumentListing',
            filters
        );
    };

    this.getInspectionDocumentCategories = function () {
        return $http.get(
            '/api/pageview/getInspectionDocumentCategories'
        );
    };

    this.getHistoricalDocumentCategories = function () {
        return $http.get(
            '/api/pageview/getHistoricalDocumentCategories'
        );
    };

    // ===========================
    // INCIDENTS
    // ===========================

    this.getIncidentListing = function (filters) {
        return $http.post(
            '/api/pageview/getIncidentListing',
            filters
        );
    };

    // ===========================
    // INTERNAL INSPECTIONS
    // ===========================

    this.getInternalInspectionListing = function (filters) {
        return $http.post(
            '/api/pageview/getInternalInspectionListing',
            filters
        );
    };

  
    this.getInspectionTypes = function () {
        return $http.get('/api/pageview/getInspectionTypes');
    };

    this.getInspectionStatuses = function () {
        return $http.get('/api/pageview/getInspectionStatuses');
    };
});
//angular.module('myApp')
//    .factory('myService', function ($http) {
//        var baseUrl = '/api/pageview/';

//        return {

//            //  Province / City / Location 
//            getProvinces: function () {
//                return $http.get(baseUrl + 'getProvincebyCountryIdByCustomer');
//            },
//            getCityAll: function () {
//                return $http.get(baseUrl + 'getCitybyByCustomer');
//            },
//            getCities: function (provinceId) {
//                return $http.get(baseUrl + 'getCitybyProvinceIdByCustomer', {
//                    params: { id: provinceId }
//                });
//            },
//            getLocationsAll: function () {
//                return $http.get(baseUrl + 'getLocationByCustomer');
//            },
//            getFacilitiesAll: function () {
//                return $http.get(baseUrl + 'getFacilityByCustomerMenu');
//            },
//            getAreaAll: function () {
//                return $http.get(baseUrl + 'getAreaByCustomer');
//            },
//            getLocations: function (cityId) {
//                return $http.get(baseUrl + 'getLocationbyCityIdByCustomer', {
//                    params: { id: cityId }
//                });
//            },
//            getLocationsRegion: function (region) {
//                return $http.get(baseUrl + 'getLocationbyRegionByCustomer', {
//                    params: { region: region }
//                });
//            },

//            //  Region 
//            getRegions: function () {
//                return $http.get(baseUrl + 'getRegionbyCustomer');
//            },

//            //  Inspection Types / Statuses 
//            getInspectionTypes: function () {
//                return $http.get(baseUrl + 'getAllInspectionType');
//            },
//            getInspectionStatuses: function () {
//                return $http.get(baseUrl + 'getAllInspectionStatus');
//            },

//            //  Facility  (Location → Facilities) 
//            // GET /api/pageview/getFacilitiesByLocationId?locationId=X
//            getFacilitiesByLocationId: function (locationId) {
//                return $http.get(baseUrl + 'getFacilitiesByLocationId', {
//                    params: { locationId: locationId }
//                });
//            },

//            //  Area  (two entry points) 
//            // 1) Location selected  → load ALL areas for that location
//            //    GET /api/pageview/getAreasByFacilityId?locationId=X
//            getAreasByLocationId: function (locationId) {
//                return $http.get(baseUrl + 'getAreasByFacilityId', {
//                    params: { locationId: locationId }
//                });
//            },
//            // 2) Facility selected  → re-filter areas by that facility
//            //    GET /api/pageview/getAreasByFacilityId?facilityId=X
//            getAreasByFacilityId: function (facilityId) {
//                return $http.get(baseUrl + 'getAreasByFacilityId', {
//                    params: { facilityId: facilityId }
//                });
//            }
//        };
//    });


////angular.module('myApp')
////    .factory('myService', function ($http) {
////        var baseUrl = '/api/pageview/';

////        return {
////            getProvinces: function () {
////                return $http.get(baseUrl + 'getProvincebyCountryIdByCustomer');
////            },
////            getCityAll: function () {
////                return $http.get(baseUrl + 'getCitybyByCustomer');
////            },
////            getCities: function (provinceId) {
////                return $http.get(baseUrl + 'getCitybyProvinceIdByCustomer', {
////                    params: { id: provinceId }
////                });
////            },
////            getLocationsAll: function () {
////                return $http.get(baseUrl + 'getLocationbyByCustomer');
////            },
////            getLocations: function (cityId) {
////                return $http.get(baseUrl + 'getLocationbyCityIdByCustomer', {
////                    params: { id: cityId }
////                });
////            },
////            getRegions: function () {
////                return $http.get(baseUrl + 'getRegionbyCustomer');
////            },
////            getInspectionTypes: function () {
////                return $http.get(baseUrl + 'getAllInspectionType');
////            },
////            getInspectionStatuses: function () {
////                return $http.get(baseUrl + 'getAllInspectionStatus');
////            },
////            getLocationsRegion: function (Region) {
////                console.log('In Services', Region);
////                return $http.get(baseUrl + 'getLocationbyRegionByCustomer', {
////                    params: { region: Region }
////                });
////            },
////            getFacilityLocation: function (Location) {
////                console.log('In Services', Location);
////                return $http.get(baseUrl + 'getAllFacilitybyLocation', {
////                    params: { region: Location }
////                });
////            },

////        };
////    });

////angular.module('myApp')
////    .factory('myService', function ($http) {
////       return {
////           getAllInspectionType: function () {
////               return $http.get('/api/pageview/getAllInspectionType')
////                   .then(response => response.data);
////           },
////           getAllInspectionStatus: function () {
////               return $http.get('/api/pageview/getAllInspectionStatus')
////                   .then(response => response.data);
////           },
////           getProvinceByCountryIdFilter: function () {
////               return $http.get('/api/pageview/getProvincebyCountryIdByCustomer')
////                   .then(response => response.data);
////           },
////           getCityByProvinceId: function (provinceId) {
////               return $http.get('/api/pageview/getCitybyProvinceIdByCustomer', {
////                   params: { id: provinceId }
////               }).then(response => response.data);
////           },
////           getLocationByCityId: function (cityId) {
////               return $http.get('/api/pageview/getLocationbyCityIdByCustomer', {
////                   params: { id: cityId }
////               }).then(response => response.data);
////           },
////           getRegionByCustomer: function () {
////               return $http.get('/api/pageview/getRegionbyCustomer')
////                   .then(response => response.data);
////           }
////       };
////    });
