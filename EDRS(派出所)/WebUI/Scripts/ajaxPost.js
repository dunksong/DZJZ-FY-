
(function ($) {
    $.AjaxPost = function (defaults) {
        defaults = $.extend({
            url: '',
            data: '',
            send: null,
            success: null,
            error: null,
            complete: null
        }, defaults);
        var maskhtml;
        $.ajax({
            type: "post",
            url: defaults.url,
            data: defaults.data,
            dataType: 'json', timeout: 10000, cache: false,
            beforeSend: function () {
                if (defaults.send)
                    defaults.send();
                // $.ligerDialog.waitting('<div class="l-panel-loading" style="display:block;width:48px;"></div><div style="padding-left:50px;">正在加载数据，请稍后...</div>');
                mask();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (defaults.error)
                    defaults.error();
                var errorText = "";
                if (textStatus == "timeout")
                    errorText = "请求超时,请刷新重试";
                else if (textStatus == "parsererror")
                    errorText = "数据解析错误,请刷新重试";
                else
                    errorText = "数据请求网络错误";
                $.ligerDialog.error("错误：" + errorText);
                return false;
            },
            success: function (data) {
                if (defaults.success) {
                    defaults.success(data);
                }
            }, complete: function () {
                if (defaults.complete)
                    defaults.complete();
                $(".whole-mask").remove();
                $(".whole-mask-txt").remove();

            }
        });
    }
    function mask() {
        var width = 74;
        maskhtml = "<div class=\"whole-mask\" style=\"background: #777 none repeat scroll 0 0;font-size: 1px;height: 100%;left: 0;opacity: 0.25;overflow: hidden;position: absolute;top: 0;width: 100%;z-index: 9001;\"></div><div class=\"whole-mask-txt\" style=\"background:url('/images/loading.gif') no-repeat;text-align:right;width:" + width + "px; position:absolute;left:50%;top:50%;margin-left:-"+(width/2)+"px;margin-top:height/2;z-index: 9002;\" >执行中......</div>";
        $(maskhtml).appendTo("body");
    }

})(jQuery);