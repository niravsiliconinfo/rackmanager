(function () {
    'use strict';    
    /*    var app = angular.module('myApp', ['ui.bootstrap']);*/
    angular.module('myApp').controller('otherCtrl', otherCtrl);

    otherCtrl.$inject = ['$scope', '$http'];

    function otherCtrl($scope, $http) {
        // Conclusion Recommendation
        // Facilities Area
        // Process Overview
        // Document Title
        // Manufacturers
        // Notification

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

        $http.get('/api/pageview/getAllConclusionRecommendations').then(function (response) {
            $scope.getAllConclusionRecommendations = response.data;
            console.log('$scope.getAllConclusionRecommendations', $scope.getAllConclusionRecommendations);
            if ($scope.getAllConclusionRecommendations != null) { $scope.getAllConclusionRecommendationscount = $scope.getAllConclusionRecommendations.length; }
            else { $scope.getAllConclusionRecommendationscount = 0; }
            $scope.totalgetAllConclusionRecommendations = $scope.getAllConclusionRecommendationscount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllFacilitiesArea').then(function (response) {
            $scope.getAllFacilitiesArea = response.data;
            console.log('$scope.getAllFacilitiesArea', $scope.getAllFacilitiesArea);
            if ($scope.getAllFacilitiesArea != null) { $scope.getAllFacilitiesAreacount = $scope.getAllFacilitiesArea.length; }
            else { $scope.getAllFacilitiesAreacount = 0; }
            $scope.totalgetAllFacilitiesArea = $scope.getAllFacilitiesAreacount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllProcessOverview').then(function (response) {
            $scope.getAllProcessOverview = response.data;
            console.log('$scope.getAllProcessOverview', $scope.getAllProcessOverview);
            if ($scope.getAllProcessOverview != null) { $scope.getAllProcessOverviewcount = $scope.getAllProcessOverview.length; }
            else { $scope.getAllProcessOverviewcount = 0; }
            $scope.totalgetAllProcessOverview = $scope.getAllProcessOverviewcount;
        }, function (response) {
            $scope.waiting = false;
        });

        $http.get('/api/pageview/getAllDocumentTitle').then(function (response) {
            $scope.getAllDocumentTitle = response.data;
            console.log('$scope.getAllDocumentTitle', $scope.getAllDocumentTitle);
            if ($scope.getAllDocumentTitle != null) { $scope.getAllDocumentTitlecount = $scope.getAllDocumentTitle.length; }
            else { $scope.getAllDocumentTitlecount = 0; }
            $scope.totalgetAllDocumentTitlecount = $scope.getAllDocumentTitlecount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllManufacturer').then(function (response) {
            $scope.getAllManufacturer = response.data;
            console.log('$scope.getAllManufacturer', $scope.getAllManufacturer);
            if ($scope.getAllManufacturer != null) { $scope.getAllManufacturercount = $scope.getAllManufacturer.length; }
            else { $scope.getAllManufacturercount = 0; }
            $scope.totalgetAllManufacturer = $scope.getAllManufacturercount;
        }, function (response) {
            $scope.waiting = false;
            });

        $http.get('/api/pageview/getAllNotificationByUserIdWeb').then(function (response) {
            $scope.getAllNotificationByUserIdWeb = response.data;
            console.log('$scope.getAllNotificationByUserIdWeb', $scope.getAllNotificationByUserIdWeb);
            if ($scope.getAllNotificationByUserIdWeb != null) { $scope.getAllNotificationByUserIdWebcount = $scope.getAllNotificationByUserIdWeb.length; }
            else { $scope.getAllNotificationByUserIdWebcount = 0; }
            $scope.totalgetAllNotificationByUserIdWebcount = $scope.getAllNotificationByUserIdWebcount;
        }, function (response) {
            $scope.waiting = false;
        });

        $scope.SaveConclusionRecommendations = function () {
            var config = {
                ConclusionRecommendations: $scope.conclusionrecommendations, ConclusionRecommendationsTitle: $scope.conclusionrecommendationstitle
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveConclusionRecommendations',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageConclusionRecommendations';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditConclusionRecommendations = function (Id) {
            var config = {
                ConclusionRecommendationsID: Id, ConclusionRecommendations: $scope.conclusionrecommendations, ConclusionRecommendationsTitle: $scope.conclusionrecommendationstitle
            }
            console.log('editConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/editConclusionRecommendations',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageConclusionRecommendations';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveConclusionRecommendations = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeConclusionRecommendations',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageConclusionRecommendations';
                    window.location = url;
                }
            }, function (error) {

            });
        }

        $scope.SaveFacilitiesArea = function () {
            var config = {
                FacilitiesAreaName: $scope.facilitiesAreaName, FacilitiesAreaDesc: $scope.facilitiesAreaDesc
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveFacilitiesArea',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageFacilitiesArea';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditFacilitiesArea = function (Id) {
            var config = {
                FacilitiesAreaId: Id, FacilitiesAreaName: $scope.facilitiesAreaName, FacilitiesAreaDesc: $scope.facilitiesAreaDesc
            }
            console.log('editFacilitiesArea', config);
            return $http({
                url: '/api/pageview/editFacilitiesArea',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageFacilitiesArea';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveFacilitiesArea = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeFacilitiesArea',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageFacilitiesArea';
                    window.location = url;
                }
            });
        }

        $scope.SaveProcessOverview = function () {
            var config = {
                ProcessOverviewDesc: $scope.processOverviewDesc
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveProcessOverview',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageProcessOverview';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditProcessOverview = function (Id) {
            var config = {
                ProcessOverviewId: Id, ProcessOverviewDesc: $scope.processOverviewDesc
            }
            console.log('editProcessOverview', config);
            return $http({
                url: '/api/pageview/editProcessOverview',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageProcessOverview';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveProcessOverview = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeProcessOverview',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageProcessOverview';
                    window.location = url;
                }
            });
        }

        $scope.SaveDocumentTitle = function () {
            var config = {
                DocumentTitle1: $scope.documentTitle, DocumentDescription: $scope.documentDescription
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveDocumentTitle',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data == "1") {
                    var url = '/Admin/ManageDocumentTitle';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditDocumentTitle = function (Id) {
            var config = {
                DocumentId: Id, DocumentTitle1: $scope.documentTitle, DocumentDescription: $scope.documentDescription
            }
            console.log('editDocumentTitle', config);
            return $http({
                url: '/api/pageview/editDocumentTitle',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDocumentTitle';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditSetting = function (Id) {
            var config = {
                SettingID: Id, SettingType: $scope.SettingType, SettingValue: $scope.SettingValue
            }
            console.log('editSetting', config);
            return $http({
                url: '/api/pageview/editSetting',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ImportantSettings';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };
        $scope.RemoveDocumentTitle = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeDocumentTitle',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageDocumentTitle';
                    window.location = url;
                }
            });
        }

        $scope.SaveManufacturer = function () {
            var config = {
                ManufacturerName: $scope.manufacturerName
            }
            console.log('saveConclusionRecommendations', config);
            return $http({
                url: '/api/pageview/saveManufacturer',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageManufacturer';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.EditManufacturer = function (Id) {
            var config = {
                ManufacturerId: Id, ManufacturerName: $scope.manufacturerName
            }
            console.log('editManufacturer', config);
            return $http({
                url: '/api/pageview/editManufacturer',
                method: "POST",
                data: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageManufacturer';
                    window.location = url;
                }
            }, function (error) {
                alert(error);
            });
        };

        $scope.RemoveManufacturer = function (id) {
            var config = { id: id }
            console.log('return config--', config);
            return $http({
                url: '/api/pageview/removeManufacturer',
                method: "POST",
                params: config,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function (response) {
                if (response.data === "Ok") {
                    var url = '/Admin/ManageManufacturer';
                    window.location = url;
                }
            });
        }
    }
})();
