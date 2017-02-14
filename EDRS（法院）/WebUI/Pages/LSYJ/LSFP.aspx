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
         div#tb {
            margin-bottom: 10px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 10px;
            background: white;
            line-height: 30px;
        }
    </style>
</head>
<body style="padding: 15px 15px 0px 15px; overflow: hidden;">
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
    <%--阅卷人登记--%>
    <div id="div_dj" style="padding: 10px; display: none;">
        <div style="padding: 10px 20px 20px 80px">
            <form id="form_dj" method="post">
            <input type="hidden" id="key_hidd_dj" name="key_hidd_dj" value="" />
            <table>
                <tr>
                    <td>
                        阅卷人：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjr" type="text" name="txt_yjr" style="width: 350px;"
                            maxlength="30" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        证件类型：
                    </td>
                    <td>
                        <input class="l-text" id="txt_zjlx" type="text" name="txt_zjlx" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        证件号：
                    </td>
                    <td>
                        <input class="l-text" id="txt_zjh" type="text" name="txt_zjh" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        阅卷人身份：
                    </td>
                    <td>
                        <input class="l-text" id="txt_yjrsf" type="text" name="txt_yjrsf" style="width: 350px;"
                            maxlength="50" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        查询原因：
                    </td>
                    <td>
                        <input class="l-text" id="txt_cxyy" type="text" name="txt_cxyy" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核部门：
                    </td>
                    <td colspan="3">
                        <input class="l-text" id="txt_bm" type="text" name="txt_bm" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核人：
                    </td>
                    <td colspan="3">
                        <input class="l-text" id="txt_shr" type="text" name="txt_shr" style="width: 350px;"
                            maxlength="350" />
                    </td>
                    <td>
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
                icon: '../../images/NewAdd/cx.png'
            });

            $('#btn_addls').ligerButton({
                text: '添加律师',
                icon: '../../images/NewAdd/add.png'
            });
            $('#btn_addfile').ligerButton({
                text: '上传文件',
                icon: '/LigerUI/lib/LigerUI/skins/icons/add.gif'
            });
            $("#txt_lszgyxsj").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });
            $("#txt_lszgyxsj1").ligerDateEditor({ labelWidth: 80, labelAlign: 'center' });

            //证件类型
            $("#txt_zjlx").ligerComboBox({
                keySupport: true, //按键支持
                width: 350,
                selectBoxWidth: 350,
                selectBoxHeight: 300,
                data: [{ id: 1, text: "身份证" }, { id: 2, text: "工作证" }, { id: 3, text: "警官证"}]
            });

            //阅卷人身份
            $("#txt_yjrsf").ligerComboBox({
                keySupport: true, //按键支持
                width: 350,
                selectBoxWidth: 350,
                selectBoxHeight: 300,
                data: [{ id: 1, text: "当事人" }, { id: 2, text: "检查干警" }, { id: 3, text: "公安干警" }, { id: 4, text: "纪检人员"}]
            });
            //查询原因
            $("#txt_cxyy").ligerComboBox({
                keySupport: true, //按键支持
                width: 350,
                selectBoxWidth: 350,
                selectBoxHeight: 300,
                data: [{ id: 1, text: "工作查考" }, { id: 2, text: "学术研究" }, { id: 3, text: "落实改策" }, { id: 4, text: "个人取证" }, { id: 5, text: "其他"}]
            });

            //审核人
            $("#txt_shr").ligerComboBox({
                keySupport: true, //按键支持
                width: 350,
                selectBoxWidth: 350,
                selectBoxHeight: 300,
                //                url: "/Pages/LSYJ/LSFP.aspx",
                //                parms: { t: "bindlsxm" },
                // valueFieldID: 'LSZH',
                textField: 'MC',
                valueField: 'GH',
                autocomplete: true,
                highLight: true,
                onSuccess: function (data) {

                },
                onSelected: function (value, text) {

                }
            });

            //审核部门
            $("#txt_bm").ligerComboBox({
                keySupport: true, //按键支持
                width: 350,
                selectBoxWidth: 350,
                selectBoxHeight: 300,
                url: "/Pages/LSYJ/LSFP.aspx",
                parms: { t: "GetSHBM" },
                // valueFieldID: 'LSZH',
                textField: 'BMMC',
                valueField: 'BMBM',
                autocomplete: true,
                highLight: true,
                tree: {
                    idFieldName: "BMBM",
                    textFieldName: "BMMC",
                    parentIDFieldName: "FBMBM",
                    checkbox: false,
                    nodeWidth: " ",
                    isExpand: 2,
                    //                    onCancelselect: function (row, target) {
                    //                        if (defaults.checkbox) {
                    //                            this.options.autoCheckboxEven = false;
                    //                            $("#" + row.data.id).children("div:first-child").children(".l-checkbox").click();
                    //                        }
                    //                    },
                    onSelect: function (row, target) {

                        //                        if (defaults.checkbox) {
                        //                            this.options.autoCheckboxEven = false;
                        //                            $("#" + row.data.id).children("div:first-child").children(".l-checkbox").click();
                        //                        }
                    }, onSuccess: function (data) {

                    }
                },
                onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.warn(data.v);
                    }
                    else {
                        this.treeManager.setData(data);
                        //设置下拉选择最大高度
                        $(this.selectBoxInner).css('max-height', this.options.selectBoxHeight);

                    }
                },
                onSelected: function (value, text) {
                    $.ajax({
                        type: "POST",
                        url: "/Pages/LSYJ/LSFP.aspx",
                        data: { t: "GetUserBybm", bmbm: value },
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
                            liger.get("txt_shr").setData(data);
                        }
                    });
                    //                    if (defaults.checkbox) {
                    //                        this.treeManager.options.autoCheckboxEven = true;
                    //                    }
                }, onBeforeOpen: function () {
                    $(".l-selected").removeClass("l-selected");
                }
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
//                { display: '阅卷密码', name: 'YJMM', width: 150 },
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
                url: '/Pages/LSYJ/LSFP.aspx',
                alternatingRow: false, allowUnSelectRow: true, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val()
                }, toolbar: { items: [
                    { text: '阅卷登记', click: addDown, img: '../../images/NewAdd/add.png' },
                    // { line: true },
                    //                                    { text: '审核', click: examineData, img: '/images/icons/edit.png' },
                    //                                    {line: true },
                    //                                    { text: '修改', click: editData, icon: 'modify' },

                                  // {line: true },
                                   { text: '写卡', click: XK, img: '../../images/NewAdd/xg.png' },
                                 //  {line: true },
                    //{text: '分配', click: distribution, img: '/images/icons/edit.png' },
                    //{ line: true },
                {text: '删除', click: deleteData, img: '../../images/NewAdd/sc.png' },// { line: true }
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


        //律师申请
        function addDown() {
            $('#key_hidd').val('');
            $("#form_dj")[0].reset();

            $.ligerDialog.open({ title: '阅卷登记', target: $('#div_dj'), width: 600,
                buttons: [{ text: '确定', onclick: function (item, dialog) {
                    var zjlx = $("#txt_zjlx").ligerGetComboBoxManager().getValue();
                    // var zjh = $("#txt_zjh").ligerGetComboBoxManager().getValue();
                    var yjrsf = $("#txt_yjrsf").ligerGetComboBoxManager().getValue();
                    var cxyy = $("#txt_cxyy").ligerGetComboBoxManager().getValue();
                    var shbm = $("#txt_bm").ligerGetComboBoxManager().getValue();
                    var shr = $("#txt_shr").ligerGetComboBoxManager().getValue();

                    if ($("#txt_yjr").val() == null || $("#txt_yjr").val() == "" || $.trim($("#txt_yjr").val()) == "") {
                        $.ligerDialog.warn("请填写阅卷人");
                        return false;
                    }
                    if ($("#txt_zjlx").val() == null || $("#txt_zjlx").val() == "" || $.trim($("#txt_zjlx").val()) == "") {
                        $.ligerDialog.warn("请选择证件类型");
                        return false;
                    }
                    if ($("#txt_zjh").val() == null || $("#txt_zjh").val() == "" || $.trim($("#txt_zjh").val()) == "") {
                        $.ligerDialog.warn("请填写证件号");
                        return false;
                    }
                    if ($("#txt_yjrsf").val() == null || $("#txt_yjrsf").val() == "" || $.trim($("#txt_yjrsf").val()) == "") {
                        $.ligerDialog.warn("请选择阅卷人身份");
                        return false;
                    }
                    if ($("#txt_cxyy").val() == null || $("#txt_cxyy").val() == "" || $.trim($("#txt_cxyy").val()) == "") {
                        $.ligerDialog.warn("请选择查询原因");
                        return false;
                    }
                    if ($("#txt_bm").val() == null || $("#txt_bm").val() == "" || $.trim($("#txt_bm").val()) == "") {
                        $.ligerDialog.warn("请选择审核部门");
                        return false;
                    }
                    if ($("#txt_shr").val() == null || $("#txt_shr").val() == "" || $.trim($("#txt_shr").val()) == "") {
                        $.ligerDialog.warn("请选择审核人");
                        return false;
                    }

                    localStorage['lssq'] = $("#txt_yjr").val() + "|&|" + zjlx + "|&|" + $("#txt_zjh").val() + "|&|" + yjrsf + "|&|" + cxyy + "|&|" + shr + "|&|" + shbm + "|&|" + $("#txt_shr").val();

                    location.href = "LSFPAJSJ.aspx";
                }, cls: 'l-dialog-btn-highlight'
                },
                    { text: '取消', onclick: function (item, dialog) {
                        $("#form_dj")[0].reset();
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
                        // return $("#form_dj").form('enableValidation').form('validate');
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
                            $.ligerDialog.open({ title: '修改申请', target: $('#div_dj'), width: 560,
                                buttons: [{ text: '确定', onclick: function (item, dialog) {
                                    submitForm();
                                }, cls: 'l-dialog-btn-highlight'
                                }, { text: '取消', onclick: function (item, dialog) {
                                    $("#form_dj")[0].reset();
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

        //写卡
        function XK() {
            var row = grid.getSelectedRow();
            if (row && row.YJXH) {

                if (row.SQDZT == "Y") {
                    $.ligerDialog.confirm('确定写卡?', function (r) {
                        if (r) {

                            //  location.href = "CopEFileCard://" + row.YJXH;
                            var manager = $.ligerDialog.waitting('正在写卡,请稍候...');
                            $.ajax({
                                type: "POST",
                                url: '/Pages/LSYJ/LSFP.aspx',
                                data: { t: "CopCard", yjxh: row.YJXH },
                                dataType: 'json',
                                timeout: 5000,
                                cache: false,
                                beforeSend: function () { },
                                error: function (xhr) {
                                    manager.close();
                                    $.ligerDialog.error('网络连接错误');
                                    return false;
                                },
                                success: function (data) {
                                    setTimeout(function () {
                                        manager.close();
                                        grid.reload();
                                    }, 3000);
                                    if (data.t == "error") {
                                        $.ligerDialog.error(data.v);
                                    } else
                                        location.href = "CopEFileCard://" + data.parm;
                                }
                            });
                        }
                    });
                } else {
                    $.ligerDialog.warn('只有被审核以后才能写卡，已写卡或已阅读不能再写卡。');
                }
            }else
                $.ligerDialog.warn('请先选择一条写卡申请');
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
