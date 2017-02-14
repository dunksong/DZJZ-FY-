<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingPage.aspx.cs" Inherits="WebUI.Pages.LSYJ.ReadingPage" %>

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
    <style type="text/css">
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #a3c0e8;
            display: inline-block;
            width: 100%;
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
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding: 0px;">
    <div id="top_div" style="padding: 5px; display: none;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
         <div id="btn_submit" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
         <div id="btn_apply" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
        <span id="time_span"></span>
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
            $("#btn_login").ligerButton({
                width: 80,
                text: '确定查阅',
                icon: '/LigerUI/lib/LigerUI/skins/icons/right.gif'
            });
            $("#btn_back").ligerButton({
                width: 80,
                text: '返 回',
                icon: '/LigerUI/lib/LigerUI/skins/icons/back.gif'
            });
            $("#btn_submit").ligerButton({
                width: 80,
                text: '提交申请',
                icon: '/LigerUI/lib/LigerUI/skins/icons/ok.gif'
            });
            $("#btn_apply").ligerButton({
                width: 80,
                text: '申请当前页',
                icon: '/LigerUI/lib/LigerUI/skins/icons/ok.gif'
            });
            var layout = $("#layout").ligerLayout({ leftWidth: 200, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
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
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = height - 30;
                $("#leftFrm").height(h);
                $("#pdfFrm").height(h);
            }


            grid = $("#mainGrid").ligerGrid({
                columns: [
                //                { display: '阅卷人工号', name: 'GH', minWidth: 150 },
                //                { display: '阅卷人姓名', name: 'MC', minWidth: 150 },

           {display: '案件编号', name: 'AJBH', minWidth: 250 },
                  { display: '案件名称', name: 'AJMC', minWidth: 80 },

            
                { display: '阅卷序号', name: 'YJXH', width: 1, hide: true },

                { display: '打印申请单号', name: 'DYSQDH', minWidth: 150 },
                 { display: '审核单状态', name: 'SQDZT', minWidth: 150, render: function (item) {
                     if (item.SQDZT == "Y")
                         return "<span style=\"color:green;\">审核通过</span>";
                     else if (item.SQDZT == "N")
                         return "<span style=\"color:red;\">未通过</span>";
                     else
                         return "<span style=\"\">待审核</SPAN>";
                 }
             },
                { display: '审核人', name: 'SHR', minWidth: 150 },
                { display: '审核说明', name: 'SHSM', minWidth: 150 },
                { display: '审核时间', name: 'SHSJ', minWidth: 150 },


                { display: '', name: 'BMSAH', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/ReadingPage.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                    //{ text: '申请', click: addDown, icon: 'add' },
                    //                { line: true },
                    //                { text: '审核', click: examineData, img: '/images/icons/edit.png' },
                    //                {line: true },
                    //                { text: '修改', click: editData, icon: 'modify' },
                    //                { line: true },
                    //                { text: '删除', click: deleteData, icon: 'delete' },
                    //              {line: true },
                {text: '阅卷', click: Read, img: '/images/icons/edit.png' }
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
                    url: '/Pages/LSYJ/ReadingPage.aspx',
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
                $("#top_div").hide();
                $("#layout").hide();
                clearInterval(time);
            });

            //申请当前页
            $("#btn_apply").click(function () {
                var cksld = grid.getSelectedRow();
                var d = path_tree.getSelected();

                if (d != null && cksld!= null) {
                    $.ligerDialog.waitting('提交申请中,请稍候...');
                    $.post("/Pages/LSYJ/ReadingPage.aspx", { t: "submitapply", id: d.data.ID, yjxh: cksld.YJXH }, function (newData) {
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
                else {
                    $.ligerDialog.warn('请选择申请页码');
                }
            })

            //提交申请
            $("#btn_submit").click(function () {
                var cksld = grid.getSelectedRow();
                var b = path_tree.getChecked();
                //alert(JSON.stringify(b));
                //return false;
                var arr = [];
                if (b.length > 0) {
                    for (var i = 0; i < b.length; i++) {
                        arr.push(b[i].data.id);
                    }
                    $.ligerDialog.waitting('提交申请中,请稍候...');
                    $.post("/Pages/LSYJ/ReadingPage.aspx", { t: "submitapplyall", 'idstr': arr, yjxh: cksld.YJXH }, function (newData) {
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
                else {
                    $.ligerDialog.warn('请勾选要申请的页码');
                }

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
                    url: "/Pages/LSYJ/ReadingPage.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 106
                    , onSuccess: function (data) {
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
                                    url: 'ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))

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
                $.post("/Pages/LSYJ/ReadingPage.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
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
//            var jdata = $('#login_form').serializeArray();
            //            jdata[jdata.length] = { name: "t", value: "ReadLogin" };
           var cksld = grid.getSelectedRow();
           if (cksld != null) {
               $.ajax({
                   type: "POST",
                   url: '/Pages/LSYJ/ReadingPage.aspx',
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
                           $("#login_div").hide();
                           $("#top_div").show();
                           $("#layout").show();
                           data = data[0];
                           $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + cksld.AJBH + "】");
                           //YJUser =data
                           yjdata = data;
                           JZMLTree(data.BMSAH, data.YJXH);

                           readtime();

                       }
                   }
               });
           }
           else {
               $.ligerDialog.warn('请选择案件');
           }
        }
        function readtime() {
//            time = setInterval(function () {
//                
//                $.post("/Handler/Common/ZZJGCommonHandler.ashx?action=Time", function (data) {
//                    if (data == "N" || data == "" || data == undefined || data == null) {
//                        $("#login_div").show();
//                        $("#top_div").hide();
//                        $("#layout").hide();
//                        clearInterval(time);
//                    }
//                    else
//                        $("#time_span").html("剩余阅读时间：" + data);
//                }, 'text');
//            }, 1000);
        }
    </script>
</body>
</html>
