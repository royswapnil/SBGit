$('.lms-loader').show();

$(document).ready(function () {

    ///GET MUSTACHE TEMPLATES FOR BINDING

    $.get('/PartialViews/Admin/ExamTableRow.html', function (template) {
        $('#PartialTemplates').append(template);
    });

    $.get('/PartialViews/Admin/ExamQuestions.html', function (template) {
        $('#PartialTemplates').append(template);
    });

    BindHtmlEditor($('textarea'));

    $('.datepicker').datepicker({ format: "dd/mm/yyyy" });
    $('.progress1 .tabloid').css('width', String(Math.floor(100 / $('.progress1 .tabloid').length)) + "%");

    $.get('/api/Course/GetCourses', function (data) {
        for (var i = 0; i < data.Result.length; i++) {
            $('select[name="CourseId"]').append('<option value=' + data.Result[i].Id + '>' + data.Result[i].Name + '</option>');
        }
        if ($('select[name="CourseId"]')[0].hasAttribute("data-selected")) {
            var selectedId = $('select[name="CourseId"]').attr("data-selected");
            $('select[name="CourseId"] option[value="' + selectedId + '"]').prop("selected", true);
            //$('input[name="ExamRetakeCount"]').val($('select[name="CourseId"] option[value="' + selectedId + '"]').attr('data-examretake'));

            /// get course exam if exists 
            var course = { CourseId: parseInt(selectedId) };
            $.get("/api/Exam/GetCourseExam", course, function (data) {
                if (data.Result != null) {
                    BindExamForm(data);
                }
               
                $('.lms-loader').hide();
            });

        }
        else {
            $('.lms-loader').hide();
        }
        $('select[name="CourseId"]').select2({ "width": "100%" });
    });



    var jqTable = $('#ExamTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Exam/GetDatatableExams',
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
            { data: "Course" },
            { data: "PassGrade" },
            { data: "Duration" },
            { data: "ExamRetakeCount" },
            { data: "Period" },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {

                    return '<button class="table-edit btnSmaller btnBlue" href="javascript:;"> <i class="fa fa-edit table-icon" style=""></i> Edit	</button>' +
                        ' <a class="table-add btnSmaller btnGreen" href="/Admin/Survey/Exam/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Add Survey</a > ' +
                        '<button class="table-delete btnSmaller btnRed" href= "javascript:;" > <i class="fa fa-trash table-icon" style=""></i> <span class="deleteLA">Delete</span></button>' +
                        ' <a class="table-clone btnSmaller btnBlue" href= "javascript:;"><i class="fa fa-copy table-icon" style=""></i>  Clone Course</a > ' +
                        '<input type="hidden" value="' + data.Id + '"/>';
                }
            }
        ]
    });



    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });


    $('select[name="CourseId"]').on('change', function () {
        
        $('.lms-loader').show();
        var selectedId = $(this).val();
        var course = { CourseId: parseInt(selectedId) };
        $.get("/api/Exam/GetCourseExam", course, function (data) {

            if ($('#ExamID').val() != "0")
                clearForm();

            if (data.Result != null) {
                clearForm()
                BindExamForm(data);
            }
            $('select[name="CourseId"] option[value="' + selectedId + '"]').prop('selected', true);
            $('select[name="CourseId"]').select2();

            $('.lms-loader').hide();
        });
    });

    /// Delete Exam

    $("#ExamTable tbody").on("click", "td.action-buttons button.table-delete", function () {
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
                url: "/api/Exam/DeleteExam?Id=" + id,

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


    /// Edit EXAM
    $("#ExamTable tbody").on("click", "td.action-buttons button.table-edit", function () {
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
        $('#CourseIgnoreDueDate').prop('checked', false);
        $('#CourseDate').val('');

        $.ajax({
            cache: false,
            async: true,
            type: "Get",
            url: "/api/Exam/GetExam",
            data: { Id: id },
            success: function (data) {

                // Bind Exam Form

                BindExamForm(data);

                $('.formArea').hide();
                $('.formArea1').show();



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

    // CLone EXAM

    $("#ExamTable tbody").on("click", "td.action-buttons a.table-clone", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        var name = $(this).closest("tr").find('td:first-child').html();

        $('#CloneID').val(id);
        $('#modalClone input[name="Name"]').html('');
        $('#ClonedExamName').html(name);
        $('#modalClone').modal('show');
        $('#modalClone .messageText').remove();

    });


    /// Action when stand alone exam is selected
    $('#chkIsStandAlone').on('change', function () {
        if ($(this).is(':checked')) {
            $('select[name="CourseId"]').attr('disabled', 'disabled');
           // $('#chkIgnoreExamReTakeCount').removeAttr('disabled');
        }
        else {
            $('select[name="CourseId"]').removeAttr('disabled');
           // $('input[name="ExamRetakeCount"]').attr('disabled', 'disabled');
        }

    });

    /// Enable or disable exam avaialable dates
    $('#IngoreDueDate').on('change', function () {
        if ($(this).is(':checked')) {
            $('input[name="StartDateFormat"]').attr('disabled', 'disabled');
            $('input[name="EndDateFormat"]').attr('disabled', 'disabled');
        }
        else {
            $('input[name="StartDateFormat"]').removeAttr('disabled');
            $('input[name="EndDateFormat"]').removeAttr('disabled');
        }
    });

    /// Enable or disable exam retake for stand alone
    $('#chkIgnoreExamReTakeCount').on('change', function () {
        $(this).is(':checked') ? $('input[name="ExamRetakeCount"]').attr('disabled', 'disabled') : $('input[name="ExamRetakeCount"]').removeAttr('disabled');
    });

    /// Enable or disable exam time limit
    $('#IgnoreTimeLimit').on('change', function () {

        $(this).is(':checked') ? $('input[name="HourLimit"],input[name="MinuteLimit"]').attr('disabled', 'disabled') : $('input[name="HourLimit"],input[name="MinuteLimit"]').removeAttr('disabled');

    });

    /// Display course exam retake count when selected

    //$('select[name="CourseId"]').on('change', function () {
    //    var val = $(this).val();
    //    if ($('select[name="CourseId"] option[value="' + val + '"]')[0].hasAttribute('data-examretake')) {
    //        $('input[name="ExamRetakeCount"]').val(($('select[name="CourseId"] option[value="' + val + '"]').attr('data-examretake')));
    //    }
    //    else {
    //        $('input[name="ExamRetakeCount"]').val("");
    //    }
    //});


    //// create new exam
    $('.createNewExam').on('click', function () {
        clearForm();
    });



    /// validate exam creation step 1 and proceed to create or update
    $("#FormStep1").validate({
        ignore: [],
        rules: {
            Name: { required: true },
            Description: { required: true, maxlength: 750 },
            CourseId: "dropdown",
            ExamRetakeCount: { required: true, digits: true },
            StartDateFormat: {
                required: true,
                DateGreaterOrEqual: 'currentDate'
            },
            EndDateFormat: {
                required: true,
                DateGreater: '[name="StartDateFormat"]'
            },


            HourLimit: { required: true, digits: true },
            MinuteLimit: { required: true, digits: true },
            PassGrade: { required: true, digits: true }
        },
        message: {
            StartDateFormat: { DateGreaterOrEqual: "Date must be greater than or equal to current day" }
        },

        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.closest('.form-group').find('label:first').after($error);
        },
        submitHandler: function (form) {

            var $button = $(form).find(':submit.nextBtn');
            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
            var formData = new FormData(form);
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Exam/AddorUpdate",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (!data.HasError) {
                        $('#ExamID').val(data.Result.Id);
                        $('#ExamTitleSpan').html(data.Result.Name);

                        var template = document.getElementById('ExamQuestions').innerHTML;
                        var output = Mustache.render(template, { array: data.Result.ExamQuestions });
                        $('#ExamQuestionHolder').html(output);
                        refreshSortableQuestions();
                        $('#ExamQuestionHolder').find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) { $(item).find('a').html(i + 1); });
                        $('#ExamQuestionHolder').find(".qnumbering .btn-group:first").addClass('active');
                        $('#ExamQuestionHolder').find(".question:first").show();

                        $('#ExamQuestionHolder').find('textarea.questionText').each(function (i, item) {
                            var html = $(item).val();
                            BindHtmlEditor($(item), html);
                        });

                        data.Result.ExamQuestions.forEach(function (item) {
                           
                            $('#ExamQuestionHolder').find('.question[data-id ="' + item.Id + '"] select.AnswerType option[value="' + GetQuestionAnswerType(item.AnswerType) + '"]').prop('selected', true);
                            
                            if (item.AnswerType == "checkbox") {
                                $('#ExamQuestionHolder').find('.question[data-id ="' + item.Id + '"]').find('.AnswerSelection').show();
                            }


                            var AnswerLogic = item.IsMultipleChoice ? 1 : 0;
                            $('#ExamQuestionHolder').find('.question[data-id ="' + item.Id + '"] select.AnswerSelection  option[value="' + AnswerLogic + '"]').prop('selected', true);
                        });

                        jqTable.ajax.reload();

                        $('.formArea1').fadeOut();
                        setTimeout(function () {
                            $('.progress1 .tabloid:nth-child(2)').addClass('active');
                            $('.progressIndicator1').css("width", "50%");
                            $('.formArea2').fadeIn();

                        }, 500);



                    }
                    else {

                        ShowNotie(data);
                    }

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


    /// Duplicate an exam

    $("#FormCloneExam").validate({
        ignore: [],
        rules: {
            Name: { required: true }
        },
        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.closest('.form-group').find('label:first').after($error);
        },
        submitHandler: function (form) {
            $('#modalClone .messageText').remove();
            var $button = $(form).find(':submit');
            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
            var formData = new FormData(form);
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Exam/CloneExamination",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    $button.html(prev);
                    $button.removeAttr('disabled');
                    $(form)[0].reset();
                    jqTable.ajax.reload();
                    $('#modalClone').modal('hide');
                    ShowNotie(data);
                },

                error: function (data) {
                    $button.html(prev);
                    $button.removeAttr('disabled');
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    var output = LoadPageError({ HasError: true, Message: message });
                    $button.after(output);
                }
            });

        }
    });

    /// ADD NEW QUIZ QUESTION

    $('#ExamQuestionHolder').on('click', '.addquestion', function () {
        var $cloned = $(this).parent().prev().clone();
        var id = $cloned.attr('data-id');
        var sort = $cloned.attr('data-sort');
        var index = $(this).closest('.qnumbering').find('.btn-group:not(.excludeSort)').length;
        $(this).closest('.qnumbering').children().removeClass('active');
        $cloned.attr('data-id', '0');
        $cloned.attr('data-sort', parseInt(sort) + 1);
        $cloned.addClass('active');
        $cloned.find('a').html(index + 1);
        $(this).parent().before($cloned);
        refreshSortableQuestions();

        var $clonedQuestion = $(this).closest('#ExamQuestionHolder').find('.question:first').clone();
        $clonedQuestion.find('label.errorHolder').remove();
        $(this).closest('#ExamQuestionHolder').find('.question').hide();
        $clonedQuestion.find('.optionPanel').show();
        $clonedQuestion.find('.optionPanel .option:not(:first-child):not(:nth-child(2))').remove();
        $clonedQuestion.find('.optionPanel .option').attr('data-id', '0');

        $clonedQuestion.find('textarea').val('');
        $clonedQuestion.find('input:checkbox').prop("checked", false);

        RemoveHtmlEditor($clonedQuestion);
        BindHtmlEditor($clonedQuestion.find('textarea.questionText'));
        $clonedQuestion.find('select option:first').prop('selected', true);
        $clonedQuestion.attr('data-id', '0');
        $clonedQuestion.attr('data-sort', parseInt(sort) + 1);

        $clonedQuestion.show();
        $(this).closest('#ExamQuestionHolder').find('.question:last').after($clonedQuestion);


    });


    //// ADD NEW QUESTION OPTION

    $('#ExamQuestionHolder').on('click', '#addNewOption', function () {

        var $clonedOption = $(this).closest('.optionPanel').find('.option:first').clone();
        $clonedOption.find('label.errorHolder').remove();
        $clonedOption.find('textarea').val('');
        $clonedOption.find('input:checkbox').prop('checked', false);
        $clonedOption.attr('data-id', '0');

        $(this).closest('.optionPanel').find('.option:last').after($clonedOption);


    });

    //// DELETE QUIZ 

    $('#ExamQuestionHolder').on('click', '.delete-question', function () {
        var $button = $(this);
        var sort = $button.closest('.question').attr('data-sort');
        var $panel = $button.closest('#ExamQuestionHolder');
        var id = $button.closest('.question').attr('data-id');

        if ($button.closest('#ExamQuestionHolder').find('.question').length == 1) {
            ShowNotie({ HasError: true, Message: "An examination must have at least one question" });
            return;
        }

        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;


            if (id == '0') {

                $button.closest('#ExamQuestionHolder').find('.qnumbering .btn-group[data-sort="' + sort + '"]').remove();
                $button.closest('.question').remove();
                var nextSort = (parseInt(sort) - 1) == 0 ? 2 : parseInt(sort) - 1;

                $panel.find('.question[data-sort="' + nextSort + '"]').show();
                $panel.find('.qnumbering .btn-group[data-sort="' + nextSort + '"]').addClass('active');

                
                $panel.find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) {
                    var moveSort = i + 1;
                    var curSort = $(item).attr('data-sort');
                    $(item).attr('data-sort', moveSort);
                    $panel.find('.question[data-sort="' + curSort + '"]').attr('data-sort', moveSort);
                    $(item).find('a').html(moveSort);
                });

            }
            else {

                if (!parseInt(id) && parseInt(id) <= 0) {
                    return;
                }
                var prev = $button.html();
                $button.attr('disabled', 'disabled');
                $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');

                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/Exam/DeleteQuestion?Id=" + id,

                    success: function (data) {

                        var $sortButton = $button.closest('#ExamQuestionHolder').find('.qnumbering .btn-group[data-id="' + id + '"]');
                        var $nextButton = !$sortButton.prev().length ? $sortButton.next() : $sortButton.prev();

                        $button.closest('#ExamQuestionHolder').find('.qnumbering .btn-group[data-id="' + id + '"]').remove();
                        $button.closest('.question').remove();
                        var nextSort = $nextButton.attr("data-sort");
                        

                        $panel.find('.question[data-sort="' + nextSort + '"]').show();
                        $panel.find('.qnumbering .btn-group[data-sort="' + nextSort + '"]').addClass('active');

                        $panel.find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) {
                            var moveSort = i + 1;
                            var curSort = $(item).attr('data-sort');
                            $(item).attr('data-sort', moveSort);
                            $panel.find('.question[data-sort="' + curSort + '"]').attr('data-sort', moveSort);
                            $(item).find('a').html(moveSort);
                        });
                    },

                    error: function (data) {
                        var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                        ShowNotie({ HasError: true, Message: message });
                        $button.removeAttr('disabled');
                        $button.html(prev);
                    }
                });


            }


        }

    });

    //// DELETE OPTION

    $('#ExamQuestionHolder').on('click', '.delete-option', function () {
        var $button = $(this);


        var id = $button.closest('.option').attr('data-id');
        var $panel = $button.closest('.optionPanel');

        if ($button.closest('.optionPanel').find('.option').length == 2) {
            ShowNotie({ HasError: true, Message: "An examination must have at least two options" });
            $button.removeClass("disabled");
            $button.find('i').removeClass("fa-spinner fa-spin");
            $button.find('i').addClass("fa-trash");
            return;
        }


        if (id == '0') {

            $button.closest('.option').remove();

        }
        else {
            var $confirm = ShowConfirm();
            $confirm.onAction = function (btnName) {

                if (btnName == "cancel")
                    return;



                if (!parseInt(id) && parseInt(id) <= 0) {
                    return;
                }
                $button.addClass("disabled");
                $button.find('i').removeClass("fa-trash");
                $button.find('i').addClass("fa-spinner fa-spin");
                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/Exam/DeleteQuestionOption?Id=" + id,

                    success: function (data) {
                        $button.closest('.option').remove();
                        ShowNotie(data);
                    },

                    error: function (data) {
                        var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                        ShowNotie({ HasError: true, Message: message });
                        $button.removeClass("disabled");
                        $button.find('i').removeClass("fa-spinner fa-spin");
                        $button.find('i').addClass("fa-trash");
                    }
                });


            }


        }

    });

    


    //// GET QUIZ QUESTION TO DISPLAY

    $('#ExamQuestionHolder').on('click', '.getquestion', function () {
        var sort = $(this).parent().attr('data-sort');
        $(this).closest('#ExamQuestionHolder').find('.question').hide();
        $(this).closest('#ExamQuestionHolder').find('.question[data-sort="' + sort + '"]').show();
        $(this).closest('.qnumbering').children().removeClass('active');
        $(this).parent().addClass('active');
    });


    //// TOGGLE QUIZ PANEL WHEN ANSWER TYPE IS CHANGED
    $('#ExamQuestionHolder').on('change', '.AnswerType', function () {
        var $item = $(this);
        if ($item.val() == 'textbox') {
            $item.closest('.question').find('.AnswerSelection,.optionPanel').hide();
        }
        else {
            $item.closest('.question').find('.AnswerSelection,.optionPanel').show();
        }

        if ($item.val() == 'checkbox') {
            $item.closest('.question').find('.AnswerSelection').show();
        }
        else {
            $item.closest('.question').find('.AnswerSelection').hide();
        }
    });


    //// UN-CHECK option checkbox when not multi selection controls

    $('#ExamQuestionHolder').on('change', '.settingPanel :checkbox', function () {
        var $item = $(this);
        var $questionPanel = $(this).closest('.question');
        var $optionPanel = $(this).closest('.optionPanel');
        var answerType = $questionPanel.find('select.AnswerType').val();

        if (answerType != "checkbox") {
            if ($item.is(':checked')) {
                $optionPanel.find('.settingPanel :checkbox').not($item).prop('checked', false);
            }
        }

    });

    ////SAVE QUESTION CONTENT

    $('#ExamQuestionHolder').on('click', '#saveQuestion', function () {
        var $button = $(this);
        var prev = $button.html();
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
        var examQuestionObject = new Object();

        var $contentPanel = $button.closest('.question');
        $contentPanel.find('label.error').remove();

        examQuestionObject.Id = $contentPanel.attr('data-id');

        var isValid = true;
        var desc = String($contentPanel.find('textarea.questionText').val());

        if (desc.trim() == "") {
            $contentPanel.find('textarea.questionText').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
            isValid = false;
        }
        else {
            examQuestionObject.Question = desc;
        }

        var weight = $contentPanel.find('input[name="Weight"]').val();

        if (!parseInt(weight)) {
            $contentPanel.find('input[name="Weight"]').before('<label class="error errorHolder">This field must be a digit.</label>');
            isValid = false;
        }
        else {
            examQuestionObject.Weight = parseInt(weight);
        }

        var answerType = $contentPanel.find('select.AnswerType').val();
       
        var answerTypeId = GetQuestionAnswerType(answerType);
      
        var answerLogic = $contentPanel.find('select.AnswerType').val() == "checkbox" ? $contentPanel.find('select.AnswerSelection').val() : 0;

        examQuestionObject.AnswerType = answerTypeId;


        if (answerType != 'textbox') {
            if (!$contentPanel.find('.optionPanel .option .settingPanel input:checked').length) {
                $contentPanel.find('.optionPanel').before('<label class="error errorHolder block">You must add at least one of your options as an answer.</label>');
                isValid = false;
            }

            if ($contentPanel.find('.optionPanel .option').length < 2) {
                $contentPanel.find('.optionPanel label').after('<label class="error errorHolder block">You must add at least two options.</label>');
                isValid = false;
            }

            if (answerType != "checkbox") {
                if ($contentPanel.find('.optionPanel .settingPanel :checkbox:checked').length > 1) {
                    $contentPanel.find('.optionPanel label').after('<label class="error errorHolder block">This answer type does not permit multiple answers.</label>');
                    isValid = false;
                }
            }

            var options = [];

            $contentPanel.find('.optionPanel .option').each(function (i, item) {

                var optionTitle = $(item).find('.questionPanel textarea').val();
                var rightAnswer = $(item).find('.settingPanel input').is(':checked');

                if (optionTitle.trim() == '') {
                    $(item).find('.questionPanel').prepend('<label class="error errorHolder">This field is required.</label>');
                    isValid = false;
                }

                var optionObject = new Object();
                optionObject.Title = optionTitle;
                optionObject.Id = $(item).attr('data-id');
                optionObject.IsAnswer = rightAnswer;
                optionObject.Value = i + 1;
                options.push(optionObject);
            });
        }



        examQuestionObject.Options = options;
        examQuestionObject.ExaminationId = $('#ExamID').val();
        examQuestionObject.IsMultipleChoice = parseInt(answerLogic) == 0 ? false : true;

       
        if (!isValid) {
            ShowNotie({ HasError: true, Message: "You have some form errors. Please verify and try again" });
            $button.removeAttr('disabled');
            $button.html(prev);

            return;
        }

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/Exam/AddorUpdateQuestionContent",
            data: examQuestionObject,
            success: function (data) {
                $contentPanel.attr('data-id', data.Result.Id);
                $contentPanel.parent().find('.qnumbering .btn-group.active').attr('data-id', data.Result.Id);
                if (GetQuestionAnswerType(data.Result.AnswerType) != 'textbox') {
                    $contentPanel.find('.optionPanel .option').each(function (i, item) {
                        $(item).attr("data-id", data.Result.Options[i].Id);
                    });
                }

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

    });



    //// STEP 2 RESORT QUESTIONS

    $('#ExamStep2').on('click', function () {
        var $button = $(this);
        var examId = $('#ExamID').val();
        var examObject = { Id: examId, ExamQuestions: [] };
        var unSavedQuestions = $('#ExamQuestionHolder').find('.qnumbering .btn-group[data-id="0"]');
        var questionList = [];
        $('#ExamQuestionHolder').find('.qnumbering .btn-group:not([data-id="0"]):not(.excludeSort)').each(function (i, item) {
            var questionObject = {
                Id: $(item).attr('data-id'),
                SortOrder: (i + 1)
            }
            examObject.ExamQuestions.push(questionObject);
        });

        if (examObject.ExamQuestions.length == 0) {
            $('.formArea2').fadeOut();
            setTimeout(function () {
                $('.formArea3').show();
                $('.progress1 .tabloid:nth-child(3)').addClass('active');
                $('.progressIndicator1').css("width", "100%");

            }, 500);
            return;
        }

        var confirmHtml = '<div class="nofloat customError"><span class="title">All re-sorted questions will be updated. Are you sure you want to proceed?</span>';

        if (unSavedQuestions > 0) {
            confirmHtml = confirmHtml + '<div><h4>Warning</h4>';
            confirmHtml = confirmHtml + '<span>You have ' + unSavedQuestions + ' unsaved questions. They will be removed if you proceed </span></div>';
        }
        confirmHtml = confirmHtml + "</div>";


        var $confirm = ShowConfirm(confirmHtml);
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;

            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Exam/ResortExamQuestions",
                data: examObject,
                success: function (data) {
                    $('.formArea2').fadeOut();
                    setTimeout(function () {
                        $('.formArea3').show();
                        $('.progress1 .tabloid:nth-child(3)').addClass('active');
                        $('.progressIndicator1').css("width", "100%");

                    }, 500);
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


    $('.goback').click(function () {
        var $area = $(this).closest('.formArea');
        var step = $area.attr('data-step');
        $('.formArea').fadeOut();
        $('.progress1 .tabloid:nth-child(' + step + ')').removeClass('active');
        $('.progressIndicator1').css("width", "" + ((step - 1) * 50) + "%");
        setTimeout(function () {
            $area.prev().fadeIn();

        }, 500);



    });

});

function clearForm() {
    $('#ExamID').val('0');
    $('input,select').removeAttr("disabled");
    $('input[name="Name"]').val('');
    $('#ExamTitle').html('');
    BindHtmlEditor($('textarea[name="Description"]'),'');
    $('input[name="HourLimit"]').html('');
    $('input[name="MinuteLimit"]').html('');
    $('input[name="PassGrade"]').html('');
    $('select[name="CourseId"] option:first').prop("selected", true);
  //  $('select[name="CourseId"]').select2({ "width": "100%" });
    $('input:checkbox').prop("checked", false);

    $('#chkIsStandAlone').removeAttr("disabled");
    $('#chkIgnoreExamReTakeCount').removeAttr("disabled");
    $('input[name="ExamRetakeCount"]').val("1");
    $('input[name="StartDateFormat"]').datepicker("update", new Date());
    $('input[name="EndDateFormat"]').datepicker("update", new Date());
    $('input[name="StartDateFormat"]').html('');
    $('input[name="EndDateFormat"]').html('');
    $('input[name="HourLimit"]').val('');
    $('input[name="MinuteLimit"]').val('');
    $('input[name="PassGrade"]').val('');
    $('#ExamQuestionHolder').html('');
    $('.formArea').hide();
    $('.formArea1').show();
    $('.progress1 .tabloid').removeClass('active');
    $('.progress1 .tabloid:nth-child(1)').addClass('active');
    $('.progressIndicator1').css("width", "25%");
    $('#listPanel').fadeOut();
    $('#managePanel').fadeIn();
}



function BindExamForm(data) {
  
    $('#ExamTitle').html(data.Result.Name);
    $('input[name="Name"]').val(data.Result.Name);
    BindHtmlEditor($('textarea[name="Description"]'), data.Result.Description);
    $('input[name="HourLimit"]').val(data.Result.HourLimit);
    $('input[name="MinuteLimit"]').val(data.Result.MinuteLimit);
    $('input[name="PassGrade"]').val(data.Result.PassGrade);
    if (data.Result.ExamType == 1) {
        $('select[name="CourseId"] option[value="' + data.Result.CourseId + '"]').prop("selected", true);
        // $('input[name="ExamRetakeCount"]').val($('select[name="CourseId"] option[value="' + data.Result.CourseId + '"]').attr("data-examretake"));
        // $('input[name="ExamRetakeCount"]').attr("disabled", "disabled");
        //  $('#chkIgnoreExamReTakeCount').attr("disabled", "disabled");
    }
    else {
        $('#chkIsStandAlone').prop("checked", true);
        $('select[name="CourseId"]').attr("disabled", "disabled");
    }

    if (data.Result.ExamRetakeCount != null) {
        $('input[name="ExamRetakeCount"]').val(data.Result.ExamRetakeCount);
        $('#chkIgnoreExamReTakeCount').prop("checked", false);
    }
    else {
        $('#chkIgnoreExamReTakeCount').prop("checked", true);
        $('input[name="ExamRetakeCount"]').attr("disabled", "disabled");
    }

    $('select[name="CourseId"]').select2({ "width": "100%" });

    if (data.Result.StartDate == null) {
        $('input[name="StartDateFormat"]').attr("disabled", "disabled");
        $('input[name="EndDateFormat"]').attr("disabled", "disabled");
        $('#IngoreDueDate').prop("checked", true);
    }
    else {
        $('input[name="StartDateFormat"]').html(data.Result.StartDateFormat);
        $('input[name="EndDateFormat"]').html(data.Result.EndDateFormat);
        $('input[name="StartDateFormat"]').datepicker("update", data.Result.StartDateFormat);
        $('input[name="EndDateFormat"]').datepicker("update", data.Result.EndDateFormat);
    }

    if (data.Result.HourLimit == null && data.Result.MinuteLimit == null) {
        $('#IgnoreTimeLimit').prop("checked", true);
        $('input[name="HourLimit"]').attr("disabled", "disabled");
        $('input[name="MinuteLimit"]').attr("disabled", "disabled");
    }
    else {
        $('input[name="HourLimit"]').val(data.Result.HourLimit);
        $('input[name="MinuteLimit"]').val(data.Result.MinuteLimit);
    }


    $('input[name="PassGrade"]').val(data.Result.PassGrade);

    $('#ExamID').val(data.Result.Id);
}

function refreshSortableQuestions() {
    $('#ExamQuestionHolder').find('.qnumbering').sortable({
        items: '.btn-group:not(.excludeSort):not([data-id="0"])',
        update: function (event, ui) {
            
            ui.item.closest('.qnumbering').find('.btn-group:not(.excludeSort)').each(function (i, item) {
                var sort = i + 1;
                var oldSort = $(item).attr('data-sort');
                $(item).closest('#ExamQuestionHolder').find('.question[data-sort="' + oldSort + '"]').data('new-sort', sort);
                $(item).data('new-sort', sort);
                $(item).find('a').html(sort);
            });
            $('#ExamQuestionHolder').find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) {
                $(item).attr('data-sort', $(item).data('new-sort'));
                $(item).removeData('new-sort');
            });

            $('#ExamQuestionHolder').find('.question').each(function (i, item) {
                $(item).attr('data-sort', $(item).data('new-sort'));
                $(item).removeData('new-sort');
            });
            

        }
    });
}

