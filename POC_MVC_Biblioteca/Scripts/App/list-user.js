$(document).ready(function () {
    //GenericPostHandler($("#editUser-fmr"), $("#container-partials"))
    GenericPostHandler($("#QueryUsers-frm"), $("#container-partials"));

    $(document).on('show.bs.modal', '#editUserModal', function (event) {
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
        var updateform = $("#editUserModal").find("#editUser-fmr");
        GenericPostHandler($(updateform), $("#modal-content"));
        updateform.submit();
        alert('Usuário alterado com sucesso');
    });
    $(document).on('hidden.bs.modal', '#editUserModal', function (event) {
        var url = $("#ConsultasUsr").data('navigation-partial');
        $.get(url, function (data) {
            $("#container-partials").html(data);
        });
    });
});