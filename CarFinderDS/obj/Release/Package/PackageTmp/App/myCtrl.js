(function () {
    var app = angular.module('myApp');

    app.controller('myCtrl', function () {
        var scope = this;

        scope.years = [];
        scope.makes = [];
        scope.models = [];
        scope.trims = [];
        scope.selected = {
            years: '',
            makes: '',
            models: '',
            trims: ''
        }
        }
        scope.date = new Date();
        scope.done = false;
        scope.priority = '';
        scope.priorities = ['Low', 'Medium', 'High', 'Urgent'];
        scope.todos = ['Ex: Do something special!'];

        scope.newDate = function () {
            if (scope.done === true) {
                scope.date1 = new Date();
            }
        }
        scope.addTask = function () {
            scope.todos.push(scope.task);
            scope.task = '';
        }

        scope.remove = function (task) {
            var index = -1;
            var todosArr = eval(scope.todos);
            for (var i = 0; i < todosArr.length; i++) {
                if (todosArr[i].task === task) {
                    index = i;
                    break;
                }
            }
            if (index === -1) {
                alert("Something's Wrong!");
            }
            scope.todos.splice(index, 1);
        };
    });

})();