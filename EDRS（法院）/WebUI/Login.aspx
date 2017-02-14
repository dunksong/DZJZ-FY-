<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebUI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>用户登录</title>
    <link href="/Styles/defaultPage.css" rel="stylesheet" type="text/css" />
    <link href="/ligerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="LigerUI/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="LigerUI/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="LigerUI/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
  
    <style type="text/css">
        .tableform td
        {
               padding: 5px 0 5px;
        }
        .textbox
        {
            border: 0px;
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }
        .textbox .textbox-text
        {
            background: rgb(236,245,250);
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }
        #aLogin
        {
            background: url(/images/bt_dl_normal.png) no-repeat -13px 0;
            border-radius: 10px;
        }
        #aLogin:hover
        {
            background: url('/images/bt_dl_hover.png') no-repeat -13px 0;
        }
        
        #aclose
        {
            background: url('/images/bt_q_normal.png') no-repeat -13px 0;
            border-radius: 10px;
        }
        #aclose:hover
        {
            background: url('/images/bt_qx_hover.png') no-repeat -13px 0;
        }
        
        .l-tree-icon
        {
            background: url('/images/icons/3.png') no-repeat !important;
            background-position: center center !important;
        }
        .l-text
        {
            height: 32px;
            border: 0px;
        }
        
        .l-trigger, .l-trigger-hover, .l-trigger-pressed
        {
            height: 30px;
            right: 0;
            top: 0px;
        }
        .l-text-over
        {
         /*   background: none; */
        }
        .l-text.l-text-combobox {
                border: 1px solid #ccc;
            }
            
            .l-box-select-inner {
                    margin-top: -2px;
                }
    </style>
