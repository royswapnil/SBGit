$(document).ready(function () {

    ///GET MUSTACHE TEMPLATES FOR BINDING

    $.get('/PartialViews/Admin/ExamQuestions.html', function (template) {
        $('#PartialTemplates').append(template);
        var template = document.getElementById('ExamQuestions').innerHTML;
        var output = Mustache.render(template, { array: [] });

        $('#QuestionHolder').append(output);
       
        $('#QuestionHolder').find('select.AnswerType option[value="textbox"]').show();
       
        $('#QuestionHolder').find('input[name="Weight"]').closest('.row').remove();
        BindHtmlEditor($('#QuestionHolder textarea.questionText'));
    });


    var jqTable = $('#SurveyTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Survey/GetTemplateDatatable',
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

                    return data.Name + '<br/><br/>';
                }
            },
            { data: "QuestionCount" },
            { data: "SurveyCount" },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {
                    return '<button class="table-edit btnSmaller btnBlue" href="javascript:;"> <i class="fa fa-edit table-icon" style=""></i> Edit	</button>' +
                        '<button class="table-delete btnSmaller btnRed" href= "javascript:;" > <i class="fa fa-trash table-icon" style=""></i> <span class="deleteLA">Delete</span></button>' +
                        '<input type="hidden" value="' + data.Id + '"/>';
                    //if (data.SurveyCount == 0) {

                    //}

                    //else {
                    //    return '';
                    //}

                }
            }
        ]
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });

    /// Delete Exam

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
                url: "/api/Survey/DeleteTemplate?Id=" + id,

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


    /// Edit Template
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
            url: "/api/Survey/GetTemplate",
            data: { Id: id },
            success: function (data) {

                // Bind Exam Form

                $('#TemplateName').val(data.Result.Name);
                $('#TemplateName').data("OldName", data.Result.Name);
                $('#TemplateID').val(data.Result.Id);

                var template = document.getElementById('ExamQuestions').innerHTML;
               
                var output = Mustache.render(template, { array: data.Result.Questions });
                $('#QuestionHolder').find('.qnumbering').remove();
                $('#QuestionHolder').find('.question').remove();
                $('#QuestionHolder').append(output);
                $('#QuestionHolder').find('select.AnswerType option[value="textbox"]').show();
                $('#QuestionHolder').find('input[name="Weight"]').closest('.row').remove();
                refreshSortableQuestions();
                $('#QuestionHolder').find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) { $(item).find('a').html(i + 1); });
                $('#QuestionHolder').find(".qnumbering .btn-group:first").addClass('active');
                $('#QuestionHolder').find(".question:first").show();

                $('#QuestionHolder').find('textarea.questionText').each(function (i, item) {
                    var html = $(item).val();
                    BindHtmlEditor($(item), html);
                });

                data.Result.Questions.forEach(function (item) {
                    var answerType = GetQuestionAnswerType(item.AnswerType);
                    $('#QuestionHolder').find('.question[data-id ="' + item.Id + '"] select.AnswerType option[value="' + answerType + '"]').prop('selected', true);

                    /// hide panels if is text box
                    if (answerType == "textbox") {
                        $('#QuestionHolder').find('.question[data-id ="' + item.Id + '"]').find('.AnswerSelection,.optionPanel').hide();
                    }

                    if (answerType == "checkbox") {
                        $('#QuestionHolder').find('.question[data-id ="' + item.Id + '"]').find('.AnswerSelection').show();
                    }

                    var AnswerLogic = item.IsMultipleChoice ? 1 : 0;
                    $('#QuestionHolder').find('.question[data-id ="' + item.Id + '"] select.AnswerSelection  option[value="' + AnswerLogic + '"]').prop('selected', true);
                });



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



    /// ADD NEW QUESTION

    $('#QuestionHolder').on('click', '.addquestion', function () {
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

        var $clonedQuestion = $(this).closest('#QuestionHolder').find('.question:first').clone();
        $clonedQuestion.find('label.errorHolder').remove();
        $(this).closest('#QuestionHolder').find('.question').hide();
        $clonedQuestion.find('.optionPanel').find('.option:not(:first-child):not(:nth-child(2))').remove();
        $clonedQuestion.find('.optionPanel .option').attr('data-id', '0');
        $clonedQuestion.find('.optionPanel').show();
        $clonedQuestion.find('textarea').val('');
        $clonedQuestion.find('input:checkbox').prop("checked", false);

        RemoveHtmlEditor($clonedQuestion);
        BindHtmlEditor($clonedQuestion.find('textarea.questionText'));
        $clonedQuestion.find('select option:first').prop('selected', true);
        $clonedQuestion.attr('data-id', '0');
        $clonedQuestion.attr('data-sort', parseInt(sort) + 1);

        $clonedQuestion.show();
        $(this).closest('#QuestionHolder').find('.question:last').after($clonedQuestion);


    });


    //// ADD NEW QUESTION OPTION

    $('#QuestionHolder').on('click', '#addNewOption', function () {

        var $clonedOption = $(this).closest('.optionPanel').find('.option:first').clone();
        $clonedOption.find('label.errorHolder').remove();
        $clonedOption.find('textarea').val('');
        $clonedOption.find('input:checkbox').prop('checked', false);
        $clonedOption.attr('data-id', '0');

        $(this).closest('.optionPanel').find('.option:last').after($clonedOption);

    });


    //// DELETE QUIZ 

    $('#QuestionHolder').on('click', '.delete-question', function () {
        var $button = $(this);
        var sort = $button.closest('.question').attr('data-sort');
        var $panel = $button.closest('#QuestionHolder');
        var id = $button.closest('.question').attr('data-id');

        if ($button.closest('#QuestionHolder').find('.question').length == 1) {
            ShowNotie({ HasError: true, Message: "A template must have at least one question" });
            return;
        }

        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;


            if (id == '0') {

                $button.closest('#QuestionHolder').find('.qnumbering .btn-group[data-sort="' + sort + '"]').remove();
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
                    url: "/api/Survey/DeleteQuestion?Id=" + id,

                    success: function (data) {

                        var $sortButton = $button.closest('#QuestionHolder').find('.qnumbering .btn-group[data-id="' + id + '"]');

                        var $nextButton = !($sortButton.prev().length) ? $sortButton.next() : $sortButton.prev();

                        $button.closest('#QuestionHolder').find('.qnumbering .btn-group[data-id="' + id + '"]').remove();
                        $button.closest('.question').remove();
                        var nextSort = $nextButton.attr("data-sort");

                        console.log(nextSort);

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

    $('#QuestionHolder').on('click', '.delete-option', function () {
        var $button = $(this);


        var id = $button.closest('.option').attr('data-id');
        var $panel = $button.closest('.optionPanel');

        if ($button.closest('.optionPanel').find('.option').length == 2) {
            ShowNotie({ HasError: true, Message: "A question must have at least two options" });
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
                    url: "/api/Survey/DeleteQuestionOption?Id=" + id,

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

    $('#QuestionHolder').on('click', '.getquestion', function () {
        var sort = $(this).parent().attr('data-sort');
        $(this).closest('#QuestionHolder').find('.question').hide();
        $(this).closest('#QuestionHolder').find('.question[data-sort="' + sort + '"]').show();
        $(this).closest('.qnumbering').children().removeClass('active');
        $(this).parent().addClass('active');
    });


    //// TOGGLE QUIZ PANEL WHEN ANSWER TYPE IS CHANGED
    $('#QuestionHolder').on('change', '.AnswerType', function () {
        var $item = $(this);
        if ($item.val() == "textbox") {
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

    $('#QuestionHolder').on('change', '.settingPanel :checkbox', function () {
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

    $('#QuestionHolder').on('click', '#saveQuestion', function () {
        var $button = $(this);
        var prev = $button.html();
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
        var $contentPanel = $button.closest('.question');
        $contentPanel.find('label.error').remove();

        var templateObj = new Object();

        templateObj.Id = parseInt($('#TemplateID').val());


        //validate template name
        var name = $('#TemplateName').val().trim();
        if (templateObj.Id == 0) {

            if (name == "") {
                $('#TemplateName').before('<label class="error errorHolder">This field is required.</label>');
                isValid = false;
            }

        }
        templateObj.Name = name;


        var questionObject = new Object();

        questionObject.Id = $contentPanel.attr('data-id');

        var isValid = true;
        var desc = String($contentPanel.find('textarea.questionText').val());

        if (desc.trim() == "") {
            $contentPanel.find('textarea.questionText').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
            isValid = false;
        }
        else {
            questionObject.Question = desc;
        }

        var answerType = $contentPanel.find('select.AnswerType').val();
        var answerTypeId = GetQuestionAnswerType(answerType);
        var answerLogic = $contentPanel.find('select.AnswerSelection').val();
        questionObject.AnswerType = answerTypeId;


        if (answerType != 'textbox') {
            //if (!$contentPanel.find('.optionPanel .option .settingPanel input:checked').length) {
            //    $contentPanel.find('.optionPanel').before('<label class="error errorHolder block">You must add at least one of your options as an answer.</label>');
            //    isValid = false;
            //}

            if ($contentPanel.find('.optionPanel .option').length < 2) {
                $contentPanel.find('.optionPanel label').after('<label class="error errorHolder block">You must add at least one option.</label>');
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


        questionObject.TemplateId = templateObj.Id;
        questionObject.Options = options;
        questionObject.IsMultipleChoice = parseInt(answerLogic) == 0 ? false : true;


        if (!isValid) {
            ShowNotie({ HasError: true, Message: "You have some form errors. Please verify and try again" });
            $button.removeAttr('disabled');
            $button.html(prev);

            return;
        }

        var oldName = $('#TemplateName').data("OldName");


        if (templateObj.Id == 0 || (oldName != null && oldName != templateObj.Name)) {
            $button.html('Updating Template Details <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Survey/AddorUpdateTemplate",
                data: templateObj,
                success: function (data) {
                    questionObject.TemplateId = data.Result.Id;

                    $('#TemplateID').val(data.Result.Id);
                    $('#TemplateName').data("OldName", data.Result.Name);

                    /// add question
                    $button.html('Updating Template Question <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
                    UpdateTemplateContent(questionObject, $button);
                    jqTable.ajax.reload();
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                    $button.removeAttr('disabled');
                    $button.html(prev);
                },
                complete: function (data) {

                }
            });
        }
        else {
            $button.html('Updating Template Question <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
            UpdateTemplateContent(questionObject, $button);
        }

    });

    $('#reSortTemplate').on('click', function () {

        var $button = $(this);
        var templateId = $('#TemplateID').val();
        var templateObject = { Id: templateId, Questions: [] };
        var unSavedQuestions = $('#QuestionHolder').find('.qnumbering .btn-group[data-id="0"]');
        var questionList = [];
        $('#QuestionHolder').find('.qnumbering .btn-group:not([data-id="0"]):not(.excludeSort)').each(function (i, item) {
            var questionObject = {
                Id: $(item).attr('data-id'),
                SortOrder: $(item).attr('data-sort')
            }
            templateObject.Questions.push(questionObject);
        });

        if (templateObject.Questions.length == 0) {
            $button.removeClass('btnBlue');
            $button.attr('disabled', 'disabled');
        }

        var confirmHtml = '<div class="nofloat customError"><span class="title">All re-sorted questions will be updated. Are you sure you want to proceed?</span></div>';

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
                url: "/api/Survey/ResortQuestions",
                data: templateObject,
                success: function (data) {
                    $button.removeClass('btnBlue');
                },

                error: function (data) {
                    $button.removeAttr('disabled');
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                },
                complete: function (data) {
                    $button.html(prev);
                }
            });
        }
    });


});

function refreshSortableQuestions() {
    $('#QuestionHolder').find('.qnumbering').sortable({
        items: '.btn-group:not(.excludeSort):not([data-id="0"])',
        update: function (event, ui) {
            $('#reSortTemplate').addClass('btnBlue');
            $('#reSortTemplate').removeAttr('disabled');
            ui.item.closest('.qnumbering').find('.btn-group:not(.excludeSort)').each(function (i, item) {
                var sort = i + 1;
                var oldSort = $(item).attr('data-sort');
                $(item).closest('#QuestionHolder').find('.question[data-sort="' + oldSort + '"]').data('new-sort', sort);
                $(item).data('new-sort', sort);
                $(item).find('a').html(sort);
            });
            $('#QuestionHolder').find('.qnumbering .btn-group:not(.excludeSort)').each(function (i, item) {
                $(item).attr('data-sort', $(item).data('new-sort'));
                $(item).removeData('new-sort');
            });

            $('#QuestionHolder').find('.question').each(function (i, item) {
                $(item).attr('data-sort', $(item).data('new-sort'));
                $(item).removeData('new-sort');
            });

        }
    });
}

function UpdateTemplateContent(question, $button) {
    $.ajax({
        cache: false,
        async: true,
        type: "POST",
        url: "/api/Survey/AddorUpdateQuestionContent",
        data: question,
        success: function (data) {
            var $contentPanel = $button.closest('.question');
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
            $button.html("Save Question");
        }
    });
}