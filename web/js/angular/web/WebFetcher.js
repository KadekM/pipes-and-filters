(function () {
    'use strict';

    var webFetcher = function () {
        var getFullContent = function (url, onDone, onFail) {
            return $.getJSON('http://anyorigin.com/get?url=' + url + '&callback=?', function(data) {
                onDone(data.contents);
                externalApply();
            }).fail( function(){
                onFail;
                externalApply();
            });
        };

        var externalApply = function()
        {
            var e = document.getElementById('webAnalyzer');
            var scope = angular.element(e).scope();

            scope.$apply();
        }

        return {
            getFullContent: getFullContent
        };
    };

    angular.module("webAnalyzer").factory("$webFetcher", webFetcher);
}());