</head>
<body style="margin: 0; background: #0E7ABA;">
    <div style="float: right; font-size: 8px; color: #0E7ABA;">
        <%=this.vr %>
    </div>
    <form id="loginForm">
    <div id="floater">
    </div>
    <div id="centent" style="border: 0; width: 660px; height: 410px;/* background: url('/images/di.png') no-repeat;*/    background: url(/images/Newadd/body.png) no-repeat -74px -40px;">
        <div style="bottom: 29px; display: inline-block; color: #787878; font-size: 12px;
            left: 143px; position: absolute; width: auto;">
        </div>
        <table class="tableform" align="center" style="text-align: center; position: absolute;
            width: 291px; margin-top: 184px;margin-left: 192px;font-size: 10pt" cellpadding="0"
            cellspacing="0" border="0">
            <tr>
                <td style="width: 33px;">
                </td>
                <td align="left" colspan="2">
                    <input id="tree_select" name="tree_select" data-options="required:true" missingmaessage="必须选择所属院"
                        style="width: 237px; height: 32px;    margin: 0;" value="">
                    <input type="hidden" name="tree_select_hid" id="tree_select_hid" value="" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="2">
                    <input id="txtUser" type="text" class="input2 textbox" missingmaessage="用户名必须输入,最多20字符。"
                        validtype="length[1,20]" data-options="required:true" style="width: 257px; height: 32px;
                        border: 1px solid #ccc;" name="txtUser" value="" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="2">
                    <input id="txtPwd" type="password" class="input2 textbox" missingmaessage="密码必须输入,最多20字符。"
                        validtype="length[1,20]" data-options="required:true" style="width: 257px; height: 32px; border: 1px solid #ccc;"
                        name="txtPwd" value="11111111" />
                </td>
            </tr>
            <%--<tr style=" display:none;">
                        <td colspan="2" align="left" id="tdcode" style="width: 162px;">
                            <input id="txtVCode" hid="cccc" type="text" class="input2 easyui-textbox" missingmaessage="验证码必须与图片字符相同"
                                validtype="length[5,5]" data-options="required:true" style="width: 155px; height: 32px;"
                                name="txtVCode" value="" />
                        </td>
                        <td align="left" style="padding-left: 7px;">
                            <img id="imgVCode" style="height: 30px;" src="ValidateCode.aspx?GUID=GUID" onclick="getCode();"
                                alt="验证码" title="点击刷新验证码" />
                        </td>
                    </tr>--%>
            <tr>
                <td colspan="3" align="center" style="text-align: left;">
                    <a id="aLogin" style="color: #666666; display: inline-block; height: 21px; margin-top: 8px;
                        margin-right: 29px;margin-left: 27px; padding: 6px 20px 10px 50px; text-decoration: none; width:45px;"
                        href="javascript:void(0);"></a><a id="aclose" style="color: #666666; display: inline-block;
                            height: 21px; margin-top: 8px;margin-left: 5px; padding: 6px 20px 10px 50px; text-decoration: none;
                            width: 45px;" href="javascript:void(0);"></a>
                </td>
            </tr>
        </table>
        <%--<div id="mainPage" class="lead">
            <div class="left" style="width: 0px;">
                <span class="title"></span>
                <br />
                <span></span>
            </div>
            <div class="middle" style="background: none;">
            </div>
            <div class="right" style="float: none; width: auto; text-align: left; position: relative;">
            </div>
        </div>--%>
    </div>
    <script type="text/javascript" language="javascript">
        var dwbm_tree;
        $(document).ready(function () {
            ExplorResize();
            window.onresize = ExplorResize;
            //设置验证码验证提示遮挡
            //            $("#tdcode").hover(function () {
            //                $(this).find("span input[type='text']").css("padding-right", "100px");
            //            });

            //            $('#loginForm').form('load', {
            //                txtUser: GetQueryString("login", "UserName")
            //            });


            var dwbm = GetQueryString("login", "UnitOption");
            var dwbmmc = GetQueryString("login", "UnitOptionName");
            var name = GetQueryString("login", "UserName");
            //点击取消
            $("#aclose").click(function () {
                document.getElementById("loginForm").reset();
                if (dwbm && dwbm_tree) {
                    //dwbm_tree.selectValue(dwbm);

                    $("#tree_select").val(dwbmmc);
                    $("#tree_select_hid").val(dwbm);
                }
                if (name)
                    $("#txtUser").val(name);
                else
                    $("#txtUser").val("");
            });

            //下拉部门绑定
            if (name)
                $("#txtUser").val(name);

            $("#tree_select").val("");
            dwbm_tree = $("#tree_select").unitJuris({ url: "/Login.aspx", width: 257 });
            $(".l-trigger-cancel").remove();
            if (dwbm && dwbm_tree) {
                // $("#tree_select_id").val(dwbm);
                //  dwbm_tree.getText();
                // dwbm_tree.setValue(dwbm);
                console.log(dwbmmc);
                $("#tree_select").val(dwbmmc);
                $("#tree_select_hid").val(dwbm);

            } else {
                $("#tree_select").val("");
                $("#tree_select_hid").val("");
            }

            //点击登录按钮
            $("#aLogin").click(function () {
                login();
            });
            //监听回车事件


            $(document).unbind("keydown");
            $(document).bind("keydown", function (event) {
                if (event.keyCode == 13) {
                    if ($(".l-dialog:visible").length > 0)
                        return false;
                    login();
                }
            });
        });
        //登录方法
        function login() {
            var jdata = $('#loginForm').serializeArray();
            jdata[jdata.length] = { name: "action", value: "UserLogin" };
            $.ajax({
                type: "POST",
                url: "Login.aspx",
                data: jdata, //"{'action':'UserLogin', 'data': '" + JSON.stringify($("form").serializeArray()) + "'}",
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                    $.ligerDialog.closeWaitting();
                    //                    if ($("#loginForm").form('enableValidation').form('validate')) {
                    $.ligerDialog.waitting('登录中，请稍后...');
                
                },
                error: function (xhr) {
                    $.ligerDialog.closeWaitting();
                    $.ligerDialog.warn('网络连接错误!');
                    return false;
                },
                success: function (data) {

                    var dt = data; // eval('(' + data.d + ')');
                    if (dt.t == "win") {
                        location.href = "Main.aspx";
                        return false;
                    }
                    else {
                        // getCode();
                        $.ligerDialog.warn(data.v);
                        $.ligerDialog.closeWaitting();
                    }

                }
            });
        }


        function getCode() {
            var gNow = new Date();
            $("#imgVCode").attr("src", "ValidateCode.aspx?x=" + gNow.getSeconds());
        }
        //浏览器大小改变
        function ExplorResize() {
            var iheight = $(window).height();
            var iwidth = $(window).width();

            $('#floater').css("height", iheight / 2);
        }

        //读取cookie值
        function GetQueryString(name, value) {
            var reg = new RegExp("(\;|^)[^;]*(" + name + ")\=([^;]*)(;|$)");
            var r = reg.exec(document.cookie);
            if (r) {
                var cookievalue = r != null ? r[3] : null;
                var re = new RegExp("(^|&)" + value + "=([^&]*)(&|$)");
                var rv = cookievalue.match(re);
                if (rv != null) {
                    try {
                        return decodeURI(rv[2]);
                    } catch (e) {

                    }

                }
            }
            return null;
        }
    </script>
    </form>
</body>
</html>
