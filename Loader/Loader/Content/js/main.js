/* 
    Toolbar button click handlers
*/

$(".minimize").click(function()
{
    mainObject.handleMinimize();
});

$(".close").click(function()
{
    mainObject.handleClose();
});

/* 
    Extra 
*/

$(document).ready(function() 
{
    $("#selectionContainer *").hide();
});
