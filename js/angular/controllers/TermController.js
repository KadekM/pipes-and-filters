(function() {

    var app = angular.module("webAnalyzer");

    var TermController = function($scope,/* github,*/ $routeParams) {

     /*   var onUserComplete = function(data) {
            $scope.user = data;
            github.getRepos($scope.user).then(onRepos, onError);
        };

        var onRepos = function(data) {
            $scope.repos = data;
        };

        var onError = function(reason) {
            $scope.error = "Could not fetch the data.";
        };*/

        $scope.term = $routeParams.term;
//        github.getUser($scope.username).then(onUserComplete, onError);

    };

    app.controller("TermController", TermController);

}());