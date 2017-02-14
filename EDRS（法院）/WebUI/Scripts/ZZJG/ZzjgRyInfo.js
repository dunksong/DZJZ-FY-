
$(document).ready(function () {

    loadRyInfoGrid();

    initWindow_ry();    

    //查询人员
    $('#btnSearch_ry').textbox({
        onClickButton:loadRyInfoGrid
    });

    //弹出添加人员窗口
    $('#btnAdd_ry').textbox({
        onClickButton:openRyWindow
    });

     //弹出添加人员窗口
    $('#btnUpdate_ry').textbox({
        onClickButton:UpdateRyInfo
    });

    //弹出添加人员窗口
    $('#btnDelete_ry').textbox({
        onClickButton:DeleteRyInfo
    });
    
    //添加人员提交按钮事件
    $('#btnSubmit_ry').textbox({
        onClickButton: SaveRyInfo
    });
    
    // 取消添加人员按钮事件
    $('#btnCancel_ry').textbox({
        onClickButton: function () {
            $('#win_ry').window('close');
        }
    });

    // 重置密码按钮事件
    $('#btnResetPwd_ry').textbox({
        onClickButton: ResetPwd_ry
    });

    // 查看详细按钮事件
    $('#btnDetail_ry').textbox({
        onClickButton: Detail_ry
    });

    // 查看角色按钮事件
    $('#btnRole_ry').textbox({
        onClickButton: Role_ry
    });

    // 关闭按钮事件
    $('#btnClose_ry').textbox({
        onClickButton: function () {
            $('#winDetail_ry').window('close');
        }
    });

//    $('#inDw_ry').combobox({    
//        url:'/Handler/ZZJG/ZZJGHandler.ashx?action=GetGxdwList' 
//    }); 
});

function loadRyInfoGrid() {
    var queryData = { action: 'GetRyList', xm: $('#xm_ry').val(), gh: $('#gh_ry').val(), dateStart: getDate($('#dateStart_cm').val()), dateEnd: getDate($('#dateEnd_cm').val()) };
    var time = new Date();
    $('#dg_ry').datagrid({
        width: 'auto',
        //height: gridHeigth,
        title: '人员信息',
        striped: true,
        singleSelect: true,
        queryParams: queryData,
        pagination: true,
        rownumbers: true,
        pageSize: 20,
        url: '/Handler/ZZJG/ZZJGHandler.ashx?t=' + time.getMilliseconds(),
        loadMsg: '数据加载中，请稍后...',
        //onDblClickRow: onDblClickRow
    });

    $('#dg_ry').datagrid('getPager').pagination({
        pageList: [10, 20, 30, 50, 100],
        beforePageText: '第',
        afterPageText: '页   共{pages}页',
        displayMsg: '当前显示【{from} ~ {to}】条记录   共【{total}】条记录'
    });
}

function openRyWindow () {
    $('#win_ry').window({
        title: '人员信息'
    });
    $('#win_ry').window('open');
    clearAddRyInfo();
    $('#inDw_ry').combobox('setValue', parent.userInfo.DWBM);
    //设置提交按钮文本
    $('#btnSubmit_ry').textbox({
        buttonText: '添加'
    });
}

/*
* 清空添加人员信息窗口的控件值
*/
function clearAddRyInfo() {
    $('#inMc_ry').textbox('setValue', '');
    $('#inDlbm_ry').textbox('setValue', '');
    $('#inGzzh_ry').textbox('setValue', '');
    $('#inYdDhhm_ry').textbox('setValue', '');
    $('#inDzyx_ry').textbox('setValue', '');
    $('#inCAID_ry').textbox('setValue', '');
}

