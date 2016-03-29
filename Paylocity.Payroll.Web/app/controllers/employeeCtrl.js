(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeCtrl', employeeCtrl);

    employeeCtrl.$inject = ['$location', 'employeeService'];

    function employeeCtrl($location, employeeService) {
        /* jshint validthis:true */
        // public functions 
        var vm = angular.extend(this, {
            getEmployees: getEmployees,
            saveEmployee: saveEmployee,
            deleteEmployee: deleteEmployee
        });

        vm.title = 'Employee List';
        vm.employees = [];
        vm.firstName = "";
        vm.lastName = "";

        activate();

        function activate() {
            // call functions here to intialize screen or other vars
            getEmployees();
        }

        function getEmployees() {
            employeeService.getEmployees()
                .then(function(response) {
                    vm.employees = response;
                });
        }

        function deleteEmployee(employeeId) {
            employeeService.deleteEmployee(employeeId)
                .then(function (response) {
                    getEmployees();
                });
        }


        function saveEmployee() {
            // set up a new employee
            var newEmployee = {
                firstName : vm.firstName,
                lastName : vm.lastName
            }
            employeeService.saveEmployee(newEmployee)
                .then(function (response) {
                    getEmployees();
                    vm.firstName = "";
                    vm.lastName = "";
            });
        }
    }
})();
