
function clearForm() {
    $('#form1')[0].reset();
    $('#Id').val('0');
    $('#PeriodPanel').find('.period-row').remove();
    $('#StartPeriod').datepicker("update", new Date());
    $('#EndPeriod').datepicker("update", new Date());
    $('.viewManagePanel').click();
}
var daysOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

function dayOfWeekAsInteger(day) {
    return daysOfWeek[day];
}


$(document).ready(function () {
    $('.datepicker').datepicker({ format: "dd/mm/yyyy" })
        .on('changeDate', function (e) {
            var startdate = $('#StartPeriod').val();
            var enddate = $('#EndPeriod').val();
            if (startdate === enddate) {
             $('#PeriodPanel').find('.period-row div.col-sm-6:first-child select').attr('disabled','disabled');
            }
            else {
                $('#PeriodPanel').find('.period-row div.col-sm-6:first-child select').removeAttr('disabled');
            }
    });

    $('.addNew').on('click', function () {
        clearForm();
    });


    $.get('/PartialViews/Admin/TrainingPeriodTemplate.html', function (template) {
        $('#PartialTemplates').append(template);
    });


    $('.select2').select2({
        placeholder: "Please select"
    });

    $('.timepicker').timepicker({
        minuteStep: 1,
        showSeconds: false,
        showMeridian: true,
        format: 'g:i A'
    }).next().on('click', function () {
        $(this).prev().focus();
    });

    var jqTable = $('#TrainingTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        "autoWidth": false,
        processing: true,
        serverSide: true,
        ajax: {
            "url": '/api/ManageTraining/GetTrainingTable',
            "type": "GET"
        },
        'language': {
            'search': '',
            'searchPlaceholder': 'Search...'
        },
        "fnDrawCallback": function (oSettings) {
        },
        "order": [],
        columns: [
            { data: "Name" },
            { data: "TrainingTypeName" },
            { data: "TrainingCategoryName" },
            {
                "render": function (d, i, data) {
                    if (data.StartDateFormat == null && data.EndDateFormat == null) {
                        return "N/A"
                    }
                    else if (data.StartDateFormat != null && data.EndDateFormat == null) {
                        return "NStarts: " + data.StartDateFormat;
                    }
                    else {
                        return data.StartDateFormat + " to " + data.EndDateFormat;
                    }
                   
                }
            },
            { data: "LocationPlace" },
            {
                "render": function (d, i, data) {
                    if (data.PeriodFormat === null)
                        return 'N/A';

                    var period = JSON.parse(data.PeriodFormat);
                    var html = '';
                    period.forEach(function (item) {
                        html = html + '<label class="label label-primary period-label">' + item.DayName + ': ' + item.StartTime + ' to ' + item.EndTime + '</label>';
                    });
                    return html;
                }
            },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {
                    var dropdown = '<div class="dropdown">' +
                        '<button class="btn btnSmaller  btnBlue dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                        ' Actions <i class="fa fa-caret-down"></i>' +
                        '</button>' +
                        '<div class="dropdown-menu pull-right" aria-labelledby="dropdownMenuButton" > ' +
                        ' <a class="table-view dropdown-item blue"' + data.Id + '"><i class="fa fa-edit table-icon" ></i>  View/Edit</a > ' +
                        ' <a class="dropdown-item table-add green" href="/Admin/Survey/Training/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Add Survey</a > ' +
                        ' <a class="table-delete dropdown-item red"' + data.Id + '"><i class="fa fa-times table-icon" ></i>  Delete</a > ' +
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

    //View and update

    $("#TrainingTable tbody").on("click", "td.action-buttons a.table-view", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $button.addClass('disabled');
        $button.find('i').removeClass('fa-edit');
        $button.find('i').addClass('fa-circle-o-notch fa-spin');

        $.ajax({
            cache: false,
            async: true,
            type: "Get",
            url: "/api/ManageTraining/GetTraining",
            data: { Id: id },
            success: function (data) {

                // Bind Training Form

                var startdate = data.Result.StartDateFormat;
                var enddate = data.Result.EndDateFormat;
                $('#Name').val(data.Result.Name);
                $('#Vendor').val(data.Result.Vendor);
                $('#TrainingCategory').find('option[value="' + data.Result.TrainingCategory + '"]').prop('selected', true);
                $('#TrainingType').find('option[value="' + data.Result.TrainingType + '"]').prop('selected', true);

                $('#BudgetPerStaff').val(data.Result.BudgetPerStaff);
                $('#Budget').val(data.Result.Budget);
               

                $('#Location').val(data.Result.Location);

                $('#StartPeriod').val(startdate);
                $('#EndDate').val(enddate);



                $('#StartPeriod').datepicker("update", startdate);
                $('#EndPeriod').datepicker("update", enddate);
                $('#Venue').val(data.Result.Venue);
                $('.changetext').html("View/Update Training");
                $("#OtherFees").val(data.Result.Budget - data.Result.BudgetPerStaff);
                $('#Id').val(data.Result.Id);
                $('.content1').css("background", "#ffffff");
                $('#PeriodPanel').find('.period-row').remove();
                var period = data.Result.TrainingPeriod;

                if (period.length > 0) {
                    var template = document.getElementById('TrainingPeriodTemplate').innerHTML;
                    var output = Mustache.render(template, { array: period });
                    $('.addquestion').closest('.row').before(output);

                    period.forEach(function (item) {
                        $('#PeriodPanel').find('.period-row[data-id="' + item.Id + '"]').find('select option[value=' + item.Day + ']').prop('selected', true);

                        $('#PeriodPanel').find('select').select2({
                            placeholder: "Please select",
                            'width': '100%'
                        });

                        $('#PeriodPanel').find('.timepicker').timepicker({
                            minuteStep: 1,
                            showSeconds: false,
                            showMeridian: true,
                            format: 'g:i A'
                        }).next().on('click', function () {
                            $(this).prev().focus();
                        });

                    });

                    $('#PeriodPanel').find('.period-row').data('saved', true);

                    if (startdate === enddate) {
                        $('#PeriodPanel').find('.period-row div.col-sm-6:first-child select').attr('disabled', 'disabled');
                    }
                    else {
                        $('#PeriodPanel').find('.period-row div.col-sm-6:first-child select').removeAttr('disabled');
                    }
                }

                $button.removeClass('disabled');
                $button.html(prev);
                $('#btnCalcHours').click();
                $('.viewManagePanel').click();
            },

            error: function (data) {
                var output = LoadPageError({ HasError: true, Message: data.responseJSON.Message });
                $('.boxBody1').prepend(output);
                $button.removeClass('disabled');
                $button.html(prev);

            }
        });


    });

    $("#TrainingTable tbody").on("click", "td.action-buttons a.table-delete", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        if (confirm("Are you sure you want to delete this training?")) {
            $button.addClass('disabled');
            $button.find('i').removeClass('fa-times');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "Delete",
                url: "/api/ManageTraining/DeleteTraining?Id=" + id,

                success: function (data) {
                    jqTable.row(tr).remove().draw(false);
                    ShowNotie(data);
                },

                error: function (data) {
                    ShowNotie({ HasError: true, Message: data.responseJSON });
                    $button.removeClass('disabled');
                    $button.html(prev);
                }
            });
        }
    });

    ////Append date pickers in date fields
    //$('#StartPeriod').datepicker({
    //    yearRange: "-119:+0",
    //    changeMonth: true,
    //    changeYear: true,
    //    dateFormat: 'dd-MM-yy'

    //});
    //$('#EndDate').datepicker({
    //    yearRange: "-119:+0",
    //    changeMonth: true,
    //    changeYear: true,
    //    dateFormat: 'dd-MM-yy'

    //});

    $('.addquestion').on('click', function () {
        var $item = $(this);
        var template = document.getElementById('TrainingPeriodTemplate').innerHTML;

        var startdate = $('#StartPeriod').val();
        var enddate = $('#EndPeriod').val();

        
        var output = Mustache.render(template, { array: [] });
        $item.closest('.row').before(output);
        var $newItem = $item.closest('.row').prev();
        $newItem.find('select').select2({
            placeholder: "Please select"
        });

        $newItem.find('.timepicker').timepicker({
            minuteStep: 1,
            showSeconds: false,
            showMeridian: true,
            format: 'g:i A'
        }).next().on('click', function () {
            $(this).prev().focus();
            });

        if (startdate === enddate) {
            $item.closest('#PeriodPanel').find('.period-row div.col-sm-6:first-child select').attr('disabled', 'disabled');
        }
        

    });

    $('#PeriodPanel').on('click', '.deletePeriod', function () {
        var $button = $(this);
        var $row = $(this).closest('.period-row');
        if ($row.data('saved') === undefined || $(this).closest('.period-row').data('saved') === null) {
            $row.remove();
        }
        else {
            var Id = $row.attr('data-id');
            var trainingId = $('#Id').val();
            if (!parseInt(Id) && parseInt(Id) <= 0 && !parseInt(trainingId) && parseInt(trainingId) <= 0) {
                return;
            }

            var $confirm = ShowConfirm();
            $confirm.onAction = function (btnName) {

                if (btnName === "cancel")
                    return;

                var prev = $button[0].outerHTML;
                $button.addClass('disabled');
                $button.find('i').removeClass('fa-trash');
                $button.find('i').addClass('fa-circle-o-notch fa-spin');

                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/ManageTraining/DeletePeriod?Id=" + Id + "&TrainingId=" + trainingId,

                    success: function (data) {
                        $row.remove();
                    },

                    error: function (data) {
                        ShowNotie({ HasError: true, Message: data.responseJSON.Message });
                        $button.replaceWith(prev);
                    }
                });

            }
            return false;


        }
    });



    //disable letter inputs, allowing only number inputs
    $("#BudgetPerStaff,#Budget,#OtherFees").keydown(function (event) {

        if (event.shiftKey === true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode === 8 || event.keyCode === 9 || event.keyCode === 37 ||
            event.keyCode === 39 || event.keyCode === 46 || event.keyCode === 190) {

        } else {
            event.preventDefault();
        }

        if ($(this).val().indexOf('.') !== -1 && event.keyCode === 190)
            event.preventDefault();
    });


    //Get total fee cost

    $('input[name="BudgetPerStaff"],input[name="OtherFees"]').on('input propertychange paste', function () {
        var budget = $("#form1").find('input[name="BudgetPerStaff"]').val();
        var others = $("#form1").find('input[name="OtherFees"]').val();

        var b = parseFloat(budget);
        var o = parseFloat(others);

        var val = 0;

        if (b) {
            val += b;
        }
        if (o) {
            val += o;
        }

        $("#Budget").val(val);

    });



    var formValidator = $("#form1").validate({
        ignore: [],
        message: {
            Name: {
                required: "Please enter the training name",
            }

        },
        rules: {
            Name: { required: true },
            //Location: { required: true },
            //Venue: { required: true },
            //Vendor: { required: true },
            //OtherFees: { required: true },
            TrainingCategory: "dropdown",
            TrainingType: 'dropdown',
            StartDateFormat: {
                DateGreaterOrEqual: {
                    param: 'currentDate',
                    depends: function (element) {
                        return $(element).val().trim().length > 0 ? true : false
                    }
                }
            },

            EndDateFormat: {
                DateGreaterOrEqual: {
                    param: '#StartPeriod',
                    depends: function (element) {
                        return ($('#StartPeriod').val().trim().length > 0 || $(element).val().trim().length > 0) ? true : false
                    }
                }
            },
            BudgetPerStaff: { required: true }
        },
      
        errorPlacement: function ($error, $element) {
            $error.addClass('errorHolder');
            $element.closest('.form-group').find('label:first').after($error);
        }

    });

    $('#SaveTraining').on('click', function () {
        $button = $(this);
        $('#form1').find('label.error').remove();
        if ($("#form1").valid()) {
           
            /// check if period exists and save

            var periods = [];
            var isValid = true;
            var hours = 0;
            var startdate = moment($('#StartPeriod').val(), 'DD/MM/YYYY').toDate();
            var enddate = moment($('#EndPeriod').val(), 'DD/MM/YYYY').toDate();

            $('#PeriodPanel').find('.period-row').each(function (i, item) {
              
                var startDay = startdate.getDay();

                var days = (startdate === enddate) ? [{ startDay }] : $(item).find('select').val();
                var id = $(item).attr('data-id');
               
                if (days === null || !days.length) {
                    $(item).find('select').before('<label class="errorHolder error">Please select from list</label>');
                    isValid = false;
                }
                else {
                    days.forEach(function (day) {
                        var periodObject = {};
                        periodObject.Id = id;
                        periodObject.Day = parseInt(day);
                        periodObject.StartTime = $(item).find('input.timepicker:first').val();
                        periodObject.EndTime = $(item).find('input.timepicker:last').val();
                        var currentStartTime = moment(periodObject.StartTime, 'HH:mm A');
                        var currentEndTime = moment(periodObject.EndTime, 'HH:mm A');

                        var StartDate = currentStartTime.toDate();
                        var EndDate = currentEndTime.toDate();

                        if (StartDate >= EndDate) {
                            $(item).find('label[for="time"]').after('<label class="errorHolder error">Start time selected is after end time</label>');
                            isValid = false;
                        }
                        else {
                            hours = hours + Math.floor((EndDate - StartDate) / 3600000);
                            periods.push(periodObject);
                        }

                        
                    });


                }
            });
            alert(startdate);
            if (startdate != "Invalid Date" && EndDate != "Invalid Date") {
                if (periods.length === 0) {
                    ShowNotie({ HasError: true, Message: "You must add at least one training period" });
                    return;
                }
            }
           
            if (isValid && startdate != "Invalid Date" && EndDate != "Invalid Date") {
                
                // check if day exists

                var toCheck = periods;
                var errors = [];

                var momentStart = moment(startdate);
                var momentEnd = moment(enddate);
                var diffDays = momentEnd.diff(momentStart, 'days');

                toCheck.forEach(function (day) {

                    var dayCheck = toCheck.filter(function (el) {
                        return el.Day === day.Day

                    });

                    

                    if (diffDays < 7) {
                        if (day.Day < startdate.getDay() || day.Day > enddate.getDay()) {
                            var errorObj = { Day: day.Day };
                            errors.push(errorObj);
                            return; 
                        }

                    }

                    toCheck = $.grep(toCheck, function (a) {
                        return a.Day !== day.Day;
                    });



                    if (dayCheck.length) {
                        var errorObj = { Day: day.Day, ErrorTime: [] };


                        /// validate time
                        dayCheck.forEach(function (time, i) {

                            var startTime = time.StartTime;
                            var endTime = time.EndTime;
                            //errorObj.Time.push(startTime + " to " + endTime);

                            var currentStartTime = moment(startTime, 'HH:mm A');
                            var currentEndTime = moment(endTime, 'HH:mm A');

                            var StartDate = currentStartTime.toDate();
                            var EndDate = currentEndTime.toDate();

                            dayCheck = dayCheck.slice(i + 1, dayCheck.length);

                            /// if my start and end time is within anothers throw error 

                            dayCheck.forEach(function (timeCheck, j) {

                                var startCheckTime = timeCheck.StartTime;
                                var endCheckTime = timeCheck.EndTime;
                                var currentCheckStartTime = moment(startCheckTime, 'HH:mm A');
                                var currentCheckEndTime = moment(endCheckTime, 'HH:mm A');
                                var StartDateCheck = currentCheckStartTime.toDate();
                                var EndDateCheck = currentCheckEndTime.toDate();


                                if (EndDateCheck <= EndDate && StartDateCheck >= StartDate) {
                                    errorObj.ErrorTime.push(startCheckTime + " to " + endCheckTime);
                                }
                            });
                        });
                        if (errorObj.ErrorTime.length > 0) {
                            errors.push(errorObj);
                        }

                    }

                });

                if (errors.length > 0) {

                    var confirmHtml = '<div class="nofloat customError"><span class="title">The following days have clashing times</span>';
                    confirmHtml = confirmHtml + '<div><h4>Warning</h4>';

                    errors.forEach(function (item) {
                        if (item.ErrorTime === undefined || item.ErrorTime === null) {
                            confirmHtml = confirmHtml + '<span>' + daysOfWeek[item.Day] + '  =>  does not come within this training period</span>';
                        }
                        else {

                            confirmHtml = confirmHtml + '<span>' + daysOfWeek[item.Day] + '  => ' +
                                item.ErrorTime.join(", ")
                                + '</span>';
                        }
                    });
                    confirmHtml = confirmHtml + "</div>";
                    confirmHtml = confirmHtml + "</div>";
                    isValid = false;
                    ShowNotie({ HasError: true, Message: confirmHtml });
                }


            }

            if (isValid) {

                //// insert into data via post

               
                var prev = $button.html();
                $button.attr('disabled', 'disabled');
                $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

                var formData = $('#form1').serializeArray();
                var startPeriod = $('#StartPeriod').datepicker('getDate');
                var endPeriod = $('#EndPeriod').datepicker('getDate');
                var weeks = Math.round((endPeriod - startPeriod) / 604800000);
                formData.push({ name: "Duration", value: weeks * hours});

                var returnArray = {};
                for (var i = 0; i < formData.length; i++) {
                    returnArray[formData[i]['name']] = formData[i]['value'];
                }

                returnArray.TrainingPeriod = periods;

                $.ajax({
                    cache: false,
                    async: true,
                    type: "POST",
                    url: "/Api/ManageTraining/AddorUpdate",
                    data: returnArray,
                    success: function (data) {
                        if (!data.HasError) {
                            ShowNotie(data);
                        }
                        else {
                            ShowNotie(data);
                            clearForm();
                        }

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
        }
        return false;
    });

    $('#btnCalcHours').on('click', function () {
        var isValid = true;
        var hours = 0;
        $('#PeriodPanel').find('.period-row').each(function (i, item) {
            var days = $(item).find('select').val();
            var id = $(item).attr('data-id');
            if (!days.length) {
                $(item).find('select').before('<label class="errorHolder error">Please select from list</label>');
                isValid = false;
            }
            else {
                days.forEach(function (day) {
                    var periodObject = {};
                    periodObject.Id = id;
                    periodObject.Day = parseInt(day);
                    periodObject.StartTime = $(item).find('input.timepicker:first').val();
                    periodObject.EndTime = $(item).find('input.timepicker:last').val();
                    var currentStartTime = moment(periodObject.StartTime, 'HH:mm A');
                    var currentEndTime = moment(periodObject.EndTime, 'HH:mm A');

                    var StartDate = currentStartTime.toDate();
                    var EndDate = currentEndTime.toDate();

                    
                    
                    if (StartDate >= EndDate) {
                        $(item).find('label[for="time"]').after('<label class="errorHolder error">Start time selected is after end time</label>');
                        isValid = false;
                    }
                    else {
                        hours = hours + Math.floor((EndDate - StartDate) / 3600000);
                    }


                });
                
            }
        });

        if (isValid) {
            var startPeriod = $('#StartPeriod').datepicker('getDate');
            var endPeriod = $('#EndPeriod').datepicker('getDate');
            var weeks = Math.round((endPeriod - startPeriod) / 604800000);
            $('#Duration').html(String(hours) + ' Hours ' + String(weeks) + ' Weeks - (' +  String(weeks * hours) + " hours)");
        }
    });

    $('#TrainingType').change(function () {
        var $value = $(this).val();
        if ($value === 2) {
            $('#Location').attr('disabled', 'disabled');
            $('#Venue').attr('disabled', 'disabled');
        }
        else {
            $('#Location').attr('disabled', false);
            $('#Venue').attr('disabled', false);
        }
    });
});



