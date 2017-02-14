<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageButManager.aspx.cs"
    Inherits="WebUI.PageButManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>页面按钮管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <style type="text/css">
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #959595;
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
        .l-text, .l-textarea
        {
            width: 250px;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        .l-box-select-inner
        {
            height: 200px !important;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px; overflow: hidden;">
    <div id="tb" style="background-color: #f8f8f8">
        <div style="padding: 4px 5px; display: none;">
            按钮编号：<input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 60px 20px 60px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        按钮名称：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input type="text" id="txt_mc" name="txt_mc" class="l-text" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        按钮编号：
                    </td>
                    <td>
                        <input type="text" id="txt_xh" name="txt_xh" class="l-text" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        功能名称：
                    </td>
                    <td>
                        <input type="text" id="txt_ymmc" name="txt_ymmc" class="l-text" value="" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">

        var grid = null;
        var gnTree = null;
        var picon = "/images/icons/AddGroup.png";
        var chicon = "/images/icons/AddGroup.png";
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            
            gnTree = $("#txt_ymmc").ligerComboBox({
                width: 250,
                resize: false, //是否调整大小 
                selectBoxWidth: 250,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: "text",
                tree: {
                    url: '/Handler/ZZJG/ZZJGHandler.ashx',
                    parms: { action: "GetGnTree" },
                    ajaxType: 'get',
                    idFieldName: 'id',
                    textFieldName: "text",
                    parentIDFieldName: "PARENTID",
                    checkbox: false,
                    nodeWidth: " "
                }
            });


            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '按钮名称', name: 'ANMC', minWidth: 200 },
                { display: '页面编号', name: 'ANBH', minWidth: 200 },
                { display: '功能名称', name: 'GNMC', minWidth: 200 },
                { display: '', name: 'ANBM', minWidth: 0, hide: true }
                ], rownumbers: true, allowUnSelectRow: true, //是否允许取消选中行
                pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '95%',       //服务器分页
                url: '/Pages/SystemMgr/PageButManager.aspx?page=1',
                alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '增加', click: addDown, img: '../../images/NewAdd/add.png' },
               // { line: true },
                { text: '修改', click: editData, img: '../../images/NewAdd/xg.png' },
               // { line: true },
                { text: '删除', click: deleteData, img: '../../images/NewAdd/sc.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
        });

        //提交保持数据
        function submitForm() {
            var isUp = false;
            var jdata = $('#add_form').serializeArray();
            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else {
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp = true;
            }
            $.ajax({
                type: "POST",
                url: "/Pages/SystemMgr/PageButManager.aspx",
                data: jdata,
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
                        $("#add_form")[0].reset();
                        $.ligerDialog.hide();
                        $("#btn_search").click();
                        $.ligerDialog.success(data.v);
                    } else {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }


        //添加按钮
        function addDown() {
       
            $('#key_hidd').val('');
            $("#add_form")[0].reset();
            $.ligerDialog.open({ title: '添加功能分类', target: $('#add_div'), width: 500,
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
        }
        //修改
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {

                $.ajax({
                    type: "POST",
                    url: '/Pages/SystemMgr/PageButManager.aspx',
                    data: { t: "GetModel", id: cksld.ANBM },
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () {
                        // return $("#add_form").form('enableValidation').form('validate');
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#key_hidd").val(data.ANBM);
                            $("#txt_mc").val(data.ANMC);
                            gnTree.setValue(data.YMMC);
                            //$("#txt_ymmc").setValue(data.YMMC);
                            $("#txt_xh").val(data.ANBH);
                            $.ligerDialog.open({ title: '编辑功能分类', target: $('#add_div'), width: 500,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    submitForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#add_form")[0].reset();
                                    dialog.hidden();
                                }
                                }], isResize: true
                            });
                            // console.log(JSON.stringify(data));
                        }
                    }
                });
            }
            else
                $.ligerDialog.warn('请先选择一个需要编辑的配置信息');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.ANBM };
                ar[2] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/SystemMgr/PageButManager.aspx',
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
                                    var prowdata = grid.getSelectedRow();
                                    grid.deleteRow(prowdata);
                                    $.ligerDialog.success(data.v);
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择一个需要删除的单位');
                }

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
                                page: 1, pagesize: grid.options.pageSize
                            });
                        }
                    });
                    
                });
        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
        }
    </script>
</body>
  <script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
