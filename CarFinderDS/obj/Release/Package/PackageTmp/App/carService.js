(function () {
    angular.module('carDetails').factory('carService', ['$http', function ($http) {
        var service = {};

        service.getMakes = function (selected) {
            return $http.post('/api/carfinder/GetMakes', selected).then(function (response) {
                return response.data;
            });

        };

        service.getModelByMake = function (selected) {
            return $http.post('/api/carfinder/GetModelByMake', selected).then(function (response) {
                return response.data;
            });

        };

        service.getTrimByMakeModel = function (selected) {
            return $http.post('/api/carfinder/GetTrimByMakeModel', selected).then(function (response) {
                return response.data;
            });

        };

        service.getYearByMakeModelTrim = function (selected) {
            return $http.post('/api/carfinder/GetYearByMakeModelTrim', selected).then(function (response) {
                return response.data;
            });

        };

        service.getCarsAlt = function (selected) {
            return $http.post('/api/carfinder/GetCarsAlt', selected).then(function (response) {
                return response.data;
            });

        };

        service.getInfo = function (id) {
            return $http.post('/api/carfinder/GetInfo', {id: id}).then(function (response) {
                return response.data;
            });

        };


        return service;
    }])
})();