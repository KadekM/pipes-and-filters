(function() {

    var app = angular.module("webAnalyzer");

    var TermController = function($scope, webParser, $routeParams) {

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

        var page;

        var onResponse = function(data){
            page = new WebPage(data);
            $scope.repo = getSearchCount(page);

        };

        var onError = function(reason){
            $scope.error = reason;
        };

        getGoogleHTMLPromise($scope.term, webParser, onResponse, onError)
          //  .then(onResponse, onError);
     //   webParser.getHTML(   //{0}&tbs=cdr%3A1%2Ccd_min%3A{1}%2Ccd_max%3A{2}
       //     "https://www.google.sk/search?q="+ $scope.term)


    };

    app.controller("TermController", TermController);

}());