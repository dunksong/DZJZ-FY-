$(function() {
    //自定义验证
    $.extend($.fn.validatebox.defaults.rules, {
        //最大字符长度
        maxLength: {
            validator: function(value, param) {
                return value.length <= param[0];
            },
            message: '请最多输入 {0} 个字.'
        },
        //电话号码
        phoneRex: {
            validator: function(value) {
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
});