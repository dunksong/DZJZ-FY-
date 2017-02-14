var bmbm_ZzjgSelectJs = '';
var bmmc_ZzjgSelectJs = '';
var jsbm_ZzjgSelectJs = '';
var jsmc_ZzjgSelectJs = '';
var jsxh_ZzjgSelectJs = '';
var gnbm_ZzjgSelectJs = '';

$(document).ready(function () {
    initContrl_ZzjgSelectJs();

    registEvent_ZzjgSelectJs();
});

/*
* 初始化控件
*/
function initContrl_ZzjgSelectJs() {

    var node = $('#dWBmJs').tree('getSelected');
    var pNode = $('#dWBmJs').tree('getParent', node.target); 
    jsbm_ZzjgSelectJs = escape(node.id);
    jsmc_ZzjgSelectJs = escape(node.text);
    bmbm_ZzjgSelectJs = escape(pNode.id);
    bmmc_ZzjgSelectJs = escape(pNode.text);

    $.ajax({
        async: false,
        url: "/Handler/ZZJG/ZZJGHandler.ashx",
        data: "action=GetJsxh&bmbm=" + escape(bmbm_ZzjgSelectJs) + "&jsbm=" + escape(jsbm_ZzjgSelectJs) + "&dwbm=" + G_ZzjgDwbm,
        dataType: "json",
        success: function (data) {
            if (data) {
                jsxh_ZzjgSelectJs = data;
            }
        }
    });
    
    /*
    * 添加角色功能
    */
    $('#winAddJsGn_ZzjgSelectJs').window({
        width: 990,
        height: 530,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '添加角色功能信息'
    });

    $('#winUpdateJsGn_ZzjgSelectJs').window({
        width: 400,
        height: 190,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '修改角色功能信息'
    });


    /*
    * 初始化选择的角色信息
    */

    $('#sJsmc_ZzjgSelectJs').val(jsmc_ZzjgSelectJs);
    $('#sJsxh_ZzjgSelectJs').val(jsxh_ZzjgSelectJs);
    $('#sSsbm_ZzjgSelectJs').val(bmmc_ZzjgSelectJs);

    /*
    * js动态渲染easyui控件
    */
    $('#sJsmc_ZzjgSelectJs').textbox({
        width: 200,
        height: 22,
        disabled: "disabled"
    });

    $('#sJsxh_ZzjgSelectJs').textbox({
        width: 200,
        height: 22,
        disabled: "disabled"
    });

    $('#sSsbm_ZzjgSelectJs').textbox({
        width: 200,
        height: 22,
        disabled: "disabled"
    });
}

