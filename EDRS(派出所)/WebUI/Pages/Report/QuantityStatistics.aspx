<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuantityStatistics.aspx.cs"
    Inherits="WebUI.Pages.Report.QuantityStatistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>卷宗数量统计</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <%--<script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>--%>
    <script src="/LigerUI/lib/LigerUI/js/ligerui.all.js" type="text/javascript"></script>
    
    <%--图形统计--%>
    <script src="/Scripts/Charts/build/dist/echarts-all.js" type="text/javascript"></script>
    <style type="text/css">
       
            
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        
      .l-layout-left,.l-layout-right,.l-layout-center,.l-layout-top,.l-layout-bottom,.l-layout-centerbottom{position:absolute;border:1px solid #BED5F3; background:white; z-index:-1;overflow:hidden;}
      
              /*左右边框*/
      div .l-layout-left,div .l-layout-right{
         border: 1px solid #dde0e3;
         border-top: 4px solid #129bbc;
         border-radius: 10px;
    }
        .l-panel { 
            overflow: hidden;
            border: none;
            border-top: 0;
            border-radius: 0;
}
    </style>
</head>
<body style="padding: 6px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style="padding-bottom: 5px; display:none; line-height: 28px;">
        <table border="0">
            <tr>
                <td>
                    所属单位
                </td>
                <td>
                    <input id="txt_dwbm" class="l-text" type="text" name="txt_dwbm" />
                </td>
                <td style="padding-left: 10px;">
                    卷宗上传人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                </td>
                <td style="padding-left: 10px;">
                    创建时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td colspan="4">
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                    部门受案号：
                    <% } %>
                </td>
                <td colspan="3" style="width: 300px">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 346px" />
                </td>
                <td style="padding-left: 10px;">
                    卷宗名称：
                </td>
                <td colspan="6" style="width: 150px;">
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 283px" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="layoutMain">
        <div position="left" title="统计图形">
            <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
            <div id="graph" style="height: 500px; width: 97%;
    position: relative;z-index:100">
            </div>
        </div>
        <div position="right" title="统计列表">
            <div id="gridNumber" style="margin: 0; padding: 0;">
            </div>
        </div>
    </div>
    <%--表格--%>
    <div style="display: none;">
        <!-- g data total ttt -->
    </div>
    <script type="text/javascript">
        $(function () {
            $("#layoutMain").ligerLayout({
                leftWidth: "50%",
                rightWidth: '49%',
                allowLeftResize: false,
                allowRightResize: false,
                allowCenterBottomResize: false,
                heightDiff: -12
            });
        
        })

        var grid = null;
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


            grid = $("#gridNumber").ligerGrid({
                columns: [
                { display: '单位编码', name: 'DWBM', minWidth: 100 },
                { display: '单位名称', name: 'DWMC', minWidth: 150 },
                { display: '已用存储', name: 'WJDX', minWidth: 100,align: 'center', render: function (item) {
                    if (parseFloat(item.WJDX) > (1024 * 1024 * 1024)) {
                        return (item.WJDX / 1024 / 1024 / 1024).toFixed(2) + " T";
                    }
                    else if (parseFloat(item.WJDX) > (1024 * 1024)) {
                        return (item.WJDX / 1024 / 1024).toFixed(2) + " GB";
                    } else if (parseFloat(item.WJDX) > 1024) {
                        return (item.WJDX / 1024).toFixed(2) + " MB";
                    }
                    else return item.WJDX.toFixed(2) + " KB";
                }
                }
                 ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
            usePager: true, enabledSort: true, width: '99%', height: '99%',       //服务器分页
                url: '/Pages/Report/QuantityStatistics.aspx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: "GetDwSum"
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onReload: function () {
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
                }

            });



            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });



            //点击搜索按钮
            $("#btn_search").click(function () {

                MyGraph(1);

            });

        });
        function gridSetParm() {

        }
    </script>
    <script type="text/javascript">

        var myChart = null;

        $(function () {
            MyGraph(1);

        });
        function MyGraph(page) {
            if (myChart) {
                myChart.clear();
                myChart.dispose();
            }
            myChart = echarts.init(document.getElementById('graph'));
            $.post("/Pages/Report/QuantityStatistics.aspx", { t: "ListBind", type: "Graph",
                key: $("#txt_key").val(),
                casename: $("#txt_name").val(),
                dwbm: $("#txt_dwbm").val(),
                dutyman: $("#txt_dutyman").val(),
                timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                timeend: $('#txt_time_end').val(),
                page: 1, pagesize: 50
            },
                function (json) {

                    if (json.split("|").length == 1) {
                        $.ligerDialog.error(JSON.parse(json).v);
                        return false;
                    } else {

                        var ar = json.split("|");
                        var ar0 = ar[0].split(",");
                        ar0[ar0.length] = "剩余空间";

                        var ar1 = ar[1].split(",");
                        var arrSeries = new Array();

                        //显示比例
                        var formatter = "已用：" + (((parseFloat(ar1[0]) / ar1[1]) * 100).toFixed(2)) + "%<br/>剩余：" + ((parseFloat((ar1[1] - ar1[0]) / ar1[1]) * 100).toFixed(2))+"%";
                      
                        //已用
                        var count1 = parseFloat(ar1[0]);
                        if (count1 > (1024 * 1024 * 1024))
                            count1 = (count1 / 1024 / 1024 / 1024).toFixed(2) + " T";
                        else if (count1 > (1024 * 1024))
                            count1 = (count1 / 1024 / 1024).toFixed(2) + " GB";
                        else if (count1 > 1024)
                            count1 = (count1 / 1024).toFixed(2) + " MB";
                        else
                            count1 = count1.toFixed(2) + " KB";
                        //剩余
                        var count2 = parseFloat((ar1[1] - ar1[0]));
                        if (count2 > (1024 * 1024 * 1024))
                            count2 = (count2 / 1024 / 1024 / 1024).toFixed(2) + " T";
                        else if (count2 > (1024 * 1024))
                            count2 = (count2 / 1024 / 1024).toFixed(2) + " GB";
                        else if (count2 > 1024)
                            count2 = (count2 / 1024).toFixed(2) + " MB";
                        else
                            count2 = count2.toFixed(2) + " KB";


                        // for (var j = 0; j < ar1.length; j++) {
                        arrSeries[0] =
                                { 'name': '已用' + count1,
                                    'value': parseFloat(ar1[0] / 1024).toFixed(2)
                                };
                        arrSeries[1] =
                                { 'name': '剩余' + count2,
                                    'value': parseFloat((ar1[1] - ar1[0]) / 1024).toFixed(2)
                                };
                        var legData = ['已用' + count1, '剩余' + count2];
                        //  }
                        //  console.log(ar0);
                        //setTimeout(function () {

                        var count = parseFloat(ar1[1]);
                        if (count > (1024 * 1024 * 1024))
                            count = (count / 1024 / 1024 / 1024).toFixed(2) + " T";
                        else if (count > (1024 * 1024))
                            count = (count / 1024 / 1024).toFixed(2) + " GB";
                        else if (count > 1024)
                            count = (count / 1024).toFixed(2) + " MB";
                        else
                            count = count.toFixed(2) + " KB";

                        option(legData, ar0, arrSeries, count, formatter);
                        //}, 500);

                    }
                },
                    'text');
        }
        function option(legData, axisData, series, count, formatter) {
            var option = {
                title: {
                    text: '卷宗空间大小',
                    subtext: '存储大小' + count,
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "" + formatter
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: legData
                },
                series: [{
                    name: '存储情况',
                    type: 'pie',
                    radius: '60%',
                    center: ['50%', '60%'],
                    data: series
                }]
            };
            myChart.setOption(option);
        }
    </script>
</body>
</html>
