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


            $scope.d3Data = [
                {name: "A", score:98},
                {name: "B", score:96},
                {name: "C", score: 48}
            ];
            $scope.d3OnClick = function(item){
                alert(item.name);
            };
        }
    );
}());