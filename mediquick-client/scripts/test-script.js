let testAPI = localHostAPI + "api/Tests";
let userConnected = sessionStorage.getItem("id");
let testId;
var chosenAnswerIndex = -1;
let questionsCounter = 1;
let shuffledQuestion;

function startTest() {
  let startTestAPI = testAPI + "/userId/" + userConnected;
  ajaxCall("POST", startTestAPI, userConnected, startTestSCB, startTestECB);
  var preTest = document.querySelector(".pre-test");
  var testContent = document.querySelector(".test-content");

  // הסתרת pre-test עם אנימציה
  preTest.style.opacity = "0";
  preTest.style.maxHeight = "0";

  // הצגת test-content עם אנימציה לאחר זמן קצר
  setTimeout(function () {
    preTest.style.display = "none";
    testContent.style.display = "flex";
    setTimeout(function () {
      testContent.style.opacity = "1";
      testContent.style.maxHeight = "1000px"; // הגדר גובה מלא שמתאים לתוכן שלך
    }, 50);
  }, 500);
}

function startTestSCB(testQuestionObject) {
  testID = testQuestionObject.testID; //אובייקט שחוזר מהשרת ומכיל testId ושאר פרטי השאלה
//   let testContentDiv = document.querySelector(".test-content");
  let str = "";
  let i = 1;
  str += `<div class="grid-list">`;
  while (i <= 30) {
    str += `<div data-number="${i}" class="grid-item">${i}</div>`;
    i++;
  }
  str += `</div>
            <div id="0" class="question-wrapper"></div>`;
  console.log(str);
  document.querySelector(".upDiv").innerHTML += AddStringToStart(str, document.querySelector(".upDiv").innerHTML);
  document.querySelector(".question-wrapper").innerHTML += renderSingleQuestion(testQuestionObject);
  //testContentDiv.innerHTML = AddStringToStart(str, testContentDiv.innerHTML);
  HandleSelectedAnswer();
}

function startTestECB(err) {
  alert(err.statusText);
}

// Test timer Function
function startCountdown(duration, display) {
  var timer = duration,
    hours,
    minutes,
    seconds;
  setInterval(function () {
    hours = parseInt(timer / 3600, 10);
    minutes = parseInt((timer % 3600) / 60, 10);
    seconds = parseInt(timer % 60, 10);

    hours = hours < 10 ? "0" + hours : hours.toString();
    minutes = minutes < 10 ? "0" + minutes : minutes.toString();
    seconds = seconds < 10 ? "0" + seconds : seconds.toString();

    display.textContent = hours + ":" + minutes + ":" + seconds;

    if (--timer < 0) {
      timer = 0;
      alert("הזמן נגמר!");
    }
  }, 1000);
}

var testTimeLimit = 60 * 90, // 1.5 hours in seconds
  display = document.querySelector("#countdown");
startCountdown(testTimeLimit, display);

function AddStringToStart(textToAdd, currentString) {
  return textToAdd + currentString;
}
//Get a question object as input, shuffles the answers and returns it as a string, ready to render
function renderSingleQuestion(question) {
  shuffledQuestion = shuffleSingleQuestionAnswers(question);
  document.querySelector('.question-wrapper').dataset.number = questionsCounter;
  document.querySelector('.question-wrapper').id = shuffledQuestion.questionSerialNumber;
  /*<div id="q-${counter}" class="question-wrapper">*/
  str = "";
  str += `
            <div class="question-content">
                <b>${questionsCounter++}. ${shuffledQuestion.content}</b>
            </div>
            <div class="options">
                <ul>
                    <li>
                        <div class="option-1">
                            <p data-number="0" class="option">א. ${
                              shuffledQuestion.shuffledAnswers[0].content
                            }</p>
                        </div>
                    </li>
                </ul>
                <ul>
                    <li>
                        <div class="option-2">
                            <p data-number="1" class="option">ב. ${
                              shuffledQuestion.shuffledAnswers[1].content
                            }</p>
                        </div>
                    </li>
                </ul>
                <ul>
                    <li>
                        <div class="option-3">
                            <p data-number="2" class="option">ג. ${
                              shuffledQuestion.shuffledAnswers[2].content
                            }</p>
                        </div>
                    </li>
                </ul>
                <ul>
                    <li>
                        <div class="option-4">
                            <p data-number="3" class="option">ד. ${
                              shuffledQuestion.shuffledAnswers[3].content
                            }</p>
                        </div>
                    </li>
                </ul>
            </div>`;
            return str;
            //</div>`;
}

function shuffleSingleQuestionAnswers(question) {
  // צור רשימה של תשובות עם המידע הנוסף האם הן נכונות או לא
  const answers = [
    { content: question.correctAnswer, isCorrect: true },
    { content: question.wrongAnswer1, isCorrect: false },
    { content: question.wrongAnswer2, isCorrect: false },
    { content: question.wrongAnswer3, isCorrect: false },
  ];

  // ערבל את התשובות
  for (let i = answers.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [answers[i], answers[j]] = [answers[j], answers[i]];
  }

  // החזר את השאלה עם התשובות המעורבלות
  return {
    ...question,
    shuffledAnswers: answers,
  };
}

function HandleSelectedAnswer() {
    // בוחרים את כל האלמנטים מסוג p עם המחלקה option
const options = document.querySelectorAll('.options .option');

options.forEach(option => {
    option.addEventListener('click', function() {
        // הסרת המחלקה selected מכל האפשרויות
        options.forEach(opt => opt.classList.remove('selected-option'));

        // הוספת המחלקה selected לאלמנט שנלחץ
        this.classList.add('selected-option');
        chosenAnswerIndex = this.dataset.number;
    });
});
}

function GoToNextQuestion() {
    let chosenAnswerIndex = document.querySelector('.selected-option').dataset.number;

    let testReq = {
        userId: userConnected,
        testId: testID,
        questionId: document.querySelector('.question-wrapper').id,
        isCorrect: shuffledQuestion.shuffledAnswers[chosenAnswerIndex].isCorrect
    }
    let testNextQuestionAPI = localHostAPI + "HandleTestQuestionAnswer";
    ajaxCall("POST", testNextQuestionAPI, JSON.stringify(testReq), GoToNextQuestionSCB, GoToNextQuestionECB)

    //graphics grid
    let currentQuestionIndex = document.querySelector('.question-wrapper').dataset.number;
    document.querySelector(`[data-number="${currentQuestionIndex}"]`).classList.add("q-answered")

    chosenAnswerIndex = -1;
    if (currentQuestionIndex == 30) {
      EndTest()
    }
}

function GoToNextQuestionSCB(data) {
    console.log(data);
    document.querySelector(".question-wrapper").innerHTML = renderSingleQuestion(data);
    
    HandleSelectedAnswer();

}

function GoToNextQuestionECB(err) {
    alert(err.statusText);
}


function EndTest() {
  let endTestAPI = localHostAPI += "testId/" + testId;
  ajaxCall("POST", endTestAPI, userConnected, endTestSCB, endTestECB);
}

function endTestSCB() {
  //render after test stats
}

function endTestECB(err) {
  alert(err.statusText)
}
