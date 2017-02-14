<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingApply.aspx.cs" Inherits="WebUI.Pages.LSYJ.ReadingApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打印管理</title>
   <link rel="stylesheet" href="/LigerUI/jquery.mobile-1.4.5.css"/>
     <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="../../Scripts/pdfobject.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/jquery.mobile-1.4.5.min.js"></script>

    
    <style type="text/css">
        .l-panel-topbar
        {
            padding: 5px 0; 
            margin-bottom: -1px;
            border-bottom: 1px solid #959595;
            display: inline-block;
            width: 100%;
        }
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
            margin: 0;
        }
        #tree_left
        {
            width: auto !important;
        }
        #leftFrm
        {
            overflow: auto !important;
            height: 100%;
        }
        .l-tree-icon-folder1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-folder1-open
        {
            background: url("/images/icons/usergroup-open.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-tree-icon-leaf1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size: 14px 14px;
        }
        .l-table
        {
            width: 100%;
        }
        .l-table tr td
        {
            padding: 5px 2px;
        }
        .l-text
        {
            width: 150px;
        }
        div#tb_search {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
           /* font-family: "微软雅黑", "宋体", Arial, sans-serif; font-size: 12px; */
        }
        .l-layout-left,.l-layout-center {
            border: 1px solid rgb(204, 204, 204);
            background: white;
            border-radius: 10px;
        }
        
        #banner{position:relative;width: 1065px;height: auto;overflow: hidden;}
        div#pdfShow
        {
            position:relative;z-index: 10;
            width:100000px;
            overflow: hidden;
            }
       div#pdfShow div {
            float: left;
        }
        a#left,a#right 
        {
            color: white;
            text-align: center;
            background: #3d6180;
            cursor:pointer;
       }  
   



        body {
            -webkit-perspective: 1000px;
            -moz-perspective: 1000px;
            -ms-perspective: 1000px;
            perspective: 1000px;
        }

        .preserve-3d {
            -webkit-transform-style: preserve-3d;
            -moz-transform-style: preserve-3d;
            -ms-transform-style: preserve-3d;
            transform-style: preserve-3d;
        }


        .flip-animation-2{
            animation: flipBook2 1.5s;
            -webkit-transform-origin: 0 100%;
            -moz-transform-origin: 0 100%;
            -ms-transform-origin: 0 100%;
            -o-transform-origin: 0 100%;
            transform-origin: 0 100%;
            transform: rotateY(0deg);
        }
        .flip-animation-1{
            animation: flipBook1 1.2s;
            -webkit-transform-origin: 0 100%;
            -moz-transform-origin: 0 100%;
            -ms-transform-origin: 0 100%;
            -o-transform-origin: 0 100%;
            transform-origin: 0 100%;
            transform: rotateY(0deg);
        }
        .page-front {
            width: 100%;
            height: 300px;
            border: 1px solid #1976D2;
            text-align: center;
            background-color: #1976D2;
            color: #fff;

        }

        @keyframes flipBook2
        {
          /*  0%   {-webkit-transform: rotateY(0deg);
                -ms-transform: rotateY(0deg);
                -o-transform: rotateY(0deg);
                transform: rotateY(0deg);}
            10%   {-webkit-transform: rotateY(-10deg);
                -ms-transform: rotateY(-10deg);
                -o-transform: rotateY(-10deg);
                transform: rotateY(-10deg);}
            20%   {-webkit-transform: rotateY(-20deg);
                -ms-transform: rotateY(-20deg);
                -o-transform: rotateY(-20deg);
                transform: rotateY(-20deg);}
            30%   {-webkit-transform: rotateY(-30deg);
                -ms-transform: rotateY(-30deg);
                -o-transform: rotateY(-30deg);
                transform: rotateY(-30deg);}
            40%   {-webkit-transform: rotateY(-40deg);
                -ms-transform: rotateY(-40deg);
                -o-transform: rotateY(-40deg);
                transform: rotateY(-40deg);}
            50% {-webkit-transform: rotateY(-50deg);
                -ms-transform: rotateY(-50deg);
                -o-transform: rotateY(-50deg);
                transform: rotateY(-50deg);}
            100% {-webkit-transform: rotateY(-100deg);
                -ms-transform: rotateY(-100deg);
                -o-transform: rotateY(-100deg);
                transform: rotateY(-100deg);}  */
                0%   {-webkit-transform: rotateY(0deg);
                -ms-transform: rotateY(0deg);
                -o-transform: rotateY(0deg);
                transform: rotateY(0deg);}
            30% {-webkit-transform: rotateY(-60deg);
                -ms-transform: rotateY(-60deg);
                -o-transform: rotateY(-60deg);
                transform: rotateY(-60deg);}
                50% {-webkit-transform: rotateY(-70deg);
                -ms-transform: rotateY(-70deg);
                -o-transform: rotateY(-70deg);
                transform: rotateY(-70deg);}
                70% {-webkit-transform: rotateY(-160deg);
                -ms-transform: rotateY(-160deg);
                -o-transform: rotateY(-160deg);
                transform: rotateY(-160deg);}
            100% {-webkit-transform: rotateY(-100deg);
                -ms-transform: rotateY(-100deg);
                -o-transform: rotateY(-100deg);
                transform: rotateY(-100deg);}
        }
        @keyframes flipBook1
        {
            0%   {-webkit-transform: rotateY(-70deg);
                -ms-transform: rotateY(-70deg);
                -o-transform: rotateY(-70deg);
                transform: rotateY(-70deg);}
            30% {-webkit-transform: rotateY(-65deg);
                -ms-transform: rotateY(-65deg);
                -o-transform: rotateY(-65deg);
                transform: rotateY(-65deg);}
                50% {-webkit-transform: rotateY(-60deg);
                -ms-transform: rotateY(-60deg);
                -o-transform: rotateY(-60deg);
                transform: rotateY(-60deg);}
                70% {-webkit-transform: rotateY(-55deg);
                -ms-transform: rotateY(-55deg);
                -o-transform: rotateY(-55deg);
                transform: rotateY(-55deg);}
            100% {-webkit-transform: rotateY(0deg);
                -ms-transform: rotateY(0deg);
                -o-transform: rotateY(0deg);
                transform: rotateY(0deg);}
        }
        div#page 
        {
            width: 99%;
            height: 300px;
            margin:0;
            background-color: #fff;
             padding: 20px 10px 20px 0px;
}

