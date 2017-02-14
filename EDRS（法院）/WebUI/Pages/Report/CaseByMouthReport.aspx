<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseByMouthReport.aspx.cs"
    Inherits="WebUI.Pages.Report.CaseByMouthReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css">
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/LigerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
    <style type="text/css">
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        .l-box-select-inner
        {
            max-height: 300px;
            min-height: 300px;
        }
        .l-box-select-inner .l-tree
        {
            min-width: 400px !important;
        }
        .l-tree-icon
        {
            background: url('/images/icons/3.png') no-repeat !important;
            background-position: center center !important;
        }
        
        div#searchbar {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
        }
        .l-panel-header {
            background: none;
           
        }
        .l-panel 
        {
            overflow: hidden;
            border: 1px solid #ccc;
            border-radius: 10px;
        }
    </style>
    <script type="text/javascript">
        var thisYear = '<%=ThisYear %>';
        var dwbm_tree;

        $(function () {
           

            var tree_node;
            var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { text: '全选/反选', click: function itemclick() {
                var sNode = tree_node;
                if (sNode == null) {
                    $.messager.alert('提示', '未选择任何节点，操作失败！', 'error');
                    return;
                }

                $(".l-expandable-close", sNode.target).click();
                if ($(".l-checkbox-checked", sNode.target).length == 0) {//未选中
                    $(".l-checkbox", sNode).removeClass("l-checkbox-checked").addClass("l-checkbox-unchecked");
                    $(".l-checkbox-unchecked", sNode.target).click();
                }
                else {
                    $(".l-checkbox", sNode).removeClass("l-checkbox-unchecked").addClass("l-checkbox-checked");
                    $(".l-checkbox-checked", sNode.target).click();
                }
            }
            }
            ]
            });

            //单位编码
            dwbm_tree = $("#txt_dwbm").unitJuris({ width: 130, checkbox: true });

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });
            $('#btn_deriveExcel').ligerButton({
                width: 80,
                text: '导出Excel',
                icon: '../../images/NewAdd/dcExcel.png'
            });

            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center', format: 'yyyy-MM-dd', initValue: thisYear + '-01-01', onChangeDate: function (value) {
                var d1 = new Date(value.replace(/\-/g, "\/"));
                var d2 = new Date($("#txt_time_end").val().replace(/\-/g, "\/"));
                if (d1 >= d2) {
                    $("#txt_time_end").val("");
                }
            }
            });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center', format: 'yyyy-MM-dd', initValue: thisYear + '-12-31', onChangeDate: function (value) {
                var d1 = new Date($("#txt_time_begin").val().replace(/\-/g, "\/"));
                var d2 = new Date(value.replace(/\-/g, "\/"));
                if (d1 > d2) {
                    $("#txt_time_end").val("");
                    $.ligerDialog.warn('统计开始日期不能大于结束日期');
                }
            }
            });





            SearchData();
            $("#btn_search").click(function () {
                $("input:radio").each(function () {
                    if (this.checked) {
                        SearchData(this.value, "caseByMouth");
                    }
                });
            });

            //导出
            $("#btn_deriveExcel").click(function () {
                $.ligerDialog.waitting('正在导出,请稍候...');
                $.ajax({
                    type: "POST",
                    url: "/Pages/Report/CaseByMouthReport.aspx",
                    data: {
                        t: "DeriveData",
                        timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                        timeend: $('#txt_time_end').val(),
                        dwbm: dwbm_tree.getValue(),
                        page: listGrid.options.page, pagesize: listGrid.options.pageSize
                    },
                    dataType: 'json',
                    timeout: 10000,
                    cache: false,
                    beforeSend: function () {
                    },
                    error: function (xhr) {
                        $.ligerDialog.closeWaitting();
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t == "win") {
                            location.href = "/download.aspx?t=getmodel&fileid=" + data.v;
                        } else {
                            $.ligerDialog.error(data.v);
                        }
                        $.ligerDialog.closeWaitting();
                    }
                });
            });

            $('input:radio').click(function () {
                SearchData(this.value, "caseByMouth");
            });
        });
        var picGrid;
        var listGrid;
        var vn = '<%= ((VersionName)0).ToString() %>';
        function SearchData(type, catvalue) {
            $("#lsitGrid").hide();
            $("#mainGrid").hide();
            if (type == "pic") {
                $("#mainGrid iframe").remove();
                var where = "timebegin=" + $("#txt_time_begin").val();
                where += "&timeend=" + $("#txt_time_end").val();
                where += "&unit=" + encodeURI(dwbm_tree.getValue());
                picGrid = $("#mainGrid").ligerPanel({ title: '卷宗月统计图', width: '100%', showToggle: false, height: 590, url: 'ReportCenter.aspx?action=caseByMouth&type=bar&' + where + '' });
                $("#mainGrid").show();
            }
            else {

                $("#lsitGrid").show();
                listGrid = $("#lsitGrid").ligerGrid({
                    columns: [
                { display: '月份', name: 'mm', minWidth: 70, render: function (item) {
                    return (item.mm.replace("-", "年"));
                }
                },
                { display: vn + '数', name: 'ajcount', minWidth: 70, totalSummary: { type: 'sum'} },
                { display: vn + '制作数', name: 'zzcount', minWidth: 70, totalSummary: { type: 'sum'} },
                { display: '卷数', name: 'jcount', minWidth: 70, totalSummary: { type: 'sum'} },
                { display: '文件数', name: 'wcount', minWidth: 70, totalSummary: { type: 'sum'} },
                { display: '文件页数', name: 'pagecount', minWidth: 70, totalSummary: { type: 'sum'} },
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                    usePager: false, enabledSort: true, width: '100%', height: '100%', heightDiff: -5,       //服务器分页
                    url: '/Handler/ZZJG/DZJZ_Report.ashx',
                    pageSizeOptions: [20, 50, 100, 500],
                    parms: {
                        action: catvalue,
                        timebegin: $("#txt_time_begin").val(),
                        timeend: $("#txt_time_end").val(),
                        unit: encodeURI(dwbm_tree.getValue())
                    },
                    onSuccess: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        }
                    }
                });
            }
        }
        
         
    </script>
</head>
<body style="padding: 15px 15px 0px 15px;overflow: hidden;">
    <form id="form1" runat="server">
    <div id="searchbar" >
        <table>
            <tr>
                <td style="padding-left: 10px;">
                    单位名称：
                </td>
                <td colspan="6" style="width: 150px;">
                    <input id="txt_dwbm" type="text" name="txt_dwbm" />
                </td>
                <td style="padding-left: 10px;">
                    统计时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" runat="server" />&nbsp;&nbsp;-&nbsp;&nbsp;
                    <input id="txt_time_end" type="text" name="txt_time_end" runat="server" />
                </td>
                <td>
                    &nbsp;&nbsp;
                    <input type="radio" value="list" name="showType" checked="checked" />列表
                    <input type="radio" value="pic" name="showType" />图表
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search">
                    </div>
                    <div id="btn_deriveExcel" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--表格--%>
    <div id="mainGrid">
    </div>
    <div id="lsitGrid">
    </div>
    </form>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
