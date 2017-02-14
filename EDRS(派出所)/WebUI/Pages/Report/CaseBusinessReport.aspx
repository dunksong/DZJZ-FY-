<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseBusinessReport.aspx.cs" Inherits="WebUI.Pages.Report.CaseBusinessReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <%--<script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>--%>
    <script src="/LigerUI/lib/LigerUI/js/ligerui.all.js" type="text/javascript"></script>
    <style type="text/css">
    .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        .l-panel-header
        {
            display:none;
        }
    </style>
</head>
<body>
    
    <div id="searchbar" style="padding: 5px; line-height: 28px;">
        <table>
            <tr>
                <td style="padding-left: 10px;">
                    单位名称：
                </td>
                <td style="width: 150px;">
                    <input id="txt_unit" class="l-text" type="text" name="txt_unit" style="width: 200px" />
                </td>
                <td style="padding-left: 10px;">
                   已制作数量范围：
                </td>
                <td><input id="count_start" type="text" name="count_start" style="width: 50px" /></td>
                <td>&nbsp;&nbsp;-&nbsp;&nbsp;</td>
                <td><input id="count_end" type="text" name="count_end" style="width: 50px" /></td>
                <td style="padding-left: 10px;">
                   创建日期：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td>
                    &nbsp;&nbsp;<input id="btn_search" type="button" class="l-button" value="搜 索" />
                </td>
            </tr>
                
        </table>
    </div>
    
    <div id="layoutMain">
        <div   position="left" id="mainGrid" title="统计列表">
        </div>
        <div position="right" title="统计图形" id="graph">
            <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
        </div>
    </div>
    <script type="text/javascript">

        var myChart = null;
        var myGrid = null;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {
            /*******************调整容器大小*******************/
            var layout = $("#layoutMain").ligerLayout({
                leftWidth: '49%', rightWidth: '50%', space:2,
                allowLeftResize :false,allowRightResize :false,
            onEndResize: function () { $(window).resize(); } });
            $(".l-layout-header-toggle", layout.left).click(function (f) {
                console.log(layout.right);
                $(".l-layout-right").width(layout.width - (29 + layout.options.space));
                var hid = $(".l-layout-right").is(":hidden");
                if (hid == true) {
                    layout.setRightCollapse(false);
                }
                layout.setLeftCollapse(true);
                $(window).resize();
                myChart.resize();
            });
            $(".l-layout-header-toggle", layout.right).click(function () {
                $(".l-layout-left").width(layout.width - (29 + layout.options.space));
                var hid = $(".l-layout-left").is(":hidden");
                if (hid == true) {
                    layout.setLeftCollapse(false);
                }
                layout.setRightCollapse(true);
                $(window).resize();
                myChart.resize();
            });
            layout.leftCollapse.toggle.click(function () {
                var hid = $(".l-layout-right").is(":hidden");
                if (hid == false) {
                    $(".l-layout-left").width('50%');
                    $(".l-layout-right").width('50%');
                }
                layout.setLeftCollapse(false);
                $(window).resize();
                myChart.resize();
            });
            layout.rightCollapse.toggle.click(function () {
                var hid = $(".l-layout-left").is(":hidden");
                if (hid == false) {
                    $(".l-layout-left").width('50%');
                    $(".l-layout-right").width('50%');
                }
                layout.setRightCollapse(false);
                $(window).resize();
                //$("#graph").ligerPanel({ width: 668, height: 530 });
                myChart.resize();
            });
            /*******************调整容器大小 End*******************/

            $("#count_start").ligerSpinner({ type: 'int', isNegative: false, width: 50 });
            $("#count_end").ligerSpinner({ type: 'int', isNegative: false, width: 50 });
            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });

            SearchData();
            $("#btn_search").click(function () {
                if (myGrid) {
                    myGrid.loadServerData({
                        action: "BusinessReport",
                        timebegin: $("#txt_time_begin").val(),
                        timeend: $("#txt_time_end").val(),
                        count_start: $("#count_start").val(),
                        count_end: $("#count_end").val(),
                        unit: $("#txt_unit").val(),
                        unit: dwbm_tree.getValue(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
                else {
                    SearchData();
                }
            });
            $("#pageloading").hide();
        });
        function SearchData() {
            myGrid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '业务类型', name: 'ywmc', width: 100 },
                { display: vn+'总数', name: 'ajcount', width: 70, totalSummary: { type: 'sum'} },
                { display: '已制作数', name: 'isregard', width: 70, totalSummary: { type: 'sum'} },
                { display: '卷数', name: 'jcount', width: 70, totalSummary: { type: 'sum'} },
                { display: '目录数', name: 'mcount', width: 70, totalSummary: { type: 'sum'} },
                { display: '文件数', name: 'wcount', width: 70, totalSummary: { type: 'sum'} },
                { display: '文件页数', name: 'pagecount', width: 70, totalSummary: { type: 'sum'} }
                ],
                pageSize: 100,showTitle:false,
                pageSizeOptions: [20, 50, 100, 500],
                enabledSort: false, rownumbers: true,
                usePager: true, width: '100%', height: '100%', heightDiff: '-10',
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                parms: { action: "BusinessReport",
                    unit: $("#txt_unit").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val(),
                    count_start: $("#count_start").val(),
                    count_end: $("#count_end").val()
                }
            });
            var where = "timebegin=" + $("#txt_time_begin").val();
            where += "&timeend=" + $("#txt_time_end").val();
            where += "&unit=" + encodeURI($("#txt_unit").val());
            where += "&count_start=" + $("#count_start").val();
            where += "&count_end=" + $("#count_end").val();
            $("#graph iframe").remove();
            $("#graph").ligerPanel({ title: '', showTitle: false, width: '100%', height: 560, showToggle: false, url: 'ReportCenter.aspx?action=business&type=bar&' + where + '&pageSize=' + myGrid.options.pageSize + '&page=' + myGrid.options.newPage + '' });
        }
    </script>

    
</body>
</html>

