var menu;//右键菜单
$(document).ready(function () {
    //initControl();

    $('#btn_dwAdd').linkbutton({
        iconCls: 'icon-add'
    });
    $('#btn_dwDel').linkbutton({
        iconCls: 'icon-remove'
    });
    $('#btn_LBAdd').linkbutton({
        iconCls: 'icon-add'
    });
    $('#btn_LBDel').linkbutton({
        iconCls: 'icon-remove'
    });
    $('#DWpanel').panel({
        width: 360,
        height: 345
    });
    menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
        [
        { text: '全选/反选', click: itemclick, icon: 'remove' }
        ]
    });
    function itemclick(item, i) {
        var sNode = dw_treeNode; //dw_tree.getSelected();
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

    /*
    * 增加单位权限
    */
    $('#btn_dwAdd').click(function () {

        //        var nodes = $("#tree_left").tree("getChecked");
        var temp = new Array();
        var rows = $('#List_Dw').ligerGrid().data.Rows;
        for (var j = 0; j < tree_dw_Data.length; j++) {
            var has = false;
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].qxbm == tree_dw_Data[j].ID) {
                    has = true;
                    break;
                }
            }
            if (!has) {
                temp.push(tree_dw_Data[j]);
            }
        }

        $.ajax({
            type: "post",
            async: false,
            url: "/Handler/ZZJG/ZZJGHandler.ashx?action=ADDDWQX",
            data: { jsonText: JSON.stringify(eval(temp)), _jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), _bmbm: $("#bmbm").val() }
                , success: function (data) {
                    var result = eval(data);
                    if (result[0].isTrue == "true") {
                        $.ligerDialog.success(result[0].errorMsg);
                        tree_dw_Data.length = 0;
                        IsReLoad();
                    }
                    else {
                        $.ligerDialog.error('操作权限过程中遇到问题，操作失败！');
                    }
                }
        });

        //        for (i = 1; i <= nodes.length; i++) {

        //            //alert(dw_tree.getDataByID(nodes[i-1].data.id));
        //            $.ajax({
        //                type: "post",
        //                async: false,
        //                url: "/Handler/ZZJG/ZZJGHandler.ashx?action=ADDDWQX",
        //                data: { dwbm: nodes[i - 1].id, jsbm: $("#jsbm").val(), dwmc: nodes[i - 1].text, _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val() },
        //                success: IsReLoad(i, nodes.length)
        //            });
        //            $("#tree_left").tree("remove", nodes[i - 1].target);
        //        }
        //完成后刷新数据
    });

    /*
    * 减少 单位权限
    */
    $('#btn_dwDel').click(function () {
        var nodes = $('#List_Dw').ligerGrid().getSelectedRows();

        $.ligerDialog.confirm('请确认是否删除？', function (yes) {
            if (yes) {
                $.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/ZZJG/ZZJGHandler.ashx?action=DELDWQX",
                    data: { jsonText: JSON.stringify(nodes), _jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), _bmbm: $("#bmbm").val() },
                    success: function (data) {
                        var result = eval(data);
                        if (result[0].isTrue == "true") {
                            $.ligerDialog.success(result[0].errorMsg);
                            IsReLoad();
                            //                    setTimeout(function () {
                            //                        $("#tree_left").deploy(true, false, true);
                            //                    }, 50)
                        }
                        else {
                            $.ligerDialog.error('操作权限过程中遇到问题，操作失败！');
                        }
                    }
                });
            }
        });
        //        for (i = 1; i <= nodes.length; i++) {
        //            $.ajax({
        //                type: "post",
        //                async: false,
        //                url: "/Handler/ZZJG/ZZJGHandler.ashx?action=DELDWQX",
        //                data: { dwbm: nodes[i - 1].QXBM, jsbm: nodes[i - 1].JSBM, _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val() },
        //                success: IsReLoad(i, nodes.length)
        //            });
        //        }
        //完成后刷新数据
    });


    /*
    * 增加 分类权限
    */
    $('#btn_LBAdd').click(function () {
        var nodes = $('#List_AJLB').ligerGrid().getSelectedRows();
        $.ajax({
            type: "post",
            async: false,
            datatype: "json",
            url: "/Handler/ZZJG/ZZJGHandler.ashx?action=ADDLBQX",
            data: { jsonText: JSON.stringify(nodes), _jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), _bmbm: $("#bmbm").val() },
            success: function (data) {
                var result = eval(data);
                if (result[0].isTrue == "true") {
                    $.ligerDialog.success(result[0].errorMsg);
                    IsReLoad1()
                }
                else {
                    $.ligerDialog.error('操作权限过程中遇到问题，操作失败！');
                }
            }
        });
        //        for (i = 1; i <= nodes.length; i++) {
        //            $.ajax({
        //                type: "post",
        //                async: false,
        //                datatype: "json",
        //                url: "/Handler/ZZJG/ZZJGHandler.ashx?action=ADDLBQX",
        //                data: { lbbm: nodes[i - 1].AJLBBM, jsbm: $("#jsbm").val(), lbmc: nodes[i - 1].AJLBMC, _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val() },
        //                success: IsReLoad1(i, nodes.length)
        //            });
        //        }

    });
    /*
    * 删除 分类权限
    */
    $('#btn_LBDel').click(function () {
        var nodes = $('#List_AJLBQX').ligerGrid().getSelectedRows();

        $.ligerDialog.confirm('请确认是否删除？', function (yes) {
            if (yes) {
                $.ajax({
                    type: "post",
                    async: false,
                    datatype: "json",
                    url: "/Handler/ZZJG/ZZJGHandler.ashx?action=DELLBQX",
                    data: { jsonText: JSON.stringify(nodes), _jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), _bmbm: $("#bmbm").val() },
                    success: function (data) {
                        var result = eval(data);
                        if (result[0].isTrue == "true") {
                            $.ligerDialog.success(result[0].errorMsg);
                            IsReLoad1();
                        }
                        else {
                            $.ligerDialog.error('操作权限过程中遇到问题，操作失败！');
                        }
                    }
                });
            }
        });
        //        for (i = 1; i <= nodes.length; i++) {
        //            $.ajax({
        //                type: "post",
        //                async: false,
        //                datatype: "json",
        //                url: "/Handler/ZZJG/ZZJGHandler.ashx?action=DELLBQX",
        //                data: { lbbm: nodes[i - 1].QXBM, jsbm: nodes[i - 1].JSBM, _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val() },
        //                success: IsReLoad1(i, nodes.length)
        //            });
        //        }
        //jsonText: JSON.stringify(nodes)
    });
})
function IsReLoad() {
    setTimeout(ReLoadDw, 10);
}
function IsReLoad1() {
    setTimeout(ReLoadLB, 10);
}
function ReLoadDw() {
    search3();
    search4();
}
function ReLoadLB() {

    search1();
    search2();
}
var tree_dw_Data = new Array();
var dw_tree;
var dw_treeNode;
var GridList_Dw;
var GridList_AJLB;
var GridList_AJLBQX;

