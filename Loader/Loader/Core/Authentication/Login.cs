using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Loader.Core.Authentication
{
    class Login
    {
        // The webclient used for post requests.
        private WebClient wc = new WebClient();

        /// <summary>
        /// A method for checking a user login.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="hwid">The user's unique hardware id.</param>
        /// <returns>
        /// Returns a result based on the provided credentials.
        /// </returns>
        public Results.LoginResults doLoginCheck(string username, string password, string hwid)
        {
            // The variable that will store our post data.
            var postData = new NameValueCollection();

            // Setting the post data.
            postData["username"] = username;
            postData["password"] = password;
            postData["hwid"] = hwid;

            // Send the request, and store the response.
            var response = wc.UploadValues(Constants.apiUrl, "POST", postData);

            // Switch the result (and convert it).
            switch (Encoding.UTF8.GetString(response))
            {
                // If the api returned login_failed.
                case "login_failed":
                    return Results.LoginResults.LoginFailed;

                // If the api returned login_success.
                case "login_success":
                    return Results.LoginResults.LoginSuccess;

                // If the api returned hwid_failed.
                case "hwid_failed":
                    return Results.LoginResults.HwidFailed;
            }

            // Default return value
            return Results.LoginResults.UnknownError;
        }
    }
}
