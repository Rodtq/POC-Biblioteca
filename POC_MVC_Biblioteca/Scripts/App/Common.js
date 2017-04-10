//--- For partial navigation
function Navigation(elementWithInfo, htmlReplacementElement) {
    $(elementWithInfo).click(function (e) {
        var partialUrl = $(this).data('navigation-partial');
        $.ajax({
            method: "GET",
            url: partialUrl,
            success: function (data) {
                $(htmlReplacementElement).html(data);
            }
        });
    });
};
//--- End

//--- For Post with necessit of loading partial views
function GenericPostHandler(form, htmlReplacementElement) {
    form.on("submit", function (e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: form.attr('action'),
            data: form.serialize(), // serializes the form's elements.
            success: function (data) {
                htmlReplacementElement.html(data);
            }
        });
    });
}

function datepickerInitializer() { // will trigger when the document is ready
    $('.datepicker').datepicker(); //Initialise any date pickers
};


