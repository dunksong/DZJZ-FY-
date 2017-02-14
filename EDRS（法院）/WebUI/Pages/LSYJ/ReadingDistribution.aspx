<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingDistribution.aspx.cs"
    Inherits="WebUI.Pages.LSYJ.ReadingDistribution" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷分配</title>
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
            margin: 0;
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
         .l-layout-left,.l-layout-center{
             border: 1px solid #ccc;
             border-radius: 10px;
          }      
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding: 10px;">
    <div id="tb" style="margin-bottom: 10px;
                overflow-x: auto;
                border: 1px solid #ccc;
                border-radius: 10px;
                padding: 10px;
                background: white;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp
        <div id="btn_search" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;
        <div id="btn_save" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
    </div>
    <div id="layout" style="width: 100%; margin: 0; padding: 0;">
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
    <%--选择案件--%>
    <div id="select_div" style="width: 760px; display: none;">
        <div id="tb" style="background-color: #f8f8f8">
            <div style="padding: 4px 5px;">
                <%  if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                    { %>
                唯一编号：
                <% }
                    else
                    { %>
                案号名称：
                <% } %>
                <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 200px" />&nbsp;&nbsp;
                <%  if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                    { %>
                事项议题：
                <% }
                    else
                    { %>
                案由：
                <% } %>
                <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 200px" />
                <div id="btn_search_aj" style="width: auto; display: inline-block; vertical-align: bottom;">
                </div>
            </div>
        </div>
        <div id="ajGrid" style="margin: 0px; padding: 0px">
        </div>
    </div>
    <%--保存设置--%>
    <div id="save_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 60px 20px 60px">
            <form id="save_form" method="post">
            <table class="l-table">
                <tr>
                    <td style="width: 90px;">
                        <%-- 律师证号：--%>
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <%-- <input class="l-text" id="txt_lszh" type="text" name="txt_lszh" maxlength="200" />--%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        阅卷人姓名：
                    </td>
                    <td>
                        <input type="hidden" id="lszh_hidd" name="lszh_hidd" value="" />
                        <input class="l-text" id="txt_lsxm" type="text" name="txt_lsxm" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        阅卷开始时间：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjkssj" type="text" name="txt_yjkssj" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        阅卷结束时间：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjjssj" type="text" name="txt_yjjssj" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div style="display: none;">
    </div>
    <script type="text/javascript">
        var path_tree;
        var grid;
        var h = 0;
        var tree_diver1 = '/LigerUI/lib/LigerUI/skins/icons/archives.gif';
        var tree_folder1 = '/LigerUI/lib/LigerUI/skins/icons/calendar.gif';
        var tree_file1 = '/LigerUI/lib/LigerUI/skins/icons/attibutes.gif';
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {
            $("#txt_yjkssj").ligerDateEditor({ labelWidth: 100, labelAlign: 'center', showTime: true });
            $("#txt_yjjssj").ligerDateEditor({ labelWidth: 100, labelAlign: 'center', showTime: true });

            $('#btn_search').ligerButton({
                width: 80,
                text: '选择'+vn,
                icon: '../../images/NewAdd/cx.png'
            });
            $("#btn_save").ligerButton({
                width: 80,
                text: '保存分配',
                icon: '../../images/NewAdd/bc.png'
            });
            $('#btn_search_aj').ligerButton({
                width: 60,
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });
            $("#btn_back").ligerButton({
                width: 80,
                text: '返回列表',
                icon: '../../images/NewAdd/fh.png'
            });
            var layout = $("#layout").ligerLayout({ leftWidth: 380, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();
            });
            function resizeLayout() {
                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = height - 30;
                $("#leftFrm").height(h);
                $("#pdfFrm").height(h);
            }

            $("#pageloading").hide();

            //案件查询
            $("#btn_search").click(function () {
                AJGrid();
                $.ligerDialog.show({ title: '选择'+vn, target: $('#select_div'), width: 780,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        var grdata = grid.getSelected();
                        if (grdata != null && grdata.BMSAH != null) {
                            JZMLTree(grdata.BMSAH);
                            $("#key_hidd").val(grdata.BMSAH);
                            $("#span_ajname").html("当前卷宗：" + grdata.AJMC + "&nbsp;【" + grdata.BMSAH + "】");
                            dialog.hidden();
                        } else {
                            $.ligerDialog.warn('请先选择一个'+vn);
                        }

                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        dialog.hidden();
                    }
                    }],
                    isResize: false
                });
            });
            //点击搜索按钮
            $("#btn_search_aj").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        casename: $("#txt_name").val(),
                        relevance:"1,2,3,4,5,6",
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });
            //返回
            $("#btn_back").click(function () {
                location.href = "/Pages/LSYJ/YJSQ.aspx";
            });

            ///保存
            $("#btn_save").click(function () {
                if (!path_tree) {
                    $.ligerDialog.warn('请先选择'+vn);
                    return false;
                }
                //绑定人员
                getPeople();
                $.ligerDialog.open({ title: '分配保存设置', target: $('#save_div'), width: 470,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        // $.ligerDialog.waitting('数据保存中,请稍候...');

                        var arrCount = 0;
                        var len = 0;
                        var jsonArr = new Array();
                        var d = path_tree.getChecked();
                        len = d.length;

                        for (var i = 0; i < len; i++) {
                            if (!path_tree.hasChildren(d[i].data)) {
                                jsonArr[arrCount] = { "WJXH": d[i].data.id };
                                arrCount++;
                            }
                        }

                        //                console.log(JSON.stringify(jsonArr));
                        //                return false;
                        if (jsonArr.length > 0) {
                            var ubmsah = parent.GetQueryString(window.location.search.substr(1), "bmsah");
                            setTimeout(function () {                                
                                if (ubmsah)
                                    $("#key_hidd").val(decodeURI(decodeURI(ubmsah)));
                                var jdata = $('#save_form').serializeArray();
                                jdata[jdata.length] = { name: "t", value: "AddData" };
                                jdata[jdata.length] = { name: "json", value: JSON.stringify(jsonArr) };
                                jdata[jdata.length] = { name: "yjxh", value: parent.GetQueryString(window.location.search.substr(1), "yjxh") };
                                $.post("/Pages/LSYJ/ReadingDistribution.aspx", jdata, function (newData) {
                                    $("#save_form")[0].reset();
                                    $("#lszh_hidd").val("");
                                    $.ligerDialog.closeWaitting();
                                    $.ligerDialog.hide();
                                    if (newData.t == "win") {
                                        $.ligerDialog.success(newData.v);
                                    } else {
                                        $.ligerDialog.error(newData.v);
                                    }
                                }, 'json');
                            }, 50);
                        } else {
                            $.ligerDialog.closeWaitting();
                            $.ligerDialog.warn('请先选择'+vn+'文件');
                        }

                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#save_form")[0].reset();
                        $("#lszh_hidd").val("");
                        dialog.hidden();
                    }
                    }], isResize: true
                });
            });
          
            var bmsah = parent.GetQueryString(window.location.search.substr(1), "bmsah");
            var ajmc = parent.GetQueryString(window.location.search.substr(1), "ajmc");
            setTimeout(function () {
                if (bmsah != null && bmsah != "" && bmsah != undefined) {
                
                    $("#span_ajname").html("当前卷宗：" + decodeURI(decodeURI(ajmc)) + "&nbsp;【" + decodeURI(decodeURI(bmsah)) + "】");
                    JZMLTree(bmsah);
                }
            }, 50);
        });


        //绑定文件目录
        function JZMLTree(bmsah) {
            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/ReadingDistribution.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: parent.GetQueryString(window.location.search.substr(1), "yjxh"),wjtype:"N" },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    nodeWidth: 600
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
                                url: 'ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))

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
            
            if(node.data.ISLEAF=="0"){
                $.ligerDialog.waitting('数据读取中,请稍候...');
                $.post("/Pages/LSYJ/ReadingDistribution.aspx?pa=2", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
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
        //绑定案件列表
        function AJGrid() {
            
            grid = $("#ajGrid").ligerGrid({
                columns: [
             
                 <%  if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                     { display: '唯一编号', name: 'BMSAH', minWidth: 280,  },
                     { display: '事项议题', name: 'AJMC', minWidth: 150 },
                    <% }
                       else
                       { %>
                     {display: '案号名称', name: 'BMSAH', minWidth: 280,  },
                    { display: '案由', name: 'AJMC', minWidth: 280 },
                    <% } %>
                
                { display:vn+ '类别名称', name: 'AJLB_MC', minWidth: 200 }
//                {display: '', name: 'BMSAH', width: 1,hide:true   },
//                { display: vn+'状态', name: 'AJZT', width: 70, render: function (item) {
//                    if (parseInt(item.AJZT) == 0) return '受理';
//                    else if (parseInt(item.AJZT) == 1) return '办理';
//                    else if (parseInt(item.AJZT) == 2) return '已办';
//                    else if (parseInt(item.AJZT) == 3) return '归档';
//                    else return item.AJZT;
//                }
//                }
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                usePager: true, width: '99%', height: 350,       //服务器分页
                url: '/Pages/LSYJ/ReadingDistribution.aspx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: "ListBind", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                    dutyman: "",
                    relevance: "1,2,3,4,5,6",
                    timebegin: "",
                    timeend: ""
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
        }
        //绑定阅卷人列表
        function getPeople() {
            $("#txt_lsxm").ligerComboBox({
                width: 150,
                slide: false,
                selectBoxWidth: 600,
                selectBoxHeight: 240,
                valueField: 'GH',
                textField: 'MC',
                parms: { t: "getPeople" },
                grid: {
                    url: "/Pages/LSYJ/ReadingDistribution.aspx",
                    columns: [
                        { display: '姓名', name: 'MC', minWidth: 100 },
                        { display: '性别', name: 'XB', minWidth: 100, render: function (item) {
                            if (item.XB == '0') return '女';
                            else if (item.XB == '1') return "男";
                            else return "";
                        }
                        },
                        { display: '工号', name: 'GH', minWidth: 100 },
                        { display: '登录别名', name: 'DLBM', minWidth: 100 },
                        { display: '工作证号', name: 'GZZH', minWidth: 100 },
                        { display: '单位', name: 'DWMC', width: 1, hide: 'none' }
                        ],
                    height: 240,
                    switchPageSizeApplyComboBox: false,
                    pageSize: 20
                },
                onSelected: function (value) {
                    if (value) {
                        var d = this.getSelected();

                        $("#lszh_hidd").val(value+","+d.DWBM);
                        
                    } else
                        $("#lszh_hidd").val("");
                },
                condition: { fields: [{ name: 'MC', label: '关键字', width: 90, type: 'text'}] }
            });
        }
        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("casename", $("#txt_name").val());
            grid.setParm("relevance","1,2,3,4,5,6");
        }
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
