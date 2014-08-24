(function () {
    'use strict';

    var webParser = function ($http) {
        var getFullContent = function (url) {
            return $.getJSON('http://anyorigin.com/get?url=' + url + '&callback=?', function (data) {
                return data.contents;
            });
        };

        return {
            getFullContent: getFullContent
        };
    };

    var module = angular.module("webAnalyzer");
    module.factory("$webParser", webParser);

}());