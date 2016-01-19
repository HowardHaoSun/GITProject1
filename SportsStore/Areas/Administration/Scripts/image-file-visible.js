(function ($) {
    $.hideNotice = function () {
        if ($("#notice").is(":visible")) {
            //console.log("Exsit");
            $("#notice").animate({ opacity: 0 }, 3000,function(){
                $(this).css({display:"none"})});
           // $("#notice").css({ display: "none" });
        }
    };
    $.imageFileVisible = function (options) {
        // 默认选项

        var defaults = {
            //包裹图片的元素
            wrapSelector: null,
            //<input type=file />元素
            fileSelector: null,
            width: '100%',
            height: 'auto',
            errorMessage: "不是图片"
        };
        // Extend our default options with those provided.    
        var opts = $.extend(defaults, options);
        $(opts.fileSelector).on("change", function () {
            if ($("#image-from-remote") != null)
            {
                $("#image-from-remote").attr("style","display:none");
            }
            var file = this.files[0];
            $("#upload-file-info").html($(this).val());
            var imageType = /image.*/;
            if (file.type.match(imageType)) {
                var reader = new FileReader();
                reader.onload = function () {
                    var img = new Image();
                    
                    img.src = reader.result;
                    $(img).attr("class", "img-thumbnail");
                    $(img).attr("width",opts.width);                    
                    $(img).attr("height", opts.height);
                    $(opts.wrapSelector).append(img);
                };
                reader.readAsDataURL(file);
            } else {
                console.log(opts.errorMessage);
            }
        });
    };
})(jQuery);