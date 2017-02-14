<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingApply.aspx.cs" Inherits="WebUI.Pages.LSYJ.ReadingApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="../../Scripts/pdfobject.js" type="text/javascript"></script>
    <style type="text/css">
        /*右边框背景颜色*/
        body
        {
            background: #eef2f5;
            }
         .l-panel-bwarp {
            background: white;
        }
        
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #ccc;
            display: inline-block;
            width: 100%;
             background: white;
        }
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
            margin: 0;
        }
        #tree_left
        {
            width: auto !important;
        }
        #leftFrm
        {
            overflow: auto !important;
            background: white;
            height: 100%;
        }
        .l-tree-icon-folder1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-folder1-open
        {
            background: url("/images/icons/usergroup-open.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-leaf1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-table
        {
            width: 100%;
        }
        .l-table tr td
        {
            padding: 5px 2px;
        }
        .l-text
        {
            width: 150px;
        }
        /*   按钮  */
        .l-button {
            color: white;
        }
        div#btn_search {
            background: #ed6d4a;
        }
        .l-toolbar-item.l-panel-btn.l-toolbar-item-hasicon {
            background: #339bca;
            color: white;
        }
        
       
         div#tb_search {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background: white;
        }
        
         div .l-layout-left,div .l-layout-right{
            border: 1px solid #dde0e3;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
    }
    .l-layout-center
    {
            border: 1px solid #dde0e3;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
        }
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding:15px;">

  <div id="tb_search">
        <div style="padding: 4px 5px;">
            案件编号：
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
            案件名称：
            <input id="txt_ajmc" style="width: 150px;" class="l-text" type="text" name="txt_ajmc" />&nbsp;&nbsp;
            申请人证号：<input id="txt_gh" style="width: 100px;" class="l-text" type="text" name="txt_gh" />&nbsp;&nbsp;
           <%-- 律师姓名：<input id="txt_mc" style="width: 100px;" class="l-text" type="text" name="txt_mc" />&nbsp;&nbsp;--%>
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>

    <div id="top_div" style="border-width: 4px 1px 1px; border-style: solid;border-color: rgb(18, 155, 188) rgb(204, 204, 204) rgb(204, 204, 204); border-image: initial;background: white;padding: 10px; margin-bottom: 10px;border-radius: 10px;padding: 10px; display: none;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_print" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_yes" style="width: auto; display: inline-block; vertical-align: bottom;
            display: none;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_no" style="width: auto; display: inline-block; vertical-align: bottom;
            display: none;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
        <%--  <span id="time_span"></span>--%>
    </div>
    <div id="layout" style="width: 100%; margin: 0; padding: 0; display: none;">
        <div id="leftFrm" position="left" title='<%=((VersionName)0).ToString() %>目录'>
            <ul id="path_tree">
                <li style="padding: 5px 5px;">请先选择<%=((VersionName)0).ToString() %>，根据<%=((VersionName)0).ToString() %>加载对应目录！</li>
            </ul>
        </div>
        <div id="centterFrm" position="center" title="">
            <div id="pdfShow" style="margin: 0px; border: 0px; padding: 0px">
            </div>
        </div>
    </div>
    <%--输入用户名密码--%>
    <div id="login_div" style="padding: 0px; display: block;">
        <div id="mainGrid" style="margin: 0px; padding: 0px">
        </div>
        <%--<div style="padding: 10px 60px 20px 60px">
            <form id="login_form" method="post">
            <table class="l-table">
                <tr>
                    <td style="width: 90px;">
                        查阅账号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjzh" type="text" value="" name="txt_yjzh" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        查阅密码：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjmm" type="password" value="" name="txt_yjmm" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="btn_login" style="width: auto; display: inline-block; vertical-align: bottom;">
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>--%>
    </div>
    <%--审核说明--%>
    <div id="select_div" style="display: none;">
        <div id="tb" style="background-color: #f8f8f8">
            <div style="padding: 4px 5px;">
                <%-- <input id="txt_shsm" class="l-text" type="b" name="txt_name" style="width: 200px" />--%>
                <textarea id="txt_shsm" class="l-text" style="width: 400px; height: 150px;"></textarea>
            </div>
        </div>
    </div>
    <%--   <iframe id="printIframe" src="ReadingFilePrintShow.aspx?pm=20160804112459869" style="position: absolute; width: 1024px; height: 400px;
        left: 220px; top: 0px; z-index: 99999999"></iframe>--%>
    <script type="text/javascript">

        var path_tree;
        var grid;
        var yjdata;
        var time;
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            $("#btn_login").ligerButton({
                width: 80,
                text: '确定查阅',
                icon: '../../images/cx.png'
            });
            $("#btn_back").ligerButton({
                text: '返 回',
                icon: '../../images/fh.png'
            });
            $("#btn_yes").ligerButton({
                text: '通 过',
                icon: '../../images/tg_1.png'
            });
            $("#btn_no").ligerButton({
                width: 80,
                text: '不通过',
                icon: '../../images/xzdy.png'
            });
            $("#btn_print").ligerButton({
                text: '打 印',
                icon: '../../images/xzdy.png',
                click: function () {

                    var gridRow = grid.getSelectedRow();
                    var notes = path_tree.getChecked();

                    var dataJson = "";
                    if (notes.length == 0) {
                        $.ligerDialog.warn("请选择打印文件页");
                        return false;
                    }
                    for (var i = 0; i < notes.length; i++) {
                        //                        dataJson += notes[i].data.WJLJ + "," + notes[i].data.WJMC + "|";
                        dataJson += notes[i].data.WJMC + ",";

                        if (i == notes.length - 1) {
                            if (dataJson.length > 0) {
                                $.ajax({
                                    type: "POST",
                                    url: '/Pages/LSYJ/ReadingApply.aspx',
                                    data: { t: 'PrintFile', param: dataJson, xh: gridRow.XH },
                                    //    dataType: 'json',
                                    //timeout: 5000,
                                    cache: false,
                                    beforeSend: function () { },
                                    error: function (xhr) {
                                        $.ligerDialog.error('网络连接错误');
                                        return false;
                                    },
                                    success: function (data) {
                                        if (data.t == "error") {
                                            $.ligerDialog.error(data.v);
                                        } else {

                                            $.ligerDialog.success(data.v);

                                            // document.getElementById("printIframe").src = "ReadingFilePrint.aspx?t=pf&n=20160804112459869";
                                            //                                            var iframe = document.createElement('IFRAME');
                                            //                                            var doc = null;
                                            //                                            iframe.setAttribute('id', 'printIframe' + data.v);
                                            //                                            iframe.setAttribute('name', 'printIframe' + data.v);
                                            //                                            iframe.setAttribute('style', 'position:absolute;width:1024px;height:500px;left:200px;top:0px; z-index:99999999');
                                            //                                            //iframe.setAttribute('src', 'ReadingFilePrint.aspx?t=pf&n=20160804120826889');
                                            //                                            iframe.setAttribute('src', '/Scripts/generic/web/viewer.html?url=ReadingFilePrint.aspx?t=pf&n=' + data.v);

                                            //                                            document.body.appendChild(iframe);
                                            //                                            if (navigator.userAgent.indexOf("MSIE") > 0) {
                                            //                                                document.body.removeChild(iframe);
                                            //                                            }


                                            //    console.log(doc);
                                            //                                            document.getElementById("printIframe").contentWindow.focus();
                                            //                                            document.getElementById("printIframe").contentWindow.print();
                                            //                                            $(window.frames["printIframe"].document).focus();
                                            //                                            $(window.frames["printIframe"].document).print();



                                            //                                            setTimeout(function () {
                                            //                                                window.parent.document.getElementById('printIframe').contentWindow.print();
                                            //                                                iframe.contentWindow.focus();
                                            //                                                iframe.contentWindow.print();

                                            //                                            }, 5000);


                                        }
                                    }
                                });
                            } else {
                                $.ligerDialog.error("请选择打印文件页");
                            }
                        }
                    }
                }
            });

            var layout = $("#layout").ligerLayout({ leftWidth: 300, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();
            });
            function resizeLayout() {
                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
                $(".l-layout-center").css("width", width - 30 + "px");
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = height - 30;
                $(".l-layout-center").css("height", h -45 + "px");
                $("#leftFrm").height(h);
                $("#pdfFrm").height(h);
            }


            grid = $("#mainGrid").ligerGrid({
                columns: [

                { display: '申请人证号', name: 'LSZH', minWidth: 80 },
                //{ display: '打印申请单号', name: 'DYSQDH', minWidth: 150 },
                {display: '申请时间', name: 'SQSJ', minWidth: 150 },
                 { display: '案件编号', name: 'AJBH', minWidth: 150 },
                { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '打印人', name: 'DYR', minWidth: 150 },
                { display: '打印时间', name: 'DYSJ', minWidth: 150 },



                //                 { display: '审核单状态', name: 'SQDZT', minWidth: 150, render: function (item) {
                //                     if (item.SQDZT == "Y")
                //                         return "<span style=\"color:green;\">审核通过</span>";
                //                     else if (item.SQDZT == "N")
                //                         return "<span style=\"color:red;\">未通过</span>";
                //                     else
                //                         return "<span style=\"\">待审核</SPAN>";
                //                 }
                //                 },
                //{ display: '审核人', name: 'SHR', minWidth: 150 },
                // { display: '审核说明', name: 'SHSM', minWidth: 150 },
                //{ display: '审核时间', name: 'SHSJ', minWidth: 150 },
                 {display: '部门受案号', name: 'BMSAH', width: 1, hide: true },
                { display: '', name: 'XH', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/ReadingApply.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '选择打印', click: Read, img: '../../images/xzdy.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        //$.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();


            //查看
            $("#btn_login").click(function () {
                var jdata = $('#login_form').serializeArray();
                jdata[jdata.length] = { name: "t", value: "ReadLogin" };
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/ReadingApply.aspx',
                    data: jdata,
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () { },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t == "error") {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#login_div").hide();
                            $("#top_div").show();
                            $("#layout").show();
                            data = data[0];
                            $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + data.BMSAH + "】");
                            //YJUser =data
                          
                            JZMLTree(data.BMSAH, data.YJXH);
                        }
                    }
                });
            });
            //返回
            $("#btn_back").click(function () {
                $("#login_div").show();
                $("#tb_search").show();
                $("#top_div").hide();
                $("#layout").hide();
                clearInterval(time);
            });

            //var YJUser ' %=YJUser %';
            //            if(YJUser.BMSAH && YJUser.YJXH)
            //            {
            //                $("#span_ajname").html("当前卷宗：" + YJUser.AJMC + "&nbsp;【" + YJUser.BMSAH + "】");
            //                $("#login_div").hide();
            //                $("#top_div").show();
            //                $("#layout").show();
            //                JZMLTree(YJUser.BMSAH, YJUser.YJXH);
            //            }



        });

        //绑定文件目录
        function JZMLTree(bmsah, yjxh) {

            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/ReadingApply.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 400, 
                    onSuccess: function (data) {
                        if (data.t) {
                            $("#path_tree").html("<li style=\" padding:5px 5px;\">" + data.v + "</li>");
                        }
                    }, onBeforeExpand: function (node) {
                        if (node.data.children.length == 0) {                            
                            JZMLWJTree(node, "false");
                        }
                    }, onSelect: function (node) {                       
                        if ($.trim(node.data.WJMC) && $.trim(node.data.WJLJ)) {
                            $("#pdfFrm").remove();
                            $("#pdfShow").ligerPanel({
                                title: node.data.text,
                                width: '100%',
                                frameName: 'pdfFrm',
                                height: h,
                                url: '/Pages/LSYJ/ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))
                            });
                        }

                    }, onCheck: function (node, checked) {
                        if (checked) {
                            JZMLWJTree(node, checked);
                        }
                    }
                });

            }
        }

        //绑定文件
        function JZMLWJTree(node, ischecked) {
            if (node.data.ISLEAF == "0") {
                $.ligerDialog.waitting('数据读取中,请稍候...');
                $.post("/Pages/LSYJ/ReadingApply.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
                    if (newData.t) {
                        //$.ligerDialog.error(newData.v);
                    } else {

                        //alert(node.data.children.length)
                        //console.log(JSON.stringify(newData));
                        $(node.target).children("ul").remove();
                        path_tree.append(node.target, newData);
                        $(node.target).find(".l-expandable-close").click();
                    }

                    $.ligerDialog.closeWaitting();
                }, 'json');
            }
        }

        ///阅卷
        function Read() {
            var cc = ["#B537B7", "#49A5A6", "#8C9E42", "#561BB6", "#4F7D7B", "#427979", "#9D256D", "#60AEAD", "#417C90", "#B33E49", "#2B9F50", "#286CAA", "#9C9095", "#6A9B93", "#457983", "#24A091", "#3D9991", "#8A8A8F", "#5EA052", "#709430"];

            $("#top_div .l-button").each(function (index) {
                $(this).addClass("l-button-" + index);
                for (var k = 0; k <= index; k++) {
                    $(".l-button-" + k).css({ background: cc[k], color: "white", boxShadow: "0px 2px 2px 1px" + cc[k], borderRadius: "5px" });
                }
            });

            $("div .l-layout-left").css("height", $(window).height() - 90 + "px");  //重新设置左边框架的高度
            $("div #leftFrm").css("height", $(window).height() -120 + "px");  //重新设置左边框架的高度
            //            var jdata = $('#login_form').serializeArray();
            //            jdata[jdata.length] = { name: "t", value: "ReadLogin" };
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/ReadingApply.aspx',
                    data: { t: "ReadLogin", yjxh: cksld.YJXH },
                    dataType: 'json',
                    timeout: 50000,
                    cache: false,
                    beforeSend: function () { },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t == "error") {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#tb_search").hide();
                            $("#login_div").hide();
                            $("#top_div").show();
                            $("#layout").show();
                            data = data[0];
                            $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + cksld.AJBH + "】");

                            //YJUser =data
                            yjdata = data;
                            JZMLTree(data.BMSAH, data.YJXH);

                        }
                    }
                });
            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }

        }


        //通过
        $("#btn_yes").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {

                $.ligerDialog.confirm('确定通过该申请吗?', function (yes) {
                    if (yes == true) {
                        $.post("/Pages/LSYJ/ReadingApply.aspx", { t: "applypass", lszh: cksld.LSZH, dysqdh: cksld.DYSQDH, sqsj: cksld.SQSJ }, function (newData) {
                            if (newData.t) {
                                if (newData.t == "win") {
                                    $.ligerDialog.success(newData.v);
                                }
                                else {
                                    $.ligerDialog.error(newData.v);
                                }

                            } else {
                                $.ligerDialog.error(newData.v);
                            }

                            $.ligerDialog.closeWaitting();
                        }, 'json');

                    }

                })
            }
        });

        //不通过
        $("#btn_no").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ligerDialog.confirm('确定不通过该申请吗?', function (yes) {
                    if (yes == true) {
                        //审核说明
                        $.ligerDialog.show({ title: '审核说明', target: $('#select_div'), width: 450,
                            buttons: [{ text: '确定', onclick: function (item, dialog) {
                                $.post("/Pages/LSYJ/ReadingApply.aspx", { t: "applyno", lszh: cksld.LSZH, dysqdh: cksld.DYSQDH, sqsj: cksld.SQSJ, shsm: $("#txt_shsm").val() }, function (newData) {
                                    if (newData.t) {
                                        if (newData.t == "win") {
                                            $.ligerDialog.success(newData.v);
                                        }
                                        else {
                                            $.ligerDialog.error(newData.v);
                                        }

                                    } else {
                                        $.ligerDialog.error(newData.v);
                                    }

                                    $.ligerDialog.closeWaitting();
                                }, 'json');

                                dialog.hidden();
                            }, cls: 'l-dialog-btn-highlight'
                            },
                    { text: '取消', onclick: function (item, dialog) {
                        dialog.hidden();
                    }
                    }],
                            isResize: false
                        });
                    }

                })

            }
        });

        $(document).ready(function () {
           
            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        ajmc: $("#txt_ajmc").val(),
                        gh: $("#txt_gh").val(),
                        //mc: $("#txt_mc").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
