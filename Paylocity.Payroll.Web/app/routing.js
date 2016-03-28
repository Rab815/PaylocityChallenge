angular.module('app')
    .config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                 .when('/', {
                     templateUrl: 'partials/home/home.html'
                 })
                .when('/home', {
                    templateUrl: 'partials/home/home.html'
                })
                .when('/employees', {
                    templateUrl: 'partials/employee/employeeList.html',
                    controller: 'EmployeeCtrl as employee'
                })
                .when('/dependents/:employeeId', {
                    templateUrl: 'partials/dependent/dependentList.html',
                    controller: 'DependentCtrl as dependent'
                })
                .otherwise({
                    template: '<h2 style="color:red;">The page you are looking for was not found.</h2>',
                    title: 'Page Not Found',
                    secure: false
                });
        }
    ]);