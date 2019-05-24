<?php 
/**
 * This is the where all data for a login request is collected.
 * Here we are also executing the functions needed to do this.
 * 
 * @author Jake
 * @copyright 2019 N/A
*/

include "core/login.class.php";

// Create a new instance of the login class.
$api = new LoginClass();

// Get the credentials we need, and escape them.
$escapedUsername = $api->EscapeString($connection, $_POST['username']);
$escapedPassword = $api->EscapeString($connection, $_POST['password']);
$escapedHWID = $api->EscapeString($connection, $_POST['hwid']);

// Handle the login, and echo the result.
echo($api->HandleLogin($escapedUsername, $escapedPassword, $escapedHWID, $connection));