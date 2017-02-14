<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTemplate.aspx.cs"
    Inherits="WebUI.Pages.Print.PrintTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>打印模板设置</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/LigerUI/lib/json2.js" type="text/javascript"></script>
    <style type="text/css">
        p
        {
            clear: both;
        }
        .author
        {
            position: fixed;
            _position: absolute;
            right: 20px;
            top: 20px;
        }
        ul
        {
            clear: both;
            float: left;
        }
        .grid1
        {
            border: 1px solid #0033FF;      
            height: 24px;
        }
        .grid
        {
            border: 1px solid red;
        }
        /*格子拖放*/
        .grid li
        {
            float: left;
            list-style: none;
            width: 100px;
            height: 32px;
            line-height: 32px;
            border: 1px solid #ccc;
            background: #fff;
            margin: 5px;
            padding: 1px;
            -moz-user-select: none;
        }
        .grid img
        {
            width: 32px;
            height: 32px;
        }
        .grid div
        {
            position: relative;
        }
        .grid span
        {
            position: absolute; /* right: 1px;*/
            left: 2px;
            top: 1px;
            color: #000;
        }
        /*交换数据用的DIV*/
        #tempBox
        {
            position: absolute;
            z-index: 9999;
            width: 100px;
        }
        /*单个拖放demo*/
        #test
        {
            clear: both;
            width: 500px;
            margin: 30px;
            padding: 10px;
            line-height: 24px;
            background: #ccc;
        }
    </style>
</head>
<body>
    <%--  <h2>
        Drop Container</h2>--%>
    <div style="display: table;">
        <div style="float: left; width: 650px;">
            <ul id="template" class="drop grid">
                <li style="width: 500px; height: 500px;"></li>
            </ul>
        </div>
        <div style="width: 20%; float: left;">
            <%--  <h2>
        Drags</h2>--%>
            <ul class="drag grid">
                <li id="l1">
                    <div name="bt">
                        <span>标题</span></div>
                </li>
                <li id="l2">
                    <div name="fbt">
                        <span>（副标题）</span></div>
                </li>
                <li id="l3">
                    <div name="ajmc">
                        <span>案件名称</span></div>
                </li>
                <li id="l4">
                    <div name="ajbh">
                        <span>案件编号</span></div>
                </li>
                <li id="l5">
                    <div name="fzxyr">
                        <span>犯罪嫌疑人</span></div>
                </li>
                <li id="l1">
                    <div name="lasj">
                        <span>立案时间</span></div>
                </li>
                <li id="l2">
                    <div name="jasj">
                        <span>结案时间</span></div>
                </li>
                <li id="l3">
                    <div name="ljdw">
                        <span>立卷单位</span></div>
                </li>
                <li id="l4">
                    <div name="ljr">
                        <span>立卷人</span></div>
                </li>
                <li id="l10">
                    <div name="shr">
                        <span>审核人</span></div>
                </li>
                <li id="Li1">
                    <div name="bagj">
                        <span>本案共 卷</span></div>
                </li>
                <li id="Li2">
                    <div name="djj">
                        <span>第 卷</span></div>
                </li>
                <li id="Li3">
                    <div name="gjy">
                        <span>共 页</span></div>
                </li>
            </ul>
            <div style="text-align: center;">
                <dl id="showTime">
                </dl>
                <input type="button" id="btn_save" name="btn_save" value="保 存" />
            </div>
        </div>
    </div>
    <div style="width: 650px; display: table; background-color: Yellow;">
        <ul id="show_print" class="drop grid">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    </div>
    <div id="print" style="width: 100%; display: block;">
        <!--startprint-->
        <div style="text-align: center; padding-top: 80px; font-family: 宋体; font-weight: bold;
            font-size: 50px;">
            <span id="span_bt">标题</span></div>
        <div style="text-align: center; font-family: 宋体; font-weight: bold; font-size: 26px;">
            (<span id="span_fbt">副标题</span>)</div>
        <div style="">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-top: 20px;
                font-family: 宋体;">
                <tr>
                    <td style="line-height: 60px;">
                        案件名称&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ajmc"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        案件编号&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ajbh"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        犯罪嫌疑人姓名&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_fzxyr"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立案时间&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_lasj"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        结案时间&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_jasj"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立卷单位&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ljdw"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立&nbsp;卷&nbsp;人&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ljr"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        审&nbsp;核&nbsp;人&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_shr"></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="line-height: 40px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="line-height: 60px;">
                        本案共<span id="span_bagj" style="display: inline-block; text-align: center; width: 80px;"></span>卷
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="line-height: 60px;">
                        第<span id="span_djj" style="display: inline-block; text-align: center; width: 80px;"></span>卷&nbsp;&nbsp;&nbsp;共<span
                            id="span_gjy" style="display: inline-block; text-align: center; width: 80px;"></span>页
                    </td>
                </tr>
            </table>
        </div>
        <!--endprint-->
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            dddd();
            function dddd() {
                var json = "([{ index: 2, key: 'bt', value: '标题' }, { index: 7, key: 'fbt', value: '（副标题）' }, { index: 10, key: 'ajmc', value: '案件名称' }, { index: 12, key: 'fzxyr', value: '犯罪嫌疑人' }, { index: 15, key: 'ajbh', value: '案件编号' }, { index: 17, key: 'lasj', value: '立案时间'}])";
                var js = eval(json); //JSON2.parse(json);
                for (var p in js) {
                    console.log(js[p].index);
                    //   alert(packJson[p].name + " " + packJson[p].password);
                    $("#template li").eq(js[p].index).html("<div name=\"" + js[p].key + "\" style=\"cursor: move;\"><span>" + js[p].value + "</span></div>");
                }

                // $("#show_print li").eq(2).html("dsdsdf");
                //    $("#show_print li").eq(2)

                //
                //                $("#show_print").each(function (i,n) { 

                //                    

                //                    if(i == )
                //                });
            }


            //保存
            $("#btn_save").click(function () {

                var lis = $("#template").find("li");



                if (lis) {
                    var json = "";
                    lis.each(function (i, n) {
                        if ($(n).html()) {
                            if (json != "")
                                json += ",";
                            json += "{\"index\":" + i + ",\"key\":\"" + $(n).find("div").attr("name") + "\",\"value\":\"" + $(n).find("span").text() + "\"}";

                        }
                    });
                    console.log(json);
                    $.ajax({
                        type: "POST",
                        url: "/Pages/Print/PrintTemplate.aspx",
                        data: { t: "save", title: "", value: json },
                        dataType: 'json',
                        timeout: 10000,
                        cache: false,
                        beforeSend: function () {
                        },
                        error: function (xhr) {
                            $.ligerDialog.error('网络连接错误');
                            return false;
                        },
                        success: function (data) {
                            if (data.t == "win") {

                                $.ligerDialog.success(data.v);
                            } else {
                                $.ligerDialog.error(data.v);
                            }
                        }
                    });
                }

            });
        });
    </script>
    <script type="text/javascript">
