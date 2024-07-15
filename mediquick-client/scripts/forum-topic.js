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