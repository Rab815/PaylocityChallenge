(function () {
    'use strict';

    angular
        .module('app')
        .controller('DependentCtrl', dependentCtrl);

    dependentCtrl.$inject = ['$location', 'employeeService', 'dependentService', '$routeParams'];

    function dependentCtrl($location, employeeService, dependentService, $routeParams) {
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
            dependentService.deleteDependent(dependentId)
                .then(function (response) {
                    getDependents(vm.employeeId);
                });
        }

        function getDependents() {
            
            dependentService.getDependents(vm.employeeId)
                .then(function (response) {
                    vm.dependents = response;
                });
        }


        function saveDependent() {
            // set up a new employee
            debugger;
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
