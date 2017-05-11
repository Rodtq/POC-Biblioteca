//--- For partial navigation
function Navigation(elementWithInfo, htmlReplacementElement) {
    $(elementWithInfo).click(function (e) {
        var partialUrl = $(this).data('navigation-partial');
        $.ajax({
            cache: false,
            method: "GET",
            url: partialUrl,
            success: function (data, textStatus, request) {
                //var req = JSON.parse(request.getResponseHeader('X-Responded-JSON'));
                //if (req !== null && req.status == 401) {
                //    alert("Not Authorized");
                //    return;
                //}
                $(htmlReplacementElement).html(data);
            }, error: function (xhr) {
                alert("Error");
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
            cache: false,
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
    $('.datepicker').datepicker({ format: 'dd/mm/yyyy' }); //Initialise any date pickers
};

function truncateText(elementList, maxLenth) {
    for (var i = 0; i < elementList.length; i++) {
        var element = $(elementList[i]);
        var text = element.text();
        if (text.length > maxLenth) {
            var subText = text.substring(0, maxLenth) + '...';
            element.text(subText);
        }
    }
};