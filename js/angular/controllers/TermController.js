(function () {
    'use strict';

    var app = angular.module("webAnalyzer");

    var TermController = function ($scope, $webParser, $routeParams) {
        $scope.term = $routeParams.term;

        var page;

        var onDone = function (contents) {
           console.log(contents);

          /*  page = new WebPage(contents);
            var countResult = getSearchCount(page);

            if (countResult.success) {
                $scope.repo = countResult.result;
                $scope.$apply();
            }*/
        };

        var onFail = function( jqxhr, textStatus, error ) {
            console.log(error);
            $scope.error = error
        }

        $scope.repo = -1;

        var url = "https://www.google.sk/search?q=" + $scope.term;
        $webParser.getFullContent(url, onDone, onFail);
    };

    app.controller("TermController", TermController);

}());