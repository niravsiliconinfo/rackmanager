(function () {
    'use strict';

    //var app = angular.module('myApp', []);
    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    angular.module('myApp')
        .controller('homeCtrl', homeCtrl);


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

    app.controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$http', '$filter', '$window', '$timeout', 'Upload', '$document', '$location', '$interval'];

    function homeCtrl($scope, $http, $filter, $window, $timeout, Upload, $document, $location, $interval, $rootScope) {

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

       
        //$http.get('/api/pageview/getAllAdminCountInUser').then(function (response) {
        //    $scope.getAllAdminCountInUser = response.data;
        //    console.log('$scope.getAllAdminCountInUser', $scope.getAllAdminCountInUser);
        //}, function (response) {
        //    $scope.waiting = false;
        //});

        //$http.get('/api/pageview/getAllEmployeeCountInUser').then(function (response) {
        //    $scope.getAllEmployeeCountInUser = response.data;
        //    console.log('$scope.getAllEmployeeCountInUser', $scope.getAllEmployeeCountInUser);
        //}, function (response) {
        //    $scope.waiting = false;
        //});

        $http.get('/api/pageview/getAllCustomers').then(function (response) {
            $scope.getAllCustomers = response.data;
            console.log('$scope.getAllCustomers', $scope.getAllCustomers);
            if ($scope.getAllCustomers != null) { $scope.getAllCustomerscount = $scope.getAllCustomers.length; }
            else { $scope.getAllCustomerscount = 0; }
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getRecentInspectionAdminDashboard').then(function (response) {
            $scope.getRecentInspectionAdminDashboard = response.data;
            console.log('$scope.getRecentInspectionAdminDashboard', $scope.getRecentInspectionAdminDashboard);
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getRecentCompletedInspectionbyCustomerId').then(function (response) {
            $scope.getRecentCompletedInspectionbyCustomerId = response.data;
            console.log('$scope.getRecentCompletedInspectionbyCustomerId', $scope.getRecentCompletedInspectionbyCustomerId);
        }, function (response) {
            $scope.waiting = false;
        });

        if (window.location.pathname == "/Customer/Index") {
            $http.get('/api/pageview/getRecentInspectionbyCustomerId').then(function (response) {
                $scope.getRecentInspectionbyCustomerId = response.data;
                console.log('$scope.getRecentInspectionbyCustomerId', $scope.getRecentInspectionbyCustomerId);
            }, function (response) {
                $scope.waiting = false;
            });
        }


        $http.get('/api/pageview/getRecentInspectionCustomerByEmployeeId').then(function (response) {
            $scope.getRecentInspectionCustomerByEmployeeId = response.data;
            console.log('$scope.getRecentInspectionCustomerByEmployeeId', $scope.getRecentInspectionCustomerByEmployeeId);
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllApproveAndCompleteInspection').then(function (response) {
            $scope.getAllApproveAndCompleteInspection = response.data;
            console.log('$scope.getAllApproveAndCompleteInspection', $scope.getAllApproveAndCompleteInspection);
            if ($scope.getAllApproveAndCompleteInspection != null) { $scope.getAllApproveAndCompleteInspectioncount = $scope.getAllApproveAndCompleteInspection.length; }
            else { $scope.getAllApproveAndCompleteInspectioncount = 0; }
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllSentForApprovalInspection').then(function (response) {
            $scope.getAllSentForApprovalInspection = response.data;
            console.log('$scope.getAllSentForApprovalInspection', $scope.getAllSentForApprovalInspection);
            if ($scope.getAllSentForApprovalInspection != null) { $scope.getAllSentForApprovalInspectioncount = $scope.getAllSentForApprovalInspection.length; }
            else { $scope.getAllSentForApprovalInspectioncount = 0; }
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllInProgressInspection').then(function (response) {
            $scope.getAllInProgressInspection = response.data;
            console.log('$scope.getAllInProgressInspection', $scope.getAllInProgressInspection);
            if ($scope.getAllInProgressInspection != null) { $scope.getAllInProgressInspectioncount = $scope.getAllInProgressInspection.length; }
            else { $scope.getAllInProgressInspectioncount = 0; }
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllDueInspectionAdminDashboard').then(function (response) {
            $scope.getAllDueInspectionAdminDashboard = response.data;
            console.log('$scope.getAllDueInspectionAdminDashboard', $scope.getAllDueInspectionAdminDashboard);
            if ($scope.getAllDueInspectionAdminDashboard != null) { $scope.getAllDueInspectionAdminDashboardcount = $scope.getAllDueInspectionAdminDashboard.length; }
            else { $scope.getAllDueInspectionAdminDashboardcount = 0; }
            $scope.totalgetAllDueInspectionAdminDashboard = $scope.getAllDueInspectionAdminDashboardcount;
        }, function (response) {
            $scope.waiting = false;
        });


        $http.get('/api/pageview/getDueInspectionByCustomerId').then(function (response) {
            $scope.getDueInspectionByCustomerId = response.data;
            console.log('$scope.getDueInspectionByCustomerId', $scope.getDueInspectionByCustomerId);
            if ($scope.getDueInspectionByCustomerId != null) { $scope.getDueInspectionByCustomerIdcount = $scope.getDueInspectionByCustomerId.length; }
            else { $scope.getDueInspectionByCustomerIdcount = 0; }
            $scope.totalgetDueInspectionByCustomerId = $scope.getDueInspectionByCustomerIdcount;
        }, function (response) {
            $scope.waiting = false;
        });
       

        $http.get('/api/pageview/getInspectionDueByEmployeeId').then(function (response) {
            $scope.getInspectionDueByEmployeeId = response.data;
            console.log('$scope.getInspectionDueByEmployeeId', $scope.getInspectionDueByEmployeeId);
            if ($scope.getInspectionDueByEmployeeId != null) { $scope.getInspectionDueByEmployeeIdcount = $scope.getInspectionDueByEmployeeId.length; }
            else { $scope.getInspectionDueByEmployeeIdcount = 0; }
            $scope.totalgetInspectionDueByEmployeeId = $scope.getInspectionDueByEmployeeIdcount;
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.filterInspectionsByStatus = function () {
            const selected = $scope.InspectionStatusLayout
                .filter(s => s.selected)
                .map(s => s.InspectionStatusId);

            // Update shared service
            inspectionFilterService.setSelectedStatuses(selected);

            // Broadcast change
            $rootScope.$broadcast('inspectionFilterChanged', selected);
        };
        //$http.get('/api/pageview/getInventoryCount').then(function (response) {
        //    $scope.getInventoryCount = response.data;
        //    console.log('$scope.getInventoryCount', $scope.getInventoryCount);
        //}, function (response) {
        //    $scope.waiting = false;
        //});

        $scope.UserEmployeeById = function (Id) {
            $http.get('/api/pageview/getUserEmployeeById', { params: { id: Id } }).then(function (response) {
                $scope.getUserEmployeeById = response.data;
                console.log('getUserEmployeeById--', $scope.getUserEmployeeById);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        if (window.location.pathname == "/Admin/Index") {
            var min = 2022,
                max = new Date().getFullYear();
            var range = [];

            for (var i = max; i >= min; i--) {
                $('#selectedYear').append($('<option />').val(i).html(i));
            }

            $scope.years = range;
        }
        if (window.location.pathname == "/Customer/Index") {
            var min = 2022,
                max = new Date().getFullYear();
            var range = [];

            for (var i = max; i >= min; i--) {
                $('#selectedYearCustomer').append($('<option />').val(i).html(i));
            }

            $scope.years = range;

            console.log('Selected Year:', '');
            var selectedYear = $('#selectedYearCustomer').val();
            console.log('Selected Year:', selectedYear);
            $http.get('/api/pageview/getDeficienciesBreakdownCategories', { params: { year: selectedYear } }).then(function (response) {
                $scope.getDeficienciesBreakdownCategorieslist = response.data;
                console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
            }, function (response) {
                $scope.waiting = false;
            });
        }
        $('#selectedYearCustomer').change(function () {
            var selectedYear = $(this).val();
            console.log('Selected Year:', '');
            /*var selectedYear = $('#selectedYear').val();*/
            console.log('Selected Year:', selectedYear);
            $http.get('/api/pageview/getDeficienciesBreakdownCategories', { params: { year: selectedYear } }).then(function (response) {
                $scope.getDeficienciesBreakdownCategorieslist = response.data;
                console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
            }, function (response) {
                $scope.waiting = false;
            });
        });
        $scope.EmpEditMyProfile = function (id) {
            var config = {
                UserName: $scope.username, UserPassword: $scope.password, Active: $scope.Active, EmployeeName: $scope.employeename,
                EmployeeEmail: $scope.email, EmployeeAddress: $scope.address, CityID: $scope.city, CountryID: $scope.country,
                ProvinceID: $scope.province, Pincode: $scope.pin, Gender: $scope.gender, TitleDegrees: $scope.titledegree, MobileNo: $scope.mobileNo,
                UserType: $scope.usertype, UserId: id
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

        $scope.InspectionsheetClick = function (id) {
            var url = '/Admin/InspectionSheet?id=' + id;
            window.location = url;
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
        }

        $scope.chartDataByYearCustomerAngular = function (selectedValue) {
            // Your logic here, e.g., call your API with the selected value
            //var year = $scope.selectedYear;
            var selectedYear = $('#selectedYear').val();
            console.log('Selected Year:', selectedYear);
            // Make your API call here
            $http.get('/api/pageview/getDeficienciesBreakdownCategories?year=' + selectedYear)
                .then(function (response) {
                    $scope.getDeficienciesBreakdownCategorieslist = response.data;
                    console.log('getDeficienciesBreakdownCategories', $scope.getDeficienciesBreakdownCategorieslist);
                }, function (response) {
                    $scope.waiting = false;
                });
        };

        console.log('-----------layoutCtrl--------------');

        $http.get('/api/pageview/getAllInspectionType').then(function (response) {
            $scope.InspectionTypeLayout = response.data;
            console.log('$scope.InspectionTypeLayout--', $scope.InspectionTypeLayout);

            $scope.InspectionTypeLayoutcount = $scope.InspectionTypeLayout ? $scope.InspectionTypeLayout.length : 0;
        }, function () {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllInspectionStatus').then(function (response) {
            $scope.InspectionStatusLayout = response.data;
            console.log('$scope.getAllInspectionStatus--', $scope.InspectionStatusLayout);
            if ($scope.InspectionStatusLayout != null) { $scope.InspectionStatusLayoutcount = $scope.InspectionStatusLayout.length; }
            else { $scope.InspectionStatusLayoutcount = 0; }
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getProvincebyCountryId', { params: { id: 32 } }).then(function (response) {
            $scope.getProvincebyCountryIdLayout = response.data;
            console.log('getProvincebyCountryId--', $scope.getProvincebyCountryIdLayout);
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getProvincebyCountryIdByCustomer').then(function (response) {
            $scope.getProvincebyCountryIdByCustomer = response.data;
            console.log('getProvincebyCountryIdByCustomer--', $scope.getProvincebyCountryIdByCustomer);
        }, function (response) {
            $scope.waiting = false;
        });
        
        $scope.getCitybyProvinceIdByCustomer = function () {
            console.log('call city from province for cust',);
            $http.get('/api/pageview/getCitybyProvinceIdByCustomer', { params: { id: $scope.province } }).then(function (response) {
                $scope.getCitybyProvinceIdByCustomer = response.data;
                console.log('---------getCitybyProvinceIdByCustomer------------------', $scope.getCitybyProvinceIdByCustomer);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.GetCitybyProvinceId = function () {
            $scope.strProvince = document.getElementById("drpprovinceLayout").value;
            console.log('strProvince', $scope.strProvince);
            $http.get('/api/pageview/getCitybyProvinceId', { params: { id: $scope.strProvince } }).then(function (response) {
                $scope.getCitybyProvinceIdLayout = response.data;
                console.log('getCitybyProvinceId--', $scope.getCitybyProvinceIdLayout);
            }, function (response) {
                $scope.waiting = false;
            });
        };

    }

})();