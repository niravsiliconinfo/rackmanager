(function () {
    'use strict';

    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    //app.controller('rackingCtrl', rackingCtrl);
    angular.module('myApp')
        .controller('rackingCtrl', rackingCtrl);

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

    rackingCtrl.$inject = ['$scope', '$http', '$timeout', 'Upload'];

    function rackingCtrl($scope, $http, $timeout, Upload) {

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

        $http.get('/api/pageview/getAllRackingType').then(function (response) {
            $scope.getAllRackingType = response.data;
            console.log('$scope.getAllRackingType', $scope.getAllRackingType);
            if ($scope.getAllRackingType != null) { $scope.getAllRackingTypecount = $scope.getAllRackingType.length; }
            else { $scope.getAllRackingTypecount = 0; }
            $scope.totalgetAllRackingType = $scope.getAllRackingTypecount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllIdentifyRackingProfile').then(function (response) {
            $scope.getAllIdentifyRackingProfile = response.data;
            console.log('$scope.getAllIdentifyRackingProfile', $scope.getAllIdentifyRackingProfile);
            if ($scope.getAllIdentifyRackingProfile != null) { $scope.getAllIdentifyRackingProfilecount = $scope.getAllIdentifyRackingProfile.length; }
            else { $scope.getAllIdentifyRackingProfilecount = 0; }
            $scope.totalgetAllRackingProfile = $scope.getAllIdentifyRackingProfilecount;
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.SaveRackingType = function () {
            var config = {
                RackingTypeName: $scope.rackingTypeName
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveRackingType',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageRackingType';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditRackingType = function (Id) {
            var config = {
                RackingTypeId: Id, RackingTypeName: $scope.rackingTypeName
            }
            console.log('editRackingType', config);
            return $http({
                url: '/api/pageview/editRackingType',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageRackingType';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveRackingType = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeRackingType',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageRackingType';
                    window.location = url;
                }
            });
        }

        $scope.SaveIdentifyRackingProfile = function () {
            var input = document.getElementById('getIdentifyRackingProfileImage');
            var file = input.files;
            $scope.SelectedFiles = file;
            console.log($scope.IdentifyRackingProfileImage.filename);
            var config = {
                IdentifyRackingProfileImage: $scope.IdentifyRackingProfileImage.filename
            }
            console.log('saveIdentifyRackingProfile', config);
            Upload.upload({
                url: '/uploadImagetoIdentifyRackingProfileFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('$scope.AddIdentifyRackingProfile--', response.data)
                    if (response.data == "Ok") {
                        var url = '/Admin/ManageIdentifyRackingProfile';
                        window.location = url;
                    }
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            });
        };

        $scope.EditIdentifyRackingProfile = function (Id) {
            var input = document.getElementById('getIdentifyRackingProfileImage');
            var file = input.files;
            $scope.SelectedFiles = file;
            console.log('123978----------', $scope.SelectedFiles);

            if ($scope.IdentifyRackingProfileImage == undefined) {
                console.log('2222-');
               
            }
            else {
                console.log('3333-');
                var config = {
                    IdentifyRackingProfileID: Id, IdentifyRackingProfileImage: $scope.IdentifyRackingProfileImage.filename
                }
            }
            console.log('editIdentifyRackingProfile', config);
            Upload.upload({
                url: '/uploadImagetoIdentifyRackingProfileFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('response.data edit customer--', response.data);
                    if (response.data == "Ok") {
                        var url = '/Admin/ManageIdentifyRackingProfile';
                        window.location = url;
                    }
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            });
            // url: '/api/pageview/editComponent',
        };

        $scope.RemoveIdentifyRackingProfile = function (id) {
            console.log('delete id', id);
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeIdentifyRackingProfile',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageIdentifyRackingProfile';
                    window.location = url;
                }
            }, function (error) {

            });
        }
    }
})();
