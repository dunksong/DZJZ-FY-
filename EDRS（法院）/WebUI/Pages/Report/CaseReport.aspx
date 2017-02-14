<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseReport.aspx.cs" Inherits="WebUI.CaseReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>信息管理</title>
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
    <style type="text/css">
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        #actvxhelp
        {
            background: url("/LigerUI/lib/LigerUI/skins/icons/help.gif") no-repeat left center;
            padding-left: 18px;
        }
    </style>
</head>
<body style="padding: 6px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style="padding-bottom: 5px; line-height: 28px;">
        <table>
            <tr>
                <td style="padding-left: 10px;">
                   
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                    部门受案号：
                    <% } %>
                </td>
                <td style="width: 300px">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 283px" />
                </td>
                <td style="padding-left: 10px;">                  
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案件名称：
                    <% } %>
                </td>
                <td>
                    <input id="txt_name" class="l-text" type="text" name="txt_name" />
                </td>
                <td style="padding-left: 10px; text-align: right;">
                    <%= ((VersionName)0).ToString() %>类别：
                </td>
                <td>
                    <input id="cmbajlb" class="l-text" type="text" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    受理日期：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="padding-left: 10px; text-align: right;">
                    承办人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                </td>
                <td style="text-align: right;">
                    关联状态：
                </td>
                <td>
                    <select id="sct_relevance" class="l-text" name="sct_relevance">
                        <option value="0">全部</option>
                        <option value="1">已关联</option>
                        <option value="2">未关联</option>
                    </select>
                </td>
                <td style="padding-left: 20px">
                    &nbsp;&nbsp;<input id="btn_search" type="button" class="l-button" value="搜 索" />
                </td>
            </tr>
        </table>
    </div>
    <%--表格--%>
    <div id="mainGrid" style="margin: 0; padding: 0">
    </div>
    <div style="display: none;">
        <!-- g data total ttt -->
    </div>
    <script type="text/javascript">

        var grid = null;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });

            grid = $("#mainGrid").ligerGrid({
                columns: [
                    { display: vn+'类别编码', name: 'ajlb_bm', width: 200, align: 'center' },
                    { display: vn+'类别名称', name: 'ajlb_mc', width: 350,  },
                    { display: vn+'数量', name: 'ajcount', width: 200, type: 'int', totalSummary: { type: 'sum'} },
                    { display: '已关联数量', name: 'regardcount', width: 200, type: 'int', totalSummary: { type: 'sum'} },
                    { display: '未关联数量', name: 'notregardcount', width: 200, type: 'int', totalSummary: { type: 'sum'} }
                ], pageSize: 50, dataAction: 'local', //服务器排序
                title: vn+'类别信息', showTitle: true,
                //dataAction: 'local', //服务器排序
                enabledSort: false,
                usePager: true, width: '100%', height: '100%',       //服务器分页
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                detail: { onShowDetail: loadAJxx },
                pageSizeOptions: [20, 50, 100, 500],
                parms: {
                    action: "GetAjlx",
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb:$("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }
            });
        });

        $(document).ready(function () {
            //点击搜索按钮
            $("#btn_search").click(function () {
                grid.loadServerData({
                    action: "GetAjlx",
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb: $("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                    timeend: $('#txt_time_end').val(),
                    page: 1, pagesize: grid.options.pageSize
                });
            });

        });

        function loadAJxx(row, detailPanel, callback) {
            var grid = document.createElement('div');
            $(detailPanel).append(grid);
            $(grid).css('margin', 10).css('margin-left', 30).ligerGrid({
                columns: [
                 { display: '关联状态', name: 'isregard', align: 'center', minWidth: 80,
                    render: function (item) {
                        if (parseInt(item.isregard) > 0) return '<span style="color:red;">已关联</span>';
                        return '未关联';
                    }
                },
                  <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    { display: '事项议题', name: 'ajmc', minWidth: 150 },
                    { display: '唯一编号', name: 'bmsah', minWidth: 280,  },
                    <% }
                       else
                       { %>
                    { display: '案件名称', name: 'ajmc', minWidth: 150 },
                    { display: '部门受案号', name: 'bmsah', minWidth: 280,  },
                    <% } %>
                
                
                { display: vn+ '类别名称', name: 'ajlb_mc', minWidth: 200 },
                { display: '承办单位', name: 'cbdw_mc', minWidth: 120 },
                { display: '卷数量', name: 'jcount', minWidth: 80, totalSummary: { type: 'sum'} },
                { display: '目录数量', name: 'mcount', minWidth: 80, totalSummary: { type: 'sum'} },
                { display: '文件数量', name: 'wcount', minWidth: 80, totalSummary: { type: 'sum'} },
                { display: '承办人', name: 'cbr', minWidth: 100 },
                { display: '当前阶段', name: 'dqjd' ,minWidth: 100},
                { display: '受理日期', name: 'slrq', minWidth: 150 },
                { display: vn+'状态', name: 'ajzt', minWidth: 70, render: function (item) {
                    if (parseInt(item.ajzt) == 0) return '受理';
                    else if (parseInt(item.ajzt) == 1) return '办理';
                    else if (parseInt(item.ajzt) == 2) return '已办';
                    else if (parseInt(item.ajzt) == 3) return '归档';
                    else return item.ajzt;
                }
                },
                { display: '到期日期', name: 'dqrq', minWidth: 150 },
                { display: '办结日期', name: 'bjrq', minWidth: 150 },
                { display: '完成日期', name: 'wcrq', minWidth: 150 },
                { display: '归档日期', name: 'gdrq', minWidth: 150 }
                ], pageSize: 50,
                //dataAction: 'local', //服务器排序
                enabledSort: false,
                usePager: true, width: 1135, height: 400,
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                detail: { onShowDetail: loadJxx },
                title: vn+'基本信息', 
                showTitle: false,
                showToggleColBtn: true,
                pageSizeOptions: [20, 50, 100, 500],
                parms: {
                    action: "GetAjxx",
                    ajlb_bm: row.ajlb_bm,
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb: $("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }
            });
        }

        var _search_name;
        var _search_bmsah;
        function loadJxx(row, detailPanel, callback) {
            if (row.jcount == null || row.jcount == 0) {
                return false;
            }
            _search_bmsah = row.bmsah
            var grid = document.createElement('div');
            $(detailPanel).append(grid);
            $(grid).css('margin', 10).css('margin-left', 30).ligerPanel({
                title: '目录文件页',
                showTitle: false,
                width: 1080,
                height : 400,
                url: 'MLReport.htm'
            });
            return;
            $(grid).css('margin', 5).ligerGrid({
                columns: [
                { display: '目录编号', name: 'mlbh', minWidth: 320 },
                  <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                
                    { display: '唯一编号', name: 'bmsah', width: 200 },
                    <% }
                       else
                       { %>
                  
                   { display: '部门受案号', name: 'bmsah', width: 200 },
                    <% } %>
//                { display: '部门受案号', name: 'bmsah', minWidth: 150, hide: 'none' },
                { display: '卷名称', name: 'mlxsmc', minWidth: 150 },
                { display: '创建时间', name: 'cjsj', minWidth: 150 },
                { display: '最后修改时间', name: 'zhxgsj', minWidth: 150 },
                { display: '所属分类', name: 'sslbmc', minWidth: 150 },
                { display: '目录数量', name: 'mcount', minWidth: 150 },
                { display: '文件数量', name: 'wcount', minWidth: 150 }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '97%',
                alternatingRow: false, tree: { columnName: 'mlbh' },fixedCellHeight: false,
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                parms: {
                    action: "GetJxx",
                    bmsah: row.bmsah,
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb: $("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }
            });
        }
        function loadMxx(row, detailPanel, callback) {
            if ((row.mcount == null || row.mcount == 0) && (row.wcount == null || row.wcount == 0)) {
                return false;
            }
            var grid = document.createElement('div');
            $(detailPanel).append(grid);
            $(grid).css('margin', 5).ligerGrid({
                columns: [
                { display: '目录编号', name: 'mlbh', minWidth: 150, hide: 'none' },
                { display: '创建时间', name: 'cjsj', minWidth: 150 },
                { display: '最后修改时间', name: 'zhxgsj', minWidth: 150 },
                { display: '目录（文件）名称', name: 'mlxsmc', minWidth: 150 },
                { display: '类型（目录/卷）', name: 'mllx', minWidth: 150, render: function (item) {
                    if (item.mllx == "2") {
                        return "目录";
                    }
                    else if (item.mllx == "3") {
                        return "文件";
                    }
                    else {
                        return "未知类型";
                    }
                }
                },
                { display: '所属分类', name: 'sslbmc', minWidth: 150 },
                { display: '文件数量', name: 'wcount', minWidth: 150 }
                ], pageSize: 50, dataAction: 'local', //服务器排序
                usePager: true, width: '100%', height: '100%',       //服务器分页
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                detail: { onShowDetail: loadWxx },
                title: '目录/文件信息', showTitle: true,
                pageSizeOptions: [20, 50, 100, 500],
                parms: {
                    action: "GetMlxx",
                    mlbh: row.mlbh,
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb: $("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }
            });
        }
        function loadWxx(row, detailPanel, callback) {
            if (row.wcount == null || row.wcount == 0) {
                return false;
            }
            if (row.mllx == '3') {//当上级目标为文件时 无下级
                return false;
            }
            var grid = document.createElement('div');
            $(detailPanel).append(grid);
            $(grid).css('margin', 5).ligerGrid({
                columns: [
                { display: '目录编号', name: 'mlbh', minWidth: 150, hide: 'none' },
                { display: '创建时间', name: 'cjsj', minWidth: 150 },
                { display: '最后修改时间', name: 'zhxgsj', minWidth: 150 },
                { display: '文件名称', name: 'mlxsmc', minWidth: 150 },
                { display: '类型（目录/卷）', name: 'mllx', minWidth: 150 , render: function (item) {
                        if(item.mllx == "2")
                        {
                            return "目录";
                        }
                        else if(item.mllx == "3")
                        {
                            return "文件";
                        }
                        else
                        {
                            return "未知类型";
                        }
                    }
                },
                { display: '所属分类', name: 'sslbmc', minWidth: 150 }
                ], pageSize: 50, dataAction: 'local', //服务器排序
                usePager: true, width: '100%', height: '100%',       //服务器分页
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                detail: { onShowDetail: loadAJxx },
                title: '文件信息', showTitle: true,
                pageSizeOptions: [20, 50, 100, 500],
                parms: {
                    action: "GetWjxx",
                    mlbh: row.mlbh,
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    cmbajlb: $("#cmbajlb").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }
            });
        }
    </script>
</body>
</html>
