﻿// Get the modal
let EditUserModal = document.getElementById("EditUserModal");


// // Get the button that opens the EditUserModal
// var userBtn = document.getElementsByClassName("edit-user-row")[0];

// Get the <userSpan> element that closes the EditUserModal
var userSpan = document.getElementsByClassName("close")[0];

// // When the user clicks the button, open the EditUserModal
// userBtn.onclick = function() {
//     EditUserModal.style.display = "block";
// }

// When the user clicks on <userSpan> (x), close the EditUserModal
userSpan.onclick = function () {
    EditUserModal.style.display = "none";
}

// When the user clicks anywhere outside of the EditUserModal, close it
// window.onclick = function (event) {
//     if (event.target == EditUserModal) {
//         EditUserModal.style.display = "none";
//     }
// }

const passwordInput = document.getElementsByClassName("password");

function togglePassword(element) {
    const box = element.parentElement.firstChild;
    if (box.type === 'password') {
        box.type = 'text';
        element.src = "./../images/icons/eye-closed.svg";
    } else {
        box.type = 'password';
        element.src = "./../images/icons/eye-open.svg";
    }
}
const img = document.querySelector("#password-textbox>img")
