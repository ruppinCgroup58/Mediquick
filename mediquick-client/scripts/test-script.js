let testAPI = localHostAPI + "api/Tests";
let userConnected = sessionStorage.getItem("id");
let testId;

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
  testId = testQuestionObject.testId; //אובייקט שחוזר מהשרת ומכיל testId ושאר פרטי השאלה
  let testContentDiv = document.querySelector(".test-content");
  let str = "";
  let i = 1;
  str += `<div class="grid-list">`;
  while (i <= 30) {
    str += `<div data-number="${i}" class="grid-item">${i}</div>`;
    i++;
  }
  str += `</div>`;
  console.log(str);
  str += renderSingleQuestion(testQuestionObject);
  testContentDiv.innerHTML = AddStringToStart(str, testContentDiv.innerHTML);
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

function renderSingleQuestion(question) {
  const shuffledQuestion = shuffleSingleQuestionAnswers(question);
  let counter = 1;
  str = "";
  str += `<div id="${counter - 1}" class="question-wrapper">
            <div class="question-content">
                <b>${counter++}. ${shuffledQuestion.content}</b>
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
            </div>
        </div>`;
        return str;
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
    });
});
}

function NextQuestion() {

}

function EndTest() {

}
