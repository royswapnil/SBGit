function closepopover() {
    $('.fc-event.popover-open').popover('hide');
    $('.popover').remove();
    $('#TrainingItems').find('.event-list-item').removeClass('selected');
}

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

$(document).ready(function () {
    var date = new Date();
    var data = {
        month: date.getMonth() + 1,
        year: date.getFullYear()
    };

    $.get('/PartialViews/Common/_UserPendingTrainingRequests.html', function (template) {
        $('#PartialTemplates').append(template);
        $.get("/api/ManageTraining/GetUserPendingTrainingRequest", function (data) {
            var template = document.getElementById('UserPendingTrainingRequests').innerHTML;
            var output = Mustache.render(template, { array: data.Result });
            $('#tab3').html(output);
        });

        $.get("/api/ManageTraining/GetTrainingRequestPendingLineManagerApproval", function (data) {
            var template = document.getElementById('UserPendingTrainingRequests').innerHTML;
            if (data.Result.HasError) {
                $('a[href="#tab4"]').parent().hide();
                return;
            }
            $('#lblRequestPending').html(data.Result.length);
            var output = Mustache.render(template, { array: data.Result });
            $('#tab4').html(output);
        });
    });

    $.get('/PartialViews/Common/_AllTraining.html', function (template) {
        $('#PartialTemplates').append(template);
    });

    $('#mycalendar').fullCalendar({
        header: {
            left: 'prev',
            center: 'title',
            right: 'next'
        },
        eventOverlap: true,
        editable: false,
        droppable: false,
        viewRender: function (view, element) {
            var view = $('#mycalendar').fullCalendar('getView');
            var calStartDate = view.start.toDate();
            var calEndDate = view.end.toDate();

            var date = $('#mycalendar').fullCalendar('getDate');

            var requestData = {
                startDate: moment(calStartDate).format('DD/MM/YYYY'),
                endDate: moment(calEndDate).format('DD/MM/YYYY')
            }

            $.get('/api/ManageTraining/GetUserTrainingByMonth', requestData, function (response) {
                var events = new Array();

                var obj = response.Result;
                $('#mycalendar').fullCalendar('removeEvents');

                obj.forEach(function (item) {
                    var list = new Object();
                    list.id = item.Id;
                    list.className = " bg wrapper-xs";
                    /// if no periods
                    list.title = item.Name;
                    list.desc = item.Description == null ? 'N/A' : item.Description;
                    list.LocationPlace = item.LocationPlace;
                    list.category = item.TrainingCategoryName;
                    list.type = item.TrainingTypeName;
                    list.vendor = item.Vendor == null ? 'N/A' : item.Vendor;
                    list.startPeriod = item.StartDateFormat;
                    list.endPeriod = item.EndDateFormat;
                    var periods = JSON.parse(item.PeriodFormat);
                    list.period = periods;
                    if (periods != null && periods.length > 0) {

                        /// add seven days to start date and check period available

                        while (calStartDate <= calEndDate) {
                            periods.forEach(function (periodObj) {
                                var start = calStartDate;
                                var startTime = moment(periodObj.StartTime, 'HH:mm A').toDate()
                                var endTime = moment(periodObj.EndTime, 'HH:mm A').toDate();

                                var startDay = start.addDays(periodObj.Day)
                                var calStartTime = startDay.setHours(startTime.getHours(), startTime.getMinutes(), 0, 0);
                                var calEndTime = startDay.setHours(endTime.getHours(), endTime.getMinutes(), 0, 0);

                                list.start = calStartTime;
                                list.end = calEndTime;
                                list.duplicate = true;
                                $('#mycalendar').fullCalendar('renderEvent', list);
                            });
                            calStartDate = calStartDate.addDays(7);
                        }

                    }
                    else {
                        list.start = item.StartPeriod < requestData.startDate ? requestData.startDate : item.StartPeriod;
                        list.end = item.EndPeriod > requestData.endDate ? requestData.endDate : item.EndPeriod;
                        $('#mycalendar').fullCalendar('renderEvent', list);
                    }

                    calStartDate = view.start.toDate();
                    calEndDate = view.end.toDate();
                });

            });

        },
        eventClick: function (event, jsEvent, view) {
            $('.fc-event.popover-open').popover('hide');
            $('.popover').remove();

            var periodHtml = '';
            if (event.period.length > 0) {
                event.period.forEach(function (item) {
                    periodHtml = periodHtml + '<label class="label label-primary period-label">' + item.DayName + ': ' + item.StartTime + ' to ' + item.EndTime + '</label>';
                });
            }

            $this = $(this);
            $this.addClass('popover-open');


            var popover = $this.popover({
                html: true, container: 'body',
                content: '<div><p> Description: <strong> ' + event.desc + '</strong ></p>' +
                '<p> Vendor: <strong> ' + event.vendor + '</strong ></p>' +
                '<p> Category: <strong> ' + String(event.category) + '</strong ></p>' +
                '<p> Type: <strong> ' + String(event.type) + '</strong ></p>' +
                '<p> Location: <strong > ' + String(event.LocationPlace) + '</strong ></p>' +
                '<p> Start Date: <strong >' + event.startPeriod + '</strong ></p>' +
                '<p> End Date: <strong >' + event.endPeriod + '</strong ></p>' +
                '<div class="nofloat">' + periodHtml + '</div>' +
                '</div>',
                title: '<span style="padding-top:5px;display:inline-block">' + event.title + '</span>' +
                '<button type="button" onclick="closepopover()" class="close" aria-hidden="true" style="">x</button>',
                placement: 'top'

            }).popover('show');

            console.log(popover);

            return false;

        },
        //eventRender: function (event, element) {
        //    $(element).attr('title', event.title);
        //},

        eventAfterRender: function (event, element, view) {

            //if (event.timestatus == 0) {
            //    element.find('.fc-event-inner').after('<span class="badge bot badge-sm up bg-danger timetaskstatus"><i class="fa fa-exclamation-circle"></i></span>');
            //}
            //else {
            //    element.find('.fc-event-inner').after('<span class="badge bot badge-sm up bg-primary timetaskstatus">&#x2714;</span>');
            //}

            //if (event.duplicate == true) {
            //    element.find('.fc-event-inner').after('<span class="badge badge-sm up bg-success timetaskstatus"><i class="fa fa-clipboard"></i></span>');
            //}
        }

    });

    $('#calendar').fullCalendar({
        header: {
            left: 'prev',
            center: 'title',
            right: 'next'
        },
        eventOverlap: true,
        editable: false,
        droppable: false,
        viewRender: function (view, element) {
            var view = $('#calendar').fullCalendar('getView');
            var calStartDate = view.start.toDate();
            var calEndDate = view.end.toDate();

            var date = $('#calendar').fullCalendar('getDate');

            var requestData = {
                startDate: moment(calStartDate).format('DD/MM/YYYY'),
                endDate: moment(calEndDate).format('DD/MM/YYYY')
            }

            $.get('/api/ManageTraining/GetUserOrgTrainingByMonth', requestData, function (response) {
                var events = new Array();

                var obj = response.Result;
                $('#calendar').fullCalendar('removeEvents');

                obj.forEach(function (item) {
                    var list = new Object();
                    list.id = item.Id;
                    list.requested = item.Requested;
                    list.ongoing = item.Ongoing;
                    var className = ''
                    if (list.requested) {
                        className = "bg-danger";
                    }
                    if (list.ongoing) {
                        className = "bg-success";
                    }


                    list.className = className + " bg wrapper-xs";
                    /// if no periods
                    list.title = item.Name;
                    list.desc = item.Description == null ? 'N/A' : item.Description;
                    list.LocationPlace = item.LocationPlace;
                    list.category = item.TrainingCategoryName;
                    list.type = item.TrainingTypeName;
                    list.vendor = item.Vendor == null ? 'N/A' : item.Vendor;
                    list.startPeriod = item.StartDateFormat;
                    list.endPeriod = item.EndDateFormat;

                    var periods = JSON.parse(item.PeriodFormat);
                    list.period = periods;
                    if (periods != null && periods.length > 0) {
                        /// add seven days to start date and check period available

                        while (calStartDate <= calEndDate) {
                            periods.forEach(function (periodObj) {
                                var start = calStartDate;
                                var startTime = moment(periodObj.StartTime, 'HH:mm A').toDate()
                                var endTime = moment(periodObj.EndTime, 'HH:mm A').toDate();

                                var startDay = start.addDays(periodObj.Day)
                                var calStartTime = startDay.setHours(startTime.getHours(), startTime.getMinutes(), 0, 0);
                                var calEndTime = startDay.setHours(endTime.getHours(), endTime.getMinutes(), 0, 0);

                                list.start = calStartTime;
                                list.end = calEndTime;
                                list.duplicate = true;
                                $('#calendar').fullCalendar('renderEvent', list);
                            });
                            calStartDate = calStartDate.addDays(7);
                        }

                    }
                    else {
                        list.start = item.StartPeriod < requestData.startDate ? requestData.startDate : item.StartPeriod;
                        list.end = item.EndPeriod > requestData.endDate ? requestData.endDate : item.EndPeriod;
                        $('#calendar').fullCalendar('renderEvent', list);
                    }
                    calStartDate = view.start.toDate();
                    calEndDate = view.end.toDate();
                });

            });

        },
        eventClick: function (event, jsEvent, view) {
            $('.fc-event.popover-open').popover('hide');
            $('.popover').remove();

            var requestHtml = '';
            
            if (!event.requested && !event.ongoing) {
                requestHtml = '<div style="border: 1px solid #c2c2c2; padding: 10px;margin:10px 0;width:100%">' +
                    '<h5 style= "float: none;">Request Training</h5>' +
                    '<textarea class="nofloat form-control" name= "comments" placeholder= "Comments" ></textarea>' +
                    '<button data-id="'+event.id+'" type="button" class="btnSmaller btnBlue nofloat saveTrainingRequest" style="margin-top: 10px;">Request Training</button></div>';
            }
            var periodHtml = '';
            if (event.period.length > 0) {
                event.period.forEach(function (item) {
                    periodHtml = periodHtml + '<label class="label label-primary period-label">' + item.DayName + ': ' + item.StartTime + ' to ' + item.EndTime + '</label>';
                });
            }

            $this = $(this);
            $this.addClass('popover-open');

            var popover = $this.popover({
                html: true, container: 'body',
                content: '<div style="width:100%"><p> Description: <strong> ' + event.desc + '</strong ></p>' +
                '<p> Vendor: <strong> ' + event.vendor + '</strong ></p>' +
                '<p> Category: <strong> ' + String(event.category) + '</strong ></p>' +
                '<p> Type: <strong> ' + String(event.type) + '</strong ></p>' +
                '<p> Location: <strong > ' + String(event.LocationPlace) + '</strong ></p>' +
                '<p> Start Date: <strong >' + event.startPeriod + '</strong ></p>' +
                '<p> End Date: <strong >' + event.endPeriod + '</strong ></p>' + 
                '<div class="nofloat">' + periodHtml+'</div>'+
                requestHtml +
                '</div>',
                title: '<span style="padding-top:5px;display:inline-block">' + event.title + '</span>' +
                '<button type="button" onclick="closepopover()" class="close" aria-hidden="true" style="">x</button>',
                placement: 'top'

            }).popover('show');
            $('#calendar').data('curEvent', event);
            return false;

        },
        //eventRender: function (event, element) {
        //    $(element).attr('title', event.title);
        //},
        eventAfterRender: function (event, element, view) {

            //if (event.timestatus == 0) {
            //    element.find('.fc-event-inner').after('<span class="badge bot badge-sm up bg-danger timetaskstatus"><i class="fa fa-exclamation-circle"></i></span>');
            //}
            //else {
            //    element.find('.fc-event-inner').after('<span class="badge bot badge-sm up bg-primary timetaskstatus">&#x2714;</span>');
            //}

            //if (event.duplicate == true) {
            //    element.find('.fc-event-inner').after('<span class="badge badge-sm up bg-success timetaskstatus"><i class="fa fa-clipboard"></i></span>');
            //}
        }

    });


    $('.dayview').on('click', function () {
        var $content = $(this).closest('.tab');
        var type = $content.find('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $content.find('.calendar').fullCalendar('changeView', 'listDay');
        }
        else {
            $content.find('.calendar').fullCalendar('changeView', 'agendaDay');
        }
      
    });

    $('.weekview').on('click', function () {
        var $content = $(this).closest('.tab');
        var type = $content.find('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $content.find('.calendar').fullCalendar('changeView', 'listWeek');
        }
        else {
            $content.find('.calendar').fullCalendar('changeView', 'agendaWeek');
        }
      

    });

    $('.monthview').on('click', function () {
        var $content = $(this).closest('.tab');
        var type = $content.find('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $content.find('.calendar').fullCalendar('changeView', 'listMonth');
        }
        else {
            $content.find('.calendar').fullCalendar('changeView', 'month');
        }
      
    });

    $('.calendar-view a').on('click', function () {
        var $content = $(this).closest('.tab');
        var cal = $content.find('.calendar').fullCalendar('getView');
        var type = $(this).attr('data-type');

        if (type == 'list') {
            switch (cal.type) {
                case 'month': $content.find('.calendar').fullCalendar('changeView', 'listMonth'); break;
                case 'agendaWeek': $content.find('.calendar').fullCalendar('changeView', 'listWeek'); break;
                case 'agendaDay': $content.find('.calendar').fullCalendar('changeView', 'listDay'); break;
            }
        }
        else {
            switch (cal.type) {
                case 'listMonth': $content.find('.calendar').fullCalendar('changeView', 'month'); break;
                case 'listWeek': $content.find('.calendar').fullCalendar('changeView', 'agendaWeek'); break;
                case 'listDay': $content.find('.calendar').fullCalendar('changeView', 'agendaDay'); break;

            }
        }

        $content.find('.calendar-view a').toggleClass('active');

    });

     /// Line Manager Approve or reject Training 
    $('body').on('click', '.action-row button', function () {
        var $button = $(this);
        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;
           
            var type = parseInt($button.attr('data-type'));

            var myObject = new Object();
            myObject.IsApproval = type == 1 ? true : false;
            myObject.Comments = $button.closest('.action-row').find('textarea').val();
            myObject.Id = $button.closest('article').attr('data-id');

            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa-circle-o-notch fa-spin"></i>');

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/ManageTraining/ManagerApproveOrRejectTrainingRequest",
                data: myObject,
                success: function (data) {
                    ShowNotie(data);
                    $button.closest('article').remove();
                    var length = parseInt($('#lblRequestPending').html()) - 1;
                    $('#lblRequestPending').html(length);
                },

                error: function (data) {
                    $button.html(prev);
                    $button.removeAttr('disabled');
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                }
            });
        }
        
    });

    /// save training request

    $('body').on('click', '.saveTrainingRequest', function () {

        var event;
         var $button = $(this);
         if (!$button[0].hasAttribute("list-button")) {
              event = $('#calendar').data('curEvent');
         }

        var id = $button.attr('data-id');
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin"></i>');
            var comments = $button.prev().val().replace(/^\s+|\s+$/g, "");
            var trainingRequest = {
                trainingId: parseInt(id),
                ReasonForRequest: comments
            };

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/ManageTraining/AddTrainingRequest",
                data: trainingRequest,
                success: function (data) {
                    if (!$button[0].hasAttribute("list-button")) {
                        event.backgroundColor = "red";
                        event.requested = true;
                        $('#calendar').fullCalendar('updateEvent', event);
                        $('#calendar').removeData('curEvent');
                        data.Result.TrainingName = event.title;
                    }
                    else {
                        $('#TrainingItems').find('.event-list-item.selected').addClass("bg-danger");
                        data.Result.TrainingName = $('#TrainingItems').find('.event-list-item.selected span.title').html();
                        $('#TrainingItems').find('.event-list-item').removeClass('selected');
                    }
                    
                    $button.closest('.popover').find('.popover-title button').click();
                    var template = document.getElementById('UserPendingTrainingRequests').innerHTML;

                    var output = Mustache.render(template, { array: data.Result });
                    if (!$('#tab3 .empty').length)
                        $('#tab3').append(output);
                    else
                        $('#tab3').html(output);

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

    $('#tab5').pagination({
        dataSource: '/api/ManageTraining/GetUserOrganizationTrainingList',
        locator: 'data',
        totalNumberLocator: function (response) {
            if ($('#TrainingItems')[0].hasAttribute("data-total"))
            { return parseInt($('#TrainingItems').attr("data-total")); }
            else {
                $('#TrainingItems').attr("data-total", response.recordsTotal);
                return response.recordsTotal;
            }

        },
        pageSize: 10,
        className: 'paginationjs-theme-blue paginationjs-big',
        ajax: {
            beforeSend: function () {
                var html = '<div class="nofloat">Fetching Data <i class="fa fa-spinner fa-pulse fa-fw nofloat"></i></div>';
                $('#TrainingItems').html(html);
            }
        },
        callback: function (data, pagination) {
            data.forEach(function (item) {
                item.Periods = JSON.parse(item.PeriodFormat);
            });

            var template = document.getElementById('TrainingList').innerHTML;
            var output = Mustache.render(template, { array: data });
            $('#TrainingItems').html(output);
        }
    });

    $('#TrainingItems').on('click','.event-list-item', function () {

        $('#TrainingItems').find('.event-list-item').removeClass('selected');
        var $this = $(this);
        $this.addClass('selected');
        $('.fc-event.popover-open').popover('hide');
        $('.popover').remove();

        var requestHtml = '';

        if ($this.attr("data-requested") == "false" && $this.attr("data-ongoing") == "false") {
            requestHtml = '<div style="border: 1px solid #c2c2c2; padding: 10px;margin:10px 0;width:100%">' +
                '<h5 style= "float: none;">Request Training</h5>' +
                '<textarea class="nofloat form-control" name= "comments" placeholder= "Comments" ></textarea>' +
                '<button list-button="true" data-id="' + $this.attr("data-id") + '" type="button" class="btnSmaller btnBlue nofloat saveTrainingRequest" style="margin-top: 10px;">Request Training</button></div>';
        }
        var periodHtml = $this.find('.period-holder').html();
        $this.addClass('popover-open');

        var popover = $this.popover({
            html: true, container: 'body',
            content: '<div style="width:100%"><p> Description: <strong> ' + $this.attr("data-desc") + '</strong ></p>' +
            '<p> Vendor: <strong> ' + $this.attr("data-vendor") + '</strong ></p>' +
            '<p> Category: <strong> ' + $this.attr("data-category") + '</strong ></p>' +
            '<p> Type: <strong> ' + $this.attr("data-type") + '</strong ></p>' +
            '<p> Location: <strong > ' + $this.attr("data-location") + '</strong ></p>' +
            '<p> Start Date: <strong >' + $this.attr("data-start") + '</strong ></p>' +
            '<p> End Date: <strong >' + $this.attr("data-end") + '</strong ></p>' +
            '<div class="nofloat">' + periodHtml + '</div>' +
            requestHtml +
            '</div>',
            title: '<span style="padding-top:5px;display:inline-block">' + $this.attr("data-title") + '</span>' +
            '<button type="button" onclick="closepopover()" class="close" aria-hidden="true" style="">x</button>',
            placement: 'top'

        }).popover('show');
    });
     

});


