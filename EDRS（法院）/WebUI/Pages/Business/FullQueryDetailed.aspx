<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullQueryDetailed.aspx.cs" Inherits="WebUI.FullQueryDetailed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件检索详细</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/ajaxPost.js" type="text/javascript"></script>
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
        .page
        {
            padding: 10px 10px 10px 100px;
        }
        .page span
        {
            width: auto;
            padding: 0 10px;
            display: inline-block;
            text-align: center;
            border: 1px solid #aecaf0;
            margin-left: 5px;
            cursor: pointer;
        }
        em
        {
            color: Red;
        }
        .selected
        {
            background-color: #aecaf0;
            color: White;
        }
        .l-grid-header
        {
            height: 0px !important;
            border: 0px !important;
        }
        
        .l-panel
        {
            border: 0px !important;
        }
        .table-d
        {
            border: 1px solid #a3c0e8;
        }
        .table-d td
        {
            border-bottom: 1px solid #a3c0e8;
        }
        .td-name
        {
            width: 80px;
            text-align: right;
            line-height: 34px;
            font-weight: bold;
            padding-right: 10px;
        }
        #td-content
        {
            padding: 10px 50px 10px 0px;
        }
    </style>
</head>
<body style="padding: 6px; overflow: hidden;">
    <div>
        <table class="table-d" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td class="td-name">                
                     <%= ((VersionCord)0).ToString()%>：
                </td>
                <td id="td_bmsah">
                </td>
            </tr>
            <tr>
                <td class="td-name">
                    <%= ((VersionName)0).ToString() %>名称：
                </td>
                <td id="td_ajmc">
                </td>
            </tr>
            <tr>
                <td class="td-name">
                    承办人：
                </td>
                <td id="td_cbr">
                </td>
            </tr>
            <tr>
                <td class="td-name">
                    受理日期：
                </td>
                <td id="td_slrq">
                </td>
            </tr>
            <tr>
                <td class="td-name">
                    文件名称：
                </td>
                <td id="td_wjmc">
                </td>
            </tr>
            <tr>
                <td class="td-name" style="vertical-align: top;">
                    内容：
                </td>
                <td id="td-content">
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        //动态生成的用live绑定事件
        $(function () {
            search();
        });
        function search() {
            var v = parent.GetQueryString(window.location.search.substr(1), "id");
            if (v != "" && v != null) {
                $.AjaxPost({
                    url: "FullQuery.aspx",
                    data: { t: "querydata", id: v },
                    success: function (data) {

                        if (data.response) {
                            //console.log(JSON.stringify(data));

                            var dataNew = data;
                            var count = data.response.numFound;
                            var datato = data.highlighting;
                            data = data.response.docs;
                            if (data == "" || count == 0) {
                                $("#td-content").html("<span style=\" font-size:14px;\" >未找到和检索词相关的记录.<span>");
                                return;
                            }
                            $.each(data, function (i, n) {
                                $("#td_bmsah").text(n.BMSAH);
                                $("#td_ajmc").text(n.AJMC);
                                $("#td_slrq").text(n.SLRQ);
                                $("#td_cbr").text(n.CBR);
                                $("#td_wjmc").text(n.WJMCDZJZ + n.WJHZ);
                                $("#td-content").text(n.WSCFLJ);
                            });
                        } else {
                            if (data.t) {
                                $("#td-content").html("<span style=\" font-size:14px;\" >" + data.v + ".<span>");
                            } else {
                                $("#td-content").html("<span style=\" font-size:14px;\" >未找到和检索词相关的记录.<span>");
                            }
                        }
                    }
                });
            }
        }

    </script>
</body>
</html>
