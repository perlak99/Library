"use strict"

const hamburger = document.querySelector(".hamburger");
const menu = document.querySelector(".menu");

hamburger.addEventListener("click", function () {
    this.classList.toggle("is-active");
    menu.classList.toggle("is-active");
});

const logoutButton = document.querySelector(".btn-logout");
if (logoutButton != null) {
    logoutButton.addEventListener("click", function () {
        const $urlLogout = "/Authentication/Logout";
        $.ajax({
            type: 'GET',
            url: $urlLogout,
            success: function (response) {
                window.location(window.location.replace("/Home/Index"));
            },
            error: function (response) {
                console.log("error");
            }
        });
    })
}
