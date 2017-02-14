<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseUnit.aspx.cs" Inherits="WebUI.Pages.Report.CaseUnit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基本情况查询</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <%-- <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>--%>
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
        #searchbar
        {
            overflow-x: auto;
        }
        #searchbar table
        {
            width: auto;
        }
        #searchbar table tr td
        {
            white-space: nowrap;
        }
        .l-tree-icon
        {
            background: url('/images/icons/3.png') no-repeat !important;
            background-position: center center !important;
        }
        
        /* 按钮 */
        div .l-button {
            color: white;
             top: -4px;
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
            border-radius: 10px;
            background: white;
        }
    </style>
</head>
<body style="padding: 15px;overflow: hidden;">
    <form id="form1" runat="server">
    <div id="searchbar" style=" line-height: 28px;">
        <table>
            <tr>
                <td>
                    &nbsp;&nbsp;单位名称：
                </td>
                <td>
                    <input id="txt_dwbm" type="text" name="txt_dwbm" />
                </td>
                <td>
                    &nbsp;&nbsp;<%= ((VersionName)0).ToString() %>类别：
                </td>
                <td>
                    <input id="txt_ajlb" class="l-text" type="text" name="txt_ajlb" />
                </td>
                <td>
                    &nbsp;&nbsp;<% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                                   { %>
                    唯一编号：
                    <% }
                                   else
                                   { %>
                    案件编号：
                    <% } %>
                </td>
                <td>
                    <input id="txt_key" class="l-text" type="text" name="txt_key" />
                </td>
                <td>
                    &nbsp;&nbsp;
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案件名称：
                    <% } %>
                </td>
                <td style="width: 150px;" colspan="2">
                    <input id="txt_name" class="l-text" type="text" name="txt_name" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;受理日期：
                </td>
                <td colspan="3">
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td>
                    &nbsp;&nbsp;承办人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                </td>
                <td>
                    &nbsp;&nbsp;制作状态：
                </td>
                <td>
                    <select id="sct_relevance" class="l-text" name="sct_relevance" style="width: 132px">
                        <option value="-2">全部</option>
                        <option value="-1">未制作</option>
                        <option value="0">制作中</option>
                        <option value="1">已上传</option>
                        <option value="2">待审核</option>
                        <option value="3">审核不通过</option>
                        <option value="4">审核通过</option>
                        <option value="5">已报送</option>
                        <option value="6">报送失败</option>
                    </select>
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_deriveExcel" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--表格--%>
    <div id="mainGridAj" style="margin: 0; padding: 0">
    </div>
    <script type="text/javascript">

        var grid = null;
        var dwbm_tree;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {
                  
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


            grid = $("#mainGridAj").ligerGrid({
                columns: [
                { display: '制作状态', name: 'ISREGARD', align: 'center', width: 80,
                    render: function (item) {
                           if(item.ZZZT == 6)
                           {
                            return '<span style="color:red;">报送失败</span>';
                           }
                           else if(item.ZZZT == 5)
                           {
                            return '已报送';
                           }
                           else if(item.ZZZT == 4)
                           {
                            return '审核通过';
                           }
                           else if(item.ZZZT == 3)
                           {
                            return '<span style="color:red;">审核不通过</span>';
                           }
                            else if(item.ZZZT == 2)
                           {
                            return '待审核';
                           }
                           else if(item.ZZZT == 1)
                           {                        
                            return '<span style="color:blue;">已上传</span>';
                            }
                           else if(item.ZZZT == 0)
                           {
                            return '制作中';
                           }
                            else
                           {                         
                            return '<span style="color:red;">未制作</span>';
                           }                       
                    }
                },
                  <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    { display: '事项议题', name: 'AJMC', width: 150 },
                    { display: '唯一编号', name: 'BMSAH', width: 250,  },
                    <% }
                       else
                       { %>
                    { display: '案件名称', name: 'AJMC', width: 150 },
                    { display: '案件编号', name: 'AJBH', width: 200,  },
                    { display: '文书编号', name: 'WSBH', width: 250,  },
                    { display: '文书名称', name: 'WSMC', width: 150,  },
                    <% } %>
                
                
                { display: vn+'类别', name: 'AJLB_MC', width: 150 },
                { display: '嫌疑人姓名', name: 'XYR', width: 200 },
                { display: '立卷单位', name: 'AJ_DWMC', width: 200,
                    render: function (item) {
                        if (item.CBDW_MC)
                            return item.CBDW_MC;
                        else return item.AJ_DWMC;
                    }
                },
              //  { display: '承办部门', name: 'CBBM_MC', width: 200 },                
                { display: '立卷人', name: 'CBR', width: 100 },
                { display: '立案时间', name: 'SLRQ', width: 200 },                
//                { display: vn+ '状态', name: 'AJZT', width: 70, render:
//                    function (item) {
//                        if (parseInt(item.AJZT) == 0) return '受理';
//                        else if (parseInt(item.AJZT) == 1) return '办理';
//                        else if (parseInt(item.AJZT) == 2) return '已办';
//                        else if (parseInt(item.AJZT) == 3) return '归档';
//                        else return item.AJZT;
//                    }
//                },
                { display: '', name: 'BMSAH', width: 1, hide:true },
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                usePager: true, enabledSort: false, width: '100%', height: '100%', heightDiff: -10,      //服务器分页
                url: '/Pages/Report/CaseUnit.aspx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: "ListBind-NO", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                    unit: dwbm_tree.getValue(),
                    ajlb: $("#ajlbbm_hidd").val(),
                    dutyman: $("#txt_dutyman").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
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
                ,
                onChangeSort: function (element) {
//                    grid.setParm("sortName", element);
//                    grid.setParm("sortOrder", grid.options.sortOrder);
                    gridSetParm();
                }
            });
            //grid.setColumnWidth(2, 110);

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            $('#btn_deriveExcel').ligerButton({
                width: 80,
                text: '导出Excel',
                icon: '../../images/dcExcel.png'
            });
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
                        caseunit: dwbm_tree.getValue(),
                        caseajlb: $("#ajlbbm_hidd").val(),
                        dutyman: $("#txt_dutyman").val(),
                        relevance: $("#sct_relevance").val(),
                        timebegin: $('#txt_time_begin').val(),
                        timeend: $('#txt_time_end').val(),
                        page: 1, pagesize: grid.options.pageSize
    //                    sortName: grid.options.sortName,
    //                    sortOrder: grid.options.sortOrder
                    });
                }
            });
            //导出
            $("#btn_deriveExcel").click(function () {
                $.ligerDialog.waitting('正在导出,请稍候...');
                $.ajax({
                    type: "POST",
                    url: "/Pages/Report/CaseUnit.aspx",
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

            $("#pageloading").hide();
        });
        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("casename", $("#txt_name").val());
            grid.setParm("dutyman", $("#txt_dutyman").val());
            grid.setParm("caseunit", dwbm_tree.getValue());
            grid.setParm("caseajlb", $("#ajlbbm_hidd").val());
            grid.setParm("timebegin", $('#txt_time_begin').val());
            grid.setParm("timeend", $('#txt_time_end').val());
            grid.setParm("relevance", $('#sct_relevance').val());

        }
    </script>
    </form>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
