#define DEBUG
//#undef DEBUG

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
    /// Interakční logika pro LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private string EmailPlaceholder = "Email";
        private string PassPlaceholder = "Password";
        public string Password {get;private set;}
        public string Email { get;private set; }
        
        public LoginView()
        {
#if DEBUG
            Console.WriteLine("LoginView.xaml.cs \t DEBUG = TRUE");
#endif
            InitializeComponent();
            PreparePlaceholders();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txbEmail.Text == EmailPlaceholder && PassPlaceholder == txbPass.Password)
            {
                MessageBox.Show("Bez příhlášení nemůžete pokračovat!");
                Environment.Exit(0); 
            }
        }

        //------------------------------------------------------------------
        //------------------------------------------------------------------
        //------------------------------------------------------------------
        private void PreparePlaceholders() {
            txbEmail.Foreground = Brushes.Gray;
            txbEmail.Text = EmailPlaceholder;

            txbPass.Foreground = Brushes.Gray;
            txbPass.Password = PassPlaceholder;
        }

        private void txbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbEmail.Text == EmailPlaceholder) {
                txbEmail.Clear();
                txbEmail.Foreground = Brushes.Black;
            }
        }

        private void txbPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbPass.Password == PassPlaceholder)
            {
                txbPass.Clear();
                txbPass.Foreground = Brushes.Black;
            }
        }

        private void txbPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbPass.Password == String.Empty)
            {
                txbPass.Foreground = Brushes.Gray;
                txbPass.Password = PassPlaceholder;
            }
        }

        private void txbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbEmail.Text == String.Empty)
            {
                txbEmail.Foreground = Brushes.Gray;
                txbEmail.Text = EmailPlaceholder;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CryptoController.CryptoController crypto = new CryptoController.CryptoController();
            this.Email = txbEmail.Text;
            string salt = crypto.ComputeSha256Hash(this.Email);
            this.Password = crypto.ComputeSha256Hash(salt + txbPass.Password + salt);         
            this.Close();
        }

        private void txbPass_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Process.Start("http://localhost/xxxxxxx");
#else
            System.Diagnostics.Process.Start("http://www.f4vopa.wz.cz/xxxxxxx");
#endif
        }

        private void btnResetPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dumb pass!");
        }
    }
}
