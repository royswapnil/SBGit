
//// show error or success messages to page

function LoadPageError(data) {
    var template = document.getElementById('ResponseMessageTemplate').innerHTML;
    var output = Mustache.render(template, data);
    return output;
}

function ShowConfirm(message) {
    return $.confirm({
        icon: 'fa fa-question',
        theme: 'material',
        closeIcon: true,
        animation: 'scale',
        type: 'orange',
        content: '' + message == undefined ? 'Are you sure to continue?' : message,
        buttons: {
            'confirm': {
                text: 'Proceed',
                btnClass: 'btnBlue'
            },
            cancel: {
                text: 'Cancel'
            }

        }
    });

}

function ShowNotie(data) {

    var title = (data.HasError == null || data.HasError == undefined || data.HasError == true) ? "Error" : "Success";
    var icon = (data.HasError == null || data.HasError == undefined || data.HasError == true) ? "fa fa-warning" : "fa fa-ok";
    var type = (data.HasError == null || data.HasError == undefined || data.HasError == true) ? "red" : "green";

    var message = data.Message;
    if (data.Message == undefined || data.Message == null) {
        message = "An error occurred when processing your request";
        if (data.HasError == false)
            message = "Success";
    }

    $.alert({
        title: title,
        icon: icon,
        type: type,
        content: message,
    });

    //if (data.HasError == null || data.HasError == undefined) {
    //    notie.alert({ type: 3, text: data.Message, stay: true });
    //    return;
    //}

    //if (data.HasError) {
    //    notie.alert({ type: 3, text: data.Message, stay: true });
    //}
    //else {
    //    notie.alert({ type: 1, text: data.Message, stay: true });
    //}
}


/// FILE UPLOADS

function GetFileType(Extension) {
    var type;
    switch (Extension) {
        case "docx": case "doc": type = "document"; break;
        case "pdf": type = "document"; break;
        case "pptx": case "ppt": type = "presentation"; break;
        case "xls": case "xlsx": case "csv": type = "excel"; break;
        case "txt": type = "text"; break;
        case "htm": case "html": case "xml": type = "webpage"; break;
        case "mp4": case "avi": case "mov": case "webm": case "mkv": case "flv": case "amv": case "ogg":
        case "mpg": case "mpeg": case "m4v": case "3gp": case "wmv": type = "video"; break;

        case "jpg": case "jpeg": case "gif": case "png": type = "image"; break;
        //default: fileInfo.Icon = "fa-file-o"
    }
    return type;
}


var DataURLFileReader = {
    read: function (file, i, callback) {
        var reader = new FileReader();
        var fileInfo = {
            name: file.name,
            type: file.type,
            fileContent: null,
            id: i,
            fileupload: file,

            size() {
                var FileSize = 0;
                if (file.size > 1048576) {
                    FileSize = Math.round(file.size * 100 / 1048576) / 100 + " MB";
                }
                else if (file.size > 1024) {
                    FileSize = Math.round(file.size * 100 / 1024) / 100 + " KB";
                }
                else {
                    FileSize = file.size + " bytes";
                }
                return FileSize;
            },

            chunk() {
                var chunk = false;
                if (file.size > 524288)
                    chunk = true;
                return chunk;
            }
        };

        var extType = GetFileType(file.name.split('.').pop().toLowerCase().trim());

        fileInfo.type = extType;
        if (!extType) {
            fileInfo.type = "invalid";
            fileInfo.Error = true;
            callback("file type not allowed", fileInfo);
            return;
        }

        reader.onload = function () {
            fileInfo.fileContent = reader.result;
            callback(null, fileInfo);
        };
        reader.onerror = function () {
            callback(reader.error, fileInfo);
        };
        reader.readAsDataURL(file);
    }
};


