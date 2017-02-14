<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSFP.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSFP" %>

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
        /*右边框背景颜色*/
        body
        {
            background: #eef2f5;
            }
         .l-panel-bwarp {
            background: white;
        }
        
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #ccc;
            display: inline-block;
            width: 100%;
             background: white;
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
        
        #add_form table tr td
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
        
        /l-grid1表格/
        .l-grid1 .l-grid-body-table tr {
            height: 44px;
        }
        
         div#tb {
            padding: 10px;
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background: white;
        }
    </style>
</head>
<body style="padding:15px; overflow: hidden;">
    <div id="tb" >
        <div style="padding: 4px 5px;">
            案件编号：
            <input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />&nbsp;&nbsp;
            案件名称：
            <input id="txt_ajmc" style="width: 150px;" class="l-text" type="text" name="txt_ajmc" />&nbsp;&nbsp;
            律师工号：<input id="txt_gh" style="width: 100px;" class="l-text" type="text" name="txt_gh" />&nbsp;&nbsp;
            律师姓名：<input id="txt_mc" style="width: 100px;" class="l-text" type="text" name="txt_mc" />&nbsp;&nbsp;
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: none;">
        <div style="padding: 0px 20px 20px 20px">
            <form id="add_form" method="post">
            <table>
                <tr>
                    <td>
                        律师姓名：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd" name="key_hidd" value="" />
                        <input class="l-text" id="txt_lszh" type="text" name="txt_lszh" maxlength="350" style="width: 150px;" />
                    </td>
                    <td>
                        <div id="btn_addls" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        律师单位：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lsdw" type="text" name="txt_lsdw"
                            style="width: 150px;" maxlength="350" />
                    </td>
                    <td>
                        &nbsp;&nbsp; &nbsp; 律师单位地址：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lsdwdz" type="text" name="txt_lsdwdz"
                            style="width: 150px;" maxlength="350" />
                    </td>
                </tr>
                <tr>
                    <td>
                        律师单位邮政号：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lsdwyzh" type="text" name="txt_lsdwyzh"
                            style="width: 150px;" maxlength="350" />
                    </td>
                    <td>
                        &nbsp;&nbsp; &nbsp; 律师联系电话：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lslxdh" type="text" name="txt_lslxdh"
                            style="width: 150px;" maxlength="350" />
                    </td>
                </tr>
                <tr>
                    <td>
                        律师手机：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lssj" type="text" name="txt_lssj"
                            style="width: 150px;" maxlength="350" />
                    </td>
                    <td>
                        &nbsp;&nbsp; &nbsp; 第二联系人：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_delxr" type="text" name="txt_delxr"
                            style="width: 150px;" maxlength="350" />
                    </td>
                </tr>
                <tr>
                    <td>
                        第二联系人电话：
                    </td>
                    <td>
                        <input class="l-text ls-input" id="txt_delxrdh" readonly="readonly" type="text" name="txt_delxrdh"
                            style="width: 150px;" maxlength="350" />
                    </td>
                    <td>
                        &nbsp;&nbsp; &nbsp; 律师资格有效期：
                    </td>
                    <td>
                        <input class="l-text ls-input" readonly="readonly" id="txt_lszgyxsj" type="text"
                            name="txt_lszgyxsj" style="width: 150px;" maxlength="350" />
                    </td>
                </tr>
                <tr>
                    <td>
                        是否吊销资格：
                    </td>
                    <td>
                        <select id="txt_sfdxzg" disabled="disabled" class="l-text ls-input" name="txt_sfdxzg"
                            style="width: 150px">
                            <option value="0">否</option>
                            <option value="1">是</option>
                        </select>
                    </td>
                    <td>
                        &nbsp;&nbsp; &nbsp;律师资质文件：
                    </td>
                    <td>
                        <%--<input class="l-text" id="txt_yjsqdm" type="text" name="txt_yjsqdm" style="width: 150px"
                            maxlength="350" />--%>
                        <a href="#" onclick="ImagesShow()">查看<label id="filenumber">0</label>个附件</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请案件名称：
                    </td>
                    <td colspan="3">
                        <input class="l-text" id="txt_yjsqdm" type="text" name="txt_yjsqdm" style="width: 480px"
                            maxlength="500" />
                    </td>
                </tr>
                <tr>
                    <td>
                        阅卷说明：
                    </td>
                    <td colspan="3">
                        <textarea id="txt_sqsm" name="txt_sqsm" cols="75" rows="5" class="l-textarea"></textarea>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <%--添加律师--%>
    <div id="addls_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 20px">
            <form id="addls_form" method="post">
            <table>
                <tr>
                    <td>
                        律师证号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lszh1" type="text" name="txt_lszh1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;律师姓名：
                    </td>
                    <td>
                        <input type="hidden" id="key_hidd1" name="key_hidd1" value="" />
                        <input class="l-text" id="txt_lsxm1" type="text" name="txt_lsxm1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                    
                </tr>

                <tr>
                    
                    <td>
                        律师单位：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdw1" type="text" name="txt_lsdw1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                     <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;律师单位地址：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdwdz1" type="text" name="txt_lsdwdz1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                   
                    <td>
                       律师单位邮政号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lsdwyzh1" type="text" name="txt_lsdwyzh1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                     <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;律师联系电话：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lslxdh1" type="text" name="txt_lslxdh1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                   
                    <td>
                        律师手机：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lssj1" type="text" name="txt_lssj1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                    <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;第二联系人：
                    </td>
                    <td>
                        <input class="l-text" id="txt_delxr1" type="text" name="txt_delxr1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                    
                    <td>
                       第二联系人电话：
                    </td>
                    <td>
                        <input class="l-text" id="txt_delxrdh1" type="text" name="txt_delxrdh1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                    <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;律师资格有效期：
                    </td>
                    <td>
                        <input class="l-text" id="txt_lszgyxsj1" type="text" name="txt_lszgyxsj1" style="width: 150px;"
                            maxlength="350" />
                    </td>
                </tr>
                <tr>
                    
                    <td>
                       是否吊销资格：
                    </td>
                    <td colspan="3">
                        <select id="txt_sfdxzg1" class="l-text" name="txt_sfdxzg1" style="width: 150px">
                            <option value="0">否</option>
                            <option value="1">是</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        律师资质文件：
                    </td>
                    <td>
                        <div id="btn_addfile" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
                        </div>
                        <input type="hidden" id="arrfile1" name="key_arrlife1" value="" />
                        <a href="javascript:">
                            <label id="numberfile">
                            </label>
                        </a>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        律师信息备注：
                    </td>
                    <td colspan="3">
                        <%--<input class="l-text" id="txt_lsxxbz" type="text" name="txt_lsxxbz" style="width: 420px;"
                            maxlength="350" />--%>
                        <textarea class="l-text" id="txt_lsxxbz1" type="text" name="txt_lsxxbz1" style="width: 490px;
                            height: 120px"></textarea>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <%--审核窗口--%>
    <div id="examine_div" style="padding: 10px; display: none;">
        <div style="padding: 10px 60px 20px 60px">
            <form id="examine_form" method="post">
            <table class="l-table">
                <tr>
                    <td style="width: 70px;">
                        <%--律师证号：--%>
                    </td>
                    <td>
                        <input type="hidden" id="key_ex_hidd" name="key_ex_hidd" value="" />
                        <%--<span class="l-span" id="span_lszh"></span>--%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请单号：
                    </td>
                    <td>
                        <span class="l-span" id="span_yjsqdh"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请单名：
                    </td>
                    <td>
                        <span class="l-span" id="span_yjsqdm"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请时间：
                    </td>
                    <td>
                        <span class="l-span" id="span_sqsj"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请说明：
                    </td>
                    <td>
                        <span class="l-span" id="span_sqsm"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核说明：
                    </td>
                    <td>
                        <textarea id="txt_shsm" name="txt_shsm" cols="100" rows="2" class="l-textarea"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="radio" id="rad_sqdzt_yes" name="rad_sqdzt" value="Y" /><label for="rad_sqdzt_yes">是通过</label>&nbsp;&nbsp;&nbsp;
                        <input type="radio" id="rad_sqdzt_no" name="rad_sqdzt" value="N" /><label for="rad_sqdzt_no">不通过</label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div id="demo" style="display: none;">
        <div id="as">
        </div>
    </div>
    <div id="fileshow" style="display: none">
    </div>
    <script type="text/javascript">
        var grid = null;
        var ls_user = null;
        var fileUp;
        var arr = [];
        var arrfile = [];
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
                accept: { title: 'Images',
                    extensions: 'gif,jpg,jpeg,bmp,png',
                    mimeTypes: 'image/*'
                }
            });

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });

            $('#btn_addls').ligerButton({
                text: '添加律师',
                icon: '../../images/add.png',
                width:80
            });
            $('#btn_addfile').ligerButton({
                text: '上传文件',
                icon: '/LigerUI/lib/LigerUI/skins/icons/add.gif'
            });
            $("#txt_lszgyxsj").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_lszgyxsj1").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '律师证号', name: 'GH', minWidth: 150 },
                { display: '律师姓名', name: 'MC', minWidth: 80 },
                { display: '阅卷密码', name: 'YJMM', width: 150 },
                { display: '申请案件名称', name: 'YJSQDM', minWidth: 150 },
                { display: '案件编号', name: 'AJBH', minWidth: 80 },
                { display: '案件名称', name: 'AJMC', minWidth: 150 },
                { display: '部门受案号', name: 'BMSAH', width: 1, hide: true },
                { display: '阅卷状态', name: 'YJKSSJ'
                    , render: function (item) {
                        var nowTime = '<%=nowTime %>';
                        var kssj = item.YJKSSJ;
                        var jssj = item.YJJSSJ;
                        nowTime = Date.parse(nowTime.replace(/-/g, '/'));
                        if(kssj)
                            kssj = Date.parse(kssj.replace(/-/g, '/'));
                        if(jssj)
                        jssj = Date.parse(jssj.replace(/-/g, '/'));
                        if (nowTime < kssj)
                            return "<font style='color:red;'>未到时</font>";
                        else if (nowTime > kssj && nowTime < jssj)
                            return "<font style='color:green;'>正常 </font>";
                        else if (nowTime > jssj)
                            return "<font style='color:gray;'>过期</font>";
                        else
                            return "";
                    }
                },
                { display: '开始时间', name: 'YJKSSJ', width: 150 },
                { display: '结束时间', name: 'YJJSSJ', width: 150 },
                { display: '分配人', name: 'JDR', minWidth: 80 },
                { display: '分配时间', name: 'JDSJ', width: 150 },
                
                 { display: '阅卷说明', name: 'SQSM', width: 150 },
                { display: '阅卷序号', name: 'YJXH', width: 1, hide: 'true' }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/LSYJ/LSFP.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                    { text: '申请', click: addDown, img: '../../images/sq.png' },
                    // { line: true },
                    //                                    { text: '审核', click: examineData, img: '/images/icons/edit.png' },
                    //                                    {line: true },
                    //                                    { text: '修改', click: editData, icon: 'modify' },

                           //        {line: true },

                    //{text: '分配', click: distribution, img: '/images/icons/edit.png' },
                    //{ line: true },
                {text: '删除', click: deleteData, img: '../../images/sc.png' },
                    //{ line: true }, 
                    // { text: '律师管理', click: deleteData, icon: 'add' }
                ]
                }, onSuccess: function (data) {
                    if (data.t) {
                        // $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();
        });

        //查看附件
        function ImagesShow() {
            $("#fileshow").html("");
            var lszh = $("#key_hidd").val();
            if (!lszh) {
                $.ligerDialog.error('未找到附件');
                return false;
            }
            $.ajax({
                type: "POST",
                url: "/Pages/LSYJ/LSFP.aspx",
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



        //律师名称
        ls_user = $("#txt_lszh").ligerComboBox({
            url: "/Pages/LSYJ/LSFP.aspx",
            parms: { t: "bindlsxm" },
            valueFieldID: 'LSZH',
            textField: 'LSXM',
            valueField: 'LSZH',
            width: 150,
            selectBoxWidth: 150,
            selectBoxHeight: 300,
            autocomplete: true,
            highLight: true,
            onSuccess: function (data) {
                $(".ls-input").val("");
            },
            onSelected: function (value, text) {
                if (!value) {
                    return false;
                }
                $.ajax({
                    type: "POST",
                    url: "/Pages/LSYJ/LSFP.aspx",
                    data: { t: "GetLS", lszh: value },
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
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            $("#filenumber").text(data[0].WJNUM);
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
                            $("#txt_lszgyxsj").ligerDateEditor().setDisabled();
                            $("#txt_lszgyxsj").nextAll(".l-trigger-cancel").remove();
                            if (data[0].SFDXZG == "N") {
                                $("#txt_sfdxzg").val(0);
                            }
                            else {
                                $("#txt_sfdxzg").val(1);
                            }
                        }
                    }
                });
            }

        });


        //添加文件
        $(document).ready(function () {
            $("#btn_addfile").click(function () {

                $.ligerDialog.open({ title: '上传附件', target: $('#demo'), width: 700, height: 500,
                    buttons: [{ text: '关闭', onclick: function (item, dialog) {
                        dialog.hide();
                        $("#numberfile").text(" 新增文件数" + arr.length);
                        $("#arrfile1").val(arr);
                    }

                    }], isResize: true //cls: 'l-dialog-btn-highlight'
                    //                    }, { text: '取消', onclick: function (item, dialog) {
                    //                        $(".diyCancelAll").click();
                    //                        // $(".parentFileBox").remove();
                    //                        // fileUp.removeFile();
                    //                        dialog.hide();
                    //                        // $.ligerDialog.hide();

                    //                    }
                    //                    }], isResize: true
                });

            })
        });


        //提交保存数据
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
                        $("#add_form")[0].reset();
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


        //律师申请
        function addDown() {
            $('#key_hidd').val('');
            $("#add_form")[0].reset();

            $.ligerDialog.open({ title: '申请阅卷', target: $('#add_div'), width: 700,
                buttons: [{ text: '确定', onclick: function (item, dialog) {

                    if (ls_user.getValue() == null || ls_user.getValue() == "") {
                        $.ligerDialog.warn("请先选择律师姓名");
                        return false;
                    }

                    if ($("#txt_yjsqdm").val() == null || $("#txt_yjsqdm").val() == "") {
                        $.ligerDialog.warn("申请案件名称不能为空");
                        return false;
                    }

                    localStorage['lssq'] = ls_user.getValue() + "|&|" + $("#txt_yjsqdm").val() + "|&|" + $("#txt_sqsm").val() + "|&|" + ls_user.getText();

                    location.href = "LSFPAJSJ.aspx";
                }, cls: 'l-dialog-btn-highlight'
                },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#add_form")[0].reset();
                        dialog.hidden();
                    }
                    }], isResize: true
            });
        }

        //添加律师
        $(document).ready(function () {
            $("#btn_addls").click(function () {
                //清空上传
                $.diyUploadClear(fileUp);
                arr = [];
                $("#numberfile").text("");
                var d = $.ligerDialog.open({ title: '添加律师', target: $('#addls_div'), width: 700,
                    buttons: [{ text: '确定', onclick: function (item, dialog) {
                        $.ajax({
                            type: "POST",
                            url: "/Pages/LSYJ/LSFP.aspx",
                            data: { t: "AddLS", lszh: $("#txt_lszh1").val(), lsxm: $("#txt_lsxm1").val(), lsdw: $("#txt_lsdw1").val(), lsdwdz: $("#txt_lsdwdz1").val(), lsdwyzh: $("#txt_lsdwyzh1").val(), lslxdh: $("#txt_lslxdh1").val(), lssj: $("#txt_lssj1").val(), delxr: $("#txt_delxr1").val(), delxrdh: $("#txt_delxrdh1").val(), lszgyxsj: $("#txt_lszgyxsj1").val(), sfdxzg: $("#txt_sfdxzg1").val(), lsxxbz: $("#txt_lsxxbz1").val(), 'filestr': arr },
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
                                    //清空上传
                                    $.diyUploadClear(fileUp);
                                    arr = [];

                                    $("#addls_form")[0].reset();
                                    d.hide();
                                    ls_user.reload();
                                    $.ligerDialog.success(data.v);

                                } else {
                                    $.ligerDialog.error(data.v);
                                }
                            }
                        });

                    }, cls: 'l-dialog-btn-highlight'
                    },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#addls_form")[0].reset();
                        dialog.hidden();
                        ls_user.reload();
                    }
                    }], isResize: true
                });

            })
        })

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

        //修改
        function editData() {
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
                            $("#key_hidd").val(data.YJSQDH);
                            //$("#txt_lszh").val(data.LSZH);                           
                            $("#txt_yjsqdm").val(data.YJSQDM);
                            $("#txt_sqsm").val(data.SQSM);
                            $.ligerDialog.open({ title: '修改申请', target: $('#add_div'), width: 560,
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
                $.ligerDialog.warn('请选择一个申请');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.YJXH };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/LSYJ/LSFP.aspx',
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
                $.ligerDialog.warn('请先选择一个被删除申请');
        }
        //分配
        function distribution() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                location.href = "LSFPAJSJ.aspx?yjxh=" + (cksld.YJXH == null ? "" : cksld.YJXH) + "&bmsah=" + (cksld.BMSAH == null ? "" : encodeURI(encodeURI(cksld.BMSAH))) + "&ajmc=" + (cksld.AJMC == null ? "" : encodeURI(encodeURI(cksld.AJMC)));
            }
            else
                location.href = "LSFPAJSJ.aspx";
            //            } else
            //                $.ligerDialog.warn('请先选择申请再进行分配');
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

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("ajmc", $("#txt_ajmc").val());
            grid.setParm("gh", $("#txt_gh").val());
            grid.setParm("mc", $("#txt_mc").val());
        }

    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
