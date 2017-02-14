<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitManage.aspx.cs" Inherits="WebUI.UnitManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>单位管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
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
        
        div#tb {
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
    <div id="tb" >
        <div style="padding: 4px 5px;">
            名称/简称
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 0px 22px 0px 22px">
            <form id="add_form" method="post">
            <table style="line-height: 30px;">
                <tr>
                    <td>
                        上级单位：
                    </td>
                    <td>
                        <input type="hidden" id="hidd_superiorNumber" name="hidd_superiorNumber" value="" />
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input id="txt_superior" class="l-text" type="text" name="txt_superior" disabled="disabled"
                            style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        单位编码：
                    </td>
                    <td>
                        <input class="l-text" type="text" id="txt_number" name="txt_number" maxlength="50"
                            style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        单位名称：
                    </td>
                    <td>
                        <input class="l-text" type="text" id="txt_name" name="txt_name" maxlength="150" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        单位简称：
                    </td>
                    <td>
                        <input class="l-text" type="text" id="txt_abbreviation" name="txt_abbreviation" maxlength="30"
                            style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        单位级别：
                    </td>
                    <td>
                        <input id="txt_rank" name="txt_rank" class="l-text" value="1" style="width: 200px;" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">

        var grid = null;
        var gridlod = true;

        $(function () {

            $("#txt_superior").ligerTextBox().setDisabled(true);
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });
         
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '单位名称', name: 'DWMC', minWidth: 60,  },
                { display: '单位编码', name: 'DWBM', width: 100 },
                { display: '单位简称', name: 'DWJC', minWidth: 60 },
                { display: '单位级别', name: 'DWJB', minWidth: 50 }
                ], rownumbers: false, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/UnitDepartment/UnitManage.aspx?page=1',
                alternatingRow: false, fixedCellHeight: false, usePager: false,
                tree: { columnName: 'DWMC', idField: 'DWBM', parentIDField: 'FDWBM' },
                parms: { t: "GetData",
                    key: $("#txt_key").val()
                }, onAfterShowData: function (currentData) {
                    if (gridlod) {
                        var l = $(".l-grid-tree-link-open").length;
                        for (var i = l - 1; i >= 1; i--) {
                            $(".l-grid-tree-link-open")[i].click();
                        }
                    }
                }, toolbar: { items: [
                { text: '增加下级单位', click: addDown, img: '../../images/add.png' },
            //    { line: true },
                { text: '修改', click: editData, img: '../../images/xg.png' },
              //  { line: true },
                { text: '删除', click: deleteData, img: '../../images/sc.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onTreeExpand: function (data) {
                    if (!grid.hasChildren(data)) {
                        $.post("/Pages/UnitDepartment/UnitManage.aspx?page=2", { t: "GetData", level: 2, pid: data.id },
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
        function submitForm() {
        var isUp=false;
            var jdata = $('#add_form').serializeArray();
            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else{
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp=true;
                }
                $.ajax({
                    type: "POST",
                    url: "/Pages/UnitDepartment/UnitManage.aspx",
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
                      
                            var prowdata = grid.getSelectedRow();
                            if (isUp == false) {                               
                                if(prowdata.children)
                                grid.addRow({ "id": "" + $("#txt_number").val() + "", "text": "" + $("#txt_name").val() + "", "DWBM": "" + $("#txt_number").val() + "", "DWMC": "" + $("#txt_name").val() + "", "FDWBM": "" + $("#hidd_superiorNumber").val() + "", "DWJB": "1", "DWJC": "" + $("#txt_abbreviation").val() + "" }, null, true, prowdata);else
                                $("#btn_search").click();
                            } else {
                                grid.updateRow(prowdata, { "id": "" + $("#txt_number").val() + "", "text": "" + $("#txt_name").val() + "", "DWBM": "" + $("#txt_number").val() + "", "DWMC": "" + $("#txt_name").val() + "", "FDWBM": "" + $("#hidd_superiorNumber").val() + "", "DWJB": "1", "DWJC": "" + $("#txt_abbreviation").val() + "" });
                            }
                            
                            $("#add_form")[0].reset();
                          
                            $.ligerDialog.hide();
                           
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
            var sv = grid.getSelectedRow();
            if (sv != null) {
                $.ligerDialog.open({ title: '增加下级单位', target: $('#add_div'), width: 380,
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
                $("#txt_superior").val(sv.DWMC);
                $("#hidd_superiorNumber").val(sv.DWBM);
                $("#txt_number").ligerTextBox().setEnabled(true);

            } else
                $.ligerDialog.warn('请先选择一个需要添加下级单位的单位');
        }
        //修改
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/UnitDepartment/UnitManage.aspx',
                    data: { t: "GetModelPList", id: cksld.DWBM, parentid: cksld.FDWBM ,dwmc:cksld.DWMC},
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
                            var pdata = null;
                            try {
                                pdata = grid.getParent(cksld);
                            } catch (e) {
                            }
                            $("#txt_number").ligerTextBox().setDisabled(true);
                            $("#key_hidd").val(data.DWBM);
                            $("#txt_number").val(data.DWBM);
                            $("#txt_name").val(data.DWMC);
                            $("#txt_abbreviation").val(data.DWJC);
                            $("#txt_rank").val(data.DWJB);
                            $("#hidd_superiorNumber").val(data.FDWBM);
                            $("#txt_superior").val(pdata != null ? pdata.DWMC : "顶级单位");
                            $.ligerDialog.open({ title: '编辑单位', target: $('#add_div'), width: 380,
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
                $.ligerDialog.warn('请先选择一个需要编辑的单位');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();

            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "DWBM", value: arrck.DWBM };
                ar[1] = { name: "DWMC", value: arrck.DWMC };
                var ch = grid.hasChildren(arrck); //是否包含子节点
                if (ch.length > 0) {
                    $.ligerDialog.warn('该单位包含下级单位，请先选择删除下级单位！');
                    return false;
                }
                ar[2] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/UnitDepartment/UnitManage.aspx',
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
                grid.loadServerData({
                    t: "GetData",
                    key: $("#txt_key").val(),
                    page: 1, pagesize: grid.options.pageSize
                });
            });

        });
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
