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
using System.Windows.Shapes;

using UniversalTimerTool.Model;

namespace UniversalTimerTool
{
    /// <summary>
    /// Interakční logika pro ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : Window
    {
        private int UpdateNumber = 0;
        public ProjectsView(Project project)
        {
            InitializeComponent();

            textBoxProjectName.Text = project.ProjektName;
            textBoxProjectTime.Text = String.Format("Days: {0}, Hours: {1}, Minutes: {2}, Seconds: {3}",
                project.TotalTimeFromUpdates().Days,
                project.TotalTimeFromUpdates().Hours,
                project.TotalTimeFromUpdates().Minutes,
                project.TotalTimeFromUpdates().Seconds);
            textBoxProjectDescription.Text = project.Description;
            textBoxProjectTrainTime.Text = "function not implemented";
            textBoxProjectWorkTime.Text = "function not implemented";

            dataGridUpdates.ItemsSource = project.Updates;

            textBoxUpdateName.Text = project.Updates.ElementAt(UpdateNumber).UpdateName;
            textBoxUpdateTime.Text = String.Format("Days: {0}, Hours: {1}, Minutes: {2}, Seconds: {3}",
                project.WorkTrainUpdateTime(UpdateNumber).Days,
                project.WorkTrainUpdateTime(UpdateNumber).Hours,
                project.WorkTrainUpdateTime(UpdateNumber).Minutes,
                project.WorkTrainUpdateTime(UpdateNumber).Seconds);
            textBoxUpdateDescription.Text = project.Updates.ElementAt(UpdateNumber).UpdateDescription;
            textBoxUpdateTrainTime.Text = String.Format("{0:T}", project.Updates.ElementAt(UpdateNumber).TrainTime);
            textBoxUpdateWorkTime.Text = String.Format("{0:T}", project.Updates.ElementAt(UpdateNumber).WorkTime);
        }
    }
}
