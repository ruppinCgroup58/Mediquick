﻿const urlParams = new URLSearchParams(window.location.search);
const TESTID = urlParams.get('testId');

$(document).ready(function () {
    calculateAndUpdateScoreAndGetDuration(TESTID);
    getTestSummary(TESTID);
    getQuestionDetailsInTest(TESTID);
});

function calculateAndUpdateScoreAndGetDuration(testId) {
    const apiUrl = `${localHostAPI}api/Tests/CalculateAndUpdateScoreAndGetDuration/${testId}`;

    fetch(apiUrl, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            const { testScore, durationInSeconds } = data;

            document.getElementById('tesGrade').textContent = `${testScore}`;
            document.getElementById('tesTime').textContent = `${formatDuration(durationInSeconds)}`;
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to calculate and update score and duration.');
        });
}

function formatDuration(seconds) {
    const hours = Math.floor(seconds / 3600);
    const minutes = Math.floor((seconds % 3600) / 60);
    const remainingSeconds = seconds % 60;

    const formattedHours = hours.toString().padStart(2, '0');
    const formattedMinutes = minutes.toString().padStart(2, '0');
    const formattedSeconds = remainingSeconds.toString().padStart(2, '0');

    return `${formattedHours}:${formattedMinutes}:${formattedSeconds}`;
}




function getTestSummary(testId) {
    const apiUrl = `${localHostAPI}api/Tests/GetTestSummary/${testId}`;

    fetch(apiUrl, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Populate the table with topic statistics
            const topicsTableBody = document.querySelector('#topics-table tbody');
            topicsTableBody.innerHTML = ''; // Clear existing rows

            data.forEach(topic => {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${topic.topicName}</td>
                <td>${topic.totalQuestions}</td>
                <td>${topic.correctAnswers}</td>
                <td>${formatDuration(topic.averageResponseTimeSeconds)}</td>
            `;
                topicsTableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to load test summary.');
        });
}

// פונקציה שמבצעת קריאה לשרת לקבלת פרטי השאלות במבחן
function getQuestionDetailsInTest(testId) {
    const apiUrl = `${localHostAPI}api/Tests/GetQuestionDetailsInTest/${testId}`;

    fetch(apiUrl, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            renderQuestionDetails(data); // קריאה לפונקציה שמציגה את הנתונים
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to load question details.');
        });
}

// פונקציה שמציגה את פרטי השאלות בצורה דינאמית על המסך
function renderQuestionDetails(questions) {
    const container = document.getElementById('question-details-container');
    container.innerHTML = ''; // ניקוי מיכל פרטי השאלות לפני הוספת השאלות החדשות

    let counter = 1;  // התחלת המונה מ-1

    questions.forEach(question => {
        const questionCard = `
        <div class="question-card">
            <h4>שאלה מספר: ${counter}</h4>
            <p><strong>תוכן השאלה:</strong> ${question.content}</p>
            <p><strong>נושא:</strong> ${question.topicName}</p>
            <p><strong>רמת קושי:</strong> ${question.difficultyLevel}</p>
            <p><strong>זמן מענה:</strong> ${formatDuration(question.responseTimeSeconds)}</p>
            <p><strong>תשובה נכונה:</strong> ${question.isAnswerCorrect ? 'כן' : 'לא'}</p>
            <p><strong>הסבר:</strong> ${question.explanation}</p>
        </div>
    `;
        container.innerHTML += questionCard;
        counter++;  // העלאה של המונה ב-1 עבור כל שאלה
    }); 

}

