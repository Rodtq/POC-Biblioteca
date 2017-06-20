$(document).ready(function () {
    $("#editImage").change(function () {
        imageUploader(this, $('#ImgCover'), $('#BookCover'));
    });
});