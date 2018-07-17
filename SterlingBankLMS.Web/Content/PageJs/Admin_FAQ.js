$(document).ready(function () {
    ///GET MUSTACHE TEMPLATES FOR BINDING

    $.get('/PartialViews/Admin/FAQContent.html', function (template) {
        $('#PartialTemplates').append(template);

        $.get('/api/faq/GetFAQs', function (data) {
            var template = document.getElementById('FAQContent').innerHTML;
            var output = Mustache.render(template, { array: data.Result });
            $('#FAQContentArea').prepend(output);
            $('#FAQContentArea').sortable({
                items: '.nabi:not([data-id="0"])',
                cancel: 'input,.nabiBd > *',

                update: function (event, ui) {
                    $('#reSortFAQ').addClass('btnBlue');
                    $('#reSortFAQ').removeAttr('disabled');
                }
            });
            BindHtmlEditor($('#FAQContentArea').find('textarea'));

        });
    });





    //// Add New FAQ 
    $('#addNewFAQ').on('click', function () {
        var $button = $(this);
        var template = document.getElementById('FAQContent').innerHTML;
        var length = $('#FAQContentArea').find('.nabi').length
        var output = Mustache.render(template, { array: [{ IsNew: true, Id: 0 }] });
        $button.before(output);
        BindHtmlEditor($('#FAQContentArea').find('.nabi:last textarea'));
    });


    ////FAQ Collapsable Action

    $('#FAQContentArea').on('click', '.module-action', function (e) {

        if ($(e.target).is(":input") || $(e.target).is('.close-edit-nabi'))
            return;

        var $item = $(this);
        $item.toggleClass('closed');
        $item.toggleClass('opened');
        $item.hasClass('closed') ? $(this).find('i.material-icons').html('keyboard_arrow_up') : $(this).find('i.material-icons').html('keyboard_arrow_down');
        $item.closest('.nabi').find('.nabiBd').toggleClass('harden');
        $(this).closest('.nabi').find('.courseLesson.bordered-section').toggleClass('collasped');

    });


    ////SAVE FAQ

    $('#FAQContentArea').on('click', '.save-faq-content', function () {
        var $button = $(this);
        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');
        var faqObject = new Object();

        var $contentPanel = $button.closest('.nabi');
        $contentPanel.find('label.error').remove();

        faqObject.Id = $contentPanel.attr('data-id');

        var isValid = true;
        var title = String($contentPanel.find('.topicNm input').val())
        var desc = String($contentPanel.find('textarea.lessonDesc').val());

        if (title.trim() == "") {
            $contentPanel.find('.topicNm input').after('<label class="error errorHolder">Required.</label>');
            isValid = false;
        }

        if (desc.trim() == "") {
            $contentPanel.find('textarea.lessonDesc').parent().find('label:first').after('<label class="error errorHolder">This field is required.</label>');
            isValid = false;
        }

        faqObject.Description = desc;
        faqObject.Title = title;


        if (!isValid) {
            ShowNotie({ HasError: true, Message: "You have some form errors. Please verify and try again" });
            $button.removeAttr('disabled');
            $button.html('Save Changes');
            return;
        }


        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/api/faq/AddorUpdateFAQ",
            data: faqObject,
            success: function (data) {

                $contentPanel.attr('data-id', data.Result.Id);
                $contentPanel.find('.courseSlate').click();
                ShowNotie(data);
                var $row = $contentPanel.find('.courseSlate');
                $contentPanel.find('.courseLesson').addClass('collasped');
                $contentPanel.find('.nabiBd').removeClass('harden');
                $row.find('.topicNm span').html(data.Result.Title);
                $row.find('.topicNm input').hide();
                $row.find('.topicNm .close-edit-nabi').hide();
                $row.find('.topicNm span').show();
                $row.find('.topicAction .edit-nabi').show();
                $contentPanel.addClass('closed');
                $contentPanel.removeClass('opened');
                $contentPanel.find('.topicNm i.material-icons').html('keyboard_arrow_up');
            },

            error: function (data) {
                var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                ShowNotie({ HasError: true, Message: message });
            },
            complete: function (data) {
                $button.removeAttr('disabled');
                $button.html('Save Changes');
            }
        });




    });


    //// Edit FAQ 
    $('#FAQContentArea').on('click', '.edit-nabi', function () {

        var $row = $(this).closest('.courseSlate');
        $row.find('.topicNm input').show();
        $row.find('.topicNm span').hide();
        $row.find('.topicNm .close-edit-nabi').show();
        $(this).closest('.nabi').find('.nabiBd').addClass('harden');
        $(this).closest('.nabi').find('.courseLesson.bordered-section').removeClass('collasped');
        $row.find('.topicAction .edit-nabi').hide();
    });


    //// Close Edit or Add FAQ
    $('#FAQContentArea').on('click', '.close-edit-nabi', function () {

        var $row = $(this).closest('.courseSlate');
        $row.find('.topicNm input').val($row.find('.topicNm span').html());
        $row.find('.topicNm input').hide();
        $row.find('.topicNm .close-edit-nabi').hide();
        $row.find('.topicNm span').show();
        $row.find('.topicAction .edit-nabi').show();
        $(this).closest('.nabi').find('.nabiBd').removeClass('harden');
        $(this).closest('.nabi').find('.courseLesson.bordered-section').addClass('collasped');
    });


    //// Delete FAQ
    $('#FAQContentArea').on('click', '.delete-module', function () {
        var $button = $(this);
        var $row = $(this).closest('.nabi');
        if ($row.attr("data-id") == "0") {
            $row.remove();
        }
        else {
            var id = $row.attr("data-id");
            if (!parseInt(id) && parseInt(id) <= 0) {
                return;
            }

            var $confirm = ShowConfirm();
            $confirm.onAction = function (btnName) {

                if (btnName == "cancel")
                    return;

                var prev = $button[0].outerHTML;
                $button.attr('disabled', 'disabled');
                $button.find('i').removeClass('material-icons');
                $button.find('i').html('');
                $button.find('i').addClass('fa fa-circle-o-notch fa-spin nofloat');

                $.ajax({
                    cache: false,
                    async: true,
                    type: "Delete",
                    url: "/api/faq/Delete?Id=" + id,

                    success: function (data) {
                        $row.remove();
                    },

                    error: function (data) {
                        var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                        ShowNotie({ HasError: true, Message: message });
                        $button.replaceWith(prev);
                    }
                });

            }
            return false;

        }

    });

    //// RESORT FAQ
    $('#reSortFAQ').on('click', function () {

        var $button = $(this);

        var faqList = [];
        $('#FAQContentArea').find('.nabi:not([data-id="0"])').each(function (i, item) {
            var faqObject = {
                Id: $(item).attr('data-id'),
                SortOrder: i + 1
            }
            faqList.push(faqObject);
        });

        if (faqList.length == 0) {
            $button.removeClass('btnBlue');
            $button.attr('disabled', 'disabled');
        }
        
        var $confirm = ShowConfirm();
        $confirm.onAction = function (btnName) {

            if (btnName == "cancel")
                return;

            var prev = $button.html();
            $button.attr('disabled', 'disabled');
            $button.html('Processing <i class="fa fa-circle-o-notch fa-spin nofloat"></i>');

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "/api/faq/ResortFAQItems",
                data: { "": faqList },
                success: function (data) {
                    $button.removeClass('btnBlue');
                },

                error: function (data) {
                    $button.removeAttr('disabled');
                    var message = data.status == 400 ? data.responseJSON.Message : data.statusText;
                    ShowNotie({ HasError: true, Message: message });
                },
                complete: function (data) {
                    $button.html(prev);
                }
            });
        }
    });

});


