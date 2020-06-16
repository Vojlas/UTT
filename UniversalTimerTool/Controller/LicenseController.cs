#define DEBUG
//#undef DEBUG
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

namespace UniversalTimerTool.CryptoController
{
    class LicenseController
    {
#if DEBUG
        private string loginURL = "http://localhost/API/login?debug=true";
        private string licenseCheckURL = "http://localhost/API/license/check?debug=true";
#else
        private string loginURL = "http://F4vopa.wz.cz/API/login";
        private string licenseCheckURL = "http://F4vopa.wz.cz/API/license/check";
        
#endif
        public LicenseController()
        {
#if DEBUG
            Console.WriteLine("LicenceController.cs \t DEBUG = TRUE");
#endif
        }

        public LoginResponseModel login(string username, string pass, bool usingHash)
        {
            LoginModel lm = new LoginModel(username, pass, usingHash);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.loginURL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) //Error Cant connect to server && No internet
            {
                string json = "{\"email\":\"" + lm.Username + "\"," +
                              "\"pass\":\"" + lm.Pass + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //Error Http - 401
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var tmp = streamReader.ReadToEnd();
                LoginResponseModel response = JsonConvert.DeserializeObject<LoginResponseModel>(tmp);
                if (response.status == "200")
                {
                    return response;
                }
            }
            return null;
        }

        public bool checkLicense(string email, string token) {
            string pc_uid = this.getHardDiskUUID();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.licenseCheckURL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) //Error Cant connect to server && No internet
            {
                string json = "{\"email\":\"" + email + "\"," +
                              "\"token\":\"" + token + "\","+
                              "\"PC_UID\":\"" + pc_uid + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //Error Http - 401
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var tmp = streamReader.ReadToEnd();
                LicenseResponseModel response = null;
                try
                {
                    response = JsonConvert.DeserializeObject<LicenseResponseModel>(tmp);
                }
                catch (Exception)
                {
                    return false;
                }
                if (response.LicenseTo > DateTime.Now)
                {
                    return true;
                }
            }

            return false;
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

    }
}
