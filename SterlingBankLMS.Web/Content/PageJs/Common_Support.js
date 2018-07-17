$('.hider').click(function () {
    $('.hiderBody').hide();
    $('.hiderHead').css("background", "#eee");
    $('.hiderHead').css("color", "#444");
    $('.hiderHead i').css("color", "#0DA9DC").html("keyboard_arrow_down");
    $('.hiderHead i', this).css("color", "#fff").html("keyboard_arrow_up");
    $('.hiderHead', this).css("background", "#0DA9DC");
    $('.hiderHead', this).css("color", "#fff");
    $('.hiderBody', this).fadeIn();
});

$("#FormSupport").validate({
    rules: {
        TicketTitle: { required: true },
        TicketDescription: { required: true, maxlength: 750 },
        ImageUpload: {
            accept: "image/*"
        },
       
    },
    message: {
        ImageUpload: { accept: "only image file types are allowed" }
    },
    errorPlacement: function ($error, $element) {
        $error.addClass('errorHolder');
        $element.before($error);
    },
    submitHandler: function (form) {

        var $button = $(form).find(':submit');
        var prev = $button.html();
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

        var formData = new FormData(form);
        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/support/CreateTicket",
            contentType: false,
            processData: false,
            data: formData,
            success: function (data) {
                $(form)[0].reset();
                ShowNotie({ HasError: false, Message: "Your ticket was successfully created" });

            },

            error: function (data) {
                ShowNotie({ HasError: true, Message: "An error occurred when processing your request" });
            },
            complete: function (data) {
                $button.removeAttr('disabled');
                $button.html(prev);
            }
        });

    }
});