var dNode;    /*tree的顶级节点*/
var sExpandNode; /*tree要选中并展开的节点*/
var jsNode; /*tree要选中的角色节点*/

var tree_dw = '/images/icons/3.png';
var tree_bm = '/images/icons/bm.png';
var tree_js = '/images/icons/4.png';

$(document).ready(function () {

    //初始化控件
    initControl();

    //注册控件事件
    RegistEvent();

});

//初始化控件
function initControl() {
    $("#dWBmJs").tree({
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetDwBmJsByDwbm',
        method: 'post',
        lines: true,
        onDblClick: function (node) {
            if (node.state == "closed") {
                $("#dWBmJs").tree('expand', node.target);
            }
            else {
                $("#dWBmJs").tree('collapse', node.target);
            }
        },
        onLoadSuccess: function (node, data) {
            var dwbm = parent.userInfo.DWBM;
            dNode = $('#dWBmJs').tree('find', 'a' + dwbm); //获取顶级节点
            $('#dWBmJs').tree('select', dNode.target);
            $('#dWBmJs').tree('expand', dNode.target);

            $('#rNorth').panel('refresh', '/View/ZZJG/ZzjgSelectDw.htm');
            resizeDgRyInfoHeight();
            resizeDgRyInfoWidth();

            //人员信息面板拖动事件
            $('#pRYInfo').panel({
                onResize: function (width, height) {
                    resizeDgRyInfoHeight();
                    resizeDgRyInfoWidth();
                }
            });

            $('#zzjgMainLayWest').panel({
                maxWidth: 200,
                minWidth: 150
            });

            if (!isNull(sExpandNode)) {
                ExpandNodeByMc(sExpandNode);
                sExpandNode = '';
            }
        }
    });


    $('#dgRyInfo').datagrid({
        fitColumns: true,
        columns: [[
        { field: 'Mc', title: '名称', width: 100 },
        { field: 'Dlbm', title: '登陆别名', width: 100 },
        { field: 'Gzzh', title: '工作证号', width: 100 },
        { field: 'Gh', title: '工号', width: 100 },
        { field: 'Jsmc', title: '角色名称', width: 100 },
        { field: 'Xb', title: '性别', width: 80 },
        { field: 'Yddhhm', title: '移动电话号码', width: 100 },
        { field: 'Sftz', title: '是否停职', width: 100 },
        { field: 'Bmbm', title: '部门编码', width: 100 },
        { field: 'Jsbm', title: '角色编码', width: 100 },
        { field: 'Sfsc', title: '是否删除', width: 50, align: 'center' },
        { field: 'action', title: '操作', width: 100, align: 'center',
            formatter: function (value, row, index) {
                //var e = '<a href="#" onclick="TransJob(' + index + ')">调岗</a> ';
                var d = '<a href="#" onclick="RemoveJob(' + index + ')">解除</a>';
                return d;
            }
        }
        ]]
    });

    /*
    *隐藏编码列
    */
    $('#dgRyInfo').datagrid('hideColumn', 'Bmbm');
    $('#dgRyInfo').datagrid('hideColumn', 'Jsbm');


    loadGrid('', '', '', '', '');
    /*
    *添加人员窗口
    */
    $('#winAddRy').window({
        width: 500,
        height: 400,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '新增人员信息'
    });

    /*
    * 添加人员时默认显示部门信息
    */
    $('#inCbbm').combobox({
        valueField: 'id',
        textField: 'text',
        onLoadSuccess: function () {
            var data = $('#inCbbm').combobox('getData');
            if (data.length > 0) {
                $('#inCbbm').combobox('select', data[0].id);
            }
        }
    });
    /*
    * 添加人员时默认显示角色信息
    */
    $('#inJs').combobox({
        valueField: 'id',
        textField: 'text',
        onLoadSuccess: function () {
            var data = $('#inJs').combobox('getData');
            if (data.length > 0) {
                $('#inJs').combobox('select', data[0].id);
            }
        }
    });

    $('#win_zzjgMain').window({
        width: 950,
        height: 490,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '添加人员信息'
    });

}
var __bm;
var __js;
/*
*公共注册控件事件
*/
function RegistEvent() {
    /*
    * 点击树形控件页面跳转
    */
    $('#dWBmJs').tree({
        onSelect: function (node) {
            G_ZzjgNode = node;
            var bm = node.id;
            var mc = node.text;
            var bits = getLength(bm.toString());
            var rootNode = $('#dWBmJs').tree('getRoot', node.target);
            G_ZzjgDwbm = rootNode.id.substring(1);
            if (bits == 4) {
                $('#btnOpenRy_zzjgMain').textbox({ disabled: false });
                jsNode = node;
                //获取父节点
                var pNode = $('#dWBmJs').tree('getParent', node.target);
                //加载角色页面
                $('#rNorth').panel('refresh', '/View/ZZJG/ZzjgSelectJs.htm');
                //刷新人员信息
                __js = bm;
                __bm = pNode.id;
                loadGrid(pNode.id, bm, '', '', '');
            }
            else if (bits == 5) {
                $('#btnOpenRy_zzjgMain').textbox({ disabled: true });
                //加载单位页面
                $('#rNorth').panel('refresh', '/View/ZZJG/ZzjgSelectBm.htm');
                //刷新人员信息
                __bm = bm;
                loadGrid(bm, '', '', '', '');
            }
            else if (bits == 7) {
                $('#btnOpenRy_zzjgMain').textbox({ disabled: true });
                //加载单位页面
                $('#rNorth').panel('refresh', '/View/ZZJG/ZzjgSelectDw.htm');
                //刷新人员信息            
                loadGrid('', '', '', '', '');
            }
        }
    });

    /*
    * 收索按钮事件
    */
    $('#btnSearchRy1_zzjgMain').textbox({
        onClickButton: function () {
            var xm = trim($('#xm').textbox('getValue'));
            var gh = trim($('#gh').textbox('getValue'));
            var gzzh = trim($('#gzzh').textbox('getValue'));
            var bm = $('#dWBmJs').tree('getSelected');
            if (getLength(bm.id.toString()) == 7) {
                loadGrid('', '', xm, gh, gzzh);
            }
            if (getLength(bm.id.toString()) == 5) {
                loadGrid(bm.id, '', xm, gh, gzzh);
            }
            if (getLength(bm.id.toString()) == 4) {
                //获取父节点
                var bmbm = $('#dWBmJs').tree('getParent', bm.target);
                loadGrid(bmbm.id, bm.id, xm, gh, gzzh);
            }
        }
    });

    $('#btnSearchRy2_zzjgMain').textbox({
        onClickButton: function () {
            loadRyInfo_zzjgMain();
        }
    });

    /*
    *向服务器发送添加人员信息
    */
    $('#btnOpenRy_zzjgMain').textbox({
        disabled: true,
        onClickButton: function () {
            loadRyInfo_zzjgMain();
            $('#win_zzjgMain').window('open');
        }
    });

    $('#btnAddRy_zzjgMain').textbox({
        onClickButton: function () {
            var ghj = '';
            var data = $('#dgRy_zzjgMain').datagrid('getChecked');
            for (var i = 0; i < data.length; i++) {
                ghj = ghj + ',' + data[i].gh;
            }
            if (ghj == '') {
                return;
            }
            var pNode = $('#dWBmJs').tree('getParent', jsNode.target);
            var bmbm = pNode.id;
            var jsbm = jsNode.id;
            $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action: "AddRYJSFP", dwbm: G_ZzjgDwbm, ghj: ghj, bmbm: bmbm, jsbm: jsbm
            },
                function (result) {
                    $('#win_zzjgMain').window('close');
                    //刷新数据
                    $('#dgRyInfo').datagrid('load');
                });
        }
    });
}

