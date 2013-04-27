//code found at 
//http://stackoverflow.com/questions/4488604/how-to-determine-if-were-at-the-bottom-of-a-page-using-javascript-jquery
function isScrolledIntoView(elem) {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height;

    var elemTop = $(elem).offset().top;
    var elemBottom = elemTop + $(elem).height();

    return ((elemBottom >= docViewTop) && (elemTop <= docViewBottom));
}

function checkAndLoad() {
    if (isScrolledIntoView($('#footer'))) {
        //trigger load
    }
}

$(document).ready(function(){
    checkAndLoad();

    $(window).scroll(function(){
        checkAndLoad();
    });
});