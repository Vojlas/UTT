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

namespace UniversalTimerTool.View.UserControlls
{
    /// <summary>
    /// Interakční logika pro ToDoControll.xaml
    /// </summary>
    public partial class ToDoControll : UserControl
    {
        public string PlaceHolder 
        { 
            get 
            {
                return this.TextBlockName.Text;
            }
            set 
            {
                this.TextBlockName.Text = value;
            }
        }

        public bool Status {
            get 
            {
                return this.CheckBoxStatus.IsChecked == null || false ? false : true;
            }
            set
            {
                this.CheckBoxStatus.IsChecked = value;
            }
        }
        public ToDoControll()
        {
            InitializeComponent();
        }
    }
}