function ChunkFile(TargetFile) {
    // create array to store the buffer chunks
    var FileChunk = [];
    // the file object itself that we will work with
    var file = TargetFile;
    // set up other initial vars
    var MaxFileSizeMB = 0.5;
    var BufferChunkSize = MaxFileSizeMB * (1024 * 1024);
    var ReadBuffer_Size = 1024;
    var FileStreamPos = 0;
    // set the initial chunk length
    var EndPos = BufferChunkSize;
    var Size = file.size;

    // add to the FileChunk array until we get to the end of the file
    while (FileStreamPos < Size) {
        // "slice" the file from the starting position/offset, to  the required length
        FileChunk.push(file.slice(FileStreamPos, EndPos));
        FileStreamPos = EndPos; // jump by the amount read
        EndPos = FileStreamPos + BufferChunkSize; // set next chunk length
    }
    // get total number of "files" we will be sending

    return FileChunk;

}


function GetQuestionAnswerType(answerType) {

    var returnData;

    if (parseInt(answerType)) {
        switch (answerType) {
            case 1: returnData = "textbox"; break;
            case 2: returnData = "radio"; break;
            case 3: returnData = "select"; break;
            case 4: returnData = "checkbox"; break;
        }
    }
    else {
        switch (answerType) {
            case "textbox": returnData = 1; break;
            case "radio": returnData = 2; break;
            case "select": returnData = 3; break;
            case "checkbox": returnData = 4; break;
        }
    }
    console.log(returnData);
    return returnData;
}

/// bind plugins

