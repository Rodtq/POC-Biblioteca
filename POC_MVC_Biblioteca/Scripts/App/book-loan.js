$(document).ready(function () {
    //locate-cmd
    $("input[id^='deliverBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Livro retirado com sucesso!');
            },
            error: function (data) {
                alert('Erro ao retirar Livro!');
            }
        });
    });
    //cancel-cmd
    $("input[id^='cancelBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Empréstimo cancelado com sucesso!');
            },
            error: function (data) {
                alert('Erro ao cancelar Livro!');
            }
        });
    });
    //return-cmd
    $("input[id^='returnBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Livro devolvido com sucesso!');
            },
            error: function (data) {
                alert('Erro ao devolver Livro!');
            }
        });
    });
});