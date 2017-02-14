<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseUnitReport.aspx.cs"
    Inherits="WebUI.Pages.Report.CaseUnitReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>卷宗制作率统计</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <%--<script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>--%>
    <script src="/LigerUI/lib/LigerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
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
        .l-panel-header
        {
            display: none;
        }
        .l-text-combobox
        {
            height: 21px;
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
        
       div .l-layout-left,div .l-layout-right{
            border: 1px solid #dde0e3;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
    }
    .l-button {
        top: -4px;
        }
       div#searchbar {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background: white;
        }
        .l-panel { 
            overflow: hidden;
            border: none;
            border-top: 0;
            border-radius: 0;
}
    </style>
</head>
<body style="padding: 15px; overflow: hidden;">
    <div id="searchbar" style=" line-height: 28px;">
        <table>
            <tr>
                <td style="padding-left: 10px;">
                    单位名称：
                </td>
                <td>
                    <input id="txt_dwbm" type="text" name="txt_dwbm" />
                </td>
                <td style="padding-left: 10px;">
                    受理日期：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="padding-left: 10px;" id="sel_radio">
                    <input id="type1" type="radio" checked="checked" value="0" name="ajtype" /><label
                        for="type1">业务类型</label>&nbsp;&nbsp;
                    <input id="type2" type="radio" value="1" name="ajtype" /><label for="type2"><%= ((VersionName)0).ToString() %>类别</label>
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_deriveExcel" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </td>
            </tr>
            <tr style="display: none;">
                <td style="padding-left: 10px;">
                    业务类型：
                </td>
                <td>
                    <input id="txt_ywbm" type="text" name="txt_ywbm" />
                </td>
                <td style="padding-left: 10px;">
                    <%= ((VersionName)0).ToString() %>类别：
                </td>
                <td>
                    <input id="txt_ajlb" class="l-text" type="text" name="txt_ajlb" />
                </td>
            </tr>
        </table>
    </div>
    <div id="layoutMain">
        <div position="left" title="统计列表">
            <div id="mainGrid" style="margin: 0; padding: 0;">
            </div>
        </div>
        <div position="right" title="详细统计">
            <div id="gridDetail" style="margin: 0; padding: 0;">
            </div>
        </div>
    </div>
    <%--<div position="left" id="mainGrid" title="统计列表">
    </div>--%>
    <script type="text/javascript">

        var myChart = null;
        var grid = null;
        var gridDetail = null;
        var dwbm_tree;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            /*******************调整容器大小*******************/
            var layout = $("#layoutMain").ligerLayout({ leftWidth: '60%', rightWidth: '40%', onEndResize: function () { $(window).resize(); } });
            $(".l-layout-header-toggle", layout.left).click(function (f) {

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
                    $(".l-layout-left").width('60%');
                    $(".l-layout-right").width('40%');
                }
                layout.setLeftCollapse(false);
                $(window).resize();
                myChart.resize();
            });
            layout.rightCollapse.toggle.click(function () {
                var hid = $(".l-layout-left").is(":hidden");
                if (hid == false) {
                    $(".l-layout-left").width('60%');
                    $(".l-layout-right").width('40%');
                }
                layout.setRightCollapse(false);
                $(window).resize();
                myChart.resize();
            });
            /*******************调整容器大小 End*******************/

            $("#type1").click(function () {
                if (!grid)
                    return false;
                var data = grid.getSelectedRow();
                if (data && data.cbdw_bm) {
                    gridDetail.removeParm("t");
                    gridDetail.setParm("t", "GetDetail");
                    gridDetail.setParm("timebegin", $('#txt_time_begin').val());
                    gridDetail.setParm("timeend", $('#txt_time_end').val());
                    gridDetail.setParm("dwbm", data.cbdw_bm);
                    gridDetail.reload();
                }
            });
            $("#type2").click(function () {
                if (!grid)
                    return false;
                var data = grid.getSelectedRow();
                if (data && data.cbdw_bm) {

                    gridDetail.removeParm("t");
                    gridDetail.setParm("t", "GetDetailLb");
                    gridDetail.setParm("timebegin", $('#txt_time_begin').val());
                    gridDetail.setParm("timeend", $('#txt_time_end').val());
                    gridDetail.setParm("dwbm", data.cbdw_bm);
                    //gridDetail.loadData();
                    gridDetail.reload();
                }
            });


            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            $('#btn_deriveExcel').ligerButton({
                width: 80,
                text: '导出Excel',
                icon: '../../images/dcExcel.png'
            });
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

            //业务类型
            $("#txt_ywbm").ligerComboBox({
                url: '/Pages/Report/DwMakeStatisticsQuery.aspx',
                parms: { t: "GetYwbm" },
                valueFieldID: 'ywbm_hidd',
                onSelected: function (newvalue) {
                    //案件类别
                    $("#txt_ajlb").ligerComboBox({
                        url: '/Handler/ZZJG/DZJZ_Report.ashx',
                        parms: { action: "GetAjlxList", ywbm: newvalue },
                        valueFieldID: 'ajlbbm_hidd',
                        selectBoxWidth: 400,
                        selectBoxHeight: 300,
                        autocomplete: true,
                        highLight: true
                    });
                }
            });

            //案件类别
            $("#txt_ajlb").ligerComboBox({
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                parms: { action: "GetAjlxList", ywbm: "" },
                valueFieldID: 'ajlbbm_hidd',
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });
            var betime = '<%=SetBeTime %>';
            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center', initValue: betime + '-12-26', onChangeDate: function (value) {
                var d1 = new Date(value.replace(/\-/g, "\/"));
                var d2 = new Date($("#txt_time_end").val().replace(/\-/g, "\/"));
                if (d1 >= d2) {
                    $("#txt_time_end").val("");
                }
            }
            });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center', onChangeDate: function (value) {
                var d1 = new Date($("#txt_time_begin").val().replace(/\-/g, "\/"));
                var d2 = new Date(value.replace(/\-/g, "\/"));
                if (d1 > d2) {
                    $("#txt_time_end").val("");
                    $.ligerDialog.warn('受理开始日期不能大于结束日期');
                }
            }
            });


            $("#btn_search").click(function () {
                if (grid) {
                    gridSetParm();
                    grid.reload();
                }
                else {
                    SearchData();
                }
            });





            //导出
            $("#btn_deriveExcel").click(function () {
                $.ligerDialog.waitting('正在导出,请稍候...');
                $.ajax({
                    type: "POST",
                    url: "/Pages/Report/CaseUnitReport.aspx",
                    data: {
                        t: "DeriveData",
                        timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                        timeend: $('#txt_time_end').val(),
                        dwbm: dwbm_tree.getValue(),
                        page: grid.options.page, pagesize: grid.options.pageSize
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

            $("#pageloading").hide();
        });
        function SearchData() {
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '承办单位', name: 'cbdw_mc', minWidth: 80, width: 150
                },
                { display: vn + '受理数', name: 'ajnum', minWidth: 80, width: 100, totalSummary: { type: 'sum'} },
                { display: '已制作数', name: 'zznum', minWidth: 80, width: 100, totalSummary: { type: 'sum' }, render: function (item) {
                    if (item.zznum)
                        return item.zznum;
                    else
                        return '0';
                }
                },
                { display: '未制作数', name: 'wznum', minWidth: 80, width: 100, totalSummary: { type: 'sum' }, render: function (item) {
                    if (item.wznum)
                        return item.wznum;
                    return '0';
                }
                },
                { display: '制作率', name: 'zzlnum', minWidth: 80, width: 100
                    //, render: function (item) {
                    //                    if (item.ajnum == '')
                    //                        return '0%';
                    //                    else
                    //                        return (parseFloat((item.zznum / item.ajnum).toFixed(2)) * 1000 / 10) + '%';
                    //  }
                }
                ],
                pageSize: 50,
                title: '单位卷宗统计',
                pageSizeOptions: [10, 20, 50, 100, 500],
                enabledSort: true, rownumbers: true,
                usePager: true, width: '100%', height: '100%', heightDiff: 20,
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                parms: { action: "GetAJReportByUnit",
                    unit: dwbm_tree.getValue(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val(),
                    caseajlb: $("#ajlbbm_hidd").val(),
                    ywbm: $("#ywbm_hidd").val()
                }
                , onReload: function () {
                    gridSetParm();
                },
                onToFirst: function (element) {
                    gridSetParm();
                },
                onToPrev: function (element) {
                    gridSetParm();
                },
                onToNext: function (element) {
                    gridSetParm();
                },
                onToLast: function (element) {
                    gridSetParm();
                },
                onSelectRow: function (rowdata, rowid, rowobj) {
                    //alert(JSON.stringify(rowdata));
                    //alert(rowdata.CBDW_BM);
                    $("#sel_radio input:radio").each(function () {
                        if (this.checked && $(this).val() == "0")
                            typeName = "GetDetail";
                        else if (this.checked && $(this).val() == "1")
                            typeName = "GetDetailLb";
                    });
                    //                    if (gridDetail) {
                    //                        gridDetail.setParm("dwbm", rowdata.CBDW_BM);
                    //                        gridDetail.setParm("t", typeName);
                    //                        gridDetail.reload();
                    //                    }
                    //                    else {

                    GetGridDetail(rowdata.cbdw_bm);
                    return false;
                    //}
                }
            });

        }

        //绑定详细
        function GetGridDetail(dwbm) {
            $.ligerDialog.waitting('正在查询详细信息,请稍候...');
            var count = 0;
            var typeName = "";
            $("#sel_radio input:radio").each(function () {
                if (this.checked && $(this).val() == "0")
                    typeName = "GetDetail";
                else if (this.checked && $(this).val() == "1")
                    typeName = "GetDetailLb";
            });

            gridDetail = $("#gridDetail").ligerGrid({
                columns: [
                { display: (typeName == "GetDetail" ? '业务名称' : '类别名称'), name: 'YWMC', minWidth: 80, width: 150, render: function (item) {
                    if (item.YWMC) {
                        return item.YWMC;
                    }
                    return item.AJLBMC;
                }
                },
                //{ display: '案件受理数', name: 'AJNUM', type: "int", minWidth: 80, width: 100, align: 'center', totalSummary: { type: 'sum'} },
                {display: '已制作数', name: 'ZZNUM', type: "int", minWidth: 80, width: 100, align: 'center', totalSummary: { type: 'sum'} }
                //                { display: '未制作数', name: 'WZNUM', type: "int", minWidth: 80, width: 100, align: 'center', totalSummary: { type: 'sum' }, render: function (item) {
                //                    if (item.WZNUM)
                //                        return item.WZNUM;
                //                    return '0';
                //                } 
                //                },
                //                { display: '制作率', name: 'ZZLNUM', type: "int", minWidth: 80, width: 100, align: 'center',  render: function (item) {

                //                    if (item.AJNUM == '')
                //                        return '0%';
                //                    else
                //                        return ((item.ZZNUM / item.AJNUM).toFixed(2) * 100) + '%';
                //                } 
                // }
                //                                { display: '卷数', name: 'JNUM', type: "int", minWidth: 80, align: 'center', totalSummary: { type: 'sum'} },
                //                                { display: '文件数', name: 'WJNUM', type: "int", minWidth: 80, align: 'center', totalSummary: { type: 'sum'} },
                //                                { display: '文件页数', name: 'WJYNUM', type: "int", minWidth: 80, align: 'center', totalSummary: { type: 'sum'} }
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                usePager: false, enabledSort: true, width: '99%', height: '100%', heightDiff: -4,        //服务器分页
                url: "/Pages/Report/CaseUnitReport.aspx",
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: typeName,
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val(),
                    dwbm: dwbm
                }, onSuccess: function (data) {

                    $(".l-dialog").remove();
                    if (data.t && count == 0) {
                        $.ligerDialog.error(data.v);
                        count++;
                    }
                    setTimeout(function () { $.ligerDialog.closeWaitting(); }, 200);
                }
            });
        }


        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("casename", $("#txt_name").val());
            grid.setParm("dutyman", $("#txt_dutyman").val());
            grid.setParm("ywbm", $("#ywbm_hidd").val());
            grid.setParm("timebegin", $('#txt_time_begin').val());
            grid.setParm("timeend", $('#txt_time_end').val());
            grid.setParm("dwbm", dwbm_tree.getValue());
            grid.setParm("ywbm", $("#ywbm_hidd").val());
        }
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
