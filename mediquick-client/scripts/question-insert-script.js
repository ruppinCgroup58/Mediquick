let questionsAPI = "https://localhost:7253/api/Questions"

$("#qiForm").submit(qiFormSubmit)

function qiFormSubmit() {
    newQuestion = {
        difficulty: 0,
        content : $("#content").val(),
        correctAnswer : $("#correct-answer").val(),
        wrongAnswer1 : $("#wrong-answer1").val(),
        wrongAnswer2 : $("#wrong-answer2").val(),
        wrongAnswer3 : $("#wrong-answer3").val(),
        explanation : $("#explanation").val(),
        status: false,
        creator: 17
    }
    
    ajaxCall("POST", questionsAPI, JSON.stringify(newQuestion), qiPostSCB, qiPostECB);
}

function qiPostSCB(isSuccess) {
    if (isSuccess) {
        alert("Question added successfully");
    } else {
        alert("Adding failed");
    }
}

function qiPostECB(err) {
    alert(err.statusText);
}