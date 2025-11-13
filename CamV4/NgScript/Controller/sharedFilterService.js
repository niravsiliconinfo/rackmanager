; (function () {
    'use strict';
    angular.module('myApp')
        .factory('sharedFilterService', function ($rootScope) {
            var scheduleFilter = {};
            var statusFilter = {};
            var docsFilter = {};
            var incidentFilter = {};

            return {
                // Schedule Filter
                setScheduleFilter: function (filter) {
                    scheduleFilter = angular.copy(filter);
                    $rootScope.$broadcast('scheduleFilterUpdated');
                },
                getScheduleFilter: function () {
                    return scheduleFilter;
                },

                // Status Filter
                setStatusFilter: function (filter) {
                    statusFilter = angular.copy(filter);
                    $rootScope.$broadcast('statusFilterUpdated');
                },
                getStatusFilter: function () {
                    return statusFilter;
                },

                // Docs Filter
                setDocsFilter: function (filter) {
                    docsFilter = angular.copy(filter);
                    $rootScope.$broadcast('docsFilterUpdated');
                },
                getDocsFilter: function () {
                    return docsFilter;
                },
                //Incident Filter
                setIncidentFilter: function (filter) {
                    incidentFilter = angular.copy(filter);
                    $rootScope.$broadcast('incidentFilterUpdated');
                },
                getIncidentFilter: function () {
                    return incidentFilter;
                }
            };
        });
    //angular.module('myApp').factory('sharedFilterService', function () {
    //    var filterStore = {
    //        schedule: {},
    //        status: {},
    //        docs: {}
    //    };

    //    return {
    //        setScheduleFilter: function (filter) {
    //            filterStore.schedule = filter;
    //        },
    //        getScheduleFilter: function () {
    //            return filterStore.schedule;
    //        },
    //        setStatusFilter: function (filter) {
    //            filterStore.status = filter;
    //        },
    //        getStatusFilter: function () {
    //            return filterStore.status;
    //        },
    //        setDocsFilter: function (filter) {
    //            filterStore.docs = filter;
    //        },
    //        getDocsFilter: function () {
    //            return filterStore.docs;
    //        }
    //    };
    //});
})();
        //// Service for Inspection Due
        //.factory('sharedInspectionDueFilterService', function () {
        //    var filterData = {};

        //    return {
        //        get: function () {
        //            return filterData;
        //        },
        //        set: function (data) {
        //            filterData = data;
        //            console.log('Inspection Due Filter Set:', filterData);
        //        },
        //        reset: function () {
        //            filterData = {};
        //        }
        //    };
        //})

        //// Service for Inspection Status (All Inspections) - Fixed typo
        //.factory('sharedInspectionFilterService', function () {
        //    var filterData = {};

        //    return {
        //        get: function () {
        //            return filterData;
        //        },
        //        set: function (data) {
        //            filterData = data;
        //            console.log('Inspection Filter Set:', filterData);
        //        },
        //        reset: function () {
        //            filterData = {};
        //        }
        //    };
        //})

        //// Service for Documents
        //.factory('sharedDocumentFilterService', function () {
        //    var filterData = {};

        //    return {
        //        get: function () {
        //            return filterData;
        //        },
        //        set: function (data) {
        //            filterData = data;
        //            console.log('Document Filter Set:', filterData);
        //        },
        //        reset: function () {
        //            filterData = {};
        //        }
        //    };
        //});

