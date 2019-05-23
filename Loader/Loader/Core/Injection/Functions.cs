using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Loader.Core.Injection
{
    class Functions
    {
        // Webclient used for downloading dll bytes.
        private static WebClient wc = new WebClient();

        /// <summary>
        /// This method will return the process id of the provided process name
        /// </summary>
        /// <param name="processName">Name of the process</param>
        /// <returns>The process id of the provided process name</returns>
        private static int getProcessId(string processName)
        {
            Process targetProcess = Process.GetProcessesByName(processName).FirstOrDefault();
            return targetProcess.Id;         
        }

        /// <summary>
        /// Method for doing an example injection.
        /// </summary>
        public static void preformInjection()
        {
            byte[] dllByteArray = null;
            Bleak.Injector injector;

            try
            {
                dllByteArray = wc.DownloadData(Constants.dllUrl);
            }
            catch { MessageBox.Show("There was an error while downloading the dll."); }

            try
            {
                injector = new Bleak.Injector(Bleak.InjectionMethod.ManualMap, getProcessId("csgo"), dllByteArray);
            }
            catch { MessageBox.Show("There was an error during the injection process."); }

            Application.Exit();
        }
    }
}
