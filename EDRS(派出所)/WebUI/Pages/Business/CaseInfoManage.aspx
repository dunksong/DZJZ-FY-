<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseInfoManage.aspx.cs"
    Inherits="WebUI.CaseInfoManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Access-Control-Allow-Origin" content="*" />
    <title>信息管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
    <style type="text/css">
        body{
        
        background: #eef2f5;
    }
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
        /* 内容区 */
       div#searchbar {
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius:10px;
            background:white;
            line-height: 30px;

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
        /*  按钮 */
        .l-button {
            height: 30px;
            line-height: 30px;
            color: white;
        }
        
        
    /*    div#btn_search {
            background: #ed6d4a;
        }
        div #btn_make {
            background: #4bacc6;
        }
        div#btn_add {
            background: #87458b;
        }
        div#btn_update {
            background: #80b75d;
        }
        div#btn_yjdc {
            background: #92afb7;
        }
        div#btn_pdfdc {
            background: #9e4a29;
        }
        div#btn_sdbs {
            background: #618bc9;
        }

        */
        
        div#btn_loadData {
            background: #1b7d0a;
        }
        div#btn_loadData img {
            top: 3px;
        }
       
        .l-button {
            height: 25px;
            border-radius: 6px;
            line-height: 25px;
            background: #3D9991;
        }
    </style>
