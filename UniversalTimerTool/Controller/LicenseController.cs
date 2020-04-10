using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace UniversalTimerTool.Controller
{
    class LicenseController
    {
        public LicenseController()
        {
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

        public void CheckRequest()
        {
            if (getHTTP("http://f4vopa.wz.cz/esence.php?esence=" + getHardDiskUUID(), "essense") == "")
            {
                System.Windows.Forms.MessageBox.Show("Bohužel nevlastníte licenci tohoto softwaru\npožádejte autora o licenci:\n"+getHardDiskUUID());
                Environment.Exit(0);
            }
        }

        private string getHTTP(string uri, string divName)
        { 
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(uri);
            HtmlNode rateNode = doc.DocumentNode.SelectSingleNode("//div[@id='"+divName+"']");
            return rateNode.InnerText;
        }

    }
}
