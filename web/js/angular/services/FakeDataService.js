'use strict';

(function () {
    angular.module('webAnalyzer.services', []).factory('$fakeDataService', function() {
        function DataService() {
            var data = [];
            var numDataPoints = 60;
            var maxNumber = 200;

            this.loadData = function(callback) {
                if (data.length > numDataPoints) {
                    data.shift();
                }
                data.push({"x":new Date(),"data1":randomNumber(),"data2":randomNumber()});
                callback(data);
            };

            function randomNumber() {
                return Math.floor((Math.random() * maxNumber) + 1);
            }
        }

        return new DataService();
    });
}());