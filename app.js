/**
 * Created by Michal on 24. 8. 2014.
 */
(function(){

    var app = angular.module("webAnalyzer", ["ngRoute"]);

    app.config(function($routeProvider){
        $routeProvider
            .when("/main", { // todo later
                templateUrl: "main.html",
                controller: "MainController"
            })
            .otherwise({redirectTo:"/main"});
});

}());