(function () {
    'use strict';

    //var app = angular.module('myApp');
    angular.module('myApp')
        .controller('employeeCtrl', employeeCtrl);
    //app.controller('employeeCtrl', employeeCtrl);

    employeeCtrl.$inject = ['$scope', '$http'];

    function employeeCtrl($scope, $http) {

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

        $http.get('/api/pageview/getAllEmployee').then(function (response) {
            $scope.getAllEmployee = response.data;
            console.log('$scope.getAllEmployee', $scope.getAllEmployee);
            if ($scope.getAllEmployee != null) { $scope.getAllEmployeecount = $scope.getAllEmployee.length; }
            else { $scope.getAllEmployeecount = 0; }
            $scope.totalemployee = $scope.getAllEmployeecount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllEmployeeUser').then(function (response) {
            $scope.getAllEmployeeUser = response.data;
            console.log('$scope.getAllEmployeeUser', $scope.getAllEmployeeUser);
            if ($scope.getAllEmployeeUser != null) { $scope.getAllEmployeeUsercount = $scope.getAllEmployeeUser.length; }
            else { $scope.getAllEmployeeUsercount = 0; }
            $scope.totalemployee = $scope.getAllEmployeeUsercount;
        }, function (response) {
            $scope.waiting = false;
            });

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

        $scope.AddUserEmployee = function () {
            console.log('Empplyee ma aayo');
            var config = {
                UserName: $scope.username, UserPassword: $scope.password, Active: $scope.Active, EmployeeName: $scope.employeename,
                EmployeeEmail: $scope.email, EmployeeAddress: $scope.address, CityID: $scope.city, CountryID: $scope.country,
                ProvianceID: $scope.province, Pincode: $scope.pin, Gender: $scope.gender, TitleDegrees: $scope.titledegree, MobileNo: $scope.mobileNo,
                UserType: $scope.usertype,
                IsStampingEngineer : $scope.isstampingengineer
            }
            console.log('adduseremployee', config);
            return $http({
                url: '/Account/EmpCreate',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                console.log('response', response);
                $scope.registermessage = response.data;
                if (response.data.message === "Ok" || response.data === "Ok") {
                    console.log('response.data--', response.data);
                    var url = '/Admin/ManageEmployee';
                    window.location = url;
                }
                //else {
                //    var url = '/Admin/Index';
                //    window.location = url;
                //}
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.edituseremployee = function (id) {
            var config = {
                UserName: $scope.username, UserPassword: $scope.password, Active: $scope.Active, EmployeeName: $scope.employeename,
                EmployeeEmail: $scope.email, EmployeeAddress: $scope.address, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, Gender: $scope.gender, TitleDegrees: $scope.titledegree, MobileNo: $scope.mobileNo,
                UserType: $scope.usertype, IsStampingEngineer: $scope.isstampingengineer, UserId: id
            }
            console.log('edituseremployee', config);
            return $http({
                url: '/Account/EmpEdit',
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
                    var url = '/Admin/ManageEmployee';
                    window.location = url;
                }
            }, function (error) {
                $scope.registermessage = error;
            });
        };

        $scope.removeUserEmployee = function (id) {
            console.log('delete id', id);
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeUserEmployee',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageEmployee';
                    window.location = url;
                }
            }, function (error) {

            });
        }

        $scope.EditEmployeePasswordByAdmin = function (id) {
            var config = {
                EmployeeID: id, UserName: $scope.userName, UserPassword: $scope.password
            }
            console.log('EditEmployeePasswordByAdmin', config);
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
        };
    }
})();
