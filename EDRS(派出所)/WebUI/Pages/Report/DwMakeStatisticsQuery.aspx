<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DwMakeStatisticsQuery.aspx.cs"
    Inherits="WebUI.Pages.Report.DwMakeStatisticsQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>单位卷宗制作情况查询</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
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
        .l-text-combobox
        {
            height:21px;
        }
        .l-box-select-inner{max-height:300px; min-height:300px;}
        .l-box-select-inner .l-tree{ min-width:400px !important;}
        #searchbar{overflow-x:auto;}
        #searchbar table
        {
            width: auto;
        }
        #searchbar table tr td
        {
            white-space:nowrap;
        }
        .l-tree-icon
        {
            background:url('/images/icons/3.png') no-repeat !important;
            background-position:center center  !important;
        }
        
        /* 按钮 */
        div .l-button {
            color: white;
        }
        div#btn_search {
            background: #ed6d4a;
        }
        div#btn_deriveExcel {
            background: #92afb7;
        }
        
         div#searchbar {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius:10px;
            background: white;
        }
    </style>
</head>
<body style="padding: 15px;overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style=" line-height: 28px;">
        <table border="0">
            <tr>
                <td>
                    单位名称：
                </td>
                <td>
                    <input id="txt_dwbm" type="text" name="txt_dwbm" />
                </td>
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
                <td style="padding-left: 10px;">
                   
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                    案件编号：
                    <% } %>
                </td>
                <td colspan="2">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 220px;" />
                </td>
            </tr>
            <tr>
                <td>
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
                <td style="padding-left: 10px;">
                    立案时间：
                </td>
                <td colspan="3">
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="padding-left: 10px;">
                    立卷人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_deriveExcel" style="margin-left: 10px; display:inline-block; 
                        vertical-align: bottom;">
                    </div>
                    
                </td>
            </tr>
        </table>
    </div>
    <%--表格--%>
    <div id="mainGridAj" style="margin: 0; padding: 0">
    </div>
    <div style="display: none;">
        <!-- g data total ttt -->
    </div>
    <script type="text/javascript">

        var grid = null;
        var dwbm_tree;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            $('#btn_deriveExcel').ligerButton({
                width: 80,
                text: '导出Excel',
                icon: '../../images/dcExcel.png'
            });

            $("#btn_deriveExcel").click(function () {
                $.ligerDialog.waitting('正在导出,请稍候...');
                $.ajax({
                    type: "POST",
                    url: "/Pages/Report/DwMakeStatisticsQuery.aspx",
                    data: {
                        t: "DeriveData",
                        key: $("#txt_key").val(),
                        casename: $("#txt_name").val(),
                        dutyman: $("#txt_dutyman").val(),
                        ywbm: $("#ywbm_hidd").val(),
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
                            location.href = "/download.aspx?t=getmodel&fileid="+data.v;
                        } else {
                            $.ligerDialog.error(data.v);
                        }
                        $.ligerDialog.closeWaitting();
                    }
                });
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

             var tree_node;
            var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { text: '全选/反选', click:  function itemclick(){
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
            } }         
            ]
            });
        
            //单位编码
            //单位编码
            dwbm_tree = $("#txt_dwbm").unitJuris({width:130,checkbox:true});
//            dwbm_tree = $("#txt_dwbm").ligerComboBox({
//                valueField: 'id',
//                selectBoxWidth: 300,
//                selectBoxHeight: 300, 
//                treeLeafOnly: false,
//                tree: { url: '/Handler/ZZJG/DZJZ_Report.ashx?action=GetDwbm', checkbox: true, ajaxType: 'get' ,autoCheckboxEven:false ,onContextmenu: function (node, e)
//            {         
//           
//             dwbm_tree.selectBox.siblings(".l-menu").bind({mouseenter:function(e){
//                dwbm_tree.selectBox.show();                       
//            },mouseleave:function(e){
//                
//            }});

//               tree_node = node;              
//                menu.show({ top: e.pageY-10, left: e.pageX+10 });
//                return false;
//            }}
//               
//            });
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
                parms: { action: "GetAjlxList" },
                valueFieldID: 'ajlbbm_hidd',
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });
            grid = $("#mainGridAj").ligerGrid({
                columns: [
                  <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    { display: '事项议题', name: 'AJMC', minWidth: 150 },
                    { display: '唯一编号', name: 'BMSAH', minWidth: 280,  },
                    <% }
                       else
                       { %>
                    { display: '案件名称', name: 'AJMC', minWidth: 150 },
                    { display: '案件编号', name: 'AJBH', minWidth: 200,  },
                     { display: '文书编号', name: 'WSBH', width: 250,  },
                    { display: '文书名称', name: 'WSMC', width: 150,  },
                    <% } %>
                
                
                { display: vn+'类别', name: 'AJLB_MC', minWidth: 200 },
                { display: '嫌疑人姓名', name: 'XYR', minWidth: 200 },
                { display: '立卷单位', name: 'AJ_DWMC', minWidth: 120,
                    render: function (item) {
                        if (item.CBDW_MC)
                            return item.CBDW_MC;
                        else return item.AJ_DWMC;
                    }
                },
//                { display: '承办部门', name: 'CBBM_MC', minWidth: 120 },
                { display: '立卷人', name: 'CBR', minWidth: 100 },
                { display: '卷数', name: 'JNUM' },
               { display: '文件数', name: 'WJNUM' },
               { display: '文件页数', name: 'WJYNUM' },
               { display: '', name: 'BMSAH', Width: 1,hide:true  }
                 ], fixedCellHeight: false, rownumbers: true, //服务器排序
                usePager: true, width: '100%', height: '100%',       //服务器分页
                url: '/Pages/Report/DwMakeStatisticsQuery.aspx',
                pageSizeOptions: [20, 50, 100, 500], pageSize: 50, 
                parms: { t: "ListBind-NO", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    ywbm: $("#ywbm_hidd").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val(),
                    dwbm: dwbm_tree.getValue()
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
                },
                onChangeSort: function (element) {
                    gridSetParm();
                }

            });
            //grid.setColumnWidth(2, 110);

            $("#pageloading").hide();

            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        casename: $("#txt_name").val(),
                        dutyman: $("#txt_dutyman").val(),
                        ywbm: $("#ywbm_hidd").val(),
                        timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                        timeend: $('#txt_time_end').val(),
                        dwbm: dwbm_tree.getValue(),
                        caseajlb: $("#ajlbbm_hidd").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("t", "ListBind");
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("casename", $("#txt_name").val());
            grid.setParm("dutyman", $("#txt_dutyman").val());
            grid.setParm("ywbm", $("#ywbm_hidd").val());
            grid.setParm("timebegin", $('#txt_time_begin').val());
            grid.setParm("timeend", $('#txt_time_end').val());
            grid.setParm("dwbm", dwbm_tree.getValue());
            grid.setParm("caseajlb", $('#ajlbbm_hidd').val());
        }
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
