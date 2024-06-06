﻿// Get the modal
var modal = document.getElementById("EditUserModal");

// Get the button that opens the modal
var btn = document.getElementsByClassName("edit-user-row")[0];

// Get the <span> element that closes the modal
var span1 = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal
btn.onclick = function() {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span1.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

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
