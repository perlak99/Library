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
        apiResponse.classList.add("d-none");
        loginSwitch.classList.add("active");
        registerSwitch.classList.remove("active");
        loginForm.classList.remove("d-none");
        registerForm.classList.add("d-none");
        authenticationHeader.innerHTML = "Log in";
    }
    if (e.target == registerSwitch) {
        apiResponse.classList.add("d-none");
        loginSwitch.classList.remove("active");
        registerSwitch.classList.add("active");
        loginForm.classList.add("d-none");
        registerForm.classList.remove("d-none");
        authenticationHeader.innerHTML = "Register";
    }
}

authenticationSwitch.addEventListener("click", (e) => registerloginSwitch(e));

//API Management
$(registerForm).submit(function (e) {
    e.preventDefault();
    const $form = $(this);
    const $url = "/Authentication/Register";

    $.ajax({
        type: 'POST',
        url: $url,
        data: $form.serialize(),
        dataType: 'JSON',
        success: function (response) {
            window.location.replace(response.data);
        },
        error: function (response) {
            if (response.responseJSON)
                renderResponse(response.responseJSON);
            else {
                renderResponse();
            }
        }
    });
});

$(loginForm).submit(function (e) {
    e.preventDefault();
    const $form = $(this);
    const $url = "/Authentication/Login";

    $.ajax({
        type: 'POST',
        url: $url,
        data: $form.serialize(),
        dataType: 'JSON',
        success: function (response) {
            window.location.replace(response.data);
        },
        error: function (response) {
            if (response.responseJSON) {
                renderResponse(response.responseJSON);
            }
            else {
                renderResponse();
            }
        }
    });
});



