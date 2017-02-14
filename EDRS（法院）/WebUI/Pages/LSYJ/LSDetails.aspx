<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSDetails.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <!--文件上传 -->
    <link href="/webuploader/webuploader.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/webuploader/demo/style.css" />
    <script src="/webuploader/webuploader.js" type="text/javascript"></script>
    <script type="text/javascript" src="/webuploader/demo/upload.js"></script>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <style type="text/css">
        .l-text
        {
            height: auto;
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
        .picBox
        {
            margin: 5px;
        }
    </style>
</head>
<body>
    <%--添加数据窗口--%>
    <div id="add_div" style="display: block; padding-left: 50px;">
        <form id="add_form" method="post" runat="server">
        <input type="text" id="action" name="action" style="display: none;" />
        <table style="line-height: 30px; width: 750px;">
            <tr>
                <td style="width: 180px;">
                    律师证号：
                </td>
                <td>
                    <input id="LSZH" class="l-text" type="text" name="LSZH" style="width: 130px" validate="{required:true}"
                        runat="server" />
                </td>
                <td>
                    律师姓名：
                </td>
                <td>
                    <input id="LSXM" class="l-text" type="text" name="LSXM" style="width: 130px" validate="{required:true}" />
                </td>
            </tr>
            <tr>
                <td>
                    律师资格有效时间：
                </td>
                <td>
                    <input id="LSZGYXSJ" class="l-text" type="text" name="LSZGYXSJ" style="width: 120px" />
                </td>
                <td colspan="2">
                    是否吊销资格(Y/N)：<input id="SFDXZG" type="checkbox" class="l-checkbox" name="SFDXZG" value=""
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    律师所属单位：
                </td>
                <td>
                    <input type="text" id="LSDW" name="LSDW" style="display: none" runat="server" />
                    <input id="LSDWMC" class="l-text" type="text" name="LSDWMC" style="width: 130px"
                        validate="{required:true}" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    律师单位地址：
                </td>
                <td>
                    <input id="LSDWDZ" class="l-text" type="text" name="LSDWDZ" style="width: 130px"
                        validate="{required:true}" />
                </td>
                <td>
                    律师单位邮政号码：
                </td>
                <td colspan="3">
                    <input id="LSDWYZHM" class="l-text" type="text" name="LSDWYZHM" style="width: 130px" />
                </td>
            </tr>
            <tr>
                <td>
                    律师联系电话：
                </td>
                <td>
                    <input id="LSLXDH" class="l-text" type="text" name="LSLXDH" style="width: 130px" />
                </td>
                <td>
                    律师手机：
                </td>
                <td>
                    <input id="LSSJ" class="l-text" type="text" name="LSSJ" style="width: 130px" />
                </td>
            </tr>
            <tr>
                <td>
                    第二联系人：
                </td>
                <td>
                    <input id="DELXR" class="l-text" type="text" name="DELXR" style="width: 130px" />
                </td>
                <td>
                    第二联系人电话：
                </td>
                <td>
                    <input id="DELXRDH" class="l-text" type="text" name="DELXRDH" style="width: 130px" />
                </td>
            </tr>
            <tr>
                <td>
                    律师信息备注：
                </td>
                <td colspan="3" style="margin: 5px; width: auto;">
                    <textarea id="LSXXBZ" name="LSXXBZ" class="l-textarea" style="width: 100%; height: 50px"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    律师资质文件：
                </td>
                <td colspan="3">
                    <div id="wrapper">
                        <div id="container">
                            <!--头部，相册选择和格式选择-->
                            <div id="uploader">
                                <div class="queueList">
                                    <div id="dndArea" class="placeholder">
                                        <div id="filePicker">
                                        </div>
                                        <p style="font-size: 18px;">
                                            或将图片拖到这里</p>
                                    </div>
                                </div>
                                <div class="statusBar" style="display: none;">
                                    <div class="progress">
                                        <span class="text">0%</span> <span class="percentage"></span>
                                    </div>
                                    <div class="info">
                                    </div>
                                    <div class="btns">
                                        <div id="filePicker2">
                                        </div>
                                        <div class="uploadBtn">
                                            开始上传</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <input id="LSZZWJ" class="l-text" type="text" name="LSZZWJ" style="width: 130px;
                        display: none;" />
                </td>
            </tr>
            <tr>
                <td>
                    创建时间：
                </td>
                <td>
                    <input id="CJSJ" class="l-text" type="text" name="CJSJ" style="width: 130px" />
                </td>
                <td>
                    最后一次阅卷时间：
                </td>
                <td>
                    <input id="ZHYCYJSJ" class="l-text" type="text" name="ZHYCYJSJ" style="width: 130px" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div class="l-dialog-buttons">
        <div class="l-dialog-buttons-inner">
            <div class="l-dialog-btn">
                <div class="l-dialog-btn-l">
                </div>
                <div class="l-dialog-btn-r">
                </div>
                <div class="l-dialog-btn-inner">
                    取消</div>
            </div>
            <div class="l-dialog-btn l-dialog-btn-highlight">
                <div class="l-dialog-btn-l">
                </div>
                <div class="l-dialog-btn-r">
                </div>
                <div class="l-dialog-btn-inner" onclick="Submit()">
                    确定</div>
            </div>
            <div class="l-clear">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var action = "<%=action %>";
        var _lszh = "<%=_LSZH %>";
        var _lsdw = "<%=_LSDW %>";
        var _lsdwmc = "<%=_LSDWMC %>";
        function Submit() {//上传图片
            var stats = uploader.getStats();
            //验证控件中文件数量与成功数量(+取消数量)是否相等总文件数量
            if ((stats.successNum + stats.cancelNum) != uploader.getFiles().length) {
                alert('律师资质文件尚未上传完成，请上传完成后再保存！');
                return;
            }
            var op = "<%=action %>";
            var jdata = $('#add_form').serializeArray();
            jdata[jdata.length] = { name: "action", value: op };
            $.ajax({
                type: "POST",
                url: "/Handler/ZZJG/DZJZ_LSYJ.ashx",
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
                    parent.CloseWindow(data);
                }
            });
        }

        function initFormStatus() {

            $("#CJSJ").ligerTextBox().setDisabled(false);
            $("#ZHYCYJSJ").ligerTextBox().setDisabled(false);

            $("#LSZGYXSJ").ligerDateEditor({ labelWidth: 90, labelAlign: 'center' });
            $("#LSDWMC").ligerTextBox().setDisabled(false);
            //$("#txt_number").ligerSpinner({ type: 'int' });
            $('input:checkbox').ligerCheckBox();
            $("#LSZH").val(_lszh);
            $("#LSDW").val(_lsdw);
            if (action == "AddData") {
                $("#LSZH").ligerTextBox().setEnabled(true);
            } else {
                $("#LSZH").ligerTextBox().setDisabled(false);
                $.ajax({
                    type: "POST",
                    url: '/Handler/ZZJG/DZJZ_LSYJ.ashx',
                    data: { action: "GetModelPList", LSZH: _lszh },
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
                        if (data) {
                            $("#LSZH").val(data.LSZH);
                            $("#LSXM").val(data.LSXM);
                            $("#LSDW").val(data.LSDW);
                            $("#LSDWMC").val(data.LSDWMC);
                            $("#LSDWDZ").val(data.LSDWDZ);
                            $("#LSDWYZHM").val(data.LSDWYZHM);
                            $("#LSLXDH").val(data.LSLXDH);
                            $("#LSSJ").val(data.LSSJ);
                            $("#DELXR").val(data.DELXR);
                            $("#DELXRDH").val(data.DELXRDH);
                            $("#LSZGYXSJ").val(data.LSZGYXSJ);
                            $("#LSXXBZ").val(data.LSXXBZ);
                            $("#LSZZWJ1").val(data.LSZZWJ1);
                            $("#LSZZWJ2").val(data.LSZZWJ2);
                            $("#LSZZWJ3").val(data.LSZZWJ3);
                            $("#LSZZWJ4").val(data.LSZZWJ4);
                            $("#LSZZWJ11").val(data.LSZZWJ1);
                            $("#LSZZWJ22").val(data.LSZZWJ2);
                            $("#LSZZWJ33").val(data.LSZZWJ3);
                            $("#LSZZWJ44").val(data.LSZZWJ4);
                            $("#CJSJ").val(data.CJSJ);
                            $("#ZHYCYJSJ").val(data.ZHYCYJSJ);
                            var SFDXZG = $("#SFDXZG").ligerCheckBox();
                            if (data.SFDXZG == "Y")
                                SFDXZG.setValue(true);
                            else
                                SFDXZG.setValue(false);
                        }
                    }
                });
            }
        }

        function validateForm() {
            var LSZH = $("#LSZH").val(); //律师证号
            if (isNull(LSZH)) {
                ShowWarn('律师证号不允许为空！');
                return false;
            }
            var LSXM = $("#LSXM").val(); //律师姓名

            if (isNull(LSXM)) {
                ShowWarn('律师姓名不允许为空！');
                return false;
            }
            var LSDW = $("#LSDW").val(); //律师单位
            if (isNull(LSDW)) {
                ShowWarn('律师单位不允许为空！');
                return false;
            }
            var LSDWDZ = $("#LSDWDZ").val(); //律师单位地址
            var LSDWYZHM = $("#LSDWYZHM").val(); //律师单位邮政编码
            var LSLXDH = $("#LSLXDH").val(); //律师联系电话
            var LSSJ = $("#LSSJ").val(); //律师手机
            var DELXR = $("#DELXR").val(); //第二联系人
            var DELXRDH = $("#DELXRDH").val(); //第二联系人电话
            var LSZGYXSJ = $("#LSZGYXSJ").val(); //律师资格有效时间
            if (isNull(LSZGYXSJ)) {
                ShowWarn('律师资格有效时间不允许为空！');
                return false;
            }
            var SFDXZG = $("#SFDXZG").val(); //是否吊销凭证
            var LSXXBZ = $("#LSXXBZ").val(); //律师信息备注
            var LSZZWJ1 = $("#LSZZWJ1").val(); //律师资质文件1
            var LSZZWJ2 = $("#LSZZWJ2").val(); //律师资质文件2
            var LSZZWJ3 = $("#LSZZWJ3").val(); //律师资质文件3
            var LSZZWJ4 = $("#LSZZWJ4").val(); //律师资质文件4
            var CJSJ = $("#CJSJ").val(); //创建时间
            var ZHYCYJSJ = $("#ZHYCYJSJ").val(); //最后修改时间
            return true;
        }
        function isNull(data) {
            return (data == "" || data == undefined || data == null) ? true : false;
        }

        $(function () {
            initFormStatus();
        });

    </script>
</body>
</html>
