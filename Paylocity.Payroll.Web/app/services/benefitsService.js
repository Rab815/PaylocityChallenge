(function () {
    'use strict';

    angular
        .module('app')
        .factory('benefitsService', benefitsService);

    benefitsService.$inject = ['$http'];

    function benefitsService($http) {
        var service = {
            getBenefits: getBenefits
        };

        return service;

        function getBenefits(employeeId) {
            return $http
                .get("http://localhost:1837/api/v1/benefits/employee/" + employeeId)
                .then(function (response) {
                    return response.data;
                });
        }
    }
})();