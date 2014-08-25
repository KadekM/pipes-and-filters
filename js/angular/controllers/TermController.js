(function () {
    'use strict';
// todo: promises
// todo: unit tests
// todo: swap to model based approach
// todo: remove applies ^^

    var app = angular.module("webAnalyzer");

    var TermController = function ($scope, $routeParams, $webFetcher, $googleParser) {
        $scope.term = $routeParams.term;

        var onDone = function (contents) {
            console.log({onDone: contents});

            try {
                $scope.repo = $googleParser.parse(contents).count;
            } catch (_) {
                $scope.repo = "ERRRRR";
            } finally {
                $scope.$apply();
            }
        };

        var onFail = function (jqxhr, textStatus, error) {
            console.log({onFail: error});
            $scope.error = error
            $scope.$apply();
        }

        $scope.repo = "Work in progress"

        var url = "https://www.google.sk/search?q=" + $scope.term;
        $webFetcher.getFullContent(url, onDone, onFail);
    };

    app.controller("TermController", TermController);

}());