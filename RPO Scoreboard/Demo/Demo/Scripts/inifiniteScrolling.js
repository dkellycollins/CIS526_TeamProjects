//code for identifying if element is scrolled into the window found at 
//http://stackoverflow.com/questions/4488604/how-to-determine-if-were-at-the-bottom-of-a-page-using-javascript-jquery

//attempts to make the ajax call thread safe and so 
//not too many requests are made to the server at once
var processing = false;
var finishedLoading = false;

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

function successfulLoad(object) {
    var table = document.getElementById("ScoreboardTable");
    var elementNum = table.rows.length + 1;

    document.getElementById("infiniScrollPageNumber").innerHTML = object.PageNum;

    if (object.FinishedLoading === true) {
        var hook = document.getElementById('infiniteScrollingHook');
        hook.parentNode.removeChild(hook);
        finishedLoading = true;
    }

    for (var i = 0; i < object.Users.length; i++) {
        createRow(table, i + elementNum, object.Users[i]);
    }
    processing = false;
}

function loadNextPage() {
    if (document.getElementById("infiniScrollPageNumber").innerHTML === "Done")
        return;

    var pageNum = parseInt(document.getElementById("infiniScrollPageNumber").innerHTML);
    var typeOfPoint = document.getElementById("infiniScrollPointType").innerHTML;
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