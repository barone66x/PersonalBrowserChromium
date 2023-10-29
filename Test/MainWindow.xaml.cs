using CefSharp;
using CefSharp.Wpf;
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

namespace Test
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private  ChromiumWebBrowser browser;
        public event EventHandler AddressChanged = delegate { };
        public MainWindow()
        {

            InitializeComponent();
            CefSettings settings = new CefSettings();
            //settings.PersistSessionCookies = false;
            //browser = new ChromiumWebBrowser();
            //Browser.AddressChanged += Browser_AddressChanged;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
