'use strict';

(function () {
    var app = angular.module("webAnalyzer", ["webAnalyzer.controllers", "webAnalyzer.services", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider
                .when("/main", { // todo later
                    templateUrl: "main.html",
                    controller: "MainController"
                })
                .otherwise({redirectTo: "/main"});
        });

	app.config(['$httpProvider', function($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }
	]);	
    angular.module('webAnalyzer.controllers', []);
    angular.module('webAnalyzer.services', []);
}());
