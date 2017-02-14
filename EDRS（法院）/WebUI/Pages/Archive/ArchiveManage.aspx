<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchiveManage.aspx.cs"
    Inherits="WebUI.ArchiveManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>信息管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <style type="text/css">
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
        }
        #actvxhelp
        {
            background: url("/LigerUI/lib/LigerUI/skins/icons/help.gif") no-repeat left center;
            padding-left: 18px;
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
        #searchbar table
        {
            width: auto;
        }
        #searchbar table tr td
        {
            white-space: nowrap;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" >
        <table>
            <tr>
                <td style="display: none;">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    案件名称：
                    <% } %>
                </td>
                <td colspan="6" style="display: none;">
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 283px" />
                </td>
                <td style="padding-left: 10px;">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                    案号名称：
                    <% } %>
                </td>
                <td>
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 283px" />
                </td>
                <td>
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                    </div>
                    <div id="btn_lock" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td>
                    受理日期：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="padding-left: 10px;">
                    承办人：
                </td>
                <td>
                    <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" />
                </td>
                <td>
                    制作状态：
                </td>
                <td colspan="4">
                    <select id="sct_relevance" class="l-text" name="sct_relevance" style="width: 105px">
                        <option value="0">全部</option>
                        <option value="1">已制作</option>
                        <option value="2">未制作</option>
                    </select>
                </td>
                <td>
                    
                </td>
            </tr>--%>
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
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });
            $('#btn_lock').ligerButton({
                text: '解锁',
                icon: "../../images/NewAdd/js.png"
            });


//            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
//            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });

            grid = $("#mainGridAj").ligerGrid({
                columns: [
                 <% if (Ver_Advanced_Alone == "ADVANCED_ALONE"){ %>
                { display: '事项议题', name: 'JZMC', minWidth: 150 },
                 { display: '唯一编号', name: 'BMSAH', minWidth: 280,  },
               <% }else{ %>
                { display: '案由', name: 'JZMC', minWidth: 150 },
                 { display: '案号名称', name: 'BMSAH', minWidth: 280,  },
               <% } %>
//               { display: '案件编号', name: 'AJBH', minWidth: 150 },
//               { display: '文书编号', name: 'WSBH', minWidth: 150 },
//               { display: '文书名称', name: 'WSMC', minWidth: 150 },
               { display: '创建人', name: 'JZSCRXM', minWidth: 150 },
                { display: '创建时间', name: 'CJSJ', minWidth: 150, render: function (item) {
                    if (item.CJSJ == '1900-01-01 00:00:00' || item.CJSJ == null || item.CJSJ == "") return '';
                    else return item.CJSJ;
                } 
                }
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                usePager: true, width: '100%', height: '99%',       //服务器分页
                checkbox: true,
                url: '/Pages/Archive/ArchiveManage.aspx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: "ListBind", key: $("#txt_key").val(),
                 casename: $("#txt_name").val()
//                    dutyman: $("#txt_dutyman").val(),
//                    relevance: $("#sct_relevance").val(),
//                    timebegin: $('#txt_time_begin').val(),
//                    timeend: $('#txt_time_end').val()
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

            $("#pageloading").hide();
        });

        $(document).ready(function () {
            //制作
            $("#btn_lock").click(function () {
                var bmsahs = "";
                var cksld = grid.getSelecteds();
                for (var i = 0; i < cksld.length; i++) {
                    bmsahs += cksld[i].BMSAH+",";
                }

                if (bmsahs) {
                     $.ligerDialog.confirm('解锁后案件文件需重新上传，是否确定解锁？', function (yes)
                      {
                          if(yes)
                          {
                            $.ajax({
                                type: "POST",
                                url: '/Pages/Archive/ArchiveManage.aspx',
                                data: { t: "RomIsLock", bmsahs: bmsahs },
                                dataType: 'json',
                                timeout: 3000,
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
                                    grid.reload();
                                    $.ligerDialog.success(data.v);
                                }
                            });
                          }
                      });


                }
                else
                    $.ligerDialog.warn('请先选择一条数据进行制作');
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
//                        dutyman: $("#txt_dutyman").val(),
//                        relevance: $("#sct_relevance").val(),
//                        timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
//                        timeend: $('#txt_time_end').val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("casename", $("#txt_name").val());
//            grid.setParm("dutyman", $("#txt_dutyman").val());
//            grid.setParm("relevance", $("#sct_relevance").val());
//            grid.setParm("timebegin", $('#txt_time_begin').val());
//            grid.setParm("timeend", $('#txt_time_end').val());
        }
    </script>
    <object id="objcab" align="CENTER" width="0" height="0" codebase="/images/YyInspectInstall.cab"
        classid="CLSID:C42B61DE-84B7-4323-B970-D23873E7691F">
    </object>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