//初始化控件
function initControl() {

    //单位
    //_dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val()
    search3();
    //已分配单位权限列表
    search4();
//    $("#List_Dw").datagrid({
//        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetDWQX&jsbm=' + $("#jsbm").val() + "&_dwbm=" + $("#dwbm").val() + "&bmbm=" + $("#bmbm").val(),
//        method: 'post',
//        checkOnSelect: true
//    });

    //案件分类
    search1();
//    $("#List_AJLB").datagrid({
//        
//        method: 'post',
//        checkOnSelect: true
//    });
    //已分配案件分类权限列表
    search2();
//    $("#List_AJLBQX").datagrid({
//        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetLBQX&jsbm=' + $("#jsbm").val() + "&_dwbm=" + $("#dwbm").val() + "&bmbm=" + $("#bmbm").val(),
//        method: 'post',
//        checkOnSelect: true
//    });
}

function search1() {
    var key = $("#type_name1").val();
    var queryString = { action: "GetAJLB", jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val(), key: key };
    GridList_AJLB = $("#List_AJLB").ligerGrid({
        url: '/Handler/ZZJG/ZZJGHandler.ashx',
        parms: queryString,
        columns: [
        { display: vn+ '分类编码', name: 'ajlbbm', width: 100, minWidth: 60 },
        { display: vn+ '分类名称', name: 'ajlbmc', width: 200, minWidth: 60 }
        ], checkbox: true,
        rownumbers: true,
        pageSizeOptions: [20, 50, 100, 500],
        pageSize: 50,
        width: 360,
        height: 351,
        usePager: true,
        dataAction: 'local'
    });
}