/*
* 注册事件
*/
function registEvent_ZzjgSelectJs() {
    /*
    * 编辑角色信息
    */
    $('#btnEdit_ZzjgSelectJs').linkbutton({
        onClick: function () {
            var stat = $('#btnEdit_ZzjgSelectJs').linkbutton('options').text;
            if (stat == "编辑") {
                //将编辑框变为使能状态
                $('#sJsmc_ZzjgSelectJs').textbox({
                    disabled: false
                });
                $('#sJsxh_ZzjgSelectJs').textbox({
                    disabled: false
                });

                $('#btnEdit_ZzjgSelectJs').linkbutton({
                    text: '保存',
                    iconCls: 'icon-save'
                });
            }
            else if (stat == "保存") {
                var jsmc = $('#sJsmc_ZzjgSelectJs').textbox('getValue');
                var jsxh = $('#sJsxh_ZzjgSelectJs').textbox('getValue');
                jsmc = trim(jsmc);
                jsxh = trim(jsxh);
                if (isNull(jsmc) || isNull(jsxh)) {
                    Alert("角色名称或者角色序号不能为空");
                    return;
                }
                if (jsmc.indexOf('\\') != -1)
                {
                    Alert("角色名称不能包含特殊符号“\\”");
                    return;
                }
                var jsbm = jsbm_ZzjgSelectJs;
                $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddJsInfo", { dwbm: G_ZzjgDwbm, jsmc: jsmc, jsxh: jsxh, jsbm: jsbm, bmbm: bmbm_ZzjgSelectJs },
                    function (result) {
                        Alert(result);
                        refreshTree();
                        //选中刚才修改的角色
                        sExpandNode = jsmc;
                    });
            }
        }
    });

    /*
    * 添加角色功能按钮
    */
    $('#btnAddGn_ZzjgSelectJs').linkbutton({
        onClick: function () {
            /*
            * 显示角色功能信息的grid
            */
            $('#dgGngl_ZzjgSelectJs').datagrid({
                fitColumns: true,
                columns: [[
                        { field: 'Gnmc', title: '功能名称', width: 150 },
                        { field: 'Gnsm', title: '功能说明', width: 150 },
                        { field: 'Gnct', title: '功能窗体', width: 200 },
                        { field: 'Gnxh', title: '功能序号', width: 100 },
                        { field: 'Gncs', title: '功能参数', width: 100 },
                        { field: 'Gnxsmc', title: '功能显示名称', width: 150 },
                        { field: 'Gnbm', title: '功能编码', width: 100 },
                        { field: 'Gnfl', title: '功能分类', width: 100 },
                        { field: 'action', title: '操作', width: 100, align: 'center',
                            formatter: function (value, row, index) {
                                var e = '<a href="#" onclick="updateRow_ZzjgSelectJs(' + index + ')">修改</a> ';
                                var d = '<a href="#" onclick="deleteRow_ZzjgSelectJs(' + index + ')">删除</a>';
                                return e+d;
                            }
                        }
                        ]],
                onClickRow: function (rowIndex, rowData) {
                    $('#dgGngl_ZzjgSelectJs').datagrid('clearSelections');
                    $('#dgGngl_ZzjgSelectJs').datagrid('highlightRow', rowIndex);
                }
            });

            /*
            * 隐藏功能编码这一项
            */
            $('#dgGngl_ZzjgSelectJs').datagrid('hideColumn', 'Gnbm');
            $('#dgGngl_ZzjgSelectJs').datagrid('hideColumn', 'Gnfl');

            //加载已分配功能
            loadJsGnGrid_ZzjgSelectJs(bmbm_ZzjgSelectJs, jsbm_ZzjgSelectJs);

            //加载未分配功能
            loadWfpgn();

            /*
            * 角色权限信息panel发生改变触发
            */
            $('#sGnInfo_ZzjgSelectJs').panel({
                onResize: function (width, height) {
                    resizeDgJsQxInfoHeight();
                    //resizeDgJsQxInfoWidth();
                }
            });

            $('#winAddJsGn_ZzjgSelectJs').window('open');
        }
    });

    /*
    * 给角色赋权限
    */
    $('#btnGnSubmit_ZzjgSelectJs').textbox({
        onClickButton: function () {
            var nodeGn = $('#tree_ZzjgSelectJs').tree('getSelected');
            if (nodeGn == null) {
                return;
            }
            var gnbm = escape(nodeGn.id);
            var pNodeGn = $('#tree_ZzjgSelectJs').tree('getParent', nodeGn.target);
            if (pNodeGn == null) {
                Alert("不能选择功能类别!");
                return;
            }
            var gnfl = escape(pNodeGn.id);
            var bz = ''; //$('#gnbz_ZzjgSelectJs').val();
            //            bz = trim(bz);
            var bmbm = bmbm_ZzjgSelectJs;
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddJsGnQx", { dwbm: G_ZzjgDwbm, bmbm: bmbm, jsbm: jsbm_ZzjgSelectJs, gnbm: gnbm, bz: bz },
                    function (result) {
                        //Alert("操作结果: " + result);
                        $('#tree_ZzjgSelectJs').tree('reload');
                        $('#dgGngl_ZzjgSelectJs').datagrid('load');
                    });
            //$('#winAddJsGn_ZzjgSelectJs').window("close");
            //刷新数据


        }
    });

    /*
    * 取消给角色赋权限
    */
    $('#btnGnCacel_ZzjgSelectJs').textbox({
        onClickButton: function () {
            $('#winAddJsGn_ZzjgSelectJs').window("close");
        }
    });

    /*
    * 给角色赋权限
    */
    $('#btnUpdateGn_ZzjgSelectJs').textbox({
        onClickButton: function () {
            var gncs = $('#gncs_ZzjgSelectJs').textbox('getValue');
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=UpdateJsGnCs", { dwbm: G_ZzjgDwbm, bmbm: bmbm_ZzjgSelectJs, jsbm: jsbm_ZzjgSelectJs, gnbm: gnbm_ZzjgSelectJs, gncs: gncs },
                    function (result) {
                        //Alert("操作结果: " + result);
                        $('#winUpdateJsGn_ZzjgSelectJs').window("close");
                        $('#dgGngl_ZzjgSelectJs').datagrid('load');
                    });

        }
    });

//    /*
//    * 功能
//    */
//    $('#sGnmc_ZzjgSelectJs').combobox({
//        valueField: 'id',
//        textField: 'text',
//        onLoadSuccess: function () {
//            var data = $('#sGnmc_ZzjgSelectJs').combobox('getData');
//            if (data.length > 0) {
//                $('#sGnmc_ZzjgSelectJs').combobox('select', data[0].id);
//            }
//        }
//    });

//    /*
//    * 添加角色功能,功能分类
//    */
//    $('#sGnfl_ZzjgSelectJs').combobox({
//        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnflByDwbm',
//        valueField: 'id',
//        textField: 'text',
//        onLoadSuccess: function () {
//            var data = $('#sGnfl_ZzjgSelectJs').combobox('getData');
//            if (data.length > 0) {
//                $('#sGnfl_ZzjgSelectJs').combobox('select', data[0].id);
//            }
//        },
//        onSelect: function () {
//            /*
//            * 通过功能分类显示功能名称
//            */
//            var gnfl = $('#sGnfl_ZzjgSelectJs').combobox('getValue');
//            $('#sGnmc_ZzjgSelectJs').combobox('clear');
//            $('#sGnmc_ZzjgSelectJs').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnmcByflb&gnfl=' + gnfl);
//        }
//    });   
}



