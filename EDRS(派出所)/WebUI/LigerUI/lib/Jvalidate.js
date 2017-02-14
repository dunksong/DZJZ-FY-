//提示
function Alert(msg) {
    $.ligerDialog.warn(msg);
}
/*
* 去除首尾空格
*/
function trim(s) {
    return s.replace(/(^\s*) | (\s*$)/g, "");
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
//邮箱验证
function mainIsValidate(textString) {
    var reg = new RegExp(/^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/);
    if (reg.test(textString)) {
        return true;
    } else {
        return false;
    }
}