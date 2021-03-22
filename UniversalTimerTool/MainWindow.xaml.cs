using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using UniversalTimerTool.DataController;
using UniversalTimerTool.FilesController;
using UniversalTimerTool.Model;
using UniversalTimerTool.View;
using UniversalTimerTool.CryptoController;
using System.Diagnostics;
using UniversalTimerTool.Controller;


namespace UniversalTimerTool
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool work { get; set; }
        private System.Windows.Threading.DispatcherTimer dispatcherTimerProject = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer dispatcherTimerAutosave = new System.Windows.Threading.DispatcherTimer();
        private PluginController pc { get; set; }
        List<Project> projects = new List<Project>();
        public int ProjectNumber { get; private set; }
        public int UpdateNumber { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            SetupComponents();

            //TRY CATCH -> INTERNET
            //LoginView loginView = new LoginView();
            //loginView.ShowDialog();
            LicenseController lc = new LicenseController();
            //LoginResponseModel login = lc.login(loginView.Email, loginView.Password, true);
            //if (!lc.checkLicense(loginView.Email, login.token))
            //{
            //    MessageBox.Show("Nevlastníte platnou licenci!");
            //    Environment.Exit(0);
            //}

            //TESTTESTTEST
                //Call the find plugins routine, to search in our Plugins Folder
                this.pc = new PluginController();

                pc.FindPlugins(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//UTT-TimerTool//Addons");

                //Add each plugin to the treeview
                foreach (Controller.Types.AvailablePlugin pluginOn in pc.AvailablePlugins)
                {
                //System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode(pluginOn.Instance.Name);
                    this.ListViewAddons.Items.Add(pluginOn.Instance.Name);
                  //  newNode = null;
                }
            //TESTTESTTEST

            FilesController.FilesController fs = new FilesController.FilesController();
            this.projects = fs.Loadprojects();
            HomeView hm = new HomeView(this);
            try
            {
                hm.ReRenderHomePage(this.projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
            }
            catch (Exception){}            

            if (this.projects.Count == 0) { ManageMainButtons(false); }
            else { ManageMainButtons(true); }

            dataGridViewProjects.ItemsSource = this.projects;            
        }
        private void dispatcherTimerProject_Tick(object sender, EventArgs e)
        {
            try
            {
                projects.ElementAt(this.ProjectNumber).AddSeconds(this.UpdateNumber, 1, work);
                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(this.projects.ElementAt(this.ProjectNumber), this.UpdateNumber,this.ProjectNumber, this.projects.Count);
                MoneyManagerController mc = new MoneyManagerController(this);
                mc.ReCalculateMoney(this.UpdateNumber, projects.ElementAt(ProjectNumber).Updates);
            }
            catch (Exception)
            {
            }
        }

        private void dispatcherTimerAutosave_Tick(object sender, EventArgs e)
        {
            FilesController.FilesController fc = new FilesController.FilesController();
            foreach (Project project in this.projects)
            {
                fc.WriteProjecToProjectFolder(project);
            }
        }

        private void stopTimer()
        {
            dispatcherTimerProject.Stop();
            buttonMain_Stop.IsEnabled = false;
            buttonMain_StartWork.IsEnabled = true;
            buttonMain_StartTrain.IsEnabled = true;
        }

        //------------------------------------------------------------------------------
        //Project tab   
             
        private void checkboxToday_Changed(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.ToString() == "True")
            {
                datePickerCreated.SelectedDate = DateTime.Today;
                datePickerCreated.IsEnabled = false;
            }
            else
            {
                datePickerCreated.SelectedDate = null;
                datePickerCreated.IsEnabled = true;
            }
            
        }

        private void checkBoxDefaultUpdate_Changed(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.ToString() == "True")
            {

                textBoxUpdateName.Text = "Init";
            }
            else
            {
                textBoxUpdateName.Text = "";
            }
            
        }

        private void buttonViewDatagridProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = (Project)((Button)e.Source).DataContext;
                ProjectsView projectsView = new ProjectsView(project);
                projectsView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ListViewAddons_Selected(object sender, RoutedEventArgs e)
        {
            //Make sure there's a selected Plugin
            if (this.ListViewAddons.SelectedItem != null)
            {
                //Get the selected Plugin
                Controller.Types.AvailablePlugin selectedPlugin = pc.AvailablePlugins.Find(ListViewAddons.SelectedValue.ToString());

                if (selectedPlugin != null)
                {
                    //Clear the current panel of any other plugin controls... 
                    //Note: this only affects visuals.. doesn't close the instance of the plugin


                    //Set the dockstyle of the plugin to fill, to fill up the space provided
                    //selectedPlugin.Instance.MainInterface.Dock = System.Windows.Forms.DockStyle.Fill;

                    //Finally, add the usercontrol to the panel... Tadah!
                    this.pnlAddonScreen.Content = selectedPlugin.Instance.MainInterface;
                    //this.pnlAddonScreen.Children.Add(selectedPlugin.Instance.MainInterface);

                }
            }
        }

        private void buttonMain_ShowToDoList_Click(object sender, RoutedEventArgs e)
        {
            if (projects.Count !=0)
            {
                ToDoView view = new ToDoView(this.projects.ElementAt(ProjectNumber).Updates.ElementAt(UpdateNumber).ToDoList);
                view.Show(); 
            }
        }
    }
}