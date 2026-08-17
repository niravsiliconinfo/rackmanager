// ============================================================
// menuCtrl.js  &ndash;  INVENTORY SIDEBAR ADDITIONS ONLY
//
// ADD these variables and functions inside your existing
// menuCtrl controller body. Do NOT replace or re-declare the
// module or controller &mdash; paste these into the correct position
// alongside your existing region/province/city filter logic.
//
// The sidebar markup in _CustomerLayout already wires:
//   ng-model="filterInventory.Region"
//   ng-model="filterInventory.ProvinceID"
//   ng-model="filterInventory.CityID"
//   ng-model="filterInventory.LocationID"
//   ng-model="filterInventory.FacilityID"
//   ng-model="filterInventory.AreaID"
//   ng-click="applySpare MaterialFilters()"
//
// These additions:
//  1. Initialize filterInventory object
//  2. Load cascading lookups for the Spare Material sidebar dropdowns
//     (Region/Province/City are shared with existing lists &mdash;
//      reuse them or use the same API calls you already have)
//  3. Watch cascade changes (Province->City->Location->Facility->Area)
//  4. Broadcast the filter so the listing controller can receive it
//  5. Add isInventory flag for sidebar accordion highlight
// ============================================================

// ── Paste inside your existing menuCtrl function body ──────

// Spare Material sidebar filter state
$scope.filterInventory = {
    Region: '',
    ProvinceID: '',
    CityID: '',
    LocationID: '',
    FacilityID: '',
    AreaID: ''
};

// Spare Material-specific dropdown lists (separate from other modules)
$scope.locationsSpare Material  = [];
$scope.facilitiesSpare Material = [];
$scope.areasSpare Material      = [];

// ── Cascade: Province -> City (Spare Material) ─────────────────
// Reuse your existing getProvinces / getCities API calls.
// Only the Location/Facility/Area dropdowns are Spare Material-specific.

$scope.$watch('filterInventory.ProvinceID', function (newVal, oldVal) {
    if (!newVal || newVal === oldVal) return;
    $scope.filterInventory.CityID      = '';
    $scope.filterInventory.LocationID  = '';
    $scope.filterInventory.FacilityID  = '';
    $scope.filterInventory.AreaID      = '';
    $scope.locationsSpare Material  = [];
    $scope.facilitiesSpare Material = [];
    $scope.areasSpare Material      = [];

    // Load cities &mdash; reuse your existing getCities call if available:
    // $scope.getCitiesByProvince(newVal);
    // OR call your cities API directly:
    $http.get('/api/pageview/getCities', { params: { ProvinceID: newVal } })
        .then(function (r) { $scope.citiesSpare Material = r.data; })
        .catch(function () { $scope.citiesSpare Material = []; });
});

// ── Cascade: City -> Location (Spare Material) ─────────────────
$scope.$watch('filterInventory.CityID', function (newVal, oldVal) {
    if (!newVal || newVal === oldVal) return;
    $scope.filterInventory.LocationID  = '';
    $scope.filterInventory.FacilityID  = '';
    $scope.filterInventory.AreaID      = '';
    $scope.locationsSpare Material  = [];
    $scope.facilitiesSpare Material = [];
    $scope.areasSpare Material      = [];

    $http.get('/api/pageview/getCustomerLocations', { params: { cityId: newVal } })
        .then(function (r) { $scope.locationsSpare Material = r.data; })
        .catch(function () { $scope.locationsSpare Material = []; });
});

// ── Cascade: Location -> Facility (Spare Material) ─────────────
$scope.$watch('filterInventory.LocationID', function (newVal, oldVal) {
    if (!newVal || newVal === oldVal) return;
    $scope.filterInventory.FacilityID = '';
    $scope.filterInventory.AreaID     = '';
    $scope.facilitiesSpare Material = [];
    $scope.areasSpare Material      = [];

    $http.get('/api/pageview/getCustomerFacilities', { params: { locationId: newVal } })
        .then(function (r) { $scope.facilitiesSpare Material = r.data; })
        .catch(function () { $scope.facilitiesSpare Material = []; });
});

// ── Cascade: Facility -> Area (Spare Material) ─────────────────
$scope.$watch('filterInventory.FacilityID', function (newVal, oldVal) {
    if (!newVal || newVal === oldVal) return;
    $scope.filterInventory.AreaID = '';
    $scope.areasSpare Material = [];

    $http.get('/api/pageview/getCustomerAreas', { params: { facilityId: newVal } })
        .then(function (r) { $scope.areasSpare Material = r.data; })
        .catch(function () { $scope.areasSpare Material = []; });
});

// ── Search Spare Material (broadcasts to listing controller) ───
$scope.applySpare MaterialFilters = function () {
    // Store in a shared service so the listing page can pick it up
    // even after a page navigation (same-page SPA: use $broadcast).
    $scope.$broadcast('inventoryFiltersApplied', angular.copy($scope.filterInventory));

    // Also store in sessionStorage so the listing controller can read
    // on page load (cross-page navigation scenario).
    try {
        sessionStorage.setItem('inventoryFilter', JSON.stringify($scope.filterInventory));
    } catch (e) { /* sessionStorage not available */ }

    // Navigate to listing page
    window.location.href = '/Customer/SpareMaterialListing';
};