function search2() {
    var key = $("#type_name2").val();
    var queryString = { action: "GetLBQX", jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val(), key: key };
    GridList_AJLBQX = $("#List_AJLBQX").ligerGrid({
        url: '/Handler/ZZJG/ZZJGHandler.ashx',
        parms: queryString,
        columns: [
        { display: vn+'分类编码', name: 'qxbm', width: 100, minWidth: 60 },
        { display: vn+'分类名称', name: 'qxmc', width: 200, minWidth: 60 }
        ], checkbox: true,
        rownumbers: true,
        pageSizeOptions: [20, 50, 100, 500],
        pageSize: 50,
        width: 360,
        height: 351,
        usePager: true,
        dataAction: 'local'
    });
}

function search3() {
    var key = $("#unit_name1").val();
    var queryString = { action: "GetAllDwBm", jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val(), key: key };
    var expandIndex = 2;
    if(key.length > 0)
    {
        expandIndex = false;//条件查询时，默认不展开
    }
    tree_dw_Data = new Array();
    $("#tree_DW").ligerTree(
    {
        url: '/Handler/ZZJG/LigerUIDate.ashx?action=GetAllDwBm&jsbm=' + $("#jsbm").val() + "&_dwbm=" + $("#dwbm").val() + "&bmbm=" + $("#bmbm").val() + "&key=" + escape(key),
        idFieldName: 'id',
        treeLine: true,
        nodeDraggable: false,
        nodeWidth: 200,
        btnClickToToggleOnly: true,
        autoCheckboxEven: false,
        isExpand: expandIndex,
        delay: [2, 3, 4],
        onContextmenu: function (node, e) {
            actionNodeID = node.data.text;
            dw_treeNode = node;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheck: function (node, checked) {
            var jsonStr;
            jsonStr = { ID: node.data.id, Text: node.data.text };
            if (checked) {
                for (var i = 0; i < tree_dw_Data.length; i++) {
                    if (tree_dw_Data[i].ID == node.data.id) {
                        return;
                    }
                }
                tree_dw_Data.push(jsonStr);
            } else {
                var temp = new Array();
                for (var i = 0; i < tree_dw_Data.length; i++) {
                    if (tree_dw_Data[i].ID != node.data.id) {
                        temp.push(tree_dw_Data[i]);
                    }
                }
                tree_dw_Data = temp;
            }
            //选中默认展开
            //            if (checked) {
            //                $(".l-expandable-close", node.target).click();
            //                $(".l-checkbox-unchecked", node.target).click();
            //            }
        }
    });
    dw_tree = $("#tree_DW").ligerGetTreeManager();
}

function search4() {
    var key = $("#unit_name2").val();
    var queryString = { action: "GetDWQX", jsbm: $("#jsbm").val(), _dwbm: $("#dwbm").val(), bmbm: $("#bmbm").val(), key: key };
    GridList_Dw = $("#List_Dw").ligerGrid({
        url: '/Handler/ZZJG/ZZJGHandler.ashx',
        parms: queryString,
        columns: [
        { display: '单位编码', name: 'qxbm', width: 100, minWidth: 60 },
        { display: '单位名称', name: 'qxmc', width: 200, minWidth: 60 }
        ], checkbox: true,
        rownumbers: true,
        pageSizeOptions: [20, 50, 100, 500],
        pageSize: 50,
        width: 360,
        height: 351,
        usePager: true,
        dataAction: 'local'
    });
}