(function () {
    var app = angular.module('carDetails');
    app.controller('carCtrl', ['carService', '$uibModal', function (carService, $uibModal) {
        var scope = this;
        scope.makes = [];
        scope.models = [];
        scope.trims = [];
        scope.years = [];
        scope.cars = [];
        scope.selected = {
            make: '',
            model: '',
            trim: '',
            year: ''
        }

        scope.getMakes = function () {
            carService.getMakes().then(function (data) {
                scope.makes = data;
                scope.models = [];
                scope.trims = [];
                scope.years = [];
                scope.selected.make = '';
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.selected.year = '';
                })
        }
        scope.getModels = function () {
            carService.getModelByMake(scope.selected).then(function (data) {
                scope.models = data;
                scope.trims = [];
                scope.years = [];
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.selected.year = '';
                scope.getCarsAlt();
            })
        }
        scope.getTrims = function () {
            console.log(scope.selected);
            carService.getTrimByMakeModel(scope.selected).then(function (data) {
                scope.trims = data;
                scope.years = [];
                scope.selected.trim = '';
                scope.selected.year = '';
                scope.getCarsAlt();
            })
        }
        scope.getYears = function () {
            carService.getYearByMakeModelTrim(scope.selected).then(function (data) {
                scope.years = data;
                scope.selected.year = '';
                scope.getCarsAlt();
            })
        }
        scope.getCarsAlt = function () {
            carService.getCarsAlt(scope.selected).then(function (data) {
                scope.cars = data
            })
        }
        scope.getMakes();

        scope.open = function (id) {
            console.log("Id in open " + id)
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'carModal.html',
                controller: 'carModalCtrl as cm',
                size: 'lg',
                resolve: {
                car: function() {
                    return carService.getInfo(id);
                }
               }
            });
        }
    }]);
    app.controller('carModalCtrl', function ($uibModalInstance, car) { // add car later to params

        var scope = this;
        scope.n = 0;
        scope.car = car;

        scope.ok = function () {
            $uibModalInstance.close();
        };

        scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    })
    })();