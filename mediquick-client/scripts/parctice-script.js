let topicAPI = "https://localhost:7253/api/Topics";
let practiceAPI = "https://localhost:7253/";
let generatePracticeAPI = practiceAPI += "GeneratePractice";
let userConnected = sessionStorage.getItem("id");
let questions = [];
let explanations = [];
let heartIcons = [];
let currentQuestionIndex = 0;
let explShown = 0;
var shuffledQuestions;

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

//Activated when a user clicks on an option
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
      HandleQuestionAnswer();
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

function shuffleAnswers(questionsList) {
  return questionsList.map(question => {
    // צור רשימה של תשובות עם המידע הנוסף האם הן נכונות או לא
    const answers = [
      { content: question.correctAnswer, isCorrect: true },
      { content: question.wrongAnswer1, isCorrect: false },
      { content: question.wrongAnswer2, isCorrect: false },
      { content: question.wrongAnswer3, isCorrect: false }
    ];

    // ערבל את התשובות
    for (let i = answers.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [answers[i], answers[j]] = [answers[j], answers[i]];
    }

    // החזר את השאלה עם התשובות המעורבלות
    return {
      ...question,
      shuffledAnswers: answers
    };
  });
}


function startPracticeSCB(questionsList) {
  let counter = 1;
  shuffledQuestions = shuffleAnswers(questionsList);
  questions = shuffledQuestions.map(
    (question) => `<div id="${counter - 1}" class="question-wrapper">
                      <div class="question-content">
                          <b>${counter++}. ${question.content}</b>
                      </div>
                      <div class="options">
                          <ul>
                              <li>
                                  <div class="option-1">
                                      <p data-number="0" class="option">א. ${question.shuffledAnswers[0].content}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-2">
                                      <p data-number="1" class="option">ב. ${question.shuffledAnswers[1].content}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-3">
                                      <p data-number="2" class="option">ג. ${question.shuffledAnswers[2].content}</p>
                                  </div>
                              </li>
                          </ul>
                          <ul>
                              <li>
                                  <div class="option-4">
                                      <p data-number="3" class="option">ד. ${question.shuffledAnswers[3].content}</p>
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

function HandleQuestionAnswer() {
  let qId = document.getElementById("question-container").firstChild.id;
  let answerChosenIndex = document.getElementsByClassName('selected-option')[0].dataset.number;
  let isCorrect = shuffledQuestions[qId].shuffledAnswers[answerChosenIndex].isCorrect
  console.log(isCorrect);
  let qSerialNumber = shuffledQuestions[qId].questionSerialNumber;
  console.log(qSerialNumber)
  let practiceReq = {
    qId: qSerialNumber,
    userId: userConnected,
    isCorrect: isCorrect
  }
  console.log(practiceReq)
  // לשלוח בקשה עם המשתנים USERID, QUESTIONID, ISCORRECT
  // לנתיב שנטלי תיצור
//   ajaxCall(
//     "POST",
//     generatePracticeAPI,
//     JSON.stringify(practiceReq),
//     startPracticeSCB,
//     startPracticeECB
//   );
}