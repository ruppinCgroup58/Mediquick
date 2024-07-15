const params = new URLSearchParams(window.location.search);
const topicId = params.get("topicId");
const topicName = params.get("topicName");
document.title += " " + topicName;
document.getElementById("main-header").innerHTML += " " + topicName;