using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    class LoginResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string token_expiry { get; set; }
        public LoginResponseModel()
        {

        }
    }
}
