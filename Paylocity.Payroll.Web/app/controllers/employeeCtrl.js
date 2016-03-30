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
        vm.loading = false;
        activate();

        function activate() {
            // call functions here to intialize screen or other vars
            getEmployees();
        }

        function getEmployees() {
            vm.loading = true;
            employeeService.getEmployees()
                .then(function(response) {
                    vm.employees = response;
                    vm.loading = false;
                });
        }

        function deleteEmployee(employeeId) {
            if (confirm("Are you sure you want to delete this employee?")) { 
                employeeService.deleteEmployee(employeeId)
                    .then(function(response) {
                        getEmployees();
                    });
            }
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
