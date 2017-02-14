<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSGL.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSGL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>律师管理</title>
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
            border-bottom: 1px solid #a3c0e8;
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
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #a3c0e8;
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
        .l-text-combobox
        {
            display: inline-table;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
         #demo
        {
            margin: 20px 10px;
            width: 95%;
            min-height: 390px; /*  background: #CF9;*/
        }
    </style>
</head>
<body style="margin: 0; overflow: hidden; padding: 0px;">
   
    <div id="tb" style="background-color: #f8f8f8">
        <div style="padding: 4px 5px;">
            律师证号：
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
            律师姓名：
            <input id="txt_lsxmcx" style="width: 150px;" class="l-text" type="text" name="txt_lsxmcx" />&nbsp;&nbsp;
          
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>



    <div id="top_div" style="padding: 5px; display: none;">
        <div id="btn_back" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_yes" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_no" style="width: auto; display: inline-block; vertical-align: bottom;">
        </div>
        &nbsp;&nbsp;&nbsp;
        <div id="btn_print" style="width: auto; display: inline-block; vertical-align: bottom;">
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
    <%--输入用户名密码--%>
    <div id="login_div" style="padding: 0px; display: block;">
        <div id="mainGrid" style="margin: 0px; padding: 0px">
        </div>
    </div>
    <%--添加律师--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 20px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        律师证号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lszh" type="text" name="txt_lszh" style="width: 150px;"
                            maxlength="350" />
                    </td>
                     <td>
                        &nbsp;&nbsp;&nbsp;律师姓名：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input class="l-text" id="txt_lsxm" type="text" name="txt_lsxm" style="width: 150px;"
                            maxlength="350" />
                    </td>

                </tr>

                <tr>
                   
                    <td>
                        律师单位：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdw" type="text" name="txt_lsdw" style="width: 150px;"
                            maxlength="350" />
                    </td>
                      <td>
                        &nbsp;&nbsp;&nbsp;律师单位地址：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdwdz" type="text" name="txt_lsdwdz" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                  
                    <td>
                        律师单位邮政号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdwyzh" type="text" name="txt_lsdwyzh" style="width: 150px;"
                            maxlength="350" />
                    </td>
                     <td>
                         &nbsp;&nbsp;&nbsp;律师联系电话：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lslxdh" type="text" name="txt_lslxdh" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                   
                    <td>
                       律师手机：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lssj" type="text" name="txt_lssj" style="width: 150px;"
                            maxlength="350" />
                    </td>
                       <td>
                       &nbsp;&nbsp;&nbsp; 第二联系人：
                    </td>
                    <td>
                        <input class="l-text" id="txt_delxr" type="text" name="txt_delxr" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                 
                    <td>
                        第二联系人电话：
                    </td>
                    <td>
                        <input class="l-text" id="txt_delxrdh" type="text" name="txt_delxrdh" style="width: 150px;"
                            maxlength="350" />
                    </td>
                     <td>
                         &nbsp;&nbsp;&nbsp;律师资格有效期：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lszgyxsj" type="text" name="txt_lszgyxsj" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                   
                    <td>
                       是否吊销资格：
                    </td>
                    <td>
                        <select id="txt_sfdxzg" class="l-text" name="txt_sfdxzg" style="width: 150px">
                            <option value="0">否</option>
                            <option value="1">是</option>
                        </select>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        律师资质文件：
                    </td>
                    <td colspan="3">
                        <%--<input class="l-text" id="txt_lszzwj" type="file" name="filename" style="width: 250px;"
                            maxlength="350" />--%>
                        <div id="btn_addfile" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <input type="hidden" id="arrfile" name="key_arrlife" value="" />
                        <a id="numberfile" href="javascript:" onclick="ImagesShow()"></a>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        律师信息备注：
                    </td>
                    <td colspan="3">
                        <%--<input class="l-text" id="txt_lsxxbz" type="text" name="txt_lsxxbz" style="width: 420px;"
                            maxlength="350" />--%>
                        <textarea class="l-text" id="txt_lsxxbz" name="txt_lsxxbz" style="width: 420px; height: 120px"></textarea>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div id="demo" style="display: none">
        <div id="as">
        </div>
    </div>
    <div id="fileshow" style="display: none">
    </div>
    <script type="text/javascript">
        var path_tree;
        var grid;
        var yjdata;
        var time;
        var jicon = "/images/jzimage/ddca32c7-d719-4001-90ca-58efc4eca2b4.png";
        var mlicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var yicon = "/images/jzimage/a9507576-681a-476f-bcfa-31051ad1c043.png";
        var wjicon = "/images/jzimage/c0cf1f33-2a72-40b7-b97b-da0d08f7f07a.png";
        var fileUp;
        var arr = [];

        $(function () {
            fileUp = $("#as").diyUpload({
                url: '/AddFile/server/fileupload.ashx',
                success: function (data) {
                    arr.push(data.id);

                },
                error: function (err) {
                    console.info(err);
                },
                buttonText: "选择文件",
                chunked: true,
                // 分片大小
                chunkSize: 512 * 1024,
                //最大上传的文件数量, 总文件大小,单个文件大小(单位字节);
                fileNumLimit: 50,
                fileSizeLimit: 500000 * 1024,
                fileSingleSizeLimit: 50000 * 1024,
                accept: {}
            });
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
            });
            $("#btn_login").ligerButton({
                width: 80,
                text: '确定查阅',
                icon: '/LigerUI/lib/LigerUI/skins/icons/right.gif'
            });
            $("#btn_back").ligerButton({
                width: 80,
                text: '返 回',
                icon: '/LigerUI/lib/LigerUI/skins/icons/back.gif'
            });
            $("#btn_yes").ligerButton({
                width: 80,
                text: '通 过',
                icon: '/LigerUI/lib/LigerUI/skins/icons/right.gif'
            });
            $("#btn_no").ligerButton({
                width: 80,
                text: '不通过',
                icon: '/LigerUI/lib/LigerUI/skins/icons/delete.gif'
            });
            $('#btn_addfile').ligerButton({
                text: '上传文件',
                icon: '/LigerUI/lib/LigerUI/skins/icons/add.gif'
            });

            $("#txt_lszh").ligerTextBox({});

            $("#btn_print").ligerButton({
                width: 80,
                text: '打印',
                icon: '/LigerUI/lib/LigerUI/skins/icons/delete.gif',
                click: function () {
                    // $("#pdfFrm").contents().find("#print").click();
                    // window.frames["pdfFrm"].document.getElementById("myH1");
                    //                    var turl = document.getElementById("pdfFrm").src;
                    //                    var newW = window.open("http://10.1.1.30:92/Pages/LSYJ/newpdf1.pdf");
                    //                    newW.print();

                    $.ajax({
                        type: "POST",
                        url: '/Pages/LSYJ/LSGL.aspx',
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

                            }
                        }
                    });


                }
            });

            var layout = $("#layout").ligerLayout({ leftWidth: 200, space: 4, height: '100%', heightDiff: 0, onEndResize: function () {
                resizeLayout();
            }, fn: function () { resizeLayout() }
            });
            $(window).resize(function () {
                resizeLayout();
            });
            $(window).load(function () {
                resizeLayout();
            });
            function resizeLayout() {
                var height = $(".l-layout-center").height();
                var width = $(".l-layout-center").width();
                $(".l-grid2").width(width - 27);
                width = $(".l-layout-left").width();
                h = height - 30;
                $("#leftFrm").height(h);
                $("#pdfFrm").height(h);
            }

            $("#txt_lszgyxsj").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            grid = $("#mainGrid").ligerGrid({
                columns: [

                { display: '律师证号', name: 'LSZH', minWidth: 80 },

                { display: '律师姓名', name: 'LSXM', minWidth: 150 },
                { display: '律师所属单位', name: 'LSDW', minWidth: 150 },
                { display: '律师联系电话', name: 'LSLXDH', minWidth: 150 },
                { display: '律师资格有效时间', name: 'LSZGYXSJ', minWidth: 150, render: function (item) {
                    if (item.LSZGYXSJ) {
                        return item.LSZGYXSJ.toString().substring(0, 10);
                    }
                    else {
                        return "";
                    }
                }
                },
                { display: '是否吊销资格', name: 'SFDXZG', minWidth: 150, render: function (item) {
                    if (item.SFDXZG == "N")
                        return "<span style=\"\">否</span>";
                    else
                        return "<span style=\"color:red;\">是</span>";
                }
                },
                { display: '创建时间', name: 'CJSJ', minWidth: 150 },
                { display: '', name: 'LSZH', width: 1, hide: true }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/LSGL.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                { text: '添加', click: addDown, img: '/LigerUI/lib/LigerUI/skins/icons/add.gif' },
                { text: '修改', click: editData, img: '/LigerUI/lib/LigerUI/skins/icons/edit.gif' },
                { text: '删除', click: deleteData, img: '/LigerUI/lib/LigerUI/skins/icons/delete.gif' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        //$.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();

            //添加
            function addDown() {
                $.diyUploadClear(fileUp);
                arr = [];
                $("#numberfile").text("");
                $("#arrfile").val("");
                $('#key_hidd').val('');
                $("#add_form")[0].reset();
                $("#txt_lszh").ligerGetTextBoxManager().setEnabled();
                $.ligerDialog.open({ title: '添加律师', target: $('#add_div'), width: 600,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        submitForm();
                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#add_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
                });
            }

            //提交保持数据
            function submitForm() {


                var isUp = false;
                var jdata = $('#add_form').serializeArray();
                if ($.trim($("#key_hidd").val()) == "")
                    jdata[jdata.length] = { name: "t", value: "AddData" };
                else {
                    jdata[jdata.length] = { name: "t", value: "UpData" };
                    isUp = true;
                }
                $.ajax({
                    type: "POST",
                    url: "/Pages/LSYJ/LSGL.aspx",
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
                            $("#add_form")[0].reset();
                            arr = [];
                            $.diyUploadClear(fileUp);
                            $.ligerDialog.hide();
                            grid.reload();
                            $.ligerDialog.success(data.v);
                        } else {
                            $.ligerDialog.error(data.v);
                        }
                    }
                });
            }
            //删除
            function deleteData() {
                var arrck = grid.getSelectedRow();
                if (arrck) {
                    var ar = new Array();
                    ar[0] = { name: "id", value: arrck.LSZH };
                    ar[1] = { name: "t", value: "DelData" };
                    $.ligerDialog.confirm('确定是否删除?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: "/Pages/LSYJ/LSGL.aspx",
                                data: ar,
                                dataType: 'json',
                                timeout: 5000,
                                cache: false,
                                beforeSend: function () { },
                                error: function (xhr) {
                                    $.ligerDialog.error('网络连接错误');
                                    return false;
                                },
                                success: function (data) {
                                    if (data.t == "win") {
                                        var prowdata = grid.getSelectedRow();
                                        grid.deleteRow(prowdata);
                                        $.ligerDialog.success(data.v);
                                    } else
                                        $.ligerDialog.error(data.v);
                                }
                            });
                        }
                    });
                } else
                    $.ligerDialog.warn('请选择一条要删除的律师信息');
            }
            //修改
            function editData() {
                var cksld = grid.getSelectedRow();
                if (cksld != null) {
                    $.diyUploadClear(fileUp);
                    arr = [];
                    $.ajax({
                        type: "POST",
                        url: "/Pages/LSYJ/LSGL.aspx",
                        data: { t: "GetModel", id: cksld.LSZH },
                        dataType: 'json',
                        timeout: 5000,
                        cache: false,
                        beforeSend: function () {
                            // return $("#add_form").form('enableValidation').form('validate');
                        },
                        error: function (xhr) {
                            $.ligerDialog.error('网络连接错误');
                            return false;
                        },
                        success: function (data) {
                            if (data.t) {
                                $.ligerDialog.error(data.v);
                            } else {
                                $("#numberfile").text("已存在" + data[0].WJNUM + "个文件");
                                $("#txt_lszh").val(data[0].LSZH);
                                $("#key_hidd").val(data[0].LSZH);
                                $("#txt_lsxm").val(data[0].LSXM);
                                $("#txt_lsdw").val(data[0].LSDW);
                                $("#txt_lsdwdz").val(data[0].LSDWDZ);
                                $("#txt_lsdwyzh").val(data[0].LSDWYZHM);
                                $("#txt_lslxdh").val(data[0].LSLXDH);

                                $("#txt_lssj").val(data[0].LSSJ);
                                $("#txt_delxr").val(data[0].DELXR);
                                $("#txt_delxrdh").val(data[0].DELXRDH);
                                $("#txt_lszgyxsj").val(data[0].LSZGYXSJ.toString().substring(0, 10));
                                //var zg = data.SFDXZG = "N" ? 0 : 1;`  
                                //select.setValue(zg);
                                if (data[0].SFDXZG == "N") {
                                    $("#txt_sfdxzg").val(0);
                                }
                                else {
                                    $("#txt_sfdxzg").val(1);
                                }
                                $("#txt_lsxxbz").val(data[0].LSXXBZ);

                                $("#txt_lszh").ligerGetTextBoxManager().setDisabled();

                                $.ligerDialog.open({ title: '编辑', target: $('#add_div'), width: 600,
                                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                                        submitForm();
                                    }, cls: 'l-dialog-btn-highlight'
                                    }, { text: '取消', onclick: function (item, dialog) {
                                        $("#add_form")[0].reset();
                                        dialog.hidden();
                                    }
                                    }], isResize: true
                                });
                                // console.log(JSON.stringify(data));
                            }
                        }
                    });
                }
                else
                    $.ligerDialog.warn('请选择一条需要编辑的律师信息');
            }


            //添加文件
            $(document).ready(function () {
                $("#btn_addfile").click(function () {
                    $(".diyCancelAll").click();

                    $.ligerDialog.open({ title: '上传附件', target: $('#demo'), width: 700, height: 500,
                        buttons: [{ text: '确定', onclick: function (item, dialog) {
                            //$(".diyStart").click();
                            //$(".parentFileBox").remove();
                            //fileUp.removeFile();
                            dialog.hide();

                            $("#numberfile").text(" 新增文件数" + arr.length);
                            $("#arrfile").val(arr);

                        }
                        }], isResize: true // cls: 'l-dialog-btn-highlight'
                        //                        }, { text: '取消', onclick: function (item, dialog) {
                        //                            $(".diyCancelAll").click();
                        //                            // $(".parentFileBox").remove();
                        //                            // fileUp.removeFile();
                        //                            dialog.hide();
                        //                            // $.ligerDialog.hide();

                        //                        }
                        //                        }], isResize: true
                    });

                })
            });


            //查看
            //            $("#btn_login").click(function () {
            //                var jdata = $('#login_form').serializeArray();
            //                jdata[jdata.length] = { name: "t", value: "ReadLogin" };
            //                $.ajax({
            //                    type: "POST",
            //                    url: '/Pages/LSYJ/LSGL.aspx',
            //                    data: jdata,
            //                    dataType: 'json',
            //                    timeout: 5000,
            //                    cache: false,
            //                    beforeSend: function () { },
            //                    error: function (xhr) {

            //                        $.ligerDialog.error('网络连接错误');
            //                        return false;
            //                    },
            //                    success: function (data) {
            //                        if (data.t == "error") {
            //                            $.ligerDialog.error(data.v);
            //                        } else {
            //                            $("#login_div").hide();
            //                            $("#top_div").show();
            //                            $("#layout").show();
            //                            data = data[0];
            //                            $("#span_ajname").html("当前卷宗：" + data.AJMC + "&nbsp;【" + data.BMSAH + "】");
            //                            //YJUser =data
            //                            JZMLTree(data.BMSAH, data.YJXH);
            //                        }
            //                    }
            //                });
            //            });
            //返回
            $("#btn_back").click(function () {
                $("#login_div").show();
                $("#top_div").hide();
                $("#layout").hide();
                clearInterval(time);
            });
        });

        //绑定文件目录
        function JZMLTree(bmsah, yjxh) {

            if (bmsah) {
                //加载树
                path_tree = $("#path_tree").ligerTree({
                    url: "/Pages/LSYJ/LSGL.aspx",
                    parms: { t: 'GetMlTree', bmsah: bmsah, yjxh: yjxh },
                    isExpand: 2,
                    checkbox: true,
                    treeLine: true,
                    slide: false,
                    //iconFieldName:"MLLX", 
                    nodeWidth: 106
                    , onSuccess: function (data) {
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
                $.post("/Pages/LSYJ/LSGL.aspx", { t: "GetMlTree", level: 3, pid: node.data.id, ischecked: ischecked, yjxh: path_tree.options.parms.yjxh, bmsah: path_tree.options.parms.bmsah }, function (newData) {
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
                }, 'json');
            }
        }

        //查看附件
        function ImagesShow() {
            $("#fileshow").html("");
            var lszh = $("#key_hidd").val();
            if (!lszh) {
                $.ligerDialog.error('未找到该律师附件');
                return false;
            }
            $.ajax({
                type: "POST",
                url: "/Pages/LSYJ/LSGL.aspx",
                data: { t: "GetLsFile", lszh: lszh },
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

                    var imgHtml = "";
                    for (var i = 0; i < data.length; i++) {

                        imgHtml += "<img src='" + data[i] + "' style='max-width:80%;' />";
                        if (i == data.length - 1) {
                            $("#fileshow").append(imgHtml);
                            $.ligerDialog.open({ title: '资质文件查看', target: $('#fileshow'), width: 900, height: 600, isResize: true
                            });
                        }
                    }

                }
            })
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
                        lsxm: $("#txt_lsxmcx").val(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });
    </script>
</body>
</html>