/*
* 删除角色处理
*/
function deleteJs_ZzjgSelectJs() {
    $.messager.confirm('确认', '您确认要删除该角色吗？', function (p) {
        if (p) {
            //删除操作
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=DeleteJsInfo", { dwbm: G_ZzjgDwbm, bmbm: bmbm_ZzjgSelectJs, jsbm: jsbm_ZzjgSelectJs
            },
                    function (result) {
                        Alert(result);
                        refreshTree();
                    });
        }
    });
}

/*
*   载入角色功能grid数据
*/
function loadJsGnGrid_ZzjgSelectJs(bmbm, jsbm) {
    $('#dgGngl_ZzjgSelectJs').datagrid({
        width: 'auto',
        striped: true,
        singleSelect: false,
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetJsQxInfo&dwbm=' + G_ZzjgDwbm + '&bmbm=' + bmbm + '&jsbm=' + jsbm,
        loadMsg: '数据加载中，请稍后...',
        pagination: true,
        rownumbers: true
    });

    $('#dgGngl_ZzjgSelectJs').datagrid('getPager').pagination({
        pageSize: 10,
        pageList: [10, 20, 30, 50, 100],
        beforePageText: '第',
        afterPageText: '页   共{pages}页',
        displayMsg: '当前显示【{from} ~ {to}】条记录   共【{total}】条记录'
    });
}

//修改功能
function updateRow_ZzjgSelectJs(index) {
    var rowDatas = $('#dgGngl_ZzjgSelectJs').datagrid('getRows');
    gnbm_ZzjgSelectJs = rowDatas[index].Gnbm; 
    var gncs = rowDatas[index].Gncs;
    $('#gncs_ZzjgSelectJs').textbox('setValue', gncs);
    $('#winUpdateJsGn_ZzjgSelectJs').window("open");
}

//删除功能
function deleteRow_ZzjgSelectJs(index) {
    /*
    * 清空所有选中的行
    * 让该行高亮
    */
    $('#dgGngl_ZzjgSelectJs').datagrid('clearSelections');
    $('#dgGngl_ZzjgSelectJs').datagrid('highlightRow', index);

    var rowDatas = $('#dgGngl_ZzjgSelectJs').datagrid('getRows');
    var gnbm = '1' + rowDatas[index].Gnbm;
    var jsbm = jsbm_ZzjgSelectJs;
    $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=DeleteJsGnQx", { dwbm: G_ZzjgDwbm, gnbm: gnbm, jsbm: jsbm },
        function (result) {
            //刷新数据
            $('#dgGngl_ZzjgSelectJs').datagrid('load');
            $('#tree_ZzjgSelectJs').tree('reload');
        });    
}



/*
* 重置角色权限信息的宽度
*/
function resizeDgJsQxInfoWidth() {
    var panelJsQxInfoWidth = $('#sGnInfo_ZzjgSelectJs').width()-6;
    $('#dgGngl_ZzjgSelectJs').datagrid('options').width = panelJsQxInfoWidth;
    $('#dgGngl_ZzjgSelectJs').datagrid('resize');
}

/*
* 重置角色权限信息的高度
*/
function resizeDgJsQxInfoHeight() {
//    var panelJsQxInfoHeight = $('#winAddJsGn_ZzjgSelectJs').height() - $('#gnTable_ZzjgSelectJs').height()
//                        - $('#gnButtons_ZzjgSelectJs').height()-50;
    $('#dgGngl_ZzjgSelectJs').datagrid('options').height = 400;
//    $('#dgGngl_ZzjgSelectJs').datagrid('options').height = panelJsQxInfoHeight;
    $('#dgGngl_ZzjgSelectJs').datagrid('resize');
}

//加载未分配功能
function loadWfpgn() {
    $('#tree_ZzjgSelectJs').tree({
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetWfpgnTreeData&bmbm=' + bmbm_ZzjgSelectJs + '&jsbm=' + jsbm_ZzjgSelectJs + "&dwbm=" + G_ZzjgDwbm,
        method: 'post',
        lines: true,
        title: '未分配功能',
        onLoadError: function (arguments) {
            Alert(arguments.responseText);
        },
        onLoadSuccess: function (node, data) {
            if (data && data.length > 0) {
                //展开根节点到指定节点
                $('#tree_ZzjgSelectJs').tree('expandAll');
            }
        },
        onSelect: function (node) {
            //考评指标数据初始化
            var pNodeGn = $('#tree_ZzjgSelectJs').tree('getParent', node.target);
            if (pNodeGn == null) {
                $('#btnGnSubmit_ZzjgSelectJs').textbox({ disabled: true });
            }
            else {
                $('#btnGnSubmit_ZzjgSelectJs').textbox({ disabled: false });
            }
        }
    });
}