using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalTimerTool.CryptoController;

namespace UniversalTimerTool.Model
{
    class LoginModel
    {
        public string Username { get; private set; }
        public string Pass { get; private set; }


        public LoginModel(string username, string pass, bool usingHash)
        {
            if (!usingHash)
            {
                CryptoController.CryptoController cp = new CryptoController.CryptoController();
                string salt = cp.ComputeSha256Hash(username);
                this.Pass = cp.ComputeSha256Hash(salt + pass + salt);
            }
            else
            {
                this.Pass = pass;
            }
            this.Username = username;
        }
    }
}
