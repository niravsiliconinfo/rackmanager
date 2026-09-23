(function () {
    'use strict';

    //var app = angular.module('myApp', []);
    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    angular.module('myApp')
        .controller('profileCtrl', profileCtrl);


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

    app.controller('profileCtrl', profileCtrl);

    profileCtrl.$inject = ['$scope', '$http', '$filter', '$window', '$timeout', 'Upload', '$document', '$location', '$interval'];

    function profileCtrl($scope, $http, $filter, $window, $timeout, Upload, $document, $location, $interval, $rootScope) {
        console.log('XXXXXXXXXXXXXXXXXXXXXXXXXXXX-------In /Admin/Index--------------------------------------------------');        

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

        $scope.UserEmployeeById = function (Id) {
            $http.get('/api/pageview/getUserEmployeeById', { params: { id: Id } }).then(function (response) {
                $scope.getUserEmployeeById = response.data;
                console.log('getUserEmployeeById--', $scope.getUserEmployeeById);
            }, function (response) {
                $scope.waiting = false;
            });
        };
       
        $scope.EmpEditMyProfile = function (id) {
            var config = {
                UserName: $scope.username, UserPassword: $scope.password, Active: $scope.Active, EmployeeName: $scope.employeename,
                EmployeeEmail: $scope.email, EmployeeAddress: $scope.address, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, Gender: $scope.gender, TitleDegrees: $scope.titledegree, MobileNo: $scope.mobileNo,
                UserType: $scope.usertype, IsProfileEdit : true,UserId: id
            }
            console.log('edituseremployee', config);
            return $http({
                url: '/Account/EmpEditMyProfile',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                $scope.registermessage = response.data;
                if (response.data.message === "Ok" || response.data === "Ok") {
                    console.log('response.data--', response.data);
                    var url = '/Employee/Index';
                    window.location = url;
                }
                else {
                    $scope.registermessage = response.data;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.EditPassword = function (id) {
            var config = {
                UserId: id, UserName: $scope.userName, UserPassword: $scope.password
            }
            if ($scope.password != null) {
                if ($scope.password != "") {
                    if (angular.equals($scope.password, $scope.confirmPassword)) {
                        //console.log('editEmployeePasswordByAdmin', config);
                        //console.log('editPassword', config);
                        return $http({
                            url: '/api/pageview/editPassword',
                            method: "POST",
                            data: config,
                            headers: {
                                "Content-Type": "application/json",
                                'RequestVerificationToken': $scope.antiForgeryToken
                            }
                        }).then(function (response) {
                            console.log('response', response);
                            if (response.data == "Ok") {
                                if (window.location.pathname == "/Admin/ManagePassword") {
                                    console.log('123');
                                    var url = '/Admin/Index';
                                    window.location = url;
                                }
                                if (window.location.pathname == "/CustomerLocationContact/ManagePassword") {
                                    console.log('456');
                                    var url = '/CustomerLocationContact/Index';
                                    window.location = url;
                                }
                                if (window.location.pathname == "/Customer/ManagePassword") {
                                    console.log('XXXXXXXx');
                                    var url = '/Customer/Index';
                                    window.location = url;
                                }
                                //else {
                                //    var url = '/Account/Login';
                                //    window.location = url;
                                //}
                            }
                        }, function (error) {
                            $scope.registermessage = error;
                        });
                    }
                    else {
                        $scope.matchPswd = "The password and confirm password do not match.";
                    }
                }
            }
        };

        $scope.EditEmployeePasswordByAdmin = function (id) {
            var config = {
                UserId: id, UserName: $scope.userName, UserPassword: $scope.password
            }
            if ($scope.password != null) {
                if ($scope.password != "") {
                    if (angular.equals($scope.password, $scope.confirmPassword)) {
                        console.log('editEmployeePasswordByAdmin', config);
                        return $http({
                            url: '/api/pageview/editEmployeePasswordByAdmin',
                            method: "POST",
                            data: config,
                            headers: {
                                "Content-Type": "application/json",
                                'RequestVerificationToken': $scope.antiForgeryToken
                            }
                        }).then(function (response) {
                            console.log('response', response);
                            if (response.data == "Ok") {
                                var url = '/Admin/ManageEmployee';
                                window.location = url;
                            }
                        }, function (error) {
                            $scope.registermessage = error;
                        });
                    }
                    else {
                        $scope.matchPswd = "The password and confirm password do not match.";
                    }
                }
            }
        };

        $scope.checkPasswords = function () {
            if (angular.equals($scope.password, $scope.confirmPassword)) {
                $scope.matchPswd = "";
            }
            else {
                $scope.matchPswd = "The password and confirm password do not match.";
            }
        };
    }
})();