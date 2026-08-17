// ============================================================
// trainingCenterCtrl.js
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
    .controller('trainingCenterCtrl', function ($scope, $http, $window) {

        var path = window.location.pathname.toLowerCase();

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

        $scope.StartNewRegistration = function () {
            $scope.showAddForm = true;
            $scope.editRegId = 0;
            $scope.persons = [];
            $scope.formError = '';
            $scope.formSuccess = '';
        };

        // ============================================================
        // CUSTOMER - REGISTRATION
        // /Customer/TrainingCenterRegistration
        // ============================================================
        if (path.indexOf('trainingcenterregistration') !== -1 && path.indexOf('admin') === -1) {

            $scope.courseList = [];
            $scope.persons = [];        // persons being built for new/edit registration
            $scope.registrationList = [];        // existing registrations for this customer
            $scope.saving = false;
            $scope.formError = '';
            $scope.formSuccess = '';
            $scope.showAddForm = false;     // toggle to show registration builder
            $scope.editRegId = 0;
            $scope.newPerson = { ContactName: '', ContactEmail: '', TrainingCourseID: '' };
            $scope.editPersonIndex = -1;

            // Load active courses (for the registration dropdown only)
            $http.get('/api/pageview/tc_getAllCourses').then(function (res) {
                $scope.courseList = res.data || [];
            });

            // Load existing registrations
            var loadRegistrations = function () {
                $http.get('/api/pageview/tc_getMyRegistrations').then(function (res) {
                    $scope.registrationList = res.data || [];
                    $scope.paging.current = 1;
                });
            };
            loadRegistrations();

            // ---- Course helpers ----
            $scope.getCourseById = function (id) {
                return $scope.courseList.find(function (c) { return c.TrainingCourseID == id; }) || {};
            };
            $scope.totalPrice = function () {
                return $scope.persons.reduce(function (sum, p) {
                    var c = $scope.getCourseById(p.TrainingCourseID);
                    return sum + (c.Price || 0);
                }, 0);
            };

            // ---- Person management ----
            $scope.AddPerson = function () {
                $scope.formError = '';
                if (!$scope.newPerson.ContactName.trim()) { $scope.formError = 'Name is required.'; return; }
                if (!$scope.newPerson.ContactEmail.trim()) { $scope.formError = 'Email is required.'; return; }
                if (!$scope.newPerson.TrainingCourseID) { $scope.formError = 'Course is required.'; return; }
                var course = $scope.getCourseById($scope.newPerson.TrainingCourseID);
                if ($scope.editPersonIndex >= 0) {
                    $scope.persons[$scope.editPersonIndex].ContactName = $scope.newPerson.ContactName;
                    $scope.persons[$scope.editPersonIndex].ContactEmail = $scope.newPerson.ContactEmail;
                    $scope.persons[$scope.editPersonIndex].TrainingCourseID = $scope.newPerson.TrainingCourseID;
                    $scope.persons[$scope.editPersonIndex].CourseName = course.CourseName;
                    $scope.persons[$scope.editPersonIndex].CoursePrice = course.Price;
                    $scope.editPersonIndex = -1;
                } else {
                    $scope.persons.push({
                        TrainingRegistrationPersonID: 0,
                        ContactName: $scope.newPerson.ContactName,
                        ContactEmail: $scope.newPerson.ContactEmail,
                        TrainingCourseID: $scope.newPerson.TrainingCourseID,
                        CourseName: course.CourseName,
                        CoursePrice: course.Price
                    });
                }
                $scope.newPerson = { ContactName: '', ContactEmail: '', TrainingCourseID: '' };
            };

            $scope.EditPerson = function (index) {
                var p = $scope.persons[index];
                $scope.newPerson = {
                    ContactName: p.ContactName, ContactEmail: p.ContactEmail,
                    TrainingCourseID: p.TrainingCourseID
                };
                $scope.editPersonIndex = index;
            };

            $scope.RemovePerson = function (index) { $scope.persons.splice(index, 1); };
            $scope.CancelPerson = function () {
                $scope.newPerson = { ContactName: '', ContactEmail: '', TrainingCourseID: '' };
                $scope.editPersonIndex = -1;
            };

            // ---- Finish Registration ----
            $scope.FinishRegistration = function () {
                $scope.formError = $scope.formSuccess = '';
                if ($scope.persons.length === 0) { $scope.formError = 'Add at least one person.'; return; }
                $scope.saving = true;

                $http({
                    url: '/api/pageview/tc_saveRegistration', method: 'POST',
                    data: {
                        TrainingRegistrationID: $scope.editRegId || 0,
                        CustomerID: 0,
                        Persons: $scope.persons.map(function (p) {
                            return {
                                TrainingRegistrationPersonID: p.TrainingRegistrationPersonID || 0,
                                ContactName: p.ContactName,
                                ContactEmail: p.ContactEmail,
                                TrainingCourseID: parseInt(p.TrainingCourseID)
                            };
                        })
                    },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    $scope.saving = false;
                    if (res.data && res.data.success) {
                        $scope.formSuccess = 'Registration submitted! Confirmation emails have been sent.';
                        $scope.persons = [];
                        $scope.editRegId = 0;
                        $scope.showAddForm = false;
                        loadRegistrations();
                    } else {
                        $scope.formError = (res.data && res.data.message) || 'Failed to save.';
                    }
                }, function () { $scope.saving = false; $scope.formError = 'An error occurred.'; });
            };

            // ---- Edit existing registration ----
            $scope.StartEdit = function (id) {
                $http.get('/api/pageview/tc_getRegistrationById', { params: { id: id } })
                    .then(function (res) {
                        var r = res.data;
                        $scope.editRegId = r.TrainingRegistrationID;
                        $scope.showAddForm = true;
                        $scope.persons = (r.Persons || []).map(function (p) {
                            return {
                                TrainingRegistrationPersonID: p.TrainingRegistrationPersonID,
                                ContactName: p.ContactName,
                                ContactEmail: p.ContactEmail,
                                TrainingCourseID: p.TrainingCourseID,
                                CourseName: p.CourseName,
                                CoursePrice: p.CoursePrice
                            };
                        });
                        window.scrollTo(0, 0);
                    });
            };

            $scope.CancelRegistration = function () {
                $scope.persons = [];
                $scope.editRegId = 0;
                $scope.showAddForm = false;
                $scope.formError = $scope.formSuccess = '';
            };
        }

        // ============================================================
        // CUSTOMER - COURSE STATUS
        // /Customer/TrainingCenterCourseStatus
        // ============================================================
        if (path.indexOf('trainingcentercoursestatus') !== -1 && path.indexOf('admin') === -1) {

            $scope.registrationList = [];
            $scope.selectedReg = null;

            var regId = getIdFromUrl();

            if (regId) {
                $http.get('/api/pageview/tc_getRegistrationById', { params: { id: regId } })
                    .then(function (res) { $scope.selectedReg = res.data; },
                        function () { $scope.selectedReg = null; });
            } else {
                $http.get('/api/pageview/tc_getMyRegistrations').then(function (res) {
                    $scope.registrationList = res.data || [];
                });
            }

            $scope.ViewRegistration = function (id) {
                $window.location.href = '/Customer/TrainingCenterCourseStatus?id=' + id;
            };
        }

        // ============================================================
        // CUSTOMER - ADDITIONAL RESOURCES (Webinar + Blog)
        // /Customer/TrainingCenterAdditionalResources
        // ============================================================
        if (path.indexOf('trainingcenteradditionalresources') !== -1 && path.indexOf('admin') === -1) {
            $scope.webinarList = [];
            $scope.blogList = [];
            $http.get('/api/pageview/tc_getAllWebinars').then(function (res) { $scope.webinarList = res.data || []; });
            $http.get('/api/pageview/tc_getAllBlogs').then(function (res) { $scope.blogList = res.data || []; });
        }
        // ============================================================
        // CUSTOMER - TECHNICAL TALK
        // /Customer/TrainingCenterTechnicalTalk
        // ============================================================
        if (path.indexOf('trainingcentertechnicaltalk') !== -1 && path.indexOf('admin') === -1) {

            // --- Published Q&A (existing) ---
            $scope.talkList = [];
            $scope.newQuestion = '';
            $scope.questionError = '';
            $scope.questionSent = false;

            $http.get('/api/pageview/tc_getPublishedTalks').then(function (res) {
                $scope.talkList = res.data || [];
            });

            // --- My submitted questions (NEW) ---
            $scope.myQuestions = [];

            var loadMyQuestions = function () {
                $http.get('/api/pageview/tc_getMyQuestions').then(function (res) {
                    $scope.myQuestions = res.data || [];
                });
            };
            loadMyQuestions();

            // SubmitQuestion � extended (was already partially stubbed)
            $scope.SubmitQuestion = function () {
                $scope.questionError = '';
                if (!$scope.newQuestion.trim()) {
                    $scope.questionError = 'Please enter your question.';
                    return;
                }
                $http({
                    url: '/api/pageview/tc_submitQuestion', method: 'POST',
                    data: { question: $scope.newQuestion.trim() },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') {
                        $scope.questionSent = true;
                        $scope.newQuestion = '';
                        loadMyQuestions();          // refresh "My Questions" list
                    } else {
                        $scope.questionError = res.data || 'Failed to submit.';
                    }
                }, function () {
                    $scope.questionError = 'An error occurred.';
                });
            };
        }

        // ============================================================
        // ADMIN - COURSES
        // /Admin/TrainingCenterCourses
        // ============================================================
        if (path.indexOf('trainingcentercourses') !== -1 && path.indexOf('admin') !== -1) {

            $scope.courseList = [];
            $scope.courseForm = {};
            $scope.showCourseForm = false;
            $scope.courseError = '';
            $scope.courseSuccess = '';

            var loadCourses = function () {
                $http.get('/api/pageview/tc_getAllCourses?activeOnly=false').then(function (res) {
                    $scope.courseList = res.data || [];
                });
            };
            loadCourses();

            $scope.OpenCourseForm = function (c) {
                $scope.courseError = $scope.courseSuccess = '';
                $scope.courseForm = c
                    ? angular.copy(c)
                    : { TrainingCourseID: 0, CourseName: '', CourseCode: '', Description: '', Price: '', IsActive: true };
                $scope.showCourseForm = true;
            };
            $scope.CloseCourseForm = function () { $scope.showCourseForm = false; $scope.courseError = $scope.courseSuccess = ''; };

            $scope.SaveCourse = function () {
                $scope.courseError = $scope.courseSuccess = '';
                if (!$scope.courseForm.CourseName) { $scope.courseError = 'Course name is required.'; return; }
                if (!$scope.courseForm.CourseCode) { $scope.courseError = 'Course code is required.'; return; }
                if (!$scope.courseForm.Price) { $scope.courseError = 'Price is required.'; return; }
                $http({
                    url: '/api/pageview/tc_saveCourse', method: 'POST',
                    data: $scope.courseForm, headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') { $scope.courseSuccess = 'Course saved successfully.'; $scope.showCourseForm = false; loadCourses(); }
                    else { $scope.courseError = res.data; }
                }, function () { $scope.courseError = 'An error occurred.'; });
            };

            $scope.ToggleCourse = function (id) {
                $http({
                    url: '/api/pageview/tc_toggleCourse', method: 'POST',
                    data: { id: id }, headers: { 'Content-Type': 'application/json' }
                }).then(function () { loadCourses(); });
            };
        }

        // ============================================================
        // ADMIN - REGISTRATION LISTING
        // /Admin/TrainingCenterRegistration
        // ============================================================
        if (path.indexOf('trainingcenterregistration') !== -1 && path.indexOf('admin') !== -1) {

            $scope.registrationList = [];
            $scope.filteredList = [];
            $scope.statusFilter = '';

            var loadAllReg = function () {
                $http.get('/api/pageview/tc_getAllRegistrations').then(function (res) {
                    $scope.registrationList = res.data || [];
                    $scope.filteredList = $scope.registrationList;
                    $scope.paging.current = 1;
                });
            };
            loadAllReg();

            $scope.FilterByStatus = function () {
                $scope.filteredList = !$scope.statusFilter
                    ? $scope.registrationList
                    : $scope.registrationList.filter(function (r) { return r.Status === $scope.statusFilter; });
                $scope.paging.current = 1;
            };
        }

        // ============================================================
        // ADMIN - PERSON STATUS + CERTIFICATE
        // /Admin/TrainingCenterPersonStatus
        // ============================================================
        if (path.indexOf('trainingcenterpersonstatus') !== -1) {

            $scope.registration = null;
            $scope.editPerson = null;
            $scope.personError = '';
            $scope.personSuccess = '';
            $scope.regStatusForm = { status: '', notes: '' };
            $scope.regError = '';
            $scope.regSuccess = '';
            $scope.saving = false;

            var regId = getIdFromUrl();

            var loadReg = function () {
                $http.get('/api/pageview/tc_getRegistrationById', { params: { id: regId } })
                    .then(function (res) {
                        $scope.registration = res.data;
                        $scope.regStatusForm.status = res.data.Status;
                        $scope.regStatusForm.notes = res.data.Notes || '';
                    });
            };
            if (regId) loadReg();

            $scope.OpenEditPerson = function (p) {
                $scope.personError = $scope.personSuccess = '';
                $scope.editPerson = {
                    TrainingRegistrationPersonID: p.TrainingRegistrationPersonID,
                    ContactName: p.ContactName,
                    CourseStatus: p.CourseStatus || 'Incomplete',
                    CertificateExpiryDate: p.CertificateExpiryDate
                        ? String(p.CertificateExpiryDate).substring(0, 10) : ''
                };
            };

            $scope.CloseEditPerson = function () { $scope.editPerson = null; };

            $scope.SavePersonStatus = function () {
                $scope.personError = $scope.personSuccess = '';
                $scope.saving = true;

                var fd = new FormData();
                fd.append('TrainingRegistrationPersonID', $scope.editPerson.TrainingRegistrationPersonID);
                fd.append('CourseStatus', $scope.editPerson.CourseStatus);
                fd.append('CertificateExpiryDate', $scope.editPerson.CertificateExpiryDate || '');
                var fileInput = document.getElementById('certFile');
                if (fileInput && fileInput.files.length > 0)
                    fd.append('file', fileInput.files[0]);

                $http({
                    url: '/api/pageview/tc_updatePersonStatus', method: 'POST',
                    data: fd, transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (res) {
                    $scope.saving = false;
                    if (res.data === 'Ok') {
                        $scope.personSuccess = 'Status updated successfully.';
                        $scope.editPerson = null;
                        loadReg();
                    } else { $scope.personError = res.data || 'Failed.'; }
                }, function () { $scope.saving = false; $scope.personError = 'An error occurred.'; });
            };

            $scope.UpdateRegStatus = function () {
                $scope.regError = $scope.regSuccess = '';
                $http({
                    url: '/api/pageview/tc_updateRegistrationStatus', method: 'POST',
                    data: {
                        id: parseInt(regId), status: $scope.regStatusForm.status,
                        notes: $scope.regStatusForm.notes
                    },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') { $scope.regSuccess = 'Registration status updated.'; loadReg(); }
                    else { $scope.regError = res.data || 'Failed.'; }
                }, function () { $scope.regError = 'An error occurred.'; });
            };
        }

        // ============================================================
        // ADMIN - WEBINAR + BLOG MANAGEMENT
        // /Admin/TrainingCenterAdditionalResources
        // ============================================================
        if (path.indexOf('trainingcenteradditionalresources') !== -1 && path.indexOf('admin') !== -1) {

            $scope.webinarList = []; $scope.blogList = [];
            $scope.webinarForm = {}; $scope.blogForm = {};
            $scope.showWebForm = false; $scope.showBlogForm = false;
            $scope.webError = ''; $scope.webSuccess = '';
            $scope.blogError = ''; $scope.blogSuccess = '';

            var loadWebinars = function () {
                $http.get('/api/pageview/tc_getAllWebinars?activeOnly=false')
                    .then(function (res) { $scope.webinarList = res.data || []; });
            };
            var loadBlogs = function () {
                $http.get('/api/pageview/tc_getAllBlogs?activeOnly=false')
                    .then(function (res) { $scope.blogList = res.data || []; });
            };
            loadWebinars(); loadBlogs();

            // Webinar
            $scope.OpenWebForm = function (w) {
                $scope.webError = $scope.webSuccess = '';
                $scope.webinarForm = w
                    ? angular.copy(w)
                    : { TrainingWebinarID: 0, Title: '', Description: '', ExternalLink: '', DisplayOrder: 0, IsActive: true };
                $scope.showWebForm = true;
            };
            $scope.CloseWebForm = function () { $scope.showWebForm = false; };
            $scope.SaveWebinar = function () {
                $scope.webError = $scope.webSuccess = '';
                if (!$scope.webinarForm.Title) { $scope.webError = 'Title is required.'; return; }
                $http({
                    url: '/api/pageview/tc_saveWebinar', method: 'POST',
                    data: $scope.webinarForm, headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') { $scope.webSuccess = 'Saved.'; $scope.showWebForm = false; loadWebinars(); }
                    else { $scope.webError = res.data; }
                });
            };
            $scope.DeleteWebinar = function (id) {
                if (!confirm('Delete this webinar entry?')) return;
                $http({
                    url: '/api/pageview/tc_deleteWebinar', method: 'POST',
                    data: { id: id }, headers: { 'Content-Type': 'application/json' }
                }).then(function () { loadWebinars(); });
            };

            // Blog
            $scope.OpenBlogForm = function (b) {
                $scope.blogError = $scope.blogSuccess = '';
                $scope.blogForm = b
                    ? angular.copy(b)
                    : { TrainingBlogID: 0, Title: '', Description: '', ExternalLink: '', DisplayOrder: 0, IsActive: true };
                $scope.showBlogForm = true;
            };
            $scope.CloseBlogForm = function () { $scope.showBlogForm = false; };
            $scope.SaveBlog = function () {
                $scope.blogError = $scope.blogSuccess = '';
                if (!$scope.blogForm.Title) { $scope.blogError = 'Title is required.'; return; }
                $http({
                    url: '/api/pageview/tc_saveBlog', method: 'POST',
                    data: $scope.blogForm, headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') { $scope.blogSuccess = 'Saved.'; $scope.showBlogForm = false; loadBlogs(); }
                    else { $scope.blogError = res.data; }
                });
            };
            $scope.DeleteBlog = function (id) {
                if (!confirm('Delete this blog entry?')) return;
                $http({
                    url: '/api/pageview/tc_deleteBlog', method: 'POST',
                    data: { id: id }, headers: { 'Content-Type': 'application/json' }
                }).then(function () { loadBlogs(); });
            };
        }

        // ============================================================
        // ADMIN - TECHNICAL TALK MANAGEMENT
        // /Admin/TrainingCenterTechnicalTalk
        // ============================================================
        if (path.indexOf('trainingcentertechnicaltalk') !== -1 && path.indexOf('admin') !== -1) {

            // ---- State ----
            $scope.talkList = [];
            $scope.talkForm = {};
            $scope.showTalkForm = false;
            $scope.talkError = '';
            $scope.talkSuccess = '';

            // Customer-question answer panel state
            $scope.answerForm = null;   // null = panel closed
            $scope.answerError = '';
            $scope.answerSuccess = '';
            $scope.talkFilter = 'all';  // 'all' | 'unanswered' | 'answered' | 'admin'

            // ---- Filtered view ----
            $scope.filteredTalkList = [];
            var applyFilter = function () {
                var f = $scope.talkFilter;
                if (f === 'all') {
                    $scope.filteredTalkList = $scope.talkList;
                } else if (f === 'unanswered') {
                    $scope.filteredTalkList = $scope.talkList.filter(function (t) {
                        return !t.IsAdminCreated && (!t.Answer || t.Answer.trim() === '');
                    });
                } else if (f === 'answered') {
                    $scope.filteredTalkList = $scope.talkList.filter(function (t) {
                        return !t.IsAdminCreated && t.Answer && t.Answer.trim() !== '';
                    });
                } else if (f === 'admin') {
                    $scope.filteredTalkList = $scope.talkList.filter(function (t) { return t.IsAdminCreated; });
                }
            };

            var loadTalks = function () {
                $http.get('/api/pageview/tc_getAllTalks').then(function (res) {
                    $scope.talkList = res.data || [];
                    applyFilter();
                });
            };
            loadTalks();

            $scope.SetTalkFilter = function (f) {
                $scope.talkFilter = f;
                applyFilter();
            };

            // ---- Knowledge-base Q&A form (admin-authored entries) ----
            $scope.OpenTalkForm = function (t) {
                $scope.talkError = $scope.talkSuccess = '';
                $scope.answerForm = null;   // close answer panel if open
                $scope.talkForm = t
                    ? angular.copy(t)
                    : {
                        TrainingTechnicalTalkID: 0, Question: '', Answer: '',
                        IsPublished: false, DisplayOrder: 0, IsActive: true
                    };
                $scope.showTalkForm = true;
            };
            $scope.CloseTalkForm = function () { $scope.showTalkForm = false; $scope.talkError = $scope.talkSuccess = ''; };

            $scope.SaveTalk = function () {
                $scope.talkError = $scope.talkSuccess = '';
                if (!$scope.talkForm.Question) { $scope.talkError = 'Question is required.'; return; }
                $http({
                    url: '/api/pageview/tc_saveTechnicalTalk', method: 'POST',
                    data: $scope.talkForm, headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') {
                        $scope.talkSuccess = 'Saved successfully.';
                        $scope.showTalkForm = false;
                        loadTalks();
                    } else { $scope.talkError = res.data; }
                }, function () { $scope.talkError = 'An error occurred.'; });
            };

            $scope.DeleteTalk = function (id) {
                if (!confirm('Delete this entry?')) return;
                $http({
                    url: '/api/pageview/tc_deleteTechnicalTalk', method: 'POST',
                    data: { id: id }, headers: { 'Content-Type': 'application/json' }
                }).then(function () { loadTalks(); });
            };

            // ---- Answer a customer-submitted question ----
            $scope.OpenAnswerForm = function (t) {
                $scope.showTalkForm = false;   // close create/edit form if open
                $scope.answerError = '';
                $scope.answerSuccess = '';
                $scope.answerForm = {
                    TrainingTechnicalTalkID: t.TrainingTechnicalTalkID,
                    CustomerName: t.CustomerName || 'Customer',
                    Question: t.Question,
                    Answer: t.Answer || ''
                };
            };
            $scope.CloseAnswerForm = function () { $scope.answerForm = null; $scope.answerError = $scope.answerSuccess = ''; };

            $scope.SubmitAnswer = function () {
                $scope.answerError = $scope.answerSuccess = '';
                if (!$scope.answerForm.Answer || !$scope.answerForm.Answer.trim()) {
                    $scope.answerError = 'Answer cannot be empty.';
                    return;
                }
                $http({
                    url: '/api/pageview/tc_answerQuestion', method: 'POST',
                    data: {
                        TrainingTechnicalTalkID: $scope.answerForm.TrainingTechnicalTalkID,
                        Answer: $scope.answerForm.Answer.trim()
                    },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (res) {
                    if (res.data === 'Ok') {
                        $scope.answerSuccess = 'Answer saved and customer notified by email.';
                        $scope.answerForm = null;
                        loadTalks();
                    } else { $scope.answerError = res.data || 'Failed to save.'; }
                }, function () { $scope.answerError = 'An error occurred.'; });
            };
        }

    });