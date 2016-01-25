angular.module('carDetails').factory('fakeSvc', ['$q', function ($q) {
    var service = {};

    service.getYears = function () {
        var deferred = $q.defer();
        deferred.resolve([1999, 2000, 2001]);
        return deferred.promise;
    }

    service.getMakes = function () {

        var deferred = $q.defer();
        
                deferred.resolve(['Ford', 'GM', ''])
               
        return deferred.promise;
    }
    return service;
}
])