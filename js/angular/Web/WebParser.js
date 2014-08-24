(function(){

    var webParser;
    webParser = function ($http) {

        var getHtml = function (url) {
         //   method: 'POST', url:
            return $http({method: 'GET',
                url: url})
                  .then(function (response) {
                    return response.data;
                });
        };

        /* var getRepos = function(user){
         return $http.get(user.repos_url)
         .then(function(response){
         return response.data;
         });
         };

         var getRepoDetails = function(username, reponame){
         var repo;
         var repoUrl = "https://api.github.com/repos/" + username + "/" + reponame;

         return $http.get(repoUrl)
         .then(function(response){
         repo = response.data;
         return $http.get(repoUrl + "/collaborators");
         })
         .then(function(response){
         repo.collaborators = response.data;
         return repo;
         });
         };*/

        return {
            getHTML: getHtml
        };

    };

    var module = angular.module("webAnalyzer");
    module.factory("webParser", webParser);

}());