/**
 * Created by Administrator on 2016/11/28.
 */
//按钮添加随机颜色
var cc=["rgb(181,55,183)", "rgb(73,165,166)", "rgb(140,158,66)", "rgb(86,27,182)", "rgb(79,125,123)", "rgb(66,121,121)", "rgb(157,37,109)", "rgb(96,174,173)", "rgb(65,124,144)", "rgb(179,62,73)", "rgb(43,159,80)", "rgb(40,108,170)", "rgb(156,144,149)", "rgb(106,155,147)", "rgb(69,121,131)", "rgb(36,160,145)", "rgb(61,153,145)", "rgb(138,138,143)", "rgb(94,160,82)", "rgb(112,148,48)"];
//循环按钮
$("#searchbar  .l-button").each(function(index){
    $(this).addClass("l-button-"+index);
   for(var k=0;k<=index;k++){
        $(".l-button-"+k).css("background",cc[k]);
    }
});