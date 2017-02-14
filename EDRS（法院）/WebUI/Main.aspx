<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebUI.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>电子卷宗管理系统</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <%-- <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>    --%>
    <script src="/LigerUI/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <%--<script src="indexdata.js" type="text/javascript"></script>--%>
    <script src="Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <%--弹出框可拖动插件--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <%--弹出框插件--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <style type="text/css">
        body, html
        {
            height: 100%;
        }
         body
        {
            padding: 0px;
            margin: 0;
            overflow: hidden;
            FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#7698ad,endColorStr=#3b5f7e); /*IE 6 7 8*/ 

            background: -ms-linear-gradient(top, #7698ad,  #3b5f7e);        /* IE 10 */

            background:-moz-linear-gradient(top,#7698ad,#3b5f7e);/*火狐*/ 

            background:-webkit-gradient(linear, 0% 0%, 0% 100%,from(#7698ad), to(#3b5f7e));/*谷歌*/ 

            background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#7698ad), to(#3b5f7e));      /* Safari 4-5, Chrome 1-9*/

            background: -webkit-linear-gradient(top, #7698ad, #3b5f7e);   /*Safari5.1 Chrome 10+*/

            background: -o-linear-gradient(top, #7698ad, #3b5f7e);  /*Opera 11.10+*/
        }
        .l-link
        {
            display: block;
            height: 26px;
            line-height: 26px;
            padding-left: 10px;
            text-decoration: underline;
            color: #333;
        }
        .l-link2
        {
            text-decoration: underline;
            color: white;
            margin-left: 2px;
            margin-right: 2px;
        }
        .l-layout-top
        {
            background: #102A49;
            color: White;
        }
        .l-layout-bottom
        {
            background: #E5EDEF;
            text-align: center;
        }
        #pageloading
        {
            position: absolute;
            left: 0px;
            top: 0px;
            background: white url('/LigerUI/lib/images/loading.gif') no-repeat center;
            width: 100%;
            height: 100%;
            z-index: 99999;
        }
        .l-link
        {
            display: block;
            line-height: 22px;
            height: 22px;
            padding-left: 16px;
            border: 1px solid white;
            margin: 4px;
        }
        .l-link-over
        {
            background: #FFEEAC;
            border: 1px solid #DB9F00;
        }
        .l-winbar
        {
            background: #2B5A76;
            height: 30px;
            position: absolute;
            left: 0px;
            bottom: 0px;
            width: 100%;
            z-index: 99999;
        }
        .space
        {
            color: #E7E7E7;
        }
        /* 顶部 */
        .l-topmenu
        {
            margin: 0;
            padding: 0;
            height: 53px;
            line-height: 53px;
            position: relatie;
            font-family: "黑体";
            color:White;
            background: url('/images/NewAdd/head_bj1.png') no-repeat left center;
        }
        .l-topmenu-logo
        {
            padding-left: 53px;
            font-size: 22px;
            margin-left: 35px;
            line-height: 53px;
            text-shadow: 1px 1px 2px white;
            background: url(/images/NewAdd/head_log.png) no-repeat left center;
        }
        .l-topmenu-head
        {
            position: absolute;
            right:0;
            top:0;
            width: 408px;
            height: 53px;
            background: url('/images/NewAdd/head_bj2.png') no-repeat left center;
            }
        .l-topmenu-welcome
        {
            position: absolute;
            height: 50px;
            line-height: 50px;
            right: 30px;
            top: 2px;
            color: #070A0C;
        }
        .l-topmenu-welcome img 
        {
            padding: 0 5px;
            margin-top: -6px;
            vertical-align: middle;
        }
        .l-topmenu-welcome a
        {
            font-size: 14px;
            text-decoration: inherit;
        }
        div .l-layout-left,div .l-layout-center  {
            border: none;
            background: none;
        }
        .l-layout-header 
        {
          /*  margin-left: 5px; */
            height: 75px;
            line-height: 75px;
            background: #248dc1;
            border-bottom: 1px solid #39afe9;
         }
         
         .l-layout-header-inner 
         {
             padding-left: 60px;
            margin-left: 20px;
            font-family: "黑体";
            color: white;
            font-size: 18px;
            padding-left: 53px;
            background: url(images/NewAdd/Main_menu.png) no-repeat 5px center;
         }
         
         div#accordion1 
         {
             /*  margin-left: 5px; */
             border: none;
            background: #248dc1;
            border-radius: 0 0 11px 0;
        }
        /*去除toggle*/
        .l-layout-left .l-layout-header-toggle
        {
            width:0;
            height:0;
            }
         /*去掉菜单背景色*/  
        .l-accordion-header
        {
            height:36px;
            line-height:36px;
            /*margin-top: 10px; */
            color: white;
            background:none; 
            padding:0;
            }
            .l-accordion-header-inner {
                padding-left:45px;
            }
            /*菜单箭头*/
         .l-accordion-header-inner.l-accordion-header-inner-open{
            background: #1678ac;
            color: white;
            border-left: 3px solid white;
            border-bottom:none;
            border-bottom: 1px solid #39afe9;
        }
        
       .l-accordion-header-inner.l-accordion-header-inner-close 
       {
            background:#248dc1;
            height: 34px;
            margin-top: 1px;
            border-bottom: 1px solid #39afe9;
        }
        /*去掉菜单hover样式*/
       .l-accordion-header-over {
            background: none;
        }
         .l-accordion-toggle-close {
         top: 13px;
         right: 9px;
         height: 7px;
         width: 13px;
         background: url(../../../../../../images/NewAdd/togglebar_close.png);
            }
        .l-accordion-toggle-open {
         top: 13px;
         right: 9px;
         height: 7px;
         width: 13px;
         background: url(../../../../../../images/NewAdd/togglebar_open.png);
            }
         /*菜单图片*/
        .icon_img-0{background:url("../../images/NewAdd/jzzz.png") no-repeat 5px center;}  
        .icon_img-1{background:url("../../images/NewAdd/dacx.png") no-repeat 5px center;}  
        .icon_img-2{background:url("../../images/NewAdd/tjcx.png") no-repeat 5px center;}   
        .icon_img-3{background:url("../../images/NewAdd/tjbb.png") no-repeat 5px center;}   
        .icon_img-4{background:url("../../images/NewAdd/xtpz.png") no-repeat 5px center;}    
            
            .icon_img {
                position: absolute;
                left: 10px;
                width: 25px;
                height: 20px;
                top: 8px;
             }
                
        
      /*  .l-accordion-header-inner-0
        {
            background: url("/images/icons/home.png") no-repeat 5px center;
        }
        .l-accordion-header-inner-1
        {
            background: url("/images/icons/system.png") no-repeat 5px center;
        }
        .l-accordion-header-inner-2
        {
            background: url("/images/icons/zz.png") no-repeat 5px center;
        }
        .l-accordion-header-inner-3
        {
            background: url("/images/icons/cx.png") no-repeat 5px center;
        }
        .l-accordion-header-inner-4
        {
            background: url("/images/icons/tj.png") no-repeat 5px center;
        }
        .l-accordion-header-inner-5
        {
            background: url("/images/icons/yj.png") no-repeat 5px center;
        } */
    </style>
    <style type="text/css">
        .l-scroll
        {
            background: #EFFEFE;
        }
        div .l-scroll.l-accordion-content
        {
            background: #389bcf;
            border-bottom: 1px solid #39afe9;
            }
        #accordion1 ul
        {
           color: white;
        }
        #accordion1 ul li
        {
            list-style-type: none;
            cursor: pointer;
            display: block;
            text-align: center;
            padding: 8px;
           /* padding: 8px 0 8px 50px;
            background: url('/Scripts/tools/easyui/themes/icons/AddGroup.png') no-repeat 30px center;  */
        }
        #accordion1 ul li:hover
        {
            background-color: #0e6390;
            border-left: 3px solid white;
        }
        .l-link2
        {
            padding-left: 18px;
        }
        #userinfo
        {
            background: url('/images/NewAdd/user-white.png') no-repeat left center;
            background-size: 11px;
        }
        #pwdup
        {
            background: url('/images/NewAdd/lock-white.png') no-repeat left center;
            background-size: 14px;
        }
        #exit
        {
            background: url('/images/NewAdd/exit-hover.png') no-repeat left center;
            background-size: 14px;
        }
        .l-accordion-header-inner
        {
            padding-left: 40px;
        }
        
        /* 内容区域头部*/
        .l-tab-links
        {
            background: white;
            }
        .l-tab-links li:first-child {
            margin-left: 0px;
        }
        .l-tab-links li.l-selected {
            background: url(../../../../../../images/Newadd/tabs-item-over-bg_1.png);
        }
        .l-tab-links li {
            background:url(/images/Newadd/nav_2.png) repeat-x;
        }
        .l-tab-links li a {
            color: #0392db;
        }
        .l-tab-links-item-left,.l-tab-links-item-right {
                position: absolute;
                top: 0;
                left: 0;
                width: 0;
                height: 0;
                background: none;
            }
    </style>
</head>
<body>
    <div id="pageloading">
    </div>
    <div id="topmenu" class="l-topmenu" style='<% if (VersionPage == "PAGE")
                       {%>
                        background: url("/images/login2/02.png") repeat-x; <%}%>'>
        <div class="l-topmenu-logo" style='<% if (VersionPage == "PAGE")
                       {%>
                        background: url("/images/login2/01.png") no-repeat left center; <%}%>'>
            深圳市中级人民法院电子卷宗系统</div>

            <div class="l-topmenu-head">
                <div class="l-topmenu-welcome">
                    <a href="javascript:;" id="userinfo" class="l-link2">管理员</a>
                    <%--<span class="space">|</span>--%>
                     &nbsp;<img src="images/NewAdd/head_line.png"/>&nbsp;
                    <a href="javascript:pwd();" id="pwdup"
                        class="l-link2">修改密码</a>
                       &nbsp;<img src="images/NewAdd/head_line.png"/>&nbsp;
                        <%--<span class="space">
                        |</span>--%>
                    <a href="javascript:exit();" id="exit" class="l-link2">退出</a>
                </div>
            </div>
        
    </div>
    <div id="layout1" style="width: 100%; margin: 0 auto;">
        <div position="left" title="主菜单" id="accordion1">
        </div>
        <div position="center" id="framecenter">
            <%--<div tabid="home" title="我的主页" style="height: 300px">
                <iframe frameborder="0" scrolling="yes" name="home" id="home" src=""></iframe>
            </div>--%>
        </div>
    </div>
    <%--<div style="height: 25px; line-height: 25px; text-align: center;">
        &nbsp;
    </div>--%>
    <div style="display: none">
    </div>
    <%--修改密码窗口--%>
    <div id="pwd_div" style="display: none; width: 188px; margin: 0 auto;">
        <form id="pwd_form" method="post">
        <table style="line-height: 30px;">
            <tr>
                <td>
                    旧密码:
                </td>
                <td>
                    <input class="l-text" type="password" name="pwd_before" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td>
                    新密码:
                </td>
                <td>
                    <input id="pwd_news" class="l-text" type="password" name="pwd_news" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td>
                    确认密码:
                </td>
                <td>
                    <input id="pwd_newsTo" name="pwd_newsTo" class="l-text" type="password" maxlength="20" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        var tab = null;
        var accordion = null;
        var tree = null;
        $(function () {
            //$("#home").attr("src", "welcome.htm");

            $(".l-link").hover(function () {
                $(this).addClass("l-link-over");
            }, function () {
                $(this).removeClass("l-link-over");
            });

            //布局
            $("#layout1").ligerLayout({ 
            leftWidth: 190,
            height: '100%', 
            allowLeftResize:false,
            allowCenterBottomResize:false,
            space:0,
            heightDiff: -12,
            onHeightChanged: f_heightChanged 
            });

            var height = $(".l-layout-center").height();

            //Tab
            $("#framecenter").ligerTab({ height: height });

            tab = $("#framecenter").ligerGetTabManager();


            //绑定功能
            $.ajax({
                url: "/Main.aspx",
                data: { t: "BindingMenu" },
                type: "POST",
                dataType: "json",
                async: false,
                success: function (data) {
               // console.log(data);
                    //console.log(JSON.stringify(data));
                    if (data.t && data.t == "error" || data == null || data == undefined) {
                        $.ligerDialog.error(data.v);
                        return false;
                    }
                    var content = "";
                    $.each(data, function (i, n) {
                        var contents = "";
                        if (n.children) {
                            $.each(n.children, function (j, m) {
                                contents += "<li id=\"" + m.id + "\" url=\"" + m.url + "\">" + m.text + "</li>";
                            });
                        }
                        content += "<div class=\"l-scroll\" title=\"" + n.text + "\"><ul>" + contents + "</ul></div>";
                    });

                    $("#accordion1").html(content);
                    f_addTab($("#accordion1 ul li:first").attr("id"), $("#accordion1 ul li:first").text(), $("#accordion1 ul li:first").attr("url"));
                }
            });

            //accordion = $("#accordion1").ligerGetAccordionManager();
            //面板
            accordion = $("#accordion1").ligerAccordion({ height: height - 24, changeHeightOnResize: true, speed: null });

            //将菜单下分层高度至零
            $(".l-scroll").css("height","auto");

            $("#pageloading").hide();
            //点击菜单
            $("#accordion1 ul").on("click", "li", function () {
                f_addTab($(this).attr("id"), $(this).text(), $(this).attr("url"));
            });

            $(window).resize(function () {
                accordion.setHeight($("#accordion1").parent("div").height() - 24);
              
               // $("div #accordion1").css();
            });

        });

        var userInfo;
        var time;
        $(document).ready(function () {
                 var height = $("#accordion1").height()-53+"px";
                 $("#accordion1").css("height",height);


           $(".l-accordion-header").append("<div class='icon_img'><div>");  //添加一个img标签
           // 循环添加一级菜单图标
        $(" .l-accordion-header-inner").each(function(index){
                        $(this).addClass("l-accordion-header-inner-"+index);
            });
                //循环子菜单，为每个子菜单添加css
        $("#accordion1 ul").each(function(index){
                  
             $(this).addClass("accordion1_ul-"+index);
         });
         
         //循环菜单图片标签，为每个菜单图片标签添加css
         $(".l-accordion-header .icon_img").each(function(index){
                     $(this).addClass("icon_img-"+index);
                       
            });
            //默认将第一个菜单进行收缩
            $(".l-accordion-header .l-accordion-header-inner-0").addClass("l-accordion-header-inner-open");
            $(".l-accordion-header .l-accordion-header-inner").addClass("l-accordion-header-inner-close");
            $(".l-accordion-header .l-accordion-header-inner-0").removeClass("l-accordion-header-inner-close");


            $(".l-accordion-header-inner").each(function (index) {
                if ($(this).text() == "")
                    $(this).addClass("l-accordion-header-inner-" + index);
               // else
               //     $(this).css("background", "url(\"" + MainMenuIcon($(this).text()) + "\") no-repeat 5px center");
            });

             userInfo = <%= EDRS.Common.JsonHelper.JsonString(this.UserInfo) %>;
            if (userInfo) {
                $("#userinfo").text(userInfo.DWMC + " - " + userInfo.MC);
            } else {
                skip();
            }

           var t = setInterval(function(){GetMessge()},30000);
         
            //console.log(JSON.stringify(userInfo));
        });

        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }
        function f_addTab(tabid, text, url) {
            if (tab.isTabItemExist(tabid))
                tab.removeTabItem(tabid);
            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }

        function pwd() {

            $.ligerDialog.open({ title: '修改密码', target: $('#pwd_div'), width: 380,
                buttons: [{ text: '确定', onclick: function (item, dialog) {
                    alterPwd();
                    $("#pwd_form")[0].reset();
                    dialog.hidden();
                }, cls: 'l-dialog-btn-highlight'
                },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#pwd_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
            });
        }
        //修改密码
        function alterPwd() {

            var jdata = $('#pwd_form').serializeArray();
            jdata[jdata.length] = { name: "t", value: "AlterPwd" };
            $.ajax({
                type: "POST",
                url: "/Main.aspx",
                data: jdata,
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.t == "win") {
                        $.ligerDialog.success(data.v);
                    } else
                        $.ligerDialog.error(data.v);
                }
            });
        }
        //退出登录
        function exit() {
            $.ligerDialog.confirm('确定是否退出？', function (yes) {
                if (yes)
                    location.href = "main.aspx?exit=1";
            });
        }
        //页面跳转
        function skip() {
            parent.location = '/Login.aspx';
        }
        //获取URL参数
        function GetQueryString(url, name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            // var r = window.location.search.substr(1).match(reg);
            var r = url.match(reg);
            if (r != null) return (r[2]); return null;
        }
        var tip;
        //消息提醒
        function GetMessge() {
            $.ajax({
                type: "POST",
                url: "Main.aspx",
                data: { t: "GetMessge" },
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                },
                error: function (xhr) {
                    //$.ligerDialog.warn('网络连接错误!');
                    return false;
                },
                success: function (data) {
                    if (data.t == "win") {
                
                        var msg = data.v.split("|");
                        var msgs;
                        if (msg.length == 2 && msg[0] == "")
                            msgs = msg[1];
                        else if (msg.length == 1 || (msg.length == 2 && msg[1] == ""))
                            msgs = msg[0];
                        else if (msg.length == 2 && msg[0] != "" && msg[1] != "")
                            msgs = msg[0] + "<br>" + msg[1];
                        if (!tip) {
                            tip = $.ligerDialog.tip({ title: '提示信息', content: msgs });
                        }
                        else {
                            var visabled = tip.get('visible');
                            if (!visabled) tip.show();
                            tip.set('content', msgs);
                        }
                    }
                }
            });
        }

        //菜单图标
        function MainMenuIcon(pams) {
            switch (pams) {
                case "系统配置":
                    return "/images/icons/system.png";
                    break;
                case "卷宗制作":
                    return "/images/icons/zz.png";
                    break;
                case "统计查询":
                    return "/images/icons/cx.png";
                    break;
                case "统计报表":
                    return "/images/icons/tj.png";
                    break;
                case "卷宗阅卷":
                    return "/images/icons/yj.png";
                    break;
                default:
                    return "/images/icons/home.png";
                    break;
            }
        }

        var tree_dw = "/images/icons/3.png";
     
     
        $(function(){
        
            setInterval(function() {
                        //判断tab是否存在
                // if($("#framecenter .l-tab-links ul > li").length=== 0){
                   //   $(".l-tab-links").css("display","none"); 
                  //  }else{
                     //   $(".l-tab-links").css("display","block");
                   // }
                   //当子菜单超出高度时，出现滚动条
                     if($(window).height()-470<=390){
                         $("ul.accordion1_ul-4").css("height",$(window).height()-324+"px");
                     };
                     
                   // $(".l-layout-content").css("overflow","hidden");
                   // $("div#accordion1").css("borderRadius","0 0 11px 0");
                   // console.log($(window).height());

                    $("div#accordion1").css("height",$(window).height()-143+"px");
                   $(".l-scroll").css("height","auto");
                },1);  


        })
    </script>
</body>
</html>
