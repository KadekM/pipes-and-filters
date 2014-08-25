(function () {
    'use strict';

    var GOOGLE_HINTS_DIV_BEGIN = "id=\"resultStats\"";
    var GOOGLE_HINTS_DIV_END = "</div>";
    var GOOGLE_HINTS_END_INSIDE = "<nobr>";

    var googleParser = function () {
        var parse = function (contents) {
            var beginPosTag = contents.indexOf(GOOGLE_HINTS_DIV_BEGIN);

            if (beginPosTag == -1)
                throw new Error("Contents failed to be parsed")

            var sub = contents.substr(beginPosTag + GOOGLE_HINTS_DIV_BEGIN.length);
            var endPosTag = sub.indexOf(GOOGLE_HINTS_DIV_END);
            sub = sub.substr(0, endPosTag);

            var numberClean = "";

            for (var j = 0; j < sub.length; j++) {
                if (sub[j] >= '0' && sub[j] <= '9') {
                    numberClean += sub[j];
                }
                else if (sub.indexOf(GOOGLE_HINTS_END_INSIDE) == j)
                    break;
            }

            var count = parseInt(numberClean);

            return  {
                count: count
            };
        };

        return {
            parse: parse
        };
    };

    angular.module("webAnalyzer").factory("$googleParser", googleParser);
}());