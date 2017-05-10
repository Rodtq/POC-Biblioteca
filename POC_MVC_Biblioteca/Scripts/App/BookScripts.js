$(document).ready(function () {
    Navigation($("a[data-navigation-partial]"), $("#conteudoDireita"));
    var elementWithInfo = $("a[data-navigation-categories]");
    var htmlReplacementElement = $("#conteudoDireita");
    $(elementWithInfo).click(function (e) {
        var partialUrl = $(this).data('navigation-categories');
        $.ajax({
            method: "GET",
            url: partialUrl,
            success: function (data, textStatus, request) {
                $(htmlReplacementElement).html(data);
            }, error: function (xhr) {
                alert("Error");
            }
        });
    });
});