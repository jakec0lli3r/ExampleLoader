using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader.Core.Authentication
{
    class Results
    {
        /// <summary>
        /// The results that can be returned by a login check.
        /// </summary>
        public enum LoginResults
        {
            LoginSuccess,
            LoginFailed,
            HwidFailed,
            UnknownError
        }
    }
}
