//var app = angular.module('myApp', ['ngFileUpload', 'naif.base64', 'ui.bootstrap']);

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

//app.controller('ImageUploadCtrl', ImageUploadCtrl);
angular.module('myApp')
    .controller('ImageUploadCtrl', ImageUploadCtrl);

ImageUploadCtrl.$inject = ['$location', '$scope', '$http', 'filterFilter', '$window', '$timeout', 'Upload', '$document'];

function ImageUploadCtrl($location, $scope, $http, filterFilter, $window, $timeout, Upload, $document) {

    $http.get('/api/pageview/getAllEmployee').then(function (response) {
        $scope.getAllEmployee = response.data;
        console.log('$scope.getAllEmployee', $scope.getAllEmployee);
        if ($scope.getAllEmployee != null) { $scope.getAllEmployeecount = $scope.getAllEmployee.length; }
        else { $scope.getAllEmployeecount = 0; }
        $scope.viewby = '5';
        $scope.totalemployee = $scope.getAllEmployeecount;
        $scope.currentPage = '1';
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = '5'; //Number of pager buttons to show

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
        };
        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

    }, function (response) {
        $scope.waiting = false;
        });

    $scope.pictures = [];

    $scope.removeSelectedPic = function (filename) {
        var i = $scope.pictures.objIndexOf('filename', filename);
        console.log('removeSelectedPic--', i);
        $scope.pictures.splice(i, 1);
    }
    $scope.refreshImageUpload = function () {
        $scope.pictures = "";
    }

    var _createtags = function () {
        console.log('dxwd');
        var imagetags = "";
        angular.forEach($scope.pictures, function (item) {
            console.log('$scope.pictures--', $scope.pictures)
            if (item) {
                console.log('item--', item)
                imagetags += 'img/items/' + item.filename + ';';
                console.log('item--', imagetags)
            }
        })
        $scope.imagetags = imagetags;
    }

    $scope.saveImages = function () {
        //_createtags();
        //var input = document.getElementById('getpicid');
        //var file = input.files;
        //$scope.SelectedFiles = file;
        console.log('image tags-', $scope.pictures);
        var config = {
            imgList: $scope.pictures
        }
        console.log('config', config);
        Upload.upload({
            url: '/uploadImagestoItmFolder',
            data: {
                files: $scope.SelectedFiles,
                model: config
            }
        }).then(function (response) {
            $timeout(function () {
                $scope.Result = response.data;
                console.log('$scope.Result--', $scope.Result)
                //$scope.userInfo.ProfilePic = '/img/items/' + file;

            });
        }, function (response) {
            if (response.status > 0) {
                var errorMsg = response.status + ': ' + response.data;
                alert(errorMsg);
            }
        });
    };

    $scope.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
    };

    Array.prototype.objIndexOf = function (field, value, value2) {
        if (value2 && field.indexOf(';') > 0) {
            var fields = field.split(';');
            for (var i = 0; i < this.length; i++) {
                if (this[i].hasOwnProperty(fields[0]) && this[i][fields[0]] == value && this[i].hasOwnProperty(fields[1]) && this[i][fields[1]] == value2) {
                    return i;
                    break;
                }
            }
        } else {
            for (var i = 0; i < this.length; i++) {
                if (this[i].hasOwnProperty(field) && this[i][field] == value) {
                    return i;
                    break;
                }
            }
        }
        return -1;
    };

}


//$scope.pictures = [];
//console.log('pictureLogo -', $scope.pictureLogo);

//$scope.removeSelectedPic = function (filename) {
//    var i = $scope.pictures.objIndexOf('filename', filename);
//    console.log('removeSelectedPic--', i);
//    $scope.pictures.splice(i, 1);
//}

//$scope.refreshImageUpload = function () {
//    $scope.pictures = "";
//}

//$scope.saveImages = function () {
//    //_createtags();
//    var input = document.getElementById('getpicid');
//    var file = input.files;
//    $scope.SelectedFiles = file;
//    var config = {
//        imgList: $scope.pictures
//    }
//    console.log('config', config);
//    Upload.upload({
//        url: '/uploadImagestoItmFolder',
//        data: {
//            files: $scope.SelectedFiles,
//            model: config
//        }
//    }).then(function (response) {
//        $timeout(function () {
//            $scope.Result = response.data;
//            console.log('$scope.Result--', $scope.Result)
//            //$scope.userInfo.ProfilePic = '/img/items/' + file;

//        });
//    }, function (response) {
//        if (response.status > 0) {
//            var errorMsg = response.status + ': ' + response.data;
//            alert(errorMsg);
//        }
//    });
//};

//$scope.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
//};

//Array.prototype.objIndexOf = function (field, value, value2) {
//    if (value2 && field.indexOf(';') > 0) {
//        var fields = field.split(';');
//        for (var i = 0; i < this.length; i++) {
//            if (this[i].hasOwnProperty(fields[0]) && this[i][fields[0]] == value && this[i].hasOwnProperty(fields[1]) && this[i][fields[1]] == value2) {
//                return i;
//                break;
//            }
//        }
//    } else {
//        for (var i = 0; i < this.length; i++) {
//            if (this[i].hasOwnProperty(field) && this[i][field] == value) {
//                return i;
//                break;
//            }
//        }
//    }
//    return -1;
//};