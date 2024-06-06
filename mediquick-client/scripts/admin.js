﻿var apiUsers = 'https://localhost:7253/api/Users';
var apiReadQuestion = 'https://localhost:7253/ReadQuestions';
var geminiAPI = 'https://localhost:7253/Gemini';
var apiQuestion = 'https://localhost:7253/api/Questions/';
//$(document).ready(function () {

//})

 
$("#addQuestionForm").submit(addQuestionToGemini)
//$("#editUserModal").submit(editUserRow)

//user

function getUsersDataTable() {
    ajaxCall("GET", apiUsers, "", usersTableGetSCB, usersTableGetECB);
    return false;
}
function usersTableGetSCB(usersList) {
    $("#QuestionForm").css("visibility", "collapse");
    $("#UsersForm").css("visibility", "visible");
    $("#manageUsersBtn").prop("disabled", true);
    $("#manageQuestionsBtn").prop("disabled", false);

    users = usersList; // keep the cars array in a global variable;
    try {
        $('#usersTable').DataTable().destroy();
        tbl = $('#usersTable').DataTable({
            data: usersList,
            pageLength: 10,
            columns: [
                { data: "userID" },
                { data: "firstName" ,editable: true },
                { data: "lastName", editable: true },
                { data: "email", editable: true },
                { data: "password", editable: true },
                { data: "phoneNumber", editable: true },
                {
                    data: "isActive",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserStatus(this)"/>';
                    }
                },
                {
                    data: "isAdmin",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserAdminStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserAdminStatus(this)"/>';
                    }
                },

                {
                    data: null, // This column does not map to a property in the data
                    render: function (data, type, row, meta) {
                        return '<button class="edit-user-row" onclick=editUserRow(this); return false;>ערוך</button>';
                    }
                },
            ],
            language: {
                processing: "מעבד...",
                search: "חפש:",
                lengthMenu: "הצג _MENU_ פריטים",
                info: "מציג _START_ עד _END_ מתוך _TOTAL_ פריטים",
                infoEmpty: "מציג 0 עד 0 מתוך 0 פריטים",
                infoFiltered: "(מסונן מתוך _MAX_ פריטים)",
                infoPostFix: "",
                loadingRecords: "טוען...",
                zeroRecords: "לא נמצאו רשומות תואמות",
                emptyTable: "אין נתונים זמינים בטבלה",
                paginate: {
                    first: "ראשון",
                    previous: "קודם",
                    next: "הבא",
                    last: "אחרון"
                }
            }
        });
      
    }
    catch (err) {
        alert(err);
    }
}
function usersTableGetECB(err) {
    alert("Error: " + err);
}



//change User Status
function changeUserStatus(user) {
    userEmail = user.parentElement.parentElement.children[3].innerHTML;
    if (user.checked) {
        newStatus = true;
    } else {
        newStatus = false;
    }
    let address = apiUsers + `/email/${userEmail}/newStatus/${newStatus}`;
    ajaxCall("POST", address, "", changeUserStatusPostSCB,changeUserStatusPostECB);
}
function changeUserStatusPostSCB(answer) {
    alert(answer);
}
function changeUserStatusPostECB(err) {
    alert("Error: " + err);
}

//change admin status
function changeUserAdminStatus(user) {
    userEmail = user.parentElement.parentElement.children[3].innerHTML;
    if (user.checked) {
        newAdminStatus = true;
    } else {
        newAdminStatus = false;
    }
    let address = apiUsers + `/email/${userEmail}/newAdminStatus/${newAdminStatus}`;
    ajaxCall("POST", address, "", usersTablePostSCB, usersTablePostECB);
}
function usersTablePostSCB(answer) {
    alert(answer);
}
function usersTablePostECB(err) {
    alert("Error: " + err);
}



function editUserRow(item) {

    let editUserModal = document.getElementById("EditUserModal");

    // When the user clicks the button, open the modal
    console.log("1");
    
    editUserModal.style.display = "block";

    console.log("2");
    return false;

} 




