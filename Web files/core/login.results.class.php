<?php 
/**
 * This is the login result class
 * 
 * This can be used to easily modify the login results
 * returned by the login class/API
 * 
 * @author Jake
 * @copyright 2019 N/A
*/

class ResultClass
{   
    /**
     * @var string The result returned on a failed login attempt.
    */
    public $loginFailed = "login_failed";

    /**
     * @var string The result returned on a successfull login attempt.
    */ 
    public $loginSuccess = "login_success";

    /**
     * @var string The result returned on a hwid mismatch.
    */  
    public $hwidFailed = "hwid_failed";
}