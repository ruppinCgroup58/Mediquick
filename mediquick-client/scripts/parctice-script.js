let topicApi = "https://localhost:7253/api/Topics";
ajaxCall("GET", topicApi, "", topicGetSCB, topicGetECB);
$("#start-practice-form").submit(startPracticeFormSubmit);

function startPracticeFormSubmit() {
  const selectedTopics = [];
  document
    .querySelectorAll('#topicsList input[type="checkbox"]:checked')
    .forEach((checkbox) => {
      selectedTopics.push(checkbox.value);
    });

  // Convert the selectedTopics array to a string
  const selectedTopicsString = selectedTopics.join(",");

  const selectedDiffs = [];
  document
    .querySelectorAll('#diffsList input[type="checkbox"]:checked')
    .forEach((checkbox) => {
      selectedTopics.push(checkbox.value);
    });

  // Convert the selectedTopics array to a string
  const selectedDiffsString = selectedTopics.join(",");

  //send the 2 strings to the server, the userid can be sent in the url
  ajaxCall("POST", topicApi, "", startPracticeSCB, startPracticeECB);

}

function topicGetSCB(topicList) {
  let str = "";
  for (var i = 0; i < topicList.length; i++) {
    str += `<li class="topic-item">
                <input type="checkbox" id="topic-${topicList[i].topicId}" value="${topicList[i].topicId}">
            <label for="topic-${topicList[i].topicId}">${topicList[i].topicName}</label>
            </li>`;
  }
  $("#topicsList")[0].innerHTML = str;
}

function topicGetECB(err) {
  alert(err.statusText);
}

function startPracticeSCB(questionsList) {
    //rendering the questions dynamically
    $("#practice")[0].innerHTML = str;
}

function startPracticeECB(err) {
    alert(err.statusText);
}