function loadRyInfo_zzjgMain() {
    var pNode = $('#dWBmJs').tree('getParent', jsNode.target);
    var bmbm = pNode.id;
    var jsbm = jsNode.id;
    var queryData = { action: 'GetWfpRyInfo', dwbm: G_ZzjgDwbm, xm: encodeURI($('#xm_zzjgMain').val()), gh: $('#gh_zzjgMain').val(), bmbm: bmbm, jsbm: jsbm };
    var time = new Date();
    $('#dgRy_zzjgMain').datagrid({
        width: 'auto',
        //height: gridHeigth,
        title: '人员信息',
        striped: true,
        singleSelect: false,
        queryParams: queryData,
        pagination: true,
        rownumbers: true,
        pageSize: 20,
        url: '/Handler/ZZJG/ZZJGHandler.ashx?t=' + time.getMilliseconds(),
        loadMsg: '数据加载中，请稍后...'
        //onDblClickRow: onDblClickRow
    });

    $('#dgRy_zzjgMain').datagrid('getPager').pagination({
        pageList: [10, 20, 30, 50, 100],
        beforePageText: '第',
        afterPageText: '页   共{pages}页',
        displayMsg: '当前显示【{from} ~ {to}】条记录   共【{total}】条记录'
    });
}

/*
*   载入grid数据
*/
function loadGrid(bmbm, jsbm, xm, gh, gzzh) {
    //判断未获取编号不执行
    if (!G_ZzjgDwbm)
        return false;
    $('#dgRyInfo').datagrid({
        width: 'auto',
        striped: true,
        singleSelect: false,
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetRyInfo&dwbm=' + G_ZzjgDwbm + '&xm=' + encodeURI(xm) + '&gh=' + gh + '&gzzh=' + gzzh
                + '&bmbm=' + bmbm + '&jsbm=' + jsbm,
        loadMsg: '数据加载中，请稍后...',
        pagination: true,
        rownumbers: true
    });

    $('#dgRyInfo').datagrid('getPager').pagination({
        pageSize: 10,
        pageList: [10, 20, 30, 50, 100],
        beforePageText: '第',
        afterPageText: '页   共{pages}页',
        displayMsg: '当前显示【{from} ~ {to}】条记录   共【{total}】条记录'
    });
}

