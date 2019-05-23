using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Loader.Core
{
    /*
        Would like to note this HWID class in basically straight from stackoverflow.
        A HWID should be made way more strict than this. For example including stuff like
        videocard, RAM, mac adress etc.
    */


    class HWID
    {
        private static string GetHash(string value)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            return GetHexString(sec.ComputeHash(bytes));
        }

        private static string GetHexString(IList<byte> bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Count; i++)
            {
                byte b = bt[i];
                int n = b;
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(CultureInfo.InvariantCulture);
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'B')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(CultureInfo.InvariantCulture);
            }
            return s;
        }

        public string Get()
        {

            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();

            return GetHash(cpuInfo + volumeSerial);
        }
    }
}
