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
                console.log("aqui bateu :" + data);
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


function imageUploader(input, preview, hiddenInput) {
    if (input.files && input.files[0]) {
        var parts = $(input).val().split('.');
        var extension = parts[parts.length - 1];
        if (extension !== "png") {
            alert('Por favor escolha uma imagem do tipo .png');
            $(input).val("");
            $(preview).attr('src', "");
            return;
        }
        if (input.files[0].size / (1024 * 1024) > 0.1) {
            alert('Por favor escolha uma imagem de tamanho menor que 100 KBytes');
            $(input).val("");
            $(preview).attr('src', "");
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = e.target.result;
            var imgbytes = img.replace("data:image/png;base64,", "");
            $(preview).attr('src', img);
            $(hiddenInput).val(imgbytes);
        }
        reader.readAsDataURL(input.files[0]);
    }
}