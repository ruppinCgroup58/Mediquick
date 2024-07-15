let topicAPI = "https://localhost:7253/api/Topics";
let userConnected = sessionStorage.getItem("id");
console.log(userConnected)
ajaxCall("GET", topicAPI, "", topicGetSCB, topicGetECB);

function topicGetSCB(topicList) {
    let cont = document.getElementById("container");
    let str = "";
    for (var i = 0; i < topicList.length; i++) {
      str += `<div class="topic-forum" onclick="GoToForumTopicPage(${topicList[i].topicId +", '"+ topicList[i].topicName}')">${topicList[i].topicName}</div>`
    }
    cont.innerHTML += str;
  }
  function topicGetECB(err) {
    alert(err.statusText);
  }
function GoToForumTopicPage(topicId, topicName) {
    window.location.href = `forum-topic.html?topicId=${topicId}&topicName=${topicName}`
}