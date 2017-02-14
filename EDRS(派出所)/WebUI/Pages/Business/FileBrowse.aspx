<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileBrowse.aspx.cs" Inherits="WebUI.Pages.Business.FileBrowse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>卷宗浏览</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/jquery.PrintArea.js" type="text/javascript"></script>
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
        .l-text, .l-textarea
        {
            width: 350px;
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
        .l-table
        {
            width: 100%;
        }
        .l-table tr td
        {
            padding: 5px 2px;
        }
        .l-text
        {
            width: 150px;
        }
        
        
        
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        
        #top_div
        {
            overflow-x: auto;
        }
        #top_div table
        {
            width: auto;
        }
        #top_div table tr td
        {
            white-space: nowrap;
        }
    </style>
    <style type="text/css">
        .print_table
        {
            width: 100%;
            font-family: 宋体;
        }
        .print_table td
        {
            line-height: 40px;
        }
        /*   按钮  */
        .l-button {
            color: white;
            top:-2px;
        }
        div#btn_search {
            background: #ed6d4a;
        }
        div#btn_xx {
            background: #4c9dc1;
        }
        div#top_div {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background: white;
        }
        .l-panel-bwarp {
            background: white;
        }
        .l-layout-left,.l-layout-center 
        {
            background: white;
            border: 1px solid #ccc;
                border-top: 4px solid #129bbc;
            border-radius: 10px;
        }
    </style>
