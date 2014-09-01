'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("MainController",
        function($scope, $http) {

            // requests:

            $scope.requests = [];

            $scope.executeAnalyze = function ($term) {

                var $request = {term:$term, showTop:true};

                if(containsAnalysisRequest($request))
                    return;

                $scope.requests.push($request);
            };

            var containsAnalysisRequest = function($request)
            {
                if(Enumerable.From($scope.requests).Any(
                        function($item)
                        {
                            return($item.term === $request.term)
                        }))
                    return true;

                return false;
            }
        }
    );
}());