</head>
<body style="padding:15px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style="padding:10px;/* line-height: 28px; */ overflow-x: auto;">
        <table class="searchbartab" border="0">
            <tr>
                <td style="width: 80px; text-align: right;">
                    立卷单位：
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
                    案件编号：
                </td>
                <td style="">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 197px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    文书编号：
                </td>
                <td>
                    <input id="txt_searchws" class="l-text" type="text" name="txt_key" style="width: 160px;" />
                </td>
                <td style="width: 80px; text-align: right; padding-left: 10px;">
                    案件名称：
                </td>
                <td>
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 160px;" />
                </td>
                <td style="width: 52px; text-align: right;">
                    扫描人：
                </td>
                <td style="width: 150px;">
                    <input id="txt_smr" class="l-text" type="text" name="txt_name" style="width: 160px;" />
                </td>
                
                
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right;">
                    立案时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" style="width: 80px;" />
                </td>
                <td style="width: 80px; text-align: right;">
                    扫描时间：
                </td>
                <td>
                    <input id="txt_smsj_begin" type="text" name="txt_smsj_begin" style="width: 80px;" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_smsj_end" type="text" name="txt_smsj_end" style="width: 80px;" />
                </td>
                <td style="padding-left: 10px; width: 80px; text-align: right;">
                    嫌疑人：
                </td>
                <td style="width: 127px;">
                    <input id="txt_xyr2" class="l-text" type="text" name="txt_xyr2" style="width: 160px;" />
                </td>
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
                <td style="padding-left: 10px; width: 52px; text-align: right;">
                    立卷人：
                </td>
                <td style="">
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" style="width: 160px;" />
                </td>
                
            </tr>
            <tr>
                <td colspan="10">
                        &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                            vertical-align: bottom;">
                        </div>
                        <div id="btn_make" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <%--添加案件--%>
                        <div id="btn_add" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <div id="btn_update" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <%--<div id="btn_derive" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>--%>
                        <% if (IsAnRole("caseinfomanage_yjdc"))
                           { %>
                        <div id="btn_yjdc" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <%} %>
                        <% if (IsAnRole("caseinfomanage_pdfdc"))
                           { %>
                        <div id="btn_pdfdc" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <%} %>
                        <%-- <div id="btn_menu" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <div id="btn_print" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>--%>
                        <div id="btn_sdbs" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
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
    <%--初始化案件--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 5px 10px 5px 10px">
            <form id="add_form">
            <input type="hidden" id="hidd_bmsah" name="hidd_bmsah" value="" />
            <table border="0" cellpadding="0" cellspacing="0" style="line-height: 30px;">
                <tr>
                    <td class="text_rigth">
                        <%--身份证号：--%>
                        案件编号：
                    </td>
                    <td>
                        <%--<input type="text" id="txt_sfzh" name="txt_sfzh" class="l-text" maxlength="18" style="width: 170px;"
                            value="" />--%>
                        <input type="text" id="txt_ajbh" name="txt_ajbh" class="l-text" style="width: 170px;"
                            value="" />
                    </td>
                    <td>
                        <div id="btn_loadData" style="margin-left: 10px; display: inline-block; vertical-align: bottom;width: 54px; height: 22px;line-height: 22px;">
                        </div>
                    </td>
                    <td align="right">
                        文书编号：
                    </td>
                    <td>
                        <%--<input type="hidden" id="wsbh_hidd" name="wsbh_hidd" value="" />--%>
                        <input type="text" id="txt_wsbh" name="txt_wsbh" class="l-text" style="width: 170px;"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="text_rigth">
                        案件名称：
                    </td>
                    <td colspan="2">
                        <input type="text" id="txt_ajmc" name="txt_ajmc" class="l-text" style="width: 250px;"
                            value="" />
                    </td>
                    <td class="text_rigth" style="padding-left: 10px;">
                        <% Response.Write(((VersionName)0).ToString() + "类别："); %>
                    </td>
                    <td>
                        <input type="text" id="txt_lbmc" class="l-text"  name="txt_lbmc" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="text_rigth" style="padding-left: 10px;">
                        嫌疑人姓名：
                    </td>
                    <td colspan="4">
                        <textarea id="txt_xyr" name="txt_xyr" class="l-text" style="width: 494px; height: 80px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="text_rigth">
                        立案时间：
                    </td>
                    <td colspan="4">
                        <input type="text" id="txt_lasj" name="txt_lasj" class="l-text" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="text_rigth">
                        立卷单位：
                    </td>
                    <td>
                        <input type="text" id="txt_ladw" name="txt_ladw" class="l-text" style="width: 250px;"
                            value="" />
                    </td>
                    <td>
                    </td>
                    <td class="text_rigth" style="padding-left: 10px;">
                        立卷人：
                    </td>
                    <td>
                        <input type="text" id="txt_lar" name="txt_lar" class="l-text" style="width: 170px;"
                            value="" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        document.getElementById("txt_ajbh").onkeydown=function mykeyDown(e){             
           e = e||event;  
           if(e.keyCode == 13) {
            getAJXX();
           }   
           return;  
        }

        var grid = null;
        var ajlbBox;
        var ajmbBox;
        var dwbm_tree;
        var ljdw_tree;
        var rybm_Box;
        var lasj_time;
        var jasj_time;
         var wsbhBox;
        var fileMake="RecEFileMaker";
        <% if (Version == "PSB") {%> 
            fileMake="CopEFileMaker";
        <%} %>
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
            //btn_loadData
            
            $('#btn_loadData').ligerButton({
                text: '获取',
                width:57,
                icon: '../../images/cx.png',
                click:getAJXX
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
            
            $("#btn_add").ligerButton({
                text: "添加"+vn,
                icon: '../../images/add.png',
                click:adddata,
                width:80
            });
            $("#btn_update").ligerButton({
                text: "修改"+vn,
                icon: '../../images/xg.png',
                click:updata,
                width:80
            });
            
             $('#btn_menu').ligerButton({
                text: '编辑目录', //编辑目录
                icon: "/images/icons/bm.png",
                width:80
            });
            $('#btn_print').ligerButton({
                text: '目录打印',//目录打印
                icon: "/images/icons/preview_16x16.png",
                width:80
            });
             $('#btn_yjdc').ligerButton({
                text: '原件导出', //原件导出
                icon: "../../images/yjdc.png",
                width:80
            });
            $('#btn_pdfdc').ligerButton({
                text: 'PDF导出',//PDF导出
                icon: "../../images/pdf.png",
                width:80
            });
            $('#btn_sdbs').ligerButton({
                text: '手动报送',//手动报送
                icon: "../../images/sdfx.png",
                width:80
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
                    $.ligerDialog.warn('开始日期不能大于结束日期');
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
                    $.ligerDialog.warn('开始日期不能大于结束日期');
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

            
                 { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '案件编号', name: 'AJBH', minWidth: 200 },
                { display: '文书编号', name: 'WSBH', minWidth: 250 },
                { display: '文书名称', name: 'WSMC', minWidth: 100 },         
                
                { display: vn+'类别名称', name: 'AJLB_MC', minWidth: 200 },
                 
                {display: '犯罪嫌疑人姓名', name: 'XYR', minWidth: 100 },
       
                
                { display: '立案时间', name: 'SLRQ', minWidth: 150, render: function (item) {
                    if (item.SLRQ == '1900-01-01 00:00:00' || item.SLRQ == null || item.SLRQ == "") return '';
                    else return item.SLRQ.substring(0,10);
                }
                },
                {display: '立卷单位', name: 'AJ_DWMC', minWidth: 120,
                render: function (item) {
                    if (item.CBDW_MC)
                        return item.CBDW_MC;
                    else return item.AJ_DWMC;
                }
                },
                { display: '立卷人', name: 'CBR', minWidth: 100 },
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
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
            usePager: true, width: '100%', height: '100%', enabledSort: false,       //服务器分页
            url: '/Pages/Business/CaseInfoManage.aspx',
            pageSizeOptions: [20, 50, 100, 500],
            parms: { t: "ListBind", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                dutyman: $("#txt_dutyman").val(),
                relevance: $("#sct_relevance").val(),
                timebegin: $('#txt_time_begin').val(),
                timeend: $('#txt_time_end').val(),
                smr:$('#txt_smr').val(),
                smsj_bg:$('#txt_smsj_begin').val(),
                smsj_en:$('#txt_smsj_end').val(),
                caseUnit:dwbm_tree?dwbm_tree.getValue():"",
                caseajlb:$('#caseajlb').val(),
                wsbh:$("#txt_searchws").val(),
                xyr:$("#txt_xyr2").val()
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

       

         // 绑定搜索单位
            dwbm_tree = $("#txt_dwbm").unitJuris({width:197,checkbox:true});
           ljdw_tree = $("#txt_ladw").unitJuris({width:170,valueFieldID: 'txt_ladw_val'});

    });

    $(document).ready(function () {
        //制作
        $("#btn_make,#btn_derive,#btn_menu,#btn_print,#btn_sdbs,#btn_yjdc,#btn_pdfdc").click(function () {
             
            var faceType=0;
            switch (this.id) {
                case "btn_make":
                faceType=0;
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
                case "btn_sdbs":
                faceType=7;
                break;
                 case "btn_yjdc":
                faceType=5;
                break;
                case "btn_pdfdc":
                faceType=8;
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
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                    timeend: $('#txt_time_end').val(),
                    smr:$('#txt_smr').val(),
                    smsj_bg:$('#txt_smsj_begin').val(),
                    smsj_en:$('#txt_smsj_end').val(),
                    caseUnit:  dwbm_tree.getValue(),//$('#caseUnit').val(),
                    caseajlb:$('#caseajlb').val(),
                    wsbh:$("#txt_searchws").val(),
                    xyr:$("#txt_xyr2").val(),
                    page: 1, pagesize: grid.options.pageSize
                });
            }
        });     
    });


    function loadBox()
    {         
         var dwbm = '<%=UserInfo.DWBM %>';
         var dwmc = '<%=UserInfo.DWMC %>';
         //立卷单位
         if(!ljdw_tree){
            $("#txt_ladw").val(""); 
            ljdw_tree = $("#txt_ladw").unitJuris({width:170,valueFieldID: 'txt_ladw_val',defaultValue:dwbm});           
           // ljdw_tree.setValue(dwbm);
         }else
         {
            ljdw_tree.setValue(dwbm);            
         }
    }


    function submitForm(dataType) {
        var rg=/^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/;
        
        if($("#txt_ajbh").val()==null || $("#txt_ajbh").val() == "" )
        {
            $.ligerDialog.error("必须填写案件编号！");
            return false;
        }
        if($("#txt_ajmc").val()==null || $("#txt_ajmc").val() == "" )
        {
            $.ligerDialog.error("必须填写案件名称！");
            return false;
        }
        if(!ajlbBox.getValue() || ajlbBox.getValue() == "")
        {
            $.ligerDialog.error("必须填写案件类别！");
            return false;
        }
        if($("#txt_lasj").val()==null || $("#txt_lasj").val() == "")
        {
            $.ligerDialog.error("必须选择立案时间！");
            return false;
        }
        if($("#txt_ladw").val()==null || $("#txt_ladw").val() == "")
        {
            $.ligerDialog.error("必须选择立卷单位！");
            return false;
        }
        
        var jdata = $('#add_form').serializeArray();
        if(oldAJBH != "" && oldAJBH != $("#txt_ajbh").val())
        {
            $.ligerDialog.error("文书编号已更改，请重新获取数据！");
            return false;
        }
        jdata[jdata.length] = { name: "t", value: dataType};
      
        if (jdata.length > 1) {
          
            jdata[jdata.length] = { name: "txt_ajlb", value: ajlbBox.getValue() };
            
            $.ajax({
                type: "POST",
                url: "/Pages/Business/CaseInfoManage.aspx",
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
                        $("#add_form")[0].reset();
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

    //添加
    function adddata() {
      var d = new Date();
       
            $("#txt_ajbh").ligerTextBox().setEnabled();
            lasj_time.setValue( d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate());
            getlb();
         //   getWSBHLB();
            loadBox();
            
            $.ligerDialog.open({ title: '添加'+vn, target: $('#add_div'), width: 650,
            buttons: [{ text: '确定', onclick: function (item, dialog) {
                submitForm("AddData");
            }, cls: 'l-dialog-btn-highlight'
            },
            { text: '取消', onclick: function (item, dialog) {
            $("#add_form")[0].reset();
            dialog.hidden();
            }
            }], isResize: true
            });
        
    }
    //修改
    function updata() {

        var cksld = grid.getSelected();
        if (cksld) {
            getlb();
//            getWSBHLB();
            $.ajax({
                type: "POST",
                url: '/Pages/Business/CaseInfoManage.aspx',
                data: { t: "GetData", id: cksld.BMSAH },
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
                    else if(data.ZZZT == 4 || data.ZZZT == 5)
                    {
                        $.ligerDialog.error('审核通过或已报送的案件不能再修改！');
                        return false;
                    }
                    $("#txt_ajbh").val(data.AJBH);
                    //初始化文书列表
                    getWSBHLB("updata");

                    loadBox();

                    $("#hidd_bmsah").val(data.BMSAH);
                    $("#txt_ajmc").val(data.AJMC);                    
                    $("#txt_sfzh").val(data.SFZH);
                    $("#txt_taryxx").val(data.TARYXX);
                    
                    $("#txt_lar").val(data.CBR);
                    $("#txt_ajbh").ligerTextBox().setDisabled();

                    if(data.SLRQ)
                        lasj_time.setValue(data.SLRQ.substring(0,10));
                    //ajlbBox.setValue(data.AJLB_BM);
                 
                    //alert(data.WSBH);
                    
                    
                    oldWSBH = data.WSBH
                    ljdw_tree.setValue(data.CBDW_BM);
                    $("#txt_xyr").val(data.XYR);
                    $("#txt_shr").val(data.SHR);
                    $("#txt_zjs").val(data.ZJS);
                    $("#txt_djj").val(data.DJJ);
                    $("#txt_djy").val(data.ZYS);
                    $("#txt_lbmc").ligerComboBox().setValue(data.AJLB_BM);//
                    
                    wsbhBox.selectValue(data.WSBH);
                    wsbhBox.setValue(data.WSBH);
                    wsbhBox.setText(data.WSMC);               
                    $("#wsbh_hidd").val(data.WSBH);
                    $("#txt_wsbh").val(data.WSMC);
                    $.ligerDialog.open({ title: '修改'+vn, target: $('#add_div'), width: 650,
                        buttons: [{ text: '确定', onclick: function (item, dialog) {
                            submitForm("UpData");
                        }, cls: 'l-dialog-btn-highlight'
                        },
                            { text: '取消', onclick: function (item, dialog) {
                                $("#add_form")[0].reset();
                                dialog.hidden();
                            }
                            }], isResize: true
                    });
                }
            });
        } else
            $.ligerDialog.error("请选择修改项");
    }
    //删除
    function deldata() {
        var cksld = grid.getSelected();
        if (cksld) {
            $.ligerDialog.confirm('确定是否删除?', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: '/Pages/Business/CaseInfoManage.aspx',
                        data: { t: "DelData", id: cksld.BMSAH },
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
                            $.ligerDialog.success(data.v);
                            grid.reload();
                        }
                    });
                }
            });
        } else
            $.ligerDialog.error("请选择删除"+vn);
    }
  
    var oldAJBH = "";
    var oldWSBH = "";
    function getAJXX()
    {
        var _ajbh = $("#txt_ajbh").val();//"A4401115400002016050012";
        if(_ajbh.length == 0)
        {
                $.ligerDialog.error('请输入案件编号！');
                return false;
        }
       var manager = $.ligerDialog.waitting('正在获取卷宗基本信息，请稍后...');
        getWSBHLB();
     
        $.ajax({
            type: "POST",
            url: '/Pages/Business/GetService.aspx',
            data: { ajbh: _ajbh,type:"aj"},//interfaceType:0制作，1编辑目录，2打印目录
            datatype: 'json',
            timeout: 10000,
            cache: false,
            beforeSend: function () {                
            },
            error: function (xhr) {
                 manager.close();
                $.ligerDialog.error('网络连接错误');
                return false;
            },
            success: function (data) {              
                 manager.close();
                var interfaceData = JSON.parse(data);

                 if (interfaceData.t && interfaceData.t == "error") {
                    $.ligerDialog.error(interfaceData.v);
                    return false;
                }

                if (interfaceData.success== 0) {
                    $.ligerDialog.error(interfaceData.EXCEPTION.message);
                    return false;
                }
                oldAJBH = interfaceData.DATA.AJBH;
                $("#txt_ajmc").val(interfaceData.DATA.AJMC);
               // $("#txt_xyr").val(interfaceData.DATA.AJMC);
                $("#txt_lbmc").ligerComboBox().setValue(interfaceData.DATA.AB);//
                $("#txt_lasj").val(interfaceData.DATA.LASJ);
                if(interfaceData.DATA.LADW != null && interfaceData.DATA.LADW != "" && interfaceData.DATA.LADW != undefined)
                    ljdw_tree.setValue(interfaceData.DATA.LADW);//
                else{
                    var dwbm = '<%=UserInfo.DWBM %>';
                    ljdw_tree.setValue(dwbm);
                }
                $("#txt_lar").val(interfaceData.DATA.AJLARY);

                 
            }
        });
       
    }
   
    function getWSBHLB(lbtype)
    {      
        
        $("#wsbh_hidd").val("");
        $("#txt_wsbh").val("");
        if(wsbhBox){         
            wsbhBox.setParm("ajbh", $("#txt_ajbh").val());
            wsbhBox.setParm("type", "ws");
            $("input [name='wRITID']").val("");
            wsbhBox.reload();
        }else{
        //var _ajbh = $("#txt_ajbh").val();//"A4401115400002016050012";
         wsbhBox = $("#txt_wsbh").ligerComboBox({
            width: 170,
            slide: false,
            selectBoxWidth: 480,
            selectBoxHeight: 230,
            valueFieldID :"wsbh_hidd",
            valueField: 'wRITID',
            textField: 'wSMC',
            parms: { ajbh: $("#txt_ajbh").val(),type:"ws" },                   
            grid: {      
                url: "/Pages/Business/GetService.aspx",
                parms: { ajbh: $("#txt_ajbh").val(),type:"ws" },  
                columns: [
                { display: '文书编号', name: 'wRITID', minWidth: 240 },
                { display: '文书名称', name: 'wSMC', minWidth: 100 },
                { display: '嫌疑人', name: 'zXDXJY', width: 100}
                ], switchPageSizeApplyComboBox: false,
                usePager:false,
                onReload: function (grid) {                  
                    grid.setParm("ajbh", $("#txt_ajbh").val());
                    grid.setParm("type", "ws");
                }
            },
            onSuccess :function(data){                
                  if (data.t) {
                    $.ligerDialog.warn(data.v);
                }
//                else {                    
//                   //  console.log(data);     
//                }
            },
            conditionSearchClick: function (e)
            { 
                e.grid.set('ajbh', { userName: 'wRITID' });
                e.grid.reload();
            },
            onSelected: function (value) {
                if(lbtype != "updata"){
                    if (value) {
                        var d = this.getSelected();
                        $("#wsbh_hidd").val(value);
                        if(d)
                        {
                            //嫌疑人姓名                       
                            $("#txt_xyr").val(d.zXDXJY); 
                        }
                    } 
                    else
                    {
                        $("#txt_xyr").val('');
                        $("#wsbh_hidd").val("");
                    }
                }
            }
            ,condition: { fields: [{ name: 'wRITID', label: '关键字', width: 200, type: 'text'}] }
        });
        }
    }
 

    function getlb() {
        //案件类别
        ajlbBox = $("#txt_lbmc").ligerComboBox({
            url: '/Handler/ZZJG/DZJZ_Report.ashx?haseRole=false',
            parms: { action: "GetAjlxList" },
            valueFieldID: 'ajlbbm_hidd',
            width: 170,
            selectBoxWidth: 400,
            selectBoxHeight: 300,
            autocomplete: true,
            highLight: true
        });
    }

    function getbm(ajlbbm){
        //案件模板
        ajmbBox = $("#txt_bmmc").ligerComboBox({
            url: '/Pages/Business/CaseInfoManage.aspx',
            parms: { t: "getajmb",ajlb:ajlbbm },
            valueFieldID: 'id',
            width: 170,
            selectBoxWidth: 400,
            selectBoxHeight: 300,
            autocomplete: true,
            highLight: true         
        });
    }

    //显示同案人员信息
    function showTaryxx(obj) {
        var tit = "";
         <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
        { %>
        tit="备注";
        <%}
        else
        { %>
        tit="同案人员信息";
        <%} %>

        $.ligerDialog.open({ title: tit, height: 200, width: 650, content: obj });
    }

    function gridSetParm() {
        grid.setParm("key", $("#txt_key").val());
        grid.setParm("casename", $("#txt_name").val());
        grid.setParm("dutyman", $("#txt_dutyman").val());
        grid.setParm("relevance", $("#sct_relevance").val());
        grid.setParm("timebegin", $('#txt_time_begin').val());
        grid.setParm("timeend", $('#txt_time_end').val());
        grid.setParm("caseUnit", dwbm_tree.getValue()); //$('#caseUnit').val()
        grid.setParm("caseajlb", $('#caseajlb').val());
        grid.setParm("wsbh", $('#txt_searchws').val());
        grid.setParm("smr", $('#txt_smr').val());
        grid.setParm("smsj_bg", $('#txt_smsj_begin').val());
        grid.setParm("smsj_en", $('#txt_smsj_end').val());
        grid.setParm("xyr", $("#txt_xyr2").val());
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
    <script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
