"use strict"

const booksDiv = document.querySelector('.books');

const renderBooks = data => {
    data.forEach(b => {
        console.log(b);
        let reservation = !b.reservation ? "" : "disabled";
        booksDiv.innerHTML += `
            <div class="col-12 col-lg-4 book p-2">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${b.title}</h5>
                        <h6 class="card-subtitle mb-2 text-muted">${b.author}</h6>
                        <h6 class="card-subtitle mb-2 text-muted">${b.releaseDate.substring(0, b.releaseDate.indexOf('T'))}</h6>
                        <p class="card-text">${b.description}</p>
                        <button type="button" class="btn btn-primary ${reservation}">Reserve</button>
                    </div>
                </div>
            </div>
        `;
    })
}


const $url = "Books/GetAllBooks"
$.ajax({
    type: 'GET',
    url: $url,
    success: function (response) {
        renderBooks(response.data)
    },
    error: function (response) {
        console.log("error");
    }
});