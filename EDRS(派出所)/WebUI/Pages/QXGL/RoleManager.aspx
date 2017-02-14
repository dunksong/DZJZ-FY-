<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManager.aspx.cs" Inherits="WebUI.Pages.QXGL.RoleManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/common.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/defaultPage.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/tools/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/tools/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.easyui.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script type="text/javascript">
        var vn = '<%= ((VersionName)0).ToString() %>';

    </script>
    <script src="/Scripts/ZZJG/RoleManage.js" type="text/javascript"></script>
    <style type="text/css">
        .l-bar-message
        {
            display: none;
        }
        a#search1
        {
           color: white;
            border:none;
            background:rgb(181, 55, 183);
        }
        a#search2{
            color: white;
            border:none;
            background: rgb(73, 165, 166);
            }
              a#btn_LBAdd 
        {
            color: white;
            border:none;
            background: #60AEAD;
        }
        a#btn_LBDel 
        {
            color: white;
            border:none;
            background:#417C90;
        }
        
        
         a#A1 
            {
                color: white;
                border:none;
                background: #427979;
            }
            a#btn_dwDel 
            {
                color: white;
                border:none;
                background: #9D256D;
            }
           a#btn_dwAdd
           {
               color: white;
               border:none;
            background:#4F7D7B;
               }
           a#search4
           {
            color: white;
            border:none;
            background:rgb(86, 27, 182);
               }
        .icon-search,span.l-btn-icon.icon-search
        {
            background: url(../../images/cx.png) no-repeat;
            }
            .icon-add {
                background:url(../../images/add.png) no-repeat;
            }
            .icon-remove {
                background:url(../../images/sc.png) no-repeat;
            }
    </style>
    <%--<script src="/Scripts/deployTree.js" type="text/javascript"></script>--%>
</head>
<body id="searchbar">
    <form id="form1" runat="server">
    <input type="hidden" id="jsbm" value="" runat="server" />
    <input type="hidden" id="bmbm" value="" runat="server" />
    <input type="hidden" id="dwbm" value="" runat="server" />
    <input type="hidden" id="checkall" value="" />
    <div id="tt" class="easyui-tabs" style="width: 100%; height: 430px;">
        <div title='<%=((VersionName)0).ToString() %>权限' style="overflow: auto; padding: 10px;
            display: block;">
            <div style="float: left; width: auto;">
                <div style="height: 30px;">
                    <%=((VersionName)0).ToString() %>分类名称：<input type="text" id="type_name1" />
                    <a id="search1" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="search1()">查询</a>
                </div>
                <div id="List_AJLB">
                </div>
                <%--<table id="List_AJLB" class="easyui-datagrid" title="案件分类列表" style="height: 351px;
                    width: 360px;">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'AJLBBM',width:0">
                                案件分类编码
                            </th>
                            <th data-options="field:'AJLBMC',width:0">
                                案件分类名称
                            </th>
                        </tr>
                    </thead>
                </table>--%>
            </div>
            <div style="float: left; width: auto; padding-top: 120px; margin: 0px 30px;">
                <a id="btn_LBAdd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                    增加<%=((VersionName)0).ToString() %>权限</a>
                <p style="height: 20px; margin: 0px; padding: 0px">
                </p>
                <a id="btn_LBDel" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                    删除<%=((VersionName)0).ToString() %>权限</a>
            </div>
            <div style="float: left; width: auto;">
                <div style="height: 30px;">
                    <%=((VersionName)0).ToString() %>分类名称：<input type="text" id="type_name2" />
                    <a id="search2" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="search2()">查询</a>
                </div>
                <div id="List_AJLBQX">
                </div>
                <%--<table id="List_AJLBQX" class="easyui-datagrid" title="案件分类权限" style="height: 351px;
                    width: 360px;">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'QXBM',width:0">
                                案件分类编码
                            </th>
                            <th data-options="field:'QXMC',width:0">
                                案件分类名称
                            </th>
                        </tr>
                    </thead>
                </table>--%>
            </div>
        </div>
        <div title="单位权限" style="padding: 10px; display: block;">
            <div style="float: left; width: auto; margin-bottom: 20px;">
                <div style="height: 30px;">
                    单位名称：<input type="text" id="unit_name1" />
                    <a id="A1" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="search3()">查询</a>
                </div>
                <div id="DWpanel" class="easyui-panel" title="所有单位" style="padding: 10px; background: #fafafa;"
                    data-options="width:360">
                    <ul id="tree_DW">
                    </ul>
                    <%--<ul id="tree_left" class="easyui-tree" data-options="animate:true">
                    </ul>--%>
                </div>
            </div>
            <div style="float: left; text-align: center; width: auto; padding-top: 120px; margin: 0px 30px;">
                <a id="btn_dwAdd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                    增加单位权限</a>
                <p style="height: 20px; margin: 0px; padding: 0px">
                </p>
                <a id="btn_dwDel" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-delete'">
                    删除单位权限</a>
            </div>
            <div style="float: left; width: auto;">
                <div style="height: 30px;">
                    单位名称：<input type="text" id="unit_name2" />
                    <a id="search4" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="search4()">查询</a>
                </div>
                <div id="List_Dw">
                </div>
                <%--<table id="List_Dw" class="easyui-datagrid" title="已分配单位" style="height: 351px; padding: 0px;
                    width: 360px">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'QXBM',width:0">
                                单位编码
                            </th>
                            <th data-options="field:'QXMC',width:0">
                                单位名称
                            </th>
                        </tr>
                    </thead>
                </table>--%>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">

        var tree_dw = '/images/icons/3.png';
        var tree_bm = '/images/icons/bm.png';
        var tree_js = '/images/icons/4.png';

        $('#tt').tabs({
            border: false,
            onSelect: function (title, index) {
                if (index == 0) {
                    search1();
                    search2();
                }
                else {
                    search3();
                    search4();
                }
            }
        });

        $('#DWpanel').panel({
            width: 360,
            height: 345
        });

        $(document).ready(function () {
            $("#dwbm").val(GetQueryString("_dwbm"));
            $("#bmbm").val(GetQueryString("_bmbm"));
            $("#jsbm").val(GetQueryString("_jsbm"));
            //            setTimeout(function () {                
            //                $("#tree_left").deploy(true, false, true);
            //            }, 20);
        });

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }

    </script>
    </form>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
