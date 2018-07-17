$(document).ready(function () {

    $('#tblAuditList').hide();

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
        $('#auditTrial').find('label.error').remove();
        var isvalid = true;
        if ($('#AuditStartDate').val() == "") {
            isvalid = false;
            $('#AuditStartDate').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
        }
        if ($('#AuditEndDate').val() == "") {
            isvalid = false;
            $('#AuditEndDate').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
        }
        if ($('#user').val() == "") {
            isvalid = false;
            $('#user').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
        }
        if ($('#ddlEntityType').val() == "" || $('#ddlEntityType').val() == "0") {
            isvalid = false;
            $('#ddlEntityType').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
        }

        $.ajax({
            cache: false,
            async: false,
            type: "Get",
            data: { start: $('#AuditStartDate').val(), end: $('#AuditEndDate').val(), operation: $('#ddlOperationType').val(), name: $('#user').val(), entityname: $('#ddlEntityType').val() },
            url: "/api/ReportManagement/GetAudits",
            success: function (data) {
                $('#tblAuditList').show();
                jqTable();
            },

            error: function (data) {
                alert(JSON.stringify(data));
            }
        });

        if (isvalid) {
        
        }       
    });

})

var jqTable = function () {
    $('#tblAuditList').DataTable({
        destroy: true,
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        "autoWidth": false,
        processing: true,
        serverSide: true,
        pageLength: 25,
        searching: false,
        ajax: {
            "url": '/api/ReportManagement/GetAuditTable',
            "type": "GET"
        },
        "fnDrawCallback": function (oSettings) {
        },
        "order": [],
        columns: [
            { data: "SeqId" },
            { data: "ChangedDateTime"},
            { data: "Name" },
            { data: "TableName" },
            { data: "ColumnName" },
            { data: "Operation" },
            { data: "BeforeUpdate" },
            { data: "AfterUpdate" }
        ],
        select: true,
    });
}