<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ywbmManage.aspx.cs" Inherits="WebUI.Pages.LBGL.ywbmManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>业务编码管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
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
        .l-text-combobox
        {
            display: inline-table;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        div#tb {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px; overflow: hidden;">
    <div id="tb">
        <div >
            业务名称：<input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 20px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        业务名称：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input class="l-text" id="txt_name" type="text" name="txt_name" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        业务简称：
                    </td>
                    <td>
                        <input class="l-text" id="txt_namejc" type="text" name="txt_namejc" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        备注：
                    </td>
                    <td>
                        <input class="l-text" id="txt_bz" type="text" name="txt_bz" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">

        var grid = null;
        var select = null;

        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });

            grid = $("#mainGrid").ligerGrid({
                columns: [            
                { display: '业务名称', name: 'YWMC', minWidth: 150 },
                { display: '业务简称', name: 'YWJC', minWidth: 150 },
               // { display: '序号', name: 'XH', minWidth: 150 },
                { display: '备注', name: 'BZ', minWidth: 150 },                
                
                { display: '业务编码', name: 'YWBM', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LBGL/ywbmManage.aspx',
                alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '增加', click: addDown, img: '../../images/NewAdd/add.png' },
               // { line: true },
                {text: '修改', click: editData, img: '../../images/NewAdd/xg.png' },
               // { line: true },
                {text: '删除', click: deleteData, img: '../../images/NewAdd/sc.png' }
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
                url: "/Pages/LBGL/ywbmManage.aspx",
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
                        grid.reload();
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

            $.ligerDialog.open({ title: '编辑类别', target: $('#add_div'), width: 570,
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
                    url: '/Pages/LBGL/ywbmManage.aspx',
                    data: { t: "GetModel", id: cksld.YWBM },
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

                            $("#key_hidd").val(data.YWBM);
                            $("#txt_name").val(data.YWMC);
                            $("#txt_namejc").val(data.YWJC);
                            $("#txt_bz").val(data.BZ);
                            $.ligerDialog.open({ title: '编辑', target: $('#add_div'), width: 570,
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
                ar[0] = { name: "id", value: arrck.YWBM };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/LBGL/ywbmManage.aspx',
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
