(function () {
    'use strict';

    angular.module('app', ['ngRoute', 'ui.bootstrap']);

    bootstrapApplication();

    function bootstrapApplication() {
        angular.element(document).ready(function () {
            angular.bootstrap(document, ["app"]);
        });
    }
})();
