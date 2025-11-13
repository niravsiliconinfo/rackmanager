(function () {
    'use strict';

    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    //app.controller('customersectionCtrl', customersectionCtrl);
    angular.module('myApp')
        .controller('customersectionCtrl', customersectionCtrl);

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

    customersectionCtrl.$inject = ['$scope', '$http', '$timeout', 'Upload', '$window'];

    function customersectionCtrl($scope, $http, $timeout, Upload, $window) {

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
            $http.get('/api/pageview/getAllCustomerLocationByCustomerId', { params: { id: para } }).then(function (response) {
                $scope.getAllCustomerLocationByCustomerId = response.data;
                console.log('getCustomerLocationByCustomerId--', $scope.getAllCustomerLocationByCustomerId);
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
                    CustomerId: $scope.customer, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    user: { UserName: $scope.userName, UserPassword: $scope.password }
                }
            }
            else {
                console.log('hhhiiiii');
                var config = {
                    CustomerId: $scope.customer, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, CustomerLogo: $scope.customerlogo.filename,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
                    user: { UserName: $scope.userName, UserPassword: $scope.password }
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
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname
                }
            }
            else {
                console.log('3333-');
                var config = {
                    CustomerId: id, CustomerName: $scope.customername, CustomerAddress: $scope.customeraddress, CityID: $scope.city, CountryID: $scope.country,
                    ProvinceID: $scope.province, Pincode: $scope.pin, CustomerNAVNo: $scope.customerNAVNo, CustomerLogo: $scope.customerlogo.filename,
                    CustomerPhone: $scope.customerPhone, CustomerEmail: $scope.customerEmail, CustomerWebsite: $scope.customerWebsite, CustomerContactName: $scope.customercontactname,
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

        if (window.location.pathname == "/Admin/EditCustomer" || window.location.pathname == "/Customer/ManageContacts" || window.location.pathname == "/Employee/CustomerLocationDetails" || window.location.pathname == "/Admin/AddLocationContact") {
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
        }

        if (window.location.pathname == "/Customer/ManageAllUserContacts") {
            $http.get('/api/pageview/getCustomerLocationByCustomerIdCustomer').then(function (response) {
                $scope.getCustomerLocationByCustomerId = response.data;
                console.log('$scope.getCustomerLocationByCustomerId', $scope.getCustomerLocationByCustomerId);
                if ($scope.getCustomerLocationByCustomerId != null) { $scope.getCustomerLocationByCustomerIdcount = $scope.getCustomerLocationByCustomerId.length; }
                else { $scope.getCustomerLocationByCustomerIdcount = 0; }
                $scope.totalgetCustomerLocationByCustomerId = $scope.getCustomerLocationByCustomerIdcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }


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

        $scope.AddContactShow = true;
        $scope.EditContactShow = false;
        $scope.SaveLocationContact = function (id) {
            $scope.GetCheckedLocationIds();

            if ($scope.contactName == null) {
                $scope.validationShow = "Enter Contact Name."
                return "Enter Contact Name.";
            }
            else if ($scope.contactEmail == null) {
                $scope.validationShow = "Enter Contact Email(UserID)."
                return "Enter Contact Email(UserID).";
            }
            else if ($scope.userPassword == null) {
                $scope.validationShow = "Enter Password."
                return "Enter Password.";
            }
            else if ($scope.checkedLocationIds.length == 0) {
                $scope.validationShow = "Select atleast one Location.";
                return "Select atleast one Location.";
            }
            else {

                var config = {
                    CustomerId: id, ContactName: $scope.contactName, ContactEmail: $scope.contactEmail, ContactPhone: $scope.contactPhone,
                    UserName: $scope.contactEmail, UserPassword: $scope.userPassword, LocationIds: $scope.checkedLocationIds
                }
                console.log('SaveContact', config);
                return $http({
                    url: '/api/pageview/saveLocationContactMultiple',
                    method: "POST",
                    data: config,
                    headers: {
                        "Content-Type": "application/json",
                        'RequestVerificationToken': $scope.antiForgeryToken
                    }
                }).then(function (response) {
                    console.log('response SaveContact--', response);
                    if (response.data != "Ok") {
                        console.log('response.data SaveContact--', response.data);
                        $scope.validationShow = response.data;
                        $scope.ContactTableInAddContact = true;
                    }
                    else {
                        $window.location.reload();
                    }
                }, function (error) {
                    $scope.registermessage = error;
                });
            }
        };

        $scope.EditContactClick = function (Id) {
            $scope.AddContactShow = false;
            $scope.EditContactShow = true;
            $http.get('/api/pageview/getLocationContactUserDetailsById', { params: { id: Id } }).then(function (response) {
                $scope.GetLocationContactDetailsById = response.data;
                console.log('$scope.GetLocationContactDetailsById ----------------XXX', $scope.GetLocationContactDetailsById);
                $scope.contactnameInEdit = $scope.GetLocationContactDetailsById.ContactName;
                $scope.locationContactIdInEdit = $scope.GetLocationContactDetailsById.LocationContactId;
                $scope.contactEmailInEdit = $scope.GetLocationContactDetailsById.ContactEmail;
                $scope.contactPhoneInEdit = $scope.GetLocationContactDetailsById.ContactPhone;
                $scope.customerid = $scope.GetLocationContactDetailsById.CustomerId;
                $scope.useridEdit = $scope.GetLocationContactDetailsById.UserID;
                $scope.userPasswordInEdit = $scope.GetLocationContactDetailsById.UserPassword;
                $scope.LinkedCustomerUserLocationIds = $scope.GetLocationContactDetailsById.LinkedCustomerUserLocationIds;
                var Userlocations = $scope.GetLocationContactDetailsById.LinkedCustomerLocationIDs || [];

                console.log('$scope.LinkedCustomerUserLocationIds--------------', $scope.LinkedCustomerLocationIDs);
                console.log('$scope.getCustomerLocationByCustomerId----------------Before---------------------', $scope.getCustomerLocationByCustomerId);
                $scope.getCustomerLocationByCustomerId.forEach(function (f) {
                    console.log('selected location id :', f.CustomerLocationID);
                    f.selected = Userlocations.includes(f.CustomerLocationID);
                });
                console.log('$scope.getCustomerLocationByCustomerId-------------AFter-----------------', $scope.getCustomerLocationByCustomerId);
                $scope.ContactIDInEditContact = $scope.GetLocationContactDetailsById.LocationContactId;
                console.log('GetLocationContactDetailsById--', $scope.GetLocationContactDetailsById);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.DeleteContactClick = function (LocationContactId) {
            if (confirm("Are you sure you want to delete this contact?")) {
                window.location.href = "/Admin/DeleteLocationContact?id=" + LocationContactId;
            }
        };

        $scope.EditLocationContact = function (Id) {
            console.log('edit Contact save--', Id);
            $scope.validationShow = '';
            $scope.GetCheckedLocationIds();

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
                    LocationContactId: Id, ContactName: $scope.contactnameInEdit, ContactEmail: $scope.contactEmailInEdit, ContactPhone: $scope.contactPhoneInEdit,
                    LocationIds: $scope.checkedLocationIds, UserPassword: $scope.userPasswordInEdit, CustomerId: $scope.customerid, UserID: $scope.useridEdit, UserName: $scope.contactEmailInEdit
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
                    console.log('response SaveContact validationShow--', response);
                    if (response.data != null) {
                        if (response.data == "Ok") {
                            $window.location.reload();
                            $scope.AddContactShow = true;
                            $scope.EditContactShow = false;
                        }
                        else
                        {
                            $scope.validationShow = response.data;
                        }                        
                        //alert(response.data);                        
                    }
                    else {
                        $scope.validationShow = 'Some Error occured. Please try again.';
                        //var url = '/Account/Login';
                        //window.location = url;
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
                    var url = '/Admin/AddLocationContact?id=' + response.data;
                    window.location = url;
                }
            }, function (error) {

            });
        }

        if (window.location.pathname == "/Admin/AddLocationContact" || window.location.pathname == "/Customer/AddLocationContact" || window.location.pathname == "/Customer/ManageAllUserContacts") {
            var custId = window.location.search;
            custId = custId.replace('?id=', '');
            console.log('url add customer contact--', custId);

            $http.get('/api/pageview/getLocationContactDetailsByLocationIdCustomer').then(function (response) {
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
