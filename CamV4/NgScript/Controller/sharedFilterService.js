app.factory('sharedFilterService', function ($rootScope) {

    var service = {};

    // =====================================
    // INSPECTION FILTERS
    // =====================================

    var inspectionFilters = {
        InspectionTypeId: '',
        SelectedStatusIds: [],
        Region: '',
        ProvinceId: null,
        CityId: null,
        CustomerLocationId: null,
        CustomerFacilityId: null,
        CustomerAreaId: null
    };

    // =====================================
    // DOCUMENT FILTERS
    // =====================================

    var documentFilters = {
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

    // =====================================
    // INCIDENT FILTERS
    // =====================================

    var incidentFilters = {
        IncidentType: '',
        Region: '',
        ProvinceId: null,
        CityId: null,
        CustomerLocationId: null,
        CustomerFacilityId: null,
        CustomerAreaId: null
    };

    // =====================================
    // INTERNAL INSPECTION FILTERS
    // =====================================

    var internalInspectionFilters = {
        Status: '',
        Region: '',
        ProvinceId: null,
        CityId: null,
        CustomerLocationId: null,
        CustomerFacilityId: null,
        CustomerAreaId: null
    };

    // =====================================
    // INTERNAL INVENTORY FILTERS
    // =====================================

    var internalInventoryFilters = {
        Region: '',
        ProvinceId: null,
        CityId: null,
        CustomerLocationId: null,
        CustomerFacilityId: null,
        CustomerAreaId: null
    };



    // =====================================
    // INSPECTION FILTERS METHODS
    // =====================================

    service.setInspectionFilters = function (filters) {
        console.log('Broadcasting inspectionFiltersUpdated', filters);
        inspectionFilters = angular.copy(filters);
        $rootScope.$broadcast(
            'inspectionFiltersUpdated',
            inspectionFilters);
    };

    service.getInspectionFilters = function () {
        return angular.copy(inspectionFilters);
    };

    // Alias methods (if controller uses singular form)
    service.setInspectionFilter = function (filters) {
        service.setInspectionFilters(filters);
    };

    service.getInspectionFilter = function () {
        return service.getInspectionFilters();
    };

    // =====================================
    // DOCUMENT FILTERS METHODS
    // =====================================

    service.setDocumentFilters = function (filters) {
        documentFilters = angular.copy(filters);
        $rootScope.$broadcast(
            'documentFiltersUpdated',
            documentFilters
        );
    };

    service.getDocumentFilters = function () {
        return angular.copy(documentFilters);
    };

    // Alias methods (if controller uses singular form)
    service.setDocumentFilter = function (filters) {
        service.setDocumentFilters(filters);
    };

    service.getDocumentFilter = function () {
        return service.getDocumentFilters();
    };

    // =====================================
    // INCIDENT FILTERS METHODS
    // =====================================

    service.setIncidentFilters = function (filters) {
        incidentFilters = angular.copy(filters);
        $rootScope.$broadcast(
            'incidentFiltersUpdated',
            incidentFilters
        );
    };

    service.getIncidentFilters = function () {
        return angular.copy(incidentFilters);
    };

    // Alias methods (if controller uses singular form) - THIS FIXES YOUR ERROR
    service.setIncidentFilter = function (filters) {
        service.setIncidentFilters(filters);
    };

    service.getIncidentFilter = function () {
        return service.getIncidentFilters();
    };

    // =====================================
    // INTERNAL INSPECTION FILTERS METHODS
    // =====================================

    service.setInternalInspectionFilters = function (filters) {
        internalInspectionFilters = angular.copy(filters);
        $rootScope.$broadcast(
            'internalInspectionFiltersUpdated',
            internalInspectionFilters
        );
    };

    service.getInternalInspectionFilters = function () {
        return angular.copy(internalInspectionFilters);
    };

    // Alias methods (if controller uses singular form)
    service.setInternalInspectionFilter = function (filters) {
        service.setInternalInspectionFilters(filters);
    };

    service.getInternalInspectionFilter = function () {
        return service.getInternalInspectionFilters();
    };

    // =====================================
    // INTERNAL INVENTORY FILTERS METHODS
    // =====================================

    service.setInternalInventoryFilters = function (filters) {
        internalInventoryFilters = angular.copy(filters);
        $rootScope.$broadcast(
            'internalInventoryFiltersUpdated',
            internalInventoryFilters
        );
    };

    service.getInternalInventoryFilters = function () {
        return angular.copy(internalInventoryFilters);
    };

    // Alias methods (if controller uses singular form)
    service.setInternalInventoryFilter = function (filters) {
        service.setInternalInventoryFilters(filters);
    };

    service.getInternalInventoryFilter = function () {
        return service.getInternalInventoryFilters();
    };

    // =====================================
    // RESET ALL FILTERS
    // =====================================

    service.clearAll = function () {
        inspectionFilters = {
            InspectionTypeId: '',
            SelectedStatusIds: [],
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        documentFilters = {
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

        incidentFilters = {
            IncidentType: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        internalInspectionFilters = {
            Status: '',
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };

        internalInventoryFilters = {
            Region: '',
            ProvinceId: null,
            CityId: null,
            CustomerLocationId: null,
            CustomerFacilityId: null,
            CustomerAreaId: null
        };
    };

    // =====================================
    // STATUS FILTER CONVENIENCE METHODS
    // =====================================

    service.setStatusFilter = function (filters) {
        console.log('In Shared Filter Services setStatusFilter', filters);
        service.setInspectionFilters(filters);
    };

    service.getStatusFilter = function () {
        return service.getInspectionFilters();
    };

    return service;
});
