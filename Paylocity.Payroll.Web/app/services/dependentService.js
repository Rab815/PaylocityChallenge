(function () {
    'use strict';

    angular
        .module('app')
        .factory('dependentService', dependentService);

    dependentService.$inject = ['$http'];

    function dependentService($http) {
        var service = {
            getDependents: getDependents,
            getDependent: getDependent,
            saveDependent: saveDependent,
            deleteDependent: deleteDependent
        };

        return service;

        function getDependents(employeeId) {
            return $http
                .get("http://localhost:1837/api/v1/dependents/"+ employeeId)
                .then(function (response) {
                    return response.data;
                });
        }

        function getDependent(dependentId) {
            return $http
                .get("http://localhost:1837/api/v1/dependent/" + dependentId)
                .then(function (response) {
                    return response.data;
                });
        }

        function deleteDependent(dependentId) {
            debugger;
            return $http
                .delete("http://localhost:1837/api/v1/dependent/" + dependentId)
                .then(function (response) {
                    return response.data;
                });
        }

        function saveDependent(dependent) {
            return $http.post("http://localhost:1837/api/v1/dependent", dependent)
                .then(function (response) {
                    return response.data;
                });
        }
    }
})();