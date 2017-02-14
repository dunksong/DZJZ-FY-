<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkNumberStatistics.aspx.cs" Inherits="WebUI.Pages.Statistics.WorkNumberStatistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>信息管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
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
    </style>
</head>
<body style="padding: 6px; overflow: hidden;">
    
    <div id="searchbar" style="padding-bottom: 5px; line-height: 28px;">
        <table>
            <tr>
                <td>
                    单位：
                </td>
                <td style="">
                    <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 150px" />
                </td>
                <td style="padding-left: 10px;">
                    制作人：
                </td>
                <td colspan="6">
                    <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 150px" />
                </td>        
                <td>
                    创建时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="padding-left: 10px;">
                      &nbsp;&nbsp;<input id="btn_search" type="button" class="l-button" value="搜 索" />
                    <input id="btn_derive" type="button" class="l-button" value="导出Excle" />
                </td>
             
            </tr>
        </table>
    </div>
    
    <div id="mainGridAj" style="margin: 0; padding: 0">
    </div>
    <div style="display: none;">
        <!-- g data total ttt -->
    </div>
    
    <div id="help" title="插件安装帮助"  style="padding: 10px; display: none;">
        <form id="add_formHelp">
        <div>
            1.打开IE浏览器选择【工具】>【Internet选项】如下图：
        </div>
        <div style="text-align: center;">
            <img style="width: 60%;" src="/images/help/1.jpg" />
        </div>
        <br />
        <div>
            2.在Internet选项窗口中选择【安全】>选中【Internet】>点击【自定义级别】如下图
        </div>
        <div style="text-align: center;">
            <img style="width: 60%;" src="/images/help/2.jpg" />
        </div>
        <br />
        <div>
            3.弹出安全设置-Internet区域，将滚动条拖动到【ActiveX控件和插件】区域，找到如下图两个位置分别选中【提示】点击【确认】弹出提示点击【是】即可。如下图：
        </div>
        <div style="text-align: center;">
            <img style="width: 60%;" src="/images/help/3.jpg" />
        </div>
        <br />
        <div>
            4.最后后面均点击确定即可，【F5】刷新页面浏览器顶部出现如下图提示，在提示上右键弹出的选项上点击【为此计算机上的所有用户安装此加载项】，后面一路确认即可。
        </div>
        <div style="text-align: center;">
            <img style="width: 70%;" src="/images/help/4.jpg" />
        </div>
        <br />
        <div>
            5.刷新下页面便可进行制作操作。
        </div>
        <br />
        </form>
    </div>
    <script type="text/javascript">

        var grid = null;
        var vn = '<%= ((VersionName)0).ToString() %>';
        $(function () {

            $("#txt_time_begin").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });

            grid = $("#mainGridAj").ligerGrid({
                columns: [
                { display: '制作状态（只查已制作数据）', name: 'ISREGARD', align: 'center', width: 80,
                    render: function (item) {
                        if (item.ISREGARD) {
                            if (parseInt(item.ISREGARD) > 0) return '<span style="color:red;">已制作</span>';
                        } else if (item.SFZZ) {
                            if (item.SFZZ == "True") return '<span style="color:red;">已制作</span>';
                        }
                        return '未制作';
                    }
                },
                <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                            { display: '事项议题', name: 'AJMC', minWidth: 150 },
                            { display: '唯一编号', name: 'BMSAH', minWidth: 280,  },
                    <% }
                       else
                       { %>
                            { display: '案件名称', name: 'AJMC', minWidth: 150 },
                            { display: '部门受案号', name: 'BMSAH', minWidth: 280,  },
                    <% } %>
                
                
                { display: vn+ '类别名称', name: 'AJLB_MC', minWidth: 200 },
                { display: '承办单位', name: 'AJ_DWMC', minWidth: 120,
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
                { display: vn+ '状态', name: 'AJZT', width: 70, render: function (item) {
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
                ], fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'server', //服务器排序
                usePager: true, width: '100%', height: '100%',       //服务器分页
                url: '/Pages/Business/CaseInfoManage.aspx',
                pageSizeOptions: [20, 50, 100, 500],
                parms: { t: "ListBind", key: $("#txt_key").val()
                , casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(),
                    timeend: $('#txt_time_end').val()
                }, onAfterShowData: function (currentData) {   //onAfterShowData

                }
            });
            //grid.setColumnWidth(2, 110);

            $("#pageloading").hide();
        });

        $(document).ready(function () {
            //制作
            $("#btn_make,#btn_derive").click(function () {
                var btnid = $(this).attr("id");
                var cksld = grid.getSelected();
                if (cksld) {
                    $.ajax({
                        type: "POST",
                        url: '/Pages/Business/CaseInfoManage.aspx',
                        data: { t: "GetMake", id: cksld.BMSAH, type: btnid },
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
                            var parm = "RecEFileMaker://" + data.parm;
                            if (isAcrobatPluginInstall()) {
                                //判断是否为自己定义浏览器
                                if (typeof (boundObjectForJS) != 'undefined')
                                    boundObjectForJS.callCsharp("RecEFileMaker://1234567812345678" + data.parm + "@");
                                else
                                    location.href = parm;
                            }
                        }
                    });

                }
                else
                    $.ligerDialog.warn('请先选择一个'+vn);
            });
            //检查客户端是否安装pdf阅读器软件
            function isAcrobatPluginInstall() {
                var flag = false;
                if (window.ActiveXObject) {
                    try {
                        var oAcro4 = new ActiveXObject("Yy Inspect Install");
                        if (oAcro4 && oAcro4.InspectInsstall("RecEFileMaker") == "ok")
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

            //点击搜索按钮
            $("#btn_search").click(function () {
                grid.loadServerData({
                    t: "ListBind",
                    key: $("#txt_key").val(),
                    casename: $("#txt_name").val(),
                    dutyman: $("#txt_dutyman").val(),
                    relevance: $("#sct_relevance").val(),
                    timebegin: $('#txt_time_begin').val(), // 获取日期输入框的值,
                    timeend: $('#txt_time_end').val(),
                    page: 1, pagesize: grid.options.pageSize
                });

                grid.changePage("first"); //重置到第一页
            });

        });
    </script>
    <object id="objcab" align="CENTER" width="0" height="0" codebase="/images/YyInspectInstall.cab"
        classid="CLSID:C42B61DE-84B7-4323-B970-D23873E7691F">
    </object>
</body>
</html>

