'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("TermController",
        function ($scope, $routeParams, $webFetcher, $timeout, $dataService) {
            //var url = "http://localhost:48213/api/analysis";
            var url = "http://webanalyzer.azurewebsites.net/api/analysis";

            $.ajax({
                crossDomain: true,
                type: 'POST',
                url: url,
                data:  JSON.stringify({"Term": $scope.request.term}),
                contentType:   'text/json',
                success:   function(json) {
                    console.log(json)
                    $scope.graphInit(url+"/"+json.id); // todo based on URI
                }
            });

            $scope.graphInit = function (taskUrl) {
                $scope.chart = null;
                $scope.config = {};

                $scope.config.data = []

                $scope.config.type1 = "spline";
                $scope.config.type2 = "spline";
                $scope.config.keys = {"x": "x", "value": ["Hits"]};

                $timeout(function () {
                    var config = {};
                    config.bindto = '#chart'+$scope.request.term;
                    config.data = {};
                    config.data.keys = $scope.config.keys;
                    config.data.json = $scope.config.data;

                    config.axis = {};
                    config.axis.x = {"type": "timeseries", "tick": {"format": function (x) { return x.format("yyyy-mm-dd"); }}};

                    config.size = {
                        "width": 640, "height": 480
                    }

                    config.data.types = {"data1": $scope.config.type1, "data2": $scope.config.type2};
                    config.data.axes = {"Sentiment": "y2"};
                    $scope.chart = c3.generate(config);

                    $scope.startLoading(taskUrl);
                }, 1000);


            }

            $scope.startLoading = function (taskUrl) {
                $scope.loadNewData(taskUrl);
            }

            $scope.loadNewData = function (taskUrl) {
                $dataService.loadData(taskUrl, function (newData) {
                    var url = taskUrl
                    var data = {};
                    data.keys = $scope.config.keys;
                    data.json = newData;
                    $scope.chart.load(data);

                    $timeout(function () {
                        if (data.json.length < 10) { // todo: better stop condition
                            $scope.loadNewData(url)
                        }
                    }, 1000);
                });
            }
        }
    );
}());