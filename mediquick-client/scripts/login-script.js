let usersAPI = "https://localhost:7253/api/Users"

$("#sign-up-form").submit(suFormSubmit)
$("#login-form").submit(loginFormSubmit)

function suFormSubmit() {
    if(!validateForm()) {
        // alert("wrong details");
        return false;
    }
    
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

function loginFormSubmit() {
    email = $("#emailText").val();
    password = $("#passwordText").val();


    let usersLoginAPI = usersAPI + "/" + email;
    try {
        ajaxCall("POST", usersLoginAPI, JSON.stringify(password), loginPostSCB, loginPostECB)
    } catch (error) {
        alert(error)
    }
    
    return false;
}



function validateForm() {
    var email = $("#email").val();
    var firstName = $("#first-name").val();
    var lastName = $("#last-name").val();
    var phoneNumber = $("#phone-number").val();
    var password = $("#password").val();
    var confirmPassword = $("#confirm-password").val();

    // Email validation using regex
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email.match(emailRegex)) {
        alert("Invalid email address");
        return false;
    }

    // First Name and Last Name validation using regex
    var nameRegex = /^[A-Za-z]+$/;
    if (!firstName.match(nameRegex)) {
        alert("Invalid First Name");
        return false;
    }
    if (!lastName.match(nameRegex)) {
        alert("Invalid Last Name");
        return false;
    }

    // Phone Number validation using regex
    var phoneRegex = /^\d{10}$/; // Assuming a 10-digit phone number
    if (!phoneNumber.match(phoneRegex)) {
        alert("Invalid phone number");
        return false;
    }

    // Password validation using regex
    // At least 8 characters, at least one uppercase letter, one lowercase letter, one number, and one special character
    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/;
    if (!password.match(passwordRegex)) {
        alert("Invalid password");
        return false;
    }

    // Confirm Password validation
    if (password !== confirmPassword) {
        alert("Passwords do not match");
        return false;
    }

    return true;
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

function loginPostSCB(isSuccess) {
    if (isSuccess) {
        alert("login succeed!");
        window.location.href = 'https://www.youtube.com/watch?v=sd4QiDvKklM&list=RDwHdiCwh2FZM&index=14';
    } else {
        alert("Adding failed");
    }
}

function loginPostECB(err) {
    if (err.responseText.includes("User Not Found")) {
        alert ("Wrong email or password, please try again");
        $("#emailText").focus();
    }
}

function resetForm() {
    $("#sign-up-form")[0].reset();
}