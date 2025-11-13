(function () {
    'use strict';

    angular.module('myApp')
        .controller('palletRackIncidentCtrl', ['$scope', '$http', function ($scope, $http) {

            $scope.previewPhotoList = [];

            $scope.getAllLevels = [];

            // Add numbers 1 to 25
            for (let i = 1; i <= 25; i++) {
                $scope.getAllLevels.push({ name: i.toString(), selected: false });
            }

            // Add extra fixed values
            $scope.getAllLevels.push({ name: 'All', selected: false });
            $scope.getAllLevels.push({ name: 'Several', selected: false });
            $scope.getAllLevels.push({ name: 'Various', selected: false });
            $scope.getAllLevels.push({ name: 'None', selected: false });


            //$scope.getAllBeamLocations = ['Front', 'Rear', 'Both'];

            //$scope.getAllFrameSides = ['Left', 'Right', '', 'None'];


            $scope.getAllBeamLocations = [
                { name: "Front", selected: false },
                { name: "Rear", selected: false },
                { name: "Both", selected: false }
            ];

            $scope.getAllFrameSides = [
                { name: "Left", selected: false },
                { name: "Right", selected: false },
                { name: "Left & Right", selected: false },
                { name: "None", selected: false }
            ];

            init();

            function init() {
                if (window.location.pathname == "/Customer/IncidentReportNew") {
                    GetCustomerLocationByCustomerIdDrpd(0);
                }
                if (window.location.pathname == "/Customer/IncidentReportView") {
                    loadIncidentReportView();
                }
            }

            function GetCustomerLocationByCustomerIdDrpd(id) {
                $http.get('/api/pageview/getCustomerLocationByCustomerIdCustomer').then(function (response) {
                    $scope.getCustomerLocationID = response.data;
                    console.log('getCustomerLocationByCustomerIdDrpd--', $scope.getCustomerLocationID);
                }, function (response) {
                    $scope.waiting = false;
                });
            }
            // Helper to collect checkbox selections
            function collectSelected(list) {
                if (!list) return '';
                return list.filter(function (item) {
                    return item.selected;
                }).map(function (item) {
                    return item.name || item.id || '';
                }).join(',');
            }

            // Add file to preview list
            $scope.handleFileSelect = function (files) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    var reader = new FileReader();
                    reader.onload = (function (f) {
                        return function (e) {
                            $scope.$apply(function () {
                                $scope.previewPhotoList.push({
                                    file: f,
                                    src: e.target.result
                                });
                            });
                        };
                    })(file);
                    reader.readAsDataURL(file);
                }
            };

            // Remove file before upload
            $scope.removePhoto = function (index) {
                $scope.previewPhotoList.splice(index, 1);
            };

            $scope.onPhotosSelected = function (files) {
                $scope.previewPhotoList = [];
                if (!files || !files.length) return;
                for (var i = 0; i < files.length; i++) {
                    $scope.previewPhotoList.push({ file: files[i], name: files[i].name, size: files[i].size });
                }
                $scope.$applyAsync();
            };
            // Save Incident Report
            $scope.SaveIncidentReport = function () {
                var fd = new FormData();

                // Basic fields
                fd.append('IncidentType', $scope.IncidentReportType || '');
                fd.append('CustomerId', $scope.customerId || '');
                fd.append('CustomerLocationId', $scope.customerLocationId || '');

                // Date handling
                if ($scope.incidentDate) {
                    try {
                        var d = new Date($scope.incidentDate);
                        fd.append('IncidentDate', d.toISOString());
                    } catch (e) {
                        fd.append('IncidentDate', $scope.incidentDate);
                    }
                } else {
                    fd.append('IncidentDate', '');
                }

                fd.append('IncidentNumber', $scope.incidentnumber || '');
                fd.append('IncidentReportedBy', $scope.reportedby || '');
                fd.append('IncidentArea', $scope.area || '');
                fd.append('IncidentRow', $scope.row || '');
                fd.append('IncidentAisle', $scope.aisle || '');
                fd.append('IncidentBay', $scope.bay || '');

                // Checkbox list values
                fd.append('IncidentLevel', collectSelected($scope.getAllLevels));
                fd.append('IncidentBeamLocation', collectSelected($scope.getAllBeamLocations));
                fd.append('IncidentFrameSide', collectSelected($scope.getAllFrameSides));

                fd.append('IncidentSummary', $scope.incidentSummary || '');

                // Images
                if ($scope.previewPhotoList && $scope.previewPhotoList.length) {
                    angular.forEach($scope.previewPhotoList, function (p) {
                        if (p && p.file) {
                            fd.append('photos', p.file);
                        }
                    });
                }

                // Call API
                $http.post('/api/pageview/saveIncidentByCustomerId', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (res) {
                    if (res.data && res.data.success) {
                        /*alert('Incident saved. ID: ' + res.data.id);*/
                        window.location.href = '/Customer/IncidentReport';
                    } else {
                        $scope.errorNot = (res.data && res.data.message) || 'Failed to save incident.';
                    }
                }, function (err) {
                    console.error(err);
                    $scope.errorNot = 'Error saving incident.';
                });
            };


           function loadIncidentReportView () {
               var id = getParameterByName('id');
               console.log('in Load Incident Reeport View', id);
               $http.get('/api/pageview/getIncidentReportById?id=' + id)
                    .then(function (res) {
                        $scope.incident = res.data;
                    }, function (err) {
                        console.error('Error loading incident report', err);
                    });
            };

            $scope.printIncidentReport = function () {
                var printContents = document.getElementById('printableArea').innerHTML || document.body.innerHTML;
                var popupWin = window.open('', '_blank', 'width=900,height=650');
                popupWin.document.open();
                popupWin.document.write('<html><head><title>Print Incident Report</title></head><body onload="window.print()">' + printContents + '</body></html>');
                popupWin.document.close();
            };

            function getParameterByName(name) {
                var url = window.location.href;
                name = name.replace(/[\[\]]/g, '\\$&');
                var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, ' '));
            }

            //function printDiv(divId) {
            //    var printContents = document.getElementById(divId).innerHTML;
            //    var originalContents = document.body.innerHTML;

            //    // Replace body content with only the div's content
            //    document.body.innerHTML = printContents;

            //    // Trigger print
            //    window.print();

            //    // Restore the original body content after print
            //    document.body.innerHTML = originalContents;

            //    // Reload scripts & styles if needed
            //    location.reload();
            //}

        }]);

})();
