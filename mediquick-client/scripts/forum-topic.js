let forumAPI = "https://localhost:7253/api/Forum";
const params = new URLSearchParams(window.location.search);
const topicId = params.get("topicId");
const topicName = params.get("topicName");
document.title += " " + topicName;
document.getElementById("main-header").innerHTML += " " + topicName;
let getIssuesAPI = forumAPI + "/topicid/" + topicId;
ajaxCall("GET", getIssuesAPI, "", issuesGetSCB, issuesGetECB);

function issuesGetSCB(issuesList) {
    // let cont = document.getElementById("container");
    // let str = "";
    // for (var i = 0; i < issuesList.length; i++) {
    //   str += ``
    // }
    // cont.innerHTML += str;
    console.log(issuesList)
}
function issuesGetECB(err) {
    alert(err.statusText);
}

function ToggleOpenCloseIssue(item) {
    console.log(item);
    let issueDiv = item.parentElement.parentElement;
    console.log(issueDiv);
    if (issueDiv.classList.contains('close')) {
        issueDiv.classList.remove('close');
        issueDiv.classList.add('open');
        item.src = './../images/icons/minus-circle-1.5thick.svg'
    } else {
        issueDiv.classList.remove('open');
        issueDiv.classList.add('close');
        item.src = './../images/icons/plus-circle.svg'
    }
}