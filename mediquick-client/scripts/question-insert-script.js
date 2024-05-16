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
        creator: 17,
        iamgeURL: uploadImage()
    }

    ajaxCall("POST", questionsAPI, JSON.stringify(newQuestion), qiPostSCB, qiPostECB);
    return false;
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

function readImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = function(event) {
            const imageData = event.target.result; // Base64-encoded image data
            resolve(imageData);
        };
        reader.onerror = function(error) {
            reject(error);
        };
        reader.readAsDataURL(file);
    });
}

function uploadImage() {
    const fileInput = document.getElementById('imageInput');
    const file = fileInput.files[0];

    if (!file) {
        console.error('No file selected');
        return;
    }

    readImage(file)
        .then(imageData => {
            console.log('Image read successfully:', imageData);
            // Do whatever you want with imageData here
            return imageData;
        })
        .catch(error => {
            console.error('Error reading image:', error);
        });
}