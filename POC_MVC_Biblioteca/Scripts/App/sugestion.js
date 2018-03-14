$(document).ready(function () {

    $("#sugestionAction").on("click", function () {
        var updateform = $("#contatoRegister-fmr");
        GenericPostHandler($(updateform), "htmlReplacement");
        updateform.submit();
        alert('Cadastro enviado com sucesso');
    });

});