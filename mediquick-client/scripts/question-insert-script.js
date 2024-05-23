let questionsAPI = "https://localhost:7253/api/Questions"

$("#qiForm").submit(confirmQuestionSubmit)

function confirmQuestionSubmit() {
    // Ask the user a yes or no question
    var userResponse = confirm("האם אתה בטוח? לא ניתן לערוך את השאלה לאח מכן");

    // Check the user's response
    if (userResponse) {
        // User clicked "OK"
        qiFormSubmit();
        
    } else {
        return false;
    }
}

function qiFormSubmit() {
    let userConnected = sessionStorage.getItem('user');
    newQuestion = {
        difficulty: 1,
        content : $("#content").val(),
        correctAnswer : $("#correct-answer").val(),
        wrongAnswer1 : $("#wrong-answer1").val(),
        wrongAnswer2 : $("#wrong-answer2").val(),
        wrongAnswer3 : $("#wrong-answer3").val(),
        explanation : $("#explanation").val(),
        status: 0,
        creator: userConnected
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