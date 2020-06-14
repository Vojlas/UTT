using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using PluginInterface;

namespace UniversalTimerTool.Controller
{
    class PluginController
    {
        public PluginController()
        {
            loadPlugins();
        }

        private void loadPlugins()
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                Assembly a = Assembly.LoadFrom(op.FileName);
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in a.GetTypes())
                    {
                        if (type.GetInterface("PluginInterface") != null)
                        {
                            PluginInterface.PluginInterface modul = Activator.CreateInstance(type) as PluginInterface.PluginInterface;
                            modul.run();
                            return;
                        }
                    }
                }
            }
        }
    }
}
