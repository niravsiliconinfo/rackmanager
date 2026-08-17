(function () {
    'use strict';

    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    //app.controller('customerCtrl', customerCtrl);
    angular.module('myApp')
        .controller('customerCtrl', customerCtrl);

    app.filter('startFrom', function () {
        return function (input, start) {
            if (input) {
                start = +start;
                return input.slice(start);
            }
            return [];
        };
    });

    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                items.forEach(function (item) {
                    var itemMatches = false;

                    var keys = Object.keys(props);
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });

    customerCtrl.$inject = ['$scope', '$http', '$timeout', 'Upload', '$window'];

    function customerCtrl($scope, $http, $timeout, Upload, $window) {

        $scope.viewby = '50';
        $scope.currentPage = '1';
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = '10'; //Number of pager buttons to show
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };
        $scope.pageChanged = function () { };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        $scope.ShowAddCustomer = true;
        $scope.ShowAddCustLocation = false;

        $http.get('/api/pageview/getAllCustomers').then(function (response) {
            $scope.getAllCustomers = response.data;
            console.log('$scope.getAllCustomers', $scope.getAllCustomers);
            if (window.location.pathname == "/Admin/ManageCustomer") {
                $scope.getAllCustomers.forEach(function (value) {
                    let cId = value.CustomerID.toString();
                    value.CustomerID = String(cId).padStart(4, '0');
                });
            }
            if ($scope.getAllCustomers != null) { $scope.getAllCustomerscount = $scope.getAllCustomers.length; }
            else { $scope.getAllCustomerscount = 0; }
            $scope.totalgetAllCustomers = $scope.getAllCustomerscount;
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.getAllCustomerLocationByCustomerId = function () {
            var para = window.location.search;
            para = para.replace('?id=', '');
            console.log('getAllCustomerLocationByCustomerId', para);
            $http.get('/api/pageview/getAllCustomerLocationByCustomerId', { params: { id: para } }).then(function (response) {
                $scope.getAllCustomerLocationByCustomerId = response.data;
                console.log('getAllCustomerLocationByCustomerId--', $scope.getAllCustomerLocationByCustomerId);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $http.get('/api/pageview/getAllCountries').then(function (response) {
            $scope.getAllCountries = response.data;
            console.log('getAllCountries--', $scope.getAllCountries);
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllSalesRep').then(function (response) {
            $scope.getAllSalesRep = response.data;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllProvince').then(function (response) {
            $scope.getAllProvince = response.data;
            console.log('getAllProvince--', $scope.getAllProvince);
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllCities').then(function (response) {
            $scope.getAllCities = response.data;
            console.log('getAllCities--', $scope.getAllCities);
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.GetProvincebyCountryId = function () {
            $scope.strCountry = document.getElementById("drpcountry").value;
            console.log('strCountry', $scope.strCountry);
            $http.get('/api/pageview/getProvincebyCountryId', { params: { id: $scope.strCountry } }).then(function (response) {
                $scope.getProvincebyCountryId = response.data;
                console.log('getProvincebyCountryId--', $scope.getProvincebyCountryId);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $scope.GetCitybyProvinceId = function () {
            $scope.strProvince = document.getElementById("drpprovince").value;
            console.log('strProvince', $scope.strProvince);
            $http.get('/api/pageview/getCitybyProvinceId', { params: { id: $scope.strProvince } }).then(function (response) {
                $scope.getCitybyProvinceId = response.data;
                console.log('getCitybyProvinceId--', $scope.getCitybyProvinceId);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $scope.GetProvincebyCountryModelId = function (id) {
            console.log('strProvince', id);
            $http.get('/api/pageview/getProvincebyCountryId', { params: { id: id } }).then(function (response) {
                $scope.getAllProvince = response.data;
                console.log('getProvincebyCountryId--', $scope.getAllProvince);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $scope.GetCitybyProvinceModelId = function (id) {
            console.log('strProvince', id);
            $http.get('/api/pageview/getCitybyProvinceId', { params: { id: id } }).then(function (response) {
                $scope.getAllCities = response.data;
                console.log('getCitybyProvinceId--', $scope.getAllCities);
            }, function (response) {
                $scope.waiting = false;
            });
        };

        $http.get('/api/pageview/getAllCustomersByUserType').then(function (response) {
            $scope.getAllCustomersByUserType = response.data;
            console.log('$scope.getAllCustomersByUserType', $scope.getAllCustomersByUserType);
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.SaveCustomer = function () {
            //console.log('AddCustomer click', $scope.customerlogo.filename);
            //console.log('AddCustomer click');
            var input = document.getElementById('getlogoid');
            var file = input.files;
            $scope.SelectedFiles = file;
            if (!$scope.customerlogo) {
                console.log('hhhhhhhh');
                var config = {
                    CustomerId: 0, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    user: { UserName: $scope.userName, UserPassword: $scope.password }, SalesRepresentativeId: $scope.salesrepresentativeid
                }
            }
            else {
                console.log('hhhiiiii');
                var config = {
                    CustomerId:0, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, CustomerLogo: $scope.customerlogo.filename,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    user: { UserName: $scope.userName, UserPassword: $scope.password }, SalesRepresentativeId: $scope.salesrepresentativeid
                }
            }

            console.log('AddCustomer', config);            
            Upload.upload({
                url: '/uploadImagetoLogoFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('$scope.AddCustomer resp--', response.data)
                    if (response.data == "Ok") {
                        var url = '/Admin/ManageCustomer';
                        window.location = url;
                    }
                    else {
                        $scope.errorNot = response.data;
                    }
                });
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.EditCustomer = function (id) {
            var input = document.getElementById('getlogoid');
            var file = input.files;
            $scope.SelectedFiles = file;
            console.log('123978-', $scope.SelectedFiles);
            console.log('0000XXXXXXXXXXXXXXXXXx-', $scope.customercontactname);

            if ($scope.customerlogo == undefined) {
                console.log('2222-');
                var config = {
                    CustomerId: id, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, user: { UserName: $scope.userName, UserPassword: $scope.password },
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    SalesRepresentativeId: $scope.salesrepresentativeid
                }
            }
            else {
                console.log('3333-');
                var config = {
                    CustomerId: id, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, CustomerLogo: $scope.customerlogo.filename,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    SalesRepresentativeId: $scope.salesrepresentativeid,
                    user: { UserName: $scope.userName, UserPassword: $scope.password }
                }
            }

            console.log('EditCustomer', config);

            Upload.upload({
                url: '/uploadImagetoLogoFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('response.data edit customer--', response.data);
                    if (response.data == "Ok") {
                        console.log('ZZZZZZZZZZZZZZZZZ--');
                        var url = '/Admin/ManageCustomer';
                        window.location = url;
                    }
                    else {
                        $scope.errorNot = response.data;
                    }
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            });
        };

        $scope.SessionAddCustomerInfo = function () {
            console.log('AddCustomer click');
            var config = {
                CustomerId: $scope.customer, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, CustomerLogo: $scope.customerlogo.filename,
                CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                user: { UserName: $scope.userName, UserPassword: $scope.password }
            }
            console.log('AddCustomer', config);
            return $http({
                url: '/Admin/SessionAddCustomerInfo',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    $scope.ShowAddCustomer = false;
                    $scope.ShowAddCustLocation = true;
                }
            }, function (error) {
                alert(error);
            });
        }

        $scope.SessionAddCustomerLocClick = function () {
            setTimeout(function () {
                angular.element('#SessionAddCustomerInfoClick').trigger('click');
            }, 0);
        };

        $scope.sendPassword = function (id) {
            console.log('sendPassword id', id);
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/sendPassword',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageCustomer';
                    window.location = url;
                }
                else {
                    $scope.errorNot = response.data;
                }
            }, function (error) {
                alert(error);
            });
        }

        if (window.location.pathname == "/Admin/EditCustomer" || window.location.pathname == "/Customer/ManageContacts" || window.location.pathname == "/Employee/CustomerLocationDetails" || window.location.pathname == "/Admin/ManageLocationContact") {
            var para = window.location.search;
            para = para.replace('?id=', '');
            $http.get('/api/pageview/getCustomerLocationByCustomerId', {
                params: { id: para }
            }).then(function (response) {
                $scope.getCustomerLocationByCustomerId = response.data;
                console.log('$scope.getCustomerLocationByCustomerId', $scope.getCustomerLocationByCustomerId);
                if ($scope.getCustomerLocationByCustomerId != null) { $scope.getCustomerLocationByCustomerIdcount = $scope.getCustomerLocationByCustomerId.length; }
                else { $scope.getCustomerLocationByCustomerIdcount = 0; }
                $scope.totalgetCustomerLocationByCustomerId = $scope.getCustomerLocationByCustomerIdcount;
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getCustomerFacilityByCustomerId', {
                params: { id: para }
            }).then(function (response) {
                $scope.facilitiesForAdd = response.data;               
            }, function (response) {
                $scope.waiting = false;
            });

            $http.get('/api/pageview/getCustomerAreaByCustomerId', {
                params: { id: para }
            }).then(function (response) {
                $scope.areasForAdd = response.data;
            }, function (response) {
                $scope.waiting = false;
            });
            
        }


        // ============================================================
        // ManageLocationContact Page
        // ============================================================

        if (window.location.pathname == "/Admin/ManageLocationContact" || window.location.pathname == "/Customer/ManageLocationContact") {

            var custId = window.location.search.replace('?id=', '');
            console.log(custId);
            // ---- Init ----
            $scope.AddContactShow = true;
            $scope.EditContactShow = false;
            $scope.facilitiesForAdd = [];
            $scope.areasForAdd = [];
            $scope.facilitiesForEdit = [];
            $scope.areasForEdit = [];
            $scope.allFacilities = [];
            $scope.allAreas = [];

            // ---- Load contact grid ----
            var loadContactGrid = function () {
                $http.get('/api/pageview/getLocationContactDetailsByCustomerId', {
                    params: { CustomerId: custId }
                }).then(function (response) {
                    $scope.GetContactByLocationId = response.data;
                    $scope.GetContactByLocationIdcount = $scope.GetContactByLocationId
                        ? $scope.GetContactByLocationId.length : 0;
                    $scope.totalGetContactByLocationId = $scope.GetContactByLocationIdcount;
                }, function () { $scope.waiting = false; });
            };
            loadContactGrid();

            // ---- Load location checklist ----
            $http.get('/api/pageview/getCustomerLocationByCustomerId', {
                params: { id: custId }
            }).then(function (response) {
                $scope.getCustomerLocationByCustomerId = response.data || [];
            }, function () { $scope.waiting = false; });

            // ---- Load all facilities for customer ----
            $http.get('/api/pageview/getCustomerFacilityByCustomerId', {
                params: { id: custId }
            }).then(function (response) {
                $scope.allFacilities = response.data || [];
            }, function () { $scope.waiting = false; });

            // ---- Load all areas for customer ----
            $http.get('/api/pageview/getCustomerAreaByCustomerId', {
                params: { id: custId }
            }).then(function (response) {
                $scope.allAreas = response.data || [];
            }, function () { $scope.waiting = false; });

            // ---- Checked ID helpers ----
            $scope.GetCheckedLocationIds = function () {
                var ids = [];
                ($scope.getCustomerLocationByCustomerId || []).forEach(function (f) {
                    if (f.selected) ids.push(f.CustomerLocationID);
                });
                $scope.checkedLocationIds = ids.join(',');
            };

            $scope.GetCheckedFacilityIds = function () {
                var ids = [];
                var source = $scope.EditContactShow ? $scope.facilitiesForEdit : $scope.facilitiesForAdd;
                (source || []).forEach(function (f) {
                    if (f.selected) ids.push(f.CustomerLocationID + '_' + f.CustomerFacilityID);
                });
                $scope.checkedFacilityIds = ids.join(',');
            };

            $scope.GetCheckedAreaIds = function () {
                var ids = [];
                var source = $scope.EditContactShow ? $scope.areasForEdit : $scope.areasForAdd;
                (source || []).forEach(function (a) {
                    if (a.selected) ids.push(a.CustomerLocationID + '_' + (a.CustomerFacilityID || 0) + '_' + a.AreaID);
                });
                $scope.checkedAreaIds = ids.join(',');
            };

            // ---- ADD form: location change ----
            $scope.onLocationChangeAdd = function () {
                var selectedLocIds = ($scope.getCustomerLocationByCustomerId || [])
                    .filter(function (l) { return l.selected; })
                    .map(function (l) { return l.CustomerLocationID; });

                $scope.facilitiesForAdd = $scope.allFacilities.filter(function (f) {
                    return selectedLocIds.indexOf(f.CustomerLocationID) !== -1;
                });
                $scope.facilitiesForAdd.forEach(function (f) { f.selected = false; });
                $scope.areasForAdd = [];
            };

            // ---- ADD form: facility change ----
            //$scope.onFacilityChangeAdd = function () {
            //    var selectedLocIds = ($scope.getCustomerLocationByCustomerId || [])
            //        .filter(function (l) { return l.selected; })
            //        .map(function (l) { return l.CustomerLocationID; });

            //    var selectedFacIds = $scope.facilitiesForAdd
            //        .filter(function (f) { return f.selected; })
            //        .map(function (f) { return f.CustomerFacilityID; });

            //    $scope.areasForAdd = $scope.allAreas.filter(function (a) {
            //        return selectedLocIds.indexOf(a.CustomerLocationID) !== -1
            //            && (a.CustomerFacilityID == 0 || a.CustomerFacilityID == null
            //                || selectedFacIds.indexOf(a.CustomerFacilityID) !== -1);
            //    });
            //    $scope.areasForAdd.forEach(function (a) { a.selected = false; });
            //};

            $scope.onFacilityChangeAdd = function () {
                var selectedLocIds =
                    ($scope.getCustomerLocationByCustomerId || [])
                        .filter(function (l) {
                            return l.selected;
                        })
                        .map(function (l) {
                            return l.CustomerLocationID;
                        });
                var selectedFacIds =
                    ($scope.facilitiesForAdd || [])
                        .filter(function (f) {
                            return f.selected;
                        })
                        .map(function (f) {
                            return f.CustomerFacilityID;
                        });
                $scope.areasForAdd =
                    ($scope.allAreas || [])
                        .filter(function (a) {

                            return selectedLocIds.indexOf(
                                a.CustomerLocationID
                            ) !== -1
                                &&
                                (
                                    a.CustomerFacilityID == 0 || a.CustomerFacilityID == null || selectedFacIds.indexOf(a.CustomerFacilityID) !== -1
                                );
                        });
                $scope.areasForAdd.forEach(
                    function (a) {
                        a.selected = false;
                    }
                );
            };

            // ---- EDIT form: location change ----
            $scope.onLocationChangeEdit = function () {
                var selectedLocIds = ($scope.getCustomerLocationByCustomerId || [])
                    .filter(function (l) { return l.selected; })
                    .map(function (l) { return l.CustomerLocationID; });

                $scope.facilitiesForEdit = $scope.allFacilities.filter(function (f) {
                    return selectedLocIds.indexOf(f.CustomerLocationID) !== -1;
                });
                $scope.facilitiesForEdit.forEach(function (f) {
                    if (!f.hasOwnProperty('selected')) f.selected = false;
                });

                var selectedFacIds = $scope.facilitiesForEdit
                    .filter(function (f) { return f.selected; })
                    .map(function (f) { return f.CustomerFacilityID; });

                $scope.areasForEdit = $scope.allAreas.filter(function (a) {
                    return selectedLocIds.indexOf(a.CustomerLocationID) !== -1
                        && (a.CustomerFacilityID == 0 || a.CustomerFacilityID == null
                            || selectedFacIds.indexOf(a.CustomerFacilityID) !== -1);
                });
                $scope.areasForEdit.forEach(function (a) {
                    if (!a.hasOwnProperty('selected')) a.selected = false;
                });
            };

            // ---- EDIT form: facility change ----
            $scope.onFacilityChangeEdit = function () {
                var selectedLocIds = ($scope.getCustomerLocationByCustomerId || [])
                    .filter(function (l) { return l.selected; })
                    .map(function (l) { return l.CustomerLocationID; });

                var selectedFacIds = $scope.facilitiesForEdit
                    .filter(function (f) { return f.selected; })
                    .map(function (f) { return f.CustomerFacilityID; });

                $scope.areasForEdit = $scope.allAreas.filter(function (a) {
                    return selectedLocIds.indexOf(a.CustomerLocationID) !== -1
                        && (a.CustomerFacilityID == 0 || a.CustomerFacilityID == null
                            || selectedFacIds.indexOf(a.CustomerFacilityID) !== -1);
                });
                $scope.areasForEdit.forEach(function (a) {
                    if (!a.hasOwnProperty('selected')) a.selected = false;
                });
            };

            // ---- Save new contact ----
            $scope.SaveLocationContact = function (id) {
                console.log('SaveLocationContact.............GET SELECTED VALUES FROM ADD FORM');
                // =========================================================
                // GET SELECTED VALUES FROM ADD FORM
                // =========================================================

                $scope.GetCheckedLocationIdsAdd();
                $scope.GetCheckedFacilityIdsAdd();
                $scope.GetCheckedAreaIdsAdd();


                // =========================================================
                // VALIDATION
                // =========================================================

                if (!$scope.contactName) {
                    $scope.validationShow = "Enter Contact Name.";
                    return;
                }

                if (!$scope.contactEmail) {
                    $scope.validationShow = "Enter Contact Email(UserID).";
                    return;
                }

                //if (!$scope.userPassword) {
                //    $scope.validationShow = "Enter Password.";
                //    return;
                //}

                if (!$scope.checkedLocationIds ||
                    $scope.checkedLocationIds.length == 0) {

                    $scope.validationShow =
                        "Select at least one Location.";

                    return;
                }


                // =========================================================
                // CONFIG
                // =========================================================

                var config = {
                    CustomerId: id,
                    ContactName: $scope.contactName,
                    ContactEmail: $scope.contactEmail,
                    ContactPhone: $scope.contactPhone,
                    UserName: $scope.contactEmail,
                    UserPassword: '',
                    LocationIds: $scope.checkedLocationIds,
                    FacilityIds: $scope.checkedFacilityIds,
                    AreaIds: $scope.checkedAreaIds
                };
                // =========================================================
                // API
                // =========================================================

                console.log('SaveLocationContact.............', config);

                //return;

                $http({
                    url: '/api/pageview/saveLocationContactMultiple',
                    method: 'POST',
                    data: config,
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken':
                            $scope.antiForgeryToken
                    }

                }).then(function (response) {
                    if (response.data == 'Ok') {
                        $window.location.reload();
                    } else {
                        $scope.validationShow = response.data;
                    }
                }, function (error) {
                    $scope.registermessage = error;
                });
            };

            $scope.GetCheckedLocationIdsAdd = function () {

                var ids = [];

                angular.forEach(
                    $scope.getCustomerLocationByCustomerId || [],
                    function (location) {

                        if (location.selected) {

                            ids.push(
                                location.CustomerLocationID
                            );
                        }
                    }
                );

                $scope.checkedLocationIds =
                    ids.join(',');
            };
            $scope.GetCheckedFacilityIdsAdd = function () {

                var ids = [];

                angular.forEach(
                    $scope.facilitiesForAdd || [],
                    function (facility) {

                        if (facility.selected) {

                            ids.push(
                                facility.CustomerLocationID +
                                '_' +
                                facility.CustomerFacilityID
                            );
                        }
                    }
                );

                $scope.checkedFacilityIds =
                    ids.join(',');
            };
            $scope.GetCheckedAreaIdsAdd = function () {

                var ids = [];

                angular.forEach(
                    $scope.areasForAdd || [],
                    function (area) {

                        if (area.selected) {

                            ids.push(
                                area.CustomerLocationID +
                                '_' +
                                (
                                    area.CustomerFacilityID ||
                                    0
                                ) +
                                '_' +
                                area.AreaID
                            );
                        }
                    }
                );

                $scope.checkedAreaIds =
                    ids.join(',');
            };
            //$scope.SaveLocationContact = function (id) {
            //    $scope.GetCheckedLocationIds();
            //    $scope.GetCheckedFacilityIds();
            //    $scope.GetCheckedAreaIds();

            //    if (!$scope.contactName) {
            //        $scope.validationShow = "Enter Contact Name."; return;
            //    }
            //    if (!$scope.contactEmail) {
            //        $scope.validationShow = "Enter Contact Email(UserID)."; return;
            //    }
            //    if (!$scope.userPassword) {
            //        $scope.validationShow = "Enter Password."; return;
            //    }
            //    if (!$scope.checkedLocationIds || $scope.checkedLocationIds.length == 0) {
            //        $scope.validationShow = "Select at least one Location."; return;
            //    }

            //    var config = {
            //        CustomerId: id,
            //        ContactName: $scope.contactName,
            //        ContactEmail: $scope.contactEmail,
            //        ContactPhone: $scope.contactPhone,
            //        UserName: $scope.contactEmail,
            //        UserPassword: $scope.userPassword,
            //        LocationIds: $scope.checkedLocationIds,
            //        FacilityIds: $scope.checkedFacilityIds,
            //        AreaIds: $scope.checkedAreaIds
            //    };

            //    $http({
            //        url: '/api/pageview/saveLocationContactMultiple',
            //        method: 'POST',
            //        data: config,
            //        headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': $scope.antiForgeryToken }
            //    }).then(function (response) {
            //        if (response.data == 'Ok') {
            //            $window.location.reload();
            //        } else {
            //            $scope.validationShow = response.data;
            //        }
            //    }, function (error) { $scope.registermessage = error; });
            //};

            // ---- Edit click - load contact into edit form ----
            $scope.EditContactClick = function (Id) {
                $scope.AddContactShow = false;
                $scope.EditContactShow = true;

                $http.get('/api/pageview/getLocationContactUserDetailsById', { params: { id: Id } })
                    .then(function (response) {
                        var d = response.data;

                        $scope.contactnameInEdit = d.ContactName;
                        $scope.locationContactIdInEdit = d.LocationContactId;
                        $scope.contactEmailInEdit = d.ContactEmail;
                        $scope.contactPhoneInEdit = d.ContactPhone;
                        $scope.customerid = d.CustomerId;
                        $scope.useridEdit = d.UserID;
                        $scope.userPasswordInEdit = d.UserPassword;
                        $scope.LinkedCustomerUserLocationIds = d.LinkedCustomerUserLocationIds;
                        $scope.ContactIDInEditContact = d.LocationContactId;

                        var linkedLocIds = d.LinkedCustomerLocationIDs || [];
                        var linkedFacIds = d.LinkedFacilityIDs || [];
                        var linkedAreaIds = d.LinkedAreaIDs || [];

                        // Pre-check locations
                        ($scope.getCustomerLocationByCustomerId || []).forEach(function (f) {
                            f.selected = linkedLocIds.indexOf(f.CustomerLocationID) !== -1;
                        });

                        // Load and pre-check facilities
                        $scope.facilitiesForEdit = $scope.allFacilities.filter(function (f) {
                            return linkedLocIds.indexOf(f.CustomerLocationID) !== -1;
                        });
                        $scope.facilitiesForEdit.forEach(function (f) {
                            f.selected = linkedFacIds.indexOf(f.CustomerFacilityID) !== -1;
                        });

                        // Load and pre-check areas
                        $scope.areasForEdit = $scope.allAreas.filter(function (a) {
                            return linkedLocIds.indexOf(a.CustomerLocationID) !== -1;
                        });
                        $scope.areasForEdit.forEach(function (a) {
                            a.selected = linkedAreaIds.indexOf(a.AreaID) !== -1;
                        });

                    }, function () { $scope.waiting = false; });
            };

            // ---- Update existing contact ----
            $scope.EditLocationContact = function (Id) {
                $scope.GetCheckedLocationIds();
                $scope.GetCheckedFacilityIds();
                $scope.GetCheckedAreaIds();

                if (!$scope.contactnameInEdit) {
                    $scope.validationShow = "Enter Contact Name."; return;
                }
                if (!$scope.contactEmailInEdit) {
                    $scope.validationShow = "Enter Contact Email(UserID)."; return;
                }
                if (!$scope.checkedLocationIds || $scope.checkedLocationIds.length == 0) {
                    $scope.validationShow = "Select at least one Location."; return;
                }

                var config = {
                    LocationContactId: Id,
                    ContactName: $scope.contactnameInEdit,
                    ContactEmail: $scope.contactEmailInEdit,
                    ContactPhone: $scope.contactPhoneInEdit,
                    LocationIds: $scope.checkedLocationIds,
                    FacilityIds: $scope.checkedFacilityIds,
                    AreaIds: $scope.checkedAreaIds,
                    UserPassword: $scope.userPasswordInEdit,
                    CustomerId: $scope.customerid,
                    UserID: $scope.useridEdit,
                    UserName: $scope.contactEmailInEdit
                };

                $http({
                    url: '/api/pageview/editLocationContactMultiple',
                    method: 'POST',
                    data: config,
                    headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': $scope.antiForgeryToken }
                }).then(function (response) {
                    if (response.data == 'Ok') {
                        $window.location.reload();
                    } else {
                        $scope.validationShow = response.data;
                    }
                }, function (error) { $scope.registermessage = error; });
            };

            // ---- Delete contact ----
            $scope.DeleteContactClick = function (LocationContactId) {
                if (!confirm('Are you sure you want to delete this contact?')) return;
                $http({
                    url: '/api/pageview/removeLocationContact',
                    method: 'POST',
                    params: { id: LocationContactId },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (response) {
                    if (response.data != '0') {
                        loadContactGrid();
                    } else {
                        alert('Failed to delete contact.');
                    }
                }, function () { alert('An error occurred.'); });
            };

            // ---- Cancel edit ----
            $scope.CancelEdit = function () {
                $scope.AddContactShow = true;
                $scope.EditContactShow = false;
                $scope.facilitiesForEdit = [];
                $scope.areasForEdit = [];
                $scope.facilitiesForAdd = [];
                $scope.areasForAdd = [];
                ($scope.getCustomerLocationByCustomerId || []).forEach(function (l) { l.selected = false; });
                $scope.validationShow = '';
            };
        }
        // ============================================================
        // END ManageLocationContact Page
        // ============================================================

        if (window.location.pathname == "/Customer/ManageContacts") {
            $http.get('/api/pageview/getCustomerLocationByUserId').then(function (response) {
                $scope.getCustomerLocationByUserId = response.data;
                console.log('$scope.getCustomerLocationByUserId', $scope.getCustomerLocationByUserId);
                if ($scope.getCustomerLocationByUserId != null) { $scope.getCustomerLocationByUserIdcount = $scope.getCustomerLocationByUserId.length; }
                else { $scope.getCustomerLocationByUserIdcount = 0; }
                $scope.totalgetCustomerLocationByUserId = $scope.getCustomerLocationByUserIdcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.RemoveCustomer = function (id) {
            console.log('delete id', id);
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeCustomer',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageCustomer';
                    window.location = url;
                }
            }, function (error) {

            });
        }

        $scope.AddCustLocation = function (id) {
            var config = {
                CustomerId: id, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, LocationName: $scope.locationname
            }
            console.log('AddCustLocation', config);
            return $http({
                url: '/Admin/SessionAddCustomerLocInfo',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response 1--', response);
                if (response.data === "Ok") {
                    console.log('response.data2--', response.data);
                    console.log('response.config.data3--', response.config.data);
                    $scope.ShowAddCustLocation = false;
                    $scope.ShowAddCustomer = true;
                    $http.get('/Admin/GetSessionAddCustomerLocInfo').then(function (response) {
                        $scope.getCustomerLocationByCustomerIdAdd = response.data;
                        console.log('$scope.getCustomerLocationByCustomerIdAdd', $scope.getCustomerLocationByCustomerIdAdd);
                        if ($scope.getCustomerLocationByCustomerIdAdd != null) { $scope.getCustomerLocationByCustomerIdAddcount = $scope.getCustomerLocationByCustomerIdAdd.length; }
                        else { $scope.getCustomerLocationByCustomerIdAddcount = 0; }
                        $scope.totalgetCustomerLocationByCustomerIdAdd = $scope.getCustomerLocationByCustomerIdAddcount;
                    }, function (response) {
                        $scope.waiting = false;
                    });
                    //$scope.ShowCustLocationTable = true;
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.AddCustLocationFromEditCust = function () {
            var config = {
                CustomerId: $scope.customer, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, LocationName: $scope.locationname, Region: $scope.region
            }
            console.log('AddCustLocation', config);
            return $http({
                url: '/api/pageview/saveCustomerLocation',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response 1--', response);
                if (response.data != null) {
                    console.log('response.data2--', response.data);
                    var url = '/Admin/EditCustomer?id=' + response.data;
                    window.location = url;
                }
                else {
                    //var url = '/Account/Login';
                    //window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.EditCustomerLocation = function () {
            var config = {
                CustomerLocationID: $scope.customerlocationID, CustomerId: $scope.customerid, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pincode, LocationName: $scope.locationname, Region: $scope.region
            }
            console.log('AddCustLocation', config);
            //return false;

            return $http({
                url: '/api/pageview/editCustomerLocation',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response', response);
                if (response.data != null) {
                    console.log('response.data2--', response.data);
                    var url = '/Admin/EditCustomer?id=' + response.data;
                    window.location = url;
                }
                else {
                    //var url = '/Account/Login';
                    //window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.RemoveCustomerLocation = function (id) {
            console.log('delete id', id);
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeCustomerLocation',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageCustomer';
                    window.location = url;
                }
            }, function (error) {

            });
        }

        $scope.AddAreaShow = true;
        $scope.EditAreaShow = false;

        $scope.SaveCustomerArea = function (id) {
            var config = {
                CustomerLocationID: id, AreaName: $scope.areaname
            }
            console.log('SaveArea', config);
            return $http({
                url: '/api/pageview/saveCustomerArea',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response SaveArea--', response);
                if (response.data != null) {
                    $scope.AreaTableInAddArea = true;
                    $window.location.reload();
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.EditAreaClick = function (Id) {
            $scope.AddAreaShow = false;
            $scope.EditAreaShow = true;
            $http.get('/api/pageview/getAreaDetailsById', { params: { id: Id } }).then(function (response) {
                $scope.getAreaDetailsById = response.data;
                $scope.AreaNameInEditArea = $scope.getAreaDetailsById.AreaName;
                $scope.AreaIDInEditArea = $scope.getAreaDetailsById.AreaID;
                console.log('getAreaDetailsById--', $scope.getAreaDetailsById);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.EditCustomerArea = function (Id) {
            console.log('edit area save--', Id);
            var config = {
                AreaID: Id, AreaName: $scope.areaname
            }
            console.log('SaveArea', config);
            return $http({
                url: '/api/pageview/editCustomerArea',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response SaveArea--', response);
                if (response.data != null) {
                    $window.location.reload();
                    $scope.AddAreaShow = true;
                    $scope.EditAreaShow = false;
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        }

        $scope.RemoveCustomerArea = function (id) {
            console.log('removeCustomerArea id', id);
            var config = { id: id }
            console.log('removeCustomerArea config--', config);
            return $http({
                url: '/api/pageview/removeCustomerArea',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data != 0) {
                    var url = '/Admin/AddCustomerArea?id=' + response.data;
                    window.location = url;
                }
            }, function (error) {

            });
        }

        $scope.AddFacilityShow = true;
        $scope.EditFacilityShow = false;

        $scope.SaveCustomerFacility = function (id) {
            var config = {
                CustomerLocationID: id, FacilityName: $scope.FacilityName
            }
            console.log('SaveFacility', config);
            return $http({
                url: '/api/pageview/saveCustomerFacility',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response SaveFacility--', response);
                if (response.data != null) {
                    $scope.FacilityTableInAddFacility = true;
                    $window.location.reload();
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.EditFacilityClick = function (Id) {
            $scope.AddFacilityShow = false;
            $scope.EditFacilityShow = true;
            console.log('getFacilityDetailsById - id', Id);
            $http.get('/api/pageview/getFacilityDetailsById', { params: { id: Id } }).then(function (response) {
                $scope.getFacilityDetailsById = response.data;
                $scope.FacilityNameInEditFacility = $scope.getFacilityDetailsById.FacilityName;
                $scope.FacilityIDInEditFacility = $scope.getFacilityDetailsById.CustomerFacilityID;
                console.log('getFacilityDetailsById--', $scope.getFacilityDetailsById);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.EditCustomerFacility = function (Id) {
            console.log('edit Facility save--', Id);
            var config = {
                CustomerFacilityID: Id, FacilityName: $scope.FacilityName
            }
            console.log('SaveFacility', config);
            return $http({
                url: '/api/pageview/editCustomerFacility',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response SaveFacility--', response);
                if (response.data != null) {
                    $window.location.reload();
                    $scope.AddFacilityShow = true;
                    $scope.EditFacilityShow = false;
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        }

        $scope.RemoveCustomerFacility = function (id) {
            if (!confirm("Are you sure you want to remove this facility?")) {
                return;
            }
            console.log('removeCustomerFacility id', id);

            var config = { id: id };

            return $http({
                url: '/api/pageview/removeCustomerFacility',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {

                if (response.data != 0) {
                    var url = '/Admin/ManageCustomerFacility?id=' + response.data;
                    window.location = url;
                }

            }, function (error) {
                console.log(error);
            });
        };




        if (window.location.pathname == "/Admin/AddCustomerArea" || window.location.pathname == "/Employee/CustomerAreaDetails") {
            var addCustURL = window.location.search;
            addCustURL = addCustURL.replace('?id=', '');
            console.log('url add customer area--', addCustURL);
            $http.get('/api/pageview/getAreaDetailsByLocationId', {
                params: { id: addCustURL }
            }).then(function (response) {
                $scope.GetAreaByLocationId = response.data;
                console.log('$scope.GetAreaByLocationId', $scope.GetAreaByLocationId);
                if ($scope.GetAreaByLocationId != null) { $scope.GetAreaByLocationIdcount = $scope.GetAreaByLocationId.length; }
                else { $scope.GetAreaByLocationIdcount = 0; }
                $scope.totalGetAreaByLocationId = $scope.GetAreaByLocationIdcount;
                $scope.currentPage = '1';
            }, function (response) {
                $scope.waiting = false;
            });
        }

        if (window.location.pathname == "/Admin/ManageCustomerFacility") {
            var addCustURL = window.location.search;
            addCustURL = addCustURL.replace('?id=', '');
            console.log('url add customer area--', addCustURL);
            $http.get('/api/pageview/getFacilityDetailsByLocationId', {
                params: { id: addCustURL }
            }).then(function (response) {
                $scope.GetFacilityByLocationId = response.data;
                console.log('$scope.getFacilityDetailsByLocationId', $scope.GetFacilityByLocationId);
                if ($scope.GetFacilityByLocationId != null) { $scope.GetFacilityByLocationIdcount = $scope.GetFacilityByLocationId.length; }
                else { $scope.GetFacilityByLocationIdcount = 0; }
                $scope.totalGetFacilityByLocationId = $scope.GetFacilityByLocationIdcount;
                $scope.currentPage = '1';
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.GetCheckedLocationIds = function () {
            var CustomerLocationID = '';
            $scope.getCustomerLocationByCustomerId.forEach(function (f) {
                if (f.selected) {
                    if (CustomerLocationID != '') {
                        CustomerLocationID += ",";
                    }
                    CustomerLocationID += f.CustomerLocationID;
                }
            });
            $scope.checkedLocationIds = CustomerLocationID;
        }

        $scope.GetCheckedFacilityIds = function () {
            $scope.checkedFacilityIds = '';
            var ids = [];
            angular.forEach($scope.facilitiesForEdit, function (f) {
                if (f.selected) {
                    ids.push(f.CustomerLocationID + '_' + f.CustomerFacilityID);
                }
            });
            $scope.checkedFacilityIds = ids.join(',');
        };

        $scope.GetCheckedAreaIds = function () {
            $scope.checkedAreaIds = '';
            var ids = [];
            angular.forEach($scope.areasForEdit, function (a) {
                if (a.selected) {
                    ids.push(a.CustomerLocationID + '_' + (a.CustomerFacilityID || 0) + '_' + a.AreaID);
                }
            });
            $scope.checkedAreaIds = ids.join(',');
        };

        //$scope.AddContactShow = true;
        //$scope.EditContactShow = false;

        //$scope.SaveLocationContact = function (id) {
        //    $scope.GetCheckedLocationIds();
        //    $scope.GetCheckedFacilityIds();
        //    $scope.GetCheckedAreaIds();

        //    if ($scope.contactName == null) {
        //        $scope.validationShow = "Enter Contact Name."
        //        return "Enter Contact Name.";
        //    }
        //    else if ($scope.contactEmail == null) {
        //        $scope.validationShow = "Enter Contact Email(UserID)."
        //        return "Enter Contact Email(UserID).";
        //    }
        //    else if ($scope.userPassword == null) {
        //        $scope.validationShow = "Enter Password."
        //        return "Enter Password.";
        //    }
        //    else if ($scope.checkedLocationIds.length == 0) {
        //        $scope.validationShow = "Select atleast one Location.";
        //        return "Select atleast one Location.";
        //    }
        //    else {
        //        var config = {
        //            CustomerId: id,
        //            ContactName: $scope.contactName,
        //            ContactEmail: $scope.contactEmail,
        //            ContactPhone: $scope.contactPhone,
        //            UserName: $scope.contactEmail,
        //            UserPassword: $scope.userPassword,
        //            LocationIds: $scope.checkedLocationIds,
        //            FacilityIds: $scope.checkedFacilityIds,
        //            AreaIds: $scope.checkedAreaIds
        //        }
        //        console.log('SaveContact', config);
        //        return $http({
        //            url: '/api/pageview/saveLocationContactMultiple',
        //            method: "POST",
        //            data: config,
        //            headers: {
        //                "Content-Type": "application/json",
        //                'RequestVerificationToken': $scope.antiForgeryToken
        //            }
        //        }).then(function (response) {
        //            console.log('response SaveContact--', response);
        //            if (response.data != "Ok") {
        //                console.log('response.data SaveContact--', response.data);
        //                $scope.validationShow = response.data;
        //                $scope.ContactTableInAddContact = true;
        //            }
        //            else {
        //                $window.location.reload();
        //            }
        //        }, function (error) {
        //            $scope.registermessage = error;
        //        });
        //    }
        //};

        //$scope.EditContactClick = function (Id) {
        //    $scope.AddContactShow = false;
        //    $scope.EditContactShow = true;
        //    $http.get('/api/pageview/getLocationContactUserDetailsById', { params: { id: Id } }).then(function (response) {
        //        $scope.GetLocationContactDetailsById = response.data;
        //        console.log('$scope.GetLocationContactDetailsById ----------------XXX', $scope.GetLocationContactDetailsById);
        //        $scope.contactnameInEdit = $scope.GetLocationContactDetailsById.ContactName;
        //        $scope.locationContactIdInEdit = $scope.GetLocationContactDetailsById.LocationContactId;
        //        $scope.contactEmailInEdit = $scope.GetLocationContactDetailsById.ContactEmail;
        //        $scope.contactPhoneInEdit = $scope.GetLocationContactDetailsById.ContactPhone;
        //        $scope.customerid = $scope.GetLocationContactDetailsById.CustomerId;
        //        $scope.useridEdit = $scope.GetLocationContactDetailsById.UserID;
        //        $scope.userPasswordInEdit = $scope.GetLocationContactDetailsById.UserPassword;
        //        $scope.LinkedCustomerUserLocationIds = $scope.GetLocationContactDetailsById.LinkedCustomerUserLocationIds;
        //        var Userlocations = $scope.GetLocationContactDetailsById.LinkedCustomerLocationIDs || [];

        //        console.log('$scope.LinkedCustomerUserLocationIds--------------', $scope.LinkedCustomerLocationIDs);
        //        console.log('$scope.getCustomerLocationByCustomerId----------------Before---------------------', $scope.getCustomerLocationByCustomerId);
        //        $scope.getCustomerLocationByCustomerId.forEach(function (f) {
        //            console.log('selected location id :', f.CustomerLocationID);
        //            f.selected = Userlocations.includes(f.CustomerLocationID);
        //        });
        //        console.log('$scope.getCustomerLocationByCustomerId-------------AFter-----------------', $scope.getCustomerLocationByCustomerId);
        //        $scope.ContactIDInEditContact = $scope.GetLocationContactDetailsById.LocationContactId;
        //        console.log('GetLocationContactDetailsById--', $scope.GetLocationContactDetailsById);
        //    }, function (response) {
        //        $scope.waiting = false;
        //    });
        //}

        $scope.CancelEdit = function () {            
            $scope.AddContactShow = true;
            $scope.EditContactShow = false;
            $scope.facilitiesForEdit = [];
            $scope.areasForEdit = [];            
            $scope.getCustomerLocationByCustomerId.forEach(function (l) { l.selected = false; });
            $scope.facilitiesForAdd = [];
            $scope.areasForAdd = [];
        };

        $scope.EditContactClick = function (Id) {
            $scope.AddContactShow = false;
            $scope.EditContactShow = true;

            $http.get('/api/pageview/getLocationContactUserDetailsById', { params: { id: Id } }).then(function (response) {
                $scope.GetLocationContactDetailsById = response.data;

                $scope.contactnameInEdit = $scope.GetLocationContactDetailsById.ContactName;
                $scope.locationContactIdInEdit = $scope.GetLocationContactDetailsById.LocationContactId;
                $scope.contactEmailInEdit = $scope.GetLocationContactDetailsById.ContactEmail;
                $scope.contactPhoneInEdit = $scope.GetLocationContactDetailsById.ContactPhone;
                $scope.customerid = $scope.GetLocationContactDetailsById.CustomerId;
                $scope.useridEdit = $scope.GetLocationContactDetailsById.UserID;
                $scope.userPasswordInEdit = $scope.GetLocationContactDetailsById.UserPassword;
                $scope.LinkedCustomerUserLocationIds = $scope.GetLocationContactDetailsById.LinkedCustomerUserLocationIds;
                $scope.ContactIDInEditContact = $scope.GetLocationContactDetailsById.LocationContactId;

                var linkedLocIds = $scope.GetLocationContactDetailsById.LinkedCustomerLocationIDs || [];
                var linkedFacIds = $scope.GetLocationContactDetailsById.LinkedFacilityIDs || [];
                var linkedAreaIds = $scope.GetLocationContactDetailsById.LinkedAreaIDs || [];

                // ---- Pre-check Locations ----
                $scope.getCustomerLocationByCustomerId.forEach(function (f) {
                    f.selected = linkedLocIds.includes(f.CustomerLocationID);
                });

                // ---- Load and pre-check Facilities for selected locations ----
                $scope.facilitiesForEdit = $scope.allFacilities.filter(function (f) {
                    return linkedLocIds.indexOf(f.CustomerLocationID) !== -1;
                });
                $scope.facilitiesForEdit.forEach(function (f) {
                    f.selected = linkedFacIds.includes(f.CustomerFacilityID);
                });

                // ---- Load and pre-check Areas for selected locations ----
                $scope.areasForEdit = $scope.allAreas.filter(function (a) {
                    return linkedLocIds.indexOf(a.CustomerLocationID) !== -1;
                });
                $scope.areasForEdit.forEach(function (a) {
                    a.selected = linkedAreaIds.includes(a.AreaID);
                });

            }, function (response) {
                $scope.waiting = false;
            });
        };

        $scope.DeleteContactClick = function (LocationContactId) {
            if (confirm("Are you sure you want to delete this contact?")) {
                window.location.href = "/Admin/DeleteLocationContact?id=" + LocationContactId;
            }
        };

        $scope.EditLocationContact = function (Id) {
            console.log('edit Contact save--', Id);

            $scope.GetCheckedLocationIds();
            $scope.GetCheckedFacilityIds();
            $scope.GetCheckedAreaIds();
            if ($scope.contactnameInEdit == null) {
                $scope.validationShow = "Enter Contact Name."
                return "Enter Contact Name.";
            }
            else if ($scope.contactEmailInEdit == null) {
                $scope.validationShow = "Enter Contact Email(UserID)."
                return "Enter Contact Email(UserID).";
            }
            else if ($scope.checkedLocationIds.length == 0) {
                $scope.validationShow = "Select atleast one Location.";
                return "Select atleast one Location.";
            }
            else {                
                var config = {
                    LocationContactId: Id,
                    ContactName: $scope.contactnameInEdit,
                    ContactEmail: $scope.contactEmailInEdit,
                    ContactPhone: $scope.contactPhoneInEdit,
                    LocationIds: $scope.checkedLocationIds,
                    FacilityIds: $scope.checkedFacilityIds,
                    AreaIds: $scope.checkedAreaIds,
                    UserPassword: $scope.userPasswordInEdit,
                    CustomerId: $scope.customerid,
                    UserID: $scope.useridEdit,
                    UserName: $scope.contactEmailInEdit
                }
                console.log('EditLocationContact', config);

                return $http({
                    url: '/api/pageview/editLocationContactMultiple',
                    method: "POST",
                    data: config,
                    headers: {
                        "Content-Type": "application/json",
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (response) {
                    console.log('response SaveContact--', response);
                    if (response.data != null) {
                        if (response.data == "Ok") {
                            $window.location.reload();
                            $scope.AddContactShow = true;
                            $scope.EditContactShow = false;
                        }
                        else {
                            $scope.validationShow = response.data;
                        }
                    }
                    else {
                        var url = '/Account/Login';
                        window.location = url;
                    }
                }, function (error) {
                    $scope.registermessage = error;
                });
            }
        }

        $scope.RemoveLocationContact = function (id) {
            console.log('removeLocationContact id', id);
            var config = { id: id }
            console.log('removeLocationContact config--', config);
            return $http({
                url: '/api/pageview/removeLocationContact',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data != 0) {
                    var url = '/Admin/ManageLocationContact?id=' + response.data;
                    window.location = url;
                }
            }, function (error) {

            });
        }

        if (window.location.pathname == "/Admin/ManageLocationContact" || window.location.pathname == "/Customer/ManageLocationContact") {
            var custId = window.location.search;
            custId = custId.replace('?id=', '');
            console.log('url add customer contact-- getLocationContactDetailsByCustomerId ---', custId);

            $http.get('/api/pageview/getLocationContactDetailsByCustomerId', {
                params: { CustomerId: custId }
            }).then(function (response) {
                $scope.GetContactByLocationId = response.data;
                console.log('$scope.GetContactByLocationId', $scope.GetContactByLocationId);
                if ($scope.GetContactByLocationId != null) { $scope.GetContactByLocationIdcount = $scope.GetContactByLocationId.length; }
                else { $scope.GetContactByLocationIdcount = 0; }
                $scope.totalGetContactByLocationId = $scope.GetContactByLocationIdcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }



        $scope.EditCustomerPasswordByAdmin = function (id) {
            var config = {
                UserId: id, UserName: $scope.userName, UserPassword: $scope.password
            }
            console.log('EditCustomerPasswordByAdmin', config);
            return $http({
                url: '/api/pageview/editCustomerPasswordByAdmin',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response', response);
                if (response.data == "Ok") {
                    var url = '/Admin/ManageCustomer';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.checkPasswords = function () {
            if (angular.equals($scope.password, $scope.confirmPassword)) { $scope.matchPswd = ""; }
            else { $scope.matchPswd = "The password and confirm password do not match."; }
        }
    }
})();
