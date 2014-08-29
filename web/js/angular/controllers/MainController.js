(function() {

    var app = angular.module("webAnalyzer");

    var MainController = function($scope, $location) {

        $scope.testVariable = "it is alive !!!";

        $scope.executeAnalyze = function(term) {
            //$location.path("/term/" + term);
            $scope.terms.push(term);
        };


        $scope.terms = [];
    };

    app.controller("MainController", MainController);

}());