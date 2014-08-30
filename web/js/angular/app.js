'use strict';

(function () {
    angular.module("webAnalyzer", ["webAnalyzer.controllers", "webAnalyzer.services", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider
                .when("/main", { // todo later
                    templateUrl: "main.html",
                    controller: "MainController"
                })
                .otherwise({redirectTo: "/main"});
        });

    angular.module('webAnalyzer.controllers', []);
    angular.module('webAnalyzer.services', []);
}());
