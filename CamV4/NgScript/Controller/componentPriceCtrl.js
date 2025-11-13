(function () {
    'use strict';

    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    //app.controller('componentPriceCtrl', componentPriceCtrl);
    angular.module('myApp')
        .controller('componentPriceCtrl', componentPriceCtrl);

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

    componentPriceCtrl.$inject = ['$scope', '$http', '$timeout', 'Upload'];

    function componentPriceCtrl($scope, $http, $timeout, Upload) {

        $scope.viewby = '50';
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10; //Number of pager buttons to show
        
        $scope.setPage = function (pageNo) {
            console.log("pageNo", pageNo);
            $scope.currentPage = pageNo;
        };
        $scope.pageChanged = function () { };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        $http.get('/api/pageview/getAllComponent').then(function (response) {
            $scope.getAllComponent = response.data;
            console.log('$scope.getAllComponent', $scope.getAllComponent);
            if ($scope.getAllComponent != null) { $scope.getAllComponentcount = $scope.getAllComponent.length; }
            else { $scope.getAllComponentcount = 0; }
            $scope.totalgetAllComponent = $scope.getAllComponentcount;
        }, function (response) {
            $scope.waiting = false;
        });

        if (window.location.pathname == '/Admin/ManageComponentPrice') {
            var para = window.location.search;
            para = para.replace('?id=', '');
            $http.get('/api/pageview/getAllComponentPrice', { params: { ComponentId: para } }).then(function (response) {
                $scope.getAllComponentPriceList = response.data;
                console.log('$scope.getAllComponentPriceList', $scope.getAllComponentPriceList);
                if ($scope.getAllComponentPriceList != null) { $scope.getAllComponentPriceListcount = $scope.getAllComponentPriceList.length; }
                else { $scope.getAllComponentPriceListcount = 0; }
                $scope.totalgetAllComponentPriceCount = $scope.getAllComponentPriceListcount;
                console.log('$scope.totalgetAllComponentPriceCount', $scope.getAllComponentPriceList);
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $http.get('/api/pageview/getAllImportantsettings').then(function (response) {
            $scope.getAllImportantSettings = response.data;
            console.log('$scope.getAllImportantsettings', $scope.getAllImportantSettings);
            if ($scope.getAllImportantSettings != null) { $scope.getAllImportantSettingscount = $scope.getAllImportantSettings.length; }
            else { $scope.getAllImportantSettingscount = 0; }
            $scope.totalgetAllComponentPrice = $scope.getAllImportantSettingscount;
        }, function (response) {
            $scope.waiting = false;
        });


        if (window.location.pathname == '/Admin/EditComponentPrice' || window.location.pathname == '/Admin/DeleteComponentPrice') {
            $http.get('/api/pageview/getAllManufacturer').then(function (response) {
                $scope.getAllManufacturer = response.data;
                console.log('---------------$scope.getAllManufacturer---------------------', $scope.getAllManufacturer);
                if ($scope.getAllManufacturer != null) { $scope.getAllManufacturercount = $scope.getAllManufacturer.length; }
                else { $scope.getAllManufacturercount = 0; }
                $scope.totalgetAllManufacturer = $scope.getAllManufacturercount;
            }, function (response) {
                $scope.waiting = false;
            });
        }


        $scope.EditComponentPrice = function (id) {
            var config = {
                ComponentPriceId: id, ManufacturerId: $scope.ManufacturerId, ItemPartNo: $scope.ItemPartNo, ComponentPriceDescription: $scope.ComponentPriceDescription, ComponentPrice: $scope.ComponentPrice,
                ComponentWeight: $scope.ComponentWeight, Surcharge: $scope.Surcharge, Markup: $scope.Markup, ComponentLabourTime: $scope.ComponentLabourTime,
                TotalPrice: $scope.TotalPrice
            }
            console.log('EditComponentPrice', config);
            return $http({
                url: '/api/pageview/editComponentPrice',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('response.data edit component price list--', response.data);
                    if (response.data == "Ok") {
                        console.log('ZZZZZZZZZZZZZZZZZ--');
                        var url = '/Admin/ManageComponent';
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

        $scope.DeleteComponentPrice = function (id) {
            var config = {
                id: id
            }
            return $http({
                url: '/api/pageview/deleteComponentPrice',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageComponent';
                    window.location = url;
                }
            });
        };

        $scope.importComponentPriceList = function () {
            var formData = new FormData();

            var selectedText = $('select[name="ComponentId"]').find("option:selected").text();
            //ComponentId: $scope.componentId
            var fileInput = document.getElementById('getcomponentPriceListid');
            var file = fileInput.files[0];

            if (file) {
                formData.append('file', file);
                formData.append('ComponentId', $scope.componentId);
                formData.append('Component', selectedText);
                //var saveFile = {
                //    ComponentId: $scope.componentId, file: formData
                //}
                console.log('importComponentPriceList----formData', formData);
                $.ajax({
                    url: '/importComponentPriceList',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        console.log('Response', response);
                        $('#result').html(response);
                    },
                    error: function (xhr, status, error) {
                        console.error('Upload failed: ', error);
                    }
                });
            } else {
                alert('Please select a file to upload.');
            }
        };

        $scope.verifyComponentPriceList = function () {
            var formData = new FormData();

            var selectedText = $('select[name="ComponentId"]').find("option:selected").text();
            //ComponentId: $scope.componentId
            var fileInput = document.getElementById('getcomponentPriceListid');
            var file = fileInput.files[0];

            if (file) {
                formData.append('file', file);
                formData.append('ComponentId', $scope.componentId);
                formData.append('Component', selectedText);
                //var saveFile = {
                //    ComponentId: $scope.componentId, file: formData
                //}
                console.log('importComponentPriceList----formData', formData);
                $.ajax({
                    url: '/verifyComponentPriceList',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        console.log('Response', response);
                        $('#result').html(response);
                    },
                    error: function (xhr, status, error) {
                        console.error('Upload failed: ', error);
                    }
                });
            } else {
                alert('Please select a file to upload.');
            }
        };
    }
})();
