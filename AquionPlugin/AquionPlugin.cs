using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;

namespace AquionPlugin
{
    public class AquionPlugin : PluginInterface.PluginInterface
    {
        public string modulName()
        {
            return "Aquion Plugin v. 1.0";
        }

        public void run()
        {
            MessageBox.Show("TODO:::");
        }
    }
}
