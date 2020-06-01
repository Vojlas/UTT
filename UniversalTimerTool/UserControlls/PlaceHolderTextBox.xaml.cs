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

namespace UniversalTimerTool.UserControlls
{
    /// <summary>
    /// Interakční logika pro PlaceHolderTextBox.xaml
    /// </summary>
    public partial class PlaceHolderTextBox : UserControl
    {   
        public PlaceHolderTextBox()
        {
            InitializeComponent();
            this.DataContext = this;

        }
        public string Holder { get; set; }
        public string source { get; set; }
        public string PlaceHolder {get; set; }
        public int CornerRadius { get; set; }

        private void TXBmail_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            if (tb.Text == this.Holder)
            {
                tb.Clear();
                tb.Foreground = Brushes.Black;
            }          
        }

        private void TXBmail_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            if (tb.Text == string.Empty)
            {
                tb.Text = this.Holder;
                tb.Foreground = Brushes.Gray;
            }
            
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TXBmail.Focus();
        }
    }
}
