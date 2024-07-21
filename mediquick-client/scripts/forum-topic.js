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
    str += `<div class="go-prev" onclick="GoToPreviousPage()"><img src="./../images/icons/go-prev-right-arrow.svg" alt=""> חזרה לכל הפורומים </div>`;
    str +=`<div class="add-issue" onclick="AddIssue()"><img src="./../images/icons/plus-circle.svg" alt=""> הוסף סוגיה</div>`;
    for (var i = 0; i < issuesList.length; i++) {
        let issueClass = issuesList[i].isClosed ? 'issue close locked' : 'issue close unlocked';
        let formattedDateTime = formatDateTime(issuesList[i].createdAt)
      str += `<div class="${issueClass}">
                <div class="issue-headers">
                    <div class="create-details">
                        <div class="date-time">${formattedDateTime.date} ${formattedDateTime.time}</div>
                        <div class="creator">${issuesList[i].userFullName}</div>
                    </div>
                    <div class="title" onclick="GoToIssuePage(${issuesList[i].issueId})"><h4>${issuesList[i].title}</h4></div>
                    <div class="issue-content">${issuesList[i].issueContent}</div>
                    <div class="num-of-comments">${issuesList[i].commentCount} תגובות</div>
                </div>
                <div class="expand">
                    <img class="expand-icon" title="הצג את תוכן הסוגיה" src="./../images/icons/plus-circle.svg" alt="הצג עוד" onclick="ToggleOpenCloseIssue(this)">
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
        item.title = "הסתר את תוכן הסוגיה"
    } else {
        issueDiv.classList.remove('open');
        issueDiv.classList.add('close');
        item.src = './../images/icons/plus-circle.svg'
        item.title = "הצג את תוכן הסוגיה"
    }
}

function GoToIssuePage(issueId) {
    console.log(issueId);
    window.location.href = `forum-issue.html?issueId=${issueId}&topicName=${topicName}`
}

function formatDateTime(dateTime) {
    var date = dateTime.split('T')[0];
    var time = dateTime.split('T')[1].split('.')[0];

    var dateParts = date.split('-');
    var formattedDate = `${dateParts[2]}.${dateParts[1]}.${dateParts[0]}`;

    return {
        date: formattedDate,
        time: time
    };
}

function GoToPreviousPage() {
    window.location.href = `forum.html`;
}

function AddIssue() {
    //הוספת סוגיה
}