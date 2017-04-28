// JavaScript Document
document.body.addEventListener('touchstart', function(){ });

$(document).ready(function () {
   $("#xiangxi").tap(function(){
       $(this).hide();
       $(this).parent().children(".xinxi_x").removeClass("xx");
       $("#shouqi").show();
   });
    $("#shouqi").tap(function(){
        $(this).hide();
        $(this).parent().children(".xinxi_x").addClass("xx");
        $("#xiangxi").show();
    });
});