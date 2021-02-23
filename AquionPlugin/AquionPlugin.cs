using System;
using PluginInterface;

namespace AquionPlugin
{
    public class AquionPlugin : IPlugin
    {
        public AquionPlugin()
        {

        }
        IPluginHost myHost = null;
        string myName = "Aquion plugin";
        string myDescription = "Description of plugin";
        string myAuthor = "Vojtěch Pavlas";
        string myVersion = "1.0.0";
		System.Windows.Controls.Control myMainInterface = new ctmMain();
		
		public string Description
		{
			get { return myDescription; }
		}
		public string Author
		{
			get { return myAuthor; }
		}

		public string Name
		{
			get { return myName; }
		}

		public System.Windows.Controls.Control MainInterface
		{
			get { return myMainInterface; }
		}
		public string Version
		{
			get { return myVersion; }
		}


		public void Initialize()
		{
			//This is the first Function called by the host...
			//Put anything needed to start with here first
		}

		public void Dispose()
		{
			//Put any cleanup code in here for when the program is stopped
		}
		/// <summary>
		/// Host of the plugin.
		/// </summary>
		public IPluginHost Host
		{
			get { return myHost; }
			set { myHost = value; }
		}
	}
}
