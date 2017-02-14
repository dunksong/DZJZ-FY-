$.messager.defaults.ok = "确定";
$.messager.defaults.cancel = "取消";
$.fn.datebox.defaults.closeText = "关闭";
$.fn.datebox.defaults.okText = "确定";
$.fn.datebox.defaults.currentText = "今天";

function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}
function myparser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}

function getStartDate() {
    var date = new Date();
    var strMonth = date.getMonth();
    strMonth = strMonth == 0 ? 12 : strMonth;
    if (strMonth < 10) {
        strMonth = '0' + strMonth;
    }
    var strDay = date.getDate();
    if (strDay < 10) {
        strDay = '0' + strDay;
    }
    var startDate = date.getFullYear() + '年' + strMonth + '月' + strDay + '日';
    return startDate;
}

function getEndDate() {
    var date = new Date();
    var strMonth = date.getMonth();
    strMonth = strMonth + 1;
    strMonth = strMonth == 13 ? 0 : strMonth;
    if (strMonth < 10) {
        strMonth = '0' + strMonth;
    }
    var strDay = date.getDate();
    if (strDay < 10) {
        strDay = '0' + strDay;
    }
    var startDate = date.getFullYear() + '年' + strMonth + '月' + strDay + '日';
    return startDate;
}

function getDate(date) {
    if (undefined == date) {
        return null;
    }
    var result = '';
    if (date.length == 11) {
        result = date.substr(0, 4) + '-' + date.substr(5, 2) + '-' + date.substr(8, 2);
    }
    return result;
}

/***************消息提示**********************/
//提示
function Alert(msg) {
    $.messager.alert('提示', msg, 'info');
}

//提示+处理
function AlertAndDo(msg, fn) {
    $.messager.alert('提示', msg, 'info', fn);
}

//错误提示
function ShowError(msg) {
    $.messager.alert('错误', msg, 'error');
}

//错误提示+处理
function ShowErrorAndDo(msg, fn) {
    $.messager.alert('错误', msg, 'error', fn);
}

//问题
function ShowQuestion(msg) {
    $.messager.alert('问题', msg, 'question');
}

//问题+处理
function ShowQuestionAndDo(msg, fn) {
    $.messager.alert('问题', msg, 'question', fn);
}

//警告
function ShowWarning(msg) {
    $.messager.alert('警告', msg, 'warning');
}

//警告+处理
function ShowWarningAndDo(msg, fn) {
    $.messager.alert('警告', msg, 'warning', fn);
}

/****************确认对话框********************/
//确认对话框
function Confirm(title, msg, fn) {
    $.messager.confirm(title, msg, fn);
}

/****************弹出输入框********************/
//弹出输入框
function Prompt(title, msg, fn) {
    $.messager.prompt(title, msg, fn);
}

/****************进度条**********************/
//显示进度条
function ShowProgress() {
    $.messager.progress();
}

//关闭进度条
function CloseProgress() {
    $.messager.progress('close');
}

