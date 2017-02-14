<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManage.aspx.cs"
    Inherits="WebUI.DepartmentManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>部门管理</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
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
        }
        #tree_left
        {
            width: auto !important;
        }
        #leftFrm
        {
            overflow: auto !important;
            height: 100%;
        }
        .l-tree-icon-folder1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-folder1-open
        {
            background: url("/images/icons/usergroup-open.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-leaf1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon
        {
            background: url('/images/icons/3.png') no-repeat !important;
            background-position: center center !important;
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
        
                   /*左右边框*/
      div .l-layout-left,div .l-layout-center{
         border: 1px solid #dde0e3;
         border-top: 4px solid #129bbc;
         border-radius: 10px;
    }
        .l-panel { 
            overflow: hidden;
            border-top: 1px solid #ccc;
            border-radius: 0;
}
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding: 10px;">
    <div id="layout" style="width: 100%; margin: 0; padding: 0;">
        <div id="leftFrm" position="left">
            <ul id="tree_left">
            </ul>
        </div>
        <div id="centterFrm" position="center" title="部门操作">
            <div id="tb" style="background-color: #f8f8f8">
                <div style="padding: 4px 5px;">
                    名称/简称
                    <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
                    <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </div>
            </div>
            <div id="mainGrid" style="margin: 0px; padding: 0px">
            </div>
        </div>
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="display: none; margin: 0 auto;">
        <form id="add_form" method="post">
        <table style="line-height: 30px; width: 100%;">
            <tr>
                <td style="text-align: right;">
                    所属单位：
                </td>
                <td style="width: 150px;">
                    <input type="hidden" id="hidd_unitNumber" name="hidd_unitNumber" value="" />
                    <input class="l-text" type="text" id="txt_unit" name="txt_unit" disabled="disabled" />
                </td>
                <td style="text-align: right;">
                    上级部门：
                </td>
                <td>
                    <input type="hidden" id="hidd_superiorNumber" name="hidd_superiorNumber" value="" />
                    <input class="l-text" type="text" id="txt_superior" name="txt_superior" disabled="disabled" />
                    <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    部门名称：
                </td>
                <td>
                    <input class="l-text" type="text" id="txt_name" name="txt_name" maxlength="150" />
                </td>
                <td style="text-align: right;">
                    部门简称：
                </td>
                <td>
                    <input class="l-text" type="text" id="txt_abbreviation" name="txt_abbreviation" maxlength="30" />
                </td>
            </tr>
            <%--   <tr>
                <td style="text-align: right;">
                    部门案号简称：
                </td>
                <td>
                    <input class="l-text" type="text" id="txt_abbreviationNum1" name="txt_abbreviationNum1"
                        maxlength="30" />
                </td>
                <td style="text-align: right;">
                    部门文号简称：
                </td>
                <td>
                    <input class="l-text" type="text" id="txt_abbreviationNum2" name="txt_abbreviationNum2"
                        maxlength="30" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    排序序号：
                </td>
                <td>
                    <input id="txt_number" name="txt_number" style="width: 80px;" value="1" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                </td>
                <td>
                    <input class="liger-checkbox" type="checkbox" id="txt_temporary" name="txt_temporary"
                        value="" />是否临时机构
                </td>
                <td style="text-align: right;">
                </td>
                <td>
                    <input class="liger-checkbox" type="checkbox" id="txt_undertake" name="txt_undertake"
                        value="" />是否承办部门
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: right;">
                    备注：
                </td>
                <td colspan="3">
                    <textarea id="txt_remark" name="txt_remark" cols="100" rows="4" class="l-textarea"
                        style="width: 410px; height: 100px"></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        var grid = null;
        var tree = null;
        var tree_dw = '/images/icons/3.png';
        var tree_bm = '/images/icons/bm.png';
        var tree_js = '/images/icons/4.png';
        function clearFormData() {
            $("#hidd_unitNumber").val('');
            $("#key_hidd").val('');
            $("#hidd_superiorNumber").val('');
            $("#txt_name").val('');
            $("#txt_abbreviation").val('');
//            $("#txt_abbreviationNum1").val('');
//            $("#txt_abbreviationNum2").val('');
//            $("#txt_temporary").ligerCheckBox().setValue(false);
//            $("#txt_undertake").ligerCheckBox().setValue(false);
        }
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });

            //$("#txt_number").ligerSpinner({ type: 'int' });
            $('input:checkbox').ligerCheckBox();
            var layout = $("#layout").ligerLayout({ leftWidth: 200, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();
            });
            function resizeLayout() {
                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                $("#leftFrm").height(height - 30);
            }
            //加载树
            tree = $("#tree_left").ligerTree({
                url: "/Handler/ZZJG/DZJZ_Report.ashx",
                parms: { action: "GetDwbm" },
                idFieldName: "id",
                parentIDFieldName: "pid",
                checkbox: false,
                nodeWidth: " ",
                isExpand: 2,
               onClick: function (node) {
                    if (tree.getSelected()) {
                        grid.setParm("t", "GetData");
                        grid.setParm("dkey", tree.getSelected().data.id);
                        grid.setParm("key", $("#txt_key").val())
                        grid.reload();
                    }
                }, onSuccess: function (data) {
                   
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onBeforeExpand: function (node) {
                    if (node.data.children.length == 0) {
                        $.post("/Handler/Common/UnitCommonHandler.ashx?pa=2", { t: "GetTreeDW", level: 3, pid: node.data.id },
                        function (newData) {
                            if (newData.t) {
                                $.ligerDialog.error(newData.v);
                            } else {
                                tree.append(node.target, newData);
                            }
                        }, 'json');
                    }
                }
            });
//            tree = $("#tree_left").ligerTree({
//                url: "/Handler/Common/UnitCommonHandler.ashx?pa=1",
//                parms: {
//                    t: 'GetTreeDW', level: 3
//                },
//                isExpand: 2,
//                checkbox: false,
//                slide: false,
//                nodeWidth: 120,
//                //                parentIcon: 'folder1',
//                //                childIcon: 'leaf1',
//                onClick: function (node) {
//                    if (tree.getSelected()) {
//                        grid.setParm("t", "GetData");
//                        grid.setParm("dkey", tree.getSelected().data.id);
//                        grid.setParm("key", $("#txt_key").val())
//                        grid.reload();
//                    }
//                }, onSuccess: function (data) {
//                    if (data.t) {
//                        $.ligerDialog.error(data.v);
//                    }
//                }, onBeforeExpand: function (node) {
//                    if (node.data.children.length == 0) {
//                        $.post("/Handler/Common/UnitCommonHandler.ashx?pa=2", { t: "GetTreeDW", level: 3, pid: node.data.id },
//                        function (newData) {
//                            if (newData.t) {
//                                $.ligerDialog.error(newData.v);
//                            } else {
//                                tree.append(node.target, newData);
//                            }
//                        }, 'json');
//                    }
//                }
//            });

            var toolbar1 = { width: 120, items: [
                { text: '增加部门', click: addData, img: '../../images/add.png' },
              //  { line: true },
                { text: '增加下级部门', click: addDataBelow, img: '../../images/add.png' },
               // { line: true },
                { text: '修改', click: editData, img: '../../images/xg.png' },
              //  { line: true },
                { text: '删除', click: DelData, img: '../../images/sc.png' }
                ]
            };

            //绑定列表
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '单位名称', name: 'DWMC', width: 80,  },
                { display: '部门名称', name: 'BMMC', minWidth: 60,  },
                //{ display: '部门编号', name: 'BMBM', width: 80 },
                { display: '部门简称', name: 'BMJC', minWidth: 50 },                
//                { display: '部门案号简称', name: 'BMAHJC', minWidth: 50 },
//                { display: '部门文号简称', name: 'BMWHJC', minWidth: 50 },
//                { display: '是否临时机构', name: 'SFLSJG', width: 70, render: function (item) {
//                    if (item.SFLSJG == 'N') return '否';
//                    else return "是";
//                }
//                },
//                { display: '是否承办部门', name: 'SFCBBM', width: 70, render: function (item) {
//                    if (parseInt(item.SFCBBM) == 1) return '是';
//                    else if (parseInt(item.SFCBBM) == 0) return '否';
//                    else return "";
//                }
//                },
                //{ display: '序号', name: 'BMXH', width: 60 },
                { display: '备注', name: 'BZ', minWidth: 10,  }
                ], rownumbers: true
                , fixedCellHeight: false, width: '100%', height: '100%', heightDiff: -14,        //服务器分页
                url: '/Pages/UnitDepartment/DepartmentManage.aspx',
                usePager: false,
                tree: { columnName: 'BMMC', idField: 'BMBM', parentIDField: 'FBMBM' },
                parms: { t: "GetData", dkey: tree.getSelected() != null ? tree.getSelected().data.id : "" }
                , onAfterShowData: function () {
                    var l = $(".l-grid-tree-link-open").length;
                    for (var i = l - 1; i >= 0; i--) {
                        $(".l-grid-tree-link-open")[i].click();
                    }
                }, onSelectRow: function (data, rowindex, rowobj) {
                    tree.selectNode(data.DWBM);
                }
                , toolbar: toolbar1
                , onSelectRow: function (rowdata, rowid, rowobj) { 
                 
                 //选中下级部门时，添加css样式
                       $(".l-toolbar-item").each(function (index) {
                        //为表格每个按钮添加css属性
                        $(this).addClass("l-toolbar-item-" + index);
                        for (var k = 0; k <= index; k++) {
                          $(".l-toolbar-item-" + k).css({ background: dd[k], color: "white" });
                        }
                    })
             //
                    if (rowdata.BMBM == "0000") {
         
                        var toolbar2 = { width: 120, items: [
                            { text: '增加部门', click: addData, img: '../../images/add.png' },
                           // { line: true },
                            { text: '增加下级部门', click: addDataBelow, img: '../../images/add.png' },
                          //  { line: true },
                            { text: '修改', click: editData, img: '../../images/xg.png' },
                          //  { line: true },
                            { text: '删除', disable: true, click: DelData, img: '../../images/sc.png' }
                        ]
                        };
                        grid._setToolbar(toolbar2);
                    } else {
                        grid._setToolbar(toolbar1);
                    }

                   //  指定按钮颜色
                          var dd = ["rgb(127,170,141)", "rgb(116,172,148)", "rgb(179,160,87)", "rgb(126,101,105)", "rgb(155,143,122)", "rgb(91,101,182)", "rgb(163,152,154)", "rgb(174,141,69)", "rgb(71,175,41)", "rgb(121,83,115)", "rgb(150,161,70)", "rgb(74,131,91)", "rgb(59,126,136)", "rgb(177,62,150)", "rgb(21,178,96)", "rgb(162,29,179)", "rgb(89,184,179)", "rgb(159,173,44)", "rgb(127,101,120)", "rgb(130,123,29)"];

                        //
                               
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
            var jdata = $('#add_form').serializeArray();
            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else
                jdata[jdata.length] = { name: "t", value: "UpData" };
            $.ajax({
                type: "POST",
                url: "/Pages/UnitDepartment/DepartmentManage.aspx",
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
                    } else
                        $.ligerDialog.error(data.v);
                }
            });
        }

        //添加部门按钮
        function addData() {
            $("#key_hidd").val('');
            var sv = tree.getSelected();
            if (sv != null) {
                clearFormData();
                $.ligerDialog.open({ title: '添加部门', target: $('#add_div'), width: 600,
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
                $("#txt_unit").val(sv.data.text);
                $("#hidd_unitNumber").val(sv.data.id);
            } else
                $.ligerDialog.warn('请先选择一个需要添加部门的单位');
        }
        //添加下级部门
        function addDataBelow() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                var sv = tree.getSelected();
                if (sv != null) {

                    clearFormData();
                    $.ligerDialog.open({ title: '添加下级部门', target: $('#add_div'), width: 600,
                        buttons: [{ text: '确定', onclick: function (item, dialog) {
                            submitForm();
                        }, cls: 'l-dialog-btn-highlight'
                        }, { text: '取消', onclick: function (item, dialog) {
                            $("#add_form")[0].reset();
                            dialog.hidden();
                        }
                        }], isResize: true
                    });

                    $("#txt_unit").val(sv.data.text);
                    $("#hidd_unitNumber").val(sv.data.id);
                    $("#txt_superior").val(cksld.BMMC);
                    $("#hidd_superiorNumber").val(cksld.BMBM);
                }
                else
                    $.ligerDialog.warn('未选中当前部门的单位!');
            } else
                $.ligerDialog.warn('请先选择部门添加!');
        }
        //            //删除按钮
        function DelData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();

                ar[0] = { name: "BMBM", value: arrck.BMBM };
                ar[1] = { name: "DWBM", value: arrck.DWBM };
                ar[2] = { name: "bmmc", value: arrck.BMMC };
                var ch = grid.hasChildren(arrck); //是否包含子节点
                if (ch.length > 0) {
                    $.ligerDialog.warn('该单位包含下级单位，请先选择删除下级单位！');
                    return false;
                }
                ar[3] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/UnitDepartment/DepartmentManage.aspx',
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
                                    grid.reload();
                                    $.ligerDialog.success(data.v);
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择一个需要删除的部门');
        }

        //点击编辑按钮
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                clearFormData();
                $.ajax({
                    type: "POST",
                    url: '/Pages/UnitDepartment/DepartmentManage.aspx',
                    data: { t: "GetModelPList", id: cksld.BMBM, did: cksld.DWBM, parentid: cksld._parentId, bmmc: cksld.BMMC },
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
                            var pdata = null;
                            var dwmc = null;

                            pdata = grid.getParent(grid.getSelectedRow());

                            dwmc = tree.getSelected() != null ? tree.getSelected().data.text : "";

                            $.ligerDialog.open({ title: '编辑部门', target: $('#add_div'), width: 600,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    submitForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#add_form")[0].reset();
                                    dialog.hidden();
                                }
                                }], isResize: true
                            });

                            $("#hidd_unitNumber").val(data.DWBM);
                            $("#key_hidd").val(data.BMBM);
                            $("#hidd_superiorNumber").val(data.FBMBM);
                            $("#txt_name").val(data.BMMC);
                            $("#txt_abbreviation").val(data.BMJC);
