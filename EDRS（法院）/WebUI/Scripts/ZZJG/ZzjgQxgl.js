$(document).ready(function () {

    initControl();

    registControlEvent();
});

/*
 * 初始化控件
 */
function initControl()
{
    $('#inSslb_ZzjgQxgl').combobox({
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnflByDwbm',
        valueField: 'id',
        textField: 'text'
    }); 

    $('#gnfl').combobox({
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnflByDwbm',
        valueField: 'id',
        textField: 'text'
    });



    /*
    * 添加功能权限窗口
    */
    $('#winAddGnQx').window({
        width: 700,
        height: 400,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '添加功能权限'
    });
    /*
    * 输入控件      
    */
    $('#gnmc').textbox({
        width:150,
        heigth:22
    });
    $('#gnct').textbox({
        width:150,
        heigth:22
    });
    $('#gnxsmc').textbox({
        width:150,
        heigth:22
    });
    $('#gnxh').textbox({
        width:150,
        heigth:22
    });
    $('#cscs').textbox({
        width:150,
        heigth:22
    });
    $('#gnsm').textbox({
        multiline:true,
        width:300,
        height:100
    });

    resizeGnInfoHeight();
}

/*
 * 注册控件事件
 */
function registControlEvent()
{
    /*
    * 查询按钮
    */
    $('#btnQuery_ZzjgQxGl').textbox({
        onClickButton: function () {
            var gnmc = $('#inMc').textbox('getValue');
            var sslb = $('#inSslb_ZzjgQxgl').combobox('getValue');
            if (isNull(gnmc)) {
                gnmc = '';
            }
            if (isNull(sslb)) {
                sslb = '';
            }
            loadGnQxGrid(gnmc, sslb);
        }
    });

    /*
    * 打开添加功能权限窗口
    */
    $('#btnAdd_ZzjgQxGl').textbox({
        onClickButton: function () {
            clearQxData();
            /*
            * 设置窗口标题
            */
            $('#winAddGnQx').window('setTitle', '添加功能权限');
            $('#winAddGnQx').window('open');
            $('#btnSave_ZzjgQxGl').textbox({
                buttonText:'保存'
            });
        }
    });

    /*
    * 添加权限功能提交按钮
    */
    $('#btnSave_ZzjgQxGl').textbox({
        onClickButton: function () {
            var obj = $('#btnSave_ZzjgQxGl').textbox('options');
            var btnText = obj.buttonText;

            var isExistedFlb = 1; //是否存在的父类别 1:存在，0:不存在
            var gnfl = $('#gnfl').combobox('getValue');
            if (isNull(gnfl)) {
                gnfl = $('#gnfl').combobox('getText');
                isExistedFlb = 0;
                if (isNull(gnfl)) {
                    Alert('功能分类不能为空');
                    return;
                }
            }
            var gnbm = document.getElementById('gnbm').value;
            /*
            * 如果是保存gnbm为空表示是添加新的功能
            */
            if (btnText == '保存') {
                gnbm = '';
            }
            var gnmc = $('#gnmc').textbox('getValue');
            var gnct = $('#gnct').textbox('getValue');
            var gnxsmc = $('#gnxsmc').textbox('getValue');
            var gnxh = $('#gnxh').textbox('getValue');
            var cscs = $('#cscs').textbox('getValue');
            var gnsm = $('#gnsm').textbox('getValue');

            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddGnQx", { isExistedFlb: isExistedFlb, gnfl: encodeURI(gnfl), gnbm: encodeURI('1' + gnbm), gnmc: encodeURI(gnmc), gnct: encodeURI(gnct),
                gnxsmc: encodeURI(gnxsmc), gnxh: gnxh, cscs: encodeURI(cscs), gnsm: encodeURI(gnsm)
            },
            function (result) {
                Alert("操作结果: " + result);
            });
            $('#winAddGnQx').window('close');


            if (isExistedFlb == 0) {
                /*
                * 添加了新的分类则刷新下拉列表
                */
                $('#gnfl').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnflByDwbm');
                $('#inSslb').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetGnflByDwbm');
            }
        }
    });

   /*
    *取消添加功能权限      
    */
    $('#btnCancel_ZzjgQxGl').textbox({
        onClickButton: function () {
            $('#winAddGnQx').window('close');
        }
    });

}



/*
* 开始编辑或删除grid行
*/
function editRow(index) {
    /*
    * 设置窗口标题
    */
    $('#winAddGnQx').window('setTitle', '编辑功能权限');
    $('#btnSave_ZzjgQxGl').textbox('options').buttonText = '编辑';
    /*
    * 清空所有选中的行
    * 让该行高亮
    */
//    $('#dgGngl').datagrid('clearSelections');
//    $('#dgGngl').datagrid('highlightRow', index);

    var rowDatas = $('#dgGngl').datagrid('getRows');
    /*
    * 将信息显示到编辑界面
    */
    var flbm = rowDatas[index].Gnfl;
    var gnmc = rowDatas[index].Gnmc;
    var gnsm = rowDatas[index].Gnsm;
    var gnct = rowDatas[index].Gnct;
    var gnxh = rowDatas[index].Gnxh;
    var gncs = rowDatas[index].Gncs;
    var gnxsmc = rowDatas[index].Gnxsmc;
    var gnbm = rowDatas[index].Gnbm;

    /*
    * 设置功能分类
    */
    var data = $('#gnfl').combobox('getData');
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            if (flbm == String(data[i].id).substring(1)) {
                $('#gnfl').combobox('select', data[i].id);
                break;
            }
        }
    }

    $('#gnmc').textbox('setValue', gnmc);
    $('#gnct').textbox('setValue', gnct);
    $('#gnxsmc').textbox('setValue', gnxsmc);
    $('#gnxh').textbox('setValue', gnxh);
    $('#cscs').textbox('setValue', gncs);
    $('#gnsm').textbox('setValue', gnsm);
    document.getElementById('gnbm').value = gnbm;

    $('#winAddGnQx').window('open');
    
}
function deleteRow(index) {
    var rowDatas = $('#dgGngl').datagrid('getRows');
    var gnbm = rowDatas[index].Gnbm;
    $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=DeleteGnQx", {gnbm:encodeURI('1' + gnbm)},
                function (result) {
                    Alert("操作结果: " + result);
    });
   /*
   * 重新载入grid数据
   */
   $('#dgGngl').datagrid('load');
}


/*
* 清空添加功能权限的窗体数据
*/
function clearQxData() {
    $('#gnmc').textbox('setValue', '');
    $('#gnct').textbox('setValue', '');
    $('#gnxsmc').textbox('setValue', '');
    $('#gnxh').textbox('setValue', '');
    $('#cscs').textbox('setValue', '');
    $('#gnsm').textbox('setValue', '');
}

function isNull(data) {
    return (data == "" || data == undefined || data == null) ? true : false;
}

/*
* 重置功能权限信息的高度
*/
function resizeGnInfoHeight() {
    var panelGnInfoHeight = $('#sGnInfo').height();

}