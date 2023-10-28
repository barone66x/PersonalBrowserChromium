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
using CefSharp;
using CefSharp.Wpf;
using PersonalBrowserChromium.Services;

namespace PersonalBrowserChromium
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChromiumWebBrowser CurrentBrowser;

        
        public MainWindow()
        {


            InitializeComponent();
            CefSettings settings = new CefSettings();
            settings.PersistSessionCookies = false;
            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }
            CurrentBrowser = new ChromiumWebBrowser();

            
            //CurrentBrowser.AddressChanged += OnBrowserAddressChanged;
            //CurrentBrowser.AddressChanged += Browser_AddressChanged;
            GridTest.Background = Brushes.GhostWhite;
            Background = Brushes.GhostWhite;
            //ChromiumWebBrowser browser = new ChromiumWebBrowser();
            //browser.LoadUrl("www.google.com");
            TabItem newTab = new TabItem();
            //newTab.Header = "Nuova Scheda";
            //newTab.Content = browser;
            Tabs.Items.Add(newTab);

            UtilitiesClass.NewPage(Tabs);



        }

        //public void Browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    CheckCanGoBackOrForward();
        //}

        public void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            CheckCanGoBackOrForward();
        }

        private void NewTabBtn_click(object sender, RoutedEventArgs e)
        {
            UtilitiesClass.NewPage(Tabs);

        }

        private void SearchBtn_click(object sender, RoutedEventArgs e)
        {
            var url = SearchCombo.Text;
            UtilitiesClass.SearchSite(Tabs, url);

        }



        private void SearchCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var input = SearchCombo.Text;
                UtilitiesClass.SearchSite(Tabs, input);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowser();
            if (CurrentBrowser.CanGoBack)
            {
                CurrentBrowser.Back();
                NextBtn.IsEnabled = true;
            }
            if (!CurrentBrowser.CanGoBack)
            {
                BackBtn.IsEnabled = false;
            }

        }

        private void GetCurrentBrowser()
        {
            var currentTab = Tabs.SelectedContent as ChromiumWebBrowser; //dico esplicitamente che il contenuto della tab sarà un ChromiumWebBrowser
            CurrentBrowser = currentTab;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowser();
            if (CurrentBrowser.CanGoForward)
            {
                CurrentBrowser.Forward();
                BackBtn.IsEnabled = true;
            }
            if (!CurrentBrowser.CanGoForward)
            {
                NextBtn.IsEnabled = false;
            }


        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                GetCurrentBrowser();
                CheckCanGoBackOrForward();
                SearchCombo.Text = CurrentBrowser.Address;

            }


        }

        private void CheckCanGoBackOrForward()
        {
            if (!CurrentBrowser.CanGoBack)
            {
                BackBtn.IsEnabled = false;
            }
            else
            {
                BackBtn.IsEnabled = true;
            }
            if (!CurrentBrowser.CanGoForward)
            {
                NextBtn.IsEnabled = false;
            }
            else
            {
                NextBtn.IsEnabled = true;
            }
        }

        private void ChangeModeBtn_Click(object sender, RoutedEventArgs e) //cambio il colore dello sfondo
        {

            if (GridTest.Background == Brushes.GhostWhite)
            {
                //GridTest.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3c3c3c")); //converto un colore esadecimale in Brush (compatibile con lo sfondo)

                //GridTest.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2d2d2d"));
                //GridTest.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#292929"));
                GridTest.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#202124"));
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#202124"));

            }
            else
            {
                GridTest.Background = Brushes.GhostWhite;
                Background = Brushes.GhostWhite;
            }

        }

        private void Tabs_Loaded(object sender, RoutedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                GetCurrentBrowser();
                CheckCanGoBackOrForward();

            }
        }

        private void Tabs_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Source is TabControl)
            {
                GetCurrentBrowser();
                CheckCanGoBackOrForward();

            }
        }

        private void Tabs_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Source is TabControl)
            {
                GetCurrentBrowser();
                CheckCanGoBackOrForward();

            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowser();
            CurrentBrowser.Reload();

        }
    }
}
