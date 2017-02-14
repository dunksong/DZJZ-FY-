<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YJSQ.aspx.cs" Inherits="WebUI.Pages.LSYJ.YJSQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷分配</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <style type="text/css">
        .l-panel-topbar
        {
            padding: 5px 0;
            margin-bottom: -1px;
             border-bottom: 1px solid #959595;
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
            width: 250px;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        .l-table
        {
            width: 100%;
        }
        .l-table tr td
        {
            padding: 5px 2px;
        }
        div#tb {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px;  overflow: hidden;">
    <div id="tb">
        <div>
         <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    唯一编号：
                    <% }
                       else
                       { %>
                  案号名称：
                    <% } %>
           <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
             <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                    事项议题：
                    <% }
                       else
                       { %>
                  案由：
                    <% } %><input id="txt_ajmc" style="width: 150px;" class="l-text" type="text" name="txt_ajmc" />&nbsp;&nbsp;
            阅卷人工号：<input id="txt_gh" style="width: 100px;" class="l-text" type="text" name="txt_gh" />&nbsp;&nbsp;
            阅卷人名称：<input id="txt_mc" style="width: 100px;" class="l-text" type="text" name="txt_mc" />&nbsp;&nbsp;
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 60px 20px 60px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        <%--律师证号：--%>
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <%--<input class="l-text" id="txt_lszh" type="text" name="txt_lszh" maxlength="200" />--%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请单名：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjsqdm" type="text" name="txt_yjsqdm" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请说明：
                    </td>
                    <td>
                        <textarea id="txt_sqsm" name="txt_sqsm" cols="100" rows="2" class="l-textarea"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <%--审核窗口--%>
    <div id="examine_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 60px 20px 60px">
            <form id="examine_form" method="post">
            <table class="l-table">
                <tr>
                    <td style="width: 70px;">
                        <%--律师证号：--%>
                    </td>
                    <td>
                        <input type="hidden" id="key_ex_hidd" name="key_ex_hidd" value="" />
                        <%--<span class="l-span" id="span_lszh"></span>--%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请单号：
                    </td>
                    <td>
                        <span class="l-span" id="span_yjsqdh"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请单名：
                    </td>
                    <td>
                        <span class="l-span" id="span_yjsqdm"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请时间：
                    </td>
                    <td>
                        <span class="l-span" id="span_sqsj"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请说明：
                    </td>
                    <td>
                        <span class="l-span" id="span_sqsm"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核说明：
                    </td>
                    <td>
                        <textarea id="txt_shsm" name="txt_shsm" cols="100" rows="2" class="l-textarea"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="radio" id="rad_sqdzt_yes" name="rad_sqdzt" value="Y" /><label for="rad_sqdzt_yes">是通过</label>&nbsp;&nbsp;&nbsp;
                        <input type="radio" id="rad_sqdzt_no" name="rad_sqdzt" value="N" /><label for="rad_sqdzt_no">不通过</label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">

        var grid = null;
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });

            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '阅卷人工号', name: 'GH', minWidth: 150 },
                { display: '阅卷人姓名', name: 'MC', minWidth: 150 },
                
                  <% if (Ver_Advanced_Alone == "ADVANCED_ALONE")
                       { %>
                       { display: '唯一编号', name: 'BMSAH', minWidth: 250 },
                    { display: '事项议题', name: 'AJMC', minWidth: 80 },
                    <% }
                       else
                       { %>
                       { display: '案号名称', name: 'BMSAH', minWidth: 250 },
                    { display: '案由', name: 'AJMC', minWidth: 250 },
                    <% } %>
