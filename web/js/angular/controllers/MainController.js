(function() {

    var app = angular.module("webAnalyzer");

    var MainController = function($scope/*, $interval*/, $location) {

        $scope.testVariable = "it is alive !!!";

        $scope.executeAnalyze = function(term) {
            $location.path("/term/" + term);
        };

    };

    app.controller("MainController", MainController);

}());