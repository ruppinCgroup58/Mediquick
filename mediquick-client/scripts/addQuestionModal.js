// Get the modal
let GeminiModal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("addQuestionByGemini");

// Get the <span> element that closes the modal
var spanGeminiModal = document.getElementsByClassName("close")[2];

// When the user clicks the button, open the modal
btn.onclick = function () {
    GeminiModal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
spanGeminiModal.onclick = function () {
    GeminiModal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == GeminiModal) {
        GeminiModal.style.display = "none";
    }
}
function resetGeminiForm() {
    $("#addQuestionForm")[0].reset();
}

