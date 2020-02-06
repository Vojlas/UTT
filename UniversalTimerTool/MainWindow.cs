using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using UniversalTimerTool.Model;

namespace UniversalTimerTool
{
    public partial class MainWindow
    {
        private void SetupComponents()
        {
            this.work = false; 
            this.ProjectNumber = 0;
            this.UpdateNumber = 0;

            buttonMain_Stop.IsEnabled = false;

            ManageMainButtons(false);

            //TimerProject - Initialize
            //System.Windows.Threading.DispatcherTimer dispatcherTimerProject = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerProject.Tick += new EventHandler(dispatcherTimerProject_Tick);
            dispatcherTimerProject.Interval = new TimeSpan(0, 0, 1);

            //TimerAutosava - Initialize
            //System.Windows.Threading.DispatcherTimer dispatcherTimerAutosave = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerAutosave.Tick += new EventHandler(dispatcherTimerAutosave_Tick);
            dispatcherTimerAutosave.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimerAutosave.Start();

            checkboxToday.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(checkboxToday_Changed));
            checkboxToday.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(checkboxToday_Changed));
            checkBoxDefaultUpdate.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(checkBoxDefaultUpdate_Changed));
            checkBoxDefaultUpdate.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(checkBoxDefaultUpdate_Changed));

            LanguageControl(MyLanguage.czech);
        }

        private void LanguageControl(string x)
        {
            MyLanguage language = new MyLanguage();
            switch (x)
            {
                case MyLanguage.czech:
                    //language.Czech(); -> Not Implemented
                    break;
                default:
                    break;
            }
            //TODO: Language switch || Multilanguage support 
        }

        public void ManageMainButtons(bool isEnabled)
        {
            buttonMain_PreviousProject.IsEnabled = isEnabled;
            buttonMain_NextProject.IsEnabled = isEnabled;
            buttonMain_SaveProject.IsEnabled = isEnabled;
            buttonMain_StartWork.IsEnabled = isEnabled;
            buttonMain_StartTrain.IsEnabled = isEnabled;
            buttonMain_ShowUpdateSettings.IsEnabled = isEnabled;
        }
    }
}
