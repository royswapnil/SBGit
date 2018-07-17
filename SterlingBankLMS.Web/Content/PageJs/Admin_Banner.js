$(document).ready(function () {
    $.get('/PartialViews/Admin/AdBannerRow.html', function (template) {
        $('#PartialTemplates').append(template);
    });


    var jqTable = $('#bannerTable').DataTable({
        dom: '<"tanHead tanHP"f>rt<"counter"lip>',
        serverSide: true,
        ajax: {
            "url": '/api/Advert/GetPagedBanner',
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
            { data: "Title" },
            { data: "Size" },
            {
                bSortable: false,
                className: "image",
                "render": function (d, i, data) {
                    return '<img src="' + data.FileUrl + '"/>';

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

    /// delete banner
    $("#bannerTable tbody").on("click", "td.action-buttons button.table-delete", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();

        $('.content1 .messageText').remove();
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
                url: "/api/Advert/DeleteBanner?Id=" + id,

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


    $("#bannerTable tbody").on("click", "td.action-buttons button.table-edit", function () {
        var tr = $(this).closest("tr");
        var id = $(this).closest("td").find('input').val();
        $('.content1 .messageText').remove();
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
            url: "/api/Advert/GetBanner",
            data: { Id: id },
            success: function (data) {
                $('#AdvertTitle').val(data.Result.Title);
                $('#PreviewFile').prop('src',data.Result.FileUrl);
                $('#BannerID').val(data.Result.Id);
                $('#FileUpload').attr('data-upload', true);
                $('#BannerImageUpload').attr('disabled', 'disabled');
                $('#PreviewFile').parent().show();

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

    $("#BannerForm").validate({

        rules: {
            Title: { required: true },
            ImageUpload: {
                required: function (item) {
                    return !$(item)[0].hasAttribute("data-upload")
                }, accept: "image/*"
            }
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
            /// if file exists

            if ($('#BannerImageUpload').val()) {
                formData.append('Size', $('#BannerImageUpload').data('Size'));
            }


            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/Advert/AddorUpdateBanner",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                  
                    var template = document.getElementById('AdBannerRow').innerHTML;
                    var newRow = Mustache.render(template, data.Result);

                    // bind table
                    if ($('#BannerID').val() == '0')
                    {
                        jqTable.row.add($(newRow)).draw(false);
                    }
                    else {
                        var tr = $('#bannerTable').find('tr td:last-child input[value="' + $('#BannerID').val() + '"]').closest('tr');
                        if (tr.length) {
                            jqTable.row(tr).remove().draw(false);
                            jqTable.row.add($(newRow)).draw(false);  
                        }
                    }
                    $('#BannerForm')[0].reset();
                    $('#BannerID').val('0');
                    $('#PreviewFile').parent().hide();
                    $('#PreviewFile img').removeProp('src');
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


    //// unchange of image track max file upload 
    $('#BannerImageUpload').on('change', function () {
        if ($(this).val()) {
            $('#AddBanner').attr('disabled', 'disabled');
            var fr = new FileReader;

            fr.onload = function () {
                var img = new Image;
                img.src = fr.result;
                img.onload = function () {
                    $('#BannerImageUpload').data('Size', img.width + ' X ' + img.height);
                    $('#AddBanner').removeAttr('disabled');
                };
            }


        };

        fr.readAsDataURL($(this).prop('files')[0])
    });


    $('.addNew').on('click', function () {
        $('#BannerForm')[0].reset();
        $('#BannerID').val('0');
        $('#PreviewFile img').removeProp('src');
        $('#PreviewFile').parent().hide();
        $('.viewManagePanel').click();
    });


});