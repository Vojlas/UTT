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

namespace UniversalTimerTool.View
{
    /// <summary>
    /// Interakční logika pro newToDo.xaml
    /// </summary>
    public partial class newToDo : Window
    {
        public string name { get; set; }
        public newToDo()
        {
            InitializeComponent();
        }

        private void txbName_KeyDown(object sender, KeyEventArgs e)
        {
            this.name = txbName.Text;
            if (e.Key == Key.Enter)
            {
                this.Close();
            }
        }
    }
}
