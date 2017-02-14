<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTemp.aspx.cs" Inherits="WebUI.Pages.Print.PrintTemp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $.fn.extend({
            //---元素拖动插件
            dragging: function (data) {
                var $this = $(this);
                var xPage;
                var yPage;
                var X; //
                var Y; //
                var xRand = 0; //
                var yRand = 0; //
                var father = $("#temp");// $this.parent();
                var defaults = {
                    move: 'both',
                    randomPosition: true,
                    hander: 1
                }
                var opt = $.extend({}, defaults, data);
                var movePosition = opt.move;
                var random = opt.randomPosition;

                var hander = opt.hander;

                if (hander == 1) {
                    hander = $this;
                } else {
                    hander = $this.find(opt.hander);
                }


                //---初始化
                father.css({ "position": "relative", "overflow": "hidden" });
                $this.css({ "position": "absolute" });
                hander.css({ "cursor": "move" });

                var faWidth = father.width();
                var faHeight = father.height();
                var thisWidth = $this.width() + parseInt($this.css('padding-left')) + parseInt($this.css('padding-right'));
                var thisHeight = $this.height() + parseInt($this.css('padding-top')) + parseInt($this.css('padding-bottom'));

                var mDown = false; //
                var positionX;
                var positionY;
                var moveX;
                var moveY;

                if (random) {
                    $thisRandom();
                }
                function $thisRandom() { //随机函数
                    $this.each(function (index) {
                        var randY = parseInt(Math.random() * (faHeight - thisHeight)); ///
                        var randX = parseInt(Math.random() * (faWidth - thisWidth)); ///
                        if (movePosition.toLowerCase() == 'x') {
                            $(this).css({
                                left: randX
                            });
                        } else if (movePosition.toLowerCase() == 'y') {
                            $(this).css({
                                top: randY
                            });
                        } else if (movePosition.toLowerCase() == 'both') {
                            $(this).css({
                                top: randY,
                                left: randX
                            });
                        }

                    });
                }

                hander.mousedown(function (e) {
                    father.children().css({ "zIndex": "0" });
                    $this.css({ "zIndex": "1" });
                    mDown = true;
                    X = e.pageX;
                    Y = e.pageY;
                    positionX = $this.position().left;
                    positionY = $this.position().top;
                    return false;
                });

                $(document).mouseup(function (e) {
                    mDown = false;
                });

                $(document).mousemove(function (e) {
                    xPage = e.pageX; //--
                    moveX = positionX + xPage - X;

                    yPage = e.pageY; //--
                    moveY = positionY + yPage - Y;

                    function thisXMove() { //x轴移动
                        if (mDown == true) {
                            $this.css({ "left": moveX });
                        } else {
                            return;
                        }
                        if (moveX < 0) {
                            $this.css({ "left": "0" });
                        }
                        if (moveX > (faWidth - thisWidth)) {
                            $this.css({ "left": faWidth - thisWidth });
                        }
                        return moveX;
                    }

                    function thisYMove() { //y轴移动
                        if (mDown == true) {
                            $this.css({ "top": moveY });
                        } else {
                            return;
                        }
                        if (moveY < 0) {
                            $this.css({ "top": "0" });
                        }
                        if (moveY > (faHeight - thisHeight)) {
                            $this.css({ "top": faHeight - thisHeight });
                        }
                        return moveY;
                    }

                    function thisAllMove() { //全部移动
                        if (mDown == true) {
                            $this.css({ "left": moveX, "top": moveY });
                        } else {
                            return;
                        }
                        if (moveX < 0) {
                            $this.css({ "left": "0" });
                        }
                        if (moveX > (faWidth - thisWidth)) {
                            $this.css({ "left": faWidth - thisWidth });
                        }

                        if (moveY < 0) {
                            $this.css({ "top": "0" });
                        }
                        if (moveY > (faHeight - thisHeight)) {
                            $this.css({ "top": faHeight - thisHeight });
                        }
                    }
                    if (movePosition.toLowerCase() == "x") {
                        thisXMove();
                    } else if (movePosition.toLowerCase() == "y") {
                        thisYMove();
                    } else if (movePosition.toLowerCase() == 'both') {
                        thisAllMove();
                    }
                });
            }
        }); 
    </script>
    <script>
        $(function () {
            $('.box-1 dl').each(function () {
                $(this).dragging({
                    move: 'x',
                    randomPosition: true
                });
            });
            $('.box-2 dl').each(function () {
                $(this).dragging({
                    move: 'y',
                    randomPosition: true
                });
            });
            $('.box-3 dl').each(function () {
                $(this).dragging({
                    move: 'both',
                    randomPosition: false
                });
            });
            $('.box-4 div dl').each(function () {
                $(this).dragging({
                    move: 'both',
                    hander:1,
                    randomPosition: true
                });
            });
            $('.box-5 dl').each(function () {
                $(this).dragging({
                    move: 'both',
                    randomPosition: true,
                    hander: '.hander'
                });
            });
        });
    </script>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        li
        {
            list-style: none outside none;
        }
        body
        {
            
            font-family: "宋体";
            padding: 50px 0 300px;
        }
        
        .box
        {
            width: 1000px;
            margin: 0 auto 20px;
            height: 500px;
            background: #3bb3e0;
            border: 5px solid #2561b4;
        }
        p
        {
            font-size: 20px;
            color: #333;
            text-align: center;
            margin: 0 0 20px;
        }
        .box.box-1
        {
            height: 200px;
        }
        .box-1 dl
        {
            top: 25px;
        }
        .box.box-2
        {
            height: 500px;
        }
        .box-2 dl
        {
            left: 50%;
            margin-left: -75px;
        }
        dl{ border:1px solid red};
        i.hander
        {
            display: block;
            width: 100%;
            height: 25px;
            background: #ccc;
            text-align: center;
            font-size: 12px;
            color: #333;
            line-height: 25px;
            font-style: normal;
        }
    </style>
</head>
<body>
    <p>
        自动拖动，初始位置随机</p>
    <div class='box box-4'>
        <div id="temp" style=" width:100%; height:200px; background:#00CC99;">
            
        </div>
        <div style=" width:100%; height:200px; background:#99FF33;">
            <dl>
                AAAAAAAAa</dl>
            <dl>
                BBBBBBBB</dl>
            <dl>
                CCCCCCCCC</dl>
            <dl>
                DDDDDDDD</dl>
        </div>
    </div>
</body>
</html>
