'use strict';

(function () {
    angular.module('webAnalyzer.services', []).factory('$fakeDataService', function() {
        function DataService() {
            var data = [];
            var numDataPoints = 60;
            var maxNumber = 200;
            var maxDataCount = 12;

            this.loadData = function(callback) {
                if (data.length > numDataPoints) {
                    data.shift();
                }
                if(data.length >= maxDataCount) {
                    callback(data);
                    return;
                }
                var currentDate = new Date();
                var useDate = new Date(currentDate.getYear()+1900, currentDate.getMonth(), 1);
                useDate.setMonth((useDate.getMonth()  -(data.length + 1) % 12) + 1);
                var xAxis = useDate.format("yyyy-mm-dd");

                //(useDate.getYear()+1900) +"-"+ useDate.getMonth() +"-1";

                data.push({"x":xAxis,"Sentiment":randomSentiment(),"Hits":randomHits()});
                callback(data);
            };

            this.dataLoaded = function(){
                return data.length >= maxDataCount;
            }

            function randomHits() {
                return Math.floor((Math.random() * maxNumber) + 1);
            }

            function randomSentiment() {
                return ((Math.random() * 2) - 1);

            }
        }

        return new DataService();
    });
}());