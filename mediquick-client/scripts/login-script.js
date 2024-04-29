let usersAPI = "https://localhost:7253/api/Users"

$("#sign-up-form").submit(suFormSubmit)

function suFormSubmit() {
    newUser = {
        firstName: $("#first-name").val(),
        lastName: $("#last-name").val(),
        email: $("#email").val(),
        password: $("#password").val(),
        phoneNumber: $("#phone-number").val()
    }

    ajaxCall("POST", usersAPI, JSON.stringify(newUser), suPostSCB, suPostECB)
    return false;
}

function suPostSCB(isSuccess) {
    if (isSuccess) {
        alert("Registration succeed!");
    } else {
        alert("Adding failed");
    }
}

function suPostECB(err) {
    alert(err.statusText);
}