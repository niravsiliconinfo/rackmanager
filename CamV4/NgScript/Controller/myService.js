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

    this.getLocationsRegion = function (region) {
        return $http.get('/api/pageview/getLocations', {
            params: {
                region: region
            }
        });
    };

    // Alias for getFacilities (existing) to match controller usage
    this.getFacilitiesByLocationId = function (locationId) {
        return $http.get('/api/pageview/getFacilities', {
            params: { locationId: locationId }
        });
    };

    // Alias for getAreas by location and facility
    this.getAreasByLocationId = function (locationId) {
        return $http.get('/api/pageview/getAreas', {
            params: { locationId: locationId }
        });
    };

    this.getAreasByFacilityId = function (facilityId) {
        return $http.get('/api/pageview/getAreas', {
            params: { facilityId: facilityId }
        });
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

    // New method: get facilities by location ID (alias for getFacilities)
    this.getFacilitiesByLocationId = function (locationId) {
        return $http.get('/api/pageview/getFacilities', {
            params: {
                locationId: locationId
            }
        });
    };

    // New method: get areas by location ID (facilityId optional)
    this.getAreasByLocationId = function (locationId) {
        return $http.get('/api/pageview/getAreas', {
            params: {
                locationId: locationId
            }
        });
    };

    // New method: get areas by facility ID (locationId optional)
    this.getAreasByFacilityId = function (facilityId) {
        return $http.get('/api/pageview/getAreas', {
            params: {
                facilityId: facilityId
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

    // New method: fetch all dropdown master data in one call
    this.getDropdownMaster = function () {
        return $http.get('/api/pageview/getDropdownMaster');
    };
});