</head>
<body style="padding: 15px;overflow: hidden;background: #eef2f5;">
    <%--搜索div--%>
    <div id="top_div" style=" line-height: 28px; overflow-x: auto;">
        <table class="searchbartab" border="0" style="height: 70px;">
            <tr>
                <td style="width: 80px; text-align: right;">
                    制作状态：
                </td>
                <td style="">
                    <select id="sct_relevance" class="l-text" name="sct_relevance" style="width: 162px">
                        <option value="-1">全部</option>
                        <option value="2">待审核</option>
                        <option value="3">审核不通过</option>
                        <option value="4">审核通过</option>
                        <option value="6">报送失败</option>
                        <%-- <option value="5">已报送</option>--%>
                    </select>
                </td>
                <td style="width: 80px; text-align: right; padding-left: 10px;">
                    案件名称：
                </td>
                <td>
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right; padding-left: 10px;">
                    案件类别：
                </td>
                <td>
                    <input id="txt_ajlb" class="l-text" type="text" name="txt_ajlb" style="width: 160px;" />
                </td>
                <td style="padding-left: 10px; width: 80px; text-align: right;">
                    立案人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    立案时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" style="width: 80px;" />
                </td>
            </tr>
            <tr>
                
                <td style="width: 80px; text-align: right;">
                    案件编号：
                </td>
                <td style="">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    文书编号：
                </td>
                <td style="">
                    <input id="txt_wsbh" class="l-text" type="text" name="txt_wsbh" style="width: 160px;" />
                </td>
                <td style="padding-left: 10px; width: 80px; text-align: right;">
                    扫描人：
                </td>
                <td>
                    <input id="txt_zzr" class="l-text" type="text" name="txt_zzr" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    扫描时间：
                </td>
                <td>
                    <input id="txt_zzsj_begin" type="text" name="txt_zzsj_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_zzsj_end" type="text" name="txt_zzsj_end" style="width: 80px;" />
                </td>
                <td colspan="4">
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_xx" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                    <div id="btn_shyes" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                    <div id="btn_shno" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                    <div id="btn_sdbs" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="top_div_to" style="display: none;border: 1px solid #ccc; border-top: 4px solid #129bbc;background: white;padding: 10px;margin-bottom: 10px;border-radius: 10px;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        <div id="btn_shyesTo" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
        </div>
        <div id="btn_shnoTo" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
        <span id="time_span"></span>
    </div>
    <div id="layout" style="width: 100%; margin: 0; padding: 0; display: none;">
   
        <div id="leftFrm" position="left" title='<%=((VersionName)0).ToString() %>目录'>
            <ul id="path_tree">
                <li style="padding: 5px 5px;">请先选择<%=((VersionName)0).ToString() %>，根据<%=((VersionName)0).ToString() %>加载对应目录！</li>
            </ul>
        </div>
        <div id="centterFrm" position="center" title="">
            <div id="pdfShow" style="margin: 0px; border: 0px; padding: 0px">
            </div>
        </div>
    </div>
    <div id="login_div" style="padding: 0px; display: block;">
        <div id="mainGrid" style="margin: 0px; padding: 0px">
        </div>
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 20px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        标题：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input id="txt_bt" type="text" name="txt_bt" style="width: 283px;" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <%--保存设置--%>
    <div id="save_div" style="padding: 10px; display: none;">
        <div style="padding: 0">
            <form id="save_form" method="post">
            <table class="l-table">
                <tr>
                    <td>
                        审核批注：
                    </td>
                    <td>
                        <textarea id="txt_pz" name="txt_pz" cols="100" rows="2" class="l-textarea"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    

    <script type="text/javascript">

        //leftFrm   keydown
        $(document).unbind("keydown");
        $(document).bind("keydown", function (event) {          
            //下40 上38
            if (event.keyCode == 40) {
                //下
                //path_tree
                var td = path_tree.getSelected();
                if (td && td.target) {
                    var n_li = $(td.target).next();
                    $(n_li).click();
                }               
            }
            else if (event.keyCode == 38) {
                //上
                var td = path_tree.getSelected();
                if (td && td.target) {
                    var n_li = $(td.target).prev();
                    $(n_li).click();
                }               
            }
        });
    </script>

    <script type="text/javascript">

        var grid = null;
        var select = null;
        var path_tree;
        var yjdata;
        var time;
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        $(function () {
            var layout = $("#layout").ligerLayout({ leftWidth: 200, space: 4,centerWidth:300, height: '100%', heightDiff: -40, onEndResize: function () {
                resizeLayout();
            }, fn: function () {
                resizeLayout()
            }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();

            });
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            $('#btn_xx').ligerButton({
                text: '查看详细',
                icon: '../../images/ckxx.png',
                width:80
            });

            $("#btn_back").ligerButton({
                text: '返 回',
                icon: '../../images/fh.png'
            });


            function resizeLayout() {

                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
                $(".l-layout-center").css("width", width - 30 + "px");
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = $(window).height() - 40;

                $("#layout").height(h );
                $("#leftFrm").height(h-26);
                $("#leftFrm").parent(".l-layout-left").height(h - 45);
                $("#leftFrm").parent(".l-layout-left").next(".l-layout-center").height(h - 45);
                $("#leftFrm").parent(".l-layout-left").next(".l-layout-center").children(".l-layout-content").height(h -45);
                
                $("#pdfFrm").height(h - 20);

            }

            var betime = '<%=SetBeTime %>';
            $("#txt_time_begin").ligerDateEditor({ width: 88, labelWidth: 80, labelAlign: 'center', initValue: betime + '-12-26', onChangeDate: function (value) {
                var d1 = new Date(value.replace(/\-/g, "\/"));
                var d2 = new Date($("#txt_time_end").val().replace(/\-/g, "\/"));
                if (d1 >= d2) {
                    $("#txt_time_end").val("");
                }
            }
            });
            $("#txt_time_end").ligerDateEditor({ width: 88, labelWidth: 80, labelAlign: 'center', onChangeDate: function (value) {
                var d1 = new Date($("#txt_time_begin").val().replace(/\-/g, "\/"));
                var d2 = new Date(value.replace(/\-/g, "\/"));
                if (d1 > d2) {
                    $("#txt_time_end").val("");
                    $.ligerDialog.warn('立案开始时间不能大于结束时间');
                }
            }
            });

            //制作时间
            $("#txt_zzsj_begin").ligerDateEditor({ width: 88, labelWidth: 80, labelAlign: 'center', onChangeDate: function (value) {
                var d1 = new Date(value.replace(/\-/g, "\/"));
                var d2 = new Date($("#txt_zzsj_end").val().replace(/\-/g, "\/"));
                if (d1 >= d2) {
                    $("#txt_zzsj_end").val("");
                }
            }
            });
            $("#txt_zzsj_end").ligerDateEditor({ width: 88, labelWidth: 80, labelAlign: 'center', onChangeDate: function (value) {
                var d1 = new Date($("#txt_zzsj_begin").val().replace(/\-/g, "\/"));
                var d2 = new Date(value.replace(/\-/g, "\/"));
                if (d1 > d2) {
                    $("#txt_zzsj_end").val("");
                    $.ligerDialog.warn('制作开始时间不能大于结束时间');
                }
            }
            });


            grid = $("#mainGrid").ligerGrid({
                //0：未制作，1：制作中，2：已上传，3：审核不通过，4：审核通过，5：已报送
                columns: [
                { display: '制作状态', name: 'ZZZT', minWidth: 150, render: function (item) {
                    if (item.ZZZT == 6) {
                        return '<span style="color:red;">报送失败</span>';
                    }
                    else if (item.ZZZT == 5) {
                        return '已报送';
                    }
                    else if (item.ZZZT == 4) {
                        return '审核通过';
                    }
                    else if (item.ZZZT == 3) {
                        return '<span style="color:red;">审核不通过</span>';
                    }
                    else if (item.ZZZT == 2) {
                        return '<span style="color:blue;">待审核</span>';
                    }
                    else
                        return '<span style="color:red;"></span>';
                }
                },
                { display: '案件编号', name: 'AJBH', minWidth: 200 },
                { display: '文书编号', name: 'WSBH', minWidth: 250 },
                { display: '文书名称', name: 'WSMC', minWidth: 250 },
                { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '案件类别名称', name: 'AJLB_MC', minWidth: 150 },
                { display: '犯罪嫌疑人姓名', name: 'XYR', minWidth: 150 },
                { display: '立案时间', name: 'SLRQ', minWidth: 150 },
                { display: '立案单位', name: 'CBDW_MC', minWidth: 150 },
                { display: '立案人', name: 'CBR', minWidth: 150 },
                { display: '审核人', name: 'JZSHR', minWidth: 150 },
                { display: '审核时间', name: 'JZSHSJ', minWidth: 150 },
                { display: '扫描人', name: 'JZSCRXM', width: 150 },
                { display: '扫描时间', name: 'CJSJ', width: 150 },
                { display: '批注', name: 'JZPZ', minWidth: 150 },
                { display: '', name: 'JZBH', width: 1, hide: true },
                { display: '', name: 'BMSAH', width: 1, hide: true }
                ], rownumbers: true, checkbox: false, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',
                url: '/Pages/Business/FileBrowse.aspx?page=1',
                alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -5,
                parms: { t: "ListBind",
                    //  key: $("#txt_key").val(),
                    zzzt: $("#sct_relevance").val(),
                    ajmc: $("#txt_name").val(),
                    ajlb: $("#ajlbbm_hidd").val(),
                    lar: $("#txt_dutyman").val(),
                    ajbh: $("#txt_key").val(),
                    wsbh: $("#txt_wsbh").val(),
                    zzr: $("#txt_zzr").val(),
                    timebegin: $("#txt_time_begin").val(),
                    timeend: $("#txt_time_end").val(),
                    zztimebegin: $("#txt_zzsj_begin").val(),
                    zztimeend: $("#txt_zzsj_end").val()
                }
                , onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
            //案件类别
            $("#txt_ajlb").ligerComboBox({
                url: '/Handler/ZZJG/DZJZ_Report.ashx',
                parms: { action: "GetAjlxList" },
                valueFieldID: 'ajlbbm_hidd',
                width: 160,
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });

            //返回
            $("#btn_back").click(function () {
                $("#top_div_to").hide();
                $("#layout").hide();
                $("#login_div").show();
                $("#top_div").show();
                grid.reload();

            });
            //详细
            $("#btn_xx").click(function () {
                $("#pdfFrm").remove();
                display();
            });
        });
        function submitForm() {
            var row = grid.getSelectedRow();
            var pz = $("#txt_pz").val();
            if (!pz || pz == "" || pz.length == 0) {
                $.ligerDialog.warn('必须填写审核批注');
                return false;
            }
            if (row && grid.getCheckedRows().length > 1) {
                $.ligerDialog.warn('一次只能审核不通过一条信息');
                return false;
            }
            var jdata = $('#save_form').serializeArray();
            jdata[jdata.length] = { name: "t", value: "Add" };


            if (jdata.length > 1 && row) {
                jdata[jdata.length] = { name: "type", value: "3" };
                jdata[jdata.length] = { name: "bmsah", value: row.JZBH };
                $.ajax({
                    type: "POST",
                    url: "FileBrowse.aspx",
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
                            $("#save_form")[0].reset();
                            $.ligerDialog.hide();
                            grid.reload();
                            $.ligerDialog.success(data.v);
                        } else {
                            $.ligerDialog.error(data.v);
                        }
                    }
                });
            }
        }


        ///阅卷
        function display() {
            //            var jdata = $('#login_form').serializeArray();
            //            jdata[jdata.length] = { name: "t", value: "ReadLogin" };
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $("#login_div").hide();
                $("#top_div").hide();
                $("#top_div_to").show();
                $("#layout").show();

                $("#span_ajname").html("当前卷宗：" + cksld.AJMC + "&nbsp;【" + cksld.AJBH + "】");
                JZMLTree(cksld.BMSAH, "");

            }
        }
        //绑定文件目录
        function JZMLTree(bmsah, yjxh) {

            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/Business/FileBrowse.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh },
                    isExpand: 2,
                    checkbox: false,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 106
                    , onSuccess: function (data) {
                        if (data.t) {
                            $("#path_tree").html("<li style=\" padding:5px 5px;\">" + data.v + "</li>");
                        }
                    }, onBeforeExpand: function (node) {
                        if (node.data.children.length == 0) {
                            JZMLWJTree(node, "false");
                        }
                    }, onSelect: function (node) {
                        if (node.data && $.trim(node.data.WJMC) && $.trim(node.data.WJLJ)) {

                            $("#pdfFrm").remove();
                            $("#pdfShow").ligerPanel({
                                title: node.data.text,
                                width: '100%',
                                frameName: 'pdfFrm',
                                height: h,
                                url: '/Pages/LSYJ/ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))

                            });
                        }

                    }, onCheck: function (node, checked) {
                        if (checked) {
                            JZMLWJTree(node, checked);
                        }
                    }
                });

            }
        }

        //绑定文件
        function JZMLWJTree(node, ischecked) {
            if (node.data.ISLEAF == "0") {
                $.ligerDialog.waitting('数据读取中,请稍候...');
                $.post("/Pages/Business/FileBrowse.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
                    if (newData.t) {
                        //$.ligerDialog.error(newData.v);
                    } else {

                        //alert(node.data.children.length)
                        //console.log(JSON.stringify(newData));
                        $(node.target).children("ul").remove();
                        path_tree.append(node.target, newData);
                        $(node.target).find(".l-expandable-close").click();
                    }

                    $.ligerDialog.closeWaitting();
                }, 'json');
            }
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
                        zzzt: $("#sct_relevance").val(),
                        ajmc: $("#txt_name").val(),
                        ajlb: $("#ajlbbm_hidd").val(),
                        lar: $("#txt_dutyman").val(),
                        ajbh: $("#txt_key").val(),
                        wsbh: $("#txt_wsbh").val(),
                        zzr: $("#txt_zzr").val(),
                        timebegin: $("#txt_time_begin").val(),
                        timeend: $("#txt_time_end").val(),
                        zztimebegin: $("#txt_zzsj_begin").val(),
                        zztimeend: $("#txt_zzsj_end").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("zzzt", $("#sct_relevance").val());
            grid.setParm("ajmc", $("#txt_name").val());
            grid.setParm("ajlb", $("#ajlbbm_hidd").val());
            grid.setParm("lar", $("#txt_dutyman").val());
            grid.setParm("ajbh", $("#txt_key").val());
            grid.setParm("wsbh", $("#txt_wsbh").val());
            grid.setParm("zzr", $("#txt_zzr").val());
            grid.setParm("timebegin", $('#txt_time_begin').val());
            grid.setParm("timeend", $('#txt_time_end').val());
            grid.setParm("zztimebegin", $('#txt_zzsj_begin').val());
            grid.setParm("zztimeend", $('#txt_zzsj_end').val());
        }
      
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
