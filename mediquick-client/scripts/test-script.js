let testAPI = localHostAPI + "api/Tests"
let userConnected = sessionStorage.getItem("id");
let testId;

function startTest() {
    let startTestAPI = testAPI + "/userId/" + userConnected;
    //ajaxCall("POST", startTestAPI, userConnected, startTestSCB, startTestECB);
    var preTest = document.querySelector('.pre-test');
    var testContent = document.querySelector('.test-content');

    // הסתרת pre-test עם אנימציה
    preTest.style.opacity = '0';
    preTest.style.maxHeight = '0';

    // הצגת test-content עם אנימציה לאחר זמן קצר
    setTimeout(function() {
        preTest.style.display = 'none';
        testContent.style.display = 'block';
        setTimeout(function() {
            testContent.style.opacity = '1';
            testContent.style.maxHeight = '1000px'; // הגדר גובה מלא שמתאים לתוכן שלך
        }, 50);
    }, 500);
    
}

function startTestSCB(testObject) {
    testId = testObject.testId; //אובייקט שחוזר מהשרת ומכיל testId ושאר פרטי השאלה
    let testContentDiv = document.querySelector(".test-content");
    let str = "";
    let i = 1;
    str += `<div class="grid-list">`
    while (i <= 30) {
        str += `<div data-number="${i}" class="grid-item">${i}</div>`
    }
    str += `</div>`;
    testContentDiv.innerHTML += str;
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

