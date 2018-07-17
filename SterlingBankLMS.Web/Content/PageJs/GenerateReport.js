$(document).ready(function () {
    //jqTable();
    $('#ddlExport').hide();

    
    $('#tblGenerateReport').hide();
    $('#txtStartDate').datepicker({
        yearRange: "-119:+0",
        changeMonth: true,
        changeYear: true,
        format: 'dd-MM-yy',
        autoclose: true

    });

    $('#txtEndDate').datepicker({
        yearRange: "-119:+0",
        changeMonth: true,
        changeYear: true,
        format: 'dd-MM-yy',
        autoclose: true

    });

    $.ajax({
        url: '/api/ReportManagement/getGroup',
        contentType: "application/json; charset=UTF-8",
        type: "GET",
        success: function (data) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                $('#ddlGroup').append('<option value="' + value.GroupId + '">' + value.GroupName + '</option>');
            });
        },
        error: function (data) {

        }
    });

    $.ajax({
        url: '/api/ReportManagement/getGrade',
        contentType: "application/json; charset=UTF-8",
        type: "GET",
        success: function (data) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                $('#ddlGrade').append('<option value="' + value.Id + '">' + value.Name + '</option>');
            });
        },
        error: function (data) {

        }
    });

    $('#ddlGroup').on('change', function () {
        $.ajax({
            url: '/api/ReportManagement/getDepartment/' + $('#ddlGroup').val(),
            contentType: "application/json; charset=UTF-8",
            type: "GET",
            success: function (data) {
                $.each(data, function (index, value) {
                    // APPEND OR INSERT DATA TO SELECT ELEMENT.
                    $('#ddlDepartment').append('<option value="' + value.Id + '">' + value.Name + '</option>');
                });
            },
            error: function (data) {

            }
        });
    });

    $('#btnGenerate').click(function () {
        $('#ddlExport').show();
        $('#ddlExport').val('0');
        $('#tblGenerateReport').show();
        var $data = {
            ReportType: $('#ddlReportType option:selected').text(),
            StartDate: $('#txtStartDate').val(),
            EndDate: $('#txtEndDate').val(),
            StaffId: $('#txtStaffId').val(),
            GroupId: $('#ddlGroup').val(),
            GradeId: $('#ddlGrade').val(),
            DepartmentId: $('#ddlDepartment').val()
        };

        var results = new Array();

        if ($('#ddlReportType').val() == '1') {
       
            results1 = new Array();
            results1 = [
                { title: "Staff Id", data: "StaffID" },
                { title: "Staff Name", data: "StaffName" },
                { title: "Region", data: "Region" },
                { title: "Group", data: "Group" },
                { title: "Grade", data: "Grade" },
                { title: "Courses", data: "Courses" },
                { title: "No Of Courses", data: "NoOfCourses" },
                { title: "Average Score", data: "AverageScore" }
            ];
            h($data, results1);
        }

        if ($('#ddlReportType').val() == '2') {
            $('.coursereport').show();
            $('.actreport').hide();
            $('.userreport').hide();
            $('.partreport').hide();
            $('.gradesreport').hide();
            results = new Array();
            results = [
                { title:"Name", data: "Name" },
                { title: "Due Date",data: "DueDate" },
                { title: "Duration",data: "Duration" },
                { title: "Average Score",data: "AverageScore" },
                { title: "No Of Participants",data: "NoOfParticipants" },
                { title: "Course Evaluation Score", data: "Courseevaluationscore" }
            ];
            h($data, results);            
        }

        if ($('#ddlReportType').val() == '3') {
            $('.coursereport').hide();
            $('.actreport').hide();
            $('.userreport').hide();
            $('.partreport').hide();
            $('.gradesreport').show();
            results = new Array();
            results = [
                { title: "Staff Id", data: "StaffID" },
                { title: "Staff Name", data: "StaffName" },
                { title: "Region", data: "Region" },
                { title: "Group", data: "Group" },
                { title: "Grade", data: "Grade" },
                { title: "Exams", data: "Exams" },
                { title: "Score per course", data: "Scorepercourse" }
            ];
            h($data, results);
        }

        if ($('#ddlReportType').val() == '4') {
            $('.coursereport').hide();
            $('.actreport').hide();
            $('.userreport').hide();
            $('.partreport').show();
            $('.gradesreport').hide();
            results = new Array();
            results = [
                { title: "Staff Id", data: "StaffID" },
                { title: "Staff Name", data: "StaffName" },
                { title: "Region", data: "Region" },
                { title: "Group", data: "Group" },
                { title: "Grade", data: "Grade" },
                { title: "Courses", data: "Courses" },
                { title: "Status of Courses", data: "StatusOfCourses" },
                { title: "No Of Attempts", data: "NoOfAttempts" },               
                { title: "Date Accessed", data: "DateAccessed" },
                { title: "Time Accessed", data: "TimeAccessed" },
                { title: "Duration", data: "Duration" },
                { title: "Location", data: "Location" } 
            ];
            h($data, results);
        }

        if ($('#ddlReportType').val() == '5') {
            $('.coursereport').hide();
            $('.actreport').show();
            $('.userreport').hide();
            $('.partreport').hide();
            $('.gradesreport').hide();
            results = new Array();
            results = [
                { title: "Staff Id", data: "StaffID" },
                { title: "Staff Name", data: "StaffName" },
                { title: "Region", data: "Region" },
                { title: "Group", data: "Group" },
                { title: "Grade", data: "Grade" },
                { title: "Courses", data: "Courses" },
                { title: "No of courses", data: "NoOfCourses" },
                { title: "No of attempts", data: "NoOfAttempts" },
                { title: "Average Score", data: "AverageScore" },
                { title: "Location", data: "Location" }
            ];
            h($data, results);
        }


        $('#ddlExport').on('change', function () {
            window.location.href = '/Admin/ReportManagement/' + $('#ddlExport').val() + '/' + $('#ddlReportType').val()
        });

        h.on('draw', function () {

            $('.tableRanch').find('input[type="search"]').addClass('searchT');
            // reset();
        });

    });
});
var h = function ($data, results) {
    $('#tblGenerateReport').DataTable().destroy(); 
    $('#tblGenerateReport').empty(); 
    $('#tblGenerateReport').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        destroy: true,
        "autoWidth": false,
        cache:false,
        processing: true,
        serverSide: true,
        ajax: {
            "url": '/api/ReportManagement/GetGenReportTable',
            "data": function (d) {
                d.AdditionalParameters = JSON.stringify($data);
            },
            "type": "GET"
        },
        'language': {
            'search': '',
            'searchPlaceholder': 'Search...'
        },
        "fnDrawCallback": function (oSettings) {
        },
        "order": [],
        columns: results
    });
}