//question
function getQuestisonsDataTable() {
    ajaxCall("GET", apiReadQuestion, "", questionsTableGetSCB, questionsTableGetECB);
}
function questionsTableGetSCB(questionsList) {
    $("#QuestionForm").css("visibility", "visible");
    $("#UsersForm").css("visibility", "collapse");
    $("#manageUsersBtn").prop("disabled", false);
    $("#manageQuestionsBtn").prop("disabled", true);
    questins = questionsList; // keep the cars array in a global variable;
    try {
        $('#QuestionTable').DataTable().destroy();
             tbl = $('#QuestionTable').DataTable({
                        data: questionsList,
                        pageLength: 10,
                        columns: [
                            { data: "questionSerialNumber" },
                            { data: "content" },
                            { data: "correctAnswer" },
                            { data: "wrongAnswer1" },
                            { data: "wrongAnswer2" },
                            { data: "wrongAnswer3" },
                            { data: "explanation" },
                            { data: "difficulty" },
                            {
                                data: "status",
                                render: function (data, type, row, meta) {
                                    if (data == 1)
                                        return `<select class="myList" onchange="changeQuestionStatus(this.parentElement.parentElement)">
                                                    <option value="מאושר" selected>מאושר</option>
                                                    <option value="ממתין">ממתין</option>
                                                    <option value="נדחה">נדחה</option>                                                    
                                                </select>`;
                                    else if(data == 0) {
                                        return `<select class="myList" onchange="changeQuestionStatus(this.parentElement.parentElement)">
                                                    <option value="מאושר">מאושר</option>
                                                    <option value="ממתין" selected>ממתין</option>
                                                    <option value="נדחה">נדחה</option>
                                                </select>`;

                                    } else {
                                        return `<select class="myList" onchange="changeQuestionStatus(this.parentElement.parentElement)">
                                                    <option value="מאושר">מאושר</option>
                                                    <option value="ממתין">ממתין</option>
                                                    <option value="נדחה" selected>נדחה</option>
                                                </select>`;
                                    }
                                }
                            },
                            { data: "creator" },
                            { data: "totalAnswers" },
                            { data: "totalCorrectAnswers" },
                            {
                                data: null, // This column does not map to a property in the data
                                render: function (data, type, row, meta) {
                                    return '<button class="edit-user-row" onclick=editQuestionRow(this)>ערוך</button>';
                                }
                            },
                 ],
                 language: {
                     processing: "מעבד...",
                     search: "חפש:",
                     lengthMenu: "הצג _MENU_ פריטים",
                     info: "מציג _START_ עד _END_ מתוך _TOTAL_ פריטים",
                     infoEmpty: "מציג 0 עד 0 מתוך 0 פריטים",
                     infoFiltered: "(מסונן מתוך _MAX_ פריטים)",
                     infoPostFix: "",
                     loadingRecords: "טוען...",
                     zeroRecords: "לא נמצאו רשומות תואמות",
                     emptyTable: "אין נתונים זמינים בטבלה",
                     paginate: {
                         first: "ראשון",
                         previous: "קודם",
                         next: "הבא",
                         last: "אחרון"
                     }
                 }
    });
       
       
    }
    catch (err) {
        alert(err);
    }
}
function questionsTableGetECB(err) {
    alert("Error: " + err);
}




//cahnge question status
function changeQuestionStatus(row) {
    newStatus = row.children[8].firstElementChild.value;
    if (newStatus == 'מאושר') {
        changedStatus = 1;
    }
    else if (newStatus == 'ממתין') {
        changedStatus = 0;
    }
    else {
        changedStatus = -1;
    }
    id = row.firstChild.innerHTML;
    let address = apiQuestion + `id/${id}`;
    ajaxCall("PUT", address, JSON.stringify(changedStatus), changeQuestionStatusSCB, changeQuestionStatusECB);
}
function changeQuestionStatusSCB(response) {
    alert("השאלה עודכנה בהצלחה");
    // כאן ניתן להוסיף פעולות נוספות לאחר העדכון המוצלח
}
function changeQuestionStatusECB(err) {
    let errorMessage;

    if (err.responseText) {
        errorMessage = JSON.parse(err.responseText).message;
    } else {
        errorMessage = "שגיאת תקשורת עם השרת";
    }

    alert(errorMessage);
    // כאן ניתן להוסיף פעולות נוספות לטיפול בשגיאה
}



