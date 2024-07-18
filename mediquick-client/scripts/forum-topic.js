let forumAPI = "https://localhost:7253/api/Forum";
const params = new URLSearchParams(window.location.search);
const topicId = params.get("topicId");
const topicName = params.get("topicName");
document.title += " " + topicName;
document.getElementById("main-header").innerHTML += " " + topicName;
let getIssuesAPI = forumAPI + "/topicid/" + topicId;
ajaxCall("GET", getIssuesAPI, "", issuesGetSCB, issuesGetECB);

function issuesGetSCB(issuesList) {
    let cont = document.getElementById("container");
    let str = "";
    for (var i = 0; i < issuesList.length; i++) {
      str += `<div class="issue close">
                <div class="issue-headers">
                    <div class="create-details">
                        <div class="date-time">${issuesList[i].createdAt}</div>
                        <div class="creator">${issuesList[i].userFullName}</div>
                    </div>
                    <div class="title" onclick="GoToIssuePage(${issuesList[i].issueId})">${issuesList[i].title}</div>
                    <div class="issue-content">${issuesList[i].issueContent}</div>
                    <div class="num-of-comments">${issuesList[i].commentCount} תגובות</div>
                </div>
                <div class="expand">
                    <img class="expand-icon" src="./../images/icons/plus-circle.svg" alt="הצג עוד" onclick="ToggleOpenCloseIssue(${issuesList[i].commentCount})">
                </div>
            </div>`
    }
    cont.innerHTML += str;
    console.log(issuesList)
}
function issuesGetECB(err) {
    alert(err.statusText);
}

function ToggleOpenCloseIssue(item) {
    let issueDiv = item.parentElement.parentElement;
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

function GoToIssuePage(issueId) {
    console.log(issueId);
    window.location.href = `forum-issue.html?topicId=${topicId}`
}