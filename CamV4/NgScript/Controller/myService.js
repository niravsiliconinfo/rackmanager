angular.module('myApp')
    .factory('myService', function ($http) {
        var baseUrl = '/api/pageview/';

        return {
            getProvinces: function () {
                return $http.get(baseUrl + 'getProvincebyCountryIdByCustomer');
            },
            getCityAll: function () {
                return $http.get(baseUrl + 'getCitybyByCustomer');
            },
            getCities: function (provinceId) {
                return $http.get(baseUrl + 'getCitybyProvinceIdByCustomer', {
                    params: { id: provinceId }
                });
            },
            getLocationsAll: function () {
                return $http.get(baseUrl + 'getLocationbyByCustomer');
            },
            getLocations: function (cityId) {
                return $http.get(baseUrl + 'getLocationbyCityIdByCustomer', {
                    params: { id: cityId }
                });
            },
            getRegions: function () {
                return $http.get(baseUrl + 'getRegionbyCustomer');
            },
            getInspectionTypes: function () {
                return $http.get(baseUrl + 'getAllInspectionType');
            },
            getInspectionStatuses: function () {
                return $http.get(baseUrl + 'getAllInspectionStatus');
            },
            getLocationsRegion: function (Region) {
                console.log('In Services', Region);
                return $http.get(baseUrl + 'getLocationbyRegionByCustomer', {
                    params: { region: Region }
                });
            }
        };
    });

//angular.module('myApp')
//    .factory('myService', function ($http) {
//        return {
//            getAllInspectionType: function () {
//                return $http.get('/api/pageview/getAllInspectionType')
//                    .then(response => response.data);
//            },
//            getAllInspectionStatus: function () {
//                return $http.get('/api/pageview/getAllInspectionStatus')
//                    .then(response => response.data);
//            },
//            getProvinceByCountryIdFilter: function () {
//                return $http.get('/api/pageview/getProvincebyCountryIdByCustomer')
//                    .then(response => response.data);
//            },
//            getCityByProvinceId: function (provinceId) {
//                return $http.get('/api/pageview/getCitybyProvinceIdByCustomer', {
//                    params: { id: provinceId }
//                }).then(response => response.data);
//            },
//            getLocationByCityId: function (cityId) {
//                return $http.get('/api/pageview/getLocationbyCityIdByCustomer', {
//                    params: { id: cityId }
//                }).then(response => response.data);
//            },
//            getRegionByCustomer: function () {
//                return $http.get('/api/pageview/getRegionbyCustomer')
//                    .then(response => response.data);
//            }
//        };
//    });
