(function () {
    'use strict';

    angular
        .module('app')
        .controller('DependentCtrl', dependentCtrl);

    dependentCtrl.$inject = ['$location', 'employeeService', 'dependentService', 'benefitsService', '$routeParams'];

    function dependentCtrl($location, employeeService, dependentService, benefitsService, $routeParams) {
        /* jshint validthis:true */

        var vm = angular.extend(this, {
            saveDependent: saveDependent,
            getDependents: getDependents,
            deleteDependent: deleteDependent
        });

        vm.title = 'Dependent List';
        vm.dependents = [];
        vm.firstName = "";
        vm.lastName = "";
        vm.employeeFirstName = "";
        vm.employeeLastName = "";
        vm.employeeId = $routeParams.employeeId;
        vm.benefits = {};
        vm.loading = false;
        activate();

        function activate() {
            // call functions here to intialize screen or other vars
            getEmployeeDetails();
            getDependents();
        }

        function getEmployeeDetails() {
            var employeeId = $routeParams.employeeId;
            employeeService.getEmployee(vm.employeeId)
                .then(function (response) {
                    vm.employeeFirstName = response.firstName;
                    vm.employeeLastName = response.lastName;
                });
        }

        function deleteDependent(dependentId) {
            if (confirm("Are you sure you want to delete this dependent?")) {
                dependentService.deleteDependent(dependentId)
                    .then(function(response) {
                        getDependents(vm.employeeId);
                    });
            }
        }

        function getDependents() {
            vm.loading = true;
            dependentService.getDependents(vm.employeeId)
                .then(function (response) {
                    vm.dependents = response;
                    vm.loading = false;
                    getBenefits();
                });
        }

        function getBenefits() {
            benefitsService.getBenefits(vm.employeeId)
                .then(function (response) {
                    vm.benefits = response;
                });
        }

        function saveDependent() {
            // set up a new employee
            var newDependent = {
                employeeId: vm.employeeId,
                firstName: vm.firstName,
                lastName: vm.lastName
            }
            dependentService.saveDependent(newDependent)
                .then(function (response) {
                    getDependents();
                    vm.firstName = "";
                    vm.lastName = "";
                });
        }
    }
})();
