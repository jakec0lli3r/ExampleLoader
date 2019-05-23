using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Loader.Core.Authentication;

namespace Loader.Core.Objects
{
    class LoginJsObject
    {
        private static ChromiumWebBrowser _instanceBrowser = null;
        private static UI.Main _instanceMainForm = null;

        private Core.Authentication.Login loginAuthenticationHelper = new Core.Authentication.Login();
        private Core.HWID hardwareHelper = new Core.HWID();

        public LoginJsObject(ChromiumWebBrowser originalBrowser, UI.Main mainForm)
        {
            _instanceBrowser = originalBrowser;
            _instanceMainForm = mainForm;
        }

        /// <summary>
        /// A method for handling a login request.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="checkState">The user's checkstate.</param>
        /// <returns>
        /// Returns a result string based on the login check.
        /// </returns>
        public string handleLogin(string username, string password, bool checkState)
        {
            // Check if the user checked the remember me box.
            if (checkState)
            {
                // Store the user's username & password.
                Properties.Settings.Default.usernameVal = username;
                Properties.Settings.Default.passwordVal = password;

                // If the checkstate was not stored as true before, set it to true.
                if (!Properties.Settings.Default.loginCheckState)
                {
                    Properties.Settings.Default.loginCheckState = true;
                }

                // Save all the changes to the application settings.
                Properties.Settings.Default.Save();
            }
            // If the user did not check the remember me box.
            else
            {
                // Store the checkstate, username & password.
                Properties.Settings.Default.loginCheckState = false;
                Properties.Settings.Default.usernameVal = string.Empty;
                Properties.Settings.Default.passwordVal = string.Empty;

                // Save all the changes to the application settings.
                Properties.Settings.Default.Save();
            }

            // Preform a login check with the user's credentials.
            switch(loginAuthenticationHelper.doLoginCheck(username, password, hardwareHelper.Get()))
            {
                // If the login check returned a successfull login result.
                case Results.LoginResults.LoginSuccess:
                    // Return a string that we can read in our javascript.
                    return "success";

                // If the login check returned a hwid mismatch.
                case Results.LoginResults.HwidFailed:
                    // Return a string that we can read in our javascript.
                    return "hwid_failed";

                // If the login check returned a failed login result.
                case Results.LoginResults.LoginFailed:
                    // Return a string that we can read in our javascript.
                    return "failed";

                // If the login check returned a unknown error result.
                case Results.LoginResults.UnknownError:
                    // Return a string that we can read in our javascript.
                    return "failed";
            }

            // Default return value
            return "failed";
        }


        /// <summary>
        /// A method for easily passing remember me information to our javascript.
        /// By converting the information to a JSON object.
        /// </summary>
        /// <returns>
        /// Returns a JSON object containing remember me information.
        /// </returns>
        public string handleRememberMe()
        {
            // Check the stored checkstate.
            if (Properties.Settings.Default.loginCheckState)
            {
                // Create a new instance of the user object.
                JSON.User currentUser = new JSON.User();

                // Set the values
                currentUser.username = Properties.Settings.Default.usernameVal;
                currentUser.password = Properties.Settings.Default.passwordVal;
                currentUser.checkState = Properties.Settings.Default.loginCheckState;

                // Convert the object to a JSON object & return it.
                return JsonConvert.SerializeObject(currentUser).ToString();
            }

            // Default return value
            return "error";
        }
    }
}
