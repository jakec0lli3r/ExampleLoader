$(".launchButton").click(function() 
{
    // Get & store the selected option
    var selectedOption = $("#selectionBox select option:selected").val();

    // Store the checkstate of the checkbox.
    var checkState = $("#saveSelectionSwitch input[type='checkbox']").is(':checked');

    // Handle the launch via the handleLaunch method.
    selectorObject.handleLaunch(selectedOption, checkState);
});

/* 
	Remember selection
*/

// Store the json formatted result in a var.
var selectionObject = JSON.parse(selectorObject.handleRememberSelection());

// Put the saved rememberme value of username in the username textbox.
$("#selectionBox select").val(selectionObject.selectedOption);
// Put the saved rememberme value of password in the username textbox.
$("#saveSelectionSwitch input").prop("checked", selectionObject.checkState);