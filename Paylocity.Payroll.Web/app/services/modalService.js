(function() {
    'use strict';

    angular.module('app').service('modalService', modalService);

    modalService.$inject = ['$uibModal'];

    function modalService($uibModal) {
        var service = {
            showModal: showModal,
            show: show
        }

        return service;

        function showModal(customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        }

        function show(customModalDefaults, customModalOptions) {
            var modalDefaults = {
                backdrop: true,
                keyboard: true,
                modalFade: true,
                templateUrl: '/partials/templates/modal.html'
            };

            var modalOptions = {
                closeButtonText: 'Close',
                actionButtonText: 'OK',
                headerText: 'Confirm',
                bodyText: 'Perform this action?'
            };

            //Create temp objects to work with since we're in a singleton service
            var tempModalDefaults = {};
            var tempModalOptions = {};

            //Map angular-ui modal custom defaults to modal defaults defined in service
            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

            //Map modal.html $scope custom properties to defaults defined in service
            angular.extend(tempModalOptions, modalOptions, customModalOptions);

            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = function ($scope, $uibModalInstance) {
                    $scope.modalOptions = tempModalOptions;
                    $scope.modalOptions.ok = function (result) {
                        $uibModalInstance.close(result);
                    };
                    $scope.modalOptions.close = function (result) {
                        $uibModalInstance.dismiss('cancel');
                    };
                }
            }
            
            return $uibModal.open(tempModalDefaults).result; 

        };
    }
})();