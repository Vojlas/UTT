using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using PluginInterface;
using System.IO;
using System.Text.RegularExpressions;

namespace UniversalTimerTool.Controller
{
	class PluginController : IPluginHost
	{
		private Types.AvailablePlugins colAvailablePlugins = new Types.AvailablePlugins();
		public PluginController()
		{

		}
		public Types.AvailablePlugins AvailablePlugins
		{
			get { return colAvailablePlugins; }
			set { colAvailablePlugins = value; }
		}
		public void FindPlugins()
		{
			FindPlugins(AppDomain.CurrentDomain.BaseDirectory);
		}
		public void FindPlugins(string Path)
		{
			Directory.CreateDirectory(Path);
			//First empty the collection, we're reloading them all
			colAvailablePlugins.Clear();

			//Go through all the files in the plugin directory
			foreach (string fileOn in Directory.GetFiles(Path))
			{
				FileInfo file = new FileInfo(fileOn);

				//Preliminary check, must be .dll
				if (file.Extension.Equals(".dll"))
				{
					//Add the 'plugin'
					this.AddPlugin(fileOn);
				}
			}
		}

		private void AddPlugin(string FileName)
		{
			//Create a new assembly from the plugin file we're adding..
			Assembly pluginAssembly = Assembly.LoadFrom(FileName);

			//Next we'll loop through all the Types found in the assembly
			foreach (Type pluginType in pluginAssembly.GetTypes()) // ----------------------------------->>>>>>>>> TRY CATCH!!!
			{
				if (pluginType.IsPublic) //Only look at public types
				{
					if (!pluginType.IsAbstract)  //Only look at non-abstract types
					{
						//Gets a type object of the interface we need the plugins to match
						Type typeInterface = pluginType.GetInterface("PluginInterface.IPlugin", true);

						//Make sure the interface we want to use actually exists
						if (typeInterface != null)
						{
							//Create a new available plugin since the type implements the IPlugin interface
							Types.AvailablePlugin newPlugin = new Types.AvailablePlugin();

							//Set the filename where we found it
							newPlugin.AssemblyPath = FileName;

							//Create a new instance and store the instance in the collection for later use
							//We could change this later on to not load an instance.. we have 2 options
							//1- Make one instance, and use it whenever we need it.. it's always there
							//2- Don't make an instance, and instead make an instance whenever we use it, then close it
							//For now we'll just make an instance of all the plugins
							newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

							//Set the Plugin's host to this class which inherited IPluginHost
							newPlugin.Instance.Host = this;

							//Call the initialization sub of the plugin
							newPlugin.Instance.Initialize();

							//Add the new plugin to our collection here
							this.colAvailablePlugins.Add(newPlugin);

							//cleanup a bit
							newPlugin = null;
						}

						typeInterface = null; //Mr. Clean			
					}
				}
			}

			pluginAssembly = null; //more cleanup
		}

		public void ClosePlugins()
		{
			foreach (Types.AvailablePlugin pluginOn in colAvailablePlugins)
			{
				//Close all plugin instances
				//We call the plugins Dispose sub first incase it has to do 
				//Its own cleanup stuff
				pluginOn.Instance.Dispose();

				//After we give the plugin a chance to tidy up, get rid of it
				pluginOn.Instance = null;
			}

			//Finally, clear our collection of available plugins
			colAvailablePlugins.Clear();
		}

		public void Feedback(string Feedback, IPlugin Plugin)
		{
			//This sub makes a new feedback form and fills it out
			//With the appropriate information
			//This method can be called from the actual plugin with its Host Property

			//System.Windows.Forms.Form newForm = null;
			//frmFeedback newFeedbackForm = new frmFeedback();

			////Here we set the frmFeedback's properties that i made custom
			//newFeedbackForm.PluginAuthor = "By: " + Plugin.Author;
			//newFeedbackForm.PluginDesc = Plugin.Description;
			//newFeedbackForm.PluginName = Plugin.Name;
			//newFeedbackForm.PluginVersion = Plugin.Version;
			//newFeedbackForm.Feedback = Feedback;

			//newForm = newFeedbackForm;
			//newForm.ShowDialog();

			//newFeedbackForm = null;
			//newForm = null;

		}
	}

	namespace Types
	{
		public class AvailablePlugins : System.Collections.CollectionBase
		{
			public void Add(Types.AvailablePlugin pluginToAdd)
			{
				this.List.Add(pluginToAdd);
			}
			public void Remove(Types.AvailablePlugin pluginToRemove)
			{
				this.List.Remove(pluginToRemove);
			}
			public Types.AvailablePlugin Find(string pluginNameOrPath)
			{
				Types.AvailablePlugin toReturn = null;

				foreach (Types.AvailablePlugin pluginOn in this.List)
				{
					if ((pluginOn.Instance.Name.Equals(pluginNameOrPath)) || pluginOn.AssemblyPath.Equals(pluginNameOrPath))
					{
						toReturn = pluginOn;
						break;
					}
				}
				return toReturn;
			}
		}

		public class AvailablePlugin
		{
			private IPlugin myInstance = null;
			private string myAssemblyPath = "";

			public IPlugin Instance
			{
				get { return myInstance; }
				set { myInstance = value; }
			}
			public string AssemblyPath
			{
				get { return myAssemblyPath; }
				set { myAssemblyPath = value; }
			}
		}
	}
}
