<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPSH.aspx.cs" Inherits="WebUI.Pages.LSYJ.FPSH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>阅卷分配</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <link rel="stylesheet" type="text/css" href="/AddFile/diyUpload/css/webuploader.css" />
    <link rel="stylesheet" type="text/css" href="/AddFile/diyUpload/css/diyUpload.css" />
    <script type="text/javascript" src="/AddFile/diyUpload/js/webuploader.html5only.min.js"></script>
    <script type="text/javascript" src="/AddFile/diyUpload/js/diyUpload.js"></script>
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
        .l-text
        {
            width: 150px;
        }
        
        #add_form table tr td, #form_dj table tr td
        {
            padding: 8px 0px;
        }
        
        #addls_form table tr td
        {
            padding: 8px 0px;
        }
        .l-table
        {
            width: 100%;
        }
        .l-table tr td
        {
            padding: 5px 2px;
        }
        
        .l-span
        {
        }
        #demo
        {
            margin: 20px 10px;
            width: 95%;
            min-height: 390px; /*  background: #CF9;*/
        }
        #leftFrm{ overflow:auto !important ;}
        div#tb {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
        }
         .l-layout-left,.l-layout-center {
            border: none;
            border-radius: 10px;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px;  overflow: hidden;">
    <div id="tb">
        <div >
            案号名称：
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
            案由：
            <input id="txt_ajmc" style="width: 200px;" class="l-text" type="text" name="txt_ajmc" />&nbsp;&nbsp;
           <%-- 律师工号：<input id="txt_gh" style="width: 100px;" class="l-text" type="text" name="txt_gh" />&nbsp;&nbsp;--%>
            阅卷人：<input id="txt_mc" style="width: 100px;" class="l-text" type="text" name="txt_mc" />&nbsp;&nbsp;
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
            &nbsp;&nbsp;&nbsp;
            <div id="yes" style="width: auto; display: inline-block; vertical-align: bottom;
            ">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="no" style="width: auto; display: inline-block; vertical-align: bottom;
            ">
        </div>
        </div>
    </div>
     <div id="top_div" style="border: 1px solid rgb(204, 204, 204);
    background: white;
    padding: 5px;
    margin-bottom: 10px;
    border-radius: 10px; display: none;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_yes" style="width: auto; display: inline-block; vertical-align: bottom;
            ">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_no" style="width: auto; display: inline-block; vertical-align: bottom;
            ">
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
            <div id="pdfShow" style="margin: 0px; border: 0px; padding: 0px">
            </div>
        </div>
    </div>
    <div id="login_div" style="padding: 0px; display: block;">
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
  </div>
    <%--审核说明--%>
    <div id="select_div" style="display: none;">
        <div id="div_shsm" style="background-color: #f8f8f8">
            <div style="padding: 4px 5px;">
                <%-- <input id="txt_shsm" class="l-text" type="b" name="txt_name" style="width: 200px" />--%>
                <textarea id="txt_shsm" class="l-text" style="width: 400px; height: 150px;"></textarea>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var grid = null;
        var h = 0;
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        $(function () {
            $('#btn_search').ligerButton({
                text: '查询',
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

            $("#yes").ligerButton({
                text: '通 过',
                icon: '../../images/NewAdd/tg.png'
            });
            $("#no").ligerButton({
                text: '不通过',
                icon: '../../images/NewAdd/btg.png'
            });

            var layout = $("#layout").ligerLayout({ leftWidth: 300, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
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
                { display: '案号名称', name: 'BMSAH', width: 200 },
                //{ display: '案件编号', name: 'AJBH', minWidth: 80 },
                {display: '案由', name: 'AJMC', minWidth: 200 },
                { display: '阅卷人', name: 'MC', minWidth: 100 },
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
                { display: '审核部门', name: 'BMMC', minWidth: 80 },
                { display: '审核人', name: 'SHR', minWidth: 80 },
                   { display: '审核状态', name: 'SQDZT', minWidth: 150, render: function (item) {
                       if (item.SQDZT == "Y")
                           return "<span style=\"color:green;\">审核通过</span>";
                       else if (item.SQDZT == "N")
                           return "<span style=\"color:red;\">未通过</span>";
                       else if (item.SQDZT == "X")
                           return "<span style=\"color:#FFB90F;\">已写卡</span>";
                       else if (item.SQDZT == "D")
                           return "<span style=\"#D1D1D1;\">已阅卷</span>";
                       else
                           return "<span style=\"\">待审核</SPAN>";
                   }
               },
                    { display: '审核时间', name: 'SHSJ', minWidth: 150 },
                { display: '审核说明', name: 'SHSM', minWidth: 150 },
               
             
                //{ display: '阅卷密码', name: 'YJMM', width: 150 },
                //                { display: '申请案件名称', name: 'YJSQDM', minWidth: 150 },


                //                { display: '阅卷状态', width: 80, name: 'YJKSSJ'
                //                    , render: function (item) {
                //                        var nowTime = '<%=nowTime %>';
                //                        var kssj = item.YJKSSJ;
                //                        var jssj = item.YJJSSJ;
                //                        nowTime = Date.parse(nowTime.replace(/-/g, '/'));
                //                        kssj = Date.parse(kssj.replace(/-/g, '/'));
                //                        jssj = Date.parse(jssj.replace(/-/g, '/'));
                //                        if (nowTime < kssj)
                //                            return "<font style='color:red;'>未到时</font>";
                //                        else if (nowTime > kssj && nowTime < jssj)
                //                            return "<font style='color:green;'>正常 </font>";
                //                        else if (nowTime > jssj)
                //                            return "<font style='color:gray;'>过期</font>";
                //                        else
                //                            return "";
                //                    }
                //                },
                //                { display: '开始时间', name: 'YJKSSJ', width: 150 },
                //                { display: '结束时间', name: 'YJJSSJ', width: 150 },
                {display: '分配人', name: 'JDR', minWidth: 80 },
                { display: '分配时间', name: 'JDSJ', width: 150 },
                //                 { display: '阅卷说明', name: 'SQSM', width: 150 },
                {display: '阅卷序号', name: 'YJXH', width: 1, hide: 'true' }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/FPSH.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
//                    { text: '阅卷登记', click: addDown, icon: 'add' },
                    // { line: true },
                    //                                    { text: '审核', click: examineData, img: '/images/icons/edit.png' },
                    //                                    {line: true },
                    //                                    { text: '修改', click: editData, icon: 'modify' },

                                

                    //{text: '分配', click: distribution, img: '/images/icons/edit.png' },
                    //{ line: true },
//                {text: '删除', click: deleteData, icon: 'delete' },
                    //{ line: true }, 
                    // { text: '律师管理', click: deleteData, icon: 'add' }
                     {text: '查看详细', click: Read, img: '/images/icons/edit.png' },
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        // $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
        });

        ///阅卷审核
        function Read() {
            //            var jdata = $('#login_form').serializeArray();
            //            jdata[jdata.length] = { name: "t", value: "ReadLogin" };
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/FPSH.aspx',
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
                        if (data.t == "error") {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#tb").hide();
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
        //返回
        $("#btn_back").click(function () {
            $("#login_div").show();
            $("#tb").show();
            $("#top_div").hide();
            $("#layout").hide();
            grid.reload();
        });

        //绑定文件目录
        function JZMLTree(bmsah, yjxh) {

            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/FPSH.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh,wjtype:"N" },
                    isExpand: 2,
                    checkbox: false,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 600,
                    onSuccess: function (data) {
                        if (data.t) {
                            $("#path_tree").html("<li style=\" padding:5px 5px;\">" + data.v + "</li>");
                        }

                    }, onBeforeExpand: function (node) {
                        if (node.data.children.length == 0) {
                            JZMLWJTree(node, "false");
                        }
                    }, onSelect: function (node) {
                        if ($.trim(node.data.WJMC) && $.trim(node.data.WJLJ)) {
                            $("#pdfFrm").remove();
                            $("#pdfShow").ligerPanel({
                                title: node.data.text,
                                width: '100%',
                                frameName: 'pdfFrm',
                                height: h,
                                url: '/Pages/LSYJ/ReadingFile.aspx?wjmc=' + encodeURI(encodeURI(node.data.WJMC)) + '&wjlj=' + encodeURI(encodeURI(node.data.WJLJ))
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
            if (node.data.ISLEAF == "0") {
                $.ligerDialog.waitting('数据读取中,请稍候...');
                $.post("/Pages/LSYJ/FPSH.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
                    if (newData.t) {
                        //$.ligerDialog.error(newData.v);
                    } else {

                        //alert(node.data.children.length)
                        //console.log(JSON.stringify(newData));
                        $(node.target).children("ul").remove();
                        path_tree.append(node.target, newData);
                        $(node.target).find(".l-expandable-close").click();
                    }
                    resizeLayout();
                    $.ligerDialog.closeWaitting();
                }, 'json');
            }
        }

        //通过
        $("#btn_yes").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                if (cksld.SQDZT != "Y" && cksld.SQDZT != "X" && cksld.SQDZT != "D") {
                    $.ligerDialog.confirm('确定通过该申请吗?', function (yes) {
                        if (yes == true) {
                            $.post("/Pages/LSYJ/FPSH.aspx", { t: "applypass", yjsqd: cksld.YJSQDH, state: "Y" }, function (newData) {
                                if (newData.t) {
                                    if (newData.t == "win") {
                                        $.ligerDialog.success(newData.v);
                                        grid.reload();
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
                else {
                    $.ligerDialog.warn('该案件只能在待审核或未通过的情况下进行审核');
                }

            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }
        });
        //通过
        $("#yes").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                if (cksld.SQDZT != "Y" && cksld.SQDZT != "X" && cksld.SQDZT != "D") {
                    $.ligerDialog.confirm('确定通过该申请吗?', function (yes) {
                        if (yes == true) {
                            $.post("/Pages/LSYJ/FPSH.aspx", { t: "applypass", yjsqd: cksld.YJSQDH, state: "Y" }, function (newData) {
                                if (newData.t) {
                                    if (newData.t == "win") {
                                        $.ligerDialog.success(newData.v);
                                        grid.reload();
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
                else {
                    $.ligerDialog.warn('该案件只能在待审核或未通过的情况下进行审核');
                }
            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }
        });


        //不通过
        $("#btn_no").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                if (cksld.SQDZT != "Y" && cksld.SQDZT != "X" && cksld.SQDZT != "D") {
                    $.ligerDialog.confirm('确定不通过该申请吗?', function (yes) {
                        if (yes == true) {
                            //审核说明
                            $.ligerDialog.show({ title: '审核说明', target: $('#select_div'), width: 450,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    $.post("/Pages/LSYJ/FPSH.aspx", { t: "applypass", yjsqd: cksld.YJSQDH, state: "N", shsm: $("#txt_shsm").val() }, function (newData) {
                                        if (newData.t) {
                                            if (newData.t == "win") {
                                                $.ligerDialog.success(newData.v);
                                                grid.reload();
                                                $("#txt_shsm").val("");
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
                else {
                    $.ligerDialog.warn('该案件只能在待审核或未通过的情况下进行审核');
                }
            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }
        });

        //不通过
        $("#no").click(function () {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                if (cksld.SQDZT != "Y" && cksld.SQDZT != "X" && cksld.SQDZT != "D") {
                    $.ligerDialog.confirm('确定不通过该申请吗?', function (yes) {
                        if (yes == true) {
                            //审核说明
                            $.ligerDialog.show({ title: '审核说明', target: $('#select_div'), width: 450,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    $.post("/Pages/LSYJ/FPSH.aspx", { t: "applypass", yjsqd: cksld.YJSQDH, state: "N", shsm: $("#txt_shsm").val() }, function (newData) {
                                        if (newData.t) {
                                            if (newData.t == "win") {
                                                $.ligerDialog.success(newData.v);
                                                grid.reload();
                                                $("#txt_shsm").val("");
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
                } else {
                    $.ligerDialog.warn('该案件只能在待审核或未通过的情况下进行审核');
                }

            }
            else {
                $.ligerDialog.warn('请选择审核案件');
            }
        });

        //提交保存数据
        function submitForm() {
            var isUp = false;
            var jdata = $('#form_dj').serializeArray();
            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else {
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp = true;
            }
            $.ajax({
                type: "POST",
                url: "/Pages/LSYJ/LSFP.aspx",
                data: jdata,
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                },
                error: function (xhr) {
                    $.ligerDialog.error('网络连接错误');
                    return false;
                },
                success: function (data) {
                    if (data.t == "win") {
                        $("#form_dj")[0].reset();
                        $.ligerDialog.hide();
                        grid.reload();
                        $.ligerDialog.success(data.v);
                        location.href = "LSFPAJSJ.aspx";
                    } else {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }

        //提交审核数据
        function examineForm() {
            var jdata = $('#examine_form').serializeArray();
            jdata[jdata.length] = { name: "t", value: "ExUpData" };
            $.ajax({
                type: "POST",
                url: "/Pages/LSYJ/LSFP.aspx",
                data: jdata,
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                },
                error: function (xhr) {
                    $.ligerDialog.error('网络连接错误');
                    return false;
                },
                success: function (data) {
                    if (data.t == "win") {
                        $("#examine_form")[0].reset();
                        $.ligerDialog.hide();
                        grid.reload();
                        $.ligerDialog.success(data.v);
                    } else {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
        }


        //审核
        function examineData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/LSYJ/LSFP.aspx',
                    data: { t: "GetModel", id: cksld.YJSQDH, cs: cksld.YJSQDM },
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () {
                    },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#key_ex_hidd").val(data.YJSQDH);
                            //$("#span_lszh").html(data.LSZH);
                            $("#span_yjsqdh").html(data.YJSQDH);
                            $("#span_yjsqdm").html(data.YJSQDM);
                            $("#span_sqsm").html(data.SQSM);
                            $("#span_sqsj").html(data.SQSJ);
                            $("#txt_shsm").html(data.SHSM);
                            if (data.SQDZT == "Y")
                                $("#rad_sqdzt_yes").click();
                            else if (data.SQDZT == "N")
                                $("#rad_sqdzt_no").click();
                            $.ligerDialog.open({ title: '申请审核', target: $('#examine_div'), width: 560,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    examineForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#examine_form")[0].reset();
                                    dialog.hidden();
                                }
                                }], isResize: true
                            });
                        }
                    }
                });
            }
            else
                $.ligerDialog.warn('请先选择一个需要编辑的配置信息');
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
                        mc: $("#txt_mc").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });
        function resizeLayout() {
            var height = $(".l-layout-center").height();
            var width = $(".l-layout-center").width();
           // $(".l-layout-center").css("width", width - 30 + "px");
            $(".l-grid2").width(width - 27);
            width = $(".l-layout-left").width();
            h = height - 40;
           // $(".l-layout-center").css("height", h - 21 + "px");
           // $(".l-layout-left").css("height", h - 21 + "px");
            $("#leftFrm").height(h+13+"px");
            $("#pdfFrm").height(h+13+ "px");
        }
        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("ajmc", $("#txt_ajmc").val());
            grid.setParm("gh", $("#txt_gh").val());
            grid.setParm("mc", $("#txt_mc").val());
        }
        $(function () {

            var height = $(".l-layout-center").height();
            var width = $(".l-layout-center").width();
            h = height - 40;
            $(".l-layout-center").css("height", h - 21 + "px");
            $(".l-layout-center").css("width", width - 30 + "px");
            $(".l-layout-left").css("height", h - 21 + "px");
        })
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
