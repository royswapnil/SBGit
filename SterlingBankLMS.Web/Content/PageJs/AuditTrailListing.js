$(document).ready(function () {
    var $data = { startdate: null, enddate: null, type: 0, user: null, entityType: [1, 2, 3, 4, 5], buttonclick: 'false' };
    jqTable($data);



    $('.select2').select2({
        placeholder: "Please select",
        maximumSelectionLength: 5,
        'width': '100%'
    });

    $.ajax({
        url: '/api/ReportManagement/GetEntityList',
        contentType: "application/json; charset=UTF-8",
        type: "GET",
        success: function (data) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                $('#selectEntities').append('<option value="' + value.id + '">' + value.text + '</option>');
            });
        },
        error: function (data) {

        }
    });



    $('#AuditStartDate').datepicker({
        yearRange: "-119:+0",
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-MM-yy',
        endDate: 'd',
        autoclose: true

    });

    $('#AuditEndDate').datepicker({
        yearRange: "-119:+0",
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-MM-yy',
        endDate: 'd',
        autoclose: true
    });



    $('#btnGenerate').click(function () {
        var isvalid = true;
        var selectentities = $("#selectEntities").select2('data')
        var entitylist = new Array();
        if (selectentities.length > 0) {
            for (var i = 0; i < selectentities.length; i++) {
                entitylist.push(selectentities[i].id)
            }
        }
        else {
            ShowNotie({ HasError: true, Message: "Please select entities" });
            $button.html(prev);
            $button.removeAttr('disabled');
            return;
        }

        if (isvalid) {
            var $data = { startdate: $('#AuditStartDate').val(), enddate: $('#AuditEndDate').val(), type: $('#ddlOperationType').val(), user: $('#user').val(), entityType: entitylist, buttonclick: 'true' };
            jqTable($data);
        }
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });

})

var jqTable = function ($data) {
    $('#tblAuditList').DataTable({
        destroy: true,
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        "autoWidth": false,
        processing: true,
        serverSide: true,
        pageLength: 50,
        ajax: {
            "url": '/api/ReportManagement/GetAuditTable',
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
        columns: [
            { data: "SeqId" },
            { data: "ChangedDateTime" },
            { data: "Name" },
            { data: "AliasName" },
            { data: "ColumnName" },
            { data: "Operation" },
            { data: "BeforeUpdate" },
            { data: "AfterUpdate" }
        ],
        select: true,
    });

}