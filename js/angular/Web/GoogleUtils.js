
var getGoogleHTMLPromise = function(searchTerm, webParser,onResponse, onError)
{
    webParser.getHTML(
                "https://www.google.sk/search?q="+ searchTerm)
        .then(onResponse, onError);
           // .then(onResponse, onError);
            //{0}&tbs=cdr%3A1%2Ccd_min%3A{1}%2Ccd_max%3A{2}


}


var getSearchCount = function(googlePage)
{
    resultStats = $(googlePage.html).filter('#resultStats'); // todo, not working
    text = resultStats.html();

    return text;
}

