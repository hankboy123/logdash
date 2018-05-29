//20180420
//create by chen zhe qi
(function ($) {
    $.fn.ControlModal = function (options, ajaxoptions) {
        var $this = $(this);
        var myDate = new Date();
        //获取当前年
        var year = myDate.getFullYear();
        //获取当前月
        var month = myDate.getMonth() + 1;
        //获取当前日
        var date = myDate.getDate();
        var h = myDate.getHours();       //获取当前小时数
        var m = myDate.getMinutes();     //获取当前分钟数
        var s = myDate.getSeconds();
        var mm = myDate.getMilliseconds();
        options.formId = year.toString() + month.toString() + date.toString() + h.toString() + m.toString() + s.toString() + mm.toString();
        options.ModalId = "modal" + options.formId;
        options.CloseId = "close" + options.formId;
        options.SaveId = "save" + options.formId;
        options.ContentId = "content" + options.formId;
        options.messageId = "messageId" + options.formId;
        //创建一些默认值，拓展任何被提供的选项
        var settings = $.extend({
            htmlUrl: "",
            submitUrl: "",
            isShowSumitBtn: true,
            confirmText: "确认保存？",
            //width: "",
            //height: "",
            onClose: function () { }
        }, options);
        var html = a.InitHtml(settings);
        $this.append(html);
        //加载页面
        $("#" + settings.ContentId).load(settings.htmlUrl, function () {
            var savebtn = $('<button type="button" id="' + settings.SaveId + '" class="btn btn-primary">保存</button>');
            var cannelbtn = $('<button type="button" id="' + settings.CloseId + '" class="btn btn-default" data-dismiss="modal" >关闭</button>');
            var vaildMessageArea = $('<div id="' + settings.messageId + '" class="text-left" style="color:red;"></div>');
            var footbtn = $('<div class="modal-footer"></div >');
            footbtn.append(vaildMessageArea);
            //判断是否需要显示保存按钮
            if (settings.isShowSumitBtn)
                footbtn.append(savebtn);
            footbtn.append(cannelbtn);
            $("#" + settings.ContentId).append(footbtn);
            //点击标题可以拖动
            $("#" + settings.ContentId).draggable({ handle: 'div.modal-header' });
            //绑定关闭按钮
            $("#" + settings.CloseId).click(function () {
                a.btnCannelClose(settings);
            });
            //绑定保存按钮事件
            $("#" + settings.SaveId).click(function () {
                $.fn.confirmBox({
                    str: settings.confirmText,
                    successfunc: function () {
                        a.ajaxSubmitForm(settings, ajaxoptions);
                    },
                    onClose: function () { }
                });
            });
            //绑定模态框关闭事件
            $("#" + settings.ModalId).on('hide.bs.modal',
                function () {
                    $("#" + settings.formId).remove();
                    //alert('嘿，我听说您喜欢模态框...');
                });
            $("#" + settings.ModalId).modal("toggle");

        });
    };
    var a = {
        //组织HTML
        InitHtml: function (options) {
            var form = $('<form id="' + options.formId + '"method="POST" enctype="multipart/form-data" name="ImgForm" action="' + options.Action + '" ></form>');
            var modal = $('<div class="modal"  id="' + options.ModalId + '"></div>')
            var dialog = $('<div class="modal-dialog" ></div>')
            var content = $('<div class="modal-content"  id="' + options.ContentId + '"></div>');
            //content.append(footbtn);
            dialog.append(content);
            modal.append(dialog);
            form.append(modal);
            return form;
        },
        //关闭文本框
        Close: function (options) {
            $("#" + options.ModalId).modal('hide');
            $("#" + options.formId).remove();
            $(document.body).removeClass("modal-open");
            options.onClose();
            //
        },
        //取消按钮点击关闭文本框
        btnCannelClose: function (options) {
            $("#" + options.ModalId).modal('hide');
            $("#" + options.formId).remove();
            $(document.body).removeClass("modal-open");

            //
        },
        ajaxSubmitForm: function ajaxSubmitForm(options, ajaxoptions) {
            var option = {
                url: options.submitUrl,
                method: 'post',
                dataType: 'json',
                onSubmit: function () { },
                success: function (data) {
                    if ("200" == data.statusCode) {
                        //$("#" + options.messageId).html(data.message);
                        $.fn.tipSuccess(data.message);

                        a.Close(options);
                    }
                    else {
                        $("#" + options.messageId).html("错误信息：<br/>" + data.message);
                        $.fn.tipError("<h2>错误信息：</h2>" + data.message);
                        return;
                    }
                },
                error: function (data) {
                    alert("失败,请联系管理员！");
                }
            }
            var settings = $.extend(option, ajaxoptions);
            settings.onSubmit();
            $("#" + options.formId).ajaxSubmit(settings);
            //ajax方法使用
            //将form序列化成json
            //var kv = $("#" + options.formId).serializeArray();
            //var json = {};
            //for (var i = 0; i < kv.length; i++) {
            //    var temp = kv[i];
            //    //判断是否有重复的用于.net Htmlcheckbox会生成个同名的隐藏域
            //    if (json['"' + temp.name + '"'] != null) {
            //        if (temp.value == true)
            //            json['"' + temp.name + '"'] = '"' + temp.value + '"';
            //    }
            //    else
            //        json['"' + temp.name + '"'] = '"' + temp.value + '"';
            //}
            //settings.data = JSON.stringify(json);
            //console.log(settings.data);
            //$.ajax(settings);

        }
    };
})(jQuery);

