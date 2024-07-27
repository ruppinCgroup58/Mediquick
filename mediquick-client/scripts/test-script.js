let testAPI = localHostAPI + "api/Tests"
let userConnected = sessionStorage.getItem("id");

function startTest() {
    let startTestAPI = testAPI + "/userId/" + userConnected;
    ajaxCall("POST", startTestAPI, userConnected, startTestSCB, startTestECB);
}

function startTestSCB(questionsList) {

}

function startTestECB(err) {
    alert(err.statusText)
}


// Test timer Function
function startCountdown(duration, display) {
    var timer = duration, hours, minutes, seconds;
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
    display = document.querySelector('#countdown');
startCountdown(testTimeLimit, display);