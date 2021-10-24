const urlEditBook = "EditBook";
const editBookForm = document.querySelector(".editbook-form");
$(editBookForm).submit(function (e) {
    e.preventDefault();
    const form = this;
    $.ajax({
        type: 'PUT',
        url: urlEditBook,
        data: $(form).serialize(),
        dataType: 'JSON',
        success: function (response) {
            renderResponse(response);
        },
        error: function (response) {
            if (response.status === 401) {
                renderAuthorizeResponse();
            } else {
                if (response.responseJSON) {
                    renderResponse(response.responseJSON);
                }
                else {
                    renderResponse();
                }
            }
        }
    });
});