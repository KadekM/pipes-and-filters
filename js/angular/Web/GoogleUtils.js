var tryGetGoogleFullContent = function (searchTerm, webParser, onResponse, onError) {
    webParser.getFullContent(
            "https://www.google.sk/search?q=" + searchTerm)
        .then(onResponse, onError);
    // .then(onResponse, onError);
    //{0}&tbs=cdr%3A1%2Ccd_min%3A{1}%2Ccd_max%3A{2}


}

var GOOGLE_HINTS_DIV_BEGIN = "id=\"resultStats\"";
var GOOGLE_HINTS_DIV_END = "</div>";
var GOOGLE_HINTS_END_INSIDE = "<nobr>";

var getSearchCount = function (googlePage) {
    var beginPosTag = googlePage.fullContent.indexOf(GOOGLE_HINTS_DIV_BEGIN);

    if (beginPosTag == -1)
        return {success: false};

    var sub = googlePage.fullContent.substr(beginPosTag + GOOGLE_HINTS_DIV_BEGIN.length);

    var endPosTag = sub.indexOf(GOOGLE_HINTS_DIV_END);

    sub = sub.substr(0, endPosTag);

  //  sub = sub.replace('Ä','');

   // var parts = sub.split(/Â| /);


   // if (parts.length == 0)
  //      return {success: false};


    var numberClean = "";

   /* for (var i = 0; i < parts.length; i++) {
        var wasNum = false;
        */
        for (var j = 0; j < sub.length; j++)
        {
            if (sub[j] >= '0' && sub[j] <= '9')
            {
                numberClean += sub[j];
            }
            else
            if(sub.indexOf(GOOGLE_HINTS_END_INSIDE) == j)
                break;
        }

    /*    if(parts[i].indexOf('<nobr>') > -1)
            break;
    }    */


    var result = parseInt(numberClean);
    return  {success: true, result: result};
}