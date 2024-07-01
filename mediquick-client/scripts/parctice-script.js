let topicAPI = "https://localhost:7253/api/Topics";
let practiceAPI = "https://localhost:7253/";
let generatePracticeAPI = practiceAPI += "GeneratePractice";
let userConnected = sessionStorage.getItem("id");
let questions = [];
let explanations = [];
let heartIcons = [];
let currentQuestionIndex = 0;
let explShown = 0;

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
    selectedDiffLevels: selectedDiffLevelsString,
    userId: userConnected
  };
  
  ajaxCall(
    "POST",
    generatePracticeAPI,
    JSON.stringify(practiceStringObject),
    startPracticeSCB,
    startPracticeECB
  );
  return false;
}

const filterButton = $("#filter-btn")[0];
const startPracticeBtn = $("#start-practice-btn")[0];

// Add an event listener to the button
filterButton.addEventListener("click", toggleFilters);

function toggleFilters() {
  const filtersDiv = document.getElementById("filters");
  filtersDiv.classList.toggle("show");
}
startPracticeBtn.addEventListener("click", toggleFilters)

function AddELToExplBtn() {
  const explButton = $("#expl-btn")[0];
  explButton.addEventListener("click", toggleExpl);
}

function toggleExpl() {
  const explDiv = document.getElementsByClassName("expl-content")[0];
    if (getComputedStyle(explDiv).height == "0px") {
      explDiv.style.height = "100px";
      explDiv.style.opacity = "1";
      explShown = 1;
      $("#expl-btn")[0].innerHTML = 'הסתר הסבר'
    } else {
      explDiv.style.height = "0px";
      explDiv.style.opacity = "0";
      explShown = 0;
      $("#expl-btn")[0].innerHTML = 'הצג הסבר'
    }
}

function applySelectedAnimation() {
  //Selecting options
  document.querySelectorAll(".option").forEach((option) => {
    option.addEventListener("click", () => {
      // Remove 'selected' class from all options
      document
        .querySelectorAll(".option")
        .forEach((opt) => opt.classList.remove("selected-option"));
      // Add 'selected' class to the clicked option
      option.classList.add("selected-option");
    });
  });
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
  questions = questionsList.map(
    (question) => `<div class="question-wrapper">
                      <div class="question-content">
                          <b>${question.questionSerialNumber}. ${question.content}</b>
                      </div>
                      <div class="options">
                          <ul>
                              <li>
                                  <div class="option-1">
                                      <p class="option">א. ${question.correctAnswer}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-2">
                                      <p class="option">ג. ${question.wrongAnswer1}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-3">
                                      <p class="option">ג. ${question.wrongAnswer2}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-4">
                                      <p class="option">ג. ${question.wrongAnswer3}</p>
                                  </div>
                              </li>
                          </ul>
                      </div>
                      <button id="favourites-btn" class="secondary-btn" onclick="toggleFavourite(${question.questionSerialNumber})">
                    <span>הוסף למועדפים</span>
                    <img id="favourite-icon" src="" alt="">
                </button>
                  </div>`
  );

  explanations = questionsList.map((question) => `${question.explanation}`);
  heartIcons = questionsList.map((question) => `${question.isFavourite == 1 ? './../images/icons/full-heart.svg' : './../images/icons/empty-heart.svg'}`)

  renderQuestion(currentQuestionIndex);
  handleArrowBtn();
  //applySelectedAnimation();
}

function startPracticeECB(err) {
  alert(err.statusText);
}

function toggleFavourite(questionId) {
  let toggleFavouritesAPI = `https://localhost:7253/api/Questions/questionId/${questionId}/userId/${userConnected}`;
  ajaxCall(
    "POST",
    toggleFavouritesAPI,
    "",
    toggleFavouriteSCB,
    toggleFavouriteECB
  );
  toggleHeartIcon();
}

function toggleHeartIcon() {
  const heartIcon = $("#favourite-icon")[0];
  if (heartIcon.src.includes('empty')) {
    heartIcon.src = './../images/icons/full-heart.svg';
  } else {
    heartIcon.src = './../images/icons/empty-heart.svg';
  }
  heartIcons[currentQuestionIndex] = heartIcon.src;
}

function toggleFavouriteSCB(num) {
  console.log(num);
}

function toggleFavouriteECB(err) {
  alert(err.statusText);
}

//Practice: render questions, going next and previous

function renderQuestion(index) {
  const questionContainer = document.getElementById("question-container");
  const explanationText = document.getElementById("expl");
  questionContainer.innerHTML = questions[index];
  explanationText.innerHTML = explanations[index];
  let heartIcon = $("#favourite-icon")[0];
  heartIcon.src = heartIcons[index];
  applySelectedAnimation();
  AddELToExplBtn();
  if (explShown) {
    toggleExpl();
  }
}

function prevQuestion() {
  if (currentQuestionIndex > 0) {
    currentQuestionIndex--;
    renderQuestion(currentQuestionIndex);
  }
  handleArrowBtn();
}

function nextQuestion() {
  if (currentQuestionIndex < questions.length - 1) {
    currentQuestionIndex++;
    renderQuestion(currentQuestionIndex);
  }
  handleArrowBtn();
}

function handleArrowBtn() {
  //check if it's the first question
  if (currentQuestionIndex == 0) {
    document.getElementById("prevQ").classList.add("disabled-btn");
  } else {
    document.getElementById("prevQ").classList.remove("disabled-btn");
    // document.getElementById("nextQ").classList.remove("disabled-btn");
  }

  //check if it's the last question
  if (currentQuestionIndex == questions.length - 1) {
    document.getElementById("nextQ").classList.add("disabled-btn");
  } else {
    // document.getElementById("prevQ").classList.remove("disabled-btn");
    document.getElementById("nextQ").classList.remove("disabled-btn");
  }
}
