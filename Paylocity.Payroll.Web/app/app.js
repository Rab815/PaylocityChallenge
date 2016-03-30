(function () {
    'use strict';

    angular.module('app', ['ngRoute']);

    bootstrapApplication();

    function bootstrapApplication() {
        angular.element(document).ready(function () {
            angular.bootstrap(document, ["app"]);
        });
    }
})();
