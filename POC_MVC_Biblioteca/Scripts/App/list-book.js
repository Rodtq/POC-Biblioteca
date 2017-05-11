$(document).ready(function () {
    GenericPostHandler($("#QueryBooks-frm"), $("#conteudoDireita"));
    truncateText($(".truncate"), 830);
    //exclude-cmd
    $("button[id^='excludeBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Livro excluido com sucesso!');
            },
            error: function (data) {
                alert('Erro ao excluir Livro!');
            }
        });
    });
});