//gemini
function addQuestionToGemini() {


    orderToGemini = `תייצר לי ${$("#numOfQuestions").val()} שאלות אמריקאיות עם 4 תשובות. בפורמט JSON עם השדות הבאים: 
    'Content'
    'CorrectAnswer'
    'WrongAnswer1'
    'WrongAnswer2'
    'WrongAnswer3'
    'Explanation'
        השאלות יתבססו על הטקסט הבא: ${$("#textInput").val()}`;

        ajaxCall("POST", geminiAPI, JSON.stringify(orderToGemini), GeminiQuestionGetSCB, GeminiQuestionGetECB);

}
function GeminiQuestionGetSCB(data) {
    alert("השאלות נוספו בהצלחה למאגר");
}
function GeminiQuestionGetECB(err) {
    alert(err.statusText);
}





// function getReportSCB(objectList) {
//     let str = `<table border="1" id="usersTable" class="display nowrap" style="width:100%" > <thead>
//                     <tr>
//                         <th>City</th>
//                         <th>Average Price</th>
//                     </tr>
//                     </thead>`;
//     for (let i = 0; i < objectList.length; i++) {
//         str += `<tr>
//                     <td>${objectList[i]["city"]}</td>
//                     <td>${objectList[i]["avg"]}</td>
//                 </tr>`
//     }
//     str += '</table>';
//     document.getElementById("report").innerHTML = str;
// }
// function getReportECB(err) {
//     alert("Error: " + err);
// }
// function resetForm() {
//     $("#addQuestionForm")[0].reset();
// }

//function addQuestionToGemini() {
//    var filePath = "";
//    var fileInput = document.getElementById('fileInput');
//    var file = fileInput.files[0];
//    filePath = URL.createObjectURL(file);
////document.getElementById('fileInput').addEventListener('change', function (event) {
////    var file = event.target.files[0];
////    filePath = URL.createObjectURL(file);
////});
    
//    orderToGemini = `תייצר לי ${$("#numOfQuestions").val()} שאלות אמריקאיות עם 4 תשובות. בפורמט JSON עם השדות הבאים: 
//    'Content'
//    'CorrectAnswer'
//    'WrongAnswer1'
//    'WrongAnswer2'
//    'WrongAnswer3'
//    'Explanation'
//        השאלות יתבססו על הטקסט הבא:`;
//    var addToString = "";
//    readPdfToString(filePath)
//        .then((pdfString) => {
//            addToString = pdfString;
//    })
//        .catch((error) => {
//            console.error('אירעה שגיאה בקריאת ה־PDF:', error);
//        });
//    orderToGemini += addToString;
//    ajaxCall("POST", geminiAPI, JSON.stringify(orderToGemini), GeminiQuestionGetSCB, GeminiQuestionGetECB);

//    return false;
//}

//function  readPdfToString(filePath) {
//    return new Promise((resolve, reject) => {
//        // טעינת הקובץ PDF
//        pdfjsLib.getDocument(filePath).promise.then(function (pdf) {
//            var pdfText = ''; // מחרוזת שתכיל את כל התוכן של ה־PDF

//            // לולאה על כל הדפים בקובץ
//            var promises = [];
//            for (let i = 1; i <= pdf.numPages; i++) {
//                // קריאת התוכן של הדף
//                var promise = pdf.getPage(i).then(function (page) {
//                    return page.getTextContent().then(function (content) {
//                        // המרת התוכן למחרוזת
//                        var pageText = content.items.map(function (item) {
//                            return item.str;
//                        }).join(' ');

//                        // הוספת התוכן של הדף למחרוזת
//                        pdfText += pageText;
//                    });
//                });
//                promises.push(promise);
//            }

//            // אחרי שקריאת כל הדפים נסיים את הפעולה ונחזיר את המחרוזת
//            Promise.all(promises).then(() => {
//                resolve(pdfText);
//            });
//        }).catch(function (error) {
//            // אם יש תקלה בקריאת הקובץ PDF, נזרוק שגיאה
//            reject(error);
//        });
//    });
//}