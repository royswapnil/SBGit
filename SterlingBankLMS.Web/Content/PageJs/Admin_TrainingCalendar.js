function closepopover() {
    $('.fc-event.popover-open').popover('hide');
    $('.popover').remove();
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

            $.get('/api/ManageTraining/GetAllTrainingByMonth', requestData, function (response) {
                var events = new Array();

                var obj = response.Result;
                $('#calendar').fullCalendar('removeEvents');

               
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
                    var periods = item.TrainingPeriod;
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



    $('#dayview').on('click', function () {
        var type = $('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $('#calendar').fullCalendar('changeView', 'listDay');
        }
        else {
            $('#calendar').fullCalendar('changeView', 'agendaDay');
        }
    });

    $('#weekview').on('click', function () {
        var type = $('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $('#calendar').fullCalendar('changeView', 'listWeek');
        }
        else {
            $('#calendar').fullCalendar('changeView', 'agendaWeek');
        }
       
    });

    $('#monthview').on('click', function () {
        var type = $('.calendar-view a.active').attr('data-type');
        if (type == 'list') {
            $('#calendar').fullCalendar('changeView', 'listMonth');
        }
        else {
            $('#calendar').fullCalendar('changeView', 'month');
        }
    });

    $('.calendar-view a').on('click', function () {
        var cal = $('#calendar').fullCalendar('getView');
        var type = $(this).attr('data-type');

        if (type == 'list') {
            switch (cal.type) {
                case 'month': $('#calendar').fullCalendar('changeView', 'listMonth'); break;
                case 'agendaWeek': $('#calendar').fullCalendar('changeView', 'listWeek'); break;
                case 'agendaDay': $('#calendar').fullCalendar('changeView', 'listDay'); break;
            }
        }
        else {
            switch (cal.type) {
                case 'listMonth': $('#calendar').fullCalendar('changeView', 'month'); break;
                case 'listWeek': $('#calendar').fullCalendar('changeView', 'agendaWeek'); break;
                case 'listDay': $('#calendar').fullCalendar('changeView', 'agendaDay'); break;

            }
        }

        $('.calendar-view a').toggleClass('active');

    });


});


