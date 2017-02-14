<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullQuery.aspx.cs" Inherits="WebUI.FullQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件检索</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/ajaxPost.js" type="text/javascript"></script>
    <style type="text/css">
       /*右边框背景颜色*/
        body
        {
            background: #eef2f5;
            }
         .l-panel-bwarp {
        }
        
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #ccc;
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
        #mainGrid
        {
            margin-left: 100px;
        }
        /* 按钮 */
        div#btn_search {
            color: white;
            background: #ed6d4a;
        }
        div .l-button {
             top: -4px;
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
<body style="padding: 15px; overflow: hidden;">
    <%--搜索div--%>
    <div id="searchbar" style=" line-height: 28px; overflow-x: auto;">
        <table class="searchbartab" border="0">
            <tr>
                <td style="width: 80px; text-align: right;">
                       <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                  <%--  部门受案号：--%> 案件名称：
                    <% } %>
                </td>
                <td style="">
                 <%--   <input id="txt_key" class="l-text" type="text" name="txt_key" style="width: 283px;" />--%>
                 <input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 200px;" />
                </td>
                <td style="width: 80px; text-align: right; padding-left: 10px;">
                    <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                    检索内容：
                    <% } %>
                </td>
                <td colspan="4" style="">
                    <input id="txt_query" class="l-text" type="text" name="txt_query" style="width: 200px;" />
                </td>
                <%--<td style="padding-left: 10px; width: 80px; text-align: right;">
                   
                </td>--%>
               <%-- <td style="width: 127px;">
                   
                </td>

