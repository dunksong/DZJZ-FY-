var bmbm = '';
var bmmc = '';
var bmInfo;

$(document).ready(function () {

    var node = $('#dWBmJs').tree('getSelected');
    bmbm = node.id;
    bmmc = node.text;

    initContrl_ZzjgSelectBm();

//    registEvent_ZzjgSelectBm();
});

/*
* 初始化控件
*/
function initContrl_ZzjgSelectBm() {

    $.ajax({
        async: false,
        url: "/Handler/ZZJG/ZZJGHandler.ashx",
        data: "action=GetBmInfo&bmbm=" + escape(bmbm) + "&bmmc=" + escape(bmmc) + "&dwbm=" + G_ZzjgDwbm,
        dataType: "json",
        success: function (data) {
            if (data) {
                bmInfo = data;
            }            
        }
    });

    /*
    * 设置部门信息
    */
    $('#inBmmc_ZzjgSelectBm').val(bmmc);
    $('#inBmjc_ZzjgSelectBm').val(bmInfo.BMJC);
    $('#inBmxh_ZzjgSelectBm').val(bmInfo.BMXH);
    $('#inBz_ZzjgSelectBm').val(bmInfo.BZ);
    
    /*
    * 添加角色时显示所选部门
    */
    $('#ShSsBm_ZzjgSelectBm').val(bmmc);

    /*
    * js动态渲染easyui的界面
    */
    $('#inBmmc_ZzjgSelectBm').textbox({
        width:200,
        height:22,
        disabled:"disabled"
    });

    $('#inBmjc_ZzjgSelectBm').textbox({
        width: 200,
        height: 22,
        disabled: "disabled"
    });

    $('#inBmxh_ZzjgSelectBm').textbox({
        width: 200,
        height: 22,
        disabled: "disabled"
    });

    $('#inBz_ZzjgSelectBm').textbox({
        width: 500,
        height: 80,
        multiline: true
    });

    $('#ShSsBm_ZzjgSelectBm').textbox({
        width:200,
        height:22,
        disabled:"disabled"
    });
}

/*
* 注册控件事件
*/
function registEvent_ZzjgSelectBm() {

}

/*
* 添加部门切换panel
*/
function addBm_ZzjgSelectBm() {
}

/*
* 添加角色切换panel
*/
function addJs_ZzjgSelectBm() {
}

/*
*添加部门
*/
$('#btnEdit_ZzjgSelectBm').linkbutton({
    onClick: function () {
        var bmmc = $('#inBmmc_ZzjgSelectBm').textbox("getValue");
        var bmjc = $('#inBmjc_ZzjgSelectBm').textbox("getValue");
        var bmxh = $('#inBmxh_ZzjgSelectBm').textbox("getValue");
        var bz = $('#inBz_ZzjgSelectBm').val();
        bmmc = trim(bmmc);
        bmjc = trim(bmjc);
        bmxh = trim(bmxh);
        bz = trim(bz);
        var stat = $('#btnEdit_ZzjgSelectBm').linkbutton('options').text;
        if (stat == "编辑") {
            //将编辑框变为使能状态
            $('#inBmmc_ZzjgSelectBm').textbox({
                disabled: false
            });
            $('#inBmjc_ZzjgSelectBm').textbox({
                disabled: false
            });
            $('#inBmxh_ZzjgSelectBm').textbox({
                disabled: false
            });

            $('#btnEdit_ZzjgSelectBm').linkbutton({
                text: '保存',
                iconCls: 'icon-save'
            });
        }
        else if (stat == "保存") { //保存数据(编辑部门)
            if (isNull(bmmc) || isNull(bmjc) || isNull(bmxh)) {
                Alert("部门名称或者部门简称或者部门序号不能为空");
                return;
            }

            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddBmInfo", { dwbm: G_ZzjgDwbm, bmmc: bmmc, bmjc: bmjc, bmxh: bmxh, bz: bz, bmbm: bmbm },
                function (result) {
                    Alert(result);
                    refreshTree();
                    //选中刚才编辑的部门
                    sExpandNode = bmmc;
                    //刷新添加人员窗口的部门combobox                    
                    $('#inCbbm').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetBmInfoByDwbm&dwbm=' + G_ZzjgDwbm);
                });
        }
        else if (stat == "添加") { //添加部门
            if (isNull(bmmc) || isNull(bmjc) || isNull(bmxh)) {
                Alert("部门名称或者部门简称或者部门序号不能为空");
                return;
            }
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddBmInfo", { dwbm: G_ZzjgDwbm, bmmc: bmmc, bmjc: bmjc, bmxh: bmxh, bz: bz, fbm: bmbm },
                    function (result) {
                        Alert(result);
                        refreshTree();
                        //选中刚才新添加的部门
                        sExpandNode = bmmc;
                        //刷新添加人员窗口的部门combobox
                        $('#inCbbm').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetBmInfoByDwbm&dwbm=' + G_ZzjgDwbm);
                    });
        }
    }
});

/*
*添加角色
*/
$('#btnAddJs_ZzjgSelectBm').textbox({
    onClickButton: function () {
        var jsmc = $('#inJsMc_ZzjgSelectBm').textbox('getValue');
        var jsxh = $('#inJsXh_ZzjgSelectBm').textbox('getValue');
        jsmc = trim(jsmc);
        jsxh = trim(jsxh);
        if (isNull(jsmc) || isNull(jsxh)) {
            Alert("角色名称或者角色序号不能为空");
            return;
        }
        $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddJsInfo", { dwbm: G_ZzjgDwbm, bmbm: bmbm, jsmc: jsmc, jsxh: jsxh },
                function (result) {
                    Alert(result);
                    refreshTree();
                    //选中刚才新添加的角色
                    sExpandNode = jsmc;
                    //刷新添加人员窗口的部门combobox
                    $('#inCbbm').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetBmInfoByDwbm&dwbm=' + G_ZzjgDwbm);
                });
    }
});