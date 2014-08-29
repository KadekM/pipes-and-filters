'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("MainController",
        function($scope) {

            $scope.executeAnalyze = function(term) {
                if($scope.terms.indexOf(term) === -1) {
                    $scope.terms.push(term);
                }
            };

            $scope.terms = [];
        }
    );
}());