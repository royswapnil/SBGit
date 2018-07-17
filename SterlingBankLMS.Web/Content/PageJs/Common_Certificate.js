$.get('/PartialViews/Common/_certificateList.html', function (template) {
    $('#PartialTemplates').append(template);
});

$('#CertificateContainer').on('click', '.hider', function () {
    $('.hiderBody').hide();
    $('.hiderHead').css("background", "#aaa");
    $('.hiderHead i').html("keyboard_arrow_down");
    $('.hiderHead i', this).html("keyboard_arrow_up");
    $('.hiderHead', this).css("background", "#4286f4");
    $('.hiderBody', this).fadeIn();
});

$('#CertificateContainer').pagination({
    dataSource: '/api/certificate/getPagedUserCertificates',
    locator: 'data',
    totalNumberLocator: function (response) {
        if ($('#CertificateItems')[0].hasAttribute("data-total"))
        { return parseInt($('#CertificateItems').attr("data-total")); }
        else {
            $('#CertificateItems').attr("data-total", response.recordsTotal);
            return response.recordsTotal;
        }

    },
    pageSize: 10,
    className: 'paginationjs-theme-blue paginationjs-big',
    ajax: {
        beforeSend: function () {
            var html = '<div class="nofloat">Fetching Data <i class="fa fa-spinner fa-pulse fa-fw nofloat"></i></div>';
            $('#CertificateItems').html(html);
        }
    },
    callback: function (data, pagination) {
        var template = document.getElementById('CertificateList').innerHTML;
        var output = Mustache.render(template, { array: data });
        $('#CertificateItems').html(output);
    }
})