<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSYJLogin.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSYJLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        html, body
        {
            margin: 0px;
            padding: 0px;
        }
    </style>
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var winWidth = 0;
            var winHeight = 0;
            function findDimensions() //函数：获取尺寸
            {
                //获取窗口宽度
                if (window.innerWidth)
                    winWidth = window.innerWidth;
                else if ((document.body) && (document.body.clientWidth))
                    winWidth = document.body.clientWidth;
                //获取窗口高度
                if (window.innerHeight)
                    winHeight = window.innerHeight;
                else if ((document.body) && (document.body.clientHeight))
                    winHeight = document.body.clientHeight;
                //通过深入Document内部对body进行检测，获取窗口大小
                if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
                    winHeight = document.documentElement.clientHeight;
                    winWidth = document.documentElement.clientWidth;
                }
                //结果输出至两个文本框
                //                document.form1.availHeight.value = winHeight;
                //                document.form1.availWidth.value = winWidth;
                $("body").height(winHeight);
                var ch = 720;
                $("#page_center_height_div").height((ch - $("#page_center_div").height()) / 2);

                if (winHeight > ch) {
                    var vagh = (winHeight - ch) / 2;
                    $("#page_top").height(vagh);
                    $("#page_bottom").height(vagh);
                }

            }
            findDimensions();
            //调用函数，获取数值
            window.onresize = findDimensions;
            //alert(ph);

        });
        
    </script>
    <script type="text/javascript">
        function Login() {
            var yjzh = $("#YJZH").val();
            var yjmm = $("#YJMM").val();

            if (!yjzh) {
                alert("账号不能为空");
                return false;
            }
            if (!yjmm) {
                alert("密码不能为空");
                return false;
            }

            $.ajax({
                type: 'post',
                url: "LSYJLogin.aspx?method=yjlslogin",
                data: { yjzh: yjzh, yjmm: yjmm },
                dataType: 'json',
                cache: false,
                success: function (data) {
                    if (data == 0) {
                        alert("用户名或密码错误");
                    }

                    else if (data == 1) {
                        alert("阅卷时间已过期");
                    }
                    else if (data == 2) {
                        alert("阅卷时间未到时");
                    }

                    else {
                        window.location.href = "/Pages/LSYJ/LSFPSJ.aspx?yjxh=" + data.yjxh + "&bmsah=" + data.bmsah + "&ajmc=" + data.ajmc + "&ajbh=" + data.ajbh + "&lszh=" + data.lszh + "&yjxh=" + data.yjxh;
                    }
                }, error: function () {
                    alert("登录失败");
                }
            })
        }
    </script>
</head>
<body>
    <form id="form1">
    <div id="page_top" style="background: #9EDDFC;">
    </div>
    <div style="background: url('/images/LSlogin/bj.png') repeat-x; width: 100%; height: 720px;
        border: 0px solid red;">
        <div id="page_center_height_div">
        </div>
        <div id="page_center_div" style="background: url('/images/LSlogin/5.png') no-repeat;
            width: 865px; height: 424px; background-position: center 69px; margin: 0 auto;">
            <div style="width: 242px; margin: 0 auto;">
                <div style="background: url('/images/LSlogin/2.png') no-repeat; width: 47px; height: 72px;
                    display: inline-block;">
                </div>
                <div style="background: url('/images/LSlogin/3.png') no-repeat; background-position: bottom;
                    width: 183px; height: 72px; display: inline-block;">
                </div>
            </div>
            <div style="width: 150px; margin: 0 auto; padding-top: 62px;">
                <div style="background: url('/images/LSlogin/6.png') no-repeat; width: 45px; height: 35px;
                    display: inline-block;">
                </div>
                <div style="background: url('/images/LSlogin/7.png') no-repeat; width: 98px; height: 26px;
                    display: inline-block;">
                </div>
            </div>
            <div style="width: 268px; margin: 0 auto; margin-top: 12px;">
                <div style="color: Black; font-size: 1.2em">
                    账 号 :
                    <input type="text" id="YJZH" style="height: 25px; width: 200px;" />
                </div>
                <br />
                <div style="color: Black; font-size: 1.2em">
                    密 码 :
                    <input type="password" id="YJMM" style="height: 25px; width: 200px;" />
                </div>
            </div>
            <div style="background: url('/images/LSlogin/登录A.png') no-repeat; margin: 0 auto; width: 68px;
                height: 35px; margin-top: 14px;" onclick="Login()">
            </div>
        </div>
    </div>
    <div id="page_bottom" style="background: #9EDDFC;">
    </div>
    </form>
</body>
</html>
