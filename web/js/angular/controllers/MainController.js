'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("MainController",
        function($scope, $timeout, $fakeDataService) {

            // requests:

            $scope.requests = [];

            $scope.executeAnalyze = function ($term) {

                var $analysisType = $scope.selectedAnalysisType.name;
                var $request = {term:$term, type:$analysisType, showTop:true};

                if(containsAnalysisRequest($request))
                    return;

                $scope.requests.push($request);
            };

            var containsAnalysisRequest = function($request)
            {
                // linq
                if(Enumerable.From($scope.requests).Any(
                        function($item)
                        {
                            return($item.term === $request.term
                                && $item.type === $request.type)
                        }))
                    return true;

                return false;
            }



            // analysis type

            $scope.analysisTypes = [
                {name:'hitsCount', text:'Hits'},
                {name:'sentimentAnalysis', text:'Sentiment'}
            ];
            $scope.selectedAnalysisType = $scope.analysisTypes[0];

            // graph:
            $scope.chart = null;
            $scope.config = {};

            $scope.config.data = []

            $scope.config.type1 = "spline";
            $scope.config.type2 = "spline";
            $scope.config.keys = {"x": "x", "value": ["data1", "data2"]};

            $scope.keepLoading = true;

            $scope.showGraph = function () {
                var config = {};
                config.bindto = '#chart';
                config.data = {};
                config.data.keys = $scope.config.keys;
                config.data.json = $scope.config.data;
                config.axis = {};
                config.axis.x = {"type": "timeseries", "tick": {"format": "%S"}};
                config.axis.y = {"label": {"text": "Number of items", "position": "outer-middle"}};
                config.data.types = {"data1": $scope.config.type1, "data2": $scope.config.type2};
                $scope.chart = c3.generate(config);
            }

            $scope.startLoading = function () {
                $scope.keepLoading = true;
                $scope.loadNewData();
            }

            $scope.stopLoading = function () {
                $scope.keepLoading = false;
            }

            $scope.loadNewData = function () {
                $fakeDataService.loadData(function (newData) {
                    var data = {};
                    data.keys = $scope.config.keys;
                    data.json = newData;
                    $scope.chart.load(data);
                    $timeout(function () {
                        if ($scope.keepLoading) {
                            $scope.loadNewData()
                        }
                    }, 1000);
                });
            }
        }
    );
}());