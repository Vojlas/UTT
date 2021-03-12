using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            FilesController.FilesController fc = new FilesController.FilesController();
            object[] obj = (object[])fc.ImportProject();
            Project pr = null;

            if (obj != null)
            {
                pr = (Project)obj[0];
            }
                

            if (pr != null)
            {
                this.projects.Add(pr);
                ManageMainButtons(true);
                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(pr, 0, this.ProjectNumber, this.projects.Count); 
            }
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
                Update update = new Update(new Time(),new Time(),0 , textBoxUpdateName.Text);
                updates.Add(update);

                Project project = new Project(textBoxProjectName.Text, new DateTime(datePickerCreated.SelectedDate.Value.Ticks), updates, textBoxDescription.Text);

                FilesController.FilesController filesController = new FilesController.FilesController();
                filesController.WriteProjecToProjectFolder(project); //TODO: Add logging system!

                try { this.projects.Add(project); } //TODO: Logging data
                catch (Exception)
                {
                    this.projects.Insert(0, project); //TODO: Logging data
                }
                ManageMainButtons(true);
                HomeView homeView = new HomeView(this);
                homeView.ReRenderHomePage(this.projects.ElementAt(ProjectNumber), 0, this.ProjectNumber, this.projects.Count);
                MessageBox.Show("Project created!");

                //CLEAN form
                textBoxProjectName.Clear();
                datePickerCreated.SelectedDate = DateTime.Today;
                textBoxDescription.Clear();
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
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
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
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
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
                homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
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
            homeView.ReRenderHomePage(projects.ElementAt(this.ProjectNumber), this.UpdateNumber, this.ProjectNumber, this.projects.Count);
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
                Update u = new Update(new Time(), new Time(), Convert.ToInt32(textBoxNewUpdate_UpdatePrice.Text), textBoxNewUpdate_UpdateName.Text,null ,textBoxNewUpdate_UpdateDescripton.Text);
                this.projects.ElementAt(this.ProjectNumber).Updates.Add(u);
            } catch {}
            MessageBox.Show("Update created");
            textBoxNewUpdate_UpdateDescripton.Clear();
            textBoxNewUpdate_UpdateName.Clear();
            textBoxNewUpdate_UpdatePrice.Clear();
        }
        private void buttonUpdateSettings_CloseSettings_Click(object sender, RoutedEventArgs e)
        {
            groupBoxMoneyManager.Visibility = Visibility.Visible;
            groupBoxUpdateSettings.Visibility = Visibility.Hidden;
            groupBoxNewUpdate.Visibility = Visibility.Hidden;
        }
    }
}
