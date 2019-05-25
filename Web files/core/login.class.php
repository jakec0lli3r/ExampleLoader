<?php
/**
 * This is the login class
 * 
 * This is used for handling login requests
 * requested from anywhere outside this class
 * 
 * @author Jake
 * @copyright 2019 N/A
*/

include "core/login.connect.php";
include "core/login.results.class.php";

class LoginClass
{ 
    /**
     * Function for handling a user's login
     * 
     * @var string $username The user's escaped username
     * @var string $password The user's escaped password
     * @var string $hwid The user's escaped hardware-id
     * @var mysqli $conn The mysqli connection object
    */
    public function HandleLogin($username, $password, $hwid, $conn) 
    {
        // Create a new instance of the result class.
        $results = new ResultClass();

        // Variable check (simply checking if all variables are set)
        if(empty($username) || empty($password) || empty($hwid))
        {
            // Return a failed login result.
            return $results->loginFailed;
        }

        // Execute the user check query.
        $checkQuery = $conn->query("SELECT * FROM user WHERE username='$username' AND password='$password'");

        // Check if the user exists
        if($checkQuery->num_rows < 1)
        {
            // Return a failed login result.
            return $results->loginFailed;
        }

        // Get the array from the query result
        $resultArray = $checkQuery->fetch_array();

        // Check if the user's hwid matches
        if($resultArray["hwid"] != $hwid)
        {
            // If the user's hwid is not empty
            if($resultArray["hwid"] != "")
            {
                // Return a hwid mismatch result.
                return $results->hwidFailed;
            }
            // If the user's hwid is empty.
            else
            {
                // Update it to the new one.
                $conn->query("UPDATE user SET hwid='$hwid' WHERE username='$username'");

                // Return a successfull login result.
                return $results->loginSuccess;
            }
        }

        // If all above checks passed, return a successfull login result.
        return $results->loginSuccess;
    }

    /**
     * Function for escaping a string
     * 
     * @var mysqli $conn The mysqli connection object
     * @var string $str The string to be escaped
    */
    public function EscapeString($conn, $str)
    {
        // Return the escaped version of the string
        return addcslashes(mysqli_real_escape_string($conn, $str), '%_');
    }
}