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
            FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#1396bc,endColorStr=#0649b5); /*IE 6 7 8*/ 

            background: -ms-linear-gradient(top, #1396bc,  #0649b5);        /* IE 10 */

            background:-moz-linear-gradient(top,#1396bc,#0649b5);/*火狐*/ 

            background:-webkit-gradient(linear, 0% 0%, 0% 100%,from(#1396bc), to(#0649b5));/*谷歌*/ 

            background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#1396bc), to(#0649b5));      /* Safari 4-5, Chrome 1-9*/

            background: -webkit-linear-gradient(top, #1396bc, #0649b5);   /*Safari5.1 Chrome 10+*/

            background: -o-linear-gradient(top, #1396bc, #0649b5);  /*Opera 11.10+*/
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
        /* 功能切换tab */
        
         .l-tab-links
        {
            background: white;
            }
            .l-tab-links-item-left {
                position: absolute;
                top: 0;
                left: 0;
                width: 0;
                height: 0;
                background: none;
            }
            .l-tab-links-item-right {
                position: absolute;
                top: 0;
                left: 0;
                width: 0;
                height: 0;
                background: none;
            }
            .l-tab-links li.l-selected {
              /*  background: #eeeeee; */
            }
            
            .l-tab-links {
                border-bottom: 1px solid #d2d2d2;
            }
         .l-tab-links li {
            height: 26px;
            line-height: 26px;
        }
        .l-tab-links li:first-child {
            margin-left: 0px;
        }
        .l-tab-links li a {
            color: #0392db;
        }
             /* 头部 */
        .l-topmenu
        {
            margin: 0;
            padding: 0;
            height: 68px;
            line-height: 68px;
            background: -webkit-linear-gradient(left, #0d8bbd, #0e78a7);
            background: -o-linear-gradient(right, #0d8bbd, #0e78a7); /* Opera 11.1 - 12.0 */
            background: -moz-linear-gradient(right, #0d8bbd, #0e78a7); /* Firefox 3.6 - 15 */
            background: linear-gradient(to right, #0d8bbd , #0e78a7); /* 标准的语法 */
            position: relative;
            border-top: 1px solid #1D438B;
            color: white;
        }
        .l-topmenu-logo
        {   
                margin-top: -7px;
               letter-spacing: 13px;
                font-family: "黑体";
                font-size: 30px;
                letter-spacing: 5px;
                margin-left: -27px;
                text-shadow: 1px 1px 1px #0461ab;
        }
        h6 {
                font-family: "Arial";
                font-size: 10px;
                margin-top: -43px;
                padding-left: 10px;
                text-shadow: 1px 1px 1px #0461ab;
        }
        .l-topmenu-centent {
            width: 427px;
            height: 68px;
            background: url(/images/head_centent.png) no-repeat;
            position: absolute;
            left: 24%;
            top: 0;
            }
        .l-topmenu-prn
        {
            position: absolute;
            right:0;
            top:0;
            width: 532px;
            height: 68px;
            line-height: 68px;
            font-family: "黑体";
            font-size: 15px;
            background: url(/images/head_right.png) no-repeat;
            }
        .l-topmenu-welcome
        {
            position: absolute;
            right: 30px;
            top: 0x;
            color: #070A0C;
        }
        .l-topmenu-welcome a
        {
            text-decoration: inherit;
        }
        /*左边框架*/
        .l-layout-left {
            border:none;
        }
        .l-layout-center {
            border-radius: 5px;
        }
        /*  主菜单背景图标  */
        .l-layout-header-inner {
            height: 48px;
            margin-top: 25px;
            background: url(/images/主菜单.png) no-repeat center 0;
        }
        .l-layout-left .l-layout-header-toggle
        {
            width:0;
            height:0;
            }
         .l-layout-header{
            margin-left: 12px;
            height: 162px;
            color:white;
            background: #1569a4;
        } 
        .zcd_text {
            margin-top: 17px;
            text-align: center;
            font-size: 26px;
        }
        .zcd_text_1 
        {
            text-align: center;
            -webkit-font-size: 10px;
            -moz-font-size: 12px;
        }
        div#accordion1 {
            margin-left: 12px;
        } 
        /*去掉菜单背景色*/  
        .l-accordion-header
        {
            height:36px;
            line-height:36px;
            margin-top: 10px;
            color: white;
          /*  background:url("../../images/nav.png");  */
            padding:0;
            }
            
            .l-accordion-header-inner {
                padding-left:45px;
            }
            /*菜单箭头*/
         .l-accordion-header-inner.l-accordion-header-inner-open{
            background: #03A9F4;
            border-radius:5px 5px 0 0;
            color: white;
        }
        
       .l-accordion-header-inner.l-accordion-header-inner-close {
            background: url(../../images/nav.png);
            border-radius: 5px;
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
         background: url(../../../../../../images/togglebar_close.png);
            }
        .l-accordion-toggle-open {
         top: 13px;
         right: 9px;
         height: 7px;
         width: 13px;
         background: url(../../../../../../images/togglebar_open.png);
            }
        /*菜单图片*/
        .icon_img-0{background:url("../../images/jzzz.png");}  
        .icon_img-1{background:url("../../images/dayj.png");}  
        .icon_img-2{background:url("../../images/tjcx.png");}   
        .icon_img-3{background:url("../../images/tjbb.png");}   
        .icon_img-4{background:url("../../images/xtpz.png");}    
            
            .icon_img {
                position: absolute;
                left: 10px;
                width: 20px;
                height: 20px;
                top: 8px;
             }
            .icon_img-0
            {
                 width: 16px;
                }
       /* 表格页数 */
      .l-grid-row-alt .l-grid-row-cell {
            background: #e5edf2;
        }
      
        .l-scroll
        {
            background: #EFFEFE;
        }
       
        #accordion1 ul
        {
            margin: 0;
        }  
        ul.accordion1_ul-4
       {
           height:390px;
           overflow-y: auto;
           overflow-x: hidden;
           }
        div .l-scroll.l-accordion-content {
            border: 1px solid #ccc;
            background: #eef2f5;
            border-radius: 0 0 6px 6px;
        }
        #accordion1 ul li
        {
            list-style-type: none;
            cursor: pointer;
            display: block;
            color: #219af3;
            border-bottom:2px solid #eef2f5;    
            padding: 5px;
            text-align: center;
        }  
        #accordion1 ul li:hover
        {
            background:#d6ebfa;
            border-bottom:2px solid #1072bd;
            }
       /* #accordion1 ul li {
            list-style-type: none;
            cursor: pointer;
            display: block;
            height:25px;
            text-align: center;
            line-height: 25px;
            background: #eceaea;
            border-bottom: 1px solid #ccc;
             background: url(/Scripts/tools/easyui/themes/icons/AddGroup.png) no-repeat 30px center; 
        }  */
        .l-link2
        {
            padding-left: 18px;
        }
        #userinfo
        {
            background: url('/images/icons/user-white.png') no-repeat left center;
            background-size: 14px;
        }
        #pwdup
        {
            background: url('/images/icons/lock-white.png') no-repeat left center;
            background-size: 14px;
        }
        #exit
        {
            background: url('/images/icons/exit-hover.png') no-repeat left center;
            background-size: 14px;
        }
        
       
       
        
    </style>
</head>
<body>

    <div id="pageloading">
    </div>
    
    <div id="topmenu" class="l-topmenu" style='<% if (VersionPage == "PAGE")
                       {%>
                        background: url("/images/login2/02.png") repeat-x; <%}%>' >
        <div class="l-topmenu-logo" style='<% if (VersionPage == "PAGE")
                       {%>
                        background: url("/images/login2/01.png") no-repeat left center; <%}%>'>
            &nbsp; 深圳市公安局</div>
            <h6>Shenzhen Municipal Public Security Bureau</h6>
            <div class="l-topmenu-centent"></div>
            <div class="l-topmenu-prn">
                    <div class="l-topmenu-welcome">
                    <a href="javascript:;" id="userinfo" class="l-link2"></a>
                    <%--<span class="space">|</span>--%>&nbsp;&nbsp; <a href="javascript:pwd();" id="pwdup"
                        class="l-link2">修改密码</a>&nbsp;&nbsp;<%--<span class="space">
                        |</span>--%>
                    <a href="javascript:exit();" id="exit" class="l-link2">退出</a>
                </div>
            </div>
        
    </div>
    <div id="layout1" style="width: 100%; margin: 0 auto;">
   <!-- <div id="nva_fixed"></div>  -->
        <div position="left" title="" id="accordion1">
        
        </div>
        <div position="center" id="framecenter" style="background: white;">
            <%--<div tabid="home" title="我的主页" style="height: 300px">
                <iframe frameborder="0" scrolling="yes" name="home" id="home" src=""></iframe>
            </div>--%>
        </div>
        <div position="right" title="" id="Div1">

    </div>
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
                    leftWidth: 162,
                    rightWidth:0,
                    height: '100%',
                    allowLeftResize:false,
                    allowCenterBottomResize:false,
                    heightDiff: -12,
                    space:12,
                    onHeightChanged: f_heightChanged,
                    onLeftToggle: function ()
                    {
                        tab && tab.trigger('sysWidthChange');
                    },
                    onRightToggle: function ()
                    {
                        tab && tab.trigger('sysWidthChange');
                    }
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
              //  console.log(data);
                    //console.log(JSON.stringify(data));
                   if(data.t && data.t == "error" || data == null || data == undefined)
                   {
                        $.ligerDialog.error(data.v);
                        return false;
                   }
                    var content = "";
                    $.each(data, function (i, n) {
                        var contents = "";
                        if (n.children) {
                            $.each(n.children, function (j, m) {
                                contents += "<li id=\"" + m.id + "\" url=\""+ m.url + "\">" + m.text + "</li>";
                            });
                        }
                        content += "<div  class=\"l-scroll\" title=\"" + n.text + "\"><ul>" + contents + "</ul></div>";
                    });
                    
                    $("#accordion1").html(content);

                    f_addTab($("#accordion1 ul li:first").attr("id"), $("#accordion1 ul li:first").text(), $("#accordion1 ul li:first").attr("url"));
                }
            });

            //accordion = $("#accordion1").ligerGetAccordionManager();
            //面板
            accordion = $("#accordion1").ligerAccordion({ height: height - 24, changeHeightOnResize: true, speed: null});

            $("#pageloading").hide();

            //将菜单下分层高度至零
            $(".l-scroll").css("height","auto");

            //点击菜单
            $("#accordion1 ul").on("click", "li", function () {
                f_addTab($(this).attr("id"), $(this).text(), $(this).attr("url"));
            });

            $(window).resize(function () {
                accordion.setHeight($("#accordion1").parent("div").height() - 24);
            });

        });

        
       

        var userInfo;
        var time;
        $(document).ready(function () {  
                       
           
            userInfo = <%= EDRS.Common.JsonHelper.JsonString(this.UserInfo) %>;
            if (userInfo) {
                $("#userinfo").text(userInfo.DWMC +" - "+ userInfo.MC);
            } else {
                skip();
            }
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
            $.ligerDialog.confirm('确定是否退出？', function (yes)
            {
                if(yes)
                location.href = "main.aspx?exit=1";
            });
        }
        //页面跳转
        function skip() {
            parent.location = '/Login.aspx';
        }     
        //获取URL参数
        function GetQueryString(url,name) {
           var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)","i");
          // var r = window.location.search.substr(1).match(reg);
          var r = url.match(reg);
           if (r!=null) return (r[2]); return null;
        }   
        



        
   $(function(){
   

       // $("div .l-layout-left").css("height",$(window).height()-105+"px");  //重新设置左边框架的高度
        $(".l-accordion-header").append("<div class='icon_img'><div>");
        $(".l-layout-header").append("<div class='zcd_text'>电子档案</div>");
        $(".l-layout-header").append("<div class='zcd_text_1'>Electronic Record</div>");
        // 循环添加一级菜单图标
        $(" .l-accordion-header-inner").each(function(index){
                        $(this).addClass("l-accordion-header-inner-"+index);
            });
            $(".l-accordion-header .icon_img").each(function(index){
                // if($(this).text()!==""){
                     $(this).addClass("icon_img-"+index);
                  //   }
                //else{
                //        $(this).css("background","url(\""+MainMenuIcon($(this).text())+"\") no-repeat 5px center");
                 //  }
                       
            });
            //循环子菜单，为每个子菜单添加css
        $("#accordion1 ul").each(function(index){
                  
             $(this).addClass("accordion1_ul-"+index);
         });
         //当子菜单超出高度时，出现滚动条
         if($(window).height()-470<=390){
             $("ul.accordion1_ul-4").css("height",$(window).height()-477+"px");
         };
        //默认将第一个菜单进行收缩
            $(".l-accordion-header .l-accordion-header-inner-0").addClass("l-accordion-header-inner-open");
            $(".l-accordion-header .l-accordion-header-inner").addClass("l-accordion-header-inner-close");
            $(".l-accordion-header .l-accordion-header-inner-0").removeClass("l-accordion-header-inner-close");
           //  $(".l-accordion-header div").removeClass("l-accordion-toggle-open");   //移除css属性
          //   $(".l-accordion-header div").addClass("l-accordion-toggle-close");  //添加css属性
           //  $(".l-accordion-content").css("display","none");   //隐藏二级菜单


        
        

       
       
   })
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

        var tree_dw = "/images/icons/3.png";$(function(){
        
            setInterval(function() {
                        //判断tab是否存在
                // if($("#framecenter .l-tab-links ul > li").length=== 0){
                   //   $(".l-tab-links").css("display","none"); 
                  //  }else{
                     //   $(".l-tab-links").css("display","block");
                   // }
                   //当子菜单超出高度时，出现滚动条
                     if($(window).height()-470<=390){
             $("ul.accordion1_ul-4").css("height",$(window).height()-477+"px");
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
