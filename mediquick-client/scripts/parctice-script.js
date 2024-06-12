let topicAPI = "https://localhost:7253/api/Topics";
let practiceAPI = "https://localhost:7253/api/Practices";
ajaxCall("GET", topicAPI, "", topicGetSCB, topicGetECB);
$("#start-practice-form").submit(startPracticeFormSubmit);

function startPracticeFormSubmit() {
  const selectedTopicsArray = [];
  document
    .querySelectorAll('#topicsList input[type="checkbox"]:checked')
    .forEach((checkbox) => {
      selectedTopicsArray.push(checkbox.value);
    });

  // Convert the selectedTopics array to a string
  const selectedTopicsString = selectedTopicsArray.join(",");

  const selectedDiffLevelsArray = [];
  document
    .querySelectorAll('#diffsList input[type="checkbox"]:checked')
    .forEach((checkbox) => {
      selectedDiffLevelsArray.push(checkbox.value);
    });

  // Convert the selectedTopics array to a string
  const selectedDiffLevelsString = selectedDiffLevelsArray.join(",");

  //send the 2 strings to the server, the userid can be sent in the url
  const practiceStringObject = {
    practiceSerialNuber: "1",
    questionsList: [],
    selectedTopics: selectedTopicsString,
    selectedDiffLevels: selectedDiffLevelsString
  };
  ajaxCall(
    "POST",
    practiceAPI,
    JSON.stringify(practiceStringObject),
    startPracticeSCB,
    startPracticeECB
  );
  return false;
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
