<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintFrontCover.aspx.cs"
    Inherits="WebUI.Pages.Print.PrintFrontCover" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>封面打印</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <%--
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>--%>
    <script src="../../Scripts/tools/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.PrintArea.js" type="text/javascript"></script>
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
        .l-text, .l-textarea
        {
            width: 190px;
        }
        #add_form table tr td, #add_form2 table tr td
        {
            padding: 4px 0px;
        }
        .form-td-right
        {
            text-align: right;
        }
        
    </style>
</head>
<body style="padding: 15px; overflow: hidden;">
    <div id="tb" style="background-color: #f8f8f8; display: none;">
        <div style="padding: 4px 5px;">
            名称：<input id="txt_key" style="width: 200px;" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
            <div id="btn_print1" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                onclick="btn_print()">
            </div>
        </div>
    </div>
    <%--<div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>--%>
    <%--添加数据窗口--%>
    <div id="add_div" style="padding: 10px; display: block;">
        <div style="
            /*padding: 10px 50px 20px 50px; margin: 0 auto; border: 1px solid #aecaf0;
            width: 570px; */
            margin: 0 auto;
            border: 1px solid #aecaf0;
            width: 570px;
            padding: 15px 15px 10px 10px;
            border: 1px solid #ccc;
            width: 570px;
            border-radius: 10px;
          /*  box-shadow: 0px 2px 2px 1px #ccc;  */
            background: white;
            ">
            <div id="title_tool_div">
                <div style="text-align: center;">
                    <span style="font-size: 24px; font-weight: bold;">
                        <%= UserInfo.DWMC %></span>
                    <input id="txt_ywlx" type="text" name="txt_ywlx" class="l-text" style="width: 80px;" />
                    <span style="font-size: 24px; font-weight: bold;">申诉讼卷宗</span>
                </div>
                <div style="padding: 10px 0px 10px 0px;">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 80px; text-align: right;">
                                年份：
                            </td>
                            <td>
                                <input id="txt_nf" type="text" name="txt_nf" class="l-text" style="width: 80px;" />
                            </td>
                            <td style="padding-left: 15px;">
                                字：
                            </td>
                            <td>
                                <input id="txt_z" type="text" name="txt_z" class="l-text" style="width: 80px;" />
                            </td>
                            <td style="padding-left: 15px;">
                                号：
                            </td>
                            <td>
                                <input id="txt_h" type="text" name="txt_h" class="l-text" onkeyup="h_keyup_func(this)"
                                    style="width: 80px;" />
                            </td>
                            <td style="padding-left: 15px;" id="searchbar">
                                <div id="btn_inquire">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <form id="add_form" method="post">
            <table id="table_temp1" border="0" style="display: block;">
                <tr>
                    <td class="form-td-right" style="width: 80px;">
                        案号名称：
                    </td>
                    <td colspan="4">
                        <input id="txt_bt" type="text" name="txt_bt" class="l-text" style="width: 488px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        案由：
                    </td>
                    <td colspan="4">
                        <input id="txt_fbt" type="text" name="txt_fbt" class="l-text" style="width: 488px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        送案机关：
                    </td>
                    <td colspan="4">
                        <input id="txt_ajmc" type="text" name="txt_ajmc" class="l-text" style="width: 488px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        原告：
                    </td>
                    <td colspan="4">
                        <textarea id="txt_ajbh" name="txt_ajbh" class="l-text" style="height: 80px; width: 488px;max-width: 488px;"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        被告：
                    </td>
                    <td colspan="4">
                        <textarea id="txt_fzxyr" name="txt_fzxyr" class="l-text" style="height: 80px; width: 488px;max-width: 488px;"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        收案日期：
                    </td>
                    <td>
                        <input id="txt_sasj" type="text" name="txt_sasj" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        结案日期：
                    </td>
                    <td>
                        <input id="txt_jasj" type="text" name="txt_jasj" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        审理结果：
                    </td>
                    <td>
                        <input id="txt_shjg" type="text" name="txt_shjg" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        关联档案系列号：
                    </td>
                    <td>
                        <input id="txt_gldaxlh" type="text" name="txt_gldaxlh" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right" rowspan="3">
                        合议庭成员：
                    </td>
                    <td rowspan="3">
                        <textarea id="txt_hytcy" name="txt_hytcy" class="l-text" style="height: 50px;"></textarea>
                    </td>
                    <td rowspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        承办人：
                    </td>
                    <td>
                        <input id="txt_cbr" type="text" name="txt_cbr" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        书记员：
                    </td>
                    <td>
                        <input id="txt_sjy" type="text" name="txt_sjy" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        本卷共：
                    </td>
                    <td>
                        <input id="txt_bjgjc" type="text" name="txt_bjgjc" style="width: 70px;" class="l-text" />册
                        属第
                        <input id="txt_sdjc" type="text" name="txt_sdjc" style="width: 70px;" class="l-text" />册
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        册内共：
                    </td>
                    <td>
                        <input id="txt_ngjy" type="text" name="txt_ngjy" style="width: 80px;" class="l-text" />页
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        归档日期：
                    </td>
                    <td>
                        <input id="txt_gdrq" type="text" name="txt_gdrq" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        保管期限：
                    </td>
                    <td>
                        <input id="txt_bgqx" type="text" name="txt_bgqx" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        业务庭：
                    </td>
                    <td colspan="4">
                        <input id="txt_ywt" type="text" name="txt_ywt" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="3">
                        <div id="btn_print" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                            onclick="btn_print('add_form',2)">
                        </div>
                        <div id="btn_print3" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                            onclick="btn_print('add_form',4)">
                        </div>
                    </td>
                </tr>
            </table>
            </form>
            <%--执行--%>
            <form id="add_form2" method="post">
            <table id="table_temp2" style="display: block;">
                <tr>
                    <td class="form-td-right" style="width: 80px;">
                        案号名称：
                    </td>
                    <td colspan="4">
                        <input id="txt_bt2" type="text" name="txt_bt" class="l-text" style="width: 488px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        案由：
                    </td>
                    <td colspan="4">
                        <input id="txt_fbt2" type="text" name="txt_fbt" class="l-text" style="width: 488px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td rowspan="4" class="form-td-right" style="width: 20px; text-align: center;">
                        <p>
                            当</p>
                        <p>
                            事</p>
                        <p>
                            人</p>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #8EC4E9; border-bottom-color: #FFFFFF;" colspan="4">
                        <div style="float: left; height: 30px; width: 72px; line-height: 32px">
                            异议人：</div>
                        <div style="float: left;">
                            <textarea id="txt_yyr2" name="txt_yyr" class="l-text" style="height: 30px; width: 410px;"></textarea></div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #8EC4E9; border-bottom-color: #FFFFFF;" colspan="4">
                        <div style="float: left; height: 30px; width: 72px; line-height: 32px">
                            申请执行人：</div>
                        <div style="float: left;">
                            <textarea id="txt_sqzxr2" name="txt_sqzxr" class="l-text" style="height: 30px; width: 410px;"></textarea></div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #8EC4E9;" colspan="4">
                        <div style="float: left; height: 30px; width: 72px; line-height: 32px">
                            被执行人：</div>
                        <div style="float: left;">
                            <textarea id="txt_bzxr2" name="txt_bzxr" class="l-text" style="height: 30px; width: 410px;"></textarea></div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        收案日期：
                    </td>
                    <td>
                        <input id="txt_sasj2" type="text" name="txt_sasj" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        结案日期：
                    </td>
                    <td>
                        <input id="txt_jasj2" type="text" name="txt_jasj" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        裁决机关：
                    </td>
                    <td>
                        <input id="txt_cjjg2" type="text" name="txt_cjjg" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        执行标的：
                    </td>
                    <td>
                        <input id="txt_zxbd2" type="text" name="txt_zxbd" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        执行结果：
                    </td>
                    <td colspan="4">
                        <input id="txt_zxjg2" type="text" name="txt_zxjg" class="l-text" style="width: 486px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        结案方式：
                    </td>
                    <td>
                        <input id="txt_jafs2" type="text" name="txt_jafs" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        关联档案系列号：
                    </td>
                    <td>
                        <input id="txt_gldaxlh2" type="text" name="txt_gldaxlh" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right" rowspan="3">
                        合议庭成员：
                    </td>
                    <td rowspan="3">
                        <textarea id="txt_hytcy2" name="txt_hytcy" class="l-text" style="height: 50px;"></textarea>
                    </td>
                    <td rowspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        承办人：
                    </td>
                    <td>
                        <input id="txt_cbr2" type="text" name="txt_cbr" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        书记员：
                    </td>
                    <td>
                        <input id="txt_sjy2" type="text" name="txt_sjy" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        本卷共：
                    </td>
                    <td>
                        <input id="txt_bjgjc2" type="text" name="txt_bjgjc" style="width: 70px;" class="l-text" />册
                        属第
                        <input id="txt_sdjc2" type="text" name="txt_sdjc" style="width: 70px;" class="l-text" />册
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        册内共：
                    </td>
                    <td>
                        <input id="txt_ngjy2" type="text" name="txt_ngjy" style="width: 80px;" class="l-text" />页
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        归档日期：
                    </td>
                    <td>
                        <input id="txt_gdrq2" type="text" name="txt_gdrq" class="l-text" />
                    </td>
                    <td>
                    </td>
                    <td class="form-td-right">
                        保管期限：
                    </td>
                    <td>
                        <input id="txt_bgqx2" type="text" name="txt_bgqx" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="form-td-right">
                        业务庭：
                    </td>
                    <td colspan="4">
                        <input id="txt_ywt2" type="text" name="txt_ywt" class="l-text" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="3">
                        <div id="btn_print2" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                            onclick="btn_print('add_form2',2)">
                        </div>
                        <div id="btn_print4" style="margin-left: 10px; display: inline-block; vertical-align: bottom;"
                            onclick="btn_print('add_form2',4)">
                        </div>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div id="print" style="display: none; width: 100%;">
        <!--startprint-->
        <div style="display: table; width: 100%; margin-bottom: 2px;">
            <div style="float: left; width: auto; border: 3px solid #000; display: table; margin-left: 40px;">
                <div style="float: left; border-right: 3px solid #000; height: 78px; font-size: 10px;">
                    <span id="span_dwmc" style="text-align: center; display: inline-table; width: 20px;
                        line-height: 11px; letter-spacing: 1px; height: 78px;">
                        <%= UserInfo.DWMC %></span> <span style="text-align: center; display: inline-table;
                            letter-spacing: 1px; line-height: 18px; width: 20px; height: 78px;">人民法院</span>
                    <span style="text-align: center; letter-spacing: 1px; line-height: 27px; display: inline-table;
                        width: 20px; height: 78px;">档案室</span>
                </div>
                <div style="float: left; padding: 4px;">
                    <img style="width: 110px; height: 70px;" src="/images/bq.jpg" /></div>
            </div>
            <div style="width: auto; display: table; float: right;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 300px; text-align: center;
                    border-bottom: 2px solid #000; border-left: 2px solid #000; font-family: 华文中宋;
                    font-size: 16px;">
                    <tr>
                        <td style="letter-spacing: 2px; height: 28px; text-align: center; border-top: 2px solid #000;
                            border-right: 2px solid #000;">
                            全&nbsp;宗&nbsp;号
                        </td>
                        <td style="text-align: center; border-top: 2px solid #000; border-right: 2px solid #000;">
                            目&nbsp;录&nbsp;号
                        </td>
                        <td style="text-align: center; border-top: 2px solid #000; border-right: 2px solid #000;">
                            卷&nbsp;&nbsp;号
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 48px; border-top: 2px solid #000; border-right: 2px solid #000;">
                            <span id="span_qzh"></span>
                        </td>
                        <td style="border-top: 2px solid #000; border-right: 2px solid #000;">
                            <span id="span_mlh"></span>
                        </td>
                        <td style="border-top: 2px solid #000; border-right: 2px solid #000;">
                            <span id="span_jh"></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <div style="border: 4px solid #000; border-bottom: 0px;">
                <div style="text-align: center; padding-top: 5px; font-family: 宋体; font-weight: 900;
                    font-size: 30px;">
                    <span id="span_bt"></span>人民法院</div>
                <div style="text-align: center; font-family: 宋体; font-weight: bold; font-size: 48px;
                    padding-top: 6px; padding-bottom: 10px;">
                    <span id="span_fbt"></span>一审诉讼卷宗</div>
            </div>
            <div style="">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: 4px solid #000;
                    border-top: 0px; table-layout: fixed; font-family: 宋体;">
                    <col style="width: 38px;" />
                    <col style="width: 76px;" />
                    <col style="width: 20px;" />
                    <col style="width: 110px;" />
                    <col />
                    <col style="width: 92px;" />
                    <col style="width: 0px;" />
                    <col style="width: 60px;" />
                    <col style="width: 110px;" />
                    <tbody>
                        <tr>
                            <td colspan="9" style="text-align: center; font-size: 22px; height: 46px; border-top: 2px solid #000;">
                                <span id="span_nf" style="width: 80px; display: inline-block;"></span>年度 <span id="span_dwjc"
                                    style="display: inline-block; padding: 0 10px; min-width: 100px;"></span><span id="span_z"
                                        style="display: inline-block; padding: 0 10px; min-width: 80px;"></span>
                                字第 <span id="span_h" style="min-width: 80px; display: inline-block;"></span>号
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                案&nbsp;&nbsp;由
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_ay"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                送案机关
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_sajg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="4" style="text-align: center; font-size: 22px; font-weight: bold; border-right: 2px solid #000;
                                border-top: 2px solid #000;">
                                当事人
                            </td>
                            <td colspan="2" style="font-size: 22px; height: 66px; font-weight: bold; padding-left: 5px;
                                border-right: 2px solid #000; border-top: 2px solid #000;">
                                原 告
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_yg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 66px; border-top: 2px solid #000;
                                border-right: 2px solid #000;">
                                &nbsp;
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 66px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                被 告
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_bg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 66px; border-top: 2px solid #000;
                                border-right: 2px solid #000;">
                                &nbsp;
                            </td>
                            <td colspan="6" style="font-size: 22px; border-top: 2px solid #000;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                收案日期
                            </td>
                            <td colspan="7" style="font-size: 0; border-top: 2px solid #000;">
                                <span id="span_sarq" style="display: inline-block; border-right: 2px solid #000;
                                    text-align: center; line-height: 46px; height: 46px; font-size: 22px; width: 40%;">
                                </span><span style="display: inline-block; border-right: 2px solid #000; text-align: center;
                                    line-height: 46px; height: 46px; font-size: 22px; width: 96px; font-weight: bold;">
                                    结案日期</span> <span id="span_jarq" style="display: inline-block; font-size: 22px; text-align: center;
                                        line-height: 46px; height: 46px; width: 40%;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 140px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                审理结果
                            </td>
                            <td colspan="7" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_sljg"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2" style="font-size: 22px; height: 46px; font-weight: bold;
                                padding-left: 5px; letter-spacing: 9px; border-right: 2px solid #000; border-top: 2px solid #000;">
                                合议庭成&nbsp;员
                            </td>
                            <td colspan="3" rowspan="2" style="font-size: 22px; border-top: 2px solid #000; border-right: 2px solid #000;">
                                <span id="span_hytcy"></span>
                            </td>
                            <td colspan="2" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                主办人
                            </td>
                            <td colspan="2" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_zbr"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                书记员
                            </td>
                            <td colspan="2" style="font-size: 22px; border-top: 2px solid #000;">
                                <span id="span_sjy"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" style="font-size: 0; border-top: 2px solid #000;">
                                <span style="display: inline-block; border-right: 2px solid #000; text-align: center;
                                    height: 46px; line-height: 46px; font-size: 22px; width: 33%;"><span style="float: left;
                                        font-weight: bold; padding-left: 5px;">本卷共</span><span id="span_bjgjc" style="display: inline-block;
                                            text-align: center;"></span><span style="float: right; font-weight: bold;">册&nbsp;</span></span>
                                <span style="display: inline-block; border-right: 2px solid #000; text-align: center;
                                    height: 46px; line-height: 46px; font-size: 22px; width: 33%;"><span style="float: left;
                                        font-weight: bold;">&nbsp;属第</span><span id="span_sdjc"></span><span style="float: right;
                                            font-weight: bold;">册&nbsp;</span></span> <span style="display: inline-block; width: 33%;
                                                height: 46px; line-height: 46px; text-align: center; font-size: 22px;"><span style="float: left;
                                                    font-weight: bold;">&nbsp;册内共</span><span id="span_ngjy"></span><span style="float: right;
                                                        font-weight: bold;">页&nbsp;</span></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 22px; height: 46px; font-weight: bold; padding-left: 5px;
                                border-top: 2px solid #000; border-right: 2px solid #000;">
                                归档日期
                            </td>
                            <td colspan="7" style="font-size: 22px; text-align: center; border-top: 2px solid #000;">
                                <span id="span_gdrq" style="display: inline-block; height: 46px; line-height: 46px;">
                                </span><span style="float: right; display: inline-block; width: auto;"><span style="display: inline-block;
                                    border-right: 2px solid #000; border-left: 2px solid #000; width: 92px; height: 46px;
                                    line-height: 46px; font-weight: bold;">保管期限</span> <span id="span_bgqx" style="display: inline-block;
                                        width: 100px; height: 46px; line-height: 46px;"></span></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!--endprint-->
    </div>
    <script type="text/javascript">

        var grid = null;
        var cmbN = null;
        var cmbYwlx = null;
        var cmbAjlb = null;
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png'
            });

            $('#btn_print').ligerButton({
                text: '打印封面',
                width:80,
                icon: '../../images/NewAdd/dy.png'
            });
            $('#btn_print2').ligerButton({
                text: '打印封面',
                width:80,
                icon: '../../images/NewAdd/dy.png'
            });
            $('#btn_print3').ligerButton({
                text: '打印封盒',
                width:80,
                icon: '../../images/NewAdd/dy.png'
            });
            $('#btn_print4').ligerButton({
                text: '打印封盒',
                width:80,
                icon: '/LigerUI/lib/LigerUI/skins/icons/print.gif'
            });
            //年

            cmbNf = $("#txt_nf").ligerComboBox({
                url: "/Handler/ZZJG/DZJZ_Report.ashx?action=GetNf",
                valueField: "id",
                textField: 'text',
                width: 80,
                autocomplete: true,                
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,

            });

            //业务编码
            cmbYwlx = $("#txt_ywlx").ligerComboBox({
                url: "/Handler/ZZJG/DZJZ_Report.ashx?action=GetAllBusinessType",
                valueField: "ywbm",
                textField: 'ywmc',
                width: 80,
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                onSuccess: function (data) {
                    if(data[0] && data[0] != undefined)
                    cmbYwlx.setValue(data[0].ywbm);
                },
                onSelected: function (value, text) {
                    $("#txt_bt,#txt_bt2").val("");
                    if(text == "执行")
                    {
                        $("#table_temp1").hide();
                        $("#table_temp2").show();
                        
                    }else
                    {
                        $("#table_temp1").show();
                        $("#table_temp2").hide();
                        
                    }
                    GetAjlbByYwbm(value);
                    
                }
            });
            //案件类别编码
            cmbAjlb = $("#txt_z").ligerComboBox({
                valueField: "AJLBBM",
                textField: 'AJLBMC',
                width: 80,
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250
            });

            //审理结果
            $("#txt_shjg").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: "一审判决", text: "一审判决" }, { id: "一审裁定、撤诉", text: "一审裁定、撤诉" }, { id: "一审判决|二审维持原判", text: "一审判决|二审维持原判" }, { id: "一审判决|二审部分改判", text: "一审判决|二审部分改判" }, { id: "一审调解", text: "一审调解"},{ id: "一审判决|二审调解", text: "一审判决|二审调解"},{ id: "一审判决|二审裁定撤诉", text: "一审判决|二审裁定撤诉"},{ id: "一审判决|二审改判", text: "一审判决|二审改判"}]
            });
            //保管期限
            $("#txt_bgqx").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: "短期", text: "短期" }, { id: "长期", text: "长期" }, { id: "永久", text: "永久"}],
                onSuccess: function (data) {                   
                    //bgqx.setValue(data[0].id);
                }
            });

            //业务庭
            $("#txt_ywt").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: 1, text: "机关党委" }, { id: 2, text: "监察室" }, { id: 3, text: "监导室" }, { id: 4, text: "立案庭" }, { id: 5, text: "民二庭" }, { id: 6, text: "民三庭" }, { id: 7, text: "民一庭" }, { id: 8, text: "前海人民法庭" }, { id: 9, text: "沙河人民法庭" }, { id: 10, text: "少年庭" }, { id: 11, text: "蛇口人民法庭" }, { id: 12, text: "审监庭" }, { id: 13, text: "司法事务室" }, { id: 14, text: "速裁庭" }, { id: 15, text: "西丽人民法庭" }, { id: 16, text: "信访办" }, { id: 17, text: "刑事审判庭" }, { id: 18, text: "行政庭" }, { id: 19, text: "粤海人民法庭" }, { id: 20, text: "知识产权庭" }, { id: 21, text: "执行局"}]
            });

            //获取部门受案号
            $('#btn_inquire').ligerButton({
                text: '查询',
                icon: '../../images/NewAdd/cx.png',
                click: function () {
                    var nf = cmbNf.getValue();
                    var z = cmbAjlb.getText();
                    var h = $("#txt_h").val();
                    if (!nf || nf == null || nf == "" || nf == undefined) {
                        $.ligerDialog.warn("请选择年份");
                        return false;
                    }
                    if (!z || z == null || z == "" || z == undefined) {
                        $.ligerDialog.warn("请选择字");
                        return false;
                    }
                    if (!h || h == null || h == "" || h == undefined) {
                        $.ligerDialog.warn("请输入号");
                        return false;
                    }
                    $.ligerDialog.waitting('执行中，请稍后...');
                    $.ajax({
                        type: "POST",
                        url: "/Pages/Print/PrintFrontCover.aspx",
                        data: { t: "GetBmsah", nf: nf, z: z, h: h },
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
                            if (data.t == "win")
                                $("#txt_bt,#txt_bt2").val(data.v);
                            else {
                                $("input[name='txt_bt']").val(data.AHMC);
                                $("input[name='txt_fbt']").val(data.AY);
                                $("input[name='txt_ajmc']").val(data.SAJG);                                 
                                $("textarea[name='txt_ajbh']").text(data.YG);
                                $("textarea[name='txt_fzxyr']").text(data.BG);
                                $("input[name='txt_sasj']").val(data.SARQ.toString().replace(" 00:00:00","")); //收案日期
                                $("input[name='txt_jasj']").val(data.JARQ.toString().replace(" 00:00:00","")); //结案日期
                                $("input[name='txt_shjg']").ligerGetComboBoxManager().setValue(data.SLJG); //审理结果                               
                                $("input[name='txt_gldaxlh']").val(data.GLDAXLH);
                                $("textarea[name='txt_hytcy']").text(data.HYTCY);
                                $("input[name='txt_cbr']").val(data.CBR);
                                $("input[name='txt_sjy']").val(data.SJY);
                                $("input[name='txt_bjgjc']").val(data.ZCS);  //总册数
                                $("input[name='txt_sdjc']").val(data.DJC); //第几册
                                $("input[name='txt_ngjy']").val(data.YS); //页数
                                $("input[name='txt_gdrq']").val(data.GDRQ.toString().replace(" 00:00:00","")); //归档日期
                                $("input[name='txt_bgqx']").ligerGetComboBoxManager().setValue(data.BGQX); //保管期限
                                var ywtid = $("input[name='txt_ywt']").ligerGetComboBoxManager().findValueByText(data.YWT);
                                $("input[name='txt_ywt']").ligerGetComboBoxManager().setValue(ywtid); //业务庭


                                $("textarea[name='txt_yyr']").text(data.YYR); //异议人
                                $("textarea[name='txt_sqzxr']").text(data.SQZXR); //申请执行人
                                $("textarea[name='txt_bzxr']").text(data.BZXR); //被执行人
                                var cjjgid = $("input[name='txt_cjjg']").ligerGetComboBoxManager().findValueByText(data.CJJG);
                                $("input[name='txt_cjjg']").val(data.cjjgid); //裁决机关
                                $("input[name='txt_zxbd']").val(data.ZXBD);//执行标的
                                $("input[name='txt_zxjg']").val(data.ZXJG);//执行结果
                                var jafsid = $("input[name='txt_jafs']").ligerGetComboBoxManager().findValueByText(data.JAFS);
                                $("input[name='txt_jafs']").ligerGetComboBoxManager().setValue(jafsid);//结案方式
                        
                                
                                //$.ligerDialog.error(data.v);
                            }
                        }, complete: function () {
                            $.ligerDialog.closeWaitting();
                        }
                    });
                }
            });

            $("#txt_sasj").ligerDateEditor({ 
//            initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });
           
            $("#txt_jasj").ligerDateEditor({ 
//            initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });
            $("#txt_gdrq").ligerDateEditor({ 
//            initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });

            window.onload = function () {
                $("body").css("overflow", "auto");
            }


            /*
            grid = $("#mainGrid").ligerGrid({
            columns: [
            { display: '标题', name: 'BT', minWidth: 150 },
            { display: '案件名称', name: 'AJMC', minWidth: 150 },
            { display: '案件编号', name: 'AJBH', minWidth: 150 },
            { display: '犯罪嫌疑人', name: 'FZXYR', minWidth: 150 },
            { display: '立案时间', name: 'LASJ', minWidth: 150, render: function (item) {
            if (item.LASJ)
            return item.LASJ.toString().replace(" 00:00:00", "");
            return "";
            }
            },
            { display: '结案时间', name: 'JASJ', minWidth: 150, render: function (item) {
            if (item.JASJ)
            return item.JASJ.toString().replace(" 00:00:00", "");
            return "";
            }
            },
            { display: '立卷单位', name: 'LJDW', minWidth: 150 },
            { display: '立卷人', name: 'LJR', minWidth: 150 },
            { display: '审核人', name: 'SHR', minWidth: 150 },
            { display: '本案共卷', name: 'BAGJ', minWidth: 150 },
            { display: '第几卷', name: 'DJJ', minWidth: 150 },
            { display: '共几页', name: 'GJY', minWidth: 150 },
            { display: '操作人', name: 'CZR', minWidth: 150 }, 
            { display: '', name: 'BM', width: 1, hide: true }
            ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
            , width: '100%', height: '100%',       //服务器分页
            url: '/Pages/Print/PrintFrontCover.aspx?page=1',
            alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -16,
            parms: { t: "ListBind",
            key: $("#txt_key").val()
            }, toolbar: { items: [
            { text: '增加', click: addDown, icon: 'add' },
            { line: true },
            { text: '修改', click: editData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
            }, onSuccess: function (data) {
            if (data.t) {
            $.ligerDialog.error(data.v);
            }
            }
            });
            */

            
          


            //裁决机关
            $("#txt_cjjg2").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250
            });

            //承办人
//            $("#txt_cbr2").ligerComboBox({
//                keySupport: true, //按键支持
//                selectBoxWidth: 200,
//                selectBoxHeight: 250
//            });

            //书记员
//            $("#txt_sjy2").ligerComboBox({
//                keySupport: true, //按键支持
//                selectBoxWidth: 200,
//                selectBoxHeight: 250
//            });

            //结案方式
            $("#txt_jafs2").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: 1, text: "全部执行完毕" }, { id: 2, text: "部分执行完毕" }, { id: 3, text: "不予执行" }, { id: 4, text: "当事人达成执行和解" }, { id: 5, text: "当事人达成执行和解并履行完毕" }, { id: 6, text: "终结执行" }, { id: 7, text: "委托执行" }, { id: 8, text: "指定执行" }, { id: 9, text: "提级执行" }, { id: 10, text: "终结本次执行程序" }, { id: 11, text: "驳回执行申请"}]
            });

            //业务庭
            $("#txt_ywt2").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: 1, text: "机关党委" }, { id: 2, text: "监察室" }, { id: 3, text: "监导室" }, { id: 4, text: "立案庭" }, { id: 5, text: "民二庭" }, { id: 6, text: "民三庭" }, { id: 7, text: "民一庭" }, { id: 8, text: "前海人民法庭" }, { id: 9, text: "沙河人民法庭" }, { id: 10, text: "少年庭" }, { id: 11, text: "蛇口人民法庭" }, { id: 12, text: "审监庭" }, { id: 13, text: "司法事务室" }, { id: 14, text: "速裁庭" }, { id: 15, text: "西丽人民法庭" }, { id: 16, text: "信访办" }, { id: 17, text: "刑事审判庭" }, { id: 18, text: "行政庭" }, { id: 19, text: "粤海人民法庭" }, { id: 20, text: "知识产权庭" }, { id: 21, text: "执行局"}]
            });


            //保管期限
            $("#txt_bgqx2").ligerComboBox({
                keySupport: true, //按键支持
                selectBoxWidth: 200,
                selectBoxHeight: 250,
                data: [{ id: 1, text: "短期" }, { id: 2, text: "长期" }, { id: 3, text: "永久"}]
            });

             $("#txt_sasj2").ligerDateEditor({ 
//             initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });
            $("#txt_jasj2").ligerDateEditor({ 
//            initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });
            $("#txt_gdrq2").ligerDateEditor({ 
//            initValue: function () {
//                var date = new Date();
//                return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
//            }
            });
            $("#pageloading").hide();

            tabKey("add_div");
            //日期自动补充横线
             $("#txt_sasj,#txt_jasj,#txt_gdrq,#txt_sasj2,#txt_jasj2,#txt_gdrq2").keyup(function(e){                
                  if (e.which != 13 && e.which != 8) {
                    var v = $(this).val();                   
                    if(v.length == 4 || v.length == 7)
                    $(this).val(v+"-");
                  }
            });

        });

        //回车执行tab事件
        function tabKey(obj)
        {
             
             $("#"+obj+" input:text,textarea").keypress(function (e) {
                if (e.which == 13) {// 判断所按是否回车键  
                    console.log(this);
                    if($(this).attr("id") == "txt_h")
                    {
                        $("#btn_inquire").click();
                    }
                    
                    var inputs = $("#"+obj+" input:text,textarea"); // 获取表单中的所有输入框  
                    var form1Num = 0;                  
                    var idx = inputs.index(this); // 获取当前焦点输入框所处的位置  
                      if($("#table_temp1").is(":hidden") && idx <= 3){
                        form1Num = $("#table_temp1 input:text,textarea").length-4;
                    }
              
                    if (idx == inputs.length - 1) {// 判断是否是最后一个输入框                         
//                        if (confirm("最后一个输入框已经输入,是否提交?")) // 用户确认  
//                            $("form[name='contractForm']").submit(); // 提交表单  
                    } else {
                        
                        if($(inputs[idx+1]).attr("ligeruiid") == "btn_inquire" )
                        {
                            $(inputs[idx+1]).click();                
                            inputs[idx+form1Num + 3].focus(); // 设置焦点  
                            inputs[idx+form1Num + 3].select(); // 选中文字  
                        }else if(idx == 3){
                          
                            inputs[idx+form1Num + 1].focus(); // 设置焦点  
                            inputs[idx+form1Num + 1].select(); // 选中文字  
                        }else
                        {
                            inputs[idx + 1].focus(); // 设置焦点  
                            inputs[idx + 1].select(); // 选中文字  
                        }
                    }
                    return false; // 取消默认的提交行为  
                }
            });
        }

        //提交保持数据
        function submitForm(obj,type) {

            var isUp = false;
            var jdata = $('#'+obj).serializeArray();
            jdata[jdata.length] = { name: "txt_type", value: type };
            jdata[jdata.length] = { name: "txt_ywlx", value: $("#txt_ywlx").val() };
            jdata[jdata.length] = { name: "txt_ywlx_val", value: $("#txt_ywlx_val").val()};
            jdata[jdata.length] = { name: "txt_nf", value: $("#txt_nf").val() };
            jdata[jdata.length] = { name: "txt_nf_val", value:$("#txt_nf_val").val()};
            jdata[jdata.length] = { name: "txt_z", value: $("#txt_z").val()};
            jdata[jdata.length] = { name: "txt_z_val", value:$("#txt_z_val").val() };
            jdata[jdata.length] = { name: "txt_h", value: $("#txt_h").val() };

            if ($.trim($("#key_hidd").val()) == "")
                jdata[jdata.length] = { name: "t", value: "AddData" };
            else {
                jdata[jdata.length] = { name: "t", value: "UpData" };
                isUp = true;
            }
             var manager = $.ligerDialog.waitting('正在打印,请稍候...');
            $.ajax({
                type: "POST",
                url: "/Pages/Print/PrintFrontCover.aspx",
                data: jdata,
                dataType: 'json',
                timeout: 10000,
                cache: false,
                beforeSend: function () {
                },
                error: function (xhr) {
                    manager.close();
                    $.ligerDialog.error('网络连接错误');
                    return false;
                },
                success: function (data) {
                    if (data.t == "win") {
                        var fileMake="RecEFileMaker";
                        <% if (Version == "PSB") {%> 
                            fileMake="CopEFileMaker";
                        <%} %>
                        var parm = fileMake + "://" + data.v;

                        if (typeof (boundObjectForJS) != 'undefined')
                            boundObjectForJS.callCsharp(fileMake + "://1234567812345678" + data.v + "@");
                        else
                            location.href = parm;
                         
                          setTimeout(function ()
                          {
                              manager.close();
                          }, 3000);
                        /*
                        $("#span_mlh").text("");
                        $("#span_jh").text($("#txt_h").val());

                        $("#span_bt").text("<%= UserInfo.DWMC %>");
                        $("#span_fbt").text($("#txt_z").val());
                        $("#span_nf").text($("#txt_nf").val());
                        $("#span_dwjc").text("<%=UserDwbm.DWJC %>");
                        $("#span_z").text($("#txt_z").val());
                        $("#span_h").text($("#txt_h").val());

                        //$("#span_ahmc").text($("#txt_bt").val());
                        $("#span_ay").text($("#txt_fbt").val());
                        $("#span_sajg").text($("#txt_ajmc").val());
                        $("#span_yg").text($("#txt_ajbh").val());
                        $("#span_bg").text($("#txt_fzxyr").val());


                        if ($("#txt_sasj").val() != "") {
                        var date = new Date($("#txt_sasj").val().replace(/-/g, '/'));
                        $("#span_sarq").text(date.getFullYear() + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日");
                        }
                        if ($("#txt_jasj").val() != "") {
                        var date = new Date($("#txt_jasj").val().replace(/-/g, '/'));
                        $("#span_jarq").text(date.getFullYear() + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日");
                        }
                        $("#span_sljg").text($("#txt_shjg").val());
                        $("#span_hytcy").text($("#txt_hytcy").val());

                        $("#span_zbr").text($("#txt_cbr").val());
                        $("#span_sjy").text($("#txt_sjy").val());

                        $("#span_bjgjc").text($("#txt_bjgjc").val());
                        $("#span_sdjc").text($("#txt_sdjc").val());
                        $("#span_ngjy").text($("#txt_ngjy").val());

                        if ($("#txt_gdrq").val() != "") {
                        var date = new Date($("#txt_gdrq").val().replace(/-/g, '/'));
                        $("#span_gdrq").text(date.getFullYear() + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日");
                        }

                        $("#span_bgqx").text($("#txt_bgqx").val());

                        $("#print").printArea(); //doPrint(); //打印

                        //                        $("#add_form")[0].reset();
                        $.ligerDialog.hide();
                        //grid.reload();
                        $.ligerDialog.success(data.v);
                        */
                    } else {
                        manager.close();
                        $.ligerDialog.error(data.v);
                    }
                }, complete: function () {
                  
                }
            });
        }


        //添加按钮
        function addDown() {
            $('#key_hidd').val('');
            $("#add_form")[0].reset();

            $.ligerDialog.open({ title: '增加资料', target: $('#add_div'), width: 570,
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
        //修改
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                $.ajax({
                    type: "POST",
                    url: '/Pages/Print/PrintFrontCover.aspx',
                    data: { t: "GetModel", id: cksld.BM },
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

                            $("#key_hidd").val(data.BM);

                            $("#txt_bt").val(data.BT);
                            $("#txt_fbt").val(data.FBT);
                            $("#txt_ajmc").val(data.AJMC);
                            $("#txt_ajbh").val(data.AJBH);
                            $("#txt_fzxyr").val(data.FZXYR);
                            if (data.LASJ)
                                $("#txt_sasj").val(data.LASJ.toString().replace(" 00:00:00", ""));
                            if (data.JASJ)
                                $("#txt_jasj").val(data.JASJ.toString().replace(" 00:00:00", ""));
                            $("#txt_ljdw").val(data.LJDW);
                            $("#txt_ljr").val(data.LJR);
                            $("#txt_shr").val(data.SHR);
                            $("#txt_bagj").val(data.BAGJ);
                            $("#txt_djj").val(data.DJJ);
                            $("#txt_gjy").val(data.GJY);

                            $.ligerDialog.open({ title: '编辑', target: $('#add_div'), width: 570,
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
                $.ligerDialog.warn('请先选择一个需要编辑的信息');
        }
        //删除
        function deleteData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();
                ar[0] = { name: "id", value: arrck.BM };
                ar[1] = { name: "t", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Pages/Print/PrintFrontCover.aspx',
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
                $.ligerDialog.warn('请先选择一个需要删除的单位');
        }
        //获取案件类别 业务编码
        function GetAjlbByYwbm(ywbm) {

            if (cmbAjlb && ywbm) {
                $.ajax({
                    type: "POST",
                    url: "/Handler/ZZJG/DZJZ_Report.ashx?action=BindAjlb",
                    data: { ywbm: ywbm },
                    dataType: 'json',
                    timeout: 5000,
                    cache: false,
                    beforeSend: function () { },
                    error: function (xhr) {
                        $.ligerDialog.error('网络连接错误');
                        return false;
                    },
                    success: function (data) {
                        if (data.t) {
                            $.ligerDialog.error(data.v);
                        } else {
                            cmbAjlb.setData(data);
                        }
                    }
                });
            }
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
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
        }
    </script>
    <script type="text/javascript">
        function btn_print(obj, type) {
            submitForm(obj, type);
        }

        function h_keyup_func(obj) {
            if ($("#txt_bt").val() != "" && $(obj).val() != "") {
                var str = $("#txt_bt").val();
                str = str.substring(0, str.lastIndexOf("第") + 1) + $(obj).val() + "号";
                $("#txt_bt").val(str);
            }
        }

    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
