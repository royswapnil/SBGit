

$(document).ready(function () {

    /// Load all required views 
    $.get('/PartialViews/Admin/LearningAreaTableRow.html', function (template) {
        $('#PartialTemplates').append(template);

        $.get('/api/Course/GetLearningAreas', function (data) {
            var template = document.getElementById('LearningAreaTableRow').innerHTML;
            var output = Mustache.render(template, { array: data.Result });
            $('#LearningAreaTable tbody').html(output);
        });
    });


    $('.addNewLearningArea').click(function () {
        $('#modallearningArea').modal('show');
        $("#LearningAreaForm button").html("Add");
        $('#LearningAreaID').val('0');
        $('#CourseCount').val('0');
        $('#LearningAreaName').val('');
    });


    $("#LearningAreaForm").validate({
        rules: {
            Name: { required: true }
        },
        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.prev().after($error);
        },
        submitHandler: function (form) {
            $('.content1 .messageText').remove();
            var $button = $(form).find(':submit');
            var prev = $button.html();

            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

            var formData = $(form).serialize();

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/AddorUpdateLearningArea",
                data: formData,
                success: function (data) {
                    if (!data.HasError) {
                        data.Result["CourseCount"] = $('#CourseCount').val();
                        var template = document.getElementById('LearningAreaTableRow').innerHTML;
                        var output = Mustache.render(template, { array: [data.Result] });

                        /// check if row exists and update or insert
                        var $row = $('#LearningAreaTable tbody tr[data-id="' + data.Result.LearningArea.Id + '"]');
                        if ($row.length > 0)
                            $row.replaceWith(output);
                        else
                            $('#LearningAreaTable tbody').append(output);

                        $('#LearningAreaID').val('0');
                        $('#CourseCount').val('0');
                        $('#LearningAreaName').val('');
                    }
                    else {
                        var output = LoadPageError(data);
                        $button.after(output);
                    }

                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;

                    var output = LoadPageError({ HasError: true, Message: message});
                    $button.after(output);
                },
                complete: function (data) {
                    $button.removeAttr('disabled');
                    $button.html(prev);
                }
            });

        }
    });


    $('#LearningAreaTable tbody').on('click', 'td.action-buttons button.table-edit', function () {
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();
        var id = $button.closest('tr').attr('data-id');

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }
        $button.attr('disabled', 'disabled');
        $button.find('i').removeClass('fa-edit');
        $button.find('i').addClass('fa-circle-o-notch fa-spin');

        $.ajax({
            cache: false,
            async: true,
            type: "GET",
            url: "/Api/Course/GetLearningArea",
            data: { Id: id },
            success: function (data) {
                if (!data.HasError) {
                    $('#LearningAreaID').val(data.Result.LearningArea.Id);
                    $('#CourseCount').val(data.Result.CourseCount);
                    $('#LearningAreaName').val(data.Result.LearningArea.Name);
                    $('#modallearningArea').modal('show');
                    $("#LearningAreaForm button").html("Update");
                }
                else {
                    var output = LoadPageError(data);
                    $('.boxBody1').prepend(output);
                }
                $button.removeAttr('disabled');
                $button.html(prev);
            },

            error: function (data) {
                var output = LoadPageError({ HasError: true, Message: "An error occurred when processing your request" });
                $('.boxBody1').prepend(output);
                $button.removeAttr('disabled');
                $button.html(prev);
            }
        });

    });

    $('#LearningAreaTable tbody').on('click', 'td.action-buttons button.table-delete', function () {
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();
        var id = $button.closest('tr').attr('data-id');

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }
        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;

            $button.attr('disabled', 'disabled');
            $button.find('i').removeClass('fa-trash');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "Delete",
                url: "/api/Course/DeleteLearningArea?Id=" + id,

                success: function (data) {
                        $button.closest('tr').remove();
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                  
                    $button.removeAttr('disabled');
                    $button.html(prev);
                }
            });
        }
       


    });

});