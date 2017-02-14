<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffAuthorityManager.aspx.cs"
    Inherits="WebUI.Pages.QXGL.StaffAuthorityManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>人员权限管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
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
        
        
        /* 按钮 */
          div .l-button {
            color: white;
        }
        div#btn_search {
            background: #ed6d4a;
        }
        .l-toolbar-item.l-panel-btn.l-toolbar-item-hasicon {
            background: #339bca;
            color: white;
        }
        
        
                                /*左右边框*/
      div .l-layout-left{
         border: 1px solid #dde0e3;
         border-top: 4px solid #129bbc;
         border-radius: 10px;
    }
    .l-layout-center {
    border: 1px solid #dde0e3;
    border-top: 4px solid #129bbc;
    border-radius: 10px;
}
        .l-panel { 
            overflow: hidden;
            border-top: 1px solid #ccc;
            border-radius: 0;
}
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding: 10px;">
    <div id="layout" style="width: 100%; margin: 0; padding: 0;">
        <div id="leftFrm" position="left" title="功能">
            <ul id="tree_left">
            </ul>
        </div>
        <div id="centterFrm" position="center" title="部门操作">
            <div id="tb" >
                <div style="padding: 4px 5px;">
                    关键字
                    <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
                    <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </div>
            </div>
            <div id="mainGrid" style="margin: 0px; padding: 0px">
            </div>
        </div>
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="display: none; margin: 0 auto;">
        <form id="add_form" method="post">
        <div id="rytb" style="background-color: #f8f8f8">
            <div style="padding: 4px 5px;">
                关键字
                <input id="txt_key_ry" style="width: 200px;" class="l-text" type="text" name="txt_keyry" />
                <div id="btn_search_ry" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                </div>
            </div>
        </div>
        <div id="mainGrid_ry" style="margin: 0px; padding: 0px">
        </div>
        </form>
    </div>
    <script type="text/javascript">
        var grid = null;
        var grid_ry = null;
        var tree = null;
        var tree_dw = '/images/icons/3.png';
        var tree_bm = '/images/icons/bm.png';
        var tree_js = '/images/icons/4.png';
        var picon = "";
        var chicon = "/images/icons/AddGroup.png";
        $(function () {

            $('#btn_search,#btn_search_ry').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
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
                $("#leftFrm").height(height - 30);
            }
            //加载树
            tree = $("#tree_left").ligerTree({
                url: "/Pages/QXGL/StaffAuthorityManager.aspx",
                parms: {
                    t: 'GetTreeDW', level: 3
                },
                isExpand: 2,
                checkbox: true,
                slide: false,
                nodeWidth: 120,
                onClick: function (node) {
                    if (tree.getSelected()) {
                        grid.setParm("t", "GetDataGn");
                        grid.setParm("gn", tree.getSelected().data.id);
                        grid.setParm("key", $("#txt_key").val())
                        grid.reload();
                    }
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                    else {
                        $("#tree_left").children("li").each(function () {
                            var text = $(this).children("div").children("span").text();
                            $(this).children("div").children("span").prev("div").find("img").attr("src", parent.MainMenuIcon(text));
                        });
                    }
                }
            });


            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '功能名称', name: 'GNMC', minWidth: 100 },
                { display: '姓名', name: 'MC', minWidth: 100 },
                { display: '性别', name: 'XB', minWidth: 100, render: function (item) {
                    if (item.XB == '0') return '女';
                    else if (item.XB == '1') return "男";
                    else return "";
                }
                },
                { display: '工号', name: 'GH', minWidth: 100 },
                { display: '登录别名', name: 'DLBM', minWidth: 100 },
                { display: '工作证号', name: 'GZZH', minWidth: 100 },
                { display: '功能编码', name: 'GNBM', width: 1, hide: 'none' },
                { display: '单位', name: 'DWMC', width: 1, hide: 'none' }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , fixedCellHeight: false, width: '100%', height: '100%', heightDiff: -14,        //服务器分页
                url: '/Pages/QXGL/StaffAuthorityManager.aspx',
                usePager: true, checkbox: true,
                parms: { t: "GetDataGn" }
                , onSuccess: function (data) {
                    if (data.t && data.v != "") {
                        $.ligerDialog.error(data.v);
                    }
                }, toolbar: { items: [
                { text: '增加人员权限', click: addData, img: '../../images/add.png' },
              ///  { line: true },
                { text: '删除人员权限', click: DelData, img: '../../images/sc.png' }
                ]
                }
            });

            $("#pageloading").hide();
        });


        //提交保持数据
        function submitForm() {

            $.ligerDialog.waitting('正在保存中,请稍候...');


            var ghs = "";
            var rylist = grid_ry.getSelectedRows();
            for (var i = 0; i < rylist.length; i++) {
                ghs += rylist[i].GH + ",";
            }
            var gns = "";
            var tr = tree.getChecked();            
            for (var i = 0; i < tr.length; i++) {
                if (tr[i].data.PARENTID != "")
                    gns += tr[i].data.ID + ",";
            }
           
            //console.log(JSON.stringify(rylist));

            $.ajax({
                type: "POST",
                url: "/Pages/QXGL/StaffAuthorityManager.aspx",
                data: { t: "AddData", ghs: ghs, gns: gns },
                dataType: 'json',
                timeout: 60000,
                cache: false,
                beforeSend: function () {
                },
                error: function (xhr) {
                    $.ligerDialog.closeWaitting();
                    $.ligerDialog.error('网络连接错误');
                    return false;
                },
                success: function (data) {
                    $.ligerDialog.closeWaitting();

                    if (data.t == "win") {
                        $("#add_form")[0].reset();
                        $.ligerDialog.hide();
                        grid.reload();
                        $.ligerDialog.success(data.v);
                    } else
                        $.ligerDialog.error(data.v);
                }
            });
        }

        //添加部门按钮
        function addData() {
            var sv = tree.getChecked();
            if (sv != "" && sv != null) {
                GetListData();
                $.ligerDialog.open({ title: '选择人员', target: $('#add_div'), width: 600,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        submitForm();
                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#add_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
                });

            } else
                $.ligerDialog.warn('请先选择功能');
        }


        function GetListData() {
            //绑定列表
            grid_ry = $("#mainGrid_ry").ligerGrid({
                columns: [
                { display: '姓名', name: 'MC', minWidth: 100 },
                { display: '性别', name: 'XB', minWidth: 100, render: function (item) {
                    if (item.XB == '0') return '女';
                    else if (item.XB == '1') return "男";
                    else return "";
                }
                },
                { display: '工号', name: 'GH', minWidth: 100 },
                { display: '登录别名', name: 'DLBM', minWidth: 100 },
                { display: '工作证号', name: 'GZZH', minWidth: 100 },
                //                { display: '联系电话', name: 'YDDHHM', minWidth: 100 },
                //                { display: '电子邮件', name: 'DZYJ', minWidth: 100 },
                {display: '单位', name: 'DWMC', width: 1, hide: 'none' }
                ], rownumbers: true, pageSize: 20, pageSizeOptions: [20]
                , fixedCellHeight: false, width: '99%', height: 450, heightDiff: -14,        //服务器分页
                url: '/Pages/QXGL/StaffAuthorityManager.aspx',
                usePager: true, checkbox: true,
                parms: { t: "GetData" }
                , onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }

        //删除按钮
        function DelData() {
            var arrck = grid.getSelectedRows();
            if (arrck != "") {
                //console.log(JSON.stringify(arrck));
                var argh = "";
            
                for (var i = 0; i < arrck.length; i++) {
                    argh += arrck[i].DWBM + arrck[i].GH + arrck[i].GNBM;
                    if (arrck.length - 1 > i)
                        argh += ",";
                }
                var ar = new Array();
                ar[0] = { name: "gh", value: argh };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/QXGL/StaffAuthorityManager.aspx',
                            data: ar,
                            dataType: 'json',
                            timeout: 5000,
                            cache: false,
                            beforeSend: function () { },
                            error: function (xhr) {
                                $.ligerDialog.error('网络连接错误');
                                return false;
                            },
                            success: function (data) {
                                if (data.t == "win") {
                                    grid.reload();
                                    $.ligerDialog.success(data.v);
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择人员再删除');
        }

        $(document).ready(function () {
            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    grid.setParm("key", $("#txt_key").val());
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "GetDataGn",
                        gn: (tree.getSelected() != null ? tree.getSelected().data.id : ""),
                        key: $("#txt_key").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });
            //点击搜索按钮
            $("#btn_search_ry").click(function () {
                if (grid.options.page > 1) {
                    grid_ry.setParm("key", $("#txt_key_ry").val());
                    grid_ry.changePage("first"); //重置到第一页         
                } else {
                    grid_ry.loadServerData({
                        t: "GetData",
                        key: $("#txt_key_ry").val(),
                        page: 1, pagesize: grid_ry.options.pageSize
                    });
                }

            });
        });
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