/*
* 刷新树形控件数据
*/
function refreshTree() {
    $('#dWBmJs').tree('options').url = "/Handler/ZZJG/ZZJGHandler.ashx?action=GetDwBmJsByDwbm";
    $('#dWBmJs').tree('reload');
}



/*
* 调岗操作
*/
function TransJob(index) {
    /*
    * 清空所有选中的行
    * 让该行高亮
    */
    $('#dgRyInfo').datagrid('clearSelections');
    $('#dgRyInfo').datagrid('highlightRow', index);

    var rowDatas = $('#dgRyInfo').datagrid('getRows');
    var gh = rowDatas[index].Gh;
    document.getElementById('inbmbm_ZzjgMain').value = rowDatas[index].Bmbm;
    document.getElementById('injsbm_ZzjgMain').value = rowDatas[index].Jsbm;
    //查找人员信息
    $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=GetRyinfoByGh", { dwbm: G_ZzjgDwbm, gh: gh },
         function (data) {
             if (!isNull(data)) {
                 var arrRyinfo = new Array();
                 arrRyinfo = data.split(',', 8);
                 for (var i = 0; i < arrRyinfo.length; i++) {
                     var tmp = arrRyinfo[i].split(':', 2);
                     matchContrl(tmp);
                 }
             }
         });
    $('#winAddRy').window({
        title: '人员调岗'
    });
    $('#winAddRy').window('open');
    //设置提交按钮文本
    $('#btnSubmitRy').textbox({
        buttonText: '调岗'
    });
}

/*
* 解除人员岗位
*/
function RemoveJob(index) {
    /*
    * 清空所有选中的行
    * 让该行高亮
    */
    $('#dgRyInfo').datagrid('clearSelections');
    $('#dgRyInfo').datagrid('highlightRow', index);

    var rowDatas = $('#dgRyInfo').datagrid('getRows');
    var gh = rowDatas[index].Gh;
    var bmbm = rowDatas[index].Bmbm;
    var jsbm = rowDatas[index].Jsbm;
    $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=RemoveJob", { dwbm: G_ZzjgDwbm, gh: gh, bmbm: '1' + bmbm, jsbm: '1' + jsbm },
        function (result) {
            if (result != '') {
                Alert("操作结果: " + result);
            }
        });
    //刷新数据
    $('#dgRyInfo').datagrid('load');
}

