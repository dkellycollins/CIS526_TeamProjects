//code for identifying if element is scrolled into the window found at 
//http://stackoverflow.com/questions/4488604/how-to-determine-if-were-at-the-bottom-of-a-page-using-javascript-jquery

//attempts to make the ajax call thread safe and so 
//not too many requests are made to the server at once
var processing = false;
var finishedLoading = false;
var selectedObject = null;

function isScrolledIntoView(elem) {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height();

    var elemTop = $(elem).offset().top;
    var elemBottom = elemTop + $(elem).height();

    return ((elemBottom >= docViewTop) && (elemTop <= docViewBottom));
}

function checkAndLoad() {
    if (isScrolledIntoView($('#infiniteScrollingHook'))) {
        //trigger load
        loadNextPage();
    }
}

function createRow(table, number, recordValues) {
    var row = table.insertRow(-1);

    row.setAttribute('class', 'leaderboardRecords');
    row.setAttribute('id', 'record' + number);

    var numberCell = row.insertCell(-1);
    var nameCell = row.insertCell(-1);
    var iconCell = row.insertCell(-1);
    var scoreCell = row.insertCell(-1);

    numberCell.innerHTML = number + ".";

    nameCell.innerHTML = recordValues.Username;

    scoreCell.innerHTML = recordValues.Score;

    for (var i = 0; i < recordValues.Milestones.length; i++) {
        var tempImg = document.createElement('img');
        tempImg.src = recordValues.Milestones[i].IconLink;
        tempImg.setAttribute('title', recordValues.Milestones[i].Description);
        tempImg.setAttribute('class', 'milestoneIcon');
        iconCell.appendChild(tempImg);
    }
}

function generateTable(table, tableValues) {
    var elementNum = table.rows.length + 1;

    for (var i = 0; i < tableValues.length; i++) {
        createRow(table, i + elementNum, tableValues[i]);
    }
}

function successfulLoad(object) {
    var table = document.getElementById("ScoreboardTable");
    var elementNum = table.rows.length + 1;

    //document.getElementById("pageNumber").innerHTML = object.PageNum;
    document.getElementById("currentPage").value = object.PageNum;

    if (object.FinishedLoading === true) {
        var hook = document.getElementById('infiniteScrollingHook');
        hook.parentNode.removeChild(hook);
        finishedLoading = true;
    }

    generateTable(table, object.Users);

    processing = false;
}

function loadNextPage() {
    if (document.getElementById("infiniScrollPageNumber").innerHTML === "Done")
        return;

    //var pageNum = parseInt(document.getElementById("pageNumber").innerHTML);
    var pageNum = document.getElementById("currentPage").value;
    var typeOfPoint = document.getElementById("pointType").value;
    //var typeOfPoint = document.getElementById("infiniScrollPointType").innerHTML;
    if (!processing) {
        processing = true;
        $.post("../Scoreboard/JS/GetNextPage", { currentPage: pageNum, pointType: typeOfPoint }, successfulLoad);
    }
}

$(document).ready(function(){
    //We're already loading the first set of results,
    //we don't need to do it again
    //checkAndLoad();

    $(window).scroll(function () {
        if (!finishedLoading) {
            checkAndLoad();
        }
    });
});


//Function found here
//http://lions-mark.com/jquery/scrollTo/
$.fn.scrollTo = function (target, options, callback) {
    if (typeof options == 'function' && arguments.length == 2)
    {
        callback = options; options = target;
    }

    var settings = $.extend(
        {
        scrollTarget: target,
        offsetTop: 50,
        duration: 500,
        easing: 'swing'
    }, options);

    return this.each(function () {
        var scrollPane = $(this);
        var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
        var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
        scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
            if (typeof callback == 'function') { callback.call(this); }
        });
    });
}

function percentOfScreen(percentVal) {
    var viewportSize = $(window).height();

    return viewportSize * (percentVal / 100);
}

function searchAjaxSuccess(jsonResult) {
    if (processing) return;

    if (!jsonResult.FinishedLoading) {
        var table = document.getElementById("ScoreboardTable");

        generateTable(table, jsonResult.Users);
        jsonResult.FinishedLoading = true;
    }

    searchWait(jsonResult);
}

var previousHeight = null;
function searchWait(jsonResult) {
    var waitLonger = false;
    var table = $('#ScoreboardTable');

    if (previousHeight == null) {
        previousHeight = table[0].scrollHeight;
        waitLonger = true;
    }
    else if (previousHeight != table[0].scrollHeight) {
        previousHeight = table[0].scrollHeight;
        waitLonger = true;
    }

    var user = $('#record' + (jsonResult.UserIndex + 1));

    console.log("Previousheight = " + user.offset().top);

    if (waitLonger) {
        setTimeout(function () { searchWait(jsonResult); }, 50);
    }
    else {
        findUser(jsonResult);
    }
}

function findUser(jsonResult) {
    if (selectedObject != null)
        selectedObject.removeClass("SelectedObject");

    //document.getElementById("pageNumber").innerHTML = jsonResult.PageNum;
    document.getElementById("currentPage").value = jsonResult.PageNum;

    var userName = $('#userName')[0].value;

    var searchVal = "#record" + (jsonResult.UserIndex + 1);

    console.log("Searching for user " + userName);

    selectedObject = $(searchVal);

    if (selectedObject.length > 0) {
        $('body').scrollTo(searchVal, { offsetTop: percentOfScreen(50) });
        console.log("User " + userName + " found");
        selectedObject.addClass("SelectedObject");
    }
    else {
        console.log("User " + userName + " not found");
        selectedObject = null;
    }

}
