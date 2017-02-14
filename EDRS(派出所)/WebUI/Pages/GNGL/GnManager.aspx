<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GnManager.aspx.cs" Inherits="WebUI.Pages.GNGL.GnManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/ligerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <%--<link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />--%>
    <!--<link href="/ligerUI/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css"/>-->
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <style>
    div#divright,div #divleft {
        overflow: hidden;
        border: 1px solid #dde0e3;
        border-top: 4px solid #129bbc;
        border-radius: 10px;
    }
    .l-panel
    {
        border: none;
        border-radius: 0;
        }
    </style>
    <script type="text/javascript">

        var picon = "";
        var chicon = "/images/icons/AddGroup.png";
        $(function () {

            $('#btn_addRole').ligerButton({
                click: AddRole,
                text: '添加权限',
                width: 70,
                icon: '../../images/add.png'
            });

//            $('#btn_editRole').ligerButton({
//                click: UpdateRole,
//                text: '修改权限',
//                width: 70,
//                icon: '../../LigerUI/lib/LigerUI/skins/icons/edit.gif'
//            });


            $('#btn_delRole').ligerButton({
                click: RemoveRole,
                text: '删除权限',
                width: 70,
                icon: '../../images/sc.png'
            });
            $("#divleft").ligerPanel({
                title: '未选择权限',
                showClose: false,
                showToggle: false,
                width: 200,
                height: 400
            });

            $("#divright").ligerPanel({
                title: '已选择权限',
                showClose: false,
                showToggle: false,
                width: 600,
                height: 400
            }); ;
            loadData();
        });
        function loadData() {
            //加载树菜单
            $('#tree_ZzjgSelectJs').ligerTree({
                url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetWfpgnTreeData&bmbm=' + parent.__bm + '&jsbm=' + parent.__js + "&dwbm=" + parent.G_ZzjgDwbm,
                //  iconFieldName: "icon",
                nodeWidth: 100,
                treeLine: true,
                slide: false,
                checkbox: false,
                onSelect: function (node) {

                }, onSuccess: function (data) {
                    $("#tree_ZzjgSelectJs").children("li").each(function () {
                        var text = $(this).children("div").children("span").text();
                        $(this).children("div").children("span").prev("div").find("img").attr("src", parent.parent.MainMenuIcon(text));
                    });
                }
            });

            //加载已选权限


            $('#mainGrid').ligerGrid({
                columns: [
                        { display: '功能名称', name: 'Gnmc', width: 150 },
//                        { display: '显示名称', name: 'Gnxsmc', width: 150 },                        
                        { display: '功能窗体', name: 'Gnct', width: 150 },
                        { display: '功能序号', name: 'Gnxh', width: 70 },
                        { display: '功能说明', name: 'Gnsm', width: 150 },
//                        { display: '功能参数', name: 'Gncs', width: 150 },                        
                        { display: '功能编码', name: 'Gnbm', width: 1, hide: "none" },
                        { display: '功能分类', name: 'Gnfl', width: 1, hide: "none" }
                        ],
                width: '100%',
                height: '100%',
                heightDiff: -30,
                url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetJsQxInfo&dwbm=' + parent.G_ZzjgDwbm + '&bmbm=' + parent.__bm + '&jsbm=' + parent.__js,
                pageSizeOptions: [20, 50, 100, 500],
                pageSize: 50,
                checkbox: false,
                dataAction: "local"
            });
        }
        function AddRole() {
            var nodeGn = $('#tree_ZzjgSelectJs').ligerTree().getSelected();
            if (nodeGn == null) {
                return;
            }
            var gnbm = nodeGn.data.id;
            var gnmc = nodeGn.data.text;
            var gnfl = $(nodeGn.target).parent().parent()[0].id;
            if (gnfl == "")
                return;
            var bz = ''; //$('#gnbz_ZzjgSelectJs').val();
            //            bz = trim(bz);
            var bmbm = parent.__bm;
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddJsGnQx", { dwbm: parent.G_ZzjgDwbm, bmbm: bmbm, jsbm: parent.__js, gnbm: gnbm, bz: bz, gnmc: gnmc },
                    function (result) {
                        //Alert("操作结果: " + result);
                        loadData();
                    });
        }

        function UpdateRole() {
            var rowDatas = $('#mainGrid').ligerGrid().getSelectedRow();
            var gnbm = rowDatas.Gnbm;
            var gnmc = rowDatas.Gnmc;
            var gncs = rowDatas.Gncs;
            $('#gncs_ZzjgSelectJs').val(gncs);
            $.ligerDialog.open({
                target: $("#winUpdateJsGn_ZzjgSelectJs"),
                height: 250,
                width: 400,
                isResize: false,
                title: "添加功能权限",
                buttons:
                    [
                    { text: '确定', onclick: function (item, dialog) {
                        if (item.text == '确定') {
                            SaveUpdate(dialog);
                        }
                    }
                    },
                    { text: '取消', onclick: function (item, dialog) { dialog.close(); } }
                    ]
            });
        }
        function SaveUpdate(dialog) {
            var gncs = $('#gncs_ZzjgSelectJs').val();
            var rowDatas = $('#mainGrid').ligerGrid().getSelectedRow();
            var gnbm = rowDatas.Gnbm;
            var gnmc = rowDatas.Gnmc;
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=UpdateJsGnCs", { dwbm: parent.G_ZzjgDwbm, bmbm: parent.__bm, jsbm: parent.__js, gnbm: gnbm, gncs: gncs, gnmc: gnmc },
                    function (result) {
                        //Alert("操作结果: " + result);
                        dialog.hidden();
                        loadData();
                    });
        }
        function RemoveRole() {

            var rowDatas = $('#mainGrid').ligerGrid().getSelectedRow();
            var gnbm = '1' + rowDatas.Gnbm;
            var gnmc = rowDatas.Gnmc;
            var jsbm = parent.__js;

            $.ligerDialog.confirm('请确认是否删除？', function (yes) {
                if (yes) {
                    $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=DeleteJsGnQx", { dwbm: parent.G_ZzjgDwbm, gnbm: gnbm, jsbm: jsbm, gnmc: gnmc },
            function (result) {
                //刷新数据
                loadData();
            });
                }
            });
        }
    </script>
</head>
<body id="searchbar">
    <div style="width: 900px; margin: 10px;">
        <div style="float: left;" id="divleft">
            <div style="border: 1px solid rgb(204, 204, 204); width: 198px; height: 373px; overflow: auto;
                position: relative;">
                <ul id="tree_ZzjgSelectJs">
                </ul>
            </div>
        </div>
        <div style="float: left; width: 16px; padding: 10px; margin-top: 100px;" id="divmiddle">
            <div id="btn_addRole" type="button">
            </div>
            <p>
                &nbsp;</p>
            <div id="btn_editRole" type="button" style=" display:none;">
            </div>
            <p>
                &nbsp;</p>
            <div id="btn_delRole" type="button">
            </div>
        </div>
        <div style="float: right;" id="divright">
            <div id="mainGrid">
            </div>
        </div>
        <div id="winUpdateJsGn_ZzjgSelectJs" style="width: 100%; height: 200px; display: none;">
            <table id="gnTable_ZzjgSelectJs" style="width: 100%">
                <tr>
                    <td style="width: 100px;">
                        功能参数:
                    </td>
                    <td>
                        <textarea id="gncs_ZzjgSelectJs" rows="10" cols="40"></textarea>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>