/*
* 重置人员信息的高度
*/
function resizeDgRyInfoHeight() {
    var panelRYInfoHeight = $('#rLay').layout('panel', 'center').height();
    var h = $('#divRyReseach').height();
    h = h + 50;

    $('#dgRyInfo').datagrid('options').height = panelRYInfoHeight - h;
    $('#dgRyInfo').datagrid('resize');
}

/*
* 重置人员信息的宽度
*/
function resizeDgRyInfoWidth() {
    var panelRYInfoWidth = $('#panleDgZzjgMain').width();
    $('#dgRyInfo').datagrid('options').width = panelRYInfoWidth;
    $('#dgRyInfo').datagrid('resize');
}

/*s
* 匹配控件进行赋值
*/
function matchContrl(data) {
    switch (data[0]) {
        case "gh": $('#inGh').textbox('setValue', data[1]);
            break;
        case "mc": $('#inMc').textbox('setValue', data[1]);
            break;
        case "dlbm": $('#inDlbm').textbox('setValue', data[1]);
            break;
        case "dlbm": $('#inDlbm').textbox('setValue', data[1]);
            break;
        case "gzzh": $('#inGzzh').textbox('setValue', data[1]);
            break;
        case "xb":
            {
                var xb = data[1];
                if (xb == "男") {
                    $("input[name='xb'][value=1]").attr("checked", true);
                }
                else {
                    $("input[name='xb'][value=0]").attr("checked", true);
                }
            }
            break;
        case "sflsry":
            {
                var lsry = data[1]
                if (lsry == "Y") {
                    $("input[name='isLsry'][value=Y]").attr("checked", true);
                }
                else {
                    $("input[name='isLsry'][value=N]").attr("checked", true);
                }
            }
            break;
        case "yddhhm": $('#inYdDhhm').textbox('setValue', data[1]);
            break;
        case "dzyj": $('#inDzyx').textbox('setValue', data[1]);
            break;
        case "caid": $('#inCAID').textbox('setValue', data[1]);
            break;
    }
}

/*
* 清空添加人员信息窗口的控件值
*/
function clearAddRyInfo() {
    $('#inMc').textbox('setValue', '');
    $('#inDlbm').textbox('setValue', '');
    $('#inGh').textbox('setValue', '');
    $('#inGzzh').textbox('setValue', '');
    $('#inYdDhhm').textbox('setValue', '');
    $('#inDzyx').textbox('setValue', '');
    $('#inCAID').textbox('setValue', '');
    document.getElementById('inbmbm_ZzjgMain').value = '';
    document.getElementById('injsbm_ZzjgMain').value = '';
}

//通过名称展开节点
function ExpandNodeByMc(mc) {
    if (isNull(mc)) {
        return;
    }
    var rootNotes = $('#dWBmJs').tree('getRoots'), children;
    for (var i = 0; i < rootNotes.length; i++) {
        if (rootNotes[i].text == mc) {
            //如果匹配到则展开选中节点
            $('#dWBmJs').tree('select', rootNotes[j].target);
            $('#dWBmJs').tree('expand', rootNotes[j].target);
            return;
        }
        children = $('#dWBmJs').tree('getChildren', rootNotes[i].tartget);
        for (var j = 0; j < children.length; j++) {
            if (children[j].text == mc) {
                //如果匹配到则选中节点，展开父节点
                $('#dWBmJs').tree('select', children[j].target);
                //$('#dWBmJs').tree('expand', children[j].target);
                var pNode = $('#dWBmJs').tree('getParent', children[j].target);
                $('#dWBmJs').tree('expand', pNode.target);
                return;
            }
        }
    }
}