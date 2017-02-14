<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintFrontCover1.aspx.cs"
    Inherits="WebUI.Pages.Print.PrintFrontCover1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>封面打印</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>

    <script src="/Scripts/jquery.PrintArea.js" type="text/javascript"></script>

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
        .l-text, .l-textarea
        {
            width: 350px;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
    </style>
    <style type="text/css">
        .print_table
        {
            width: 100%;
            font-family: 宋体;
        }
        .print_table td
        {
            line-height: 40px;
        }
    </style>
</head>
<body style="padding: 0px; overflow: hidden;">
    <div id="tb" style="background-color: #f8f8f8">
        <div style="padding: 4px 5px;">
            名称：<input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
            <div id="btn_print" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                onclick="btn_print()">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 20px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        标题：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input id="txt_bt" type="text" name="txt_bt"  class="l-text" />
                        <%--<input class="l-text" id="slct_type" type="text" name="slct_type" maxlength="200" />--%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        副标题：
                    </td>
                    <td>
                        <input id="txt_fbt" type="text" name="txt_fbt"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        案件名称：
                    </td>
                    <td>
                        <input id="txt_ajmc" type="text" name="txt_ajmc"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        案件编号：
                    </td>
                    <td>
                        <input id="txt_ajbh" type="text" name="txt_ajbh"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        犯罪嫌疑人姓名：
                    </td>
                    <td>
                        <input id="txt_fzxyr" type="text" name="txt_fzxyr"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        立案时间：
                    </td>
                    <td>
                        <input id="txt_lasj" type="text" name="txt_lasj"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        结案时间：
                    </td>
                    <td>
                        <input id="txt_jasj" type="text" name="txt_jasj"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        立卷单位：
                    </td>
                    <td>
                        <input id="txt_ljdw" type="text" name="txt_ljdw"  class="l-text"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        立卷人：
                    </td>
                    <td>
                        <input id="txt_ljr" type="text" name="txt_ljr"   class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核人：
                    </td>
                    <td>
                        <input id="txt_shr" type="text" name="txt_shr"   class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        本案共：
                    </td>
                    <td>
                        <input id="txt_bagj" type="text" name="txt_bagj"  class="l-text"  />卷
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        第：
                    </td>
                    <td>
                        <input id="txt_djj" type="text" name="txt_djj" style="width: 80px;" class="l-text"  />卷 共
                        <input id="txt_gjy" type="text" name="txt_gjy" style="width: 80px;" class="l-text"  />页
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
        
    <div id="print" style=" display:none;">
        <!--startprint-->
        <div style="text-align: center; padding-top:80px; font-family: 宋体; font-weight: bold; font-size: 50px;">
            <span id="span_bt">标题</span></div>
        <div style="text-align: center; font-family: 宋体; font-weight: bold; font-size: 26px;">
            (<span id="span_fbt">副标题</span>)</div>
        <div style=" ">
            <table border="0" cellpadding="0" cellspacing="0" style=" width:100%; padding-top:20px;
            font-family: 宋体; ">
                <tr>
                    <td style="line-height: 60px;">
                        案件名称&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ajmc"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        案件编号&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ajbh"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        犯罪嫌疑人姓名&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_fzxyr"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立案时间&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_lasj"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        结案时间&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_jasj"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立卷单位&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ljdw"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        立&nbsp;卷&nbsp;人&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_ljr"></span>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 60px;">
                        审&nbsp;核&nbsp;人&nbsp;&nbsp;&nbsp;&nbsp;<span id="span_shr"></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="line-height: 40px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="10"  style="line-height: 60px;">
                        本案共<span id="span_bagj" style=" display:inline-block; text-align:center; width:80px;"></span>卷
                    </td>
                </tr>
                <tr>
                    <td colspan="10"  style="line-height: 60px;">
                        第<span id="span_djj" style=" display:inline-block;  text-align:center; width:80px;"></span>卷&nbsp;&nbsp;&nbsp;共<span id="span_gjy" style=" display:inline-block; text-align:center;  width:80px;"></span>页
                    </td>
                </tr>
            </table>
        </div>
        <!--endprint-->
    </div>
    <script type="text/javascript">

        var grid = null;
        var select = null;

        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            $('#btn_print').ligerButton({
                text: '打印',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            $("#txt_lasj").ligerDateEditor();
            $("#txt_jasj").ligerDateEditor();       

            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '标题', name: 'BT', minWidth: 150 },
                { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '案件编号', name: 'AJBH', minWidth: 150 },
                { display: '犯罪嫌疑人', name: 'FZXYR', minWidth: 150 },
                { display: '立案时间', name: 'LASJ', minWidth: 150, render: function (item) {
                    if (item.LASJ)
                        return item.LASJ.toString().replace(" 00:00:00", "");
                    return "";
                }
                },
                { display: '结案时间', name: 'JASJ', minWidth: 150, render: function (item) {
                    if (item.JASJ)
                        return item.JASJ.toString().replace(" 00:00:00", "");
                    return "";
                }
                },
                { display: '立卷单位', name: 'LJDW', minWidth: 150 },
                { display: '立卷人', name: 'LJR', minWidth: 150 },
                { display: '审核人', name: 'SHR', minWidth: 150 },
                { display: '本案共卷', name: 'BAGJ', minWidth: 150 },
                { display: '第几卷', name: 'DJJ', minWidth: 150 },
                { display: '共几页', name: 'GJY', minWidth: 150 },
                { display: '操作人', name: 'CZR', minWidth: 150 }, 
                { display: '', name: 'BM', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/Print/PrintFrontCover.aspx?page=1',
                alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '增加', click: addDown, icon: 'add' },
                { line: true },
                { text: '修改', click: editData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
        });

        //提交保持数据
        function submitForm() {
         
            var isUp = false;
            var jdata = $('#add_form').serializeArray();
            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else {
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp = true;
            }
            $.ajax({
                type: "POST",
                url: "/Pages/Print/PrintFrontCover.aspx",
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
                        $("#span_bt").text($("#txt_bt").val());
                        $("#span_fbt").text($("#txt_fbt").val());
                        $("#span_ajmc").text($("#txt_ajmc").val());
                        $("#span_ajbh").text($("#txt_ajbh").val());
                        $("#span_fzxyr").text($("#txt_fzxyr").val());
                        if ($("#txt_lasj").val() != "") {
                            var date = new Date($("#txt_lasj").val().replace(/-/g, '/'));
                            $("#span_lasj").text(date.getFullYear() + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日");
                        } else
                            $("#span_lasj").html("&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日");
                        if ($("#txt_jasj").val() != "") {
                            var date = new Date($("#txt_jasj").val().replace(/-/g, '/'));
                            $("#span_jasj").text(date.getFullYear() + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日");
                        } else
                            $("#span_jasj").html("&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日");
                        $("#span_ljdw").text($("#txt_ljdw").val());
                        $("#span_ljr").text($("#txt_ljr").val());
                        $("#span_shr").text($("#txt_shr").val());
                        $("#span_bagj").text($("#txt_bagj").val());
                        $("#span_djj").text($("#txt_djj").val());
                        $("#span_gjy").text($("#txt_gjy").val());

                        $("#print").printArea(); //doPrint(); //打印

//                        $("#add_form")[0].reset();
                        $.ligerDialog.hide();
                        grid.reload();
                        $.ligerDialog.success(data.v);
                    } else {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }


        //添加按钮
        function addDown() {
            $('#key_hidd').val('');
            $("#add_form")[0].reset();

            $.ligerDialog.open({ title: '增加资料', target: $('#add_div'), width: 570,
                buttons: [{ text: '确定', onclick: function (item, dialog) {
                    submitForm();
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
        function editData() {         
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/Print/PrintFrontCover.aspx',
                    data: { t: "GetModel", id: cksld.BM },
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () {
                        // return $("#add_form").form('enableValidation').form('validate');
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {

                            $("#key_hidd").val(data.BM);

                            $("#txt_bt").val(data.BT);
                            $("#txt_fbt").val(data.FBT);
                            $("#txt_ajmc").val(data.AJMC);
                            $("#txt_ajbh").val(data.AJBH);
                            $("#txt_fzxyr").val(data.FZXYR);
                            if (data.LASJ)
                                $("#txt_lasj").val(data.LASJ.toString().replace(" 00:00:00",""));
                            if (data.JASJ)
                                $("#txt_jasj").val(data.JASJ.toString().replace(" 00:00:00",""));
                            $("#txt_ljdw").val(data.LJDW);
                            $("#txt_ljr").val(data.LJR);
                            $("#txt_shr").val(data.SHR);
                            $("#txt_bagj").val(data.BAGJ);
                            $("#txt_djj").val(data.DJJ);
                            $("#txt_gjy").val(data.GJY);

                            $.ligerDialog.open({ title: '编辑', target: $('#add_div'), width: 570,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    submitForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#add_form")[0].reset();
                                    dialog.hidden();
                                }
                                }], isResize: true
                            });
                            // console.log(JSON.stringify(data));
                        }
                    }
                });
            }
            else
                $.ligerDialog.warn('请先选择一个需要编辑的信息');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.BM };          
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/Print/PrintFrontCover.aspx',
                            data: ar,
                            dataType: 'json',
                            timeout: 5000,
                            cache: false,
                            beforeSend: function () { },
                            error: function (xhr) {
                                $.ligerDialog.error('网络连接错误');
                                return false;
                            },
                            success: function (data) {
                                if (data.t == "win") {
                                    var prowdata = grid.getSelectedRow();
                                    grid.deleteRow(prowdata);
                                    $.ligerDialog.success(data.v);
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择一个需要删除的单位');
        }

   

        $(document).ready(function () {

            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
        }
    </script>
    <script type="text/javascript">
        function btn_print() {

            $.ligerDialog.open({ title: '打印', target: $('#print'), width: 570,
                buttons: [{ text: '打印', onclick: function (item, dialog) {

                    doPrint();
                    //  window.print();

                }, cls: 'l-dialog-btn-highlight'
                },
                    { text: '取消', onclick: function (item, dialog) {

                        dialog.hidden();
                    }
                    }], isResize: true
            });
        }

        //打印
        function doPrint() {

            $("#print").printArea(); 
//            bdhtml = document.getElementById("print").innerHTML;  //window.document.body.innerHTML;
//            printwin 
//            sprnstr = "<!--startprint-->";
//            eprnstr = "<!--endprint-->";
//            var prnhtml = "";
//            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
//            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
//            //   window.document.body.innerHTML = prnhtml;
//            var printwin =  window.frames["myframe"];  //window.open('about:blank');
//            printwin.document.body.innerHTML = prnhtml;
            //            printwin.print();


            bdhtml = window.document.body.innerHTML;         
                        sprnstr = "<!--startprint-->";
                        eprnstr = "<!--endprint-->";
                        var prnhtml = "";
                        prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
                        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
                        window.document.body.innerHTML = prnhtml;
                        window.print();

        }

    </script>
</body>
</html>
