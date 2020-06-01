using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    class LicenseCheckRequestModel
    {
        public int userID { get; set; }
        public string PC_UID { get; set; }


        public LicenseCheckRequestModel(int userID, string PC_UID)
        {
            this.userID = userID;
            this.PC_UID = PC_UID;
        }
    }
}
