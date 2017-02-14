<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadApplicants.aspx.cs"
    Inherits="WebUI.Pages.LSYJ.ReadApplicants" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷申请</title>
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
        .l-table-edit
        {
            margin: 0 auto;
        }
        .l-table-edit tr td
        {
            padding: 5px 0px;
        }
    </style>
</head>
<body style="margin: 0; overflow: hidden;">
    <div style="padding: 6px 5px;">
        关键字：<input type="text" class="l-text" id="txt_key" value="" />
        <input class="l-button" type="button" id="btn_search" value="搜索" />
    </div>
    <div id="maingrid">
    </div>
    <div style="display: none;">
    </div>
    <div id="editor" style="display: none;">
        <form id="editor_form" method="post">
        <input type="hidden" name="hidd_sqbm" id="hidd_sqbm" value="" />
        <input type="hidden" name="hidd_type" id="hidd_type" value="" />
        <table cellpadding="0" cellspacing="0" class="l-table-edit">
            <tr>
                <td align="right" class="l-table-edit-td">
                    律师证号 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_lszh" type="text" id="txt_lszh" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    律师姓名 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_lsxm" type="text" id="txt_lsxm" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    所属单位 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_ssdw" type="text" id="txt_ssdw" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案件名称：
                    <% } %>
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_ajmc" type="text" id="txt_ajmc" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    嫌疑人 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_xyrmc" type="text" id="txt_xyrmc" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    申请人 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_sqrmc" type="text" id="txt_sqrmc" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">
                    申请单位 ：
                </td>
                <td align="left" class="l-table-edit-td">
                    <input name="txt_sqdw" type="text" id="txt_sqdw" ltype="text" />
                </td>
                <td align="left">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        var maingrid;
        $(function () {
            $("form").ligerForm({
                inputWidth: 260
            });

            maingrid = $("#maingrid").ligerGrid({
                columns: [
                    { display: '状态', name: 'SQDZT', align: 'center', minWidth: 150, render: function (item) {
                        if (item.SQDZT == "D")
                            return "待审核";
                        else if (item.SQDZT == "N")
                            return "已驳回";
                        else if (item.SQDZT == "Y")
                            return "已审核";
                        else "未知";
                    }
                },
                     <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                   { display: '事项议题', name: 'AJMC', minWidth: 150 },
                    <% }
                       else
                       { %>
                    { display: '案件名称', name: 'AJMC', minWidth: 150 },
                    <% } %>
                   
                    { display: '嫌疑人', name: 'XYRMC', align: 'center', minWidth: 150 },
                    { display: '申请人', name: 'SQRMC', align: 'center', minWidth: 150 },
                    { display: '申请单位', name: 'SQDW', minWidth: 150 },
                    { display: '申请时间', name: 'SQSJ', align: 'center', minWidth: 150 },
                    { display: '律师证号', name: 'LSZH', align: 'center', minWidth: 150 },
                    { display: '律师姓名', name: 'LSXM', align: 'center', minWidth: 150 },
                    { display: '所属单位', name: 'SSDW', minWidth: 150 },
                    { display: '下载地址', name: 'XZDZ', minWidth: 150 },
                    { display: '审核人', name: 'SHR', align: 'center', minWidth: 150 },
                    { display: '审核时间', name: 'SHSJ', align: 'center', minWidth: 150 },
                    { display: '备注', name: 'BZ', minWidth: 150 },
                    { display: '申请编码', name: 'SQBM', width: 1, hide: true }
                ], fixedCellHeight: false,
                usePager: true,
                rownumbers: true,
                pageSize: 50, pageSizeOptions: [20, 50, 100, 500],
                heightDiff: -10,
                height: '100%', width: '100%',
                url: 'ReadApplicants.aspx',
                parms: { t: "gridbind", key: $("#txt_key").val() },
                toolbar: { items: [
                    { text: '申请', click: f_added, icon: 'add' },
                    { line: true },
                    { text: '修改', click: f_amend, icon: 'modify' },
                    { line: true },
                    { text: '删除', click: f_strikeout, icon: 'delete'}]
                }
            }); 
            
            $("#pageloading").hide();
            //查询
            $("#btn_search").click(function () {
                if (maingrid.options.page > 1) {
                    f_gridparm();
                    maingrid.changePage("first"); //重置到第一页     
                } else {
                    maingrid.loadServerData({
                        t: "gridbind",
                        key: $("#txt_key").val(),
                        page: 1, pagesize: maingrid.options.pageSize
                    });
                }
            });
        });
        var ligerdialog;
        function f_dialog() {
            if (ligerdialog)
                ligerdialog.show();
            else {
                ligerdialog = $.ligerDialog({ title: '编辑', target: $("#editor"), width: 500,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        f_submint();
                    }, cls: 'l-dialog-btn-highlight'
                    }, { text: '取消', onclick: function (item, dialog) {
                        $("#editor_form")[0].reset();
                        ligerdialog.hidden();
                    }
                    }], isResize: true
                });
            }
        }
        //提交
        function f_submint() {
            var jdata = $('#editor_form').serializeArray();
            jdata[jdata.length] = { name: "t", value: $("#hidd_type").val() };
            $.ajax({
                type: "post",
                url: "ReadApplicants.aspx",
                data: jdata, dataType: 'json', timeout: 10000, cache: false,
                beforeSend: function () { },
                error: function () {
                    $.ligerDialog.error('网络连接错误'); return false;
                },
                success: function (data) {
                    if (data.t == "win") {
                        $("#editor_form")[0].reset();
                        ligerdialog.hidden(); maingrid.reload(); $.ligerDialog.success(data.v);
                    } else { $.ligerDialog.error(data.v); }
                }
            });
        }
        //添加
        function f_added() {
            $("#hidd_type").val("add"); f_dialog();
        }
        //修改
        function f_amend() {
            var rowdata = maingrid.getSelectedRow();
            if (rowdata != null) {
                $.post('ReadApplicants.aspx', { t: "getmodel", SQBM: rowdata.SQBM }, function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    } else {
                        $("#hidd_sqbm").val(data.SQBM);
                        $("#txt_lszh").val(data.LSZH);
                        $("#txt_lsxm").val(data.LSXM);
                        $("#txt_ssdw").val(data.SSDW);
                        $("#txt_ajmc").val(data.AJMC);
                        $("#txt_xyrmc").val(data.XYRMC);
                        $("#txt_sqrmc").val(data.SQRMC);
                        $("#txt_sqdw").val(data.SQDW);
                        $("#hidd_type").val("update");
                        f_dialog();
                    }
                }, 'json');
            } else $.ligerDialog.warn("请选择修改信息");
        }
        //删除
        function f_strikeout() {
            var rowdata = maingrid.getSelectedRow();
            if (rowdata) {
                var ar = new Array();
                ar[0] = { name: "id", value: rowdata.SQBM }; ar[1] = { name: "t", value: "delete" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "post",
                            url: 'ReadApplicants.aspx',
                            data: ar, dataType: 'json', timeout: 5000, cache: false,
                            beforeSend: function () { },
                            error: function () { $.ligerDialog.error('网络连接错误'); return false; },
                            success: function (data) {
                                if (data.t == "win") {
                                    maingrid.deleteRow(rowdata);
                                    $.ligerDialog.success(data.v);
                                } else $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else $.ligerDialog.warn('请先选择一个需要删除的信息');
        }
        //重置参数
        function f_gridparm() {
            maingrid.setParm("key", $("#txt_key").val());
        } </script>
</body>
</html>
