﻿var apiUsers = 
var apiQuestion = 
$(document).ready(function () {

})

function getUsersDataTable() {
    ajaxCall("GET", apiUsers, "", usersTableGetSCB, usersTableGetECB);
    ajaxCall("GET", apiQuestion, "", questionTableGetSCB, questionTableGetECB);
    document.getElementById("usersTable").style.visibility = "visible";
}

function usersTableGetSCB(usersList) {
    users = usersList; // keep the cars array in a global variable;
    try {
        tbl = $('#usersTable').DataTable({
            data: usersList,
            pageLength: 10,
            columns: [
                { data: "firstName" },
                { data: "lastName" },
                { data: "email" },
                { data: "password" },
                { data: "phoneNumber" },
                {
                    data: "isAdmin",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserStatus(this)"/>';
                    }
                },
                {
                    data: "isActive",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserStatus(this)"/>';
                    }
                },
            ],
        });
    }
    catch (err) {
        alert(err);
    }
}

function usersTableGetECB(err) {
    alert("Error: " + err);
}

function questionTableGetSCB(questionList) {
    questins = questionList; // keep the cars array in a global variable;
    try {
        tbl = $('#QuestionTable').DataTable({
            data: questionList,
            pageLength: 10,
            columns: [
                { data: "questionSerialNumber" },
                { data: "difficulty" },
                { data: "content" },
                { data: "correctAnswer" },
                { data: "wrongAnswer1" },
                { data: "wrongAnswer2" },
                { data: "wrongAnswer3" },
                { data: "explanation" },
                {
                    data: "status",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserStatus(this)"/>';
                    }
                },
                { data: "creator" },
                { data: "totalAnswers" },
                { data: "totalCorrectAnswers" },

            ],
        });
    }
    catch (err) {
        alert(err);
    }
}

function usersTableGetECB(err) {
    alert("Error: " + err);
}
function changeUserStatus(user) {
    userEmail = user.parentElement.parentElement.children[3].innerHTML;
    if (user.checked) {
        newStatus = true;
    } else {
        newStatus = false;
    }
    let address = apiUsers + /email/${ userEmail }/newStatus/${ newStatus };
    ajaxCall("POST", address, "", usersTablePostSCB, usersTablePostECB);
}
function usersTablePostSCB(answer) {
    alert(answer);
}
function usersTablePostECB(err) {
    alert("Error: " + err);
}

function getReport() {
    let selectedMonth = document.getElementById("month").value;
    let address = apiVacations + /${selectedMonth};
    ajaxCall("GET", address, "", getReportSCB, getReportECB);
}

function getReportSCB(objectList) {
    let str = `<table border="1" id="usersTable" class="display nowrap" style="width:100%" > <thead>
                    <tr>
                        <th>City</th>
                        <th>Average Price</th>
                    </tr>
                    </thead>`;
    for (let i = 0; i < objectList.length; i++) {
        str += `<tr>
                    <td>${objectList[i]["city"]}</td>
                    <td>${objectList[i]["avg"]}</td>
                </tr>`
    }
    str += '</table>';
    document.getElementById("report").innerHTML = str;
}
function getReportECB(err) {
    alert("Error: " + err);
}