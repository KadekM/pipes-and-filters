'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("TermController",
        function ($scope, $routeParams, $location, $anchorScroll, $webFetcher, $googleParser) {
            var onDone = function (contents) {
                console.log({onDone: contents});

                try {
                    $scope.repo = $googleParser.parse(contents).count;
                } catch (_) {
                    $scope.repo = "ERRRRR";
                }
            };

            var onFail = function (jqxhr, textStatus, error) {
                console.log({onFail: error});
                $scope.error = error
            }


            $scope.gotoTop = function() {
                document.body.scrollTop = document.documentElement.scrollTop = 0;
            };

            $scope.repo = "Work in progress"

            var url = "https://www.google.sk/search?q=" + $scope.request.term;
            $webFetcher.getFullContent(url, onDone, onFail);
        }
    );
}());