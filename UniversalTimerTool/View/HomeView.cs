using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Windows.Controls;
using UniversalTimerTool.Model;

namespace UniversalTimerTool.View
{
    class HomeView
    {
        private MainWindow mainWindow { get; set; }
        public HomeView(MainWindow window)
        {
            mainWindow = window;
        }

        public void EditLabelProject(MainWindow window)
        {
           
        }

        public void ProjectsNull()
        {
            string[] labels = { "labelProjectName", "labelUpdateName" };
            string[] texts = { "Example project", "Example update" };
            UpdateLabel(labels,texts);
        }

        private void UpdateLabel(string[] labels, string[] textForEachLabel)
        {
            for (int i = 0; i < labels.Length; i++)
            {
                var lbl = (Label)mainWindow.FindName(labels[i]);
                lbl.Content = textForEachLabel[i];
            }
        }
        private void UpdateLabel(string[] labels, string textForAllLabels)
        {
            for (int i = 0; i < labels.Length; i++)
            {
                var lbl = (Label)mainWindow.FindName(labels[i]);
                lbl.Content = textForAllLabels;
            }
        }

        public void ReRenderHomePage(Project project, int UpdateNumber, int projectNumber, int ProjectsCount)
        {
            mainWindow.labelProjectName.Content = project.ProjektName;
            mainWindow.labelProjectTime.Content = String.Format("Days: {0}, Hours: {1}, Minutes: {2}, Seconds: {3}",
                project.TotalTimeFromUpdates().Days,
                project.TotalTimeFromUpdates().Hours,
                project.TotalTimeFromUpdates().Minutes,
                project.TotalTimeFromUpdates().Seconds);
            mainWindow.labelUpdateName.Content = project.Updates.ElementAt(UpdateNumber).UpdateName;

            mainWindow.labelWorkTime.Content = String.Format("{0}:{1}:{2}",project.Updates.ElementAt(UpdateNumber).WorkTime.Day*24+ project.Updates.ElementAt(UpdateNumber).WorkTime.Hour, project.Updates.ElementAt(UpdateNumber).WorkTime.Minute, project.Updates.ElementAt(UpdateNumber).WorkTime.Second);
            mainWindow.labelTrainTime.Content = String.Format("{0}:{1}:{2}", project.Updates.ElementAt(UpdateNumber).TrainTime.Day * 24 + project.Updates.ElementAt(UpdateNumber).TrainTime.Hour, project.Updates.ElementAt(UpdateNumber).TrainTime.Minute, project.Updates.ElementAt(UpdateNumber).TrainTime.Second);

            mainWindow.labelTotalUpdateTime.Content = String.Format("Days: {0}, Hours: {1}, Minutes: {2}, Seconds: {3}",
                project.WorkTrainUpdateTime(UpdateNumber).Days,
                project.WorkTrainUpdateTime(UpdateNumber).Hours,
                project.WorkTrainUpdateTime(UpdateNumber).Minutes,
                project.WorkTrainUpdateTime(UpdateNumber).Seconds);
            mainWindow.labelProjectOutOfNumber.Content = projectNumber + 1 + " / " + ProjectsCount;
            mainWindow.labelUpdateOutOfNumber.Content = UpdateNumber + 1 + " / " +  project.Updates.Count;
            //TODO: BUG FIX
        }


    }
}