<!--
        //拖放插件DragDrop，作者：kele527，整理：模板无忧（mb5u.com）
        //类似物品栏里的物品可相互拖拽，而且可以有限制拖放，如：下面格子里的东西可以拖到上面，但是不能拖到下面的其他格子里，上面格子里的东西可以在上下格子里任意拖放
        $.fn.Drag = function (options) {
            var defaults = {
                limit: window, //是否限制拖放范围，默认限制当前窗口内
                drop: false, //是否drop
                handle: false, //拖动手柄
                finish: function () { } //回调函数
            };
            var options = $.extend(defaults, options);
            this.X = 0; //初始位置
            this.Y = 0;
            this.dx = 0; //位置差值
            this.dy = 0;
            var This = this;
            var ThisO = $(this); //被拖目标
            var thatO;
            if (options.drop) {
                var ThatO = $(options.drop); //可放下位置
                ThisO.find('div').css({ cursor: 'move' });
                var tempBox = $('<div id="tempBox" class="grid1"></div>');
            } else {
                options.handle ? ThisO.find(options.handle).css({ cursor: 'move', '-moz-user-select': 'none' }) : ThisO.css({ cursor: 'move', '-moz-user-select': 'none' });
            }
            //拖动开始
            this.dragStart = function (e) {
                var cX = e.pageX;
                var cY = e.pageY;

                if (options.drop) {
                    ThisO = $(this);
                    if (ThisO.find('div').length != 1) { return } //如果没有拖动对象就返回
                    This.X = ThisO.find('div').offset().left;
                    This.Y = ThisO.find('div').offset().top;

                    ThisO.find("div").css({ position: "", left: cX, top: cY, "z-index": "1", width: "80px", height: "24px", background: "red" });
                    console.log(ThisO.html());
                    tempBox.html(ThisO.html());

                    ThisO.html('');
                    $('body').append(tempBox);
                    tempBox.css({ left: This.X, top: This.Y });
                  
                } else {
                    This.X = ThisO.offset().left;
                    This.Y = ThisO.offset().top;
                    ThisO.css({ margin: 0 });
                }

                $("#showTime").html(This.X + "----" + This.Y);

                This.dx = cX - This.X;
                This.dy = cY - This.Y;
                if (!options.drop) {
                    ThisO.css({ position: 'absolute', left: This.X, top: This.Y });
                }
                $(document).mousemove(This.dragMove);
                $(document).mouseup(This.dragStop);
                if ($.browser && $.browser.msie) { ThisO[0].setCapture(); } //IE,鼠标移到窗口外面也能释放
            }
            //拖动中
            this.dragMove = function (e) {
                var cX = e.pageX;
                var cY = e.pageY;
                ThisO.find("div").css({ position: "", left: cX, top: cY, "z-index": "1", width: "80px", height: "24px", background: "red" });
                if (options.limit) {//限制拖动范围
                    //容器的尺寸
                    var L = $(options.limit)[0].offsetLeft ? $(options.limit).offset().left : 0;
                    var T = $(options.limit)[0].offsetTop ? $(options.limit).offset().top : 0;
                    var R = L + $(options.limit).width();
                    var B = T + $(options.limit).height();
                    //获取拖动范围
                    var iLeft = cX - This.dx, iTop = cY - This.dy;
                    //获取超出长度
                    var iRight = iLeft + parseInt(ThisO.innerWidth()) - R, iBottom = iTop + parseInt(ThisO.innerHeight()) - B;
                    //alert($(window).height())
                    //先设置右下，再设置左上
                    if (iRight > 0) iLeft -= iRight;
                    if (iBottom > 0) iTop -= iBottom;
                    if (L > iLeft) iLeft = L;
                    if (T > iTop) iTop = T;
                    if (options.drop) {
                        tempBox.css({ left: iLeft, top: iTop })
                    } else {
                        ThisO.css({ left: iLeft, top: iTop })
                    }
                } else {
                    //不限制范围
                    if (options.drop) {
                        tempBox.css({ left: cX - This.dx, top: cY - This.dy })
                    } else {
                        ThisO.css({ left: cX - This.dx, top: cY - This.dy });
                    }
                }
            }
            //拖动结束
            this.dragStop = function (e) {
                if (options.drop) {
                    var flag = false;
                    var cX = e.pageX;
                    var cY = e.pageY;
                    var oLf = ThisO.offset().left;
                    var oRt = oLf + ThisO.width();
                    var oTp = ThisO.offset().top;
                    var oBt = oTp + ThisO.height();
                    if (!(cX > oLf && cX < oRt && cY > oTp && cY < oBt)) {//如果不是在原位

                        for (var i = 0; i < ThatO.length; i++) {
                            var XL = $(ThatO[i]).offset().left;
                            var XR = XL + $(ThatO[i]).width();
                            var YL = $(ThatO[i]).offset().top;
                            var YR = YL + $(ThatO[i]).height();
                            if (XL < cX && cX < XR && YL < cY && cY < YR) {//找到拖放目标，交换位置
                                //position: absolute; cursor: move; top: 180px; left: 462px; z-index: 1;

                                tempBox.find("div").css({ position: "absolute", left: cX, top: cY, "z-index": "1", width: "80px", height: "24px", background: "red" });
                                //   console.log(tempBox.html());
                                var newElm = $(ThatO[i]).html();
                                $(ThatO[i]).html(tempBox.html());
                                ThisO.html(newElm);

                                thatO = $(ThatO[i]);

                                /*
                                var newElm = $(ThatO[i]).html();
                                $(ThatO[i]).html(tempBox.html());
                                ThisO.html(newElm);
                                
                                thatO = $(ThatO[i]);*/
                                tempBox.remove();
                                flag = true;
                                break; //一旦找到，就终止循环
                            }
                        }
                    }
                    if (!flag) {//如果找不到拖放位置，归回原位
                        tempBox.css({ left: This.X, top: This.Y });
                        ThisO.html(tempBox.html());
                        tempBox.remove();
                    }
                }
                $(document).unbind('mousemove');
                $(document).unbind('mouseup');
                options.finish(e, ThisO, thatO);
                if ($.browser && $.browser.msie) { ThisO[0].releaseCapture(); }
            }
            //绑定拖动
            options.handle ? ThisO.find(options.handle).mousedown(This.dragStart) : ThisO.mousedown(This.dragStart);
            //IE禁止选中文本
            //document.body.onselectstart=function(){return false;}
        }
        //下面是例子
        //.drag li里面的元素对应的放置位置是.drop li，完成后回调change函数，默认限制拖动范围是窗口内部
        $('.drag li').Drag({ drop: '.drop li', finish: change });
        //.drag li里面的元素对应的放置位置是.drop li和.drag li（自身），完成后回调change函数，默认限制拖动范围是窗口内部
        $('.drop li').Drag({ drop: '.drop li, .drag li', finish: change });
        // $('#test').Drag({ handle: 'h2', finish: change }); //不限制拖动范围，可设置limit:false

        var change = function (e, oldElm, newElm) {
            console.log(e);
            //alert('拖动完成')
        }
//-->
    </script>
</body>
</html>
