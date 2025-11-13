angular.module('myApp').factory('Authentication', function ($resource) {
    var resource = $resource('/user', {}, {
        query: {
            method: 'GET',
            cache: true
        }
    });
    return resource.get().$promise;
});


//(function () {
//    'use strict';

//    angular.module('Authentication', [
//        // Angular modules 
//        'ngRoute'

//        // Custom modules 

//        // 3rd Party Modules

//    ]);
//})();