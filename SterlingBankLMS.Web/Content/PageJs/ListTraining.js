$(document).ready(function () {

    //Load the Training table
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
                    return data.StartDateFormat + " to " + data.EndDateFormat;
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

                    return '<button class="table-view btnSmaller bkPurple" href="javascript:;"> <i class="fa fa-edit table-icon" style=""></i> View/Edit	</button>' +
                        ' <a class="table-add btnSmaller btnGreen" href="/Admin/Survey/Training/' + data.Id + '"><i class="fa fa-plus table-icon" style=""></i> Add Survey</a > ' +
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

    $("#TrainingTable tbody").on("click", "td.action-buttons button.table-view", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        $('.content1 .messageText').remove();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $button.attr('disabled', 'disabled');
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
                $('#Name').html(data.Result.Name);
                $('#Vendor').val(data.Result.Vendor);
                $('#TrainingCategory').find('option[value="' + data.Result.TrainingCategory + '"]').prop('selected', true);
                $('#TrainingType').find('option[value="' + data.Result.TrainingType + '"]').prop('selected', true);
                $('#BudgetPerStaff').val(data.Result.BudgetPerStaff);
                $('#Budget').val(data.Result.Budget);
                $('#Location').val(data.Result.Location);
                $('#Duration').val(data.Result.Duration);
                $('#StartPeriod').val(startdate);
                $('#EndDate').val(enddate);
                $('#Venue').val(data.Result.Venue);
                $('.changetext').html("View/Update Training");
                $("#OtherFees").val(data.Result.Budget - data.Result.BudgetPerStaff);
                $('#Id').val(data.Result.Id);
                $('.content1').css("background", "#ffffff");

                $button.removeAttr('disabled');
                $button.html(prev);
                $('#TrainingListPanel').fadeOut();
                $('#TrainingViewPanel').fadeIn();

            },

            error: function (data) {
                var output = LoadPageError({ HasError: true, Message: data.responseJSON.Message });
                $('.boxBody1').prepend(output);
                $button.removeAttr('disabled');
                $button.html(prev);
            }
        });


    });

})