--%>
                 <%--<td style="width: 80px; text-align: right;">
                    扫描时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>--%>
                <%--<td style="width: 65px; text-align: right;">
                   承办人：
                </td>
                <td colspan="4">
                     <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" style="width: 283px;" />
                </td>--%>
                <td colspan="2">
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                      
                    </div>
                </td>
                 

            </tr>
           <%-- <tr>
                <td style="width: 80px; text-align: right;">
                    扫描时间：
                </td>
                <td>
                    <input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                </td>
                <td style="width: 65px; text-align: right;">
                   承办人：
                </td>
                <td colspan="4">
                     <input id="txt_dutyman" class="l-text" type="text" name="txt_dutyman" style="width: 283px;" />
                </td>
                <td colspan="2">
                    &nbsp;&nbsp;<div id="btn_search" style="margin-left: 10px; display: inline-block;
                        vertical-align: bottom;">
                      
                    </div>
                </td>
            </tr>--%>
        </table>
    </div>
    <div style="border-top: 1px solid #c4c4c4; height: 0px; padding-bottom: 10px; margin-top: 10px;">
    </div>
    <div style="margin-left: 100px; padding: 10px 0;">
        <div style="color: #c4c4c4;">
            找到 <span id="resultNum">0</span> 条结果（用时 <span id="qtime">0.000</span> 秒）
        </div>
        <%-- <div>
            <b style="font-size: 14px;">排序</b>&nbsp;&nbsp;&nbsp;&nbsp;<a id="inStock" href="javascript:">相关度</a>，<a
                id="price" href="javascript:">files</a></div>--%>
    </div>
    <div id="mainGrid">
        <div style="padding-top: 10px; width: 600px;">
            <div style="font-size: 14px;">
                <a href="javascript:">title</a></div>
            <div>
                content
            </div>
        </div>
    </div>
    <div id="page_div" class="page">
        <a id="btn_page1" href="javascript:">首页</a> &nbsp;&nbsp;<div id="count_div" style="display: inline-block;
            width: auto;">
            1
        </div>
        &nbsp;&nbsp; <a id="btn_page2" href="javascript:">下一页</a>&nbsp;&nbsp;<input style="width: 30px;
            border: 1px solid #aecaf0;" type="text" id="page_index" name="page_index" value="" />&nbsp;&nbsp;
        <a id="btn_page3" href="javascript:">跳转到</a>
        <input type="hidden" id="hidd_count" name="hidd_count" value="" />
    </div>
    <script type="text/javascript">

        //动态生成的用live绑定事件
        $(function () {
            $("#count_div").on("click", "span", function () {
                page_next(parseInt($(this).text()));
            });

            //首页
            $("#btn_page1").click(function () {
                var zcount = $("#count_div span").length;
                if (zcount < 1)
                    return false;
                search(1);
            });
            //下页
            $("#btn_page2").click(function () {
                var zcount = $("#count_div span").length;
                if (zcount <= 1)
                    return false;
                var val = parseInt($(".selected").text());
                page_next(val + 1);
            });
            //跳转页面
            $("#btn_page3").click(function () {
                var count = parseInt($("#hidd_count").val());
                var val = $("#page_index").val();
                if (parseInt(val) > 0 && parseInt(val) <= count) {
                    page_next(parseInt(val));
                }
            });
        });
        $(function () {
            var grid = $("#mainGrid").ligerGrid({
                width: '100%', height: '100%', heightDiff: -50, usePager: false
            });

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png',
                click: function () {
                    search();
                }
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
                    $.ligerDialog.warn('扫描开始时间不能大于结束时间');
                }
            }
            });
        });

        function search(rows, type) {
            if ($("#txt_query").val() == "" && $("#txt_key").val() == "" && $("#txt_name").val() == "" && $("#txt_time_end").val() == "") {
                $(".l-grid-body").html("<span style=\" font-size:14px;\" >请输入检索条件.<span>");
                return;
            }
            $.AjaxPost({
                url: "FullQuery.aspx",
                data: { t: "querydata", txt: $("#txt_query").val(), rows: rows, bmsah: $("#txt_key").val(), ajmc: $("#txt_name").val(), slrq1: $("#txt_time_begin").val(), slrq2: $("#txt_time_end").val() },
                success: function (data) {

                    if (data.response) {
                        //console.log(JSON.stringify(data));

                        var dataNew = data;
                        var count = data.response.numFound;
                        page_pag(count, type);
                        var datato = data.highlighting;
                        data = data.response.docs;
                        if (data == "" || count == 0) {
                            $(".l-grid-body").html("<span style=\" font-size:14px;\" >未找到和检索词相关的记录.<span>");
                            return;
                        }
                        $("#resultNum").text(count);
                        $("#qtime").text((parseInt(dataNew.responseHeader.QTime) / 1000));
                        // console.log(JSON.stringify(datato));
                        var html = "";
                        $.each(data, function (i, n) {
                            html += "<div style=\" padding-bottom:15px;width:600px;\"><div style=\"padding:5px 0; font-size:14px;\">";
                            html += "<a href=\"javascript:\" onclick=\"parent.f_addTab('" + n.id + "','" + n.WJXSMC + "', '/Pages/Business/FullQueryDetailed.aspx?id=" + n.id + "');\" >" + n.AJMC + "&nbsp;&nbsp;&nbsp;&nbsp;" + n.WJXSMC /*n.title[0]*/ + "</a></div>";
                            if (datato["" + n.id + ""].text)
                                html += "<div>" + datato["" + n.id + ""].text + "</div></div>";
                            else
                                html += "<div>" + n.WSCFLJ.toString().substring(0, 200) + "</div></div>";                            
                        });

                        $(".l-grid-body").html(html);
                        // console.log(JSON.stringify(data));
                    } else {
                        if (data.t) {
                            $(".l-grid-body").html("<span style=\" font-size:14px;\" >" + data.v + ".<span>");
                        } else {
                            $(".l-grid-body").html("<span style=\" font-size:14px;\" >未找到和检索词相关的记录.<span>");
                        }
                    }
                }
            });
        }

        function page_pag(count, type) {
            //是否重新绑定分页控件
            if (type != "page") {
                var spanNum = "";
                var zcount = parseInt(count) / 10;

                if ((parseInt(count) % 10) > 0)
                    zcount = Math.ceil(zcount);
              
                $("#hidd_count").val(zcount);

                for (var i = 0; i <= zcount; i++) {
                    if (i < 11 && i != zcount) {
                        if (i == 0)
                            spanNum += "<span class=\"selected\">" + (i + 1) + "</span>";
                        else
                            spanNum += "<span>" + (i + 1) + "</span>";
                    }
                    else if (i >= 11) {
                        spanNum += "......";
                        break;
                    }
                }
                if (zcount > 11)
                    spanNum += "<span>" + zcount + "</span>";
                $("#count_div").html(spanNum);
            }
        }

        function page_next(val) {
            var count = parseInt($("#hidd_count").val());
           
            //var val = parseInt($(this).text());
            var value = val - 5;
            if (value <= 0)
                value = 1;
            if (val <= count && val >= count - 5)
                value = count - 11;
            if (value > 0) {
                var spanNum = "";
                for (var i = 0; i < 11; i++) {
                    if ((value + i) == val)
                        spanNum += "<span class=\"selected\">" + (value + i) + "</span>";
                    else
                        spanNum += "<span>" + (value + i) + "</span>";
                }
                if (count > 11) {
                    if (val < count - 6)
                        spanNum += "......";
                    if (count == val)
                        spanNum += "<span class=\"selected\">" + count + "</span>";
                    else
                        spanNum += "<span>" + count + "</span>";
                }

                $("#count_div").html(spanNum);
            }
            search(val, "page");
        }
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
