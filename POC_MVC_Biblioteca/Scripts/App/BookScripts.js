function CategoriesNavigation(elementWithInfo, htmlReplacementElement) {
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
};