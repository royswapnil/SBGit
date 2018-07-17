

$(document).ready(function () {

    BindHtmlEditor($('textarea'));

    ///GET MUSTACHE TEMPLATES FOR BINDING

    //$.get('/PartialViews/Admin/ExamQuestions.html', function (template) {
    //    $('#PartialTemplates').append(template);
    //    var template = document.getElementById('ExamQuestions').innerHTML;
    //    var output = Mustache.render(template, { array: [] });
    //    $('#QuestionHolder').append(output);
    //    $('#QuestionHolder textarea').froalaEditor({ theme: "royal", heightMin: 150 });
    //});

    $.get('/api/Survey/GetTemplates', function (data) {
        for (var i = 0; i < data.Result.length; i++) {
            $('#SurveyTemplate').append('<option value=' + data.Result[i].Id + '>' + data.Result[i].Name + '</option>');
        }
    });
   
    

  

    var jqTable = $('#SurveyTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Survey/GetSurveyDatatable',
            "type": "GET"
        },
        'language': {
            'search': '',
            'searchPlaceholder': 'Search...'
        },
        "fnDrawCallback": function (oSettings) {
            // $('.tr_checkbox').uniform();
        },
        "order": [],
        columns: [
            {
                bSortable: true,
                "render": function (d, i, data) {

                    return data.Name;
                }
            },
            { data: "Template" },
            { data: "SurveyTypeName" },
            { data: "UsersTaken" },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {
                    if (data.UsersTaken > 0) {
                        return '<button class="table-edit btnSmaller btnBlue" href="javascript:;"> <i class="fa fa-edit table-icon" style=""></i> Edit	</button>' +
                            '<button class="table-delete btnSmaller btnRed" href= "javascript:;" > <i class="fa fa-trash table-icon" style=""></i> <span class="deleteLA">Delete</span></button>' +
                            '<input type="hidden" value="' + data.Id + '"/>';
                    }
                    else {
                        return '';
                    }

                }
            }
        ]
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });

    /// Delete Survey

    $("#SurveyTable tbody").on("click", "td.action-buttons button.table-delete", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }
        if (confirm("Are you sure you want to do this?")) {
            $button.attr('disabled', 'disabled');
            $button.find('i').removeClass('fa-trash');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "Delete",
                url: "/api/Survey/DeleteSurvey?Id=" + id,

                success: function (data) {
                    jqTable.row(tr).remove().draw(false);
                    ShowNotie(data);
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


    /// Edit Survey
    $("#SurveyTable tbody").on("click", "td.action-buttons button.table-edit", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $button.attr('disabled', 'disabled');
        $button.find('i').removeClass('fa-trash');
        $button.find('i').addClass('fa-circle-o-notch fa-spin');

        $.ajax({
            cache: false,
            async: true,
            type: "Get",
            url: "/api/Survey/GetSurvey",
            data: { Id: id },
            success: function (data) {

                // Bind Survey Form
                BindSurveyForm(data);
                $button.removeAttr('disabled');
                $button.html(prev);
                $('#listPanel').hide();
                $('.viewManagePanel').hide();
                $('#managePanel').fadeIn();
                $('.viewListPanel').show();

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
                $button.removeAttr('disabled');
                $button.html(prev);
            }
        });


    });


    /// TOGGLE SELECT TYPE 

    $('#SurveyType').on('change', function (e, selectedId) {
        var type = parseInt($(this).val());
        var url = '';
        var dummyClass = '';
        var firstOption = 'Select a Survey Type';

        switch (type) {
            case 1: url = '/api/Course/GetCourses'; dummyClass = 'dummyCourse'; firstOption = 'Select a Course'; break;
            case 2: url = '/api/Exam/GetExams'; dummyClass = 'dummyExam'; firstOption = 'Select an Exam'; break;
            case 3: url = '/api/ManageTraining/GetTraining'; dummyClass = 'dummyTraining'; firstOption = 'Select a Training'; break;

        }


        $('#SurveyAsso option[value="0"]').html(firstOption);
        $('#SurveyAsso').closest('.form-group').find('label:first').html(firstOption + ':');
        $('#SurveyAsso').attr('disabled', 'disabled');

        if (dummyClass == '') {
            $('#SurveyAsso option:not([value="0"])').remove();
            $('#SurveyAsso').select2();
            return;
        }

        if ($('.' + dummyClass).find('option').length >= 1) {
            $('#SurveyAsso option:not([value="0"])').remove();
            $('#SurveyAsso').append($('.' + dummyClass).html());
            if (selectedId != undefined) {
                $('#SurveyAsso option[value="' + selectedId + '"]').prop('selected', true);
            }
            $('#SurveyAsso').removeAttr('disabled');
            $('#SurveyAsso').select2({"width":"100%"});
          

        }
        else {
            if (url != '') {
                $('#SurveyAsso').closest('.form-group').find('label:first').html('Loading Items....');
                $.get(url, function (data) {
                    for (var i = 0; i < data.Result.length; i++) {
                        $('.' + dummyClass).append('<option value=' + data.Result[i].Id + '>' + data.Result[i].Name + '</option>');
                    }
                    $('#SurveyAsso option:not([value="0"])').remove();
                    $('#SurveyAsso').append($('.' + dummyClass).html());
                    if (selectedId != undefined) {
                        $('#SurveyAsso').val(selectedId).trigger('change', selectedId);
                       // $('#SurveyAsso option[value="' + selectedId + '"]').prop('selected', true);
                    }
                    $('#SurveyAsso').removeAttr('disabled');
                    $('#SurveyAsso').select2({ "width": "100%" });
                    $('#SurveyAsso').closest('.form-group').find('label:first').html(firstOption + ':');
                });
            }
        }



    });

  


    $('#SurveyAsso').on('change', function (e, selectedId) {

        $('.lms-loader').show();

        if ($('#SurveyID').val() != "0")
            clearForm();

        var id = (selectedId == undefined) ? $(this).val() : selectedId;
        var param = { Id: parseInt(id) };

        var url = '';
        switch ($('#SurveyType').val()) {
            case '1': url = '/api/Survey/GetCourseSurvey'; break;
            case '2': url = '/api/Survey/GetExamSurvey';  break;
            case '3': url = '/api/Survey/GetTrainingSurvey';  break;
        }

        if (url != '') {
            $.get(url, param, function (data) {
               
                if (data.Result != null) {
                    clearForm();
                    BindSurveyForm(data);
                }

              
            });
        }
        $('#SurveyAsso option[value="' + id + '"]').prop('selected', true);
        $('#SurveyAsso').select2();
        $('.lms-loader').hide();
    });


    /// validate Survey Creation and insert or update
    $("#SurveyForm").validate({
        ignore: [],
        rules: {
            Name: { required: true },
            TemplateId: "dropdown",
            SurveyType: "dropdown",
            SurveyAssoID: "dropdown"
        },
        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.closest('.form-group').find('label:first').after($error);
        },
        submitHandler: function (form) {

            var $button = $('#SaveSurvey');
            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

            var formData = new FormData(form);

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Survey/AddorUpdate",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    $('#SurveyID').val(data.Result.Id);
                    jqTable.ajax.reload();
                    ShowNotie(data);
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                },
                complete: function (data) {
                    $button.removeAttr('disabled');
                    $button.html(prev);
                }
            });

        }
    });

    //// If navigating from course page
    if ($('#SurveyAsso')[0].hasAttribute("data-selected")) {
        var selectedId = $('#SurveyAsso').attr("data-selected");
        var type = $('#SurveyAsso').attr("data-type");
        $('#SurveyType').val(type).trigger('change', parseInt(selectedId));
    }
});

function clearForm() {
    $('#SurveyName').val('');
    BindHtmlEditor($('#SurveyDescription'),'');
    $('#SurveyID').val('0');
    $('#SurveyTemplate option:first').prop('selected', true);
   // $('#SurveyType option:first').prop('selected', true);
   // $('#SurveyType').val(0).trigger('change', 0);
}

function BindSurveyForm(data) {
 
    $('#SurveyName').val(data.Result.Name);
    BindHtmlEditor($('#SurveyDescription'), data.Result.Description);
    $('#SurveyID').val(data.Result.Id);
    $('#SurveyTemplate option[value="' + data.Result.TemplateId + '"]').prop('selected', true);
    $('#SurveyType').val(data.Result.SurveyType).trigger('change', data.Result.SurveyAssoID);

}
