(function () {
    'use strict';

    var webParser = function () {
        var getFullContent = function (url, onDone, onFail) {
            return $.getJSON('http://anyorigin.com/get?url=' + url + '&callback=?', function(data) {
                onDone(data.contents);
            }).fail(onFail);
        };

        return {
            getFullContent: getFullContent
        };
    };

    var module = angular.module("webAnalyzer");
    module.factory("$webParser", webParser);

}());