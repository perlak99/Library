﻿const reservationsDiv = document.querySelector(".reservations");
const renderReservations = data => {
    data.forEach(b => {
        reservationsDiv.innerHTML += `
            <div class="col-12 col-lg-4 book p-2">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${b.title}</h5>
                        <h6 class="card-subtitle mb-2 text-muted">${b.author}</h6>
                        <h6 class="card-subtitle mb-2 text-muted">${b.releaseDate.substring(0, b.releaseDate.indexOf('T'))}</h6>
                        <p class="card-text">${b.description}</p>
                        <h6 class="card-subtitle mb-2 text-muted">Reservation date: ${b.reservationDate.substring(0, b.releaseDate.indexOf('T'))}</h6>
                        <input type="hidden" class="input-id" value="${b.reservationId}" />
                        <button type="button" class="btn btn-primary btn-reservation">Remove</button>
                        <p class="api-response"></p>
                    </div>
                </div>
            </div>
        `;
    })
}

const $urlGetReservations = "GetReservations";
$.ajax({
    type: 'GET',
    url: $urlGetReservations,
    success: function (response) {
        renderReservations(response.data)
    },
    error: function (response) {
        if (response.status = 401)
            renderAuthorizeResponse()
        else
            renderResponse(response.responseJSON);
    }
});