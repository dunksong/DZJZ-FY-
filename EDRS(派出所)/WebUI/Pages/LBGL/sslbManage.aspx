<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sslbManage.aspx.cs" Inherits="WebUI.Pages.LBGL.sslbManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>所属类别管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="../../LigerUI/lib/json2.js" type="text/javascript"></script>
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
            margin: 0;
        }
        .l-text-combobox
        {
            display: inline-table;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        #update_form table tr td
        {
            padding: 8px 0px;
        }
        
         /* 按钮 */
          div .l-button {
            color: white;
        }
        div#btn_search {
            background: #ed6d4a;
        }
        .l-toolbar-item.l-panel-btn.l-toolbar-item-hasicon {
            background: #339bca;
            color: white;
        }
        
          /* 内容区 */
       div#tb {
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background:white;
        }
    </style>
</head>
<body style="padding:15px; overflow: hidden;">
    <div id="tb">
        <div style="padding: 10px;">
            所属类别名称：<input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
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
               <tr id="tr_select_j" style="display:none;">
                    <td>
                        选择卷：
                    </td>
                    <td>
                        <input id="key_hidd" name="key_hidd" type="hidden" value="" />
                        <input class="l-text" id="txt_slj" type="text" name="txt_slj" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        名称：
                    </td>
                    <td>
                        <input class="l-text" id="txt_name" type="text" name="txt_name" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        顺序：
                    </td>
                    <td>
                        <input class="l-text" id="txt_sx" type="text" name="txt_sx" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        说明：
                    </td>
                    <td>
                        <input class="l-text" id="txt_sm" type="text" name="txt_sm" style="width: 350px;"
                            maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <%--修改--%>
    
    <script type="text/javascript">

        var grid = null;
      //  var select = null;
        var gridlod = true;
        var stype = null;
       
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });

            stype = $("#txt_slj").ligerComboBox({
                url: 'sslbManage.aspx',
                parms: { t: "GetPrntJ" },
                valueFieldID: 'key_fhidd',
                valueField: 'SSLBBM',
                textField: "SSLBMC",
                width: 350,
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });


            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '所属类别名称', name: 'SSLBMC', minWidth: 60, align: 'left' },
                { display: '所属类别类型', name: 'SSLBLX', minWidth: 60, render: function (item) {
                    if (item.SSLBLX == "1")
                        return "卷";
                    else if (item.SSLBLX == "2")
                        return "目录";
                    else if (item.SSLBLX == "3")
                        return "文件";
                }
                },
                { display: '所属类别顺序', name: 'SSLBSX', minWidth: 50 },
                { display: '所属类别说明', name: 'SSLBSM', minWidth: 50, align: 'left' },
                { display: '所属类别编码', name: 'SSLBBM', width: 1, hide: true }
                ], rownumbers: false, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LBGL/sslbManage.aspx',
                alternatingRow: false, fixedCellHeight: false, usePager: false, heightDiff: -16,
                tree: { columnName: 'SSLBMC', idField: 'SSLBBM', parentIDField: 'FSSLBBM' },
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, onAfterShowData: function (currentData) {
                    //                    if (gridlod) {
                    //                        var l = $(".l-grid-tree-link-open").length;
                    //                        for (var i = l - 1; i >= 1; i--) {
                    //                            $(".l-grid-tree-link-open")[i].click();
                    //                        }
                    //                    }
                }, toolbar: { items: [
                { text: '增加卷', click: addDown, img: '../../images/add.png' },
              //  { line: true },
                { text: '增加文件', click: addDownTo, icon: '../../images/add.png' },
                //{ line: true },
                { text: '修改', click: editData, icon: '../../images/sc.png' },
               // { line: true },
                { text: '删除', click: deleteData, icon: '../../images/sc.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onTreeExpand: function (data) {
                    if (!grid.hasChildren(data)) {
                        $.post("/Pages/LBGL/sslbManage.aspx", { t: "ListBind", level: 3, pid: data.id },
                            function (json) {
                                gridlod = false;
                                for (var i = 0; i < json.Rows.length; i++) {
                                    var cd = grid.addRow(json.Rows[i], true, false, data);
                                    grid.collapse(cd);
                                }
                            }, 'json');
                    }
                }
            });
            $("#pageloading").hide();

        });

        //提交保持数据
        function submitForm(obj) {
            var isUp = false;
            var jdata = $('#add_form').serializeArray();
            if ($.trim($("#key_hidd").val()) == "") {
                if (obj == "add_formto")
                    jdata[jdata.length] = { name: "slct_type_val", value: "3" };
                else
                    jdata[jdata.length] = { name: "slct_type_val", value: "1" };
                jdata[jdata.length] = { name: "t", value: "AddData" };
            }
            else {                
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp = true;
            }

            $.ajax({
                type: "POST",
                url: "/Pages/LBGL/sslbManage.aspx",
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

                        if (obj != "add_formto") {
                            $.ligerDialog.hide();
                            $("#add_form")[0].reset();
                        } else {
                            $("#txt_name").val("");
                            var sx = Number($("#txt_sx").val());
                            $("#txt_sx").val(sx + 1);
                            $("#txt_sm").val("");
                        }
                        $.ligerDialog.success(data.v);
                        grid.reload();
                       
                    } else {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }


        //添加按钮卷
        function addDown() {
            $("#tr_select_j").hide();
            $('#key_hidd').val('');
            $('#key_fhidd').val('');
            $("#add_form")[0].reset();
            //   select.selectValue("1");
            getmaxnum();
            $.ligerDialog.open({ title: '编辑类别', target: $('#add_div'), width: 570,
                buttons: [{ text: '确定', onclick: function (item, dialog) {
                    submitForm("add_form");
                }, cls: 'l-dialog-btn-highlight'
                },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#add_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
            });
        }
        //添加按钮文件
        function addDownTo() {
            $('#key_hidd').val('');
            $('#key_fhidd').val('');
            stype.reload();
            $("#tr_select_j").show();
            var cksld = grid.getSelectedRow();
            if (cksld != null && cksld.SSLBLX == "1") {
                $("#add_form")[0].reset();
                getmaxnum(cksld.SSLBBM);
                //                $('#key_fhidd').val(cksld.SSLBBM);
                //select.selectValue("3");
                stype.selectValue(cksld.SSLBBM);
                $.ligerDialog.open({ title: '编辑类别', target: $('#add_div'), width: 570,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        submitForm("add_formto");
                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#add_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
                });
            } else {
                $.ligerDialog.warn('请先选择一个卷类型才能添加文件');
            }
        }
        //修改
        function editData() {

            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $("#tr_select_j").hide();
                $.ajax({
                    type: "POST",
                    url: '/Pages/LBGL/sslbManage.aspx',
                    data: { t: "GetModel", id: cksld.SSLBBM },
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
                            $("#key_hidd").val(data.SSLBBM);
                            //select.selectValue(data.SSLBLX);
                            $("#txt_name").val(data.SSLBMC);
                            $("#txt_sx").val(data.SSLBSX);
                            $("#txt_sm").val(data.SSLBSM);

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
                $.ligerDialog.warn('请先选择一个需要编辑的配置信息');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();

            if (arrck) {
                var str = "确定是否删除?";
                if (arrck.children) {
                    //                    $.ligerDialog.warn('请先删除文件');
                    //                    return false;
                    str = "确定下级类别一起删除?";
                }
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.SSLBBM };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm(str, function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/LBGL/sslbManage.aspx',
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
                                    grid.reload();
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择一个需要删除的单位');
        }
        //获取最大排序编号
        function getmaxnum(fid) {
            $.ajax({
                type: "POST",
                url: '/Pages/LBGL/sslbManage.aspx',
                data: { t: "GetMaxSx", fid: fid },
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
                    if (data.t == "win") {
                        $("#txt_sx").val(data.v);
                    } else {
                        $("#txt_sx").val(1);
                    }
                }
            });
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
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
