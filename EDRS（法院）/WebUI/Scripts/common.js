


/* 常用方法 */

//去掉两边空格
String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//去掉左边空格
String.prototype.LTrim = function () {
    return this.replace(/(^\s*)/g, "");
}

//去掉右边空格
String.prototype.RTrim = function () {
    return this.replace(/(\s*$)/g, "");
}

// 返回字符的长度，一个中文算2个
String.prototype.ChineseLength = function () {
    return this.replace(/[^\x00-\xff]/g, "**").length;
}

// 判断字符串是否以指定的字符串开始
String.prototype.StartsWith = function (str) {
    return this.substr(0, str.length) == str;
}

// 判断字符串是否以指定的字符串结束
String.prototype.EndsWith = function (str) {
    return this.substr(this.length - str.length) == str;
}

// 替换字符 类似于replaceAll
String.prototype.ReplAll = function () {
    var reg = new RegExp("(" + arguments[0] + ")", "g");
    //return this.replace(reg, "<font color=red>$1</font>");
    return this.replace(reg, arguments[1]);
}

//导航
function lochref(url) {
    window.location.href = url;
}
//自定义验证
/*
$.extend($.fn.validatebox.defaults.rules, {
    //最大字符长度
    maxLength: {
        validator: function (value, param) {
            return value.length <= param[0];
        },
        message: '请最多输入 {0} 个字.'
    },
    //电话号码
    phoneRex: {
        validator: function (value) {
            varrex = /^1[3-8]+\d{9}$/;
            varrex2 = /^((0\d{2,3})-)(\d{7,8})(-(\d{3}))?$/;
            if (rex.test(value) || rex2.test(value)) {
                return true;
            } else {
                return false;
            }
        },
        message: '请输入正确电话或手机格式'
    }
});

*/