ul#path_tree li span {
    font-size: 12px;
}
.ui-overlay-a, .ui-page-theme-a, .ui-page-theme-a .ui-panel-wrapper {
    text-shadow: none;
}
    </style>
</head>
<body style="margin: 0; overflow: hidden;padding: 15px 15px 0px 15px;zoom: 1;" class="ui-mobile-viewport ui-overlay-a" >
    <div id="tb_search">
        <div>
            案号名称：
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
            案由：
            <input id="txt_ajmc" style="width: 200px;" class="l-text" type="text" name="txt_ajmc" />&nbsp;&nbsp;
            <%--申请人证号：<input id="txt_gh" style="width: 100px;" class="l-text" type="text" name="txt_gh" />&nbsp;&nbsp;--%>
            阅卷人：<input id="txt_mc" style="width: 100px;" class="l-text" type="text" name="txt_mc" />&nbsp;&nbsp;
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="top_div" style="display: none;border: 1px solid rgb(204, 204, 204);
    background: white;
    padding: 5px;
    margin-bottom: 10px;
    border-radius: 10px;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_print" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_yes" style="width: auto; display: inline-block; vertical-align: bottom;
            display: none;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_no" style="width: auto; display: inline-block; vertical-align: bottom;
            display: none;">
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 14px;" id="span_ajname"></span>
        <%--  <span id="time_span"></span>--%>
    </div>
    <div id="layout" style="width: 100%; margin: 0; padding: 0; display: none;">
        <div id="leftFrm" position="left" title='<%=((VersionName)0).ToString() %>目录'>
            <ul id="path_tree">
                <li style="padding: 5px 5px;">请先选择<%=((VersionName)0).ToString() %>，根据<%=((VersionName)0).ToString() %>加载对应目录！</li>
            </ul>
        </div>
        <div id="centterFrm" position="center" title="">
        
            <div data-role="page" data-position="fixed" data-fullscreen="true" id="page" class="book preserve-3d" style="-webkit-perspective: 1000px;
            -moz-perspective: 1000px;
            -ms-perspective: 1000px;
            perspective: 1000px;" >
    
