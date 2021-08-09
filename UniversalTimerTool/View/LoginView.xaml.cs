//#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace UniversalTimerTool.View
{
    /// <summary>
    /// Interakční logika pro LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public string tmpToken { get; private set; }
        public LoginView()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TokenInput.Text == "")
            {
                DialogResult = false;
                Close();
            }
            DialogResult = true;
            this.tmpToken = this.TokenInput.Text;
            Close();
        }
    }
}
