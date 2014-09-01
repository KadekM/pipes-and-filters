'use strict';

(function() {
    angular.module("webAnalyzer.controllers").controller("TermController",
        function ($scope, $routeParams, $webFetcher, $googleParser, $timeout, $fakeDataService) {
            var onDone = function (contents) {
                console.log({onDone: contents});

                try {
                    $scope.repo = $googleParser.parse(contents).count;
                } catch (_) {
                    $scope.repo = "ERRRRR";
                }
            };

            var onFail = function (jqxhr, textStatus, error) {
                console.log({onFail: error});
                $scope.error = error
            }


            $scope.gotoTop = function() {
                document.body.scrollTop = document.documentElement.scrollTop = 0;
            };

            $scope.repo = "Work in progress"

            var url = "https://www.google.sk/search?q=" + $scope.request.term;
            $webFetcher.getFullContent(url, onDone, onFail);


            // graph:


            $scope.graphIni = function () {

                $scope.chart = null;
                $scope.config = {};

                $scope.config.data = []

                $scope.config.type1 = "spline";
                $scope.config.type2 = "spline";
                $scope.config.keys = {"x": "x", "value": ["Sentiment", "Hits"]};

                $scope.keepLoading = true;

                $timeout(function () {
                    var config = {};
                    config.bindto = '#chart'+$scope.request.term;
                    config.data = {};
                    config.data.keys = $scope.config.keys;
                    config.data.json = $scope.config.data;

                    config.axis = {};
                    config.axis.x = {"type": "timeseries", "tick": {"format": function (x) { return x.format("yyyy-mm-dd"); }}};

                    config.size = {
                        "height": 240, "width": 480
                    }

                    config.data.types = {"data1": $scope.config.type1, "data2": $scope.config.type2};
                    config.data.axes = {"Sentiment": "y2"};
                    config.axis.y2 = {"show":"true", "tick": {"format": d3.format("0.1f")}};
                    $scope.chart = c3.generate(config);

                    $scope.startLoading();
                }, 1000);


            }

            $scope.startLoading = function () {
                $scope.keepLoading = true;
                $scope.loadNewData();
            }

            $scope.loadNewData = function () {
                $fakeDataService.loadData(function (newData) {
                    var data = {};
                    data.keys = $scope.config.keys;
                    data.json = newData;
                    $scope.chart.load(data);
                    $timeout(function () {
                        if($fakeDataService.dataLoaded()) {
                            $scope.keepLoading = false;
                        }
                        if ($scope.keepLoading) {
                            $scope.loadNewData()
                        }
                    }, 1000);
                });
            }

            $scope.graphIni();

        }
    );
}());