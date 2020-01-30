using System;
using System.Collections.Generic;
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

        List<Project> project = new List<Project>();
        public int ProjectNumber { get; private set; }
        public int UpdateNumber { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            SetupComponents();
        }

        private void dispatcherTimerProject_Tick(object sender, EventArgs e)
        {
            try
            {
                project.ElementAt(this.ProjectNumber).AddSeconds(0, 1, work);
                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(this.project.ElementAt(this.ProjectNumber), 0);
            }
            catch (Exception)
            {
            }
        }

        private void dispatcherTimerAutosave_Tick(object sender, EventArgs e)
        {
            //TODO: Autosave
            //UniversalTimerTool.View.HomeView homeView = new View.HomeView(this);
            //homeView.ProjectsNull();
        }

        private void stopTimer()
        {
            dispatcherTimerProject.Stop();
            ButtonStop.IsEnabled = false;
            buttonStartWork.IsEnabled = true;
            buttonStartTrain.IsEnabled = true;
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
                datePickerCreated.SelectedDate = DateTime.Today;
                datePickerCreated.IsEnabled = false;
            }
            else
            {
                datePickerCreated.SelectedDate = null;
                datePickerCreated.IsEnabled = true;
            }

        }
    }
}