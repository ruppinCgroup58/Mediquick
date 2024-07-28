let forumAPI = localHostAPI + "api/Forum";
const params = new URLSearchParams(window.location.search);
const issueId = params.get("issueId");
const topicName = params.get("topicName");
let userConnected = sessionStorage.getItem("id");

let getIssueAPI = (forumAPI += "/issueId/" + issueId); //get issue api
ajaxCall("GET", getIssueAPI, "", issueGetSCB, issueGetECB);

function issueGetSCB(issue) {
  document.title = issue[0].title + " - MediQuick";
  let cont = document.getElementById("container");
  let str = "";
  str += `<div class="go-prev" onclick="GoToPreviousPage(${issue[0].topicid})"><img src="./../images/icons/go-prev-right-arrow.svg" alt=""> חזרה לפורום ${topicName}</div>`;
  if (issue[0].isClosed) {
    str += `<div class="cant-comment" onclick="">הסוגיה נעולה, לא ניתן להגיב</div>`;
  } else {
    str +=`<div class="add-comment" onclick="AddComment()"><img src="./../images/icons/plus-circle.svg" alt=""> הוסף תגובה</div>`;
  }

  //let formattedIssueDateTime = formatDateTime(issue[0].createdAt);
  let formattedIssueDateDite = "";
  let formattedCommentDateTime = "";
  str += `<div class="issue">
                <div class="issue-headers">
                    <div class="create-details">
                        <div class="date-time">${formatDateTime(issue[0].issueCreatedAt).date + " " + formatDateTime(issue[0].issueCreatedAt).time}</div>
                        <div class="creator">${issue[0].issueUserFullName}</div>`
                        if (issue[0].issueCreatorId == userConnected) {
                          str += `<div class="edit-comment"><img title="ערוך סוגיה" src="./../images/icons/edit-pencil.svg" alt="עריכה"></div>`
                          
                        }
                    str += `</div>
                    <div class="title"><h4>${issue[0].title}</h4></div>
                    <div class="issue-content">${issue[0].issueContent}</div>
                    <div class="num-of-comments">${issue[0].commentCount} תגובות</div>
                </div>
            </div>
            `;
  //render the issue, for loop for the comments
  if (issue[0].commentCount > 0) {
    str += `<h3>תגובות</h3>`;
    for (let i = 0; i < issue.length; i++) {
      formattedCommentDateTime = formatDateTime(issue[i].commentCreatedAt);
      str += `<div class="comment">
                <div class="create-details">
                    <div class="comment-id">#${i + 1}</div>
                    <div class="date-time">${formattedCommentDateTime.date} ${
        formattedCommentDateTime.time
      }</div>
                    <div class="creator">${issue[i].userFullName}</div>`
                    if (issue[i].commentCreatorId == userConnected) {
                      str += `<div class="edit-comment"><img title="ערוך תגובה" src="./../images/icons/edit-pencil.svg" alt="עריכה"></div>`
                      str += `<div class="edit-comment"><img title="מחק תגובה" src="./../images/icons/clear-form-24.svg" alt="מחיקה"></div>`
                    }
                str += `</div>
                <div class="comment-content">
                    ${issue[i].commentContent}
                </div>
            </div>`;
    }
  }
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

function formatDateTime(dateTime) {
  var date = dateTime.split("T")[0];
  var time = dateTime.split("T")[1].split(".")[0];

  var dateParts = date.split("-");
  var formattedDate = `${dateParts[2]}.${dateParts[1]}.${dateParts[0]}`;

  return {
    date: formattedDate,
    time: time,
  };
}

function GoToPreviousPage(topicId) {
  window.location.href = `forum-topic.html?topicId=${topicId}&topicName=${topicName}`;
}

function AddComment() {
  //הוספת תגובה
}
