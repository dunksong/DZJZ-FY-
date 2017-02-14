<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateDeploy.aspx.cs"
    Inherits="WebUI.Pages.Template.TemplateDeploy" %>

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
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>

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
        .l-text-combobox
        {
            height: 21px;
        }
         .l-box-select-inner
        {
            max-height: 300px;
            min-height: 300px;
        }
        .l-box-select-inner .l-tree
        {
            min-width: 400px !important;
        }
         .l-tree-icon
        {
            background:url('/images/icons/3.png') no-repeat !important;
            background-position:center center  !important;
        }
        .cccc
        {
            display: table;
        }
        .cccc li
        {
            float: left;
            padding-left: 10px;
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
        span#butAddDivNew {
            border: none;
            background: #5EA052;
            border-radius: 5px;
            color: white;
            height: 25px;
            line-height: 25px;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px; overflow: hidden;">
    <div id="tb">
        <div>
            <ul class="cccc">
                <li><span>单位名称：</span>
                    <input id="txt_dwbm" type="text" name="txt_dwbm" />
                </li>
                <li><span>业务类别：</span><input id="txt_ajlb" class="l-text" type="text" name="txt_ajlb" />
                </li>
                <li>
                    <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                    </div>
                </li>
            </ul>
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
                    <td>
                        所属单位：
                    </td>
                    <td>
                        <!--DossierParentMember-->
                        <input type="hidden" id="key_parent" name="key_parent" value="" />
                        <!-- DWBM -->
                        <input type="hidden" id="dwbm" name="dwbm" value="" />
                        <input type="text" name="dwmc" id="dwmc" maxlength="150" class="liger-textbox" ligerui="width:200" />
                    </td>
                </tr>
                <tr>
                    <td>
                        业务类别：
                    </td>
                    <td>
                        <input type="hidden" id="ajlbbm" name="ajlbbm">
                        <input type="text" id="ajlbmc" name="ajlbmc" class="liger-textbox" ligerui="width:200">
                    </td>
                </tr>
                <tr>
                    <td>
                        所属类别:
                    </td>
                    <td>
                        <input type="hidden" id="sslbbm" name="sslbbm">
                        <input type="text" id="sslbmc" name="sslbmc" class="liger-textbox" ligerui="width:200">
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
                    </td>
                    <td>
                        <input id="chk_autoFound" type="checkbox" class="l-checkbox" name="chk_autoFound"
                            value="" /><label for="chk_autoFound">自动生成模板</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <div id="btnSave" style="line-height: 23px; display: inline-block;">
                            保 存</div>
                        &nbsp;&nbsp; <span id="btnDel" style="line-height: 23px; margin-top: 10px; display: inline-block;">
                            删 除</span>
                        <div id="sortDiv" style="display: none;">
                            <span id="btn_Up" style="line-height: 16px; margin: 0px; display: inline-block;">
                            </span><span id="btn_Down" style="line-height: 16px; margin: 0px; display: inline-block;">
                            </span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <!--2015-8-25-->
    <div id="divNew1" style="padding: 10px; display: none;">
        <form id="Form1" method="post">
        <div style="float: left; width: 310px; border: 1px solid #A3C0E8; height: 400px;
            overflow: auto;">
            <table style="line-height: 40px; width: 280px; margin-top: 30px; margin-left: 30px;">
                <tr>
                    <td>
                        所属单位：
                    </td>
                    <td>
                        <input type="text" value="" runat="server" disabled="disabled" name="dwmc_add" id="dwmc_add"
                            maxlength="150" class="liger-textbox" ligerui="width:150" style="display: inline-block;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        业务类别：
                    </td>
                    <td>
                        <input name="ajlb_add" id="ajlb_add" ligerui="width:150" />
                    </td>
                </tr>
                <tr>
                    <td>
                        所属类别：
                    </td>
                    <td>
                        <input name="sslb_add" id="sslb_add" ligerui="width:150" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <span id="butAddDivNew" style="line-height: 23px; margin-top: 10px; display: inline-block;">
                            保 存</span>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; height: 360px; width: 250px; border: 1px solid #A3C0E8;
            margin-left: 20px; padding: 20px; overflow: scroll;">
            <ul id="divNewTree">
            </ul>
            <div id="divSS1">
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        var dwmc = "<%=UserInfo.DWMC %>";
        var tree_diver1 = '/LigerUI/lib/LigerUI/skins/icons/archives.gif';
        var tree_folder1 = '/LigerUI/lib/LigerUI/skins/icons/calendar.gif';
        var tree_file1 = '/LigerUI/lib/LigerUI/skins/icons/attibutes.gif';
        var rank;
        var autoFound = $("#chk_autoFound").ligerCheckBox();
        var treeDivTree;
        var vn = '<%= ((VersionName)0).ToString() %>';
        function submitAddForm() {
            var snode = treeDivTree.getChecked();
            var arrayList = new Array();
            var isChecked = false;
            $.each(treeDivTree.nodes, function (i, n) {
                n["auto"] = "N";
                $.each(snode, function (ii, nn) {
                    if (nn.data.id == n.id) {
                        n["auto"] = "Y";
                        arrayList.push(nn.data);
                        isChecked = true;
                    }
                });
                if (!isChecked) {
                    arrayList.push(n);
                    isChecked = false;
                }
            });
            $.ajax({
                type: "post",
                async: false,
                url: "/Pages/Template/TemplateDeploy.aspx?t=AddTemp",
                data: { jsonText: JSON.stringify(eval(arrayList)), sslbbm: sslb_add.getValue(), sslbmc: sslb_add.getText(), ajlbbm: ajlb_add.getValue(), ajlbmc: ajlb_add.getText() }
                , success: function (data) {
                    var result = eval(data);
                    if (result[0].isTrue == "true") {
                        $.ligerDialog.success(result[0].errorMsg);
                        $("#btn_search").click();
                    }
                    else {
                        $.ligerDialog.error('保存模板过程中遇到问题，操作失败！');
                    }
                }
            });
        }
        var lastSelectedNode;
        //提交保持数据
        function submitForm() {

            var snode = path_tree.getSelected();
            lastSelectedNode = snode;
            if (!snode) {
                $.ligerDialog.warn('未选中任何节点！', '系统提示');
                return;
            }
            var jdata = new Array();
            //chk_autoFound  txt_name
            jdata[jdata.length] = { name: "chk_autoFound_ar", value: autoFound.getValue() };
            jdata[jdata.length] = { name: "sslbbm", value: snode.data.id };
            jdata[jdata.length] = { name: "dwbm", value: $("#dwbm").val() };
            jdata[jdata.length] = { name: "ajlbbm", value: $("#ajlbbm").val() };
            jdata[jdata.length] = { name: "txt_rank", value: $("#txt_rank").val() };
            jdata[jdata.length] = { name: "t", value: "UpDate2" };
            $.ajax({
                type: "POST",
                url: "/Pages/Template/TemplateDeploy.aspx",
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
                    if (data.t != "win")
                        $.ligerDialog.error(data.v, '系统错误');
                    else {
                        //$.ligerDialog.success(data.v, '系统提示');
                        var node = grid.getSelected();
                        InitData(node);
                        setTimeout(function () {
                            var _node = path_tree.getDataByID(snode.data.id);
                            path_tree.selectNode(_node);
                        }, 100);
                    }
                }
            });
        }
        function setCatalogue() {

            var node = grid.getSelected();
            if (node == null) {
                $.ligerDialog.warn('未选择任何单位！', '系统提示');
                return false;
            }     
            InitData(node);
            $.ligerDialog.open({ title: '  编辑模板    ' + '( ' + node.qxmc + ' [' + node.ajlbmc + '])', target: $('#add_div'), width: 670,
                isResize: false
            });
        }
        var sslb_add;
        var ajlb_add;
        function addNew1() {

            //ajlb_add
            ajlb_add = $("#ajlb_add").ligerComboBox({
                url: '/Pages/Template/TemplateDeploy.aspx',
                parms: { t: "GetYWLXList" },
                valueFieldID: 'ajlbbm_hidd1',
                selectBoxWidth: 400,
                delay: 2,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });
            ajlb_add.setValue('');
            ajlb_add.setText('');
            //divNewTree
            treeDivTree = $("#divNewTree").ligerTree(
              {

                  checkbox: true,
                  treeLine: true,
                  slide: false,
                  isExpand: 1,
                  nodeWidth: 300

              });
            treeDivTree.clear();
            //sslb_add
            sslb_add = $("#sslb_add").ligerComboBox({
                url: "/Pages/Template/TemplateDeploy.aspx",
                parms: { t: 'GetData' },
//                valueField: "id",
//                textField: "text",
                valueFieldID: 'id',
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true,
                onSelected: function (val) {
               
                    $.ajax({
                        type: "POST",
                        url: "/Pages/Template/TemplateDeploy.aspx",
                        data: { t: 'GetData' },
                        success: function (data) {                       
                            $.each(data, function (i, n) {
                                if (n.id == val) {
                                    treeDivTree.clear();
                                    var nodes = [];
                                    nodes.push(n);
                                    treeDivTree.append(null, nodes);
                                }
                            });
                            treeDivTree.expandAll();
                            
                        }
                    });
                }
            });
            sslb_add.setValue('');
            sslb_add.setText('');
            $("#dwmc_add").val(dwmc);
            $.ligerDialog.open({
                title: '  新增模板 （' + dwmc + '）',
                target: $('#divNew1'),
                width: 670,
                isResize: false
            });
        }
        var AutoIcon = "/images/icons/01664.ico";
        var notAutoIcon = "/images/icons/00752.ico";
        function InitData(node) {
       
            $("#dwbm").val(node.qxbm);
            $("#dwmc").val(node.qxmc);
            $("#ajlbbm").val(node.ajlbbm);
            $("#ajlbmc").val(node.ajlbmc);
            //加载配置-树数据
            path_tree = $("#path_tree").ligerTree({
                url: "/Pages/Template/TemplateDeploy.aspx",
                parms: { t: 'GetLoaclData', dwbm: node.qxbm, ajlbbm: node.ajlbbm },
                idFieldName: 'id',
                checkbox: false,
                treeLine: true,
                slide: true,
                isExpand: 2,
                nodeWidth: 300,
                render: function (data, target) {
                    //自定义显示
                    var text = data.SortIndex + '、' + data.text;
                    return text;
                },
                onSelect: function (data, target) {

                    data = path_tree.getSelected();
                    if (!data)
                        return;
                    //防止重复调用
                    if (data.data.id == $("#sslbbm").val())
                        return;
                    $("#sslbbm").val(data.data.id);
                    $("#sslbmc").val(data.data.text);
                    //查询是否已配置模板,已配置选中，未配置不选
                    $.ajax({
                        type: "POST",
                        url: "/Pages/Template/TemplateDeploy.aspx?t=GetYpzmb&dwbm=" + $("#dwbm").val() + "&ajlbbm=" + $("#ajlbbm").val() + "&sslbbm=" + data.data.id,
                        dataType: 'json',
                        timeout: 10000,
                        cache: false,
                        beforeSend: function () {
                        },
                        error: function (xhr) {
                            return false;
                        },
                        success: function (_data) {
                            if (_data.k != "") {
                                $("#txt_rank").val(_data.k.split(',')[1]);
                                if (_data.k.split(',')[0] == "Y")
                                    autoFound.setValue(true);
                                else
                                    autoFound.setValue(false);
                            }
                        }
                    });

                },
                onCancelselect: function (data, target) {
                    $("#sslbbm").val('');
                    $("#sslbmc").val('');
                    $("#key_parent").val('');
                    $("#txt_rank").val(0);
                    autoFound.setValue(false);
                }
            });
        }

        var menus = { width: 120, items:
            [{ text: '编辑模板', click: setCatalogue, img: '../../images/NewAdd/bjmb.png'},
            { text: '新增模板', click: addNew1, img: '../../images/NewAdd/addmb.png' }
            ]
        };


        var grid;
        var path_tree;
        function loadGridDate() {
            grid = $("#maingrid").ligerGrid({
                columns: [
                { display: '单位编码', name: 'qxbm', minWidth: 100, hide: 'none' },
                { display: '单位名称', name: 'qxmc', minWidth: 200,  },
                { display: vn+ '类别编码', name: 'ajlbbm', minWidth: 100, hide: 'none' },
                { display: vn+ '类别名称', name: 'ajlbmc', minWidth: 200,  },
                { display: '是否存在自动生成', name: 'autonum', minWidth: 150, isSort: false, render:
                function (row) {
                    if (row.autonum > 0) {
                        return '是';
                    }
                    else {
                        return '否';
                    }
                }
                }
                ], rownumbers: true
                , width: '100%', height: '100%', heightDiff: -20,       //服务器分页
                url: '/Pages/Template/TemplateDeploy.aspx?page=1',
                pageSizeOptions: [20, 50, 100, 500], pageSize: 50,
                usePager: true,
               // dataAction: "server",
                alternatingRow: false,
                parms: {
                    t: "GetDwAjData",
                    dwbm: dwbm_tree.getValue(), //$("#dwbm_hidd").val(),
                    ajlb: $("#ajlbbm_hidd").val()
                }, toolbar: menus
                , onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onReload: function () {
                    gridSetParm();
                },
                onToFirst: function (element) {
                    gridSetParm();
                },
                onToPrev: function (element) {
                    gridSetParm();
                },
                onToNext: function (element) {
                    gridSetParm();
                },
                onToLast: function (element) {
                    gridSetParm();
                },
                onChangeSort: function (element) {
                    gridSetParm();
                }
            });
        }
        var comboxDwbm = null;

        function gridSetParm() {
            grid.setParm("dwbm", dwbm_tree.getValue());
            grid.setParm("ajlb", $("#ajlbbm_hidd").val());
        }
        var btnSave;
        var btnAddNew1;
        var dwbm_tree;
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });
            btnSave = $('#btnSave').ligerButton({
                text: '保存'
            });
            $('#btnDel').ligerButton({
                text: '删 除'
            });
            $('#btn_Up').ligerButton({
                text: '',
                width: 10,
                icon: '/LigerUI/lib/LigerUI/skins/icons/up.ico'
            });
            $('#btn_Down').ligerButton({
                text: '',
                width: 10,
                icon: '/LigerUI/lib/LigerUI/skins/icons/down.ico'
            });
            $('#btn_Up').click(function () {
                var node = path_tree.getSelected();
                if (node) {
                    $.each(path_tree.nodes, function (i, n) {
                        if (n.id == node.data.id)
                            path_tree.nodes[i].treedataindex = 0;
                    });
                    path_tree.refreshTree();
                }
            });
            $("#btnDel").click(function () {

                var snode = path_tree.getSelected();
                if (!snode) {
                    $.ligerDialog.warn('请选择删除节点', '系统提示');
                    return;
                }
                $.ligerDialog.confirm('如果包含下级将一起删除，确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/Template/TemplateDeploy.aspx',
                            data: { t: "DelData", sslbbm: snode.data.id, ajlbbm: $("#ajlbbm").val(), dwbm: $("#dwbm").val() },
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
                                } else
                                    $.ligerDialog.error(data.v, '系统错误');
                            }
                        });
                    }
                });
            });
            btnAddNew1 = $('#butAddDivNew').ligerButton({
                text: '保存'
            });
            $('#butAddDivNew').click(function () {
                submitAddForm();
            });
            $('#btnSave').click(function () {
                submitForm();
            });
            //点击搜索按钮
            $("#btn_search").click(function () {
                grid.changePage("first"); //重置到第一页         
                grid.loadServerData(
                {
                    t: "GetDwAjData",
                    dwbm: dwbm_tree.getValue(),
                    ajlb: $("#ajlbbm_hidd").val(),
                    page: 1,
                    pagesize: grid.options.pageSize
                });
            });

           var tree_node;
            var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { text: '全选/反选', click: function itemclick() {
                var sNode = tree_node;
                if (sNode == null) {
                    $.messager.alert('提示', '未选择任何节点，操作失败！', 'error');
                    return;
                }

                $(".l-expandable-close", sNode.target).click();
                if ($(".l-checkbox-checked", sNode.target).length == 0) {//未选中
                    $(".l-checkbox", sNode).removeClass("l-checkbox-checked").addClass("l-checkbox-unchecked");
                    $(".l-checkbox-unchecked", sNode.target).click();
                }
                else {
                    $(".l-checkbox", sNode).removeClass("l-checkbox-unchecked").addClass("l-checkbox-checked");
                    $(".l-checkbox-checked", sNode.target).click();
                }
            } 
            }
            ]
            });

            //单位编码
            dwbm_tree = $("#txt_dwbm").unitJuris({width:130,checkbox:true});

            
            //案件类别
            $("#txt_ajlb").ligerComboBox({
                url: '/Pages/Template/TemplateDeploy.aspx',
                parms: { t: "GetYWLXList" },
                valueFieldID: 'ajlbbm_hidd',
                selectBoxWidth: 400,
                selectBoxHeight: 300,
                autocomplete: true,
                highLight: true
            });
            //加载Grid数据
            loadGridDate();
            rank = $("#txt_rank").ligerSpinner({ type: 'int', isNegative: false, width: 200 });
            rank_up = $("#txt_rank_up").ligerSpinner({ type: 'int', isNegative: false, width: 200 });
            $("#ajlbmc").ligerTextBox().setDisabled(true);
            $("#dwmc").ligerTextBox().setDisabled(true);
            $("#sslbmc").ligerTextBox().setDisabled(true);
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
        
    </script>
</body>
  <script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
