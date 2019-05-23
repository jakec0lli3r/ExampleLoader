/*
	Animation stuff
*/

$(document).ready(function() 
{
	animationObject.loginShow();
	//animationObject.selectorDebug();
});

/* 
	User interaction
*/

// Onclick event for the login button
$(".loginButton").click(function() 
{
	// Store the value of the username textbox in a var.
	var usr = $("#username").val();
	// Store the value of the password textbox in a var.
	var pass = $("#password").val();
	// Store the checkstate of the checkbox in a var.
	var checkState = $("input[type='checkbox']").is(':checked');;

	// Handle the login via the handleLogin method, and store the result.
	var loginResult = loginObject.handleLogin(usr, pass, checkState);

	// Check the returned result.
	switch(loginResult)
	{
		// If the login was a success.
		case "success":
			// Hide the login page, and show the selector page.
			animationObject.loginHide();
			break;

		// If the login failed.
		case "failed":
			break;

		// If there was an inknown error.
		case "unknown":
			break;
	}
});

/* 
	Remember me
*/

// Store the json formatted result in a var.
var userObject = JSON.parse(loginObject.handleRememberMe());

// Put the saved rememberme value of username in the username textbox.
$("#username").val(userObject.username);
// Put the saved rememberme value of password in the username textbox.
$("#password").val(userObject.password);
// Change the checkstate of the checkbox to the saved on.
$("input[type='checkbox']").prop("checked", userObject.checkState);
