<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeCheck.aspx.cs" Inherits="WebUI.Pages.Business.MakeCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>制作审核</title>
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
        div#btn_shyes {
            background: #4CAF50;
        }
        div#btn_shno {
            background: red;
        }
        div#btn_sdbs {
            background: #618bc9;
        }
        div#top_div {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background: white;
            line-height: 30px;

        }
         .l-panel-bwarp {
            background: white;
        }
    </style>
</head>
<body style="padding: 15px; overflow: hidden;background: #eef2f5;">
    <%--搜索div--%>
    <div id="top_div" style=" overflow-x: auto;">
        <table class="searchbartab" border="0" >
            <tr>
                <td style="width: 80px; text-align: right;">
                    制作状态：
                </td>
                <td style="">
                    <select id="sct_relevance" class="l-text" name="sct_relevance" style="width: 162px">
                        <option value="-1">全部</option>
                        <option value="2" selected>待审核</option>
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

                 <td style="width: 80px; text-align: right;">
                    制作人：
                </td>
                <td style="">
                    <input id="txt_zzr" class="l-text" type="text" name="txt_zzr" style="width: 160px;" />
                </td>
                 <td style="width: 80px; text-align: right;">
                    制作时间：
                </td>
                <td>
                    <input id="txt_zzsj_begin" type="text" name="txt_zzsj_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_zzsj_end" type="text" name="txt_zzsj_end" style="width: 80px;" />
                </td>

                
            </tr>
            <tr>
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
    <div id="top_div_to" style="padding: 5px; display: none;">
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
            var layout = $("#layout").ligerLayout({ leftWidth: 200, space: 4, height: '100%', heightDiff: -40, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
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
                width: 70
            });
            $('#btn_shyes,#btn_shyesTo').ligerButton({
                text: '通过',
                icon: '../../images/tg.png'
            });
            $('#btn_shno,#btn_shnoTo').ligerButton({
                text: '不通过',
                icon: '../../images/wtg.png'
            });
            $('#btn_sdbs').ligerButton({
                text: '手动报送',
                icon: '../../images/sdfx.png',
                width:70
            });

            $("#btn_back").ligerButton({
                width: 80,
                text: '返 回',
                icon: '/LigerUI/lib/LigerUI/skins/icons/back.gif'
            });


            function resizeLayout() {
                h = $(window).height() - 40;
                $("#layout").height(h);
                $("#leftFrm").height(h - 26);
                $("#leftFrm").parent(".l-layout-left").height(h);
                $("#leftFrm").parent(".l-layout-left").next(".l-layout-center").height(h);
                $("#leftFrm").parent(".l-layout-left").next(".l-layout-center").children(".l-layout-content").height(h);
                $("#pdfFrm").height(h - 20);
            }
            
            var betime = '<%=SetBeTime %>'; //initValue: betime + '-12-26',
            $("#txt_time_begin").ligerDateEditor({ width: 88, labelWidth: 80, labelAlign: 'center',  onChangeDate: function (value) {
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
                { display: '制作状态', name: 'ZZZT', width: 150, render: function (item) {
                    if (item.ZZZT == 6) {
                        return '<span style="color:red;">报送失败</span>';
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
                { display: '案件编号', name: 'AJBH', width: 200 },
                { display: '文书编号', name: 'WSBH', width: 250 },
                { display: '案件名称', name: 'AJMC', width: 150 },
                { display: '案件类别名称', name: 'AJLB_MC', width: 150 },
                { display: '犯罪嫌疑人姓名', name: 'XYR', width: 150 },
                { display: '立案时间', name: 'SLRQ', width: 150 },
                { display: '立案单位', name: 'CBDW_MC', width: 150 },
                { display: '立案人', name: 'CBR', width: 150 },
                { display: '审核人', name: 'JZSHR', width: 150 },
                { display: '审核时间', name: 'JZSHSJ', width: 150 },

                { display: '制作人', name: 'JZSCRXM', width: 150 },
                { display: '制作时间', name: 'CJSJ', width: 150 },

                { display: '批注', name: 'JZPZ', width: 150 },
                { display: '', name: 'JZBH', width: 1, hide: true },
                { display: '', name: 'BMSAH', width: 1, hide: true }
                ], rownumbers: true, checkbox: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/Business/MakeCheck.aspx?page=1',
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
            //审核通过
            $("#btn_shyes,#btn_shyesTo").click(function () {
                var row = grid.getCheckedRows();
                if (row) {
                    var str = "";
                    $(row).each(function () {
                        str += $.trim(this.JZBH) + ",";
                    });

                    var ar = new Array();
                    ar[0] = { name: "bmsah", value: str };
                    ar[1] = { name: "t", value: "Add" };
                    ar[2] = { name: "type", value: "4" };
                    ar[3] = { name: "txt_pz", value: "" };
                    $.ligerDialog.confirm('确定选择的案件审核通过？', function (yes) {
                        if (yes) {
                            $.ajax({
                                type: "POST",
                                url: "MakeCheck.aspx",
                                data: ar,
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
                                        grid.reload();
                                        $.ligerDialog.success(data.v);
                                    } else {
                                        $.ligerDialog.error(data.v);
                                    }
                                }
                            });
                        }
                    });

                } else {
                    $.ligerDialog.warn('请选择审核通过项');
                }
            });
            //审核不通过
            $("#btn_shno,#btn_shnoTo").click(function () {
                var row = grid.getSelectedRow();
                if (row && grid.getCheckedRows().length > 1) {
                    $.ligerDialog.warn('一次只能审核不通过一条信息');
                    return false;
                }
                $.ligerDialog.confirm('确定选择的案件审核不通过？', function (yes) {
                    if (yes) {
                        $.ligerDialog.open({ title: '审核', target: $('#save_div'), width: 500,
                            buttons: [{ text: '确定', onclick: function (item, dialog) {
                                submitForm();
                            }, cls: 'l-dialog-btn-highlight'
                            },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#save_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
                        });
                    }
                });
            });
            //手动报送
            $('#btn_sdbs').click(function () {
                var row = grid.getSelectedRow();
                if (row) {
                    if (row.ZZZT != 6) {
                        $.ligerDialog.error("必须报送失败的案件才能手动报送。");
                        return false;
                    }
                    $.ligerDialog.confirm('是否确定手动报送？', function (yes) {
                        if (yes) {
                            $.ajax({
                                type: "POST",
                                url: "MakeCheck.aspx",
                                data: { t: "sdsb", AJBH: row.AJBH, WSBH: row.WSBH, ZZZT: row.ZZZT },
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
                                    if (data.Stat == 0) {
                                        grid.reload();
                                        $.ligerDialog.success(data.Msg);
                                    } else {
                                        $.ligerDialog.error(data.Msg);
                                    }
                                }
                            });
                        }
                    });
                }
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
                    url: "MakeCheck.aspx",
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
                    url: "/Pages/Business/MakeCheck.aspx",
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
                        if ($.trim(node.data.WJMC) && $.trim(node.data.WJLJ)) {

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
                $.post("/Pages/Business/MakeCheck.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
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
