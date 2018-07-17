var htmlTagsTemplate = '<span data-id="0" data-AssoId="{TagValue}" data-type="{TypeId}">' +
    '<i class="fa fa-times-circle-o removeTag"></i><small>{TagName}</small>' +
    '</span>';

var htmlTableTagsTemplate = '<span data-id="0" data-AssoId="{TagValue}" data-type="{TypeId}">' +
    '<small>{TagName}</small>' +
    '</span>';



$(document).ready(function () {

    var jqTable = $('#GroupTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/LearningGroup/GetDatatableLearningGroups',
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
            {
                bSortable: false,
                className: "learningGroupTagPanel",
                "render": function (d, i, data) {
                    var tags = JSON.parse(data.TagFormat);
                    var htmlString = "";
                    tags.forEach(function (tag) {
                        htmlString = htmlString + htmlTableTagsTemplate.replace('{TagValue}', tag.TagValue).replace('{TagName}', tag.Name).replace('{TypeId}', tag.TagType);

                    });
                    return htmlString;
                }
            },

            { data: "CourseCount" },
            { data: "TrainingCount" },
            { data: "ExamCount" },
            { data: "SurveyCount" },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {

                    var deleteButton = '';
                    if (data.CourseCount == 0 && data.TrainingCount == 0 && data.ExamCount == 0 && data.SurveyCount == 0)
                        deleteButton = ' <a class="dropdown-item table-delete red"> <i class="fa fa-trash table-icon" style=""></i> Delete</a>';

                    var dropdown = '<div class="dropdown">' +
                        '<button class="btn btnSmaller  btnBlue dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                        ' Actions <i class="fa fa-caret-down"></i>' +
                        '</button>' +
                        '<div class="dropdown-menu pull-right" aria-labelledby="dropdownMenuButton" > ' +
                        ' <a class="table-edit dropdown-item blue"' + data.Id + '"><i class="fa fa-edit table-icon" style=""></i>  Edit</a > ' +
                        ' <a class="table-users dropdown-item blue"' + data.Id + '"><i class="fa fa-user table-icon" style=""></i>  View Users</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/ManageCourse/Assign/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Assign Course</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/ManageTraining/AssignTraining/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Assign Training</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/Examination/Assign/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Assign Exam</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/Survey/Assign/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Assign Survey</a > ' + deleteButton
                        + ' </div> '
                        + ' </div>'

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

    $('#toggleSelect1').select2();

    $('#mainSelect').on('change', function (e, selectedId) {

        $('#toggleSelect1').removeAttr('data-next');
        $('#toggleSelect1').removeAttr('data-next-type');
        $('#toggleSelect2').attr('disabled', 'disabled');
        $('#toggleSelect2').closest('.toggleSelectPanel').hide();

        var selected = $(this).val();
        if (selected == "0" && selectedId == undefined)
            return;

        if (selected == "indv") {
            $('#UserSearchPanel').show();
            $('#SelectPanel').hide();
            $('#addLearningGroup').hide();
            return;
        }
        else if (selected == "all" || selected == "inactive") {

            return;
        }
        else {
            $('#UserSearchPanel').hide();
            $('#SelectPanel').show();
            $('#addLearningGroup').show();
        }


        var url = ''
        var dummyClass = '';
        var bindClass = '';
        var emptyoption = '';
        switch (selected) {
            case "group":
                url = '/api/organization/GetGroups';
                dummyClass = 'dummyGroup';
                emptyoption = 'Select a group';
                break;
            case "gender": url = '';
                dummyClass = 'dummyGender';
                break;
            case "grade":
                url = '/api/organization/GetGrades';
                dummyClass = 'dummyGrade';
                emptyoption = 'Select a grade';
                break;
            case "role":
                url = '/api/organization/GetRoles';
                dummyClass = 'dummyRole';
                emptyoption = 'Select a role';
                break;
            case "indv":
                url = '/api/Employee/GetActiveEmployees';
                dummyClass = 'dummyIndv';
                emptyoption = 'Select individuals';
                break;
            case "region":
                url = '/api/organization/GetRegion';
                dummyClass = 'dummyRegion';
                emptyoption = 'Select a region';
                break;
            case "learner":
                url = '/api/LearningGroup/GetLearnerGroup';
                dummyClass = 'dummyLearnerGroup';
                emptyoption = 'Select a learner group';
                break;

        }


        // if fetched bind from page else get
        if ($('.' + dummyClass).find('option').length > 0) {
            $('#toggleSelect1').html($('.' + dummyClass).html());


            $('#toggleSelect1').removeAttr('disabled');
            $('#toggleSelect1').select2();
            $('#toggleSelect1').attr('data-type', $('.' + dummyClass).attr('data-type'));
            if (selected == 'learner')
                bindSavedLearnerTags($('.' + dummyClass));

        }
        else {
            $.get(url, function (data) {
                $('.' + dummyClass).html('<option value="0">' + emptyoption + '</option>');
                data.Result.forEach(function (option) {

                    if (selected == 'indv') {
                        $('.' + dummyClass).append('<option value="' + option.Id + '">' + option.FirstName + ' ' + option.LastName + '</option>');
                    }
                    else {
                        $('.' + dummyClass).append('<option value="' + option.Id + '">' + option.Name + '</option>');
                    }

                    if (selected == 'learner') {

                        $('.' + dummyClass).find('option:last').data('tag', JSON.parse(option.TagFormat));
                    }

                });

                $('#toggleSelect1').html($('.' + dummyClass).html());
                if (selected == 'learner')
                    bindSavedLearnerTags($('.' + dummyClass));

                if (selected == 'learner' && selectedId != undefined) {
                    $('.dummyLearnerGroup').removeAttr('data-selected');
                    $('#toggleSelect1 option[value="' + selectedId + '"]').prop('selected', true);
                    $('#addLearningGroup').click();
                }


                $('#toggleSelect1').removeAttr('disabled');
                $('#toggleSelect1').select2();
                $('#toggleSelect1').attr('data-type', $('.' + dummyClass).attr('data-type'));

            });

        }




        if ($('.' + dummyClass)[0].hasAttribute('data-next')) {
            $('#toggleSelect1').attr('data-next', $('.' + dummyClass).attr('data-next'));
            $('#toggleSelect1').attr('data-next-type', $('.' + dummyClass).attr('data-next-type'));
        }


    });


    // if available select extra filter option : right now works for Group Departments

    $('#toggleSelect1').on('change', function () {

        var $elem = $(this);

        if ($elem[0].hasAttribute("data-next")) {
            $('#toggleSelect2').html('<option value="0">Select a ' + $elem.attr('data-next') + '</option>');
            var group = { Id: $elem.val() };
            $.get('/api/organization/GetDepartmentByGroupId', group, function (data) {

                data.Result.forEach(function (option) {
                    $('#toggleSelect2').append('<option value="' + option.Id + '">' + option.Name + '</option>');
                });
            });
            $('#toggleSelect2').removeAttr('disabled');
            $('#toggleSelect2').attr('data-showing', $elem.attr('data-next'));
            $('#toggleSelect2').attr('data-type', $elem.attr('data-next-type'));
            $('#toggleSelect2').select2({ 'width': '100%' });
            $('#toggleSelect2').closest('.toggleSelectPanel').show();
        }


    });


    //// serach for user

    $('#searchUser').on('click', function () {
        /// add serach and populate next togglepanel with users from search
    });

    /// add new learning group tags

    $('#addLearningGroup').on('click', function () {


        $('.learnerInfo').hide();
        if ($('#mainSelect').val() == "0") {
            ShowNotie({ HasError: true, Message: "Please select a type" });
            return;
        }

        if ($('#toggleSelect1').val() == "0") {
            return ShowNotie({ HasError: true, Message: "Please select a " + $('#mainSelect').val() });
        }

        if ($('#mainSelect').val() == "all") {
            /// add all tag
        }
        else if ($('#mainSelect').val() == "inactive") {
            // add in active tag
        }
        else if ($('#mainSelect').val() == "learner") {

             //// if learner group add from data tags in dom not from text 
            var tagsObject = $('#toggleSelect1 option[value="' + $('#toggleSelect1').val() + '"]').data('tag');

            /// if no tags added set learning group Id for possible assignment using old saved group
            if (!$('#learningGroupTagPanel span').length) {
                $('#LearningGroupId').val($('#toggleSelect1').val());
                $('.learnerInfo').show();
            }


            tagsObject.forEach(function (tag) {

                if (!$('#learningGroupTagPanel span[data-assoId="' + tag.TagValue + '"][data-type="' + tag.TagType + '"]').length) {

                    /// if a gender exists don't add multiple
                    if (tag.TagType == 2 && $('#learningGroupTagPanel span[data-type="' + tag.TagType + '"]').length > 0) {
                        return;
                    }
                    else {
                        var htmlString = htmlTagsTemplate.replace('{TagValue}', tag.TagValue).replace('{TagName}', tag.Name).replace('{TypeId}', tag.TagType);
                        $('#learningGroupTagPanel .saveBox').before(htmlString);
                        //if ($('#LearningGroupId').val() != "0" && $('#LearningGroupId').val() != $('#toggleSelect1').val()) {
                        //    $('#LearningGroupId').val("0");
                        //}
                    }


                }

            });

            $('#saveLearningGroup').html('Update learning group');
        }
        else {

            var tag1 = $('#mainSelect option[value="' + $('#mainSelect').val() + '"]').text();
            var tag2 = $('#toggleSelect1 option[value="' + $('#toggleSelect1').val() + '"]').text().trim().replace(/\s/g, "_");
            var tagType = $('#toggleSelect1').attr('data-type');
            var tagAssoId = $('#toggleSelect1').val();


            ////if gender dont allow multiple 
            if ($('#mainSelect').val() == "gender") {
                if ($('#learningGroupTagPanel span[data-type="' + tagType + '"]').length > 0) {
                    return ShowNotie({ HasError: true, Message: "Gender has been added" });
                }
            }

            var parentTag1 = String(tag1 + "_" + tag2).trim();

            if (!$('#learningGroupTagPanel span[data-assoId="' + tagAssoId + '"][data-type="' + tagType + '"]').length) {
                var htmlString = htmlTagsTemplate.replace('{TagValue}', tagAssoId).replace('{TagName}', parentTag1).replace('{TypeId}', tagType);
                $('#learningGroupTagPanel .saveBox').before(htmlString);
                /// set always for fresh learner group set
                // $('#LearningGroupId').val('0');
            }



            if ($('#toggleSelect2').parent().is(':visible') && $('#toggleSelect2').val() != "0") {
                var tag1 = $('#toggleSelect2').attr('data-showing');
                upperCaseStart = tag1.substr(0, 1).toUpperCase();
                tag1 = upperCaseStart + tag1.substr(1, tag1.length - 1);
                var tag2 = $('#toggleSelect2 option[value="' + $('#toggleSelect2').val() + '"]').text().trim().replace(/\s/g, "_");
                var tagType = $('#toggleSelect2').attr('data-type');
                var tagAssoId = $('#toggleSelect2').val();

                var parentTag1 = String(tag1 + "_" + tag2).trim();

                if (!$('#learningGroupTagPanel span[data-assoId="' + tagAssoId + '"][data-type="' + tagType + '"]').length) {
                    var htmlString = htmlTagsTemplate.replace('{TagValue}', tagAssoId).replace('{TagName}', parentTag1).replace('{TypeId}', tagType);
                    $('#learningGroupTagPanel .saveBox').before(htmlString);
                    /// set always for fresh learner group set
                    // $('#LearningGroupId').val('0');
                }

            }
        }

        $('#learningGroupTagPanel').show();
        clearLearningPanel();
    });


    /// Save a learning group 

    $('#saveLearningGroup').on('click', function () {
        $('.saveBox .messageText').remove();
        var $button = $(this);
        var prev = $button.html();
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');
        $button.attr('disabled', 'disabled');
        var groupName = $('#LearningGroupName').val().trim();

        if (groupName.length == 0) {
            var output = LoadPageError({ HasError: true, Message: "Learning group name is required" });
            $('.saveBox').append(output);
            $button.html(prev);
            $button.removeAttr('disabled');
            return;
        }

        if ($('#learningGroupTagPanel span').length == 0) {
            var output = LoadPageError({ HasError: true, Message: "You must add at least one tag" });
            $('.saveBox').append(output);
            $button.html(prev);
            $button.removeAttr('disabled');
            return;
        }

        var tagArray = [];
        $('#learningGroupTagPanel span').each(function (i, item) {
            var tagObj = { Id: 0, Name: $(item).find('small').text().trim(), TagType: parseInt($(item).attr('data-type')), TagValue: parseInt($(item).attr('data-AssoId')) };
            tagArray.push(tagObj);
        });

        var group = { Id: $('#LearningGroupId').val(), Name: groupName, Tags: tagArray }

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/LearningGroup/SaveLearnerGroup",
            data: group,
            success: function (data) {
                $('#LearningGroupId').val('0');
                ShowNotie(data);
                $('#LearningGroupName').val('');
                var newOption = '<option value="' + data.Result.Id + '">' + data.Result.Name + '</option>';
                $('#dummyLearnerGroup option').length ? $('#dummyLearnerGroup').append(newOption) : $('#dummyLearnerGroup').html(newOption);
                clearLearningPanel();
                jqTable.ajax.reload();

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
            },
            complete: function (data) {
                $button.removeAttr('disabled');
                $button.html('Save as new learner group');
            }
        });



    });



    /// remove Tag

    $('#learningGroupTagPanel').on('click', '.removeTag', function () {
        var $elem = $(this);
        if ($elem.closest('span').attr('data-id') == "0") {
            $elem.closest('span').remove();
        }
    });


    $('#showLearningGroupUsers').on('click', function () {
        var $button = $(this);
        var prev = $button.html();
        $button.attr("disabled", "disabled");
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');

        var tagArray = [];
        $('#learningGroupTagPanel span').each(function (i, item) {
            var tagObj = { Id: 0, Name: $(item).find('small').text().trim(), TagType: parseInt($(item).attr('data-type')), TagValue: parseInt($(item).attr('data-AssoId')) };
            tagArray.push(tagObj);
        });

        if (!tagArray.length) {
            ShowNotie({ HasError: true, Message: "You must add at least one tag" });
            return;
        }

        if ($.fn.DataTable.isDataTable('#table_users')) {
            $('#table_users').DataTable().destroy();
        }

        $('#table_users tbody').empty();
        $('#table_users').data("tags", tagArray);

        $('#table_users').DataTable({
            dom: '<"tanHead tanHP"f>rt<"counter"lip>',
            serverSide: true,
            ajax: {
                "url": '/api/LearningGroup/GetLearningGroupUsers',
                "data": function (d) {
                    d.AdditionalParameters = JSON.stringify($('#table_users').data("tags"));
                },
                "type": "GET"
            },
            'language': {
                'search': '',
                'searchPlaceholder': 'Search...'
            },
            "fnDrawCallback": function (oSettings) {
                console.log(oSettings.json.recordsTotal);
                $('#LearningGroupUserCount').html(oSettings.json.recordsTotal);
                $('#modalUsers').modal('show');

            },
            "order": [],
            columns: [
                { data: "FirstName" },
                { data: "LastName" },
                { data: "Department" },
                { data: "Branch" },
                { data: "Location" },
                { data: "Grade" },
                { data: "Sex" }
            ]
        });

        $button.html(prev);
        $button.removeAttr("disabled");

    });


    /// Delete Learning Group

    $("#GroupTable tbody").on("click", "td.action-buttons a.table-delete", function () {
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
                url: "/api/LearningGroup/DeleteLearningGroup?Id=" + id,

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


    /// Edit Learning Group

    $("#GroupTable tbody").on("click", "td.action-buttons a.table-edit", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $('#mainSelect').val("learner").trigger('change', id);
        $('#saveLearningGroup').html('Update learning group');

    });


    /// View table users for Learning Group

    $("#GroupTable tbody").on("click", "td.action-buttons a.table-users", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }


        if ($.fn.DataTable.isDataTable('#table_users')) {
            $('#table_users').DataTable().destroy();
        }

        $('#table_users tbody').empty();

        $('#table_users').DataTable({
            dom: '<"tanHead tanHP"f>rt<"counter"lip>',
            serverSide: true,
            ajax: {
                "url": '/api/LearningGroup/GetLearningGroupUsersById',
                "data": function (d) {
                    d.AdditionalParameters = id;
                },
                "type": "GET"
            },
            'language': {
                'search': '',
                'searchPlaceholder': 'Search...'
            },
            "fnDrawCallback": function (oSettings) {
                console.log(oSettings.json.recordsTotal);
                $('#LearningGroupUserCount').html(oSettings.json.recordsTotal);
                $('#modalUsers').modal('show');

            },
            "order": [],
            columns: [
                { data: "FirstName" },
                { data: "LastName" },
                { data: "Department" },
                { data: "Branch" },
                { data: "Region" },
                { data: "Grade" },
                { data: "Sex" }
            ]
        });

    });


});

function clearLearningPanel() {
    $('#mainSelect option:first').prop('selected', true);
    $('#toggleSelect1 option:first').prop('selected', true);
    $('#toggleSelect1').select2();
    $('#toggleSelect2').html('');
    $('#toggleSelect2').parent().hide();

    $('#toggleSelect1').html('<option value="0">Choose a selection type</option>');
    $('#toggleSelect1').attr('disabled', 'disbaled');
    $('#toggleSelect1').select2();
    $('#toggleSelect1').removeAttr('data-type');
    $('#toggleSelect1').removeAttr('data-next');
    $('#toggleSelect1').removeAttr('data-next-type');

}

function bindSavedLearnerTags($elem) {

    $('#toggleSelect1 option').each(function (i, item) {
        var val = $(item).attr('value');
        var tagObj = $elem.find(' option[value="' + val + '"]').data('tag');
        $(item).data('tag', tagObj);
    });



}

