$(document).ready(function () {

    $('input:file').on('change', function (e) {
        fileuploader(e, this);
    });

    $('.uploadFile').on('click', function () {

        var $button = $(this);
       
        var $fileUpload = $button.prev();
        
        file = $fileUpload.data('file');

        if (file == undefined || file == null)
            return ShowNotie({ HasError: true, Message: "No file selected" });

        $button.attr('disabled', 'disabled');
        $button.html('Processing <i class="fa fa-circle-o-notch fa-spin pull-right"></i>');

        var FileChunk = [];
        if (file.chunk())
            FileChunk = ChunkFile(file.fileupload);
        else
            FileChunk.push(file.fileupload);


        var TotalParts = FileChunk.length;
        var PartCount = 0;

        // loop through, pulling the first item from the array each time and sending it


        var totalFiles = FileChunk.length;
        while (chunk = FileChunk.shift()) {
            PartCount++;

            var FilePartName;

            if (file.chunk()) {
                // file name convention
                FilePartName = file.name + ".part_" + PartCount + "." + TotalParts;

            }
            else {
                FilePartName = file.name;

            }
            var FD = new FormData();
            FD.append('IsChunked', file.chunk());
            FD.append('file', chunk, FilePartName);

            var $messageText = $button.parent().find('messageText');
            // send the file

            $.ajax({
                type: 'POST',
                url: "/api/FileUpload/UploadFile",
                cache: false,
                data: FD,
                contentType: false,
                processData: false,
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowNotie({ HasError: true, Message: xhr.responseJSON });
                    $button.removeAttr('disabled');
                    $button.html('Upload');
                    return;
                },
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    //Download progress
                    xhr.addEventListener("progress", function (evt) {
                        console.log(evt.lengthComputable); // false
                        if (evt.lengthComputable) {
                            var complete = (event.loaded / event.total * 100 | 0) / ((totalFiles - PartCount) + 1);
                            //  $contentPanel.closest('.courseSlate').find('.messageText span').html(complete);
                        }
                    }, false);
                    return xhr;
                },
                beforeSend: function () {

                },
                complete: function () {

                },
                success: function (data) {
                    if (data.Result !== null) {
                       
                        updateFileUploadNotification(data.Result);
                    }
                }
            });



        }
    });
});

function updateFileUploadNotification(filePath) {
    if (filePath === null) {
        return ShowNotie({ HasError: false, Message: "Invalid request" });
    }

    var dataSent = { FilePath: filePath, uploadType: 1 }

    $.ajax({
        cache: false,
        async: true,
        type: "POST",
        url: "/api/FileUpload/UpdateBulkUploadNotification",
        data: dataSent,
        success: function (data) {

            ShowNotie(data);
        },

        error: function (data) {
            var message = data.status === 400 ? data.responseJSON.Message : data.statusText;
            ShowNotie({ HasError: true, Message: message });
        },
        complete: function (data) {
            $item.removeAttr('disabled');
            $item.html("Upload");
        }
    });
}

function fileuploader(evt, elem) {
    evt.stopPropagation();
    evt.preventDefault();

    selectedFiles = evt.target.files || evt.dataTransfer.files;
    if (selectedFiles) {

        for (var i = 0; i < selectedFiles.length; i++) {

            DataURLFileReader.read(selectedFiles[i], i, function (err, fileInfo) {

                if (err !== null) {
                    ShowNotie({ HasError: true, Message: err });
                }
                else {
                    if (fileInfo.type === "excel") {
                        $(elem).data('file', fileInfo);
                    }
                    else {
                        $(elem).val("");
                        ShowNotie({ HasError: true, Message: "File type not supported" });
                    }
                }
            });
        }


    }
}