//弹出提示浮动条
(function ($) {
    $.fn.tipSuccess = function (str) {
        var myDate = new Date();
        //获取当前年
        var year = myDate.getFullYear();
        //获取当前月
        var month = myDate.getMonth() + 1;
        //获取当前日
        var date = myDate.getDate();
        var h = myDate.getHours();       //获取当前小时数
        var m = myDate.getMinutes();     //获取当前分钟数
        var s = myDate.getSeconds();
        var mm = myDate.getMilliseconds();
        var Id = year.toString() + month.toString() + date.toString() + h.toString() + m.toString() + s.toString() + mm.toString();
        if ($("#temp-toptip").length > 0 == false) {
            var toptip = $('<div id="temp-toptip" class="text-center center-vertical" style="height:auto;position:fixed;width:100%;z-index:99999;top:90%;left:0px;"></div>');
            $(document.body).append(toptip);
        }
        var successdiv = $('<div id="' + Id + '" class="text-center center-vertical" style="color:white;font-size:20px;background:rgb(0, 204, 17)">' + str + '</div>');
        $("#temp-toptip").append(successdiv);
        $("#" + Id).show(1000, function () {
            $("#" + Id).fadeOut(1000, function () {
                $("#" + Id).remove();
            });
        });


    };
    $.fn.tipError = function (str) {
        var myDate = new Date();
        //获取当前年
        var year = myDate.getFullYear();
        //获取当前月
        var month = myDate.getMonth() + 1;
        //获取当前日
        var date = myDate.getDate();
        var h = myDate.getHours();       //获取当前小时数
        var m = myDate.getMinutes();     //获取当前分钟数
        var s = myDate.getSeconds();
        var mm = myDate.getMilliseconds();
        var Id = year.toString() + month.toString() + date.toString() + h.toString() + m.toString() + s.toString() + mm.toString();
        if ($("#temp-toptip").length > 0 == false) {
            var toptip = $('<div id="temp-toptip" class="text-center center-vertical" style="height:auto;position:fixed;width:100%;z-index:99999;top:90%;left:0px;"></div>');
            $(document.body).append(toptip);
        }
        var successdiv = $('<div id="' + Id + '" class="text-center center-vertical" style="color:white;font-size:20px;background:#d15b47;">' + str + '</div>');
        $("#temp-toptip").append(successdiv);
        $("#" + Id).show(1000, function () {
            $("#" + Id).fadeOut(3000, function () {
                $("#" + Id).remove();
            });
        });
    };
    //successfunc点击确认后调用
    $.fn.confirmBox = function (option) {
        //创建一些默认值，拓展任何被提供的选项
        var settings = $.extend({
            str: "",
            successfunc: function () {
            },
            onClose: function () { }
        }, option);
        var hiddendiv = $('<div id="confirmbackdrophidden" style="z-index:999999;" class="modal-backdrop in"></div>');
        var confirmBox = $('<div class="alert alert-warning text-center center-vertical" style="height:auto;position:fixed;width:100%;z-index:2147483647;top:50%;left:0px;"></div>');
        confirmBox.append($("<div class='row' style='font-size:20px;'>" + settings.str + "</div><br/>"))
        var ok = $("<button class='btn btn-danger'>确认</button><span>&nbsp;&nbsp;</span>");
        var cannel = $("<button class='btn btn-info'>取消</button>");
        var btnfooter = $('<div class="row"></div>');
        btnfooter.append(ok);
        btnfooter.append(cannel);
        confirmBox.append(btnfooter);
        ok.click(function () {
            settings.successfunc();
            settings.onClose();
            hiddendiv.remove();
            confirmBox.remove();
        });
        cannel.click(function () {

            settings.onClose();
            hiddendiv.remove();
            confirmBox.remove();
        });
        $(document.body).append(hiddendiv);
        $(document.body).append(confirmBox);
    };
})(jQuery);




