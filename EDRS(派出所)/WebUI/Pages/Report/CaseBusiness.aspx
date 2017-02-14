<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseBusiness.aspx.cs" Inherits="WebUI.Pages.Report.CaseBusiness" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <%--表格插件--%>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--日期插件--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <%--弹出框插件--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <%--弹出框可拖动插件--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <%--表格宽度可调整--%>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerPanel.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <style type="text/css">
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        #searchbar{overflow-x:auto;}
        #searchbar table
        {
            width: auto;
        }
        #searchbar table tr td
        {
            white-space:nowrap;
        }
    </style>
</head>
<body>

    <div id="searchbar" style="padding: 5px; line-height: 28px;">
                    
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                    部门受案号：
                    <% } %>
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 205px" />
                    &nbsp;&nbsp; <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案件名称：
                    <% } %>
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 200px" />
                    &nbsp;&nbsp;承办单位：<input id="txt_unit" class="l-text" type="text" name="txt_unit" />
                    <br />
                    &nbsp;&nbsp;&nbsp;创建时间：<input id="txt_time_begin" type="text" name="txt_time_begin" />
                    &nbsp;&nbsp;-&nbsp;&nbsp;<input id="txt_time_end" type="text" name="txt_time_end" />
                    &nbsp;&nbsp;上传人：<input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                    &nbsp;&nbsp;业务类型：<input id="txt_ywlx" type="text" name="txt_ywlx" />
                    &nbsp;&nbsp;<input id="btn_search" type="button" class="l-button" value="搜 索" />
    </div>
    <div id="mainGrid" style="margin: 0; padding: 0;">
    </div>
    <script type="text/javascript">
        var grid = null;
        var cmbYwlx = null;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            cmbYwlx = $("#txt_ywlx").ligerComboBox({
                url: "/Handler/ZZJG/DZJZ_Report.ashx?action=GetAllBusinessType",
                valueField: "ywbm",
                textField: 'ywmc',
                selectBoxWidth: 200,
                selectBoxHeight: 300
            });
            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '业务类型', name: 'ywmc', align: 'center', width: 70 },
                { display: vn+'类别名称', name: 'ajlbmc', width: 100 },
                { display: '承办单位', name: 'cbdw_mc', align: 'center', width: 100 },
                { display: '承办部门', name: 'cbbm_mc', align: 'center', width: 100 },
                 <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    { display: '事项议题', name: 'ajmc', width: 150 },
                    { display: '唯一编号', name: 'bmsah', width: 200 },
                    <% }
                       else
                       { %>
                   { display: '案件名称', name: 'ajmc', width: 150 },
                   { display: '部门受案号', name: 'bmsah', width: 200 },
                    <% } %>
                
                
                { display: '承办人', name: 'cbr', width: 70 },
                { display: '创建时间', name: 'cjsj', align: 'center', width: 100 },
                { display: '卷数量', name: 'jcount', type: "int", width: 70, align: 'center', totalSummary: { type: 'sum'} },
                { display: '目录数', name: 'mcount', type: "int", width: 70, align: 'center', totalSummary: { type: 'sum'} },
                { display: '文件数', name: 'wcount', type: "int", width: 70, align: 'center', totalSummary: { type: 'sum'} },
                { display: '文件页数', name: 'pagecount', type: "int", width: 70, align: 'center', totalSummary: { type: 'sum'} }
                ], fixedCellHeight: false, rownumbers: true, pageSize: 20, dataAction: 'server', //服务器排序
                usePager: true, width: '99%', height: '100%', heightDiff: -5,      //服务器分页
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: {
                    action: "BusinessList",
                    key: $("#txt_key").val(),
                    unit: $("#txt_unit").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    ywlx: cmbYwlx.getValue(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onReload: function () {
                    $("#btn_search").click();
                }
            });
            //点击搜索按钮
            $("#btn_search").click(function () {
                grid.loadServerData({
                    action: "BusinessList",
                    key: $("#txt_key").val(),
                    unit: $("#txt_unit").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    ywlx: cmbYwlx.getValue(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val(),
                    page: 1, pagesize: grid.options.pageSize
                });
                grid.changePage("first"); //重置到第一页
            });
        });

    </script>
</body>
</html>
