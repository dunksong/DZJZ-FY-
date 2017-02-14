<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetDossierPage.aspx.cs"
    Inherits="WebUI.Interface.GetDossierPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>卷宗浏览</title>
    <link href="css/ztree.css" rel="stylesheet" type="text/css" />
    <link href="css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="Script/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="Script/jquery.ztree.core.js" type="text/javascript"></script>
    <script src="Script/jquery.ztree.excheck.min.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/LigerUI/js/core/base.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <style type="text/css">
        /* tr,td
        {
            height:100%;
        }
        */
    </style>
    <%--查询案件--%>
    <script type="text/javascript">
        var dwbm;
        var wsbh;
        var bm;
        var btnno;
        var btnyes;
        $(document).ready(function () {

            btnyes = $('#btn_tg').ligerButton({
                text: '通过',
                width: 70,
                icon: '/LigerUI/lib/LigerUI/skins/icons/right.gif',
                click: tg
            });
            btnno = $('#btn_btg').ligerButton({
                text: '不通过',
                width: 80,
                icon: '/LigerUI/lib/LigerUI/skins/icons/candle.gif',
                click: btg
            });

            //    dwbm = GetQueryString("dwbm");
            bm = GetQueryString("bh");
            wsbh = GetQueryString("wsbh");

            if (!bm && !wsbh) {
                $.ligerDialog.warn('请输入案件编号或者文书号');
                return false;
            }
            //            if (!dwbm) {
            //                alert("请输入单位编码");
            //                return false;
            //            }

            //            if (!bm) {
            //                alert("请输入案件编码");
            //                return false;
            //            }
            setting(wsbh, bm);

            //审核通过
            function tg() {
                var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = treeObj.getCheckedNodes(true);

                //console.log(nodes.length);
                if (nodes && nodes.length > 0) {
                    var str = "";
                    $(nodes).each(function () {
                        //console.log(this.type);
                        if (this.type == "a")
                            str += $.trim(this.id) + ",";
                    });
                    var ar = new Array();
                    ar[0] = { name: "bmsah", value: str };
                    ar[1] = { name: "otherParam", value: "setsh" };
                    ar[2] = { name: "txt_type", value: "4" };
                    ar[3] = { name: "txt_pz", value: "" };
                    $.ligerDialog.confirm('确定选择的案件审核通过？', function (yes) {
                        if (yes) {
                            $.ajax({
                                type: "POST",
                                url: "GetDossierPage.aspx",
                                data: ar,
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
                                        $.ligerDialog.success(data.v);
                                        location.href = location.href;
                                    } else {
                                        $.ligerDialog.error(data.v);
                                    }
                                }
                            });
                        }
                    });
                }
                else {
                    $.ligerDialog.warn('请选择需要审核的文书编号');
                }
            }
            //审核不通过
            function btg() {
                var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = treeObj.getCheckedNodes(true);

                if (!nodes || nodes.length == 0) {
                    $.ligerDialog.warn('请先选择一个文书编号');
                    return false;
                }
                else if (nodes && nodes.length > 0) {
                    var count = 0;
                    var str = "";
                    $(nodes).each(function () {
                        if (this.type == "a") {
                            str = this.id;
                            count++;
                        }
                    });
                    if (count > 1) {
                        $.ligerDialog.warn('一次只能审核不通过一个文书编号');
                        return false;
                    } else if (str == "") {
                        $.ligerDialog.warn('请先选择一个文书编号');
                        return false;
                    } else {
                        $.ligerDialog.prompt('审核批注', true, function (yes, value) {
                            if (yes) {
                                if (!value || value == "" || value.length == 0) {
                                    $.ligerDialog.warn('必须填写审核批注');
                                    return false;
                                }
                                var ar = new Array();
                                ar[0] = { name: "bmsah", value: str };
                                ar[1] = { name: "otherParam", value: "setsh" };
                                ar[2] = { name: "txt_type", value: "3" };
                                ar[3] = { name: "txt_pz", value: value };
                                if (ar.length > 1 && ar) {
                                    $.ajax({
                                        type: "POST",
                                        url: "GetDossierPage.aspx",
                                        data: ar,
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
                                                location.href = location.href;
                                                $.ligerDialog.success(data.v);

                                            } else {
                                                $.ligerDialog.error(data.v);
                                            }
                                        }
                                    });
                                }
                            }
                        });
                    }
                }
            }
        });


        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) {
                return encodeURI(r[2]);
            }
            return null;
        }

    </script>
    <%--绑定树形--%>
    <script type="text/javascript">

        function setting(wsbh, bh) {

            var setting = {
                data: {
                    key: {
                        title: "title"
                    },
                    simpleData: {
                        enable: true
                    }
                },
                check: {
                    enable: true,
                    autoCheckTrigger: true
                },
                async: {
                    enable: true,
                    url: "GetDossierPage.aspx",
                    autoParam: ["id", "name=n", "level=lv", "type", "JZBH"],
                    otherParam: { "otherParam": "zTreeAsyncTest", "wsbh": wsbh, "bh": bh },
                    dataFilter: filter
                },
                callback: {
                    onClick: onClick,
                    onAsyncSuccess: function (event, treeId, treeNode, msg) {// 此回调函数可逐级异步展开子节点      
                    
                        if (treeNode && treeNode.isParent == true && treeNode.children && treeNode.children.length > 0) {
                            var nodes = treeNode.children;
                            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                            for (var i = 0; i < nodes.length; i++) {
                                treeObj.expandNode(nodes[i], true, false, true, true);
                            }
                        } else {
                            //  $("#treeDemo li:first span:first").click(); // treeDemo_1_switch
                            $("#treeDemo_2_switch").click();
                        }
                    }
                }
            };


            $(document).ready(function () {
                var treeObj = $.fn.zTree.init($("#treeDemo"), setting);
            });
        }
        //构造节点
        function filter(treeId, parentNode, childNodes) {
           
            if (childNodes.t) {
                $("#treeDemo").html("<li>未找到卷宗信息</li>");
                $.ligerDialog.warn(childNodes.v);
                return null;
            }
            if (!childNodes)
                return null;
            // console.log(childNodes);
            for (var i = 0, l = childNodes.length; i < l; i++) {
                if (i == 0) {
                    switch (childNodes[i].zzzt && childNodes[i].zzzt.toString()) {
                        case "-1":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件还是未制作状态");
                            break;
                        case "0":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件正在制作中");
                            break;
                        case "1":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件已上传状态");
                            break;
                        case "3":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件审核不通过状态");
                            break;
                        case "4":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件已审核通过状态");
                            break;
                        case "5":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件已报送");
                            break;
                        case "6":
                            btnyes.setDisabled();
                            btnno.setDisabled();
                            $.ligerDialog.warn("该案件报送失败");
                            break;
                    }                   

//                    if (childNodes[i].zzzt == "False") {                    
//                        btnyes.setDisabled();
//                        btnno.setDisabled();                      
//                    }
                }
                if (childNodes[i].name)
                    childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
            }
            return childNodes;
        }


        //点击节点事件
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#btn_page").text("");
            $("#btn_page").attr("data-json", "");

            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            if (!treeNode.isParent) {

                var pNode = treeNode.getParentNode().getParentNode();
                if (pNode) {
                    $("#btn_page").text(pNode.name);
                    $("#btn_page").attr("data-json", pNode.tId);
                }

                var sNodes = treeObj.getSelectedNodes();
                if (sNodes.length > 0) {
                    var nodeNext = sNodes[0].getNextNode(); //后一个节点
                    var nodePre = sNodes[0].getPreNode(); //前一个节点
                    if (nodeNext) {
                        //下一页
                        $("#btn_next").attr("data-json", nodeNext.tId);
                    } else
                        $("#btn_next").attr("data-json", "");

                    if (nodePre) {
                        //上一页
                        $("#btn_pre").attr("data-json", nodePre.tId);
                    } else
                        $("#btn_pre").attr("data-json", "");

                }

                var src = "/Interface/GetDossierFilePage.aspx?JZBH=" + treeNode.JZBH + "&JZWJYBH=" + treeNode.id + "";
                $("#fileShowDemo").attr("src", src);
            } else {
                //console.log(treeNode);
                treeObj.expandNode(treeNode, true);
            }
        }

        //上一页
        function func_pre(obj) {
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            var tid = $("#btn_pre").attr("data-json");
            var node = treeObj.getNodeByTId(tid);
            if (node) {
                $("#" + node.tId + "_a").click();
                //   treeObj.selectNode(node);            
            }
        }

        //下一页
        function func_next(obj) {
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            var tid = $("#btn_next").attr("data-json");
            var node = treeObj.getNodeByTId(tid);

            if (node) {
                $("#" + node.tId + "_a").click();
                //   treeObj.selectNode(node);            
            }
        }

        //跳转
        function func_page(obj) {
            var text = $("#txt_pageIndex").val(); //文本值
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            var tid = $("#btn_page").attr("data-json");
            var node = treeObj.getNodeByTId(tid);
            var result = "";
            if (node) {
                var childrenNodes = treeObj.getNodesByParam("type", "y", node);
                var len = childrenNodes.length;
                if (len >= Number(text) - 1) {
                    var chNode = childrenNodes[Number(text) - 1];
                    if (chNode) {
                        $("#" + chNode.tId + "_a").click();
                    }
                } else
                    alert("超过总页数");
            }
        }
       
    </script>
    <script type="text/javascript">

        window.onload = function () {
            var ah = $(window).height();
            $("#fileShowDemo").height(ah - 65); //40
            $("#treeDemo").height(ah - 46); //12
            //console.log(ah);
        }
        window.onresize = function () {
            var ah = $(window).height();
            $("#fileShowDemo").height(ah - 65);
            $("#treeDemo").height(ah - 46);
            //console.log(ah);
        }
    </script>
</head>
<body>
    <table id="size" border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin: 0px;
        padding: 0px;">
        <tr>
            <td style="padding: 5px 20px;" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style=" width:90px;">
                            <div id="btn_tg">
                                审核通过</div>
                        </td>
                        <td>
                            <div id="btn_btg">
                                审核不通过</div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <ul id="treeDemo" class="ztree">
                </ul>
            </td>
            <td style="width: 80%;">
                <iframe id="fileShowDemo" name="foot" marginwidth="0" marginheight="0" src="" frameborder="0"
                    scrolling="no" width="100%" height="100%"></iframe>
                <div style="padding-top: 10px; display: table; height: 30px; width: 100%;">
                    <div style="float: left; width: 49%;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 跳转到 <a id="btn_page" data-json=""
                            onclick="func_page(this)" href="javascript:"></a>
                        <input id="txt_pageIndex" type="text" style="width: 30px;" name="name" value="" />页
                    </div>
                    <div style="float: right; width: 49%; text-align: right;">
                        <a id="btn_pre" data-json="" href="javascript:" onclick="func_pre(this)">【<】上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a id="btn_next" href="javascript:" data-json="" onclick="func_next(this)">下一页【>】</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
