// NgScript/Controller/inventoryFilesCtrl.js
// Admin / internal-staff (UserType 1,2,3,5,6,7).
// Scoped to ONE customer &mdash; customerId passed from URL ?id= set in
// ManageCustomer.cshtml: @Url.Action("Admin_InventoryFileCreate","Admin")?id={{d.CustomerID}}

var app = angular.module('myApp');


app.controller('inventoryFilesCtrl',
    ['$scope', '$http', '$timeout',
    function ($scope, $http, $timeout) {

    console.log('----------- inventoryFilesCtrl loaded -----------');

    //  ui-bootstrap pagination 
    $scope.viewby       = '10';
    $scope.currentPage  = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize      = '10';
    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage  = 1;
    };

    //  Page state 
    $scope.search          = '';
    $scope.pageError       = '';
    $scope.pageSuccess     = '';
    $scope.fileList        = [];
    $scope.filteredFiles   = [];
    $scope.customerInfo    = {};

    // Upload form
    $scope.upload             = {};
    $scope.pendingFile        = null;
    $scope.uploading          = false;
    $scope.uploadProgress     = 0;
    $scope.uploadError        = '';
    $scope.uploadSuccess      = '';
    $scope.uploadLocationList = [];
    $scope.uploadFacilityList = [];
    $scope.uploadAreaList     = [];

    // Edit modal
    $scope.showEditFileModal  = false;
    $scope.editFile           = {};
    $scope.editFileError      = '';
    $scope.savingFile         = false;
    $scope.editLocationList   = [];
    $scope.editFacilityList   = [];
    $scope.editAreaList       = [];

    // Delete modal
    $scope.showDeleteModal = false;
    $scope.fileToDelete    = {};
    $scope.deleting        = false;

    // Filters
    $scope.filterLocationId = '';
    $scope.filterFacilityId = '';
    $scope.filterStatus     = '';

    //  Watch customerId (bound via ng-init in view) 
    $scope.$watch('customerId', function (val) {
        if (!val) return;
        loadCustomerInfo(val);
        loadLocationsForCustomer(val);
        $scope.LoadFiles();
    });

    //  Load customer banner info 
    function loadCustomerInfo(customerId) {
        $http.get('/api/pageview/getCustomerById', { params: { customerId: customerId } })
            .then(function (r) { $scope.customerInfo = r.data; })
            .catch(function () { /* non-fatal &mdash; banner just stays hidden */ });
    }

    //  Load locations scoped to this customer 
    // Uses your existing getCustomerLocationByCustomerId endpoint
    // which is already confirmed working in the browser console.
    function loadLocationsForCustomer(customerId) {
        $http.get('/api/pageview/getCustomerLocationByCustomerId', { params: { id: customerId } })
            .then(function (r) {
                // Normalise to {LocationID, LocationName} regardless of
                // which property names the existing endpoint returns.
                $scope.uploadLocationList = (r.data || []).map(function (l) {
                    return {
                        LocationID:   l.CustomerLocationID || l.LocationID   || l.Id,
                        LocationName: l.LocationName       || l.Name         || ''
                    };
                });
                // Pre-populate edit modal location list too
                $scope.editLocationList = $scope.uploadLocationList.slice();
            })
            .catch(function () {});
    }

    //  Upload form cascade: Location → Facility 
    $scope.OnLocationChange = function () {
        $scope.upload.facilityId  = '';
        $scope.upload.areaId      = '';
        $scope.uploadFacilityList = [];
        $scope.uploadAreaList     = [];
        if (!$scope.upload.locationId) return;

        $http.get('/api/pageview/getInventoryFacilities',
            { params: { locationId: $scope.upload.locationId } })
            .then(function (r) {
                $scope.uploadFacilityList = r.data || [];
            })
            .catch(function () {});
    };

    //  Upload form cascade: Facility → Area 
    $scope.OnFacilityChange = function () {
        $scope.upload.areaId  = '';
        $scope.uploadAreaList = [];
        if (!$scope.upload.facilityId) return;

        $http.get('/api/pageview/getInventoryAreas',
            { params: { facilityId: $scope.upload.facilityId } })
            .then(function (r) {
                $scope.uploadAreaList = r.data || [];
            })
            .catch(function () {});
    };

    //  Client-side filter (location / facility / status) 
    $scope.ApplyClientFilter = function () {
        if (!Array.isArray($scope.fileList)) {
            $scope.filteredFiles = [];
            return;
        }
        $scope.filteredFiles = $scope.fileList.filter(function (f) {
            if ($scope.filterLocationId &&
                String(f.LocationID) !== String($scope.filterLocationId)) return false;
            if ($scope.filterFacilityId &&
                String(f.FacilityID) !== String($scope.filterFacilityId)) return false;
            if ($scope.filterStatus && f.Status !== $scope.filterStatus) return false;
            return true;
        });
        $scope.currentPage = 1;
    };

    //  READ: load files for this customer 
    $scope.LoadFiles = function () {
        if (!$scope.customerId) return;
        $scope.pageError = '';

        $http.post('/api/pageview/getInventoryFilesFiltered',
            { LocationID: null, FacilityID: null, AreaID: null },
            { params: { customerId: $scope.customerId } })
            .then(function (res) {
                // Guard: ensure we always have an array
                var data = res.data;
                $scope.fileList      = Array.isArray(data) ? data : [];
                $scope.filteredFiles = $scope.fileList.slice();
                $scope.ApplyClientFilter();
            })
            .catch(function (err) {
                $scope.fileList      = [];
                $scope.filteredFiles = [];
                $scope.pageError = 'Could not load files: ' +
                    (err.data ? (err.data.Message || JSON.stringify(err.data))
                               : err.statusText);
            });
    };

    //  CREATE: select file 
    $scope.HandleFileSelect = function (files) {
        if (files && files[0]) {
            $timeout(function () { $scope.pendingFile = files[0]; });
        }
    };

    //  CREATE: upload Excel file 
    $scope.UploadFile = function () {
        $scope.uploadError = '';

        if (!$scope.upload.locationId) {
            $scope.uploadError = 'Please select a location.'; return;
        }
        if (!$scope.pendingFile) {
            $scope.uploadError = 'Please select a file (.xlsx).'; return;
        }
        if (!$scope.pendingFile.name.toLowerCase().endsWith('.xlsx')) {
            $scope.uploadError = 'Only .xlsx files are accepted.'; return;
        }

        $scope.uploading = true;
        $scope.uploadProgress = 5;

        var fd = new FormData();
        fd.append('file',        $scope.pendingFile);
        fd.append('customerId',  $scope.customerId);
        fd.append('locationId',  $scope.upload.locationId);
        if ($scope.upload.facilityId)
            fd.append('facilityId',  $scope.upload.facilityId);
        if ($scope.upload.areaId)
            fd.append('areaId',      $scope.upload.areaId);
        if ($scope.upload.description)
            fd.append('description', $scope.upload.description);

        $http.post('/api/pageview/uploadInventoryFile', fd, {
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity,
            uploadEventHandlers: {
                progress: function (e) {
                    if (e.lengthComputable) {
                        $timeout(function () {
                            $scope.uploadProgress =
                                Math.round((e.loaded / e.total) * 90) + 5;
                        });
                    }
                }
            }
        }).then(function (res) {
            $timeout(function () {
                $scope.uploadProgress = 100;
                $scope.uploading      = false;
                $scope.uploadSuccess  = 'File uploaded and imported successfully!';
                $scope.pendingFile    = null;
                $scope.upload         = {};
                $scope.uploadFacilityList = [];
                $scope.uploadAreaList     = [];
                $scope.LoadFiles();
            });
            $timeout(function () { $scope.uploadSuccess = ''; }, 4000);
        }).catch(function (err) {
            $timeout(function () {
                $scope.uploading   = false;
                $scope.uploadError = 'Upload failed: ' +
                    (err.data ? (err.data.Message || JSON.stringify(err.data))
                               : err.statusText);
            });
        });
    };

    $scope.ClearUpload = function () {
        $scope.upload             = {};
        $scope.pendingFile        = null;
        $scope.uploadError        = '';
        $scope.uploadSuccess      = '';
        $scope.uploadFacilityList = [];
        $scope.uploadAreaList     = [];
        $scope.uploadProgress     = 0;
    };

    //  UPDATE: open Edit modal 
    $scope.OpenEditFileModal = function (f) {
        $scope.editFile = {
            FileID:      f.FileID,
            FileName:    f.FileName,
            LocationID:  f.LocationID  ? String(f.LocationID)  : '',
            FacilityID:  f.FacilityID  ? String(f.FacilityID)  : '',
            AreaID:      f.AreaID      ? String(f.AreaID)       : '',
            Status:      f.Status,
            Description: f.Description || ''
        };

        $scope.editFileError    = '';
        $scope.editLocationList = $scope.uploadLocationList.slice();
        $scope.editFacilityList = [];
        $scope.editAreaList     = [];

        if (f.LocationID) {
            $http.get('/api/pageview/getInventoryFacilities',
                { params: { locationId: f.LocationID } })
                .then(function (r) { $scope.editFacilityList = r.data || []; })
                .catch(function () {});
        }
        if (f.FacilityID) {
            $http.get('/api/pageview/getInventoryAreas',
                { params: { facilityId: f.FacilityID } })
                .then(function (r) { $scope.editAreaList = r.data || []; })
                .catch(function () {});
        }
        $scope.showEditFileModal = true;
    };

    //  Cascade in edit modal: Location → Facility 
    $scope.OnEditLocationChange = function () {
        $scope.editFile.FacilityID = '';
        $scope.editFile.AreaID     = '';
        $scope.editFacilityList    = [];
        $scope.editAreaList        = [];
        if (!$scope.editFile.LocationID) return;

        $http.get('/api/pageview/getInventoryFacilities',
            { params: { locationId: $scope.editFile.LocationID } })
            .then(function (r) { $scope.editFacilityList = r.data || []; })
            .catch(function () {});
    };

    //  Cascade in edit modal: Facility → Area 
    $scope.OnEditFacilityChange = function () {
        $scope.editFile.AreaID = '';
        $scope.editAreaList    = [];
        if (!$scope.editFile.FacilityID) return;

        $http.get('/api/pageview/getInventoryAreas',
            { params: { facilityId: $scope.editFile.FacilityID } })
            .then(function (r) { $scope.editAreaList = r.data || []; })
            .catch(function () {});
    };

    $scope.CloseEditFileModal = function () {
        $scope.showEditFileModal = false;
        $scope.editFile          = {};
    };

    $scope.SaveFileMetadata = function () {
        if (!$scope.editFile.LocationID) {
            $scope.editFileError = 'Location is required.'; return;
        }
        $scope.editFileError = '';
        $scope.savingFile    = true;

        $http.put('/api/pageview/updateInventoryFileMetadata', $scope.editFile)
            .then(function () {
                $scope.savingFile        = false;
                $scope.showEditFileModal = false;
                $scope.pageSuccess       = 'File details updated successfully.';
                $scope.LoadFiles();
                $timeout(function () { $scope.pageSuccess = ''; }, 3000);
            })
            .catch(function (err) {
                $scope.savingFile    = false;
                $scope.editFileError = 'Save failed: ' +
                    (err.data ? (err.data.Message || JSON.stringify(err.data))
                               : err.statusText);
            });
    };

    //  DELETE 
    $scope.ConfirmDelete = function (f) {
        $scope.fileToDelete    = f;
        $scope.showDeleteModal = true;
    };

    $scope.DeleteFile = function () {
        $scope.deleting = true;
        $http.delete('/api/pageview/deleteInventoryFile',
            { params: { fileId: $scope.fileToDelete.FileID } })
            .then(function () {
                $scope.deleting        = false;
                $scope.showDeleteModal = false;
                $scope.pageSuccess     = 'File deleted successfully.';
                $scope.LoadFiles();
                $timeout(function () { $scope.pageSuccess = ''; }, 3000);
            })
            .catch(function (err) {
                $scope.deleting  = false;
                $scope.pageError = 'Delete failed: ' +
                    (err.data ? (err.data.Message || JSON.stringify(err.data))
                               : err.statusText);
            });
    };

    //  EXPORT 
    $scope.ExportFile = function (fileId) {
        window.location.href =
            '/api/pageview/exportInventoryFile?fileId=' + fileId;
    };

}]);
