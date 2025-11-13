(function () {
    'use strict';

    //var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);
    //app.controller('componentCtrl', componentCtrl);
    angular.module('myApp')
        .controller('componentCtrl', componentCtrl);

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

    componentCtrl.$inject = ['$scope', '$http', '$timeout', 'Upload'];

    function componentCtrl($scope, $http, $timeout, Upload) {

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

        $scope.ShowAddComponentPropertyTypeBox = true;
        $scope.ShowAddComponentPropertyValueBox = false;

        if (window.location.pathname == "/Admin/AddComponent") {
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


        if (window.location.pathname == '/Admin/EditComponent') {
            $http.get('/api/pageview/getAllManufacturer').then(function (response) {
                $scope.getAllManufacturer = response.data;
                console.log('$scope.getAllManufacturer', $scope.getAllManufacturer);
                if ($scope.getAllManufacturer != null) { $scope.getAllManufacturercount = $scope.getAllManufacturer.length; }
                else { $scope.getAllManufacturercount = 0; }
                $scope.totalgetAllManufacturer = $scope.getAllManufacturercount;
            }, function (response) {
                $scope.waiting = false;
            });
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

        $http.get('/api/pageview/getComponentPropertyType').then(function (response) {
            $scope.getComponentPropertyType = response.data;
            console.log('$scope.getComponentPropertyType', $scope.getComponentPropertyType);
            if ($scope.getComponentPropertyType != null) { $scope.getComponentPropertyTypecount = $scope.getComponentPropertyType.length; }
            else { $scope.getComponentPropertyTypecount = 0; }
            $scope.totalgetComponentPropertyType = $scope.getComponentPropertyTypecount;
            console.log('$scope.totalgetComponentPropertyType', $scope.totalgetComponentPropertyType);

        }, function (response) {
            $scope.waiting = false;
        });

        $scope.SaveComponent = function () {
            var input = document.getElementById('getcomponentImageid');
            var file = input.files;
            $scope.SelectedFiles = file;

            var checkedManufacturer = '';
            $scope.getAllManufacturer.forEach(function (man) {
                if (man.selected) {
                    if (checkedManufacturer != '') {
                        checkedManufacturer += ",";
                    }
                    checkedManufacturer += man.ManufacturerId;
                }
            });
            $scope.checkedManufacturerId = checkedManufacturer;
            var config = {
                ComponentName: $scope.componentName, ManufacturerId: $scope.checkedManufacturerId, ComponentImage: $scope.componentImage.filename
            }
            console.log('saveConclusionRecommendations', config);
            Upload.upload({
                url: '/uploadImagetoComponentFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('$scope.AddCustomer resp--', response.data)
                    if (response.data == "Ok") {
                        var url = '/Admin/ManageComponent';
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

        $scope.EditComponent = function (Id) {
            var input = document.getElementById('getcomponentImageid');
            var file = input.files;
            $scope.SelectedFiles = file;

            var checkedManufacturer = '';
            $scope.getAllManufacturer.forEach(function (man) {
                if (man.selected) {
                    if (checkedManufacturer != '') {
                        checkedManufacturer += ",";
                    }
                    checkedManufacturer += man.ManufacturerId;
                }
            });
            $scope.checkedManufacturerId = checkedManufacturer;
            console.log('$scope.componentImage', $scope.componentImage);
            if ($scope.componentImage == undefined) {
                console.log('2222-');
                var config = {
                    ComponentId: Id, ComponentName: $scope.componentName, ManufacturerId: $scope.checkedManufacturerId
                }
            }
            else {
                console.log('3333-');
                var config = {
                    ComponentId: Id, ComponentName: $scope.componentName, ManufacturerId: $scope.checkedManufacturerId, ComponentImage: $scope.componentImage.filename
                }
            }
            console.log('editComponent', config);
            Upload.upload({
                url: '/uploadImagetoComponentFolder',
                data: {
                    files: $scope.SelectedFiles,
                    model: config
                }
            }).then(function (response) {
                $timeout(function () {
                    console.log('response.data edit customer--', response.data);
                    if (response.data == "Ok") {
                        var url = '/Admin/ManageComponent';
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

        $scope.RemoveComponent = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeComponent',
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
        }

        if (window.location.pathname == '/Admin/EditComponent') {
            $http.get('/api/pageview/getAllManufacturer').then(function (response) {
                $scope.getAllManufacturer = response.data;
                console.log('$scope.getAllManufacturer', $scope.getAllManufacturer);
            }, function (response) {
                $scope.waiting = false;
            });
            var para = window.location.search;
            para = para.replace('?id=', '');
            $http.get('/api/pageview/getComponentsManufacturerById', { params: { id: para } }).then(function (response) {
                $scope.getComponentsManufacturerById = response.data;
                var mnftrr = $scope.getComponentsManufacturerById.ManufacturerId;
                console.log('$scope.getComponentsManufacturerById', $scope.getComponentsManufacturerById);
                console.log('$scope.mnftrr', mnftrr);
                angular.forEach($scope.getAllManufacturer, function (man) {
                    if (mnftrr.indexOf(man.ManufacturerId) > -1) {
                        man.selected = true;
                    }
                });

            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.SaveComponentPropertyType = function () {
            var config = {
                ComponentPropertyTypeName: $scope.componentPropertyTypeName, ComponentPropertyTypeDesctiption: $scope.componentPropertyTypeDesctiption, ComponentId: $scope.componentId
            }
            console.log('saveConclusionRecommendations----123456', config);
            return $http({
                url: '/SaveComponentPropertyType',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data == "1") {
                    var url = '/Admin/ManageComponentPropertyType';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

       

        $scope.EditComponentPropertyType = function (Id) {
            var config = {
                ComponentPropertyTypeId: Id, ComponentPropertyTypeName: $scope.componentPropertyTypeName, ComponentPropertyTypeDesctiption: $scope.componentPropertyTypeDesctiption, ComponentId: $scope.componentId
            }
            console.log('editComponentPropertyType', config);
            return $http({
                url: '/api/pageview/editComponentPropertyType',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageComponentPropertyType';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveComponentPropertyType = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeComponentPropertyType',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageComponentPropertyType';
                    window.location = url;
                }
            });
        }

        if (window.location.pathname == "/Admin/EditComponentPropertyType") {
            var para = window.location.search;
            para = para.replace('?id=', '');
            console.log('parameter--', para);
            $http.get('/api/pageview/GetComponentPropertyValues', { //getCompPropertyValuesByCompPropertyTypeId
                params: { ComponentPropertyTypeId: para }
            }).then(function (response) {
                $scope.getCompPropertyValuesByCompPropertyTypeId = response.data;
                console.log('$scope.getCompPropertyValuesByCompPropertyTypeId', $scope.getCompPropertyValuesByCompPropertyTypeId);
                if ($scope.getCompPropertyValuesByCompPropertyTypeId != null) { $scope.getCompPropertyValuesByCompPropertyTypeIdcount = $scope.getCompPropertyValuesByCompPropertyTypeId.length; }
                else { $scope.getCompPropertyValuesByCompPropertyTypeIdcount = 0; }
                $scope.totalgetCompPropertyValuesByCompPropertyTypeId = $scope.getCompPropertyValuesByCompPropertyTypeIdcount;
            }, function (response) {
                $scope.waiting = false;
            });
        }

        $scope.SessionAddComponentPropertyTypeClick = function () {
            var config = {
                ComponentPropertyTypeName: $scope.componentPropertyTypeName, ComponentPropertyTypeDesctiption: $scope.componentPropertyTypeDesctiption, ComponentId: $scope.componentId
            }
            console.log('SessionAddComponentPropertyValueClick', config);
            return $http({
                url: '/Admin/SessionAddComponentPropertyType',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    $scope.ShowAddComponentPropertyTypeBox = false;
                    $scope.ShowAddComponentPropertyValueBox = true;
                }
            }, function (error) {
                alert(error);
            });
        }

        $scope.SessionAddComponentPropertyValueClick = function () {
            setTimeout(function () {
                angular.element('#SessionAddComponentPropertyTypeClickId').trigger('click');
            }, 0);
        };

        $scope.SaveComponentPropertyValues = function () {
            var config = {
                ComponentPropertyValue1: $scope.componentPropertyValue, ComponentPropertyTypeId: $scope.componentPropertyTypeId
            }
            console.log('SessionAddComponentPropertyValue', config);
            return $http({
                url: '/Admin/SessionAddComponentPropertyValue',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    console.log('response.data2--', response.data);
                    console.log('response.config.data3--', response.config.data);
                    $scope.componentPropertyValue = "";
                    $scope.ShowAddComponentPropertyValueBox = false;
                    $scope.ShowAddComponentPropertyTypeBox = true;
                    $http.get('/Admin/GetSessionComponentPropertyValue').then(function (response) {
                        $scope.getComponentPropertyValueAdd = response.data;
                        console.log('$scope.getComponentPropertyValueAdd', $scope.getComponentPropertyValueAdd);
                        if ($scope.getComponentPropertyValueAdd != null) { $scope.getComponentPropertyValueAddcount = $scope.getComponentPropertyValueAdd.length; }
                        else { $scope.getComponentPropertyValueAddcount = 0; }
                        $scope.totalgetComponentPropertyValueAdd = $scope.getComponentPropertyValueAddcount;
                    }, function (response) {
                        $scope.waiting = false;
                    });
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.SaveComponentPropertyValuesEdit = function () {
            var config = {
                ComponentPropertyValue1: $scope.componentPropertyValue, ComponentPropertyTypeId: $scope.componentPropertyTypeId
            }
            console.log('SaveComponentPropertyValuesEdit', config);
            return $http({
                url: '/api/pageview/saveComponentPropertyValues',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data != null) {
                    console.log('response.data2--', response.data);
                    var url = '/Admin/EditComponentPropertyType?id=' + response.data;
                    window.location = url;
                }
                else {
                    var url = '/Account/Login';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditComponentPropertyValues = function (Id) {
            var config = {
                ComponentPropertyValueId: Id, ComponentPropertyValue1: $scope.componentPropertyValue, ComponentPropertyTypeId: $scope.componentPropertyTypeId
            }
            console.log('editComponentPropertyValues', config);
            return $http({
                url: '/api/pageview/editComponentPropertyValues',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageComponentPropertyType';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveComponentPropertyValues = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeComponentPropertyValues',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageComponentPropertyType';
                    window.location = url;
                }
            });
        }
    }
})();
