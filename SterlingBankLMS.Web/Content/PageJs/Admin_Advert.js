
$(document).ready(function () {

    $('.datepicker').datepicker({ format: "dd/mm/yyyy" });

    $.get('/PartialViews/Admin/AdvertTableRow.html', function (template) {
        $('#PartialTemplates').append(template);
    });

    $.get('/api/Advert/GetBanners', function (data) {
        $('#AdBanner').append('<option>Please select</opton>');
        for (var i = 0; i < data.Result.length; i++) {
            $('#AdBanner').append('<option value=' + data.Result[i].Id + '>' + data.Result[i].Title + ' (' + data.Result[i].Size + ')</option>');
            $('#AdBanner').find('option:last').data("FilePath", data.Result[i].FileUrl);
        }
    });

    /// TOGGLE TAB

    $('.tabs .tab-links a').on('click', function (e) {
        var $item = $(this);
        var $tab = $item.closest('.tabs');
        var currentAttrValue = $(this).attr('href').substr(1, $(this).attr('href').length - 1);
        // Show/Hide Tabs
        $tab.find('.tab').removeClass('active');
        $tab.find('.tab input,select').attr('disabled', 'disabled');
        $tab.find('.' + currentAttrValue).addClass('active');
        $tab.find('.' + currentAttrValue + ' input,select').removeAttr('disabled');
        // Change/remove current tab to active
        $item.parent('li').addClass('active').siblings().removeClass('active');
        if (currentAttrValue == 'tabExternal')
            $('#AdvertLink').attr('disabled', 'disabled');
        else
            $('#AdvertLink').removeAttr('disabled');
        e.preventDefault();
    });

    var jqTable = $('#advert_table').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Advert/GetPagedAdvert',
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
            { data: "Title" },
            { data: "SectionName" },
            { data: "AdvertLink" },
            { data: "StartDate" },
            { data: "EndDate" },
            { data: "LocationName" },
            {
                bSortable: false,
                className: "image",
                "render": function (d, i, data) {
                    if (data.HTMLTag != null)
                        return data.HTMLTag;
                    else
                        return '<img src="' + data.FileUrl + '"/>';
                }
            },
            {
                bSortable: false,
                className: "",
                "render": function (d, i, data) {
                    if (data.IsActive)
                        return '<label>Active</label>';
                    else
                        return '<label>In-Active</label>';
                }
            },
            {
                bSortable: false,
                className: "action-buttons",
                "render": function (d, i, data) {
                    return '<button class="table-edit btnSmaller btnBlue" href="javascript:;"> <i class="fa fa-edit table-icon" style=""></i> Edit	</button>' +
                        '<button class="table-delete btnSmaller btnRed" href= "javascript:;" > <i class="fa fa-trash table-icon" style=""></i> <span class="deleteLA">Delete</span></button>' +
                        '<input type="hidden" value="' + data.Id + '"/>';

                }
            }
        ]
    });

    jqTable.on('draw', function () {

        $('.tableRanch').find('input[type="search"]').addClass('searchT');
        // reset();
    });

    $('#AdBanner').on('change', function () {
        var value = $('#AdBanner').val();
        $('#previewImg').prop('src', $('#AdBanner option[value="' + value + '"]').data("FilePath"));
    });

   

    $("#advert_table tbody").on("click", "td.action-buttons button.table-delete", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        if (confirm("Are you sure you want to do this?")) {
            $button.attr('disabled', 'disabled');
            $button.find('i').removeClass('fa-trash');
            $button.find('i').addClass('fa-circle-o-notch fa-spin');

            $.ajax({
                cache: false,
                async: true,
                type: "Delete",
                url: "/api/Advert/DeleteAdvert?Id=" + id,

                success: function (data) {
                    jqTable.row(tr).remove().draw(false);
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


    $("#advert_table tbody").on("click", "td.action-buttons button.table-edit", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        var $button = $(this);
        var prev = $button.html();

        if (!parseInt(id) && parseInt(id) <= 0) {
            return;
        }

        $button.attr('disabled', 'disabled');
        $button.find('i').removeClass('fa-trash');
        $button.find('i').addClass('fa-circle-o-notch fa-spin');


        $.ajax({
            cache: false,
            async: true,
            type: "Get",
            url: "/api/Advert/GetAdvert",
            data: { Id: id },
            success: function (data) {
                ClearForm();
                $('#AdvertTitle').val(data.Result.Title);
                $('#AdvertSection option[value="' + data.Result.Section+'"]').prop('selected',true);
                $('#AdvertLink').val(data.Result.AdvertLink);
                if (data.Result.AdBannerId != null) {
                    $('#AdBanner option[value="' + data.Result.AdBannerId + '"]').prop('selected', true);
                    $('#previewImg').prop('src', $('#AdBanner option[value="' + data.Result.AdBannerId + '"]').data("FilePath"));
                    $('a[href="#tabUpload"]').click();
                }
                else {
                    $('#AdvertTag').val(data.Result.HTMLTag);
                    $('#previewImg').removeProp('src');
                    $('a[href="#tabExternal"]').click();
                }

                $('#AdvertStartDate').val(data.Result.StartDate);
                $('#AdvertEndDate').val(data.Result.EndDate);
                $('#AdvertStartDate').datepicker("update", data.Result.StartDate);
                $('#AdvertEndDate').datepicker("update", data.Result.EndDate);

                $('#AdvertLocation option[value="' + data.Result.Location + '"]').prop('selected', true);
                $('#AdvertID').val(data.Result.Id);

                $button.removeAttr('disabled');
                $button.html(prev);
                $('#listPanel').hide();
                $('.viewManagePanel').hide();
                $('#managePanel').fadeIn();
                $('.viewListPanel').show();

            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
                $button.removeAttr('disabled');
                $button.html(prev);
            }
        });


    });


    $("#AdvertForm").validate({

        rules: {
            Title: { required: true },
            FileUrl: { required: true },
            AdvertLink:{url:true},
            Section: "dropdown",
            Location: "dropdown",
            AdBannerId: "dropdown",
            HtmlTag: { required: true },
            StartDate: {
                required: true,
                DateGreaterOrEqual: 'currentDate'
            },
            EndDate: {
                required: true,
                DateGreater: '#AdvertStartDate'
            },

        },
        message: {
            StartDate: { DateGreaterOrEqual: "Date must be greater than or equal to current day" }
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
            
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Advert/AddorUpdate",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {

                    var FileUrl = null;
                    if (data.Result.AdBannerId != null) {

                        FileUrl =   $('#AdBanner').find('option[value="' + data.Result.AdBannerId+'"]').data("FilePath");
                    }

                    data.Result.FileUrl = FileUrl;

                    var template = document.getElementById('AdvertRow').innerHTML;
                    var newRow = Mustache.render(template, data.Result);

                    // bind table
                    if ($('#AdvertID').val() == '0') {
                        jqTable.row.add($(newRow)).draw(false);
                    }
                    else {
                        var tr = $('#advert_table').find('tr td:last-child input[value="' + $('#AdvertID').val() + '"]').closest('tr');
                        if (tr.length) {
                            jqTable.row(tr).remove().draw(false);
                            jqTable.row.add($(newRow)).draw(false);
                        }
                    }
                    $('#AdvertForm')[0].reset();
                    $('#AdvertID').val('0');
                    $('#previewImg').removeProp('src');
                    ShowNotie(data);
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

    $('.addNew').on('click', function () {
        ClearForm();
        $('.viewManagePanel').click();
    });

});

function ClearForm() {
    $('#AdvertTitle').val('');
    $('#AdvertSection option:first').prop('selected', true);
    $('#AdvertLink').val('');
    $('#AdBanner option:first').prop('selected', true);
    $('#previewImg').removeProp('src');
    $('a[href="#tabUpload"]').click();
    $('#AdvertTag').val('');

    $('#AdvertStartDate').val('');
    $('#AdvertEndDate').val('');
    $('#AdvertStartDate').datepicker("update",new Date());
    $('#AdvertEndDate').datepicker("update", new Date());

    $('#AdvertLocation option:first').prop('selected', true);
    $('#AdvertID').val('0');
}