</div>
          
           <%-- <div id="banner">
                <div id="pdfShow" style="margin: 0px; border: 0px; padding: 0px" class="preserve-3d"></div>
                
            </div>
            <div style="position: absolute;bottom: 10px;"><a id="left">上一页</a> <a id="right">下一页</a></div>  --%>
        </div>
      
       
    </div>
    <%--输入用户名密码--%>
    <div id="login_div" style="padding: 0px; display: block;">
        <div id="mainGrid" style="margin: 0px; padding: 0px">
        </div>
        <%--<div style="padding: 10px 60px 20px 60px">
            <form id="login_form" method="post">
            <table class="l-table">
                <tr>
                    <td style="width: 90px;">
                        查阅账号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjzh" type="text" value="" name="txt_yjzh" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        查阅密码：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjmm" type="password" value="" name="txt_yjmm" maxlength="200" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="btn_login" style="width: auto; display: inline-block; vertical-align: bottom;">
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>--%>
    </div>
    <%--审核说明--%>
    <div id="select_div" style="display: none;">
        <div id="tb" style="background-color: #f8f8f8">
            <div style="padding: 4px 5px;">
                <%-- <input id="txt_shsm" class="l-text" type="b" name="txt_name" style="width: 200px" />--%>
                <textarea id="txt_shsm" class="l-text" style="width: 400px; height: 150px;"></textarea>
            </div>
        </div>
    </div>
    <%--   <iframe id="printIframe" src="ReadingFilePrintShow.aspx?pm=20160804112459869" style="position: absolute; width: 1024px; height: 400px;
        left: 220px; top: 0px; z-index: 99999999"></iframe>--%>
    <script type="text/javascript">

        var path_tree;
        var grid;
        var yjdata;        
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });
            $("#btn_login").ligerButton({
                width: 70,
                text: '确定查阅',
                icon: '../../images/NewAdd/cx.png'
            });
            $("#btn_back").ligerButton({
                text: '返 回',
                icon: '../../images/NewAdd/fh.png'
            });
            $("#btn_yes").ligerButton({
                text: '通 过',
                icon: '../../images/NewAdd/tg.png'
            });
            $("#btn_no").ligerButton({
                text: '不通过',
                icon: '../../images/NewAdd/btg.png'
            });
            $("#btn_print").ligerButton({
                text: '打 印',
                icon: '../../images/NewAdd/dy.png',
                click: function () {

                    var gridRow = grid.getSelectedRow();
                    var notes = path_tree.getChecked();
                   // console.log(gridRow);
                    var dataJson = "";
                    if (notes.length == 0) {
                        $.ligerDialog.warn("请选择打印文件页");
                        return false;
                    }
                    var manager = $.ligerDialog.waitting('正在打印,请稍候...');
                    for (var i = 0; i < notes.length; i++) {
                        //dataJson += notes[i].data.WJLJ + "," + notes[i].data.WJMC + "|";
                        dataJson += notes[i].data.WJMC + ",";
                        //判断最后一个文件执行
                        if (i == notes.length - 1) {

                            if (dataJson.length > 0) {
                                $.ajax({
                                    type: "POST",
                                    url: '/Pages/LSYJ/ReadingApply.aspx',
                                    data: { t: 'PrintFile', param: dataJson, xh: gridRow.XH },
                                    //    dataType: 'json',
                                    //timeout: 5000,
                                    cache: false,
                                    beforeSend: function () { },
                                    error: function (xhr) {
                                        $.ligerDialog.error('网络连接错误');
                                        return false;
                                    },
                                    success: function (data) {
                                        if (data.t == "error") {
                                             manager.close();
                                            $.ligerDialog.error(data.v);
                                        } else {
                                            var fileMake = "RecEFileMaker";
                                              <% if (Version == "PSB") {%> 
                                                fileMake="CopEFileMaker";
                                            <%} %>
                                            var parm = fileMake + "://" + data.v;

                                            if (typeof (boundObjectForJS) != 'undefined')
                                                boundObjectForJS.callCsharp(fileMake + "://1234567812345678" + data.v + "@");
                                            else
                                                location.href = parm;

                                            setTimeout(function () {
                                                manager.close();
                                            }, 3000);
                                            // $.ligerDialog.success(data.v)
                                        }
                                    }

                                });
                            } else {
                                manager.close();
                                $.ligerDialog.error("请选择打印文件页");
                            }
                        }
                    }
                }
            });
              var layout = $("#layout").ligerLayout({ leftWidth: 300, space: 4, height: '100%',centerBottomHeight:30, heightDiff:0, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();
            });    

            grid = $("#mainGrid").ligerGrid({
                columns: [

              //  { display: '申请人证号', name: 'LSZH', minWidth: 80 },
                //{ display: '打印申请单号', name: 'DYSQDH', minWidth: 150 },
                
                { display: '案号名称', name: 'BMSAH', minWidth: 200 },
                { display: '案由', name: 'AJMC', minWidth: 200 },
                { display: '阅卷人', name: 'LSXM', minWidth: 100 },
                {display: '申请时间', name: 'SQSJ', minWidth: 150 },
                { display: '证件类型', name: 'LSDW', minWidth: 80, render: function (item) {
                    if (item.LSDW) {
                        if (item.LSDW.toString() == "1")
                            return "身份证";
                        else if (item.LSDW.toString() == "2")
                            return "工作证";
                        else if (item.LSDW.toString() == "3")
                            return "警官证";
                    }
                }
                },
                { display: '证件号', name: 'LSLXDH', minWidth: 150 },
                { display: '阅卷人身份', name: 'LSDWDZ', minWidth: 80, render: function (item) {
                    if (item.LSDWDZ) {
                        if (item.LSDWDZ.toString() == "1")
                            return "当事人";
                        else if (item.LSDWDZ.toString() == "2")
                            return "检查干警";
                        else if (item.LSDWDZ.toString() == "3")
                            return "公安干警";
                        else if (item.LSDWDZ.toString() == "4")
                            return "纪检人员";
                    } 
                } 
                },
                { display: '查询原因', name: 'DELXR', minWidth: 100, render: function (item) {
                    if (item.DELXR) {
                        if (item.DELXR.toString() == "1")
                            return "工作查考";
                        else if (item.DELXR.toString() == "2")
                            return "学术研究";
                        else if (item.DELXR.toString() == "3")
                            return "落实改策";
                        else if (item.DELXR.toString() == "4")
                            return "个人取证";
                        else if (item.DELXR.toString() == "5")
                            return "其他";
                    }
                } 
                },
                { display: '打印人', name: 'DYR', minWidth: 150 },
                { display: '打印时间', name: 'DYSJ', minWidth: 150 },



                //                 { display: '审核单状态', name: 'SQDZT', minWidth: 150, render: function (item) {
                //                     if (item.SQDZT == "Y")
                //                         return "<span style=\"color:green;\">审核通过</span>";
                //                     else if (item.SQDZT == "N")
                //                         return "<span style=\"color:red;\">未通过</span>";
                //                     else
                //                         return "<span style=\"\">待审核</SPAN>";
                //                 }
                //                 },
                //{ display: '审核人', name: 'SHR', minWidth: 150 },
                // { display: '审核说明', name: 'SHSM', minWidth: 150 },
                //{ display: '审核时间', name: 'SHSJ', minWidth: 150 },
                //   {display: '部门受案号', name: 'BMSAH', width: 1, hide: true },
                {display: '', name: 'XH', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/ReadingApply.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '选择打印', click: Read, img: '../../images/NewAdd/dy.png' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        //$.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();


            //查看
            $("#btn_login").click(function () {
                var jdata = $('#login_form').serializeArray();
                jdata[jdata.length] = { name: "t", value: "ReadLogin" };
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/ReadingApply.aspx',
                    data: jdata,
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () { },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t == "error") {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#login_div").hide();
                            $("#top_div").show();
                            $("#layout").show();
                            data = data[0];
                            $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + data.BMSAH + "】");
                            //YJUser =data

                            JZMLTree(data.BMSAH, data.YJXH);
                        }
                    }
                });
            });
            //返回
            $("#btn_back").click(function () {
                $("#login_div").show();
                $("#tb_search").show();
                $("#top_div").hide();
                $("#layout").hide();
               grid.reload();
            });

            //var YJUser ' %=YJUser %';
            //            if(YJUser.BMSAH && YJUser.YJXH)
            //            {
            //                $("#span_ajname").html("当前卷宗：" + YJUser.AJMC + "&nbsp;【" + YJUser.BMSAH + "】");
            //                $("#login_div").hide();
            //                $("#top_div").show();
            //                $("#layout").show();
            //                JZMLTree(YJUser.BMSAH, YJUser.YJXH);
            //            }



        });



        //绑定文件目录
        function JZMLTree(bmsah, yjxh) {

            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/ReadingApply.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh,wjtype:'N' },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 600, 
                    onSuccess: function (data) {
                   // console.log(data);
                        if (data.t) {
                            $("#path_tree").html("<li style=\" padding:5px 5px;\">" + data.v + "</li>");
                        }
                       
                    }, onBeforeExpand: function (node) {
                        if (node.data.children.length == 0) {                            
                            JZMLWJTree(node, "false");
                        }
                    }, onSelect: function (node) {                       
                        if ($.trim(node.data.WJMC) && $.trim(node.data.WJLJ)) {
                           /* $("#pdfFrm").remove();
                            $("#pdfShow").ligerPanel({
                                title: node.data.text,
                                width: '100%',
                                frameName: 'pdfFrm',
                                height: h,
                                url: '/Pages/LSYJ/ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))
                            });
                            */
                           $.ajax({
                                type:"get",
                                url: '/Pages/LSYJ/ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ)),
                                success: function (data) {
                                //console.log("+++++++++");
                                //console.log(node.data);
                                console.log("+++++++++");
                                $("#page div").remove();
                                     var src="";
                                  src+="<div style='padding: 10px;width:1043px;border:1px solid red;margin-top:10px' class='page-front'>"+node.data.text+"</div>";
                              
                                  $("#page").append(src); 
                                

                                }
                            });  
                          

                        }

                    }, onCheck: function (node, checked) {
                        if (checked) {
                            JZMLWJTree(node, checked);
                        }
                    }
                });

            }
        }

     
        //绑定文件
        function JZMLWJTree(node, ischecked) {
        console.log("++++++++++++++=");
        console.log(node.data);
        console.log("++++++++");
        //$("#pdfShow").remove();
            if (node.data.ISLEAF == "0") {
                $.ligerDialog.waitting('数据读取中,请稍候...');
                $.post("/Pages/LSYJ/ReadingApply.aspx",
                 { t: "GetMlTree",
                  level: 3, 
                  pid: node.data.id, 
                  ischecked: ischecked, 
                  yjxh: path_tree.options.parms.yjxh, 
                  bmsah: path_tree.options.parms.bmsah
                  },function (newData) {
               console.log("----------");
               console.log(newData);
              $(".l-layout-content #path_tree li ul li .l-checkbox").click(function(){
                    if($(this).hasClass('l-checkbox-checked')){
                    //alert("已有选中了的");
                    }else{
                    $(".l-layout-content #path_tree li ul li  .l-checkbox").removeClass("l-checkbox-checked");
                     $(".l-layout-content #path_tree li ul li  .l-checkbox").addClass("l-checkbox-unchecked");
                     if( $(".l-layout-content #path_tree li ul li .l-checkbox-unchecked").parent(".l-body").siblings().length  > 0){
                        $("#page div").remove();
                     }else{
                       // alert("meiyouul");
                     }  
                    }
              })
        $("#page div").remove();
               var src="";
 $(newData).each(function(index){
        var indexData=index+1;
        src+="<div id='div" +indexData+ "'  style='display:none;-webkit-transform-style: preserve-3d;-moz-transform-style: preserve-3d;-ms-transform-style: preserve-3d;transform-style: preserve-3d;' class='s_page  book-page-box  preserve-3d' ><div class='page-front'>"+this.text+"</div></div>";
    });  
    $("#page").append(src);
    setInterval(function() {
            var height = $(".l-layout-center").height();
            h1 = height -31;
            $(".page-front").css("height", h1 + "px");
    },1); 

    $("#div1").css({"display":"block"});
    for (let i=0;i<$(".s_page").length-1;i++){
        $(".s_page").eq(i).on("swipeleft",function(){
        $(".s_page").eq(i).addClass("flip-animation-2");
            $(".s_page").eq(i).removeClass("flip-animation-1");
            setTimeout(function(){
                $(".s_page").eq(i).hide();
                $(".s_page").eq(i+1).show(1);
            },800);
        });
    }
    for (let i=1;i<$(".s_page").length;i++){
        $(".s_page").eq(i).on("swiperight",function(){
            $(".s_page").eq(i-1).removeClass("flip-animation-2");  
            $(".s_page").eq(i-1).addClass("flip-animation-1");
            setTimeout(function(){
                $(".s_page").eq(i).hide();
                $(".s_page").eq(i-1).show(1);
                
             },100);
        });
    }

         /*   $(newData).each(function(){
                src+="<div  style='width: 1043px;padding:10px;margin-top:10px;'><span class='page-front'>"+this.text+"</span></div>";

                });
                $("#pdfShow").append(src);
             var curIndex = 0, //当前index
             imgLen = $("#pdfShow div").length;   //数据总条数
             var Width=imgLen*1065;
            $("div#pdfShow").css({widtn:Width});
            //左箭头点击处理
            $("#left").click(function(){
               curIndex = (curIndex > 0) ? (--curIndex) :curIndex;
                console.log(curIndex);
                $('#pdfShow div').eq(curIndex).removeClass("flip-animation-1");
                $('#pdfShow div').eq(curIndex).addClass("flip-animation-2");
                var goLeft = curIndex * 1065;
                console.log(goLeft);
                setTimeout(function(){
                    $("#pdfShow").animate({marginLeft: "-" + goLeft + "px"},10);
                },1000);
            });
            //右箭头点击处理
            $("#right").click(function(){
               curIndex = (curIndex < imgLen - 1) ? (++curIndex) :curIndex;
                console.log(curIndex);
                var curIndex1=curIndex-1;
                $('#pdfShow div').eq(curIndex1).removeClass("flip-animation-2");
                $('#pdfShow div').eq(curIndex1).addClass("flip-animation-1");
                    var goLeft = curIndex * 1065;
                setTimeout(function(){
                    $("#pdfShow").animate({marginLeft: "-" + goLeft + "px"},10);
                },1000);
            });  */

               console.log("-----------");
                    if (newData.t) {
                        //$.ligerDialog.error(newData.v);
                    } else {

                        //alert(node.data.children.length)
                        //console.log(JSON.stringify(newData));
                        $(node.target).children("ul").remove();
                        path_tree.append(node.target, newData);
                        $(node.target).find(".l-expandable-close").click();
                      
                    }

                    $.ligerDialog.closeWaitting();
                },'json');
            }
        }

   

        ///阅卷
        function Read() {
            //            var jdata = $('#login_form').serializeArray();
            //            jdata[jdata.length] = { name: "t", value: "ReadLogin" };
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/ReadingApply.aspx',
                    data: { t: "ReadLogin", yjxh: cksld.YJXH },
                    dataType: 'json',
                    timeout: 50000,
                    cache: false,
                    beforeSend: function () { },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                    //打印出选中的案件
                    //console.log(data);
                        if (data.t == "error") {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#tb_search").hide();
                            $("#login_div").hide();
                            $("#top_div").show();
                            $("#layout").show();
                          
                            data = data[0];
                            $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + cksld.BMSAH + "】");
                            //YJUser =data
                            yjdata = data;
                            JZMLTree(data.BMSAH, data.YJXH);

                        }
                    }
                });
            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }

        }


        //通过
        $("#btn_yes").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {

                $.ligerDialog.confirm('确定通过该申请吗?', function (yes) {
                    if (yes == true) {
                        $.post("/Pages/LSYJ/ReadingApply.aspx", { t: "applypass", lszh: cksld.LSZH, dysqdh: cksld.DYSQDH, sqsj: cksld.SQSJ }, function (newData) {
                            if (newData.t) {
                                if (newData.t == "win") {
                                    $.ligerDialog.success(newData.v);
                                }
                                else {
                                    $.ligerDialog.error(newData.v);
                                }

                            } else {
                                $.ligerDialog.error(newData.v);
                            }

                            $.ligerDialog.closeWaitting();
                        }, 'json');

                    }

                })
            }
        });

        //不通过
        $("#btn_no").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ligerDialog.confirm('确定不通过该申请吗?', function (yes) {
                    if (yes == true) {
                        //审核说明
                        $.ligerDialog.show({ title: '审核说明', target: $('#select_div'), width: 450,
                            buttons: [{ text: '确定', onclick: function (item, dialog) {
                                $.post("/Pages/LSYJ/ReadingApply.aspx", { t: "applyno", lszh: cksld.LSZH, dysqdh: cksld.DYSQDH, sqsj: cksld.SQSJ, shsm: $("#txt_shsm").val() }, function (newData) {
                                    if (newData.t) {
                                        if (newData.t == "win") {
                                            $.ligerDialog.success(newData.v);
                                        }
                                        else {
                                            $.ligerDialog.error(newData.v);
                                        }

                                    } else {
                                        $.ligerDialog.error(newData.v);
                                    }

                                    $.ligerDialog.closeWaitting();
                                }, 'json');

                                dialog.hidden();
                            }, cls: 'l-dialog-btn-highlight'
                            },
                    { text: '取消', onclick: function (item, dialog) {
                        dialog.hidden();
                    }
                    }],
                            isResize: false
                        });
                    }

                })

            }
        });
          function resizeLayout() {
                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
               // $(".l-layout-center").css("width", width - 30 + "px");
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = height - 40;
                //$(".l-layout-center").css("height", h-21+"px");
               // $(".l-layout-left").css("height", h-21+"px");
                $("#leftFrm").height(h+13+"px");
                $("#pdfFrm").height(h+13+"px");
            }
        $(document).ready(function () {

            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        ajmc: $("#txt_ajmc").val(),
                        gh: $("#txt_gh").val(),
                        //mc: $("#txt_mc").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });
        $(function () {

            var height = $(".l-layout-center").height();
            var width = $(".l-layout-center").width();
            h = height - 40;
          //  $(".page-front").css("height", h -41 + "px");
            $(".l-layout-center").css("height", h - 23 + "px");
            $(".l-layout-center").css("width", width - 30 + "px");
            $(".l-layout-left").css("height", h -23 + "px");
            $(".l-layout-content").css("height", h -23 + "px");
           // alert( $(".l-layout-content").height());
        })
    </script>

</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
