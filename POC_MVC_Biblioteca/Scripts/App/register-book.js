$(document).ready(function () {
    datepickerInitializer();
    GenericPostHandler($("#bookRegister-fmr"), $("#conteudoDireita"));
    
    $("#ISBN").focus();
    $("#ISBN").focusout(function (e) {
        var isbn = $(this).val();
        if (isbn === "") {
            return;
        }
        $.ajax({
            method: "GET",
            url: "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, request) {
                initializeModel(data);
            }, error: function (xhr) {
                alert(xhr.getResponseHeader);
            }
        });
    });
});
function initializeModel(data) {
    if (data.totalItems === 0) {
        alert('Data for ISBN not found');
        return;
    }
    console.log(data);
    $("#Title").val(data.items[0].volumeInfo.title);
    $("#Editor").val(data.items[0].volumeInfo.publisher);
    $("#Description").val(data.items[0].volumeInfo.description);
    var concatAuthors = "";
    var authors = data.items[0].volumeInfo.authors;
    for (var i = 0; i < authors.length; i++) {
        concatAuthors += authors[i];
        if (i != authors.length - 1) {
            concatAuthors += ",";
        }
    }
    $("#Author").val(concatAuthors);
    var date = data.items[0].volumeInfo.publishedDate;
    if (date.length <= 4) {
        date = moment("1/1/" + data.items[0].volumeInfo.publishedDate).format("L");
    }
    $("#BookYear").val(date);

    $("#ImgCover").attr("src","/Content/images/open-book-icon.png");
    if (data.items[0].volumeInfo.imageLinks !== undefined) {
        $("#ImgCover").attr("src", data.items[0].volumeInfo.imageLinks.smallThumbnail);
        $("#BookCover").val(data.items[0].volumeInfo.imageLinks.smallThumbnail);
    }
    
};