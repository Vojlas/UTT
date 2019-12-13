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
            this.work = false; ButtonStop.IsEnabled = false;
            this.ProjectNumber = 0;
            this.UpdateNumber = 0;

            //TimerProject - Initialize
            //System.Windows.Threading.DispatcherTimer dispatcherTimerProject = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerProject.Tick += new EventHandler(dispatcherTimerProject_Tick);
            dispatcherTimerProject.Interval = new TimeSpan(0, 0, 1);

            //TimerAutosava - Initialize
            //System.Windows.Threading.DispatcherTimer dispatcherTimerAutosave = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerAutosave.Tick += new EventHandler(dispatcherTimerAutosave_Tick);
            dispatcherTimerAutosave.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimerAutosave.Start();
            

            //TODO: Language switch || Multilanguage support 
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


        private void buttonStartWork_Click(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimerProject.Start();
            this.work = true;
            ButtonStop.IsEnabled = true;
            buttonStartWork.IsEnabled = false;
            buttonStartTrain.IsEnabled = true;
        }

        private void buttonStartTrain_Click(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimerProject.Start();
            this.work = false;
            ButtonStop.IsEnabled = true;
            buttonStartWork.IsEnabled = true;
            buttonStartTrain.IsEnabled = false;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            stopTimer();
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

        private void buttonLoadNewProject_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Import project
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                // Open document 
                string filename = dlg.FileName;
            }
        }

        private void buttonCreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            groupBoxCreateNewProject.Visibility = Visibility.Visible;
            buttonLoadNewProject.Visibility = Visibility.Hidden;

            List<Update> updates = new List<Update>();
            Update update1 = new Update(new DateTime(63711542), new DateTime(637115331), 255, "init");
            Update update2 = new Update(new DateTime(62151551), new DateTime(256), 250, "init + changes");
            updates.Add(update1);
            updates.Add(update2);

            Project project = new Project("Test project", DateTime.UtcNow, updates, "Obnovení projektu dne: 01.12.2019");

            FilesController.FilesController filesController = new FilesController.FilesController();
            filesController.WriteProjecToProjectFolder(project);

            /*DataController.DataController dataController = new DataController.DataController();

            Project projectEnd;
            projectEnd = dataController.projectFromDataset(filesController.Loadprojects().ElementAt(0));*/

            try
            {
                this.project.Add(project);
                Console.WriteLine("Null project created! {0}", project.ProjektName);
            }
            catch (Exception)
            {
                this.project.Insert(0, project);
                Console.WriteLine("Project created {0}", project.ProjektName);
            }
            
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(this.project.ElementAt(ProjectNumber), 0);
        }

        private void buttonSaveProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonBackProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProjectNumber > 0 && this.project != null)
            {
                this.ProjectNumber--;
            }
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(project.ElementAt(this.ProjectNumber), this.UpdateNumber);
            stopTimer();
            Console.WriteLine(this.ProjectNumber);
        }

        private void buttonNextProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProjectNumber < this.project.Count -1 && this.project != null)
            {
                this.ProjectNumber++;
            }
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(project.ElementAt(this.ProjectNumber), this.UpdateNumber);
            stopTimer();
            Console.WriteLine(this.ProjectNumber);
        }

        private void buttonCreateProject_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Verification and saving project
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            groupBoxCreateNewProject.Visibility = Visibility.Hidden;
            buttonLoadNewProject.Visibility = Visibility.Visible;
        }
    }
}
