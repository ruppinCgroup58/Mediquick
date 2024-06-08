let questionsAPI = "https://localhost:7253/api/Questions";
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
                </div>
            </div>`;
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

// function OpenCloseEL() {
//   //Adding event listeners to handle the logic of questions open and closed display types
//   const questionsCollection = document.getElementsByClassName("question");
//   const questionsArray = Array.from(questionsCollection);
//   questionsArray.forEach((question) => {
//     question.addEventListener("click", () => {
//       // Close all other divs
//       questionsArray.forEach((d) => {
//         if (d !== question) {
//           d.classList.remove("open");
//           d.classList.add("closed");
//         }
//       });

//       // Toggle the clicked div
//       if (question.classList.contains("open")) {
        
//         question.classList.remove("open");
//         question.classList.add("closed");
//       } else {
//         question.classList.remove("closed");
//         question.classList.add("open");
//         setTimeout(() => {
//           console.log("Process ended after 0.7 seconds.");
//           question.style.height = 'auto';
//       }, 200);
//       }
//     });
//   });
// }

// Add this function to set the height to auto after transition ends
// function setAutoHeight(element) {
//   element.style.height = 'auto';
// }

// function OpenCloseEL() {
//   const questionsCollection = document.getElementsByClassName("question");
//   const questionsArray = Array.from(questionsCollection);
//   questionsArray.forEach((question) => {
//       question.addEventListener("click", () => {
//           // Close all other divs
//           questionsArray.forEach((d) => {
//               if (d !== question) {
//                   d.classList.remove("open");
//                   d.classList.add("closed");
//                   d.style.height = '50px';
//               }
//           });

//           // Toggle the clicked div
//           if (question.classList.contains("open")) {
//               question.classList.remove("open");
//               question.classList.add("closed");
//               question.style.height = '50px';
//           } else {
//               question.classList.remove("closed");
//               question.classList.add("open");
//               // Calculate the full height of the content
//               const fullHeight = question.scrollHeight + 'px';

//               // Use requestAnimationFrame to ensure the transition is smooth
//               requestAnimationFrame(() => {
//                   question.style.height = fullHeight;

//                   // Set the height to auto after the transition ends
//                   question.addEventListener('transitionend', function setAuto() {
//                       // Use requestAnimationFrame to ensure it's applied immediately
//                       requestAnimationFrame(() => {
//                           question.style.height = 'auto';
//                       });
//                       question.removeEventListener('transitionend', setAuto);
//                   });
//               });
//           }
//       });
//   });
// }

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
      const margin = calculateMargin();
      
      let expDiv = document.getElementsByClassName('open')[0].firstElementChild.lastElementChild;
      expDiv.style.marginTop = margin + "px";

    });
  });
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
}

function toggleFavouriteSCB(num) {
  console.log(num);
}

function toggleFavouriteECB(err) {
  alert(err.statusText);
}

function calculateMargin() {
  let contentDiv = document.getElementsByClassName('open')[0].firstElementChild.children[0];
      let optionsDiv = document.getElementsByClassName('open')[0].firstElementChild.children[1];
      let expDiv = document.getElementsByClassName('open')[0].firstElementChild.lastElementChild;

      let questionDivHieght = 238;
      let contentDivHeight = contentDiv.getBoundingClientRect().height;
      let optionsDivHeight = optionsDiv.getBoundingClientRect().height;
      let expDivHeight = expDiv.getBoundingClientRect().height;

      return questionDivHieght - contentDivHeight - optionsDivHeight - expDivHeight;
}