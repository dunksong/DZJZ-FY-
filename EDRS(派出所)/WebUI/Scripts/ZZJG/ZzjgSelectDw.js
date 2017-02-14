
$(document).ready(function () {    

    initContrl_ZzjgSelectDw();

    registEvent_ZzjgSelectDw();

});

//初始化控件
function initContrl_ZzjgSelectDw() {
    //添加部门按钮
    $('#btnAddBm_ZzjgSelectDw').linkbutton({
        onClick: function () {
            $('#pDwInfo_ZzjgSelectDw').panel('close');
            $('#pBm_ZzjgSelectDw').panel('open');
        }
    });

    $.post("/Handler/ZZJG/ZZJGHandler.ashx", { action: "GetDwInfo", dwbm: G_ZzjgDwbm },
            function (data) {
                if (!isNull(data)) {
                    var arrRyinfo = new Array();                    
                    arrRyinfo = data.split(',', 8);
                    for (var i = 0; i < arrRyinfo.length; i++) {
                        var tmp = arrRyinfo[i].split(':', 2);
                        matchDwContrl(tmp);
                    }
                }
                /*
                * js动态渲染easyui控件
                */
                $('#showDwmc_ZzjgSelectDw').textbox({
                    width: 200,
                    height: 22,
                    disabled: "disabled"
                });

                $('#showDwjc_ZzjgSelectDw').textbox({
                    width: 200,
                    height: 22,
                    disabled: "disabled"
                });

                $('#showDwjb_ZzjgSelectDw').textbox({
                    width: 200,
                    height: 22,
                    disabled: "disabled"
                });
            });    
}

function registEvent_ZzjgSelectDw() {
    /*
    *添加部门
    */
    $('#btnSave_ZzjgSelectDw').textbox({
        onClickButton: function () {
            var bmmc = $('#inBmmc_ZzjgSelectDw').textbox("getValue");
            var bmjc = $('#inBmjc_ZzjgSelectDw').textbox("getValue");
            var bmxh = $('#inBmxh_ZzjgSelectDw').textbox("getValue");
            var bz = $('#inBz_ZzjgSelectDw').val();
            /*
            * 判断输入是否为空值
            */
            bmmc = trim(bmmc);
            bmjc = trim(bmjc);
            bmxh = trim(bmxh);
            if (isNull(bmmc) || isNull(bmjc) || isNull(bmxh)) {
                Alert('部门名称或者部门简称或者部门序号不能为空');
                return;
            }

            if (isNull(G_ZzjgNode)) {
                fbm = '';
            }
            else {
                var fbm = G_ZzjgNode.id;
                //如果选择的是单位编码则去掉单位编码
                if (fbm == "1" + G_ZzjgDwbm) {
                    fbm = "";
                }
            }
            $.post("/Handler/ZZJG/ZZJGHandler.ashx?action=AddBmInfo", { dwbm: G_ZzjgDwbm, bmmc: bmmc, bmjc: bmjc, bmxh: bmxh, bz: bz, fbm: fbm },
                    function (result) {
                        /*
                        * 重新加载树形控件数据
                        */
                        Alert(result);
                        refreshTree();
                        //选中刚才新添加的部门
                        sExpandNode = bmmc;

                        //刷新添加人员窗口的部门combobox
                        $('#inCbbm').combobox('reload', '/Handler/ZZJG/ZZJGHandler.ashx?action=GetBmInfoByDwbm&dwbm=' + G_ZzjgDwbm);
                    });
        }
    });
}
/*s
* 匹配控件进行赋值
*/
function matchDwContrl(data) {
    switch (data[0]) {
        case "dwmc": $("#showDwmc_ZzjgSelectDw").val(data[1]);
            break;
        case "dwjc": $('#showDwjc_ZzjgSelectDw').val(data[1]);
            break;
        case "dwjb": $('#showDwjb_ZzjgSelectDw').val(data[1]);
            break;        
    }
}