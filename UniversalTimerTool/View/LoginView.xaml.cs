//#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Password {get;private set;}
        public string Email { get;private set; }
        
        public LoginView()
        {
#if DEBUG
            Console.WriteLine("LoginView.xaml.cs \t DEBUG = TRUE");
#endif
            InitializeComponent();
            LoGinBrowser.Navigated += new NavigatedEventHandler(wbMain_Navigated);

            LoGinBrowser.Navigate("http://localhost/API/LoginUser");
            //PreparePlaceholders();
        }

        private void wbMain_Navigated(object sender, NavigationEventArgs e)
        {
            SetSilent(LoGinBrowser, true);
            if (LoGinBrowser.Source.ToString().Contains("/.exit"))
            {
                MessageBox.Show("HEAL YEOI");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }


        private void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            // get an IWebBrowser2 from the document
            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }


        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }
    }
}
