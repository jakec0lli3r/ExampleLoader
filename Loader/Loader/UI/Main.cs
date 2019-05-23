using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using CefSharp;
using CefSharp.WinForms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loader.UI
{
    public partial class Main : Form
    {
        #region DLLImports
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        // Create the Chromium browser that we'll be using.
        ChromiumWebBrowser mainBrowser;

        /// <summary>
        /// Initializing the browser used for displaying the UI.
        /// Using a CefSharp Chromium browser.
        /// </summary>
        public void InitializeBrowser()
        {
            // Enable LegacyJavascriptBindings to allow us access to custom objects.
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
        
            // Create an initialize CefSettings.
            CefSettings loginBrowserSettings = new CefSettings();
            Cef.Initialize(loginBrowserSettings);

            // Set the path to the correct html file.
            mainBrowser = new ChromiumWebBrowser(String.Format("file:///{0}/content/index.html", Environment.CurrentDirectory)); 

            // Dock the Chromium browser to the form.
            this.Controls.Add(mainBrowser);
            mainBrowser.Dock = DockStyle.Fill;

            // Enable various settings to allow access to custom objects.
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            browserSettings.Javascript = CefState.Enabled;

            // Assign the settings to the correct browser.
            mainBrowser.BrowserSettings = browserSettings;

            // Update the browser.
            mainBrowser.Update();  
        }

        /// <summary>
        /// A short function for setting up form bounds & style.
        /// </summary>
        public void SetupForm()
        {
            // Set the size of the form.
            this.Size = new Size(500, 650);

            // Change the form's borderstyle to none.
            this.FormBorderStyle = FormBorderStyle.None;

            // Set the default form starting position.
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Onload function
        /// </summary>
        public Main()
        {
            InitializeComponent();

            // Initialize the Chromium browser by calling the InitializeBrowser function.
            InitializeBrowser();

            // Set up form size & borders by calling the SetupForm function.
            SetupForm();

            // Register the objects for handling user interactions via javascript.
            mainBrowser.RegisterJsObject("mainObject", new Core.Objects.MainJsObject(mainBrowser, this));
            mainBrowser.RegisterJsObject("loginObject", new Core.Objects.LoginJsObject(mainBrowser, this));
            mainBrowser.RegisterJsObject("selectorObject", new Core.Objects.SelectorJsObject(mainBrowser, this));
        }

        /// <summary>
        /// A mouse down event handler, used for dragging the form via the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Rectangle rct = DisplayRectangle;
                if (rct.Contains(e.Location))
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }
    }
}
