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
            textBoxProjectTime.Text = String.Format("Hours: {0}, Minutes: {1}, Seconds: {2}",
                project.TotalProjectTime().Hours,
                project.TotalProjectTime().Minutes,
                project.TotalProjectTime().Seconds);
            textBoxProjectDescription.Text = project.Description;
            textBoxProjectTrainTime.Text = "function not implemented";
            textBoxProjectWorkTime.Text = "function not implemented";

            dataGridUpdates.ItemsSource = project.Updates;

            textBoxUpdateName.Text = project.Updates.ElementAt(UpdateNumber).UpdateName;
            textBoxUpdateTime.Text = String.Format("Hours: {0}, Minutes: {1}, Seconds: {2}",
                project.TotalUpdateTime(UpdateNumber).Hours,
                project.TotalUpdateTime(UpdateNumber).Minutes,
                project.TotalUpdateTime(UpdateNumber).Seconds);
            textBoxUpdateDescription.Text = project.Updates.ElementAt(UpdateNumber).UpdateDescription;
            textBoxUpdateTrainTime.Text = project.Updates.ElementAt(UpdateNumber).Train.Show();
            textBoxUpdateWorkTime.Text = project.Updates.ElementAt(UpdateNumber).Work.Show();
        }
    }
}
