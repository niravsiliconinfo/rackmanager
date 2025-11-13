// services/inspectionFilterService.js
app.factory('inspectionFilterService', function () {
    var filterData = {
        selectedStatuses: []
    };

    return {
        getFilter: function () {
            return filterData;
        },
        setSelectedStatuses: function (statuses) {
            filterData.selectedStatuses = statuses;
        }
    };
});