//                  { display: '', name: 'BMSAH', width:1,hide :true },
                { display: '阅卷状态', name: 'YJKSSJ'
                    , render: function (item) {
                        var nowTime = '<%=nowTime %>';
                        var kssj = item.YJKSSJ;
                        var jssj = item.YJJSSJ;
                        nowTime = Date.parse(nowTime.replace(/-/g, '/'));
                        if(kssj)
                        kssj = Date.parse(kssj.replace(/-/g, '/'));
                        if(jssj)
                        jssj = Date.parse(jssj.replace(/-/g, '/'));
                        if (nowTime < kssj)
                            return "<font style='color:red;'>未到时</font>";
                        else if (nowTime > kssj && nowTime < jssj)
                            return "<font style='color:green;'>正常 </font>";
                        else if (nowTime > jssj)
                            return "<font style='color:gray;'>过期</font>";
                        else
                            return "";
                    }
                },
                { display: '开始时间', name: 'YJKSSJ', width: 150 },
                { display: '结束时间', name: 'YJJSSJ', width: 150 },
                { display: '分配人', name: 'JDR', minWidth: 80 },
                { display: '分配时间', name: 'JDSJ', width: 150 },
                { display: '阅卷序号', name: 'YJXH', width: 1, hide: 'true' }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/YJSQ.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                    //{ text: '申请', click: addDown, icon: 'add' },
                    //                { line: true },
                    //                { text: '审核', click: examineData, img: '/images/icons/edit.png' },
                    //                {line: true },
                    //                { text: '修改', click: editData, icon: 'modify' },

                    //               {line: true },
                {text: '分配', click: distribution, img: '../../images/NewAdd/fp.png' },
                //{ line: false },
                { text: '删除', click: deleteData, img: '../../images/NewAdd/sc.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                       // $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
        });

        //提交保存数据
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
                url: "/Pages/LSYJ/YJSQ.aspx",
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

        //提交审核数据
        function examineForm() {
            var jdata = $('#examine_form').serializeArray();
            jdata[jdata.length] = { name: "t", value: "ExUpData" };
            $.ajax({
                type: "POST",
                url: "/Pages/LSYJ/YJSQ.aspx",
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
                        $("#examine_form")[0].reset();
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

            $.ligerDialog.open({ title: '申请阅卷', target: $('#add_div'), width: 560,
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

        //审核
        function examineData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/YJSQ.aspx',
                    data: { t: "GetModel", id: cksld.YJSQDH, cs: cksld.YJSQDM },
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () {
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#key_ex_hidd").val(data.YJSQDH);
                            //$("#span_lszh").html(data.LSZH);
                            $("#span_yjsqdh").html(data.YJSQDH);
                            $("#span_yjsqdm").html(data.YJSQDM);
                            $("#span_sqsm").html(data.SQSM);
                            $("#span_sqsj").html(data.SQSJ);
                            $("#txt_shsm").html(data.SHSM);
                            if (data.SQDZT == "Y")
                                $("#rad_sqdzt_yes").click();
                            else if (data.SQDZT == "N")
                                $("#rad_sqdzt_no").click();
                            $.ligerDialog.open({ title: '申请审核', target: $('#examine_div'), width: 560,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    examineForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#examine_form")[0].reset();
                                    dialog.hidden();
                                }
                                }], isResize: true
                            });
                        }
                    }
                });
            }
            else
                $.ligerDialog.warn('请先选择一个需要编辑的配置信息');
        }

        //修改
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/YJSQ.aspx',
                    data: { t: "GetModel", id: cksld.YJSQDH, cs: cksld.YJSQDM },
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
                            $("#key_hidd").val(data.YJSQDH);
                            //$("#txt_lszh").val(data.LSZH);                           
                            $("#txt_yjsqdm").val(data.YJSQDM);
                            $("#txt_sqsm").val(data.SQSM);
                            $.ligerDialog.open({ title: '修改申请', target: $('#add_div'), width: 560,
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
                $.ligerDialog.warn('请选择一个申请');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.YJXH };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/LSYJ/YJSQ.aspx',
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
                $.ligerDialog.warn('请先选择一个被删除申请');
        }
        //分配
        function distribution() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                location.href = "ReadingDistribution.aspx?yjxh=" + (cksld.YJXH == null ? "" : cksld.YJXH) + "&bmsah=" + (cksld.BMSAH == null ? "" : encodeURI(encodeURI(cksld.BMSAH))) + "&ajmc=" + (cksld.AJMC == null ? "" : encodeURI(encodeURI(cksld.AJMC)));
            }
            else
                location.href = "ReadingDistribution.aspx";
            //            } else
            //                $.ligerDialog.warn('请先选择申请再进行分配');
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
                        ajmc: $("#txt_ajmc").val(),
                        gh: $("#txt_gh").val(),
                        mc: $("#txt_mc").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("ajmc", $("#txt_ajmc").val());
            grid.setParm("gh", $("#txt_gh").val());
            grid.setParm("mc", $("#txt_mc").val());
        }
    </script>
</body>
    <script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
