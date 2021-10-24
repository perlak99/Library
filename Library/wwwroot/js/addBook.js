const urlAddBook = "AddBook";
const addBookForm = document.querySelector(".addbook-form");
$(addBookForm).submit(function(e) {
    e.preventDefault();
    const form = this;
    $.ajax({
        type: 'POST',
        url: urlAddBook,
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