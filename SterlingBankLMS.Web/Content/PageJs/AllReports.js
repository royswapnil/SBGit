$(document).ready(function () {

    $('#btnClick').click(function (e) {
        location.href = "/Admin/ReportManagement/NewReport/?ReportTitle=" + $('#txtReportName').val();
    });

    var jqTable = $('#tblReportList').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        "autoWidth": false,
        processing: true,
        serverSide: true,
        ajax: {
            "url": '/api/ReportManagement/GetReportTable',
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
            { data: "ReportName" },
            { data: "Fields" },
            {
                bSortable: false,
                "render": function (d, i, data) {

                    return ' <a class="table-add btnSmaller btnGreen" href="/Admin/ReportManagement/NewReport/' + data.ReportId + '">Edit</a>'
                }
            }
        ]
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });
})