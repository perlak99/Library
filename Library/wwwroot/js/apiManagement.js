'use strict'
const apiResponse = document.querySelector(".api-response");

const renderResponse = (requestResponse) => {
    //overloading function
    if (requestResponse === undefined) {
        apiResponse.innerHTML = "Something went wrong";
        apiResponse.classList.add("alert-danger");
        apiResponse.classList.remove("d-none");
    } else {
        if (requestResponse.success) {
            apiResponse.classList.add("alert-success")
            apiResponse.classList.remove("alert-danger")
        } else {
            apiResponse.classList.remove("alert-success")
            apiResponse.classList.add("alert-danger")
        }
        apiResponse.classList.remove("d-none");
        apiResponse.innerHTML = requestResponse.message;
    }
}

const renderAuthorizeResponse = () => {
    apiResponse.classList.remove("d-none");
    apiResponse.classList.add("alert-danger");
    apiResponse.innerHTML = "You have to be logged in";
}