'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("TermController",
        function ($scope, $routeParams, $webFetcher, $googleParser) {
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

            $scope.repo = "Work in progress"

            var url = "https://www.google.sk/search?q=" + $scope.request.term;
            $webFetcher.getFullContent(url, onDone, onFail);
        }
    );
}());