let GetQuestionsByTopicAPI = "https://localhost:7253/api/Questions/topicName/";
const params = new URLSearchParams(window.location.search);
const topicName = params.get("topicName");
GetQuestionsByTopicAPI += topicName;
ajaxCall(
  "GET",
  GetQuestionsByTopicAPI,
  "",
  getQuestionsByTopicSCB,
  getQuestionsByTopicECB
);

function OpenCloseEL() {
  //Adding event listeners to handle the logic of questions open and closed display types
  const questionsCollection = document.getElementsByClassName("question");
  const questionsArray = Array.from(questionsCollection);
  questionsArray.forEach((question) => {
    question.addEventListener("click", () => {
      // Close all other divs
      questionsArray.forEach((d) => {
        if (d !== question) {
          d.classList.remove("open");
          d.classList.add("closed");
        }
      });

      // Toggle the clicked div
      if (question.classList.contains("open")) {
        question.classList.remove("open");
        question.classList.add("closed");
      } else {
        question.classList.remove("closed");
        question.classList.add("open");
      }
    });
  });
}

function HeartIconEL() {
  //Adding event listeners to handle the logic of adding a question to favourites
  const heartIconCollection = document.getElementsByClassName("icon");
  const heartIconArray = Array.from(heartIconCollection);
  heartIconArray.forEach((icon) => {
    icon.addEventListener("click", (event) => {
      event.stopPropagation();
      if (icon.classList.contains("fav")) {
        icon.src = "./../images/icons/empty-heart.svg";
        icon.classList.remove("fav");
        //----adding to favourites code will be here----
      } else {
        icon.src = "./../images/icons/full-heart.svg";
        icon.classList.add("fav");
        //----removing from favourites code will be here----
      }
    });
  });
}
function getQuestionsByTopicSCB(questionsList) {
  $("#main-header")[0].innerHTML += topicName;
  document.title += ` ${topicName}`;
  let str = "";
  for (let i = 0; i < questionsList.length; i++) {
    str = `<div class="closed question">
    <div class="question-wrapper">
        <div class="question-content">
            <b>${questionsList[i].questionSerialNumber}. 
              ${questionsList[i].content}</b>
        </div>
        <div class="options">
            <div class="option-1">
                 א. ${questionsList[i].correctAnswer}
            </div>
            <div class="option-2">
                 ב. ${questionsList[i].wrongAnswer1}
            </div>
            <div class="option-3">
                 ג. ${questionsList[i].wrongAnswer2}
            </div>
            <div class="option-4">
                ד. ${questionsList[i].wrongAnswer3}
            </div>
            <br>
            <div class="explanation">
            <b><u>הסבר:</u></b> ${questionsList[i].explanation}
            </div>
        </div>
    </div>
    <img class="icon"
        src="./../images/icons/empty-heart.svg" alt="" srcset="">
</div>`;
    if (i <= questionsList.length / 2 - 0.5) {
      document.getElementsByClassName("col1")[0].innerHTML += str;
    } else {
      document.getElementsByClassName("col2")[0].innerHTML += str;
    }
  }
  OpenCloseEL();
  HeartIconEL();
}

function getQuestionsByTopicECB(err) {
  alert(err.statusText);
}
