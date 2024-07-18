let forumAPI = "https://localhost:7253/api/Forum";
const params = new URLSearchParams(window.location.search);
const topicId = params.get("topicId");
// document.title = "121"; set on get issue scb
let getIssueAPI = forumAPI += ""; //get issue api
ajaxCall("GET", getIssueAPI, "", issueGetSCB, issueGetECB);

function issueGetSCB(issue) {
    let cont = document.getElementById("container");
    let str = "";
    //render the issue, for loop for the comments
    cont.innerHTML += str;
    /*<div class="issue close">
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
            </div>*/
}

function issueGetECB(err) {
    alert(err.statusText);
}