function BindHtmlEditor($element, content = null) {
    // using summernote
   
        $element.summernote({
            minHeight: 100, fontNames: ['Century Gothic', 'Arial', 'Arial Black',
                'Comic Sans MS', 'Courier New', 'Helvetica', 'Impact', 'Tahoma', 'Times New Roman', 'Verdana'],
            fontNamesIgnoreCheck: ['Century Gothic'], fontname: 'Century Gothic',
            toolbar: [
                ['paragragh', ['style']],
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['style', ['fontname']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']]
                ['button', ['table']],
                ['misc', ['fullscreen', 'codeview', 'undo', 'redo','help']]
                
            ]
    });

        if (content != null) {
        $element.summernote('code', content);
      
    }
       
}

function DestroyHtmlEditor($element) {
    if ($element.data().summernote != undefined) {
        $element.summernote('destroy');
      //  $element.parent().find('.note-editor').remove();
    }
      
}

function RemoveHtmlEditor($element) {
    $element.find('.note-editor').remove();
}

function BindSelectBox(element) {
    // using select2 
}

/***********************

Author : Mishael Kama
Date: 5th January, 2018

********************/
$(document).ready(function () {

    //// Bind Plugins 

    /// HTML SUMMERNOTE EDITOR





    //// TOGGLE SCREEN VIEW OPTIONS
    $('.viewManagePanel').on('click', function () {
        $('#listPanel').hide();
        $('#managePanel').fadeIn();
        $(this).hide();
        $('.viewListPanel').show();
    });


    $('.viewListPanel').on('click', function () {
        $('#managePanel').hide();
        $('#listPanel').fadeIn();
        $(this).hide();
        $('.viewManagePanel').show();
    });


    //sticky headers
    // Hide Header on on scroll down
    var didScroll;
    var lastScrollTop = 0;
    var delta = 5;
    var navbarHeight = $('header.second').outerHeight();
    $(window).scroll(function (event) {
        didScroll = true;
    });
    setInterval(function () {
        if (didScroll) {
            hasScrolled();
            didScroll = false;
        }
    }, 250);
    //sticky headers
    // Hide Header on on scroll down
    var didScroll;
    var lastScrollTop = 0;
    var delta = 5;
    var navbarHeight = $('header.second').outerHeight();
    $(window).scroll(function (event) {
        didScroll = true;
    });
    setInterval(function () {
        if (didScroll) {
            hasScrolled();
            didScroll = false;
        }
    }, 250);

    function hasScrolled() {
        var st = $(this).scrollTop();
        // Make sure they scroll more than delta
        if (Math.abs(lastScrollTop - st) <= delta) return;
        // If they scrolled down and are past the navbar, add class .header_up.
        // This is necessary so you never see what is "behind" the navbar.
        if (st > lastScrollTop && st > navbarHeight) {
            // Scroll Down
            $('header.second').addClass('header_up');
            $('.pallete1').addClass('pallete_up');
        } else {
            // Scroll Up
            if (st + $(window).height() < $(document).height()) {
                $('header.second').removeClass('header_up');
                $('.pallete1').removeClass('pallete_up');
            }
        }
        lastScrollTop = st;
    }
    //end of sticky headers






    //menu Button
    $('.menuBtn1').click(function () {
        $('.pallete1').toggleClass('pallete_big');
        $('.menuTab1').toggleClass('shrink');
    });


    //notification, msg and profile pop up


    $('.msg').click(function () {

        $('.msgBox').toggleClass("showPop");
        $('.notyBox, .probBox').removeClass("showPop");

    });

    $('.noty').click(function () {

        $('.notyBox').toggleClass("showPop");
        $('.msgBox, .probBox').removeClass("showPop");

    });

    $('.user').click(function () {

        $('.probBox').toggleClass("showPop");
        $('.msgBox, .notyBox').removeClass("showPop");

    });

    //add course and topics

    $('.fab3').click(function () {
        $('.relay1').show();
        $('.relay3').hide();
    });

    $('.fab4').click(function () {
        $('.relay3').show();
        $('.relay1').hide();
    });


    $('select.typeUpload').change(function () {

        var nag = $(this).val();

        if (nag == "Text" || nag == "Quiz") {
            $('.relay4').show();

            $('.fab3').prop("checked", true);
            $('.fab4').prop("checked", false);
            $('.relay2, .relay3').hide();
            $('.relay1').show();
        }
        else {
            $('.relay4').hide();
            $('.relay2').show();
            $('.relay1, .relay3').hide();
        }

    });



    $('.nabi').click(function () {
        $('.nabiBd').removeClass('harden');
        $('.nabi .nabiHd .topicNm i').html("keyboard_arrow_down");
        $('.nabiBd', this).addClass('harden');
        $('.nabiHd .topicNm i', this).html("keyboard_arrow_up");
    });




    $('.select11').change(function () {
        var select1 = $('.select11').val();

        if (select1 == "") {
            $('.select12, .select13').prop("disabled", true);
        }
        else {
            $('.select12').prop("disabled", false);
        }
    });


    $('.select12').change(function () {
        var select2 = $('.select12').val();

        if (select2 == "") {
            $('.select13').prop("disabled", true);
        }
        else {
            $('.select13').prop("disabled", false);
        }
    });







    $('.nabiHd i.editMe').click(function () {
        $('.backdrop, .changeModule').fadeIn();
    });

    //$('.deleteMe').click(function () {
    //    $('.backdrop, .deleteSome').fadeIn();

    //});

    //$('.addNewLearningArea').click(function () {
    //    $('.backdrop, .learningArea').fadeIn();
    //});

    $('.editLA').click(function () {
        $('.backdrop, .editLearningArea').fadeIn();
    });


    $('.deleteLA').click(function () {
        $('.backdrop, .deleteLearningArea').fadeIn();
    });





    $('.tabs .tab-links a').on('click', function (e) {
        var currentAttrValue = $(this).attr('href');
        // Show/Hide Tabs
        $('.tabs ' + currentAttrValue).slideDown(400).siblings().slideUp(400);
        // Change/remove current tab to active
        $(this).parent('li').addClass('active').siblings().removeClass('active');
        e.preventDefault();
        $('.tabs ' + currentAttrValue).trigger('tabbed', e);
    });




    //backdrop remove

    $('.backdrop').click(function () {

        $('.backdropBox, .backdrop').fadeOut();

    });


});