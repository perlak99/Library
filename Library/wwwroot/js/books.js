"use strict"

const booksDiv = document.querySelector('.books');

const renderBooks = data => {
    data.forEach(b => {
        let reservation = !b.reservation ? "" : "disabled";
        booksDiv.innerHTML += `
            <div class="col-12 col-lg-4 book p-2">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${b.title}</h5>
                        <h6 class="card-subtitle mb-2 text-muted">${b.author}</h6>
                        <h6 class="card-subtitle mb-2 text-muted">${b.releaseDate.substring(0, b.releaseDate.indexOf('T'))}</h6>
                        <p class="card-text">${b.description}</p>
                        <input type="hidden" class="input-id" value="${b.id}" />
                        <button type="button" class="btn btn-primary btn-reservation ${reservation}">Reserve</button>
                        <a href="/Books/EditBook/${b.id}" type="button" class="btn btn-primary">Edit</a>
                        <p class="api-response"></p>
                    </div>
                </div>
            </div>
        `;
    })
}


booksDiv.addEventListener("click", function (e) {
    if (e.target.classList.contains("btn-reservation")) {
        const thisButton = e.target;
        const id = thisButton.closest(".card-body").querySelector(".input-id").value;
        const $urlReserveBook = "Books/ReserveBook/";
        const data = { bookId: id };
        const apiResponse = thisButton.closest(".card-body").querySelector(".api-response");
        $.ajax({
            type: 'POST',
            url: $urlReserveBook,
            contentType: 'application/x-www-form-urlencoded',
            data: data,
            success: function (response) {
                thisButton.classList.add("disabled")
                apiResponse.classList.add("alert-success");
                apiResponse.innerHTML = response.message;
            },
            error: function (response) {
                apiResponse.classList.add("alert-danger");
                if (response.status = 401)
                    apiResponse.innerHTML = "You have to be logged in to reserve the book";
                else
                    apiResponse.innerHTML = response.responseJSON.message;
            }
        });
    }
})



const $urlGetAllBooks = "Books/GetAllBooks";
$.ajax({
    type: 'GET',
    url: $urlGetAllBooks,
    success: function (response) {
        renderBooks(response.data)
    },
    error: function (response) {
        console.log("error");
    }
});