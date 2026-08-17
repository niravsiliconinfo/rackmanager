// NgScript/inventoryFilters.js
// Shared Angular filters for the Inventory module.
// Load this ONCE in your layout or on every inventory page —
// before any inventory controller scripts.
//
// Add to _AdminLayout.cshtml + _CustomerLayout.cshtml:
//   <script src="~/NgScript/inventoryFilters.js"></script>
//
// If you already have a startFrom filter registered in your
// global app.js, DO NOT load this file — remove the duplicate.

var app = angular.module('myApp');

// startFrom: companion to limitTo, used for manual pagination slicing.
// Usage in ng-repeat: | startFrom:(currentPage-1)*itemsPerPage | limitTo:itemsPerPage
app.filter('startFrom', function () {
    return function (input, start) {
        if (!Array.isArray(input)) return [];
        return input.slice(parseInt(start, 10) || 0);
    };
});
