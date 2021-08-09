//#define DEBUG
#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;
using System.Net;
using System.IO;

using UniversalTimerTool.Model;
using Newtonsoft.Json;
using System.Windows.Markup;
using System.Windows;
using UniversalTimerTool.View;
using System.Text.RegularExpressions;

namespace UniversalTimerTool.CryptoController
{
    class LicenseController
    {
        private string siteURL = "https://localhost/";

        public void AuthetificateUser()
        {
            if (this.CheckForInternetConnection()) {
                string tmp = getTmpToken();
                Token token = new Token();
                
                if (!CheckLicense(tmp))
                {
                    Environment.Exit(0);
                }
                return;
            }
            MessageBox.Show("You must be connected to the Internet to proceed");
            Environment.Exit(0);
        }

        private bool CheckLicense(string tmpToken)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(siteURL + "API/CheckLicence/" + tmpToken);
            }
            return htmlCode == "true" ? true : false;
        }

        private string getTmpToken()
        {
            string args = hideIP(getPublicIP()) + "x" + getHardDiskUUID();
            string url = siteURL + "API/login/" + args;
            System.Diagnostics.Process.Start(url);
            LoginView loginConfirmation = new LoginView();
            if (loginConfirmation.ShowDialog() == true) {
                return loginConfirmation.tmpToken;
            }
            MessageBox.Show("Wrong token!");
            Environment.Exit(0);
            return null;
        }

        private string hideIP(string ip) {
            Regex regex = new Regex(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$");
            byte[] RandomDigits = { 2, 4, 0, 2, 4, 2, 0, 1, 4, 5, 5, 0, 8, 5, 9, 0 };
            var chars = "ABCDEFGHIJKLNOPQRSTUVWXYZbcdeghijklmnopqrstuvwyz0123456789-";
            string output = "";

            if (regex.IsMatch(ip)) {
                string[] partIp = ip.Split('.');
                string stripIp = partIp[0] + 'M' + partIp[1] + 'f' + partIp[2] + 'a' + partIp[3];
                
                Random random = new Random();
                for (int i = 0; i < stripIp.Length; i++)
                {
                    output += stripIp[i];
                    for (int y = 0; y < RandomDigits[i]; y++)
                    {
                        output += chars[random.Next(0,chars.Length)];
                    }
                }
                return output;
            }
            throw new FormatException("Wrong ip format");
        }

        private string getHardDiskUUID()
        {
            string hddID = null;
            ManagementClass mc = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject strt in moc)
            {
                hddID += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return hddID.Trim();
        }

        public string getPublicIP()
        {
            return new WebClient().DownloadString("http://icanhazip.com").Trim();            
        }

        private bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
