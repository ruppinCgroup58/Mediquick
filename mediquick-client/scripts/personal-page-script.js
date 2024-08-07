let questionsAPI = localHostAPI + "api/Questions";
let userConnected = sessionStorage.getItem("id");
questionsAPI += `/userId/${userConnected}`;
ajaxCall(
  "GET",
  questionsAPI,
  "",
  getFavouriteQuestionsSCB,
  getFavouriteQuestionsECB
);

function getFavouriteQuestionsSCB(questionsList) {
  let str = "";
  for (let i = 0; i < questionsList.length; i++) {
    str += `<div class="closed question">
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
                    </div>
                    <div class="explanation">
                        <b><u>הסבר:</u></b> ${questionsList[i].explanation}
                    </div>
                </div>`;
    if (questionsList[i].isFavourite != 0) {
      str += `<img class="icon fav"
                  src="./../images/icons/full-heart.svg" onclick="toggleFavourite(${questionsList[i].questionSerialNumber})" alt="" srcset="">`;
    } else {
      str += `<img class="icon"
                  src="./../images/icons/empty-heart.svg" onclick="toggleFavourite(${questionsList[i].questionSerialNumber})" alt="" srcset="">`;
    }
    str += `</div>`;
  }
  document.getElementById("questions-container").innerHTML += str;
  OpenCloseEL();
  HeartIconEL();
}

function getFavouriteQuestionsECB(err) {
  alert(err.statusText);
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

      //calculate the margin-top for the explanation div to be at the buttom of the question div
      let openDiv = document.getElementsByClassName("open");
      if (openDiv.length) {
        let expDiv = openDiv[0].firstElementChild.lastElementChild;
        const margin = calculateMargin();
        expDiv.style.marginTop = margin + "px";
      }
    });
  });
}

function toggleFavourite(questionId) {
  let toggleFavouritesAPI = localHostAPI + `api/Questions/questionId/${questionId}/userId/${userConnected}`;
  ajaxCall(
    "POST",
    toggleFavouritesAPI,
    "",
    toggleFavouriteSCB,
    toggleFavouriteECB
  );
}

function toggleFavouriteSCB(num) {
  console.log(num);
}

function toggleFavouriteECB(err) {
  alert(err.statusText);
}

function calculateMargin() {
  let contentDiv =
    document.getElementsByClassName("open")[0].firstElementChild.children[0];
  let optionsDiv =
    document.getElementsByClassName("open")[0].firstElementChild.children[1];
  let expDiv =
    document.getElementsByClassName("open")[0].firstElementChild
      .lastElementChild;

  let questionDivHieght = 198;
  let contentDivHeight = contentDiv.getBoundingClientRect().height;
  let optionsDivHeight = optionsDiv.getBoundingClientRect().height;
  let expDivHeight = expDiv.getBoundingClientRect().height;

  return questionDivHieght - contentDivHeight - optionsDivHeight - expDivHeight;
}

//statistics
$(document).ready(function () {
    loadUserTopicStats(userConnected); 
    loadAllTestAverageAndGrades(userConnected);
    loadUserAverageAndGradesPerMonth(userConnected);
});

function loadUserTopicStats(userID) {
    $.ajax({
        url: localHostAPI + `api/Users/UserTopicStats/${userID}`,
        method: 'GET',
        success: function (data) {
            displayCharts(data);
        },
        error: function (error) {
            console.error(error);
            alert("Failed to load stats. Please check the User ID and try again.");
        }
    });
}
function displayCharts(data) {
    const topicNames = data.map(item => item.topicName);
    const totalQuestions = data.map(item => item.totalQuestions);
    const correctAnswers = data.map(item => item.correctAnswers);
    const percentageCorrect = data.map(item => item.percentageCorrect);

    const ctx1 = document.getElementById('questionsChart').getContext('2d');
    const ctx2 = document.getElementById('correctAnswersChart').getContext('2d');
    const ctx3 = document.getElementById('percentageCorrectChart').getContext('2d');

    new Chart(ctx1, {
        type: 'bar',
        data: {
            labels: topicNames,
            datasets: [{
                label: 'Total Questions',
                data: totalQuestions,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'כמות שאלות שנענו מכל נושא'
                }
            }
        }
    });

    new Chart(ctx2, {
        type: 'bar',
        data: {
            labels: topicNames,
            datasets: [{
                label: 'Correct Answers',
                data: correctAnswers,
                backgroundColor: 'rgba(153, 102, 255, 0.2)',
                borderColor: 'rgba(153, 102, 255, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'מספר התשובות הנכונות לפי נושא'
                }
            }
        }
    });

    new Chart(ctx3, {
        type: 'polarArea',
        data: {
            labels: topicNames,
            datasets: [{
                label: 'Percentage Correct',
                data: percentageCorrect,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: false // הסתרת המקרא
                },
                title: {
                    display: true,
                    text: 'אחוז התשובות הנכונות לפי נושא'
                }
            }
        }
    });
}

function loadAllTestAverageAndGrades(userID) {
    $.ajax({
        url: localHostAPI + `api/Users/AllTestAverageAndGrades/${userID}`,
        method: 'GET',
        success: function (data) {
            AllTestAverageAndGradesSCB(data);
        },
        error: function (error) {
            console.error(error);
            alert("BLAT.");
        }
    });
}

function AllTestAverageAndGradesSCB(data) {
    document.getElementById("countOfTest").innerHTML = data.numberOfTests;
    document.getElementById("avgOfTestGrades").innerHTML = data.averageGrade;
} 

function loadUserAverageAndGradesPerMonth(userID) {
  $.ajax({
      url: localHostAPI + `api/Users/UserAverageAndGradesPerMonth/${userID}`,
        method: 'GET',
        success: function (data) {
            displayTestStatsPerMonth(data);
        },
        error: function (error) {
            console.error(error);
            alert("Failed to load stats. Please check the User ID and try again.");
        }
    });
}


function displayTestStatsPerMonth(data) {
    const container = document.getElementById('timeline-container');
    data.forEach(item => {
        const timelineItem = document.createElement('div');
        timelineItem.classList.add('timeline-item');
        timelineItem.innerHTML = `
                    <div class="timeline-item-content">
                        <span class="tag" style="background-color: #2196F3;">${item.testYear}-${item.testMonth}</span>
                        <p>סה"כ מבחנים: ${item.totalTestsCompleted}</p>
                        <p>ממוצע ציונים: ${item.averageGrade.toFixed(2)}</p>
                        <span class="circle"></span>
                    </div>
                `;
        container.appendChild(timelineItem);
    });
}
