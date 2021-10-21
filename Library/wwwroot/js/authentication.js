"use strict"



const loginSwitch = document.querySelector(".login-switch");
const registerSwitch = document.querySelector(".register-switch");
const loginForm = document.querySelector(".login-form");
const registerForm = document.querySelector(".register-form");
const authenticationSwitch = document.querySelector(".authentication-switch");
const authenticationHeader = document.querySelector(".authentication-header");

//SWITCHING LOG IN/REGISTER VIEWS
const registerloginSwitch = (e) => {
    if (e.target == loginSwitch) {
        authenticationResponse.classList.add("d-none");
        loginSwitch.classList.add("active");
        registerSwitch.classList.remove("active");
        loginForm.classList.remove("d-none");
        registerForm.classList.add("d-none");
        authenticationHeader.innerHTML = "Log in";
    }
    if (e.target == registerSwitch) {
        authenticationResponse.classList.add("d-none");
        loginSwitch.classList.remove("active");
        registerSwitch.classList.add("active");
        loginForm.classList.add("d-none");
        registerForm.classList.remove("d-none");
        authenticationHeader.innerHTML = "Register";
    }
}

authenticationSwitch.addEventListener("click", (e) => registerloginSwitch(e));

//API Management
const authenticationResponse = document.querySelector(".authentication-response");

const renderResponse = (requestResponse) => {
    if (requestResponse.success) {
        authenticationResponse.classList.add("alert-success")
        authenticationResponse.classList.remove("alert-danger")
    } else {
        authenticationResponse.classList.remove("alert-success")
        authenticationResponse.classList.add("alert-danger")
    }
    authenticationResponse.classList.remove("d-none");
    authenticationResponse.innerHTML = requestResponse.message;
}

$(registerForm).submit(function (e) {
    e.preventDefault();
    const $form = $(this);
    const $url = "/Authentication/Register";

    //if (!$form.valid()) return;

    $.ajax({
        type: 'POST',
        url: $url,
        data: $form.serialize(),
        dataType: 'JSON',
        success: function (response) {
            renderResponse(response);
            window.location.replace(response.data);
        },
        error: function (response) {
            if (response.responseJSON)
                renderResponse(response.responseJSON);
            else {
                authenticationResponse.innerHTML = "Something went wrong";
                authenticationResponse.classList.add("alert-danger");
                authenticationResponse.classList.remove("d-none");
            }
        }
    });
});

$(loginForm).submit(function (e) {
    e.preventDefault();
    const $form = $(this);
    const $url = "/Authentication/Login";

    //if (!$form.valid()) return;

    $.ajax({
        type: 'POST',
        url: $url,
        data: $form.serialize(),
        dataType: 'JSON',
        success: function (response) {
            renderResponse(response);
            window.location.replace(response.data);
        },
        error: function (response) {
            if (response.responseJSON) {
                renderResponse(response.responseJSON);
            }
            else {
                authenticationResponse.innerHTML = "Something went wrong";
                authenticationResponse.classList.add("alert-danger");
                authenticationResponse.classList.remove("d-none");
            }
        }
    });
});



