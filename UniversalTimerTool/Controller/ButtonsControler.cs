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
    public partial class MainWindow : Window
    {
        private void buttonMain_StartWork_Click(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimerProject.Start();
            this.work = true;
            buttonMain_Stop.IsEnabled = true;
            buttonMain_StartWork.IsEnabled = false;
            buttonMain_StartTrain.IsEnabled = true;
        }
        private void buttonMain_StartTrain_Click(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimerProject.Start();
            this.work = false;
            buttonMain_Stop.IsEnabled = true;
            buttonMain_StartWork.IsEnabled = true;
            buttonMain_StartTrain.IsEnabled = false;
        }
        private void buttonMain_Stop_Click(object sender, RoutedEventArgs e)
        {
            stopTimer();
        }
        private void buttonLoadNewProject_Click(object sender, RoutedEventArgs e) 
        {
            ////TODO: Import project
            //// Create OpenFileDialog 
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //// Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            //// Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();


            //// Get the selected file name and display in a TextBox 
            //if (result.HasValue && result.Value)
            //{
            //    // Open document 
            //    string filename = dlg.FileName;
            //}
            FilesController.FilesController fc = new FilesController.FilesController();
            Project pr = fc.ImportProject();
            this.projects.Add(pr);
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(pr, 0);
            tabControlMain.SelectedIndex = 0;
        }

        private void buttonCreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            groupBoxCreateNewProject.Visibility = Visibility.Visible;
            buttonLoadNewProject.Visibility = Visibility.Hidden;
        }

        private void buttonCreateProject_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Verification and saving project
            if (textBoxProjectName.Text != String.Empty &&
                datePickerCreated.SelectedDate.Value.Ticks != 0 &&
                textBoxUpdateName.Text != String.Empty)
            {

                List<Update> updates = new List<Update>();
                Update update = new Update(new DateTime(0), new DateTime(0), 0, textBoxUpdateName.Text);
                updates.Add(update);

                Project project = new Project(textBoxProjectName.Text, new DateTime(datePickerCreated.SelectedDate.Value.Ticks), updates, textBoxDescription.Text);

                FilesController.FilesController filesController = new FilesController.FilesController();
                filesController.WriteProjecToProjectFolder(project); //TODO: Add logging system!

                try { this.projects.Add(project); } //TODO: Logging data
                catch (Exception)
                {
                    this.projects.Insert(0, project); //TODO: Logging data
                }

                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(this.projects.ElementAt(ProjectNumber), 0);
                MessageBox.Show("Project created!");
            }
            else
            {
                MessageBox.Show("You must entry the project name, update name and date of creation!");
            }
        }

        private void buttonMain_SaveProject_Click(object sender, RoutedEventArgs e)
        {
            FilesController.FilesController fc = new FilesController.FilesController();
            foreach (Project project in this.projects)
            {
                fc.WriteProjecToProjectFolder(project);
            }
            MessageBox.Show("Project saved!");
        }
        
        private void buttonMain_PreviousProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProjectNumber > 0 && this.projects != null)
            {
                this.ProjectNumber--;
            }
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber);
            stopTimer();
            Console.WriteLine(this.ProjectNumber);
        }

        private void buttonMain_NextProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProjectNumber < this.projects.Count - 1 && this.projects != null)
            {
                this.ProjectNumber++;
            }
            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber);
            stopTimer();
            Console.WriteLine(this.ProjectNumber);
        }
       
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            groupBoxCreateNewProject.Visibility = Visibility.Hidden;
            buttonLoadNewProject.Visibility = Visibility.Visible;
        }

        private void buttonUpdateSettings_PreviousUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (this.UpdateNumber > 0  && this.projects != null)
            {
                this.UpdateNumber--;
                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber);
                stopTimer();
                Console.WriteLine(this.ProjectNumber); //TODO: Delete: Testing Purposes
            }
        }

        private void buttonUpdateSettings_NextUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (this.projects.Count != 0)
            {
                if (this.UpdateNumber < this.projects.ElementAt(this.ProjectNumber).Updates.Count - 1 && this.projects.ElementAt(this.ProjectNumber).Updates.Count != 0)
                {
                    this.UpdateNumber++;
                }
            }

            HomeView homeView = new HomeView(this);
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber);
            stopTimer();
            Console.WriteLine(this.ProjectNumber); //TODO: Delete: Testing Purposes
        }

        private void buttonUpdateSettings_NewUpdate_Click(object sender, RoutedEventArgs e)
        {
            groupBoxMoneyManager.Visibility = Visibility.Hidden;
            groupBoxNewUpdate.Visibility = Visibility.Visible;
            groupBoxUpdateSettings.Visibility = Visibility.Hidden;
        }

        private void buttonMain_ShowUpdateSettings_Click(object sender, RoutedEventArgs e)
        {
            if (groupBoxUpdateSettings.Visibility == Visibility.Visible)
            {
                groupBoxMoneyManager.Visibility = Visibility.Visible;
                groupBoxUpdateSettings.Visibility = Visibility.Hidden;
                groupBoxNewUpdate.Visibility = Visibility.Hidden;
            }
            else
            {
                groupBoxMoneyManager.Visibility = Visibility.Hidden;
                groupBoxUpdateSettings.Visibility = Visibility.Visible;
                groupBoxNewUpdate.Visibility = Visibility.Hidden;
            }
        }

        private void buttonCloseNewUpdate_Click(object sender, RoutedEventArgs e)
        {
            groupBoxMoneyManager.Visibility = Visibility.Hidden;
            groupBoxNewUpdate.Visibility = Visibility.Hidden;
            groupBoxUpdateSettings.Visibility = Visibility.Visible;
        }

        private void buttonNewUpdate_SaveNewUpdate_Click(object sender, RoutedEventArgs e)
        {
            try {
                Update u = new Update(new DateTime(0), new DateTime(0), Convert.ToInt32(textBoxNewUpdate_UpdatePrice.Text), textBoxNewUpdate_UpdateName.Text, textBoxNewUpdate_UpdateDescripton.Text);
                this.projects.ElementAt(this.ProjectNumber).Updates.Add(u);
            } catch {}
            MessageBox.Show("Update created");
            textBoxNewUpdate_UpdateDescripton.Text = string.Empty;
            textBoxNewUpdate_UpdateName.Text = string.Empty;
            textBoxNewUpdate_UpdatePrice.Text = string.Empty;
        }
        private void buttonUpdateSettings_CloseSettings_Click(object sender, RoutedEventArgs e)
        {
            groupBoxMoneyManager.Visibility = Visibility.Visible;
            groupBoxUpdateSettings.Visibility = Visibility.Hidden;
            groupBoxNewUpdate.Visibility = Visibility.Hidden;
        }
    }
}
