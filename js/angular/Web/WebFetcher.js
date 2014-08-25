(function () {
    'use strict';

    var webFetcher = function () {
        var getFullContent = function (url, onDone, onFail) {
            return $.getJSON('http://anyorigin.com/get?url=' + url + '&callback=?', function(data) {
                onDone(data.contents);
            }).fail(onFail);
        };

        return {
            getFullContent: getFullContent
        };
    };

    angular.module("webAnalyzer").factory("$webFetcher", webFetcher);
}());