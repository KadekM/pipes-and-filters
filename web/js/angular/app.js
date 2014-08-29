'use strict';

(function () {
    angular.module("webAnalyzer", ["webAnalyzer.controllers", "webAnalyzer.directives", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider
                .when("/main", { // todo later
                    templateUrl: "main.html",
                    controller: "MainController"
                })
                .when("/term/:term", {
                    templateUrl: "term.html",
                    controller: "TermController"
                })
                .otherwise({redirectTo: "/main"});
        });

    angular.module('d3', []);
    angular.module('webAnalyzer.controllers', []);
    angular.module('webAnalyzer.directives', ['d3']);
}());
