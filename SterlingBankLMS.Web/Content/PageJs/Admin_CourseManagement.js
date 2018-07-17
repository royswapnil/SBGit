$(document).ready(function () {
    ///GET MUSTACHE TEMPLATES FOR BINDING

    $.get('/PartialViews/Admin/CourseTableRow.html', function (template) {
        $('#PartialTemplates').append(template);
    });

    $.get('/PartialViews/Admin/ManageCourseModules.html', function (template) {
        $('#PartialTemplates').append(template);
    });


    BindHtmlEditor($('textarea:not(#CourseShortDesc)'));
    $('.datepicker').datepicker({ format: "dd/mm/yyyy" });


    $.get('/api/Course/GetLearningAreas', function (data) {
        for (var i = 0; i < data.Result.length; i++) {
            $('#LearningArea,#FilterLearningArea').append('<option value=' + data.Result[i].LearningArea.Id + '>' + data.Result[i].LearningArea.Name + '</option>');
        }
    });

    var jqTable = $('#CourseTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Course/GetDatatableCourses',
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
            { data: "LearningAreaName" },
            { data: "ModuleCount" },
            { data: "DueDateFormat" },
            //{ data: "PassGrade" },
            //{ data: "ExamRetakeCount" },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {
                    var btnClass = data.IsPublished ? 'btnGrey' : 'btnDarkPink';
                    var btnIconClass = data.IsPublished ? 'fa-ban' : 'fa-power-off';
                    var btnText = data.IsPublished ? 'Un-publish' : 'Publish';

                    var dropdown = '<div class="dropdown">' +
                        '<button class="btn btnSmaller  btnBlue dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                        ' Actions <i class="fa fa-caret-down"></i>' +
                        '</button>' +
                        '<div class="dropdown-menu pull-right" aria-labelledby="dropdownMenuButton" > ' +
                        ' <a class="table-edit dropdown-item blue"><i class="fa fa-edit table-icon" style=""></i>  Edit</a > ' +
                        '  <a class="dropdown-item table-publish"> <i class="fa ' + btnIconClass + ' table-icon" style=""></i> ' + btnText + '</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/Examination/Course/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Add Exam</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/Survey/Course/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Add Survey</a > ' +
                        '    <a class="dropdown-item table-delete red"> <i class="fa fa-trash table-icon" style=""></i> Delete</a>' +
                        ' <a class="table-clone dropdown-item blue"><i class="fa fa-copy table-icon" style=""></i>  Clone Course</a > ' +
                        ' </div> ' +
                        ' </div>'




                    return dropdown +
                        '<input type="hidden" value="' + data.Id + '"/>';
                }
            }
        ]
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });


    /// Delete Course

    $("#CourseTable tbody").on("click", "td.action-buttons a.table-delete", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }
        if (confirm("Are you sure you want to do this?")) {
            $button.addClass('disabled');
            $button.find('i').removeClass('fa-trash');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "Delete",
                url: "/api/Course/DeleteCourse?Id=" + id,

                success: function (data) {
                    jqTable.row(tr).remove().draw(false);
                    ShowNotie(data);
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                    $button.removeClass('disabled');
                    $button.html(prev);
                }
            });
        }

    });

    $("#CourseTable tbody").on("click", "td.action-buttons a.table-clone", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        var name = $(this).closest("tr").find('td:first-child').html();

        $('#CloneID').val(id);
        $('#modalClone input[name="Name"]').html('');
        $('#ClonedCourseName').html(name);
        $('#modalClone').modal('show');
        $('#modalClone .messageText').remove();

    });

    // un publish or publish course 

    $("#CourseTable tbody").on("click", "td.action-buttons a.table-publish", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }
        if (confirm("Are you sure you want to do this?")) {
            $button.addClass('disabled');
            $button.find('i').removeClass('fa-trash');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/UpdatePublishCourse?Id=" + id,

                success: function (data) {
                    if (data.Result) {
                        $button.removeClass('disabled');
                        $button.html('<i class="fa fa-ban table-icon"></i>Un-publish');
                    }
                    else {
                        $button.removeClass('disabled');
                        $button.html('<i class="fa fa-power-off table-icon"></i>Publish');
                    }

                    ShowNotie(data);
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                    $button.removeClass('disabled');
                    $button.html(prev);
                }
            });
        }

    });


    /// Edit Course
    $("#CourseTable tbody").on("click", "td.action-buttons a.table-edit", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $button.addClass('disabled');
        $button.find('i').removeClass('fa-trash');
        $button.find('i').addClass('fa-circle-o-notch fa-spin');
        $('#CourseIgnoreDueDate').prop('checked', false);
        $('#CourseDate').val('');
        $('#CourseDate').datepicker('update', new Date());

        $.ajax({
            cache: false,
            async: true,
            type: "Get",
            url: "/api/Course/GetCourse",
            data: { Id: id },
            success: function (data) {

                // Bind Course Form

                var date = data.Result.DueDate == null ? null : new Date(Date.parse(data.Result.DueDate));
                $('#CourseTitle').val(data.Result.Name);
                $('#CourseTitleSpan').html(data.Result.Name);
                BindHtmlEditor($('#CourseDesc'), data.Result.Description);
                $('#CourseShortDesc').val(data.Result.ShortDescription);
                if (date == null) {
                    $('#CourseIgnoreDueDate').prop('checked', true);
                    $('#CourseDate').attr('disabled', 'disabled');
                    $('#CourseDate').val('');
                }
                else {

                    $('#CourseDate').val(moment(date).format('DD/MM/YYYY'));
                    $('#CourseDate').datepicker('update', moment(date).format('DD/MM/YYYY'));
                    $('#CourseDate').removeAttr('disabled');
                    $('#CourseIgnoreDueDate').prop('checked', false);
                }

                $('.uploadBox img').prop('src', data.Result.ImageUrl);
                $('#FileUpload').attr('data-upload', true);
                $('#LearningArea').find('option[value="' + data.Result.LearningAreaId + '"]').prop('selected', true);
                BindHtmlEditor($('#CourseOverview'), data.Result.Overview);
                BindHtmlEditor($('#CourseObjectives'), data.Result.Objectives);
                $('#CourseEstimatedDuration').val(data.Result.EstimatedDuration);
                $('#CourseHoursPerWeek').val(data.Result.HoursPerWeek);

                //data.Result.ExamRetakeCount == null ? $('#CourseIgnoreRetake').prop('checked', true) : $('#ExamRetakeCount').val(data.Result.ExamRetakeCount);
                //$('#PassGrade').val(data.Result.PassGrade);
                $('#HasCertificate').prop('checked', data.Result.HasCertificate);
                $('#IsPublished').prop('checked', data.Result.IsPublished);

                $('#CourseID').val(data.Result.Id);

                // Bind Modules and Lesson Modules

                $('#CourseModuleHolder').find('.nabi').remove();
                $('.formArea').hide();
                $('.formArea1').show();

                var template = document.getElementById('ManageCourseModules').innerHTML;
                var output = Mustache.render(template, { array: data.Result.Modules });
                $('#CourseModuleHolder').prepend(output);

                $('#CourseModuleHolder').sortable({
                    items: '.nabi:not([data-id="0"])',
                    cancel: 'input,.nabiBd > *',
                    update: function (event, ui) {
                        ui.item.addClass('changed');
                        ui.item.nextAll().addClass('changed');
                    }

                });

                $button.removeClass('disabled');
                $button.html(prev);
                $('#CourseTablePanel').hide();
                $('.courseManagement').hide();
                $('#NewCoursePanel').fadeIn();
                $('.viewCourse').show();

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
                $button.removeClass('disabled');
                $button.html(prev);
            }
        });


    });


    $('#CourseIgnoreDueDate').on('change', function () {
        $(this).is(':checked') ? $('#CourseDate').attr('disabled', 'disabled') : $('#CourseDate').removeAttr('disabled');
    });

    //$('#CourseIgnoreRetake').on('change', function () {
    //    $(this).is(':checked') ? $('#ExamRetakeCount').attr('disabled', 'disabled') : $('#ExamRetakeCount').removeAttr('disabled');
    //});

    //// TOGGLE SCREEN VIEW OPTIONS
    $('.courseManagement').on('click', function () {
        $('#CourseTablePanel').fadeOut();
        $('#NewCoursePanel').fadeIn();
        $(this).hide();
        $('.viewCourse').show()

    });

    $('.createNewCourse').on('click', function () {

        $('#CourseTitle').val('');
        $('#CourseTitleSpan').html('');
        BindHtmlEditor($('#CourseDesc'), '');
        $('#CourseShortDesc').val('');
        $('#CourseIgnoreDueDate').prop('checked', false);
        $('#CourseDate').val('');
        $('#CourseDate').datepicker('update', new Date());
        $('.uploadBox img').removeAttr('src');
        $('#FileUpload').removeAttr('data-upload');
        $('#LearningArea').find('option[value="0"]').prop('selected', true);
        BindHtmlEditor($('#CourseOverview'), '');
        $('#CourseEstimatedDuration').val('');
        $('#CourseHoursPerWeek').val('');
        $('#CourseIgnoreRetake').prop('checked', false);
        //$('#ExamRetakeCount').val('');
        //$('#PassGrade').val('');
        $('#CourseID').val('0');
        $('#CourseModuleHolder').find('.nabi').remove();
        $('.formArea').hide();
        $('.formArea1').show();
        $('.progress1 .tabloid:nth-child(1)').addClass('active');
        $('.progressIndicator1').css("width", "25%");
        $('#CourseTablePanel').fadeOut();
        $('#NewCoursePanel').fadeIn();
        $('#HasCertificate').prop('checked', true);
        $('#IsPublished').prop('checked', false);
    });

    $('.viewCourse').on('click', function () {
        $('#NewCoursePanel').fadeOut();
        $('#CourseTablePanel').fadeIn();
        $(this).hide();
        $('.courseManagement').show();
    });


    //// file upload course changed preview

    $('#FileUpload').on('change', function (evt) {
        var elem = $(this);
        selectedFiles = evt.target.files || evt.dataTransfer.files;
        if (selectedFiles) {

            DataURLFileReader.read(selectedFiles[0], 0, function (err, fileInfo) {

                if (err != null) {
                    ShowNotie({ HasError: true, Message: err });
                }
                else {

                    var $img = $(elem).closest('.uploadBox').find('img');
                    var currentSrc = $img.attr("src");
                    if (fileInfo.type == "image") {

                        var fileUrl = window.URL.createObjectURL(fileInfo.fileupload);
                        $img.attr("src", fileUrl);
                    }
                    else {
                        if (currentSrc != undefined)
                            $img.attr("src", currentSrc);

                        $(elem).val("");
                        ShowNotie({ HasError: true, Message: "File type not supported" });
                    }
                }
            });

        }

    });


    /// Duplicate a course

    $("#FormCloneCourse").validate({
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
                url: "/api/Course/CloneCourse",
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



    /// validate Course creation step 1 and proceed to create or update
    $("#FormStep1").validate({
        ignore: [],
        rules: {
            Name: { required: true },
            Description: { required: true, maxlength: 5000 },
            DueDate: {
                required: "#CourseIgnoreDueDate:not(:checked)",
                DateGreater: 'currentDate'
            },
            LearningAreaId: "dropdown",
            ImageUpload: {
                required: function (item) {
                    return !$(item)[0].hasAttribute("data-upload")
                }, accept: "image/*"
            },
            Overview: { required: true },
            HoursPerWeek: { required: true, digits: true, min: 1 },
            EstimatedDuration: { required: true, digits: true, min: 1 },
            Objectives: { required: true }
        },
        message: {
            ImageUpload: { accept: "only image file types are allowed" },
            DueDate: { DateGreater: "Date must be greater than current day"}
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
            formData.append('CurrentStep', 1);
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/AddorUpdate",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (!data.HasError) {
                        $('#CourseID').val(data.Result.Id);
                        $('#CourseTitleSpan').html(data.Result.Name);
                        $('.formArea1').fadeOut();
                        jqTable.ajax.reload();
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
                    $button.html(prev);
                    $button.removeAttr('disabled');
                }
            });

        }
    });


    /// validate Course creation exam step 3 and proceed to update
    $("#FormStep3").validate({
        ignore: [],
        rules: {
            //PassGrade: { required: true, digits: true },
            //ExamRetakeCount: { required: "#CourseIgnoreRetake:not(:checked)" }
        },

        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.closest('.form-group').find('label:first').after($error);
        },
        submitHandler: function (form) {

            var $button = $(form).find(':submit');
            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

            var formData = new FormData(form);
            formData.append('CurrentStep', 3);
            formData.append('Id', $('#CourseID').val());

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/AddorUpdate",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (!data.HasError) {

                        $('.formArea3').fadeOut();
                        setTimeout(function () {
                            $('.progress1 .tabloid:nth-child(4)').addClass('active');
                            $('.progressIndicator1').css("width", "100%");
                            $('.formArea4').fadeIn();

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


    ////Course Module Collapsable Action

    $('#CourseModuleHolder').on('click', '.module-action', function (e) {

        if ($(e.target).is(":input"))
            return;

        var $item = $(this);
        $item.addClass('disabed');
        $item.toggleClass('closed');
        $item.toggleClass('opened');
        $item.hasClass('closed') ? $(this).find('i.material-icons').html('keyboard_arrow_up') : $(this).find('i.material-icons').html('keyboard_arrow_down');
        $item.closest('.nabi').find('.nabiBd').toggleClass('harden');

        if (!$item.hasClass('loaded')) {
            var Id = $item.closest('.nabi').attr('data-id');
            $item.closest('.nabi').find('.loadingPanel').show();
            $item.closest('.nabi').find('.emptyPanel').hide();

            $.ajax({
                url: '/api/Course/GetModuleLesson',
                type: 'GET',
                data: { Id: Id },
                cache: false,
                async: true,
                success: function (data) {
                    if (!data.HasError) {
                        var template = document.getElementById('LessonContentTemplate').innerHTML;
                        var output = Mustache.render(template, { array: data.Result });
                        $item.closest('.nabi').find('.nabiBd .emptyPanel').hide();
                        $item.closest('.nabi').find('.nabiBd .loadingPanel').hide();
                        $item.closest('.nabi').find('.nabiBd .emptyPanel').after(output);
                        $item.closest('.nabi').find('.nabiBd .action-buttons').show();
                        $item.addClass('loaded');

                        // sortable 
                        $item.closest('.nabi').find('.nabiBd').sortable({
                            items: '.courseSlate:not([data-id="0"])',
                            cancel: 'input,.courseLesson > *',
                            update: function (event, ui) {
                                ui.item.addClass('changed');
                                ui.item.nextAll().addClass('changed');
                            }
                        });

                        /// bind plugins to elements
                        if (data.Result != null && data.Result.length > 0) {
                            var $lessons = $item.closest('.nabi').find('.nabiBd .courseSlate');
                            $lessons.find('textarea.lessonDesc, .textWriter textarea,textarea.questionText').each(function (i, item) {
                                var html = $(item).val();
                                BindHtmlEditor($(item), html);
                            });

                        }
                        
                        for (var i = 0; i < data.Result.length; i++) {
                            if (data.Result[i].IsQuiz) {
                                var $quizLesson = $item.closest('.nabi').find('.nabiBd .courseSlate[data-id="' + data.Result[i].Id + '"]');

                                /// bind tag editors and option type and sort for quiz
                               
                                $quizLesson.find('.qnumbering').sortable({
                                    items: '.btn-group:not(.excludeSort):not([data-id="0"])',
                                    update: function (event, ui) {
                                        $('#reSortTemplate').addClass('btnBlue');
                                        $('#reSortTemplate').removeAttr('disabled');

                                        var $qPanel = ui.item.closest('.qnumbering');
                                        var $quesPanel = ui.item.closest('.uploadBox'); 

                                        $qPanel.find('.btn-group:not(.excludeSort)').each(function (j, item) {
                                            var sort = 1 + j;
                                            var oldSort = $(item).attr('data-sort');
                                            $quesPanel.find('.question[data-sort="' + oldSort + '"]').data('new-sort', sort);
                                            $(item).data('new-sort', sort);
                                            $(item).find('a').html(sort);
                                        });

                                        $qPanel.find('.btn-group:not(.excludeSort)').each(function (k, item) {s
                                            $(item).attr('data-sort', $(item).data('new-sort'));
                                            $(item).removeData('new-sort');
                                        });

                                        $quesPanel.find('.question').each(function (l, item) {
                                            $(item).attr('data-sort', $(item).data('new-sort'));
                                            $(item).removeData('new-sort');
                                        });
                                        
                                    }
                                });

                                $quizLesson.find(".uploadBox .qnumbering .btn-group:first").addClass('active');
                                $quizLesson.find(".uploadBox .question:first").show();

                                //if (data.Result[i].Questions.length == 0) {
                                //    var $quizQuestion = $quizLesson.find('.question[data-id="0"]');
                                //    $quizLesson.find('textarea.answerOptions').tagEditor(
                                //        {
                                //            initialTags: answerOptions,
                                //            beforeTagDelete: function (field, editor, tags, val) {
                                //                var answers = $quizQuestion.find('textarea.rightAnswer').val();
                                //                if (answers.indexOf(val) > -1)
                                //                    $quizQuestion.find('textarea.rightAnswer').tagEditor('removeTag', val, true);

                                //            }
                                //        }
                                //    );

                                //    $quizLesson.find('textarea.rightAnswer').tagEditor({
                                //        initialTags: rightAnswer,
                                //        beforeTagSave: function (field, editor, tags, tag, val) {
                                //            var answers = $quizQuestion.find('textarea.answerOptions').val();
                                //            if (answers.indexOf(val) == -1)
                                //                return false;

                                //        }
                                //    });

                                //    return;
                                //}


                                for (var j = 0; j < data.Result[i].Questions.length; j++) {
                                    var $quizQuestion = $quizLesson.find('.question[data-id="' + data.Result[i].Questions[j].Id + '"]');
                                    var answerType = GetQuestionAnswerType(data.Result[i].Questions[j].AnswerType);

                                    $quizQuestion.find('select.AnswerType option[value="' + answerType + '"]').prop('selected', true);

                                    var AnswerLogic = data.Result[i].Questions[j] ? 1 : 0;
                                    $quizQuestion.find('select.AnswerSelection option[value="' + AnswerLogic + '"]').prop('selected', true);

                                    if (answerType == "checkbox") {
                                        $quizQuestion.find('.AnswerSelection').show();
                                    }

                                    //var answerOptions = [];
                                    //var rightAnswer = [];

                                    //data.Result[i].Questions[j].Options.forEach(function (item) {

                                    //    var val = item.Title
                                    //    answerOptions.push(val);
                                    //    if (item.IsAnswer)
                                    //        rightAnswer.push(val);

                                    //});

                                    //$quizQuestion.find('textarea.answerOptions').tagEditor(
                                    //    {
                                    //        initialTags: answerOptions,
                                    //        beforeTagDelete: function (field, editor, tags, val) {
                                    //            var answers = $quizQuestion.find('textarea.rightAnswer').val();
                                    //            if (answers.indexOf(val) > -1)
                                    //                $quizQuestion.find('textarea.rightAnswer').tagEditor('removeTag', val, true);

                                    //        }
                                    //    }
                                    //);

                                    //$quizQuestion.find('textarea.rightAnswer').tagEditor({
                                    //    initialTags: rightAnswer,
                                    //    beforeTagSave: function (field, editor, tags, tag, val) {
                                    //        var answers = $quizQuestion.find('textarea.answerOptions').val();
                                    //        if (answers.indexOf(val) == -1)
                                    //            return false;

                                    //    }
                                    //});


                                }
                            }
                        }
                    }
                    else {
                        $item.closest('.nabi').find('.nabiBd .loadingPanel').hide();
                        $item.closest('.nabi').find('.nabiBd .emptyPanel').show();

                    }
                    $item.closest('.nabi').find('.nabiBd .action-buttons').show();
                },
                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                }
            });

        }


    });

    $('#CourseModuleHolder').on('click', '.lesson-action', function (e) {
        if ($(e.target).is(":input"))
            return;

        var $item = $(this);
        $item.addClass('disabed');
        $item.toggleClass('closed');
        $item.toggleClass('opened');
        $item.hasClass('closed') ? $(this).find('i.material-icons').html('keyboard_arrow_up') : $(this).find('i.material-icons').html('keyboard_arrow_down');
        $(this).closest('.courseSlate').find('.courseLesson.bordered-section').toggleClass('collasped');
    });


    //// Add New Course Module
    $('#addNewModule').on('click', function () {
        var $button = $(this);
        var template = document.getElementById('ManageCourseModules').innerHTML;
        var length = $('#CourseModuleHolder').find('.nabi').length
        var output = Mustache.render(template, { array: [{ IsNew: true, Id: 0 }] });
        $button.before(output);
    });

    //// Delete Course Module
    $('#CourseModuleHolder').on('click', '.delete-module', function () {
        var $button = $(this);
        var $row = $(this).closest('.nabi');
        if ($row.attr("data-id") == "0") {
            $row.remove();
        }
        else {
            var id = $row.attr("data-id");
            if (!parseInt(id) && parseInt(id) <= 0) {
                return;
            }

            var $confirm = ShowConfirm();
            $confirm.onAction = function (btnName) {

                if (btnName == "cancel")
                    return;

                var prev = $button[0].outerHTML;
                $button.attr('disabled', 'disabled');
                $button.find('i').removeClass('material-icons');
                $button.find('i').html('');
                $button.find('i').addClass('fa fa-circle-o-notch fa-spin nofloat');

                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/Course/DeleteModule?Id=" + id,

                    success: function (data) {
                        $row.remove();
                    },

                    error: function (data) {
                        var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                        ShowNotie({ HasError: true, Message: message });
                        $button.replaceWith(prev);
                    }
                });

            }
            return false;

        }

    });

    //// Delete Module Lesson
    $('#CourseModuleHolder').on('click', '.delete-lesson', function () {
        var $button = $(this);
        var $row = $(this).closest('.courseSlate');
        if ($row.attr("data-id") == "0") {
            $row.remove();
        }
        else {
            var id = $row.attr("data-id");
            if (!parseInt(id) && parseInt(id) <= 0) {
                return;
            }

            var $confirm = ShowConfirm();
            $confirm.onAction = function (btnName) {

                if (btnName == "cancel")
                    return;

                var prev = $button[0].outerHTML;
                $button.attr('disabled', 'disabled');
                $button.find('i').removeClass('material-icons');
                $button.find('i').html('');
                $button.find('i').addClass('fa fa-circle-o-notch fa-spin nofloat');

                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/Course/DeleteLesson?Id=" + id,

                    success: function (data) {
                        $row.remove();
                    },

                    error: function (data) {
                        var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                        ShowNotie({ HasError: true, Message: message });
                        $button.replaceWith(prev);
                    }
                });

            }
            return false;

        }

    });

    //// Edit Course Module
    $('#CourseModuleHolder').on('click', '.edit-nabi', function () {

        var $row = $(this).closest('.courseSlate');
        $row.find('.topicNm input').show();
        $row.find('.topicNm span').hide();
        $row.find('.topicNm .close-edit-nabi').show();

        $row.find('.topicAction .save-nabi').show();
        $row.find('.topicAction .edit-nabi').hide();
    });

    //// Close Edit or Add Course Module
    $('#CourseModuleHolder').on('click', '.close-edit-nabi', function () {

        var $row = $(this).closest('.courseSlate');
        $row.find('.topicNm input').val($row.find('.topicNm span').html());
        $row.find('.topicNm input').hide();
        $row.find('.topicNm .close-edit-nabi').hide();
        $row.find('.topicNm span').show();
        $row.find('.topicAction .edit-nabi').show();
        $row.find('.topicAction .save-nabi').hide();
    });


    //// Save New Course Module
    $('#CourseModuleHolder').on('click', '.save-module', function () {
        var $button = $(this);
        var prev = $button[0].outerHTML;
        var $row = $(this).closest('.courseSlate');
        var moduleName = $row.find('.topicNm input').val();

        if (moduleName.trim() == '') {
            return ShowNotie({ Message: "Module name is required" });
        }

        $button.attr('disabled', 'disabled');
        $button.find('i').removeClass('material-icons');
        $button.find('i').html('');
        $button.find('i').addClass('fa fa-circle-o-notch fa-spin nofloat');

        var formData = { Id: $button.closest('.nabi').attr('data-id'), Name: moduleName, courseId: $('#CourseID').val() };

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/Course/AddorUpdateModule",
            data: formData,
            success: function (data) {
                if (!data.HasError) {
                    $button.closest('.nabi').attr('data-id', data.Result.Id);
                    $row.find('.topicNm span').html(data.Result.Name);
                    $row.find('.topicNm input').hide();
                    $row.find('.topicNm .close-edit-nabi').hide();
                    $row.find('.topicNm span').show();

                    $button.replaceWith(prev);
                    $row.find('.topicAction .edit-nabi').show();
                    $row.find('.topicAction .save-module').hide();
                }

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
                $button.replaceWith(prev);
            }
        });



    });

    //// Save New Module Lesson
    $('#CourseModuleHolder').on('click', '.save-lesson', function () {
        var $button = $(this);
        var prev = $button[0].outerHTML;
        var $row = $(this).closest('.courseSlate');
        var Name = $row.find('.topicNm input').val();
        var moduleId = $row.closest('.nabi').attr('data-id');
        var lessonId = $button.closest('.courseSlate').attr('data-id');
        var lessonType = $button.closest('.courseSlate').attr('data-type');

        if (moduleId == '0') {
            return ShowNotie({ HasError: true, Message: "Please save a module name before adding a lesson" });
        }

        if (Name.trim() == '') {
            return ShowNotie({ Message: "Lesson name is required" });
        }

        $button.attr('disabled', 'disabled');
        $button.find('i').removeClass('material-icons');
        $button.find('i').html('');
        $button.find('i').addClass('fa fa-circle-o-notch fa-spin nofloat');

        var formData = { Id: lessonId, Name: Name, moduleId: moduleId, LessonContentType: lessonType };

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/Course/AddorUpdateLesson",
            data: formData,
            success: function (data) {
                if (!data.HasError) {
                    $button.closest('.courseSlate').attr('data-id', data.Result.Id);
                    $row.find('.topicNm span').html(data.Result.Name);
                    $row.find('.topicNm input').hide();
                    $row.find('.topicNm .close-edit-nabi').hide();
                    $row.find('.topicNm span').show();
                    $row.closest('.nabi').find('.loadingPanel').hide();
                    $row.closest('.nabi').find('.emptyPanel').hide();


                    $button.replaceWith(prev);
                    $row.find('.topicAction .edit-nabi').show();
                    $row.find('.topicAction .save-lesson').hide();
                }

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
                $button.replaceWith(prev);
            }
        });



    });


    // Upload Tab Panel

    $('#CourseModuleHolder').on('click', '.tabs .tab-links a', function (e) {
        var $item = $(this);
        var $tab = $item.closest('.tabs');
        var currentAttrValue = $(this).attr('href').substr(1, $(this).attr('href').length - 1);
        // Show/Hide Tabs
        $tab.find('.tab').removeClass('active');
        $tab.find('.' + currentAttrValue).addClass('active');
        // Change/remove current tab to active
        $item.parent('li').addClass('active').siblings().removeClass('active');
        e.preventDefault();
    });


    /// ADD NEW LESSON CONTENT

    $('#CourseModuleHolder').on('click', '.addNewLessonContent', function () {

        var $button = $(this);
        var type = $button.attr('data-type');
        var $ContentItem = $button.closest('.nabiBd');


        /// Bind new content area
        var lessonObject = {
            Id: 0,
            IsNew: true,
            LessonContentType: type,
            IsText: type == "1" ? true : false,
            IsVideo: type == "2" ? true : false,
            IsDocument: type == "3" ? true : false,
            IsQuiz: type == "4" ? true : false,
            IsExternalContent: false
        };
        var template = document.getElementById('LessonContentTemplate').innerHTML;
        var output = Mustache.render(template, { array: [lessonObject] });
        $ContentItem.find('.action-buttons').before(output);

        var $newContent = $ContentItem.find('.action-buttons').prev();
       // BindHtmlEditor($newContent.find('textarea.lessonDesc'));
        BindHtmlEditor($newContent.find('.textWriter textarea'));
        BindHtmlEditor($newContent.find('.quiz textarea.questionText'));
        
        //$newContent.find('.quiz textarea.answerOptions').tagEditor(
        //    {

        //        beforeTagDelete: function (field, editor, tags, val) {
        //            var answers = $(editor).closest('.question').find('textarea.rightAnswer').val();
        //            if (answers.indexOf(val) > -1)
        //                $(editor).closest('.question').find('textarea.rightAnswer').tagEditor('removeTag', val, true);

        //        }
        //    }
        //);

        //$newContent.find('.quiz textarea.rightAnswer').tagEditor({
        //    beforeTagSave: function (field, editor, tags, tag, val) {
        //        var answers = $(editor).closest('.question').find('textarea.answerOptions').val();
        //        if (answers.indexOf(val) == -1)
        //            return false;

        //    }
        //});

    });


    /// ADD NEW QUIZ QUESTION

    $('#CourseModuleHolder').on('click', '.addquestion', function () {
        var $cloned = $(this).parent().prev().clone();
        var id = $cloned.attr('data-id');
        var sort = $cloned.attr('data-sort');
        $(this).closest('.qnumbering').children().removeClass('active');
        $cloned.attr('data-id', '0');
        $cloned.attr('data-sort', parseInt(sort) + 1);
        $cloned.addClass('active');
        $cloned.find('a').html(parseInt(sort) + 1);
        $(this).parent().before($cloned);

        //$(this).closest('.qnumbering').sortable({
        //    items: '.btn-group:not(.excludeSort):not([data-id="0"])',
        //    update: function (event, ui) {
        //        $('#reSortTemplate').addClass('btnBlue');
        //        $('#reSortTemplate').removeAttr('disabled');

        //        var $buttons = ui.item.closest('.qnumbering .btn-group:not([data-id="0"]):not(.excludeSort)');

        //        $buttons.each(function (i, btnItem) {
        //            $(btnItem).data('new-sort', i);
        //            $(btnItem).find('a').html(i);
        //        });

        //        $quizLesson.find('.question').each(function (i, quizItem) {
        //            var sort = $(quizItem).attr('data-sort');
        //            var $sortParent = ui.item.closest('.qnumbering .btn-group[data-sort="' + sort + '"]');
        //            var newSort = $sortParent.data('new-sort');
        //            $(quizItem).attr('data-sort', newSort);
        //            $sortParent.attr('data-sort', newSort);
        //            $sortParent.removeData('new-sort');
        //        });


        //        ui.item.closest('.qnumbering').find('.btn-group:not(.excludeSort)').each(function (i, item) {
        //            var sort = i + 1;
        //            var oldSort = $(item).attr('data-sort');
        //            $(item).closest('.quiz').find('.question[data-sort="' + oldSort + '"]').attr('data-sort', sort);
        //            $(item).find('a').html(sort);
        //        });
        //    }
        //});


        var $clonedQuestion = $(this).closest('.content').find('.question:first').clone();
        $(this).closest('.content').find('.question').hide();
        $clonedQuestion.find('textarea').val('');
        // $clonedQuestion.find('textarea:not(.questionText)').tagEditor('destroy');

        //$clonedQuestion.find('textarea.answerOptions').tagEditor(
        //    {

        //        beforeTagDelete: function (field, editor, tags, val) {
        //            var answers = $clonedQuestion.find('textarea.rightAnswer').val();
        //            if (answers.indexOf(val) > -1)
        //                $clonedQuestion.find('textarea.rightAnswer').tagEditor('removeTag', val, true);

        //        }
        //    }
        //);

        //$clonedQuestion.find('textarea.rightAnswer').tagEditor({
        //    beforeTagSave: function (field, editor, tags, tag, val) {
        //        var answers = $clonedQuestion.find('textarea.answerOptions').val();
        //        if (answers.indexOf(val) == -1)
        //            return false;

        //    }
        //});
        RemoveHtmlEditor($clonedQuestion);
        BindHtmlEditor($clonedQuestion.find('textarea.questionText'));
        $clonedQuestion.find('label.errorHolder').remove();
        $clonedQuestion.find('.optionPanel').find('.option:not(:first-child):not(:nth-child(2))').remove();
        $clonedQuestion.find('.optionPanel .option').attr('data-id', '0');
        $clonedQuestion.find('.optionPanel input:checkbox').prop("checked", false);


        $clonedQuestion.show();
        $clonedQuestion.find('textarea').val('');
        $clonedQuestion.find('input').val('');
        $clonedQuestion.find('input[name="Weight"]').val(1);
        $clonedQuestion.find('select option:first').prop('selected', true);
        $clonedQuestion.attr('data-id', '0');
        $clonedQuestion.attr('data-sort', parseInt(sort) + 1);

        $(this).closest('.content').find('.question:last').after($clonedQuestion);


    });


    //// GET QUIZ QUESTION TO DISPLAY

    $('#CourseModuleHolder').on('click', '.getquestion', function () {
        var sort = $(this).parent().attr('data-sort');
        $(this).closest('.content').find('.question').hide();
        $(this).closest('.content').find('.question[data-sort="' + sort + '"]').show();
        $(this).closest('.qnumbering').children().removeClass('active');
        $(this).parent().addClass('active');
       
    });


    //// TOGGLE QUIZ PANEL WHEN ANSWER TYPE IS CHANGED
    $('#CourseModuleHolder').on('change', '.AnswerType', function () {
        var $item = $(this);
        if ($item.val() == 'textbox') {
            $item.closest('.question').find('.answerPanel,.optionPanel').hide();
        }
        else {
            $item.closest('.question').find('.answerPanel,.optionPanel').show();
        }

        if ($item.val() == 'checkbox') {
            $item.closest('.question').find('.AnswerSelection').show();
        }
        else {
            $item.closest('.question').find('.AnswerSelection').hide();
        }

    });

    //// UN-CHECK option checkbox when not multi selection controls

    $('#CourseModuleHolder').on('change', '.settingPanel :checkbox', function () {
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



    //// DELETE QUIZ 

    $('#CourseModuleHolder').on('click', '.delete-question', function () {
        var $button = $(this);
        var sort = $button.closest('.question').attr('data-sort');
        var $panel = $button.closest('.content');
        var id = $button.closest('.question').attr('data-id');

        if ($button.closest('.content').find('.question').length == 1) {
            ShowNotie({ HasError: true, Message: "A quiz must have at least one question" });
            return;
        }

        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;


            if (id == '0') {

                $button.closest('.content').find('.qnumbering .btn-group[data-sort="' + sort + '"]').remove();
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
                    url: "/api/Course/DeleteQuiz?Id=" + id,

                    success: function (data) {

                        var $sortButton = $button.closest('.content').find('.qnumbering .btn-group[data-id="' + id + '"]');
                        var $nextButton = !$sortButton.prev().length ? $sortButton.next() : $sortButton.prev();

                        $button.closest('.content').find('.qnumbering .btn-group[data-id="' + id + '"]').remove();
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


    /// ADD QUIZ OPTIONS 

    $('#CourseModuleHolder').on('click', '#addNewOption', function () {

        var $clonedOption = $(this).closest('.optionPanel').find('.option:first').clone();
        $clonedOption.find('textarea').val('');
        $clonedOption.find('input:checkbox').prop('checked', false);
        $clonedOption.attr('data-id', '0');

        $(this).closest('.optionPanel').find('.option:last').after($clonedOption);

    });

    /// DELETE QUIZ OPTION
    $('#CourseModuleHolder').on('click', '.delete-option', function () {
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
                    url: "/api/Course/DeleteQuizOption?Id=" + id,

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

    ////SAVE LESSON CONTENT

    $('#CourseModuleHolder').on('click', '.save-quiz-content', function () {
        var $button = $(this);
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

        $button.closest('.courseSlate').find('label.error').remove();
        var lessonID = $button.closest('.courseSlate').attr('data-id');
        if (lessonID == '0') {
            ShowNotie({ HasError: true, Message: "Please save a lesson name before adding content" });
            $button.removeAttr('disabled');
            $button.html('Save Changes');
            return;
        }

        var isValid = true;
        var questionPanel = $button.closest('.question');
        var quizFormatObject = GetQuizObject(questionPanel);
        isValid = quizFormatObject.Error;
        var quizObject = quizFormatObject.Quiz;
        quizObject.lessonId = lessonID;

        if (isValid) {
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/AddorUpdateQuizQuestion",
                data: quizObject,
                success: function (data) {
                    var question = data.Result;
                    var $panel = $button.closest('.uploadBox');
                    var $panelQuestion = $button.closest('.question');
                    $panelQuestion.attr("data-id", question.Id);
                    $panel.find('.qnumbering .btn-group[data-sort="' + $panelQuestion.attr('data-sort') + '"]').attr("data-id", question.Id);
                    ShowNotie(data);
                },

                error: function (data) {
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                },
                complete: function (data) {
                    $button.removeAttr('disabled');
                    $button.html('Save Question');
                }
            });
        }

        else {
            $button.removeAttr('disabled');
            $button.html('Save Question');
        }



    });


    $('#CourseModuleHolder').on('click', '#reSortTemplate', function () {

        var $button = $(this);
        var lessonId = $button.closest('.courseSlate').attr('data-id');
        var templateObject = { Id: lessonId, Questions: [] };
        var $contentPanel = $button.closest('.uploadBox');

        var unSavedQuestions = $contentPanel.find('.qnumbering .btn-group[data-id="0"]');
        var questionList = [];
        $contentPanel.find('.qnumbering .btn-group:not([data-id="0"]):not(.excludeSort)').each(function (i, item) {
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
                url: "/api/Course/ResortQuestions",
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

    $('#CourseModuleHolder').on('click', '.save-lesson-content', function () {
        var $button = $(this);
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
        var lessonObject = new Object();
        var file = null;

        var $contentPanel = $button.closest('.manage-lesson-panel').find('.uploadBox');
        $contentPanel.closest('.courseSlate').find('label.error').remove();
        $contentPanel.closest('.courseSlate').find('.messageText').remove();

        lessonObject.Id = $contentPanel.closest('.courseSlate').attr('data-id');

        if (lessonObject.Id == '0') {
            ShowNotie({ HasError: true, Message: "Please save a lesson name before adding content" });
            $button.removeAttr('disabled');
            $button.html('Save Changes');
            return;
        }

        var isValid = true;
        //var desc = String($contentPanel.closest('.courseSlate').find('textarea.lessonDesc').val());



        //if (desc.trim() != "") {
        //    lessonObject.Description = desc;
        //}


        /// if text content validate and create object
        if ($contentPanel.hasClass("textWriter")) {
            lessonObject.isText = true;
            lessonObject.LessonContentType = 1;
            var content = $contentPanel.find('textarea').val().replace(/^\s+|\s+$/g, "");
            if (content.trim() == "") {
                $contentPanel.before('<label class="error errorHolder">This field is required.</label>');
                isValid = false;
            }
            else {
                lessonObject.HtmlContent = content;
            }
        }

        /// if media upload validate and create object
        if ($contentPanel.hasClass("mediaUpload")) {
            lessonObject.IsVideo = true;
            lessonObject.IsExternalContent = $contentPanel.find('.tabUpload').hasClass('active') ? false : true;

            if (lessonObject.IsExternalContent) {
                if (String($contentPanel.find('.tabExternal input').val()).trim() == "") {
                    $contentPanel.find('.tabExternal input').before('<label class="error errorHolder">This field is required.</label>');
                    isValid = false;
                }
                else {
                    lessonObject.ContentUrl = String($contentPanel.find('.tabExternal input').val()).trim();
                }
            }
            else {
                var videoSource = $contentPanel.find('video source').attr("src");

                if (videoSource == undefined || !videoSource.length) {

                    videoSource = $contentPanel.find('video').attr("src");
                }

                if (videoSource == undefined || !videoSource.length) {
                    $contentPanel.find('.tabUpload input').before('<label class="error errorHolder">This field is required.</label>');
                    isValid = false;
                }

                else {

                    if ($contentPanel.find('.tabUpload input').data('file') != undefined) {
                        file = $contentPanel.find('.tabUpload input').data('file');
                    }
                }
            }
        }

        /// if document upload validate and create object

        if ($contentPanel.hasClass("docUpload")) {
            lessonObject.IsDocument = true;

            var docSource = $contentPanel.find('a').attr("href");
            if (!docSource.length) {
                $contentPanel.find('input').before('<label class="error errorHolder">This field is required.</label>');
                isValid = false;
            }

            else {

                if ($contentPanel.find('input').data('file') != undefined) {
                    file = $contentPanel.find('input').data('file');
                }
            }
        }

        // if isQuiz loop through questions and validate
        if ($contentPanel.hasClass("quiz")) {
            lessonObject.IsQuiz = true;

            var retakeCount = $contentPanel.closest('.courseSlate').find('input[name="RetakeCount"]').val().trim();
            var passMark = $contentPanel.closest('.courseSlate').find('input[name="PassMark"]').val().trim();
            var questionNo = $contentPanel.closest('.courseSlate').find('input[name="QuestionNo"]').val().trim();
            var gradeQuiz = $contentPanel.closest('.courseSlate').find('input[name="IsGradeableContent"]').is(':checked');

            lessonObject.IsGradeableContent = gradeQuiz;

            if (!parseInt(retakeCount) || parseInt(retakeCount) < 1) {
                if (!parseInt(retakeCount))
                    $contentPanel.find('input[name="RetakeCount"]').before('<label class="error errorHolder">This field must be a digit.</label>');
                else
                    $contentPanel.find('input[name="RetakeCount"]').before('<label class="error errorHolder">This field must be greater than 0.</label>');

                isValid = false;
            }
            else {
                lessonObject.QuizRetakeCount = parseInt(retakeCount);
            }

            if (passMark.length) {
                if (!parseFloat(passMark)) {
                    $contentPanel.find('input[name="PassMark"]').before('<label class="error errorHolder">This field must be a decimal.</label>');
                    isValid = false;
                }
                else {
                    lessonObject.PassMark = parseFloat(passMark);
                }
            }

            if (questionNo.length) {
                if (!parseInt(questionNo)) {
                    $contentPanel.find('input[name="QuestionNo"]').before('<label class="error errorHolder">This field must be a digit.</label>');
                    isValid = false;
                }
                else {
                    lessonObject.QuestionNo = parseInt(questionNo);
                }
            }

            var quiz = [];
            $contentPanel.find('.question').each(function (i, item) {
                var quizFormatObject = GetQuizObject(item);
                isValid = quizFormatObject.Error;
                var quizObject = quizFormatObject.Quiz;

                quizObject.lessonId = lessonObject.Id;
                quiz.push(quizObject);
            });
            lessonObject.Questions = quiz;
        }

        if (!isValid) {
            ShowNotie({ HasError: true, Message: "You have some form errors. Please verify and try again" });
            $button.removeAttr('disabled');
            $button.html('Save Changes');
            return;
        }

        if (file == null) {
            SaveLessonObject(lessonObject, null, $button);
            return;
        }


        var FileChunk = [];
        if (file.chunk())
            FileChunk = ChunkFile(file.fileupload);
        else
            FileChunk.push(file.fileupload);


        var TotalParts = FileChunk.length;
        var PartCount = 0;

        $button.parent().append('<div class="messageText success"><i class="fa fa-spinner fa-spin" ></i > <p> Uploading Files <span class="nofloat">0</span>% of 100%</p> </div>');


        // loop through, pulling the first item from the array each time and sending it


        var totalFiles = FileChunk.length;
        while (chunk = FileChunk.shift()) {
            PartCount++;

            var FilePartName;

            if (file.chunk()) {
                // file name convention
                FilePartName = file.name + ".part_" + PartCount + "." + TotalParts;

            }
            else {
                FilePartName = file.name;

            }
            var FD = new FormData();
            FD.append('IsChunked', file.chunk());
            FD.append('file', chunk, FilePartName);

            var $messageText = $button.parent().find('messageText');
            // send the file

            $.ajax({
                type: 'POST',
                url: "/api/FileUpload/UploadFile",
                cache: false,
                data: FD,
                contentType: false,
                processData: false,
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowNotie({ HasError: true, Message: xhr.responseJSON });
                    $button.removeAttr('disabled');
                    $button.html('Save Changes');
                    $contentPanel.closest('.courseSlate').find('.messageText').remove();
                    return;
                },
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    //Download progress
                    xhr.addEventListener("progress", function (evt) {
                        console.log(evt.lengthComputable); // false
                        if (evt.lengthComputable) {
                            var complete = (event.loaded / event.total * 100 | 0) / ((totalFiles - PartCount) + 1);
                            $contentPanel.closest('.courseSlate').find('.messageText span').html(complete);
                        }
                    }, false);
                    return xhr;
                },
                beforeSend: function () {

                },
                complete: function () {

                },
                success: function (data) {
                    if (data.Result != null) {
                        $contentPanel.closest('.courseSlate').find('.messageText p').html("Processing your information");
                        SaveLessonObject(lessonObject, data.Result, $button);
                    }
                }
            });



        }



    });

    // HANDLE NEW FILES TO UPLOAD

    $('#CourseModuleHolder').on('change', 'input:file', function (e) {
        fileuploader(e, this);
    });


    //// STEP 2 COURSE MANAGEMENT RESORT MODULES AND LESSONS ACCORDINLY

    $('#CourseStep2').on('click', function () {
        var $button = $(this);
        var courseId = $('#CourseID').val();
        var courseObject = { Id: courseId, Modules: [] };
        var unSavedModules = 0;
        var unSavedLessons = 0;
        var moduleList = [];
        $('#CourseModuleHolder').find('.nabi').each(function (i, item) {

            var lessonObject = [];
            $(item).find('.nabiBd .courseSlate').each(function (i, item) {
                if ($(item)[0].hasAttribute("data-id")) {
                    var lessonId = $(item).attr('data-id');
                    if (parseInt(lessonId) && parseInt(lessonId) > 0) {
                        if ($(item).hasClass('changed')) {
                            lessonObject.push({ Id: lessonId, SortOrder: (i + 1) });
                        }


                    }
                    else {
                        unSavedLessons++;
                    }
                }
            });

            var moduleId = $(item).attr('data-id');
            if (parseInt(moduleId) && parseInt(moduleId) > 0) {
                if ($(item).hasClass('changed') || lessonObject.length > 0) {
                    moduleList.push({
                        Id: $(item).attr('data-id'),
                        SortOrder: (i + 1),
                        Lessons: lessonObject
                    });
                }
            }
            else {
                unSavedModules++;
            }

        });
        courseObject.Modules = moduleList;

        if (moduleList.length == 0) {
            $('.formArea2').fadeOut();
            setTimeout(function () {
                $('.formArea3').show();
                $('.progress1 .tabloid:nth-child(3)').addClass('active');
                $('.progressIndicator1').css("width", "75%");

            }, 500);
            return;
        }

        var confirmHtml = '<div class="nofloat customError"><span class="title">All re-sorted modules and lessons will be updated. Are you sure you want to proceed?</span>';

        if (unSavedLessons > 0 || unSavedModules > 0) {
            confirmHtml = confirmHtml + '<div><h4>Warning</h4>';
            if (unSavedModules > 0) {
                confirmHtml = confirmHtml + '<span>You have ' + unSavedModules + ' unsaved modules. They will be deleted if you proceed </span>'
            }

            if (unSavedLessons > 0) {
                confirmHtml = confirmHtml + '<span> You have ' + unSavedLessons + ' unsaved lessons. They will be deleted if you proceed </span>'
            }
            confirmHtml = confirmHtml + "</div>";
        }
        confirmHtml = confirmHtml + "</div>";



        var $confirm = ShowConfirm(confirmHtml);
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;



            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');
            console.log(courseObject);

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Course/ResortCourseItems",
                data: courseObject,
                success: function (data) {
                    $('.formArea2').fadeOut();
                    setTimeout(function () {
                        $('.formArea3').show();
                        $('.progress1 .tabloid:nth-child(3)').addClass('active');
                        $('.progressIndicator1').css("width", "75%");

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
        $('.progressIndicator1').css("width", "" + ((step - 1) * 25) + "%");
        setTimeout(function () {
            $area.prev().fadeIn();

        }, 500);



    });

});

function GetQuizObject(item) {
    isValid = true;
    var quizObject = new Object();

    var quizname = $(item).find('.questionPanel textarea.questionText').val();
    var answerType = $(item).find('select.AnswerType').val();
    var answerTypeId = GetQuestionAnswerType(answerType);
    var answerLogic = $(item).find('select.AnswerType').val() == "checkbox" ? $(item).find('select.AnswerSelection').val() : 0;
    var weight = $(item).find('input[name="Weight"]').val();


    //var answers = String($(item).find('textarea.answerOptions').tagEditor('getTags')[0].tags).split(',');
    //var rightAnswer = String($(item).find('textarea.rightAnswer').tagEditor('getTags')[0].tags).split(',');

    if (quizname.trim() == '') {
        $(item).find('.questionPanel label').after('<label class="error errorHolder">This field is required.</label>');
        isValid = false;
    }

    if (!parseInt(weight)) {
        $(item).find('input[name="Weight"]').before('<label class="error errorHolder">This field must be a digit.</label>');
        isValid = false;
    }
    else {
        quizObject.Weight = parseInt(weight);
    }




    quizObject.Question = quizname;
    quizObject.Id = $(item).attr('data-id');
    quizObject.SortOrder = $(item).attr('data-sort');
    // quizObject.SortChanged = $(item).hasClass('changed');
    quizObject.AnswerType = answerTypeId;
    quizObject.IsMultipleChoice = parseInt(answerLogic) == 0 ? false : true;

    /// if not textarea answer validate options and also if answer is present and bind
    if (answerType != 'textbox') {


        if ($(item).find('.optionPanel .option').length < 2) {
            $(item).find('.optionPanel label').after('<label class="error errorHolder block">You must add at least two options.</label>');
            isValid = false;
        }

        if (answerType != "checkbox") {
            if ($(item).find('.optionPanel .settingPanel :checkbox:checked').length > 1) {
                $(item).find('.optionPanel label').after('<label class="error errorHolder block">This answer type does not permit multiple answers.</label>');
                isValid = false;
            }
        }

        var options = [];

        $(item).find('.optionPanel .option').each(function (i, optionItem) {

            var optionTitle = $(optionItem).find('.questionPanel textarea').val();
            var rightAnswer = $(optionItem).find('.settingPanel input').is(':checked');

            if (optionTitle.trim() == '') {
                $(optionItem).find('.questionPanel').prepend('<label class="error errorHolder">This field is required.</label>');
                isValid = false;
            }

            var optionObject = new Object();
            optionObject.Title = optionTitle;
            optionObject.Id = $(optionItem).attr('data-id');
            optionObject.IsAnswer = rightAnswer;
            optionObject.Value = i + 1;
            options.push(optionObject);
        });
        quizObject.Options = options;
    }

    return {
        Error: isValid, Quiz: quizObject
    };


    //if (answerType != 1) {
    //    if (!rightAnswer.length) {

    //        $(item).find('.answerPanel label').after('<label class="error errorHolder block">You must add at least one of your options as an answer.</label>');
    //        isValid = false;
    //    }

    //    if (!answers.length) {
    //        $(item).find('.optionPanel label').after('<label class="error errorHolder block">You must add at least one option.</label>');
    //        isValid = false;
    //    }
    //    else {

    //        if (rightAnswer.length > 0) {
    //            var optionsObject = []
    //            for (var i = 0; i < answers.length; i++) {
    //                optionsObject.push({
    //                    Title: answers[i],
    //                    Value: i + 1,
    //                    IsAnswer: (rightAnswer.indexOf(answers[i]) > -1) ? true : false,
    //                    QuizId: quizObject.Id
    //                });
    //            }
    //            quizObject.Options = optionsObject;
    //        }

    //    }



    //}

}

function SaveLessonObject(lessonObject, filePath, $item) {
    var $panel = $item.closest('.manage-lesson-panel');
    if (filePath != null) {
        lessonObject.ContentUrl = filePath;
    }

    $.ajax({
        cache: false,
        async: true,
        type: "POST",
        url: "/api/Course/AddorUpdateLessonContent",
        data: lessonObject,
        success: function (data) {
            if ($panel.find('.docUpload').length) {
                $panel.find('.docUpload a').html(data.Result.Name);
            }

            if (data.Result.Questions.length > 0) {
                data.Result.Questions.forEach(function (question) {
                    $panel.find('.question[data-sort="' + question.SortOrder + '"]').attr("data-id", question.Id);
                    $panel.find('.qnumbering .btn-group[data-sort="' + question.SortOrder + '"]').attr("data-id", question.Id);
                });

            }

            ShowNotie(data);
        },

        error: function (data) {
            var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
            ShowNotie({ HasError: true, Message: message });
        },
        complete: function (data) {
            $item.removeAttr('disabled');
            $item.html('Save Changes');
            $item.parent().find('.messageText').remove();
        }
    });
}


function fileuploader(evt, elem) {
    evt.stopPropagation();
    evt.preventDefault();

    selectedFiles = evt.target.files || evt.dataTransfer.files;
    if (selectedFiles) {

        for (var i = 0; i < selectedFiles.length; i++) {

            DataURLFileReader.read(selectedFiles[i], i, function (err, fileInfo) {

                if (err != null) {
                    ShowNotie({ HasError: true, Message: err });
                }
                else {

                    if ($(elem).closest('.uploadBox').hasClass('mediaUpload')) {
                        if (fileInfo.type == "video") {
                            var $video = $(elem).closest('.uploadBox').find('video');
                            var fileUrl = window.URL.createObjectURL(fileInfo.fileupload);
                            $video.attr("src", fileUrl);
                            $video.removeClass('empty');
                            $(elem).data('file', fileInfo);
                        }
                        else {
                            $(elem).val("");
                            ShowNotie({ HasError: true, Message: "File type not supported" });
                        }
                    }

                    if ($(elem).closest('.uploadBox').hasClass('docUpload')) {
                        if (fileInfo.type != "video") {
                            var $file = $(elem).closest('.uploadBox').find('a');
                            var fileUrl = window.URL.createObjectURL(fileInfo.fileupload);
                            $file.attr("href", fileUrl);
                            $file.html("New File");
                            $(elem).data('file', fileInfo);
                        }
                        else {
                            $(elem).val("");
                            ShowNotie({ HasError: true, Message: "File type not supported" });
                        }
                    }



                }
            });
        }


    }
}

