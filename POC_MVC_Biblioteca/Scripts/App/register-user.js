$(document).ready(function () {
    GenericPostHandler($("#userRegister-fmr"), $("#container-partials"));
    GenericPostHandler($("#findUser-fmr"), $("#container-partials"));
    $("#SamAccountName").focusout(function () {
        if ($("#SamAccountName").val() === "") {
            return;
        }
        $("#findUser-fmr").trigger("submit");
    });

    $("#manualImage").change(function () {
        imageUploader(this, $('#ImgCover'), $('#BookCover'));
    });

});