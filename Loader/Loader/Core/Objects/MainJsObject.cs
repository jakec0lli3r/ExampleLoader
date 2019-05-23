using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Loader.Core.Objects
{
    class MainJsObject
    {
        private static ChromiumWebBrowser _instanceBrowser = null;
        private static UI.Main _instanceMainForm = null;

        public MainJsObject(ChromiumWebBrowser originalBrowser, UI.Main mainForm)
        {
            _instanceBrowser = originalBrowser;
            _instanceMainForm = mainForm;
        }

        /// <summary>
        /// Method for closing the application.
        /// </summary>
        public void handleClose()
        {
            Application.Exit();
        }

        /// <summary>
        /// Method for minimizing the application.
        /// </summary>
        public void handleMinimize()
        {
            
        }
    }
}
