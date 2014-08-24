(function() {

    var app = angular.module("webAnalyzer", ["ngRoute"]);

    app.config(function($routeProvider){
        $routeProvider
            .when("/main", { // todo later
                templateUrl: "main.html",
                controller: "MainController"
            })
            .when("/term/:term", {
                templateUrl: "term.html",
                controller: "TermController"
            })
            .otherwise({redirectTo:"/main"});
});

}());