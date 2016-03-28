(function () {
    'use strict';

    angular
        .module('app')
        .factory('employeeService', employeeService);

    employeeService.$inject = ['$http'];

    function employeeService($http) {
        var service = {
            getEmployees: getEmployees,
            getEmployee: getEmployee,
            saveEmployee: saveEmployee
        };

        return service;

        function getEmployees() {
            return $http
                .get("http://localhost:1837/api/v1/employees")
                .then(function(response) {
                    return response.data;
                });
        }

        function getEmployee(userId) {
            return $http
                .get("http://localhost:1837/api/v1/employee/" + userId)
                .then(function (response) {
                    return response.data;
                });
        }

        function saveEmployee(employee) {
            return $http.post("http://localhost:1837/api/v1/employee", employee)
                .then(function(response) {
                    return response.data;
                });
        }

    }
})();