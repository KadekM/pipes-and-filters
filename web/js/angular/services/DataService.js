'use strict';

(function () {
    angular.module('webAnalyzer.services', []).factory('$dataService', function() {
        function DataService() {
            var data = [];

            this.loadData = function(callback) {
                var xAxis = useDate.format("yyyy-mm-dd");



                data.push({"x":xAxis,"Sentiment":randomSentiment(),"Hits":randomHits()});
                callback(data);
            };
        }

        return new DataService();
    });
}());