function SaveRyInfo() {
//    获取按钮上的文本
    var obj = $('#btnSubmit_ry').textbox('options');
    var btnText = obj.buttonText;
    var mc = trim($('#inMc_ry').textbox("getValue"));
    if (!isNull(mc)) {
        if (getLength(mc) > 60) {
            Alert('名称不能超过60个字符,汉字则不能超过30个字符');
            return;
        }
    }
    var dlbm = trim($('#inDlbm_ry').textbox("getValue"));
    if (!isNull(dlbm)) {
        if (getLength(dlbm) > 60) {
            Alert('登录别名不能超过60个字符,汉字则不能超过30个字符');
            return;
        }
    }
    var gzzh = trim($('#inGzzh_ry').textbox('getValue'));
    if (!isNull(gzzh)) {
        if (getLength(gzzh) > 20) {
            Alert("工作证号不能超过20位!");
            return;
        }
    }
    var xb = $("input[name='xb_ry']:checked").val();
    var lsry = $("input[name='isLsry_ry']:checked").val();
    var yddhhm = trim($('#inYdDhhm_ry').textbox('getValue'));
    if (!isNull(yddhhm)) {
        if (!HRFilePhonevalidate(yddhhm)) {
            Alert("请输入正确的移动电话号码!");
            return;
        }
    }
    var dzyx = trim($('#inDzyx_ry').textbox('getValue'));
    if (!isNull(dzyx)) {
        if (!(mainIsValidate(dzyx))) {
            Alert("请输入正确的邮箱");
            return;
        }
    }
    var CAIDH = trim($('#inCAID_ry').textbox('getValue'));
    if (!isNull(CAIDH)) {
        if (getLength(CAIDH) > 100) {
            Alert('CAID号不能超过100个字符,汉字则不能超过50个字符');
            return;
        }
    }  
    var action = '';
    var gh = '';  
    if (btnText == '添加') {
        action = 'AddRyInfo';
    }   
    else {
        action = 'UpdateRyInfo';
        gh = $('#inGh_ry').textbox('getValue');
    }  
    var dwbm = "";// $('#inDw_ry').combobox('getValue');    
    $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action: action, dwbm: dwbm, mc: mc, dlbm: dlbm, gzzh: gzzh,
        xb: xb, lsry: lsry, yddhhm: yddhhm, dzyx: dzyx, CAIDH: CAIDH, gh: gh
    },
            function (result) {
                Alert(result);
            });
    $('#win_ry').window('close');
    //刷新数据
    $('#dg_ry').datagrid('load');
}

/*
* 修改人员操作
*/
function UpdateRyInfo() {

    var gh = '';
    var data = $('#dg_ry').datagrid('getChecked');
    var i = data.length;
    if (i == 0) {
        return;
    }
//    if (i > 1) {
//        Alert('一次只能修改一个人员！');
//        return;
//    }
    gh =  data[0].gh;
    //查找人员信息
    $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action:'GetRyinfoByGh', dwbm: data[0].dwbm, gh: gh },
         function (data) {
             if (!isNull(data)) {
                 var arrRyinfo = new Array();
                 arrRyinfo = data.split(',', 9);
                 for (var i = 0; i < arrRyinfo.length; i++) {
                     var tmp = arrRyinfo[i].split(':', 2);
                     matchContrl(tmp);
                 }
             }
         });
    $('#win_ry').window({
        title: '新增人员信息'
    });
    $('#inDw_ry').combobox('setValue', data[0].dwbm);
    $('#win_ry').window('open');
    //设置提交按钮文本
    $('#btnSubmit_ry').textbox({
        buttonText: '修改'
    });
}
/*s
* 匹配控件进行赋值
*/
function matchContrl(data) {
    switch (data[0]) {
        case "gh": $('#inGh_ry').textbox('setValue', data[1]);
            break;
        case "mc": $('#inMc_ry').textbox('setValue', data[1]);
                   $('#mc_ry').textbox('setValue', data[1]);
            break;
        case "dlbm": $('#inDlbm_ry').textbox('setValue', data[1]);
                     $('#dlbm_ry').textbox('setValue', data[1]);
            break;        
        case "gzzh": $('#inGzzh_ry').textbox('setValue', data[1]);
                     $('#gzzh_ry').textbox('setValue', data[1]);
            break;
        case "xb":
            {
                var xb = data[1];
                if (xb == "男") {
                    $("input[name='xb_ry'][value=1]").attr("checked", true);
                    $("input[name='sex_ry'][value=1]").attr("checked", true);
                }
                else {
                    $("input[name='xb_ry'][value=0]").attr("checked", true);
                    $("input[name='sex_ry'][value=1]").attr("checked", true);
                }
            }
            break;
        case "sflsry":
            {
                var lsry = data[1]
                if (lsry == "Y") {
                    $("input[name='isLsry_ry'][value=Y]").attr("checked", true);
                    $("input[name='lsry_ry'][value=Y]").attr("checked", true);
                }
                else {
                    $("input[name='isLsry_ry'][value=N]").attr("checked", true);
                    $("input[name='lsry_ry'][value=N]").attr("checked", true);
                }
            }
            break;
        case "yddhhm": $('#inYdDhhm_ry').textbox('setValue', data[1]);
                       $('#dhhm_ry').textbox('setValue', data[1]);
            break;
        case "dzyj": $('#inDzyx_ry').textbox('setValue', data[1]);
                     $('#email_ry').textbox('setValue', data[1]);
            break;
        case "caid": $('#inCAID_ry').textbox('setValue', data[1]);
                     $('#caid_ry').textbox('setValue', data[1]);
            break;
    }
}

