let GetQuestionsByTopicAPI = "https://localhost:7253/api/Questions/topicName/";
const params = new URLSearchParams(window.location.search);
const topicName = params.get('topicName');
GetQuestionsByTopicAPI += topicName;
ajaxCall("GET", GetQuestionsByTopicAPI, "", getQuestionsByTopicSCB, getQuestionsByTopicECB);

const questionsCollection = document.getElementsByClassName('question');
const questionsArray = Array.from(questionsCollection);
questionsArray.forEach(question => {
    question.addEventListener('click', () => {
        // Close all other divs
        questionsArray.forEach(d => {
            if (d !== question) {
                d.classList.remove('open');
                d.classList.add('closed');
            }
        });

        // Toggle the clicked div
        if (question.classList.contains('open')) {
            question.classList.remove('open');
            question.classList.add('closed');
        } else {
            question.classList.remove('closed');
            question.classList.add('open');
        }
    });
});

const heartIconCollection = document.getElementsByClassName('icon');
const heartIconArray = Array.from(heartIconCollection);
heartIconArray.forEach(icon => {
    icon.addEventListener('click', (event) => {
        event.stopPropagation();
        if(icon.classList.contains('fav')) {
            icon.src = './../images/icons/empty-heart.svg';
            icon.classList.remove('fav');
            //----adding to favourites code will be here----
        } else {
            icon.src = './../images/icons/full-heart.svg';
            icon.classList.add('fav');
            //----removing from favourites code will be here----
        }
        
    })
})

function getQuestionsByTopicSCB(questionsList) {
    str = "";
    for (let i = 0; i < questionsList.length; i++) {
    str = `<div class="closed question">
    <div class="question-wrapper">
        <div class="question-content">
            ${questionsList[0].content}
        </div>
        <div class="options">
            <div class="option-1">
                 א. ${questionsList[0].correctAnswer}
            </div>
            <div class="option-2">
                 ב. ${questionsList[0].wrongAnswer1}
            </div>
            <div class="option-3">
                 ג. ${questionsList[0].wrongAnswer2}
            </div>
            <div class="option-4">
                ד. ${questionsList[0].wrongAnswer3}
            </div>
            <br>
            <div class="explanation">
            ${questionsList[0].explanation}
            </div>
        </div>
    </div>
    <img class="icon"
        src="./../images/icons/empty-heart.svg" alt="" srcset="">
</div>`;
    if (i <= questionsList.length/2) {
        document.getElementsByClassName("col1")[0].innerHTML += str;
    } else {
        document.getElementsByClassName("col1")[0].innerHTML += str;
    }
    }
}

function getQuestionsByTopicECB(err) {
    alert(err.statusText);
}