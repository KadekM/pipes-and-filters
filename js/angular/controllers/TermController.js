(function () {

    var app = angular.module("webAnalyzer");

    var TermController = function ($scope, $webParser, $routeParams) {

        $scope.term = $routeParams.term;

        var page;

        var onResponse = function (data) {
            page = new WebPage(data.contents);
            var countResult = getSearchCount(page);

            if (countResult.success) {
                $scope.repo = countResult.result;
                $scope.$apply();
            }

        };

        var onError = function (reason) {
            $scope.error = reason;
        };
        $scope.repo = -1;
        tryGetGoogleFullContent($scope.term, $webParser, onResponse, onError)
    };

    app.controller("TermController", TermController);

}());