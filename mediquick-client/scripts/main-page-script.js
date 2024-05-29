let topicApi = "https://localhost:7253/api/Topics";
let userConnected = sessionStorage.getItem('user');
topicApi += '/UserID/' + userConnected;
ajaxCall("GET", topicApi, '', topicGetSCB, topicGetECB);

document.addEventListener("DOMContentLoaded", function() {
    const circles = document.querySelectorAll('.percentage-circle');

    circles.forEach(circle => {
        const percentage = circle.getAttribute('data-percentage');
        const progress = circle.querySelector('.circle-progress');
        const offset = 251.2 - (251.2 * percentage / 100);
        progress.style.strokeDashoffset = offset;
    });
});