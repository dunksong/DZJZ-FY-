<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSFPSJ.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSFPSJ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷分配</title>
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
<body style="margin: 0; overflow: hidden; padding: 10px;">
    <div style="padding-bottom: 5px;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
      &nbsp;&nbsp;&nbsp;&nbsp; 
        <div id="btn_submitapplyall" style="width: auto; display: inline-block; vertical-align: bottom; display:none;">
        </div>
      
        <div id="btn_save" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        
        <div id="btn_dy" style="width: auto; display: inline-block; vertical-align: bottom; display:none;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
          <span id="time_span"></span>
    </div>
    <div id="layout" style="width: 100%; margin: 0; padding: 0;">
        <div id="leftFrm" position="left" title='案件目录'>
            <ul id="path_tree">
                <li style="padding: 5px 5px;">请先选择案件，根据案件加载对应目录！</li>
            </ul>
        </div>
        <div id="centterFrm" position="center" title="">
            <div id="pdfShow" style="margin: 0px; border: 0px; padding: 0px">
            </div>
        </div>
    </div>
   
    <div style="display: none;">
    </div>
    <script type="text/javascript">
        var path_tree;
        var grid;
        var h = 0;
        var tree_diver1 = '/LigerUI/lib/LigerUI/skins/icons/archives.gif';
        var tree_folder1 = '/LigerUI/lib/LigerUI/skins/icons/calendar.gif';
        var tree_file1 = '/LigerUI/lib/LigerUI/skins/icons/attibutes.gif';
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        var vn = '案件';
        $(function () {
            $("#txt_yjkssj").ligerDateEditor({ labelWidth: 100, labelAlign: 'center', showTime: true });
            $("#txt_yjjssj").ligerDateEditor({ labelWidth: 100, labelAlign: 'center', showTime: true });

            $('#btn_search').ligerButton({
                width: 80,
                text: '选择' + vn,
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });

            $('#btn_search_aj').ligerButton({
                width: 60,
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            $("#btn_back").ligerButton({
                width: 80,
                text: '退出',
                icon: '/LigerUI/lib/LigerUI/skins/icons/back.gif'
            });

            $("#btn_submitapplyall").ligerButton({
                width: 80,
                text: '申请打印',
                icon: '/LigerUI/lib/LigerUI/skins/icons/logout.gif'
            });

            $("#btn_dy").ligerButton({
                width: 80,
                text: '打印',
                icon: '/LigerUI/lib/LigerUI/skins/icons/print.gif'
            });
            var layout = $("#layout").ligerLayout({ leftWidth: 380, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
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

            $("#pageloading").hide();

            //退出
            $("#btn_back").click(function () {
                $.ligerDialog.confirm('确定退出吗?', function (yes) {
                    if (yes == true) {
                        location.href = "/Pages/LSYJ/LSYJLogin.aspx";
                    }
                });
            });

            var bmsah = parent.GetQueryString(window.location.search.substr(1), "bmsah");
            var ajmc = parent.GetQueryString(window.location.search.substr(1), "ajmc");
            var ajbh = parent.GetQueryString(window.location.search.substr(1), "ajbh");

            setTimeout(function () {
                if (bmsah != null && bmsah != "" && bmsah != undefined) {
                    $("#span_ajname").html("当前卷宗：" + decodeURI(decodeURI(ajmc)) + "&nbsp;【" + decodeURI(decodeURI(ajbh)) + "】");
                    JZMLTree(bmsah);
                }
            }, 50);
        });
        var lszh = parent.GetQueryString(window.location.search.substr(1), "lszh");
        var yjxh = parent.GetQueryString(window.location.search.substr(1), "yjxh");
        //申请打印
        $("#btn_submitapplyall").click(function () {
            var b = path_tree.getChecked();
            var arr = [];
            if (b.length > 0) {
                for (var i = 0; i < b.length; i++) {
                    arr.push(b[i].data.id);
                }
                $.ligerDialog.confirm('确定提交申请吗?', function (yes) {
                    if (yes == true) {
                        $.ligerDialog.waitting('提交申请中,请稍候...');
                        $.post("/Pages/LSYJ/LSFPSJ.aspx", { t: "submitapplyall", 'idstr': arr, yjxh: yjxh, lszh: lszh }, function (newData) {
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
                });
            }
            else {
                $.ligerDialog.warn('请勾选要申请的页码');
            }
        });

        //绑定文件目录
        function JZMLTree(bmsah) {
            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/LSFPSJ.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: parent.GetQueryString(window.location.search.substr(1), "yjxh") },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    nodeWidth: 400,
                     onSuccess: function (data) {

                        if (data.t) {

                            $("#path_tree").html("<li style=\" padding:5px 5px;\">" + data.v + "</li>");
                        }
                        sh();
                        readtime();
                        IsSh();
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
                                url: 'LSReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))

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
                $.post("/Pages/LSYJ/LSFPSJ.aspx?pa=2", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
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
        //获取URL参数
        function GetQueryString(url, name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            // var r = window.location.search.substr(1).match(reg);
            var r = url.match(reg);
            if (r != null) return (r[2]); return null;
        }

        function readtime() {

            var time = setInterval(function () {

                $.post("/Handler/Common/ZZJGCommonHandler.ashx?action=Time", function (data) {
                    if (data == "N" || data == "" || data == undefined || data == null) {
                        $("#login_div").show();
                        $("#top_div").hide();
                        $("#layout").hide();                       
                        clearInterval(time);
                    }
                    else
                        $("#time_span").html("剩余阅读时间：" + data);
                }, 'text');
            }, 1000);
        }

        function IsSh() {
            var time = setInterval(sh, 10000);
        }
        function sh() {
            $.post("/Pages/LSYJ/LSFPSJ.aspx", { t: "IsSh", yjxh: yjxh }, function (data) {
               
                if (data == "y") {
                    $("#btn_submitapplyall").hide();
                    $("#btn_dy").show();
                }
                else {
                    $("#btn_dy").hide();
                    $("#btn_submitapplyall").show();
                }
            }, 'text');
        }
    </script>
</body>
</html>
