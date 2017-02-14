/// <reference path="../tools/jquery/jquery-1.9.1.js" />

$(function () {
    var objs = $(".larg-iconbutton");
    $(objs).each(
        function () {
            var img = "url('/images/icons/" + $(this).attr('icon') + ".png')";
            $(this).css("background-image", img);
        });

        //$('#floater').css("height", "300");
        //$('#floater').css("height", "50%");
    });

    