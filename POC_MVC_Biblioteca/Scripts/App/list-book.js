$(document).ready(function () {
    GenericPostHandler($("#QueryBooks-frm"), $("#conteudoDireita"));
    truncateText($(".truncate"), 430);

    $(document).on('show.bs.modal', '#deleteBookModal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var cmdUrl = button.data('cmd'); // Extract info from data-* attributes
        $('#deleteAction').on('click', function () {
            console.log(cmdUrl);
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this);
            var content = $('#modal-content');
            content.html("");
            $.ajax({
                cache: false,
                type: "GET",
                url: cmdUrl,
                success: function (data) {

                },
                error: function (data) {
                    alert('Erro ao excluir livro');
                }
            });
        });
    });

    //locate-cmd
    $("button[id^='locateBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Livro locado com sucesso! \n Atenção! \n Você tem 48 horas para retirar o livro. \n Colaboradores ADM, Turnos A e B: Retirar Amanhã no RH. \n Colaboradores Turno C: Retirar amanhã com Paulo Nascimento.');
            },
            error: function (data) {
                alert('Erro ao locar o Livro!');
            }
        });
    });

    //reserve-cmd
    $("button[id^='reserveBook-']").on("click", function () {
        var btn = $(this);
        var cmdUrl = btn.data("cmd");
        console.log(cmdUrl);
        var htmlReplacementElement = $("#conteudoDireita");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                htmlReplacementElement.html(data);
                alert('Livro reservado com sucesso! \n Atenção!!! \n Você será notificado quando o livro \n estiver disponível para locação!');
            },
            error: function (data) {
                alert(' Erro ao reservar o livro!');
            }
        });
    });

    //detail-cmd
    $(document).on('show.bs.modal', '#detailBookModal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var cmdUrl = button.data('cmd'); // Extract info from data-* attributes
        console.log(cmdUrl);
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        var content = $('#modal-content-detail');
        content.html("");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                content.html(data);
            },
            error: function (data) {
                alert('Erro');
            }
        });
    });

    $(document).on('show.bs.modal', '#editBookModal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var cmdUrl = button.data('cmd'); // Extract info from data-* attributes
        console.log(cmdUrl);
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        var content = $('#modal-content');
        content.html("");
        $.ajax({
            cache: false,
            type: "GET",
            url: cmdUrl,
            success: function (data) {
                content.html(data);
            },
            error: function (data) {
                alert('Erro');
            }
        });
    });


    $("#updateAction").on("click", function () {
        var updateform = $("#editBookModal").find("#bookEdit-fmr");
        GenericPostHandler($(updateform), $("#modal-content"));
        updateform.submit();
        alert('livro alterado com sucesso');
    });
    $(document).on('hidden.bs.modal', '#editBookModal', function (event) {
        var url = $("#Consultas").data('navigation-partial');
        $.get(url, function (data) {
            $("#conteudoDireita").html(data);
        });
    });
    $(document).on('hidden.bs.modal', '#deleteBookModal', function (event) {
        var url = $("#Consultas").data('navigation-partial');
        $.get(url, function (data) {
            $("#conteudoDireita").html(data);
        });
    });

});