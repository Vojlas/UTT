using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    class LicenseResponseModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public string info { get; set; }
        public string pc_uid { get; set; }
        public DateTime LicenseFrom { get; set; }
        public DateTime LicenseTo { get; set; }
        public int userID { get; set; }

        LicenseResponseModel() { 
        
        }
    }
}
