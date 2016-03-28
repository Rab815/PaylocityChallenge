(function () {
    'use strict';

    angular.module('app', ['ngRoute']);

    //angular.module("app").run();

    bootstrapApplication();

    //function getQueryVariable(variable) {
    //    var query = window.location.search.substring(1);
    //    var vars = query.split('&');
    //    for (var i = 0; i < vars.length; i++) {
    //        var pair = vars[i].split('=');
    //        if (decodeURIComponent(pair[0]) === variable) {
    //            return decodeURIComponent(pair[1]);
    //        }
    //    }
    //    return null;
    //}

    //function fetchData() {
    //    var initInjector = angular.injector(["ng"]);
    //    var $http = initInjector.get("$http");
    //    var $log = initInjector.get("$log");
    //    var prefix = document.getElementsByTagName('base')[0].href;
    //    var globalConfig = {
    //        // local
    //        appTitle: 'fi360',
    //        // assembly
    //        version: '',
    //        // web.config
    //        debugMode: true,
    //        apiPath: '',
    //        authPath: '',
    //        tokenTimeout: 1,
    //        sessionTimeout: 20,
    //        stripeApiKey: '',
    //        basePath: prefix
    //    };
    //    function handleResponse(response) {
    //        angular.extend(globalConfig, response.data);
    //        if (prefix.length > 0) {
    //            if (globalConfig.apiPath.length === 0 || !(globalConfig.apiPath.substr(0, 1) === '/' || globalConfig.apiPath.indexOf('http') === 0)) {
    //                globalConfig.apiPath = prefix + globalConfig.apiPath;
    //            }
    //        }
    //        $log.info('Application Version: ' + globalConfig.version);
    //        if (getQueryVariable('debug') === 'true') {
    //            globalConfig.debugMode = true;
    //        }
    //        else if (!globalConfig.debugMode) {
    //            $log.info('Logging disabled by DebugMode');
    //        }
    //        angular.module('app').constant("configService", globalConfig);
    //    }
    //    function handleError(errorResponse) {
    //        alert("error: " + errorResponse);
    //    }
    //    return $http
    //        .get(prefix + 'api/configuration/globalSettings', { cache: true })
    //        .then(handleResponse, handleError);
    //}


    function bootstrapApplication() {
        angular.element(document).ready(function () {
            angular.bootstrap(document, ["app"]);
        });
    }
})();
