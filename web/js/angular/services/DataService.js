'use strict';

(function () {
    angular.module('webAnalyzer.services', []).factory('$dataService', function() {
        function DataService() {
            this.loadData = function(taskUrl, callback) {
                $.ajax({
                    crossDomain: true,
                    type: 'GET',
                    url: taskUrl,
                    contentType:   'text/json',
                    success:   function(json) {
                        console.log(json)

                        var processed = [];
                        for (var index = 0; index < json.data.length; ++index) {
                            var entry = json.data[index]
                            var amount = entry.total;
                            var date = new Date(entry.date).format("yyyy-mm-dd");
                            processed.push({"x": date, "Hits":amount});
                        }

                        console.log(processed);
                        callback(processed);
                    }
                });
            };
        }

        return new DataService();
    });
}());