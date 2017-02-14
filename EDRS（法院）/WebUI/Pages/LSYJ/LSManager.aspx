<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LSManager.aspx.cs" Inherits="WebUI.Pages.LSYJ.LSManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>律师管理</title>
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
      
    <style type="text/css">
        .l-text{ height:21px;}
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
            background: url("/images/icons/usergroup.png") no-repeat  scroll left center !important;
            background-size:14px 14px;
        }
        .l-tree-icon-folder1-open
        {
            background: url("/images/icons/usergroup-open.png") no-repeat scroll left center !important;
            background-size:14px 14px;
        }
        .l-tree-icon-leaf1
        {
            background: url("/images/icons/usergroup.png") no-repeat scroll left center !important;
            background-size:14px 14px;
        }
        .picBox
        {
            margin:5px;
        }
    </style>
</head>
<body runat="server" style="margin: 0; overflow: hidden; padding: 1px;">
    <div id="layout" style="width: 100%; margin: 0; padding: 0;">
        <div id="leftFrm" position="left" title="单位列表">
            <ul id="tree_left">
            </ul>
        </div>
        <div id="centterFrm" position="center" title="律师管理">
            <div id="tb" style="background-color: #f8f8f8">
                <div style="padding: 4px 5px;">
                
                
                    律师证号：<input id="txt_id" class="l-text" type="text" name="txt_id" style="width: 130px" />
                    &nbsp;&nbsp;律师姓名：<input id="txt_name" class="l-text" type="text" name="txt_name" style="width: 130px" />
                    &nbsp;&nbsp;创建时间：<input id="txt_time_begin" type="text" name="txt_time_begin" />&nbsp;&nbsp;-&nbsp;&nbsp;<input
                        id="txt_time_end" type="text" name="txt_time_end" />
                    <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
                </div>
            </div>
            <div id="mainGrid" style="margin: 0px; padding: 0px">
            
            </div>
        </div>
    <%--添加数据窗口--%>
    <div id="add_div" style="display:block; padding-left:50px;">
        <form id="add_form" method="post" runat="server" action="LSManager.aspx">
        <input type="text" id="action" name="action" style="display:none;" />
        <table style="line-height:30px; width:750px;">
    <tr>
    <td>律师证号：</td>
    <td><input id="LSZH" class="l-text" type="text" name="LSZH" style="width: 130px" validate="{required:true}"/>
    </td>
    
    <td>律师姓名：</td>
    <td><input id="LSXM" class="l-text" type="text" name="LSXM" style="width: 130px" validate="{required:true}"/></td>
    </tr>
    <tr>
    <td>律师资格有效时间：</td>
    <td><input id="LSZGYXSJ" class="l-text" type="text" name="LSZGYXSJ" style="width: 120px" />
    </td>
    <td colspan="2">是否吊销资格(Y/N)：<input id="SFDXZG" type="checkbox" class="l-checkbox"  name="SFDXZG" value="" runat="server"/></td>
    </tr>
    <tr>
    <td>律师所属单位：</td>
    <td>
    <input type="text" id="LSDW" name="LSDW"  style="display:none"/>
    <input id="LSDWMC" class="l-text" type="text" name="LSDWMC" style="width: 130px" validate="{required:true}"/>
    </td>
    </tr>
    <tr>
    <td>律师单位地址：</td>
    <td><input id="LSDWDZ" class="l-text" type="text" name="LSDWDZ" style="width: 130px" validate="{required:true}"/></td>
    <td>律师单位邮政号码：</td>
    <td colspan="3"><input id="LSDWYZHM" class="l-text" type="text" name="LSDWYZHM" style="width: 130px" /></td>
    </tr>
    <tr>
    <td>律师联系电话：</td>
    <td><input id="LSLXDH" class="l-text" type="text" name="LSLXDH" style="width: 130px" /></td>
    <td>律师手机：</td>
    <td><input id="LSSJ" class="l-text" type="text" name="LSSJ" style="width: 130px" /></td>
    </tr>
    <tr>
    <td>第二联系人：</td>
    <td><input id="DELXR" class="l-text" type="text" name="DELXR" style="width: 130px" /></td>
    <td>第二联系人电话：</td>
    <td><input id="DELXRDH" class="l-text" type="text" name="DELXRDH" style="width: 130px" /></td>
    </tr>
    <tr>
    <td>律师信息备注：</td>
    <td colspan="3" style="margin:5px; width:auto;"><textarea id="LSXXBZ" name="LSXXBZ" class="l-textarea"
                        style="width: 100%; height: 50px"></textarea></td>
    </tr>
    <tr>
    <td>律师资质文件：</td>
    <td colspan="3">
    
    <div id="wrapper">
        <div id="container">
            <!--头部，相册选择和格式选择-->
            <div id="uploader">
                <div class="queueList">
                    <div id="dndArea" class="placeholder">
                        <div id="filePicker"></div>
                        <p style=" font-size:18px;">或将图片拖到这里</p>
                    </div>
                </div>
                <div class="statusBar" style="display:none;">
                    <div class="progress">
                        <span class="text">0%</span>
                        <span class="percentage"></span>
                    </div><div class="info"></div>
                    <div class="btns">
                        <div id="filePicker2"></div><div class="uploadBtn">开始上传</div>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
    <br />
    <input id="LSZZWJ" class="l-text" type="text" name="LSZZWJ" style="width: 130px; display:none;"/>
    </td>
    </tr>
    <tr>
    <td>创建时间：</td>
    <td><input id="CJSJ" class="l-text" type="text" name="CJSJ" style="width: 130px"/></td>
    <td>最后一次阅卷时间：</td>
    <td><input id="ZHYCYJSJ" class="l-text" type="text" name="ZHYCYJSJ" style="width: 130px" />
    
    </td>
    </tr>
    </table>
        </form>
    </div>
    
    <script type="text/javascript">
        var grid = null;
        var tree = null;
        var tree_dw = '/images/icons/3.png';
        var tree_bm = '/images/icons/bm.png';
        var tree_js = '/images/icons/4.png';
        function ShowWarn(msg) {
            $.ligerDialog.warn(msg);
        }
        $(function () {
            //时间控件
            $("#txt_time_begin").ligerDateEditor({ labelWidth: 90, labelAlign: 'center' });
            $("#txt_time_end").ligerDateEditor({ labelWidth: 90, labelAlign: 'center' });
            $('#btn_search').ligerButton({
                text: '查询',
                icon: '/LigerUI/lib/LigerUI/skins/icons/search.gif'
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
                $("#leftFrm").height(height - 30);
            }
            //加载树
            tree = $("#tree_left").ligerTree({
                url: "/Handler/Common/UnitCommonHandler.ashx?pa=1",
                parms: {
                    t: 'GetTreeDW', level: 3
                },
                isExpand: 2,
                checkbox: false,
                slide: false,
                nodeWidth: 120,
                //                parentIcon: 'folder1',
                //                childIcon: 'leaf1',
                onClick: function (node) {
                    if (tree.getSelected()) {
                        grid.loadServerData({ "dkey": tree.getSelected().data.id, txt_id: $("#txt_id").val(), txt_name: $("#txt_name").val(), txt_time_begin: $("#txt_time_begin").val(), txt_time_end: $("#txt_time_end").val() });
                    }
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }, onBeforeExpand: function (node) {
                    if (node.data.children.length == 0) {
                        $.post("/Handler/Common/UnitCommonHandler.ashx?pa=2", { t: "GetTreeDW", level: 3, pid: node.data.id },
                        function (newData) {
                            if (newData.t) {
                                $.ligerDialog.error(newData.v);
                            } else {
                                tree.append(node.target, newData);
                            }
                        }, 'json');
                    }
                }
            });
            //绑定列表
            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '律师证号', name: 'lszh', width: 100,  },
                { display: '律师姓名', name: 'lsxm', width: 70 },
                { display: '律师资质文件', name: 'lszzwj', width: 100, align: 'center', render: function (row, index) {
                    return '<img src="/LigerUI/lib/LigerUI/skins/Aqua/images/tree/folder-open.gif" onclick="OpenDiv(\'' + row.lszh + '\',1,\'' + row.lszzwj1 + '\')"/>';
                }
                },
                { display: '律师单位编码', name: 'lsdw', width: 200, hide: true },
                { display: '律师单位', name: 'lsdwmc', width: 200 },
                { display: '律师单位地址', name: 'lsdwdz', width: 200 },
                { display: '邮政编码', name: 'lsdwyzhm', width: 100 },
                { display: '联系电话', name: 'lslxdh', width: 100 },
                { display: '手机', name: 'lssj', width: 100 },
                { display: '第二联系人', name: 'delxr', width: 100 },
                { display: '第二联系人电话', name: 'delxrdh', width: 100 },
                { display: '律师资格有效时间', name: 'lszgyxsj', width: 100 },
                { display: '是否吊销资格(Y/N)', name: 'sfdxzg', width: 120, render: function (item) {
                    if (item.sfdxzg == "N")
                        return '否';
                    else
                        return '是';
                }
                },
                { display: '律师信息备注', name: 'lsxxbz', width: 100 },
                { display: '创建时间', name: 'cjsj', width: 100 },
                { display: '最后一次阅卷时间', name: 'zhycyjsj', width: 100 }
            ], width: '100%', height: '100%', heightDiff: -20,
                url: '/Handler/ZZJG/DZJZ_LSYJ.ashx?action=ListBind',
                param: { action: "ListBind" },
                fixedCellHeight: false, rownumbers: true, pageSize: 50, dataAction: 'local', //服务器排序
                usePager: true, width: '100%', height: '100%',       //服务器分页
                pageSizeOptions: [20, 50, 100, 500],
                toolbar: { items: [
                { text: '新增', click: addData, icon: 'add' },
                { line: true },
                { text: '修改', click: editData, icon: 'modify' },
                { line: true },
                { text: '删除', click: DelData, icon: 'delete' }
                ]
                }
            });
            $("#pageloading").hide();
        });

        function OpenDiv(lszh, index, obj) {
            var ar = new Array();
            Array.prototype.del = function (n) {
                if (n < 0)
                    return this;
                else
                    return this.slice(0, n).concat(this.slice(n + 1, this.length));
            };
            ar[0] = { name: "lszh", value: lszh };
            ar[1] = { name: "action", value: "GetImgList" };
            //获取图片列表
            $.ajax({
                type: "POST",
                url: '/Handler/ZZJG/DZJZ_LSYJ.ashx',
                data: ar,
                dataType: 'json',
                timeout: 5000,
                cache: false,
                beforeSend: function () { },
                error: function (xhr) {
                    $.ligerDialog.error('网络连接错误！');
                    return false;
                },
                success: function (data) {
                    if (data.length == 0) {
                        $.ligerDialog.error('无有效资质文件！');
                        return;
                    }
                    var imgIndex = 0;
                    var context = ""; //
                    var imgDiv = $('<div id="imgDiv" style="width:100%; height:auto;"></div>');
                    context = '<img src="../../LSZZWJ/' + data[imgIndex] + '" width="475px" height="430px" />';
                    $(imgDiv).html(context);
                    $.ligerDialog.open({ target: $(imgDiv), height: 500, width: 500, buttons: [
                    { text: '查看大图', onclick: function (item, dialog) {
                        window.open('../../LSZZWJ/' + data[imgIndex], "imgShow");
                    }
                    },
                    { text: '上一张', onclick: function (item, dialog) {
                        if (imgIndex > 0)
                            imgIndex--;
                        $(imgDiv).html('<img src="../../LSZZWJ/' + data[imgIndex] + '" width="475px" height="430px" />');
                    }
                    },
                    { text: '下一张', onclick: function (item, dialog) {
                        if (imgIndex < data.length - 1)
                            imgIndex++;
                        $(imgDiv).html('<img src="../../LSZZWJ/' + data[imgIndex] + '" width="475px" height="430px" />');
                    }
                    },
                    { text: '删除', onclick: function (item, dialog) {

                        var ar = new Array();

                        ar[0] = { name: "lszh", value: lszh };
                        ar[1] = { name: "imgName", value: data[imgIndex] };
                        ar[2] = { name: "action", value: "DelImg" };
                        //数据库删除
                        $.ligerDialog.confirm('确定是否删除?', function (r) {
                            if (r) {
                                $.ajax({
                                    type: "POST",
                                    url: '/Handler/ZZJG/DZJZ_LSYJ.ashx',
                                    data: ar,
                                    dataType: 'json',
                                    timeout: 5000,
                                    cache: false,
                                    beforeSend: function () { },
                                    error: function (xhr) {
                                        $.ligerDialog.error('网络连接错误！');
                                        return false;
                                    },
                                    success: function (returnData) {
                                        //成功后删除界面元素
                                        if (returnData.t == "win") {
                                            data = data.del(imgIndex);
                                            if (imgIndex > 0)
                                                imgIndex--;
                                            $(imgDiv).html('<img src="../../LSZZWJ/' + data[imgIndex] + '" width="475px" height="430px" />');
                                            $.ligerDialog.success(returnData.v);
                                        } else
                                            $.ligerDialog.error(returnData.v);
                                    }
                                });
                            }
                        });
                    }
                    },
                    { text: '关闭', onclick: function (item, dialog) { dialog.close(); } }]
                    });
                }
            });

        }


        //添加律师按钮
        function addData() {
//            $("#LSZH").val('');
//            $("#LSZH").ligerTextBox().setEnabled(true);
            var sv = tree.getSelected();
            if (sv != null) {
                detailForm = $.ligerDialog.open({ title: '添加律师',
                    url: 'LSDetails.aspx?LSDW=' + sv.data.id + '&action=' + 'AddData',
                    width: 950, height: 600, isResize: true
                });
            } else
                $.ligerDialog.warn('请先选择一个需要添加律师的单位！');
        }
        function CloseWindow(data) {
            if (data.t == "win") {
                $.ligerDialog.hide();
                $("#btn_search").click();
                $.ligerDialog.success(data.v);
            } else
                $.ligerDialog.error(data.v);
        }
        //            //删除按钮
        function DelData() {
            var arrck = grid.getSelectedRow();
            if (arrck) {
                var ar = new Array();

                ar[0] = { name: "LSZH", value: arrck.lszh };
                ar[1] = { name: "action", value: "DelData" };
                $.ligerDialog.confirm('确定是否删除?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "POST",
                            url: '/Handler/ZZJG/DZJZ_LSYJ.ashx',
                            data: ar,
                            dataType: 'json',
                            timeout: 5000,
                            cache: false,
                            beforeSend: function () { },
                            error: function (xhr) {
                                $.ligerDialog.error('网络连接错误！');
                                return false;
                            },
                            success: function (data) {
                                if (data.t == "win") {
                                    $("#btn_search").click();
                                    $.ligerDialog.success(data.v);
                                } else
                                    $.ligerDialog.error(data.v);
                            }
                        });
                    }
                });
            } else
                $.ligerDialog.warn('请先选择一个需要删除的律师！');
        }
        var detailForm;
        //点击编辑按钮
        function editData() {
            var cksld = grid.getSelectedRow();
            if (cksld != null) {
                detailForm = $.ligerDialog.open({ title: '修改律师',
                    url: 'LSDetails.aspx?LSZH=' + cksld.lszh + '&action=' + 'UpData', 
                    width: 950, height: 600, isResize: true
                });
            }
            else
                $.ligerDialog.warn('请先选择一个需要修改律师！');
        }
        $(document).ready(function () {
            //点击搜索按钮
            $("#btn_search").click(function () {
                //清除列表选中行     
                 var dkey = '';
                if(tree.getSelected())
                {
                    dkey = tree.getSelected().data.id;
                }
                grid.loadServerData({ "dkey": dkey, txt_id:$("#txt_id").val(),txt_name:$("#txt_name").val(),txt_time_begin:$("#txt_time_begin").val(),txt_time_end:$("#txt_time_end").val()});
            });

        });
    </script>
</body>
</html>
