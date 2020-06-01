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

        List<Project> projects = new List<Project>();
        public int ProjectNumber { get; private set; }
        public int UpdateNumber { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            SetupComponents();

            LoginView loginView = new LoginView();
            loginView.ShowDialog();
            LicenseController lc = new LicenseController();
            LoginResponseModel login = lc.login(loginView.Email, loginView.Password, true);
            if (!lc.checkLicense(loginView.Email, login.token))
            {
                MessageBox.Show("Nevlastníte platnou licenci!");
                Environment.Exit(0);
            }



            FilesController.FilesController fs = new FilesController.FilesController();
            this.projects = fs.Loadprojects();
            HomeView hm = new HomeView(this);
            try
            {
                hm.ReRenderHomePage(this.projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
            }
            catch (Exception)
            {
            }
            

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
    }
}