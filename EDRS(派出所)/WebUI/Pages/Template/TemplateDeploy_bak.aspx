<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateDeploy_bak.aspx.cs"
    Inherits="WebUI.Pages.Template.TemplateDeploy_bak" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>模板配置</title>
    <link href="/ligerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <link href="/Scripts/tools/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.easyui.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
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
        }
        .textbox-readonly
        {
            border-color: #c2c2c2;
            background: #e0e0e0;
        }
        .textbox-readonly input
        {
            background: #e0e0e0;
        }
    </style>
</head>
<body style="padding: 0px; margin: 0; overflow: hidden;">
    <div id="tb" style="background-color: #f8f8f8">
        <div style="padding: 4px 5px;">
            模板名称
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="maingrid" style="margin: 0px; padding: 0px;">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <form id="add_form" method="post">
        <div style="float: left; width: 300px; border: 1px solid #A3C0E8; height: 400px;
            overflow: auto;">
            <ul id="path_tree">
            </ul>
        </div>
        <div style="float: left; height: 360px; border: 1px solid #A3C0E8; margin-left: 20px;
            padding: 20px; width: auto;">
            <table style="line-height: 30px; width: 100%;">
                <tr>
                <td colspan="2" style="text-align:center;">
                        <input id="OpAdd" type="radio" class="l-radio" value="Add" name="op" checked="checked"/>新增&nbsp;&nbsp;
                        <input id="OpEndit" type="radio" class="l-radio" value="Edit" name="op" />修改&nbsp;&nbsp;
                </td>
                </tr>
                <tr>
                    <td>
                        所属单位：
                    </td>
                    <td>
                        <!--DossierTypeValueMember-->
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <!--DossierParentMember-->
                        <input type="hidden" id="key_parent" name="key_parent" value="" />
                        <!-- DWBM -->
                        <input type="text" name="tree_dwbm" id="tree_dwbm" maxlength="150" class="liger-textbox"
                            ligerui="width:200" />
                    </td>
                </tr>
                <tr>
                    <td>
                        选择节点：
                    </td>
                    <td>
                        <input id="tree_lable" value="无" class="liger-textbox" ligerui="width:200">
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= ((VersionName)0).ToString() %>类别：
                    </td>
                    <td>
                        <!--CaseInfoTypeID-->
                        <input id="tree_ajtype" name="tree_ajtype">
                    </td>
                </tr>
                <tr>
                    <td>
                        所属类别:
                    </td>
                    <td>
                        <input id="tree_sslb" name="tree_sslb">
                    </td>
                </tr>
                <tr>
                    <td>
                        名称:
                    </td>
                    <td>
                        <!--Category-->
                        <input type="hidden" id="hidd_Category" name="hidd_Category" value="" />
                        <!--DossierTypeDisplayMember-->
                        <input type="hidden" name="text_id" id="text_id"/>
                        <input type="text" name="txt_name" id="txt_name" maxlength="150" class="liger-textbox"
                            ligerui="width:200" />
                    </td>
                </tr>
                <tr>
                    <td>
                        序号：
                    </td>
                    <td>
                        <!--SortIndex-->
                        <input name="txt_rank" id="txt_rank" value="1">
                    </td>
                </tr>
                <tr>
                    <td>
                        类型
                    </td>
                    <td>
                        <input id="j_radio" type="radio" class="l-radio" value="J" name="age" />卷&nbsp;&nbsp;
                        <input id="m_radio" type="radio" class="l-radio" value="M" name="age" />目录&nbsp;&nbsp;
                        <input id="w_radio" type="radio" class="l-radio" value="W" name="age" />文件
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input id="chk_autoFound" type="checkbox" class="l-checkbox"  name="chk_autoFound" value="" /><label for="chk_autoFound">自动生成模板</label> 
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <div id="btnJ" style="line-height: 23px; display: inline-block;">
                            保 存</div>
                        &nbsp;&nbsp;
                        <div id="btn_delete" style="line-height: 23px; display: inline-block;">
                            删 除</div>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <%--修改窗口--%>
    <div id="up_div" style="padding: 10px; display: none;">
        <form id="up_form" method="post">
        <table style="line-height: 30px; width: 100%;">
            <tr>
                <td>
                    所属单位：
                </td>
                <td>
                    <!--DossierTypeValueMember-->
                    <input type="hidden" id="key_hidd_up" name="key_hidd_up" value="" />
                    <!-- UnitID -->
                    <%--<input id="tree_dwbm_up" class="l-text" name="tree_dwbm_up" data-options="required:false" style="border-radius: 0px;
                        width: 200px; height: 24px;">--%>
                    <input id="txt_dwbm_up" disabled style="width: 200px;">
                </td>
            </tr>
            <tr>
                <td>
                    <%= ((VersionName)0).ToString() %>类别：
                </td>
                <td>
                    <!--CaseInfoTypeID-->
                    <input id="tree_ajtype_up" name="tree_ajtype_up">
                </td>
            </tr>
            <tr>
                <td>
                    所属类别：
                </td>
                <td>
                    <input id="tree_sslb_up" name="tree_sslb_up">
                </td>
            </tr>
            <tr>
                <td>
                    名称：
                </td>
                <td>
                    <!--Category-->
                    <input type="hidden" id="hidd_Category_up" name="hidd_Category_up" value="" />
                    <!--DossierTypeDisplayMember-->
                    <input type="text" name="txt_name_up" id="txt_name_up" maxlength="150" class="liger-textbox"
                        ligerui="width:200" />
                </td>
            </tr>
            <tr>
                <td>
                    排序:
                </td>
                <td>
                    <!--SortIndex-->
                    <input name="txt_rank_up" id="txt_rank_up" value="1">
                </td>
            </tr><tr>
                <td>
                    
                </td>
                <td>
                    <input id="chk_autoFound_up" type="checkbox" class="l-checkbox"  name="chk_autoFound_up" value="" /><label for="chk_autoFound_up">自动生成模板</label> 
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">

        var tree_diver1 = '/LigerUI/lib/LigerUI/skins/icons/archives.gif';
        var tree_folder1 = '/LigerUI/lib/LigerUI/skins/icons/calendar.gif';
        var tree_file1 = '/LigerUI/lib/LigerUI/skins/icons/attibutes.gif';
        var btnJ = null;
        var rank;
        var rank_up;
        var autoFound = $("#chk_autoFound").ligerCheckBox();
        var autoFound_up = $("#chk_autoFound_up").ligerCheckBox();
        var radioJ = $("#j_radio").ligerRadio();
        var radioW = $("#w_radio").ligerRadio();
        var radioM = $("#m_radio").ligerRadio();
        var vn = '<%= ((VersionName)0).ToString() %>';
        //提交保持数据
        function submitForm(dialog) {
            var jdata = $('#add_form').serializeArray();
            //chk_autoFound  txt_name
            jdata[jdata.length] = { name: "chk_autoFound_ar", value: autoFound.getValue() };

            var snode = path_tree.getSelected();
            if ($("#OpAdd")[0].checked) {
                jdata[jdata.length] = { name: "t", value: "AddData" };
            }
            else {
                //所选中节点ID
                jdata[jdata.length] = { name: "tempID", value: snode.data.id };
                jdata[jdata.length] = { name: "t", value: "UpDate1" };
            }

            $.ajax({
                type: "POST",
                url: "/Pages/Template/TemplateDeploy_bak.aspx",
                data: jdata,
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                    $.ligerDialog.waitting('正在保存中,请稍候...', '系统提示');
                },
                error: function (xhr) {
                    $.ligerDialog.closeWaitting();
                    $.ligerDialog.error('网络连接错误!', '系统错误');
                    return false;
                },
                success: function (data) {
                    $.ligerDialog.closeWaitting();
                    if (data.t == "win") {
                        // dialog.hidden();
                        // $.ligerDialog.success(data.v);
                        grid.loadServerData(
                        {
                            t: "GetData",
                            key: $("#txt_key").val()
                        });
                        AddNode(data)
                        $("#txt_name").val("");
                        $("#txt_name").focus();
                        //                        path_tree.clear();
                        //                        path_tree.loadData(null, "/Pages/Template/TemplateDeploy_bak.aspx", { t: "GetData", type: "t" });

                    } else
                        $.ligerDialog.error(data.v, '系统错误');
                }
            });
        }
        function UpNode(data) { 
            
        }
        //添加节点
        function AddNode(data) {
            var _text = $("#txt_name").val();
            var _CASEINFOTYPEID = $('#tree_ajtype').ligerComboBox().getValue();
            var _SSLBBM = $("#tree_sslb").ligerComboBox().getValue();
            var _SSLBMC = $("#tree_sslb").ligerComboBox().getText();
            var _SortIndex = $("#txt_rank").val();
            var _Auto = autoFound.getValue();
            if(_Auto == true)
                _Auto = "Y";
            else
                _Auto = "N";
            var node = path_tree.getSelected();
            var pnode = node;
            if (node) {
                if (node.data.DOSSIERPARENTMEMBER) {
                    var _pnode = path_tree.getDataByID(node.data.DOSSIERPARENTMEMBER);
                    path_tree.selectNode(_pnode);
                    pnode = path_tree.getSelected();
                }
            }
            var nodes = [];
            var n;
            //修改时直接修改数据
            if (!$("#OpAdd")[0].checked) {
                path_tree.update(node.target, { text: _text, CASEINFOTYPEID: node.data.CASEINFOTYPEID, SSLBBM: _SSLBBM, SSLBMC: _SSLBMC, SORTINDEX: _SortIndex, AUTO: _Auto });
            }
            else {
                if ($("#j_radio")[0].checked) {
                    nodes.push({ id: data.k, text: _text, CASEINFOTYPEID: _CASEINFOTYPEID, DOSSIERPARENTMEMBER: "", icon: 'tree_diver1', SSLBBM: _SSLBBM, SSLBMC: _SSLBMC, CATEGORY: "J", SORTINDEX: _SortIndex, AUTO: _Auto });
                    path_tree.append(null, nodes);
                    path_tree.selectNode(data.k);
                }
                else if ($("#m_radio")[0].checked) {
                    nodes.push({ id: data.k, text: _text, CASEINFOTYPEID: pnode.data.CASEINFOTYPEID, DOSSIERPARENTMEMBER: pnode.data.id, icon: 'tree_folder1', SSLBBM: _SSLBBM, SSLBMC: _SSLBMC, CATEGORY: "M", SORTINDEX: _SortIndex, AUTO: _Auto });
                    path_tree.append(pnode.target, nodes);
                }
                else if ($("#w_radio")[0].checked) {
                    nodes.push({ id: data.k, text: _text, CASEINFOTYPEID: pnode.data.CASEINFOTYPEID, DOSSIERPARENTMEMBER: pnode.data.id, icon: 'tree_file1', SSLBBM: _SSLBBM, SSLBMC: _SSLBMC, CATEGORY: "W", SORTINDEX: _SortIndex, AUTO: _Auto });
                    path_tree.append(pnode.target, nodes);
                }
            }
            n = path_tree.getSelected();
            if (n)
                $("#tree_lable").val(n.data.text);
            path_tree.selectNode(node.target);
        }

        //提交保持数据
        function submitUpForm(dialog) {
            var jdata = $('#up_form').serializeArray();
            jdata[jdata.length] = { name: "chk_autoFound_up_ar", value: autoFound_up.getValue() };
            jdata[jdata.length] = { name: "t", value: "UpData" };

            $.ajax({
                type: "POST",
                url: "/Pages/Template/TemplateDeploy_bak.aspx",
                data: jdata,
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                    $.ligerDialog.waitting('正在保存中,请稍候...', '系统提示');
                },
                error: function (xhr) {
                    $.ligerDialog.closeWaitting();
                    $.ligerDialog.error('网络连接错误!', '系统错误');
                    return false;
                },
                success: function (data) {
                    $.ligerDialog.closeWaitting();
                    if (data.t == "win") {
                        dialog.hidden();
                        $.ligerDialog.success(data.v);
                        grid.loadServerData(
                        {
                            t: "GetData",
                            key: $("#txt_key").val()
                        });
                    } else
                        $.ligerDialog.error(data.v, '系统错误');
                }
            });
        }
        function changeIndex() {
            $.ligerDialog.open({ title: '模板顺序调整', url: 'ManagerSortIndex.aspx', width: 670, height: 500,
                isResize: true
            });
        }
        function setCatalogue() {
            InitData();
            $.ligerDialog.open({ title: '模板配置', target: $('#add_div'), width: 670, 
                isResize: false
            });
        }

        function Save() {
            var count = 0;
            $("input:radio").each(function () {
                if (this.checked) {
                    count = 1;
                    if ($(this).val() == "J") {
                        AddJ();
                    }
                    else if ($(this).val() == "M")
                        AddM();
                    else if ($(this).val() == "W")
                        AddW();
                }
            });
            if (count == 0) {
                $.ligerDialog.error('请选择保存类型');
            }
        }

        function AddJ() {
            $("#hidd_Category").val("J");
            $('#key_parent').val("");
            submitForm();
        }
        function AddM() {
            var stree = path_tree.getSelected();
            if (stree == null || stree.length == 0 || stree.data == null) {
                $.ligerDialog.warn('未选择任何卷或目录，请先选择卷或目录再进行增加！', '系统提示');
                return false;
            }
            if (stree.data.CATEGORY == 'W') {
                $.ligerDialog.warn('未选择有效的卷或目录，请先选择卷或目录再进行增加！', '系统提示');
                return false;
            }
            $('#key_parent').val($.trim(stree.data.id));
            $('#hidd_Category').val('M');
            submitForm();
        }
        function AddW() {
            var data = path_tree.getSelected();
            if (data == null || data.length == 0 || data.data == null) {
                $.ligerDialog.warn('未选择任何卷或目录，请先选择卷或目录再进行增加！', '系统提示');
                return false;
            }
            if (data.data.DOSSIERPARENTMEMBER) {
                data = path_tree.getDataByID(data.data.DOSSIERPARENTMEMBER);
            }
            else {
                data = path_tree.getDataByID(data.data.id);
            }
            if (data.CATEGORY == 'W') {
                $.ligerDialog.warn('未选择有效的卷或目录，文件父级只能为卷或目录！', '系统提示');
                return false;
            }
            $('#key_parent').val($.trim(data.id));
            $('#hidd_Category').val('W');
            submitForm();
        }
        function DelData() {
            var arrck = grid.getSelectedRows();
            if (arrck.length > 0) {
                deleteData(arrck[0].id, arrck[0].DOSSIERTYPEDISPLAYMEMBER);
            }
            else {
                $.ligerDialog.warn('请至少选择一行删除', '系统提示');
            }
        }
        function treeDelete() {
            var snode = path_tree.getSelected();
            if (snode) {
                deleteData(snode.data.id, snode.data.DOSSIERTYPEDISPLAYMEMBER);
            } else {
                $.ligerDialog.warn('请选择删除节点', '系统提示');
            }
        }
        function deleteData(id, name) {
            $.ligerDialog.confirm('如果包含目录或者文件一起删除，确定是否删除?', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: '/Pages/Template/TemplateDeploy_bak.aspx',
                        data: { t: "DelData", DossierTypeValueMember: id, name: name },
                        dataType: 'json',
                        timeout: 5000,
                        cache: false,
                        beforeSend: function () { },
                        error: function (xhr) {
                            $.ligerDialog.error('网络连接错误!', '系统错误');
                            return false;
                        },
                        success: function (data) {
                            if (data.t == "win") {
                                $.ligerDialog.success(data.v, '系统提示');
                                if (path_tree && path_tree.getSelected())
                                    path_tree.remove(path_tree.getSelected().data);
                                grid.loadServerData({
                                    t: "GetData",
                                    key: $("#txt_key").val()
                                });
                            } else
                                $.ligerDialog.error(data.v, '系统错误');
                        }
                    });
                }
            });

            LoadSSLB(3);
        }
        function searchData() {

           // InitData();

            GetComboxAjtype();
            var sdata = grid.getSelectedRow();
            if (sdata != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/Template/TemplateDeploy_bak.aspx',
                    data: { t: "GetModelPList", id: sdata.id, name: sdata.DOSSIERTYPEDISPLAYMEMBER },
                    dataType: 'json',
                    timeout: 5000,
                    cache: true,
                    beforeSend: function () {
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误!', '系统错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            //加载所属类别
                            var comboxAjtype = $('#tree_sslb_up').ligerComboBox({
                                url: '/Pages/Template/TemplateDeploy_bak.aspx?t=GetSSLB&LBLX=' + data.FSslbBM,
                                valueField: 'id',
                                textField: 'text',
                                width: 200,
                                selectBoxWidth: 200,
                                selectBoxHeight: 200,
                                autocomplete: true,
                                highLight: true,
                                onSuccess: function () {
                                    $('#tree_sslb_up').ligerComboBox().setValue($.trim(data.SSLBBM));
                                }
                            });

                            
                            if (data.Category == 'M' || data.Category == 'W') {
                                $('#tree_ajtype_up').ligerComboBox().setEnabled(true);
                            }
                            $('#key_hidd_up').val($.trim(data.DossierTypeValueMember));
                            //FSslbBM
                            $('#hidd_Category_up').val($.trim(data.Category));
                            $('#txt_name_up').val($.trim(data.DossierTypeDisplayMember));
                            $('#txt_rank_up').val($.trim(data.SortIndex));
                            $('#tree_ajtype_up').ligerComboBox().setValue($.trim(data.CaseInfoTypeID));
                            $('#txt_dwbm_up').val(sdata.DWMC);
                           
                            if (!data.DossierParentMember)
                                autoFound_up.setEnabled(true);
                            else
                                autoFound_up.setDisabled(true);

                            if (data.Auto == "Y")
                                autoFound_up.setValue(true);
                            else
                                autoFound_up.setValue(false);
                            var title;
                            if (data.Category == "J") {
                                title = '编辑卷';
                            } else if (data.Category == "M") {
                                title = '编辑目录';
                            } else {
                                title = '编辑文件';
                            }
                            $.ligerDialog.open({ title: title, target: $('#up_div'), width: 400,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    submitUpForm(dialog);
                                }, cls: 'l-dialog-btn-highlight'
                                },
                            { text: '取消', onclick: function (item, dialog) {
                                $("#up_form")[0].reset();
                                dialog.hidden();
                            }
                            }], isResize: true
                            });
                        }
                    }
                });
            }
            else
                $.ligerDialog.warn('请先选择一行!', '系统提示');
        }
        function LoadSSLB(type) {
            LoadSSLB(type,'');
        }
        function LoadSSLB(type,selectedValue) {
            var FSSLBBM = "";
            if ($("#j_radio")[0].checked == false) {
                if (!path_tree)
                    return;
                var snode = path_tree.getSelected();
                if (snode == null)
                    return;
                if (snode.data.DOSSIERPARENTMEMBER) {
                    var pnode = path_tree.getDataByID(snode.data.DOSSIERPARENTMEMBER);
                    FSSLBBM = pnode.SSLBBM;
                }
                else
                    FSSLBBM = snode.data.SSLBBM;
            }
            $('#tree_sslb').ligerComboBox().setText('');
            $('#tree_sslb').ligerComboBox().setValue('');
            var comboxAjtype = $('#tree_sslb').ligerComboBox({
                url: '/Pages/Template/TemplateDeploy_bak.aspx?t=GetSSLB&LBLX=' + FSSLBBM,
                valueField: 'id',
                textField: 'text',
                width: 200,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                autocomplete: true,
                highLight: true

            });
            var _name = $("#txt_name").val();
            if (!$("#OpAdd")[0].checked)
                comboxAjtype.setValue(selectedValue);
            $("#txt_name").val(_name);
        }

        function GetComboxAjtype() {
            $('#tree_ajtype_up').ligerComboBox({
                url: '/Pages/Template/TemplateDeploy_bak.aspx?t=GetAJType',
                valueField: 'id',
                textField: 'text',
                width: 200,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                autocomplete: true,
                highLight: true
            });
        }

        function InitData() {
            $('#tree_ajtype').ligerComboBox({
                url: '/Pages/Template/TemplateDeploy_bak.aspx?t=GetAJType',
                valueField: 'id',
                textField: 'text',
                width: 200,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                autocomplete: true,
                highLight: true
            });
            $('#tree_sslb').ligerComboBox({
                onSelected: function (value, text) {
                    var txt = $("#txt_name").val();
                    if (!txt)
                        $("#txt_name").val(text);
                }
            });

            btnJ = $("#btnJ").ligerButton({ click: function () {
                Save();
            }
            });
            $("#btn_delete").ligerButton({ click: function () {
                treeDelete();
            }
            });

        $("#tree_lable").ligerTextBox({ disabled: true });
        $("#tree_dwbm").ligerTextBox({ disabled: true });

            $("#j_radio")[0].checked = true;
            //            $("#j_radio").ligerRadio().setValue(true);
            //            $("#m_radio").ligerRadio().setValue(false);
            //            $("#w_radio").ligerRadio().setValue(false);

            LoadSSLB(1);

            if (path_tree)
                path_tree.clear();
            //加载树
            path_tree = $("#path_tree").ligerTree({
                url: "/Pages/Template/TemplateDeploy_bak.aspx",
                parms: { t: 'GetData', type: 't' },
                isExpand: 1,
                checkbox: false,
                treeLine: true,
                slide: false,
                nodeWidth: 300,
                onClick: function (node) {
                    $("#txt_name").focus();
                    var snode = path_tree.getSelected();
                    // btnJ.setEnabled(true);
                    if (snode == null) {
                        $("#j_radio")[0].checked = true; //选中卷类型                       
                        //引用点击事件
                        $("#j_radio")[0].click();
                        autoFound.setValue(false);
                        $("#txt_name").val('');
                        $("#tree_lable").val("无");
                        btnJ.setEnabled(true);
                        $('#tree_ajtype').ligerComboBox().setEnabled(true);
                        if (path_tree.data && path_tree.data.length > 0) {
                            $("#txt_rank").val(parseInt(path_tree.data[path_tree.data.length - 1].SORTINDEX) + 1);
                        }
                    } else {
                        if (node.data.DOSSIERPARENTMEMBER) {
                            var pnode = path_tree.getDataByID(node.data.DOSSIERPARENTMEMBER);
                        }
                        else {
                            $("#tree_lable").val(snode.data.text);
                            if (snode.data.AUTO == "Y")
                                autoFound.setValue(true);
                            else
                                autoFound.setValue(false);

                        }
                    }
                }
                , onSelect: function (node) {
                    selectedNode();
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                    $('#txt_rank').val(parseInt(data[data.length - 1].SORTINDEX) + 1);
                }
            });
            $('#key_hidd').val('');
            $('#key_parent').val('');
            $('#txt_name').val('');
            $('#tree_ajtype').ligerComboBox().setText('');
            $('#tree_ajtype').ligerComboBox().setEnabled(true);
            $('#tree_sslb').ligerComboBox().setText('');
            $('#tree_sslb').ligerComboBox().setValue(null);
            $('#tree_dwbm').val("<%=UserInfo.DWMC %>");
        }

        var menus = { width: 120, items:
            [{ text: '模板配置', click: setCatalogue, icon: 'settings' },
                { text: '序号调整', click: changeIndex, icon: 'settings' },
               { line: true },
               { text: '修改', click: searchData, icon: 'modify' },
               { line: true },
               { text: '删除', click: DelData, icon: 'delete' }
            ]
        };


        var grid;
        var path_tree;
        function loadGridDate() {
            grid = $("#maingrid").ligerGrid({
                columns: [
                { display: '模板编码', name: 'DOSSIERTYPEVALUEMEMBER', minWidth: 200, hide: 'none' },
                { display: '模板名称', name: 'DOSSIERTYPEDISPLAYMEMBER', minWidth: 100,  },
                { display: '类别', name: 'CATEGORY', minWidth: 100, render: function (item) {
                    if (item.CATEGORY == 'J') return '卷'; else if (item.CATEGORY == 'M') return '目录'; else if (item.CATEGORY == 'W') return '文件';
                    else return '';
                }
                },
                { display: vn+ '类别名称', name: 'CASEINFOTYPENAME', minWidth: 100 },
                { display: '所属分类', name: 'SSLBMC', minWidth: 100 },
                { display: '单位名称', name: 'DWMC', minWidth: 100 },
                { display: '单位编号', name: 'UNITID', minWidth: 100 },
                { display: '是否自动生成模板', name: 'AUTO', minWidth: 150, render:
                function (row) {
                    if (row.AUTO == "Y") {
                        return '是';
                    }
                    else {
                        return '否'; 
                    } 
                } 
                }
                ], rownumbers: true
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/Template/TemplateDeploy_bak.aspx?page=1',
                usePager: false,
                dataAction: "local",
                alternatingRow: false,
                tree: { columnName: 'DOSSIERTYPEDISPLAYMEMBER', idField: 'DOSSIERTYPEVALUEMEMBER', parentIDField: 'DOSSIERPARENTMEMBER' },
                parms: { t: "GetData",
                    key: $("#txt_key").val()
                }, onAfterShowData: function () {
                    var num = 0;
                    if ($("#txt_key").val() != "")
                        num = 1;
                    var l = $(".l-grid-tree-link-open").length;
                    for (var i = l - 1; i >= num; i--) {
                        $(".l-grid-tree-link-open")[i].click();
                    }
                }, toolbar: menus
                , onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }
        var comboxDwbm = null;

        $(function () {

            //IE浏览器 Radio 值兼容问题，无延迟
            if ($.browser.msie) {
                $('input:radio').click(function () {
                    try {
                        this.blur();
                        this.focus();
                    } catch (e) {
                    }
                });
            }

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });

            //点击搜索按钮
            $("#btn_search").click(function () {
                grid.loadServerData(
                {
                    t: "GetData",
                    key: $("#txt_key").val()
                });
            });
            loadGridDate();
            rank = $("#txt_rank").ligerSpinner({ type: 'int', isNegative: false, width: 200 });
            rank_up = $("#txt_rank_up").ligerSpinner({ type: 'int', isNegative: false, width: 200 });


            //监听回车事件
            $(document).unbind("keydown");
            $(document).bind("keydown", function (event) {
                if (event.keyCode == 13) {
                    var t = $("#add_div").is(":visible")
                    if (t) {
                        $("#btnJ").click();
                    }
                    return false;
                }
            });

        });
        $(document).ready(function () {
            //选择保存类型
            $(".l-radio").click(function () {
                if ($(this).val() == "J") {
                    $('#tree_ajtype').ligerComboBox().setEnabled(true);
                    if (path_tree.data && path_tree.data.length > 0) {
                        $("#txt_rank").val(parseInt(path_tree.data[path_tree.data.length - 1].SORTINDEX) + 1);
                    }
                    var snode = path_tree.getSelected();
                    if (snode)
                        LoadSSLB(2, snode.data.SSLBBM);
                    else
                        LoadSSLB(2, "");
                }
                else if ($(this).val() == "M") {
                    $('#tree_ajtype').ligerComboBox().setDisabled(true);
                    LoadSSLB(2);
                }
                else if ($(this).val() == "W") {
                    $('#tree_ajtype').ligerComboBox().setDisabled(true);
                    var snode = path_tree.getSelected();
                    if (snode.data.children && snode.data.children.length > 0) {
                        $("#txt_rank").val(parseInt(snode.data.children[snode.data.children.length - 1].SORTINDEX) + 1);
                    }
                    if (snode)
                        LoadSSLB(2, snode.data.SSLBBM);
                    else
                        LoadSSLB(2, "");
                }
                else if ($(this).val() == "Add") {
                    //新增
                    selectedNode();
                    var snode = path_tree.getSelected();
                    LoadSSLB(2, "");
                }
                else if ($(this).val() == "Edit") {
                    //修改
                    selectedNode();
                    var snode = path_tree.getSelected();
                    if (snode)
                        LoadSSLB(2, snode.data.SSLBBM);
                    else
                        LoadSSLB(2, "");

                }
            });
        });
        function selectedNode() {
            var snode = path_tree.getSelected();
            //设置单选按钮可用
            radioJ.setEnabled(true);
            radioW.setEnabled(true);
            radioM.setEnabled(true);
            if (snode != null) {
                $('#tree_ajtype').ligerComboBox().setDisabled(true);
                //新增 else  修改
                if ($("#OpAdd")[0].checked) {
                    var pnode = path_tree.getDataByID(snode.data.id);
                    if (snode.data.DOSSIERPARENTMEMBER) {
                        pnode = path_tree.getDataByID(snode.data.DOSSIERPARENTMEMBER);
                    }
                    $("#tree_lable").val(pnode.text);
                    $('#tree_ajtype').ligerComboBox().setValue(pnode.CASEINFOTYPEID);
                    $("#txt_name").val("");
                    $("#txt_rank").val(1);
                    if (pnode.children && pnode.children.length > 0) {
                        $("#txt_rank").val(parseInt(pnode.children[pnode.children.length - 1].SORTINDEX) + 1);
                    }
                    //取消选择
                    radioJ.setValue(false);
                    radioW.setValue(false);
                    radioM.setValue(false);
                    $("#w_radio")[0].checked = true; //选中文件类型
                    //引用点击事件
                    $("#w_radio")[0].click();
                }
                else {
                    var pnode = path_tree.getDataByID(snode.data.id);
                    //取消选择
                    radioJ.setValue(false);
                    radioW.setValue(false);
                    radioM.setValue(false);
                    if (pnode.CATEGORY == "W") {
                        $("#w_radio")[0].click();
                        $("#w_radio")[0].checked = true;
                    }
                    if (pnode.CATEGORY == "J") {
                        $("#j_radio")[0].click();
                        $("#j_radio")[0].checked = true;
                    }

                    if (pnode.CATEGORY == "M") {
                        $("#m_radio")[0].click();
                        $("#m_radio")[0].checked = true;
                    }
                    //延迟加载
                    setTimeout(function () {
                        $('#tree_sslb').ligerComboBox().setValue(pnode.SSLBBM);
                    }, 50);
                    $("#tree_lable").val(pnode.text);
                    $('#tree_ajtype').ligerComboBox().setValue(pnode.CASEINFOTYPEID);
                    $("#txt_name").val(pnode.text);
                    $("#txt_rank").val(pnode.SORTINDEX);
                    //设置单选按钮不可用
                    autoFound.setValue(false);
                    if (pnode.AUTO == "Y")
                        autoFound.setValue(true);
                    radioJ.setDisabled(true);
                    radioW.setDisabled(true);
                    radioM.setDisabled(true);
                }

            } else {
                btnJ.setEnabled(true);
                $("#j_radio")[0].checked = true; //选中卷类型

            }
        }
        
    </script>
</body>
</html>
