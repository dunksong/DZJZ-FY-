//按钮添加颜色
$(function () {
    var cc = ["#B537B7", "#49A5A6", "#8C9E42", "#561BB6", "#4F7D7B", "#427979", "#9D256D", "#60AEAD", "#417C90", "#B33E49", "#2B9F50", "#286CAA", "#9C9095", "#6A9B93", "#457983", "#24A091", "#3D9991", "#8A8A8F", "#5EA052", "#709430"];

    var dd = ["rgb(127,170,141)", "rgb(116,172,148)", "rgb(179,160,87)", "rgb(126,101,105)", "rgb(155,143,122)", "rgb(91,101,182)", "rgb(163,152,154)", "rgb(174,141,69)", "rgb(71,175,41)", "rgb(121,83,115)", "rgb(150,161,70)", "rgb(74,131,91)", "rgb(59,126,136)", "rgb(177,62,150)", "rgb(21,178,96)", "rgb(162,29,179)", "rgb(89,184,179)", "rgb(159,173,44)", "rgb(127,101,120)", "rgb(130,123,29)"];


    $("#searchbar .l-button,#add_form .l-button,.searchbartab .l-button,#tb .l-button,#top_div .l-button,#top_div_to .l-button,#tb_search .l-button").each(function (index) {
        $(this).addClass("l-button-" + index);
        for (var k = 0; k <= index; k++) {
            $(".l-button-" + k).css({ background: cc[k], color: "white", borderRadius: "5px", height: "25px", lineHeight: "25px", border: "none" });
        }
    });
    //禁止按钮
    $("#searchbar .l-button-disabled").css({ background: "#ccc", color: "#9D9D9E" });
    //小图标按钮
    $(".l-toolbar-item").each(function (index) {
        //为表格每个按钮添加css属性
        $(this).addClass("l-toolbar-item-" + index);
        for (var k = 0; k <= index; k++) {
            $(".l-toolbar-item-" + k).css({ background: dd[k], color: "white", borderRadius: "5px" });
        }
    })

    $(".l-panel-topbar").css({
        background: "white",
        borderRadius: "10px 10px 0 0",
        borderBottom: "1px solid #959595"
    });

})
