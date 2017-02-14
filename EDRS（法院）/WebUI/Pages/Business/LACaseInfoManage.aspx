<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LACaseInfoManage.aspx.cs"
    Inherits="WebUI.LACaseInfoManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Access-Control-Allow-Origin" content="*" />
    <title>信息管理</title>
     <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" /> 
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>   
    <script src="/Scripts/tools/ligerUI/js/ligerui.all.js" type="text/javascript"></script>   
    <script src="/Scripts/jquery.PrintArea.js" type="text/javascript"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
    <style type="text/css">
        .text_rigth
        {
            text-align: right;
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
        .l-box-select-lookup .l-box-select-inner
        {
            min-height: 247px;
            height: 247px;
        }
        #actvxhelp
        {
            background: url("/LigerUI/lib/LigerUI/skins/icons/help.gif") no-repeat left center;
            padding-left: 18px;
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
    </style>
    <style type="text/css">
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #a3c0e8;
            display: inline-block;
            width: 100%;
        }
     
        .l-text-field
        {
            position: inherit;
            margin: 0;
        }
        .l-text, .l-textarea
        {
            width: 190px;
        }
        #add_form table tr td, #add_form2 table tr td
        {
            padding: 4px 0px;
        }
        .form-td-right
        {
            text-align: right;
        }
    </style>
</head>
<body style="padding: 0px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style="padding-bottom: 5px; line-height: 28px; overflow-x: auto;">
        <table class="searchbartab" border="0">
            <tr>
                <td style="width: 80px; text-align: right;">
                    <% if (Version == "PSB")
                       { %>
                    立卷单位：
                    <%}
                       else
                       {%>
                    单位名称：
                    <%}%>
                </td>
                <td style="">
                    <input id="txt_dwbm" type="text" name="txt_dwbm" style="width: 283px;" />
                </td>
                <%-- <td style="width: 80px; text-align: right; padding-left: 10px;">
                    案件类别：
                </td>
                <td colspan="6" style="">
                    <input id="txt_ajlb" class="l-text" type="text" name="txt_ajlb" style="width: 283px;" />
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>--%>
                <td style="width: 80px; text-align: right;">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else if (Version == "PSB")
                       { %>
                    案号名称：
                    <%}
                       else
                       { %>
                    部门受案号：
                    <% } %>
                </td>
                <td style="">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 160px;" />
                </td>
                <%-- <td style="width: 80px; text-align: right;">
                    文书编号：
                </td>
                <td>
                    <input id="txt_searchws" class="l-text" type="text" name="txt_key" style="width: 160px;" />
                </td>--%>
                <td style="width: 80px; text-align: right; padding-left: 10px;">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案由：
                    <% } %>
                </td>
                <td>
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    扫描人：
                </td>
                <td>
                    <input id="txt_smr" class="l-text" type="text" name="txt_name" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    扫描时间：
                </td>
                <td>
                    <input id="txt_smsj_begin" type="text" name="txt_smsj_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_smsj_end" type="text" name="txt_smsj_end" style="width: 80px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right;">
                    <% if (Version == "PSB")
                       { %>
                    归档日期：
                    <%}
                       else
                       {%>
                    受理日期：
                    <%}%>
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" style="width: 80px;" />
                </td>
                <td style="padding-left: 10px; width: 80px; text-align: right;">
                    <% if (Version == "PSB")
                       { %>
                    承办人：
                    <%}
                       else
                       {%>
                    承办人：
                    <%}%>
                </td>
                <td style="width: 127px;">
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" style="width: 160px;" />
                </td>
                <%--   <td style="padding-left: 10px; width: 80px; text-align: right;">
                    嫌疑人：
                </td>
                <td style="width: 127px;">
                    <input id="txt_xyr2" class="l-text" type="text" name="txt_xyr2" style="width: 160px;" />
                </td>--%>
                <td style="width: 65px; text-align: right;">
                    制作状态：
                </td>
                <td>
                    <select id="sct_relevance" class="l-text" name="sct_relevance" style="width: 163px">
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
                <td colspan="6">
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_make" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                    <%--添加案件--%><% if (Version == "PSB")
                                   {%>
                    <div id="btn_add" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                   <%-- <div id="btn_update" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>--%>
                    <%--<div id="btn_derive" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>--%>
                    <% }%>
                    <%-- <div id="btn_menu" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                    <div id="btn_print" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>--%>
                    <%--&nbsp;<input id="btn_make" type="button" class="l-button" value="制 作" />
                    &nbsp;<input id="btn_derive" type="button" class="l-button" value="导出PDF" />--%>
                    <%--<a id="actvxhelp" onclick="$.ligerDialog.open({title:'插件安装帮助',width:500,height:510, target: $('#help') });"
                        href="javascript:">插件无法正常使用帮助</a>--%>
                </td>
            </tr>
        </table>
    </div>
    <%--表格--%>
    <div id="mainGridAj" style="margin: 0; padding: 0">
    </div>
    <div style="display: none;">
    </div>
  
    <script type="text/javascript">
        var grid = null;
        var dwbm_tree;
        var lasj_time;
        var jasj_time;       
        var fileMake="RecEFileMaker";
        <% if (Version == "PSB") {%> 
            fileMake="CopEFileMaker";
        <%} %>
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
           
            $('#btn_make').ligerButton({
                text: '制作',
                icon: "/images/icons/edittask.png"
                //icon: '/LigerUI/lib/LigerUI/skins/icons/bookpen.gif'
            });
            $('#btn_derive').ligerButton({
                text: '导出PDF',
                icon: "/images/icons/Redo (2).png"
                //icon: '/LigerUI/lib/LigerUI/skins/icons/back.gif'
            });



            <% if (Version == "PSB") {%> 
            
            $("#btn_add").ligerButton({
                text: "编辑"+vn,
                icon: '/LigerUI/lib/LigerUI/skins/icons/edit.gif',
                click:adddata
            });
            $("#btn_update").ligerButton({
                text: "修改"+vn,
                icon: '/LigerUI/lib/LigerUI/skins/icons/edit.gif',
                click:updata
            });
            
             $('#btn_menu').ligerButton({
                text: '编辑目录',
                icon: "/images/icons/bm.png"
            });
            $('#btn_print').ligerButton({
                text: '目录打印',
                icon: "/images/icons/preview_16x16.png"
            });

            lasj_time = $("#txt_lasj").ligerDateEditor({format: "yyyy-MM-dd",width:493,labelWidth: 170, labelAlign: 'right',initValue:function(){
            var d = new Date();
            return d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate();
                },onChangeDate:function(value){
                var d1=new Date($("#txt_lasj").val().replace(/\-/g,"\/"));
                var d2=new Date(value.replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_jasj").val("");
                    $.ligerDialog.warn('立案日期不能大于结案日期');
                }
            } });
           jasj_time = $("#txt_jasj").ligerDateEditor({format: "yyyy-MM-dd",width:170,labelWidth: 170, labelAlign: 'right',onChangeDate:function(value){
                var d1=new Date($("#txt_lasj").val().replace(/\-/g,"\/"));
                var d2=new Date(value.replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_jasj").val("");
                    $.ligerDialog.warn('立案日期不能大于结案日期');
                }
            } });

            <%} %>
                var betime = '<%=SetBeTime %>';
            $("#txt_time_begin").ligerDateEditor({width:88,  labelWidth: 80, labelAlign: 'center', onChangeDate:function(value){                
                var d1=new Date(value.replace(/\-/g,"\/"));
                var d2=new Date($("#txt_time_end").val().replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_time_end").val("");
                }
            } });
            $("#txt_time_end").ligerDateEditor({ width:88, labelWidth: 80, labelAlign: 'center',onChangeDate:function(value){
                var d1=new Date($("#txt_time_begin").val().replace(/\-/g,"\/"));
                var d2=new Date(value.replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_time_end").val("");
                    $.ligerDialog.warn('受理开始日期不能大于结束日期');
                }
            }});

            $("#txt_smsj_begin").ligerDateEditor({width:88,  labelWidth: 80, labelAlign: 'center',initValue: betime + '-12-26',onChangeDate:function(value){                
                var d1=new Date(value.replace(/\-/g,"\/"));
                var d2=new Date($("#txt_smsj_end").val().replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_smsj_end").val("");
                }
            } });
            $("#txt_smsj_end").ligerDateEditor({ width:88, labelWidth: 80, labelAlign: 'center',onChangeDate:function(value){
                var d1=new Date($("#txt_smsj_begin").val().replace(/\-/g,"\/"));
                var d2=new Date(value.replace(/\-/g,"\/"));
                if(d1>d2){
                    $("#txt_smsj_end").val("");
                    $.ligerDialog.warn('受理开始日期不能大于结束日期');
                }
            }});

            
            var tree_node;
            var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { text: '全选/反选', click: function itemclick() {
                var sNode = tree_node;
                if (sNode == null) {
                    $.messager.alert('提示', '未选择任何节点，操作失败！', 'error');
                    return;
                }
            } 
            }
            ]
            });
            // 绑定搜索单位
            dwbm_tree = $("#txt_dwbm").unitJuris({width:197,checkbox:true});

            grid = $("#mainGridAj").ligerGrid({
                columns: [
                { display: '制作状态', name: 'ISREGARD', align: 'center', width: 80,
                    render: function (item) {
                           if(item.LAZZZT == 6)
                           {
                            return '<span style="color:red;">报送失败</span>';
                           }
                           else if(item.LAZZZT == 5)
                           {
                            return '已报送';
                           }
                           else if(item.LAZZZT == 4)
                           {
                            return '审核通过';
                           }
                           else if(item.LAZZZT == 3)
                           {
                            return '<span style="color:red;">审核不通过</span>';
                           }
                            else if(item.LAZZZT == 2)
                           {
                            return '待审核';
                           }
                           else if(item.LAZZZT == 1)
                           {                        
                            return '<span style="color:blue;">已上传</span>';
                            }
                           else if(item.LAZZZT == 0)
                           {
                            return '制作中';
                           }
                            else
                           {                         
                            return '<span style="color:red;">未制作</span>';
                           }
                       
                    }
                },

              <% if (Ver_Advanced_Alone == "ADVANCED_ALONE"){ %>
                { display: '事项议题', name: 'AJMC', minWidth: 150 },
                { display: '唯一编号', name: 'BMSAH', minWidth: 180,  },
               <% }else if(Version == "PSB"){%>
                 { display: '案由', name: 'AJMC', minWidth: 250 },
                    { display: '案号名称', name: 'BMSAH', minWidth: 180,  },
               <%} else{ %>
                { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '部门受案号', name: 'BMSAH', minWidth: 280,  },
               <% } %>
                
                { display: vn+'类别名称', name: 'AJLB_MC', minWidth: 200 },
                        <% if(Version=="PSB" ) {
                        if (Ver_Advanced_Alone != "ADVANCED_ALONE"){
                         %> 
                {display: '原告', name: 'YG', minWidth: 100 },
                {display: '被告', name: 'BG', minWidth: 100 },
                <% }else{ %>
                { display: '备注', name: 'TARYXX', minWidth: 150, render: function (item) {
                    if (item.TARYXX) {
                        return '<a href="javascript:" onclick="showTaryxx(\'' + item.TARYXX + '\')">' + item.TARYXX + '</a>';
                    };
                }
                },
                            <% }} %>

             <% if(Version=="PSB" ) {%>               
                { display: '收案日期', name: 'SARQ', minWidth: 150, render: function (item) {
                    if (item.SARQ == '1900-01-01 00:00:00' || item.SARQ == null || item.SARQ == "") return '';
                    else return item.SARQ.substring(0,10);
                }
                },{ display: '结案日期', name: 'JARQ', minWidth: 150, render: function (item) {
                    if (item.JARQ == '1900-01-01 00:00:00' || item.JARQ == null || item.JARQ == "") return '';
                    else return item.JARQ.substring(0,10);
                }
                },
                { display: '归档日期', name: 'GDRQ', minWidth: 150, render: function (item) {
                    if (item.GDRQ == '1900-01-01 00:00:00' || item.GDRQ == null || item.GDRQ == "") return '';
                    else return item.GDRQ.substring(0,10);
                }
                },
//                { display: '立案时间', name: 'SLRQ', minWidth: 150, render: function (item) {
//                    if (item.SLRQ == '1900-01-01 00:00:00' || item.SLRQ == null || item.SLRQ == "") return '';
//                    else return item.SLRQ.substring(0,10);
//                }
//                },
                {display: '立卷单位', name: 'AJ_DWMC', minWidth: 120,
                render: function (item) {
                    if (item.CBDW_MC)
                        return item.CBDW_MC;
                    else return item.AJ_DWMC;
                }
                },
                { display: '承办人', name: 'CBR', minWidth: 100 },
                { display: '扫描时间', name: 'CJSJ', minWidth: 150, render: function (item) {
                    if (item.CJSJ == null || item.CJSJ == "") return '';
                    else return item.CJSJ;
                }},
                { display: '扫描人', name: 'JZSCRXM', minWidth: 100 },
                { display: '批注', name: 'JZPZ', minWidth: 150,render:function(item){
                    if(item.JZPZ)
                return "<span style='color:red;'>"+ item.JZPZ+"</span>";
                else
                return "";
                } },
                   
            <%}else{%>
                {display: '承办单位', name: 'AJ_DWMC', minWidth: 120,
                render: function (item) {
                    if (item.CBDW_MC)
                        return item.CBDW_MC;
                    else return item.AJ_DWMC;
                }
            },            
                { display: '承办部门', name: 'CBBM_MC', minWidth: 120 },
                { display: '承办人', name: 'CBR', minWidth: 100 },
                { display: '当前阶段', name: 'DQJD' },
                { display: '受理日期', name: 'SLRQ', minWidth: 150, render: function (item) {
                    if (item.SLRQ == '1900-01-01 00:00:00' || item.SLRQ == null || item.SLRQ == "") return '';
                    else return item.SLRQ;
                }
                },
                { display: vn+'状态', name: 'AJZT', width: 70, render: function (item) {
                    if (parseInt(item.AJZT) == 0) return '受理';
                    else if (parseInt(item.AJZT) == 1) return '办理';
                    else if (parseInt(item.AJZT) == 2) return '已办';
                    else if (parseInt(item.AJZT) == 3) return '归档';
                    else return item.AJZT;
                }
                },
                { display: '到期日期', name: 'DQRQ', minWidth: 150, render: function (item) {
                    if (item.DQRQ == '1900-01-01 00:00:00' || item.DQRQ == null || item.DQRQ == "") return '';
                    else return item.DQRQ;
                }
                },
                { display: '办结日期', name: 'BJRQ', minWidth: 150, render: function (item) {
                    if (item.BJRQ == '1900-01-01 00:00:00' || item.BJRQ == null || item.BJRQ == "") return '';
                    else return item.BJRQ;
                }
                },
                { display: '完成日期', name: 'WCRQ', minWidth: 150, render: function (item) {
                    if (item.WCRQ == '1900-01-01 00:00:00' || item.WCRQ == null || item.WCRQ == "") return '';
                    else return item.WCRQ;
                }
                },
                { display: '归档日期', name: 'GDRQ', minWidth: 150, render: function (item) {
                    if (item.GDRQ == '1900-01-01 00:00:00' || item.GDRQ == null || item.GDRQ == "") return '';
                    else return item.GDRQ;
                }
                }
                <%}%>
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
            usePager: true, width: '100%', height: '100%', enabledSort: false,       //服务器分页
            url: '/Pages/Business/CaseInfoManage.aspx',
            pageSizeOptions: [20, 50, 100, 500],
            parms: { t: "ListBind", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                dutyman: $("#txt_dutyman").val(),
                relevance_la: $("#sct_relevance").val(),
                timebegin: $('#txt_time_begin').val(),
                timeend: $('#txt_time_end').val(),
                smr:$('#txt_smr').val(),
                smsj_bg:$('#txt_smsj_begin').val(),
                smsj_en:$('#txt_smsj_end').val(),
                caseUnit:dwbm_tree.getValue(),
                caseajlb:$('#caseajlb').val(),
                wsbh:$("#txt_searchws").val(),
                xyr:$("#txt_xyr2").val(),
                smajla:1
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
   
        $("#pageloading").hide();   

       


    });

    $(document).ready(function () {
        //制作
        $("#btn_make,#btn_derive,#btn_menu,#btn_print").click(function () {
             
            var faceType=0;
            switch (this.id) {
                case "btn_make":
                faceType=6;
                break;
                case "btn_derive":
                faceType="";
                break;
                case "btn_menu":
                faceType=1;
                break;
                case "btn_print":
                faceType=2;
                break;                
                default:
                faceType="";
                break;
            }
     
            var btnid = $(this).attr("id");
            var cksld = grid.getSelected();
            if (cksld) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/Business/CaseInfoManage.aspx',
                    data: { t: "GetMake", id: cksld.BMSAH, dwbm: cksld.AJ_DWBM, type: btnid,interfaceType:faceType,ajbh:cksld.AJBH,wsbh:cksld.WSBH,wsmc:cksld.WSMC },//interfaceType:0制作，1编辑目录，2打印目录
                    dataType: 'json',
                    timeout: 20000,
                    cache: false,
                    beforeSend: function () {
                        // return $("#add_form").form('enableValidation').form('validate');
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        
                        if (data.t && data.t == "error") {
                            $.ligerDialog.error(data.v);
                            return false;
                        }
                        var parm = fileMake+"://" + data.parm;                       

                        if (isAcrobatPluginInstall()) {
                            //判断是否为自己定义浏览器
                         
                         
                            if (typeof (boundObjectForJS) != 'undefined')
                                boundObjectForJS.callCsharp(fileMake+"://1234567812345678" + data.parm + "@");
                            else
                                location.href = parm;
                        }
                    }
                });

            }
            else{
                $.ligerDialog.warn('请先选择一条数据');
                }
         
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
                    dutyman: $("#txt_dutyman").val(),
                    relevance_la: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                    timeend: $('#txt_time_end').val(),
                    smr:$('#txt_smr').val(),
                    smsj_bg:$('#txt_smsj_begin').val(),
                    smsj_en:$('#txt_smsj_end').val(),
                    caseUnit:  dwbm_tree.getValue(),//$('#caseUnit').val(),
                    caseajlb:$('#caseajlb').val(),
                    wsbh:$("#txt_searchws").val(),
                    xyr:$("#txt_xyr2").val(),
                    smajla:1,
                    page: 1, pagesize: grid.options.pageSize
                });
            }
        });     
    });



    <% if (Version == "PSB") {%> 
    //添加
    function adddata() {
        var h = $("body").height();       
        if(!h)
            h=600;
        if(h > 750)
            h=750;
           $.ligerDialog.open({title:'案件制作', url: '/Pages/Print/AddCase.aspx', height: h-50, width: 750
           , buttons: [
                { text: '确定', onclick: function (item, dialog) {                   
                  dialog.frame.btn_printprint(dialog,grid);
                },cls:'l-dialog-btn-highlight' },
                { text: '取消', onclick: function (item, dialog) { dialog.close(); } }
             ], 
             isResize: true
            });
    }
    //修改
    function updata() {
    
        if(grid){
        var row = grid.getSelectedRow();
        if(row){
     $.ligerDialog.open({title:'案件制作', url: '/Pages/Print/AddCase.aspx', height: 400, width: 750
           , buttons: [
                { text: '确定', onclick: function (item, dialog) {                   
                  dialog.frame.btn_printprint(dialog,grid);
                },cls:'l-dialog-btn-highlight' },
                { text: '取消', onclick: function (item, dialog) { dialog.close(); } }
             ], 
             isResize: true
            });
            }else
            $.ligerDialog.warn('请先选择一条案件数据');
            }else
            $.ligerDialog.warn('列表绑定失败');
    }
    //删除
    function deldata() {
       
    }
    <%} %>
  

    function gridSetParm() {
        grid.setParm("key", $("#txt_key").val());
        grid.setParm("casename", $("#txt_name").val());
        grid.setParm("dutyman", $("#txt_dutyman").val());
        grid.setParm("relevance_la", $("#sct_relevance").val());
        grid.setParm("timebegin", $('#txt_time_begin').val());
        grid.setParm("timeend", $('#txt_time_end').val());
        grid.setParm("caseUnit", dwbm_tree.getValue()); //$('#caseUnit').val()
        grid.setParm("caseajlb", $('#caseajlb').val());
        grid.setParm("wsbh", $('#txt_searchws').val());
        grid.setParm("smr", $('#txt_smr').val());
        grid.setParm("smsj_bg", $('#txt_smsj_begin').val());
        grid.setParm("smsj_en", $('#txt_smsj_end').val());
        grid.setParm("xyr", $("#txt_xyr2").val());
        grid.setParm("smajla",1);
    }


    //检查客户端是否安装pdf阅读器软件
    function isAcrobatPluginInstall() {
        var flag = false;
        if (window.ActiveXObject) {
            try {
                var oAcro4 = new ActiveXObject("Yy Inspect Install");
                if (oAcro4 && oAcro4.InspectInsstall(fileMake) == "ok")
                    flag = true;
            } catch (e) {
                flag = false;
            }
        } else {
            flag = true;
        }
        if (flag) {
            return true;
        } else {
            $.ligerDialog.warn('对不起,请先安装电子卷宗客户端！');
        }
        return flag;
    }

    </script>
  
    <object id="objcab" align="CENTER" width="0" height="0" codebase="/images/YyInspectInstall.cab"
        classid="CLSID:C42B61DE-84B7-4323-B970-D23873E7691F">
    </object>
</body>
</html>
