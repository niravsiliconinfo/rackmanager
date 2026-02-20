// Retrieves the existing 'myApp' module — does NOT re-declare it
angular.module('myApp')
    .controller('customerHierarchyCtrl', function ($scope, $http) {

        // customerId is set via ng-init in the view
        $scope.customerId = 0;

        $scope.locationList = [];
        $scope.facilityList = [];
        $scope.areaList = [];
        $scope.getAllCountries = [];
        $scope.getProvincebyCountryId = [];
        $scope.getCitybyProvinceId = [];
        $scope.filteredFacilitiesForArea = [];

        $scope.loc = {};
        $scope.fac = {};
        $scope.area = {};

        $scope.locError = ''; $scope.locSuccess = '';
        $scope.facError = ''; $scope.facSuccess = '';
        $scope.areaError = ''; $scope.areaSuccess = '';

        // ---- Helpers ----
        $scope.GetLocationName = function (id) {
            var found = $scope.locationList.filter(function (l) { return l.CustomerLocationID == id; });
            return found.length > 0 ? found[0].LocationName : '';
        };

        $scope.GetFacilityName = function (id) {
            if (!id || id == 0) return '-';
            var found = $scope.facilityList.filter(function (f) { return f.CustomerFacilityID == id; });
            return found.length > 0 ? found[0].FacilityName : '';
        };

        // ---- Data Load ----
        $scope.LoadHierarchy = function () {
            if (!$scope.customerId || $scope.customerId == 0) return;
            $http.get('/api/pageview/GetCustomerHierarchy?customerId=' + $scope.customerId)            
                .then(function (res) {
                    $scope.locationList = res.data.Locations || [];
                    $scope.facilityList = res.data.Facilities || [];
                    $scope.areaList = res.data.Areas || [];
                });
        };

        $scope.LoadCountries = function () {
            $http.get('/api/pageview/getAllCountries')
                .then(function (res) { $scope.getAllCountries = res.data; });
        };

        $scope.getProvincebyCountryId = function () {
            $scope.loc.provinceId = '';
            $scope.loc.cityId = '';
            $scope.getProvincebyCountryId = [];
            $scope.getCitybyProvinceId = [];
            if (!$scope.loc.countryId) return;
            $http.get('/api/pageview/getProvincebyCountryId?id=' + $scope.loc.countryId)
                .then(function (res) { $scope.getProvincebyCountryId = res.data; });
        };

        $scope.getCitybyProvinceId = function () {
            $scope.loc.cityId = '';
            $scope.getCitybyProvinceId = [];
            if (!$scope.loc.provinceId) return;
            $http.get('/api/pageview/getCitybyProvinceId?id=' + $scope.loc.provinceId)
                .then(function (res) { $scope.getCitybyProvinceId = res.data; });
        };

        // ---- LOCATION ----
        $scope.ClearLocation = function () {
            $scope.loc = {};
            $scope.getProvincebyCountryId = [];
            $scope.getCitybyProvinceId = [];
            $scope.locError = '';
            $scope.locSuccess = '';
        };

        $scope.SaveLocation = function () {
            $scope.locError = '';
            $scope.locSuccess = '';

            if (!$scope.loc.locationName || $scope.loc.locationName.trim() === '') {
                $scope.locError = 'Location Name is required.';
                return;
            }

            var duplicate = $scope.locationList.filter(function (l) {
                return l.LocationName.toLowerCase() === $scope.loc.locationName.trim().toLowerCase()
                    && l.CustomerLocationID != $scope.loc.customerLocationId;
            });
            if (duplicate.length > 0) {
                $scope.locError = 'A location with this name already exists.';
                return;
            }

            var data = {
                CustomerLocationID: $scope.loc.customerLocationId || 0,
                CustomerId: $scope.customerId,
                LocationName: $scope.loc.locationName,
                CustomerAddress: $scope.loc.customerAddress || '',
                CountryID: $scope.loc.countryId || null,
                ProvinceID: $scope.loc.provinceId || null,
                CityID: $scope.loc.cityId || null,
                Pincode: $scope.loc.pincode || '',
                Region: $scope.loc.region || ''
            };

            $http.post('/api/pageview/SaveCustomerLocation', data, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data >= 1) {
                    $scope.locSuccess = 'Location saved successfully.';
                    $scope.ClearLocation();
                    $scope.LoadHierarchy();
                } else {
                    $scope.locError = 'Failed to save location.';
                }
            }).catch(function () { $scope.locError = 'An error occurred.'; });
        };

        $scope.EditLocation = function (l) {
            $scope.loc = {
                customerLocationId: l.CustomerLocationID,
                locationName: l.LocationName,
                customerAddress: l.CustomerAddress,
                countryId: l.CountryID ? String(l.CountryID) : '',
                provinceId: l.ProvinceID ? String(l.ProvinceID) : '',
                cityId: l.CityID ? String(l.CityID) : '',
                pincode: l.Pincode ? l.Pincode.trim() : '',
                region: l.Region || ''
            };
            $scope.locError = '';
            $scope.locSuccess = '';

            if ($scope.loc.countryId) {
                $http.get('/api/pageview/getProvincebyCountryId?id=' + $scope.loc.countryId).then(function (res) {
                    $scope.getProvincebyCountryId = res.data;
                    if ($scope.loc.provinceId) {
                        $http.get('/api/pageview/getCitybyProvinceId?id=' + $scope.loc.provinceId).then(function (r2) {
                            $scope.getCitybyProvinceId = r2.data;
                        });
                    }
                });
            }
            window.scrollTo(0, 0);
        };

        $scope.DeleteLocation = function (id) {
            if (!confirm('Delete this location? All associated facilities and areas will also be deactivated.')) return;
            $http.post('/api/pageview/DeleteCustomerLocation', { id: id }, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data === 1) { $scope.LoadHierarchy(); }
                else { alert('Failed to delete location.'); }
            });
        };

        // ---- FACILITY ----
        $scope.ClearFacility = function () {
            $scope.fac = {};
            $scope.facError = '';
            $scope.facSuccess = '';
        };

        $scope.SaveFacility = function () {
            $scope.facError = '';
            $scope.facSuccess = '';

            if (!$scope.fac.customerLocationId) {
                $scope.facError = 'Location is required.';
                return;
            }
            if (!$scope.fac.facilityName || $scope.fac.facilityName.trim() === '') {
                $scope.facError = 'Facility Name is required.';
                return;
            }

            var duplicate = $scope.facilityList.filter(function (f) {
                return f.FacilityName.toLowerCase() === $scope.fac.facilityName.trim().toLowerCase()
                    && f.CustomerLocationID == $scope.fac.customerLocationId
                    && f.CustomerFacilityID != $scope.fac.customerFacilityId;
            });
            if (duplicate.length > 0) {
                $scope.facError = 'A facility with this name already exists for the selected location.';
                return;
            }

            var data = {
                CustomerFacilityID: $scope.fac.customerFacilityId || 0,
                CustomerID: $scope.customerId,
                CustomerLocationID: $scope.fac.customerLocationId,
                FacilityName: $scope.fac.facilityName
            };

            $http.post('/api/pageview/SaveCustomerFacility', data, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data >= 1) {
                    $scope.facSuccess = 'Facility saved successfully.';
                    $scope.ClearFacility();
                    $scope.LoadHierarchy();
                } else {
                    $scope.facError = 'Failed to save facility.';
                }
            }).catch(function () { $scope.facError = 'An error occurred.'; });
        };

        $scope.EditFacility = function (f) {
            $scope.fac = {
                customerFacilityId: f.CustomerFacilityID,
                customerLocationId: String(f.CustomerLocationID),
                facilityName: f.FacilityName
            };
            $scope.facError = '';
            $scope.facSuccess = '';
        };

        $scope.DeleteFacility = function (id) {
            if (!confirm('Delete this facility?')) return;
            $http.post('/api/pageview/DeleteCustomerFacility', { id: id }, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data === 1) { $scope.LoadHierarchy(); }
                else { alert('Failed to delete facility.'); }
            });
        };

        // ---- AREA ----
        $scope.FilterFacilitiesForArea = function () {
            $scope.area.customerFacilityId = '';
            if (!$scope.area.customerLocationId) {
                $scope.filteredFacilitiesForArea = [];
                return;
            }
            $scope.filteredFacilitiesForArea = $scope.facilityList.filter(function (f) {
                return f.CustomerLocationID == $scope.area.customerLocationId;
            });
        };

        $scope.ClearArea = function () {
            $scope.area = {};
            $scope.filteredFacilitiesForArea = [];
            $scope.areaError = '';
            $scope.areaSuccess = '';
        };

        $scope.SaveArea = function () {
            $scope.areaError = '';
            $scope.areaSuccess = '';

            if (!$scope.area.customerLocationId) {
                $scope.areaError = 'Location is required.';
                return;
            }
            if (!$scope.area.areaName || $scope.area.areaName.trim() === '') {
                $scope.areaError = 'Area Name is required.';
                return;
            }

            if ($scope.area.customerFacilityId) {
                var validFac = $scope.facilityList.filter(function (f) {
                    return f.CustomerFacilityID == $scope.area.customerFacilityId
                        && f.CustomerLocationID == $scope.area.customerLocationId;
                });
                if (validFac.length === 0) {
                    $scope.areaError = 'Selected facility does not belong to the selected location.';
                    return;
                }
            }

            var duplicate = $scope.areaList.filter(function (a) {
                return a.AreaName.toLowerCase() === $scope.area.areaName.trim().toLowerCase()
                    && a.CustomerLocationID == $scope.area.customerLocationId
                    && a.AreaID != $scope.area.areaId;
            });
            if (duplicate.length > 0) {
                $scope.areaError = 'An area with this name already exists for the selected location.';
                return;
            }

            var data = {
                AreaID: $scope.area.areaId || 0,
                CustomerID: $scope.customerId,
                CustomerLocationID: $scope.area.customerLocationId,
                CustomerFacilityID: $scope.area.customerFacilityId || 0,
                AreaName: $scope.area.areaName
            };

            $http.post('/api/pageview/SaveCustomerArea', data, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data >= 1) {
                    $scope.areaSuccess = 'Area saved successfully.';
                    $scope.ClearArea();
                    $scope.LoadHierarchy();
                } else {
                    $scope.areaError = 'Failed to save area.';
                }
            }).catch(function () { $scope.areaError = 'An error occurred.'; });
        };

        $scope.EditArea = function (a) {
            $scope.area = {
                areaId: a.AreaID,
                customerLocationId: String(a.CustomerLocationID),
                customerFacilityId: a.CustomerFacilityID && a.CustomerFacilityID != 0 ? String(a.CustomerFacilityID) : '',
                areaName: a.AreaName
            };
            $scope.FilterFacilitiesForArea();
            $scope.areaError = '';
            $scope.areaSuccess = '';
        };

        $scope.DeleteArea = function (id) {
            if (!confirm('Delete this area?')) return;
            $http.post('/api/pageview/DeleteCustomerArea', { id: id }, {
                headers: { 'RequestVerificationToken': $scope.antiForgeryToken }
            }).then(function (res) {
                if (res.data === 1) { $scope.LoadHierarchy(); }
                else { alert('Failed to delete area.'); }
            });
        };

        // ---- Bootstrap: wait for ng-init to set customerId ----
        var unwatch = $scope.$watch('customerId', function (val) {
            if (val && val !== '0' && val !== '' && val !== null) {
                $scope.LoadCountries();
                $scope.LoadHierarchy();
                unwatch();
            }
        });
    });