/********************动态添加元素引用*********************/
//添加Script元素
function AddScript(type, src) {
    var add = true;
    var oHead = document.getElementsByTagName('head').item(0);
    for (var i = 0; i < oHead.childNodes.length; i++) {
        if (oHead.childNodes[i].src && oHead.childNodes[i].src.indexOf(src.replace(/\.\.\//g, '')) != -1) {
            var oScirpt = document.createElement("script");
            oScirpt.type = type;
            oScirpt.src = src;

            oHead.replaceChild(oScirpt, oHead.childNodes[i]);
            add = false;
            break;
        }
    }
    if (add) {
        var scirpt = document.createElement("script");
        scirpt.type = type;
        scirpt.src = src;
        oHead.appendChild(scirpt);
    }
}

//添加Link元素
function AddLink(type, href, rel) {
    var add = true;
    var es = document.getElementsByTagName('link');
    for (var i = 0; i < es.length; i++) {
        if (es[i]['href'].indexOf(href.replace(/\.\.\//g, '')) != -1) {
            add = false;
            break;
        }
    }
    if (add) {
        var oHead = document.getElementsByTagName('head').item(0);
        var oLink = document.createElement("link");
        oLink.type = type;
        oLink.href = href;
        oLink.rel = rel;
        oHead.appendChild(oLink);
    }
}

/******************************Tab标签打开********************************/
function OpenTab(href, title, params) {
    var time = new Date();
    if (href.indexOf('?') > 0) {

        href = href + "&t=" + time.getMilliseconds();
    }
    else {
        href = href + "?t=" + time.getMilliseconds();
    }
    if (params && params.length > 0) {
        href = href + '&' + params;
    }
    var tab = $('#tabMain').tabs('getTab', title);
    if (tab) {
        $('#tabMain').tabs('select', title);
    }
    else {
        $('#tabMain').tabs('add', {
            title: title,
            href: href,
            closable: true,
            tools: [{
                iconCls: 'icon-mini-refresh',
                handler: function () {
                    var tab = $('#tabMain').tabs('getSelected');  // 获取选择的面板
                    $('#tabMain').tabs('update', {
                        tab: tab,
                        options: {
                            title: tab.title,
                            href: tab.href  // 新内容的URL
                        }
                    });

                }
            }]
        });
    };
}




//获取combotree中选择的值
function getdwbmj(cbo) {
    var i = 0;
    //    var strdwbmj = "370000";
    var strdwbmj = "";
    var dwbmj = $(cbo).combo('getValues');
    for (var i = 0; i < dwbmj.length; i++) {
        var dwbm = dwbmj[i].substring(1);
        if (strdwbmj == "") {
            strdwbmj = dwbm;
        }
        else {
            strdwbmj = strdwbmj + "," + dwbm;
        }
    }
    return strdwbmj
}

/*
* 获取字符串的真实长度
*/
function getLength(str) {
    var realLength = 0, len = str.length, charCode = -1;
    for (var i = 0; i < len; i++) {
        charCode = str.charCodeAt(i);
        if (charCode >= 0 && charCode <= 128) {
            realLength += 1;
        }
        else {
            realLength += 2;
        }
    }
    return realLength;
}

/*
* 除去字符串的左空格
*/
function ltrim(s) {
    return s.replace(/(^\s*)/g, "");
}

/*
* 除去字符串的右空格
*/
function rtrim(s) {
    return s.replace(/(\s*$)/g, "");
}

/*
* 去除首尾空格
*/
function trim(s) {
    return s.replace(/(^\s*) | (\s*$)/g, "");
}

/*
* 判断字符串是否为空
*/
function isNull(data) {
    return (data == "" || data == undefined || data == null) ? true : false;
}

//电话验证 11位数字以1开头
function HRFilePhonevalidate(textString) {
    var reg = new RegExp(/^1\d{10}$/);
    if (reg.test(textString)) {
        return true;
    } else {
        return false;
    }
}

//邮政编码验证
function ValidateYZHM(textString) {
    var reg = new RegExp("[1-9]\d{5}(?!\d)");
    if (reg.test(textString)) {
        return true;
    } else {
        return false;
    }
}

//国内固定电话验证
function ValidateTel(textString) {
}

//邮箱验证
function mainIsValidate(textString) {
    var reg = new RegExp(/^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/);
    if (reg.test(textString)) {
        return true;
    } else {
        return false;
    }
}

//加码
function escape(val) {
    return val.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
}
//解码
function unescape(val) {
    return val.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&amp;/g, '&');
}

//获取界面传递的参数
function getQueryString(url, name) {
    var paramStr = "";
    if (url == "" || !url || url.indexOf('?') <= 0) {
        return "";
    }
    paramStr = url.split('?')[1];
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = paramStr.match(reg);
    if (r != null && r)
        return unescape(r[2]);
    return "";
}