/*
* 删除人员操作
*/
function DeleteRyInfo() {

    var ghj = '';
    var data = $('#dg_ry').datagrid('getChecked');
    for (var i = 0; i < data.length; i++) {
        ghj = ghj + ',' + data[i].gh;
    }
    if (ghj == '') {
        return;
    };
    $.messager.confirm('确认','您确认想要删除所选人员吗？',function(r){    
        if (r){    
                $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action: "DeleteRyInfo", dwbm: data[0].dwbm, ghj: ghj
                },
                function (result) {
                    Alert(result);
                });
                //刷新数据
                $('#dg_ry').datagrid('load');
         }    
    });      
    
}

function ResetPwd_ry(){
    var ghj = '';
    var data = $('#dg_ry').datagrid('getChecked');
    for (var i = 0; i < data.length; i++) {
        ghj = ghj + ',' + data[i].gh;
    }
    if (ghj == '') {
        return;
    };
    $.messager.confirm('确认','您确认想要将所选人员重置密码吗？',function(r){    
        if (r){    
                $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action: "ResetPwd", dwbm: data[0].dwbm, ghj: ghj
                },
                function (result) {
                    Alert(result);
                });
         }    
    });  
}

function initWindow_ry()
{
    /*
    * 添加角色功能
    */
    $('#win_ry').window({
//        width: 990,
//        height: 530,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '人员信息'
    });

    $('#winDetail_ry').window({
//        width: 990,
//        height: 530,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '人员详细信息'
    });

    $('#winRole_ry').window({
//        width: 990,
//        height: 530,
        modal: true,
        maximizable: false,
        minimizable: false,
        closed: true,
        collapsible: false,
        title: '角色信息'
    });
}

function Detail_ry()
{
    var gh = '';
    var data = $('#dg_ry').datagrid('getChecked');
    var i = data.length;
    if (i == 0) {
        return;
    }
//    if (i > 1) {
//        Alert('一次只能查看一个人员！');
//        return;
//    }
    gh =  data[0].gh;
    //查找人员信息
    $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action:'GetRyinfoByGh', dwbm: data[0].dwbm, gh: gh },
         function (data) {
             if (!isNull(data)) {
                 var arrRyinfo = new Array();
                 arrRyinfo = data.split(',', 9);
                 for (var i = 0; i < arrRyinfo.length; i++) {
                     var tmp = arrRyinfo[i].split(':', 2);
                     matchContrl(tmp);
                 }
             }
         });
    $('#winDetail_ry').window('open');
}

function Role_ry()
{
    var gh = '';
    var data = $('#dg_ry').datagrid('getChecked');
    var i = data.length;
    if (i == 0) {
        return;
    }
//    if (i > 1) {
//        Alert('一次只能查看一个人员！');
//        return;
//    }
    gh =  data[0].gh;
    //查找人员信息
    $('#tree_ry').tree({
        url: '/Handler/ZZJG/ZZJGHandler.ashx?action=GetRyJsData&gh=' + gh + "&dwbm=" + data[0].dwbm,
        method: 'post',
        lines: true,
        title: '已分配角色',
        onLoadError: function (arguments) {
            Alert(arguments.responseText);
        },
        onLoadSuccess: function (node, data) {
            if (data && data.length > 0) {
                //展开根节点到指定节点
                $('#tree_ry').tree('expandAll');
            }
        }        
    });
    $('#winRole_ry').window('open');
}




