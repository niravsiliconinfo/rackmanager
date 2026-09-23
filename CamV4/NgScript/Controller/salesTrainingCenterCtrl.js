// ============================================================
// salesTrainingCenterCtrl.js
// Training Center Module - all pages in one controller
// ============================================================

try {
    angular.module('myApp').filter('startFrom', function () {
        return function (input, start) {
            if (input) { start = +start; return input.slice(start); }
            return [];
        };
    });
} catch (e) { }

angular.module('myApp')
    .controller('salesTrainingCenterCtrl', function ($scope, $http, $window) {

        var path = window.location.pathname.toLowerCase();

        $scope.courses = [];
        $scope.registrations = [];
        $scope.webinars = [];
        $scope.blogs = [];
        $scope.talks = [];

        $scope.course = {};
        $scope.registration = {};
        $scope.webinar = {};
        $scope.blog = {};
        $scope.talk = {};

        $scope.loading = false;

        $scope.viewby = 10;
        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;
        $scope.maxSize = 10;

        $scope.setPage = function (page) {
            $scope.currentPage = page;
        };

        $scope.pageChanged = function () { };

        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1;
        };

        function getIdFromUrl() {
            var m = window.location.search.match(/[?&]id=([^&]*)/);
            return m ? m[1] : null;
        }

        // ---- Paging ----
        $scope.paging = { current: 1, size: 10 };
        $scope.getPages = function (total, size) {
            var p = []; for (var i = 1; i <= Math.ceil(total / size); i++) p.push(i); return p;
        };
        $scope.setPage = function (p) { $scope.paging.current = p; };
        $scope.prevPage = function () { if ($scope.paging.current > 1) $scope.paging.current--; };
        $scope.nextPage = function (total) {
            if ($scope.paging.current < Math.ceil(total / $scope.paging.size)) $scope.paging.current++;
        };

        function init() {
            console.log(path);
            // ---------------- COURSES ----------------
            if (path.indexOf('/salesmanager/managetrainingcoursesales') !== -1) {
                loadCourses();
            }

            if (path.indexOf('/salesmanager/viewtrainingcoursesales') !== -1) {
                loadCourseById(getIdFromUrl());
            }

            // ---------------- REGISTRATION ----------------
            if (path.indexOf('/salesmanager/trainingcenterregistrationsales') !== -1) {
                loadRegistrations();
            }

            if (path.indexOf('/salesmanager/viewtrainingregistrationsales') !== -1) {
                loadRegistrationById(getIdFromUrl());
            }

            // ---------------- WEBINAR ----------------
            if (path.indexOf('/salesmanager/trainingcenteradditionalresourcessales') !== -1) {
                loadWebinars();
            }

            if (path.indexOf('/salesmanager/viewtrainingwebinarsales') !== -1) {
                loadWebinarById(getIdFromUrl());
            }

            // ---------------- BLOG ----------------
            if (path.indexOf('/salesmanager/trainingcenteradditionalresourcessales') !== -1) {
                loadBlogs();
            }

            if (path.indexOf('/salesmanager/viewtrainingblogsales') !== -1) {
                loadBlogById(getIdFromUrl());
            }

            // ---------------- TECH TALK ----------------
            if (path.indexOf('/salesmanager/trainingcentertechnicaltalksales') !== -1) {
                loadTalks();
            }

            if (path.indexOf('/salesmanager/viewtrainingtechnicaltalksales') !== -1) {
                loadTalkById(getIdFromUrl());
            }
        }

        init();

        function loadCourses() {

            $scope.loading = true;

            $http.get('/api/pageview/tc_getSalesCourses')
                .then(function (res) {
                    $scope.courses = res.data;
                })
                .finally(function () {
                    $scope.loading = false;
                });
        }

        function loadCourseById(id) {

            $http.get('/api/pageview/tc_getSalesCourseById', {
                params: { id: id }
            }).then(function (res) {

                $scope.course = res.data;

            });
        }

        $scope.viewCourse = function (id) {
            $window.location.href =
                "/SalesManager/ViewTrainingCourseSales?id=" + id;
        };
        function loadRegistrations() {

            $scope.loading = true;

            $http.get('/api/pageview/tc_getSalesRegistrations')
                .then(function (res) {
                    $scope.registrationList = res.data;
                })
                .finally(function () {
                    $scope.loading = false;
                });
        }

        function loadRegistrationById(id) {

            $http.get('/api/pageview/tc_getSalesRegistrationById', {
                params: { id: id }
            }).then(function (res) {

                $scope.registration = res.data;

            });
        }

        $scope.viewRegistration = function (id) {

            $window.location.href =
                "/SalesManager/ViewTrainingRegistrationSales?id=" + id;

        };

        function loadWebinars() {

            $scope.loading = true;

            $http.get('/api/pageview/tc_getSalesWebinars')
                .then(function (res) {
                    $scope.webinarList = res.data;
                })
                .finally(function () {
                    $scope.loading = false;
                });
        }

        function loadWebinarById(id) {

            $http.get('/api/pageview/tc_getSalesWebinarById', {
                params: { id: id }
            }).then(function (res) {

                $scope.webinar = res.data;

            });
        }

        $scope.viewWebinar = function (id) {

            $window.location.href =
                "/SalesManager/ViewTrainingWebinarSales?id=" + id;

        };

        function loadBlogs() {

            $scope.loading = true;

            $http.get('/api/pageview/tc_getSalesBlogs')
                .then(function (res) {
                    $scope.blogList = res.data;
                })
                .finally(function () {
                    $scope.loading = false;
                });
        }

        function loadBlogById(id) {

            $http.get('/api/pageview/tc_getSalesBlogById', {
                params: { id: id }
            }).then(function (res) {

                $scope.blog = res.data;

            });
        }

        $scope.viewBlog = function (id) {

            $window.location.href =
                "/SalesManager/ViewTrainingBlogSales?id=" + id;

        };

        function loadTalks() {

            $scope.loading = true;

            $http.get('/api/pageview/tc_getSalesTechnicalTalks')
                .then(function (res) {
                    $scope.talkList = res.data;
                })
                .finally(function () {
                    $scope.loading = false;
                });
        }

        function loadTalkById(id) {

            $http.get('/api/pageview/tc_getSalesTechnicalTalkById', {
                params: { id: id }
            }).then(function (res) {

                $scope.talk = res.data;

            });
        }

        $scope.viewTalk = function (id) {

            $window.location.href =
                "/SalesManager/ViewTrainingTechnicalTalkSales?id=" + id;

        };       
    });