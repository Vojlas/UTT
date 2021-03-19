using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    public class ToDo
    { 
        public bool Done { get; set; }
        public string Name { get; set; }

        public ToDo()
        {

        }
        public ToDo(string _name, bool _done = false) {
            this.Done = _done;
            this.Name = _name;
        }
    }
}