//                            $("#txt_abbreviationNum1").val(data.BMAHJC);
//                            $("#txt_abbreviationNum2").val(data.BMWHJC);
//                            var temporary = $("#txt_temporary").ligerCheckBox();
//                            var undertake = $("#txt_undertake").ligerCheckBox();
//                            if (data.SFLSJG == "Y")
//                                temporary.setValue(true);
//                            else
//                                temporary.setValue(false);
//                            if (parseInt(data.SFCBBM) == 1)
//                                undertake.setValue(true);
//                            else
//                                undertake.setValue(false);
//                            $("#txt_number").val(data.BMXH);
                            $("#txt_remark").val(data.BZ);
                            $("#txt_superior").val(pdata != null ? pdata.BMMC : "");
                            $("#txt_unit").val(dwmc);

                            // console.log(JSON.stringify(data));
                        }
                    }
                });
            }
            else
                $.ligerDialog.error('请先选择一个需要修改部门');
        }
        $(document).ready(function () {
            //点击搜索按钮
            $("#btn_search").click(function () {
                //清除列表选中行     
                grid.loadServerData({
                    t: "GetData",
                    dkey: (tree.getSelected() != null ? tree.getSelected().data.id : ""),
                    key: $("#txt_key").val()
                });
            });

        });
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
