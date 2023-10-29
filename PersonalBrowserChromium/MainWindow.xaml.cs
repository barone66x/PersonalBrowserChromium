using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            InitializeSearchComboBox();
          //CurrentBrowser.AddressChanged += Browser_AddressChanged;
            GridTest.Background = Brushes.GhostWhite;
            Background = Brushes.GhostWhite;
 
            UtilitiesClass.NewPage(Tabs);
        }
        private void InitializeSearchComboBox()
        {
            var historyList = FileInputOutput.GetHistory();
            foreach (var link in historyList)
            {
                SearchCombo.Items.Add(link);
            }
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
                var url = SearchCombo.Text;
                UtilitiesClass.SearchSite(Tabs, url);
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
            //CurrentBrowser.Loaded += Browser_AddressChanged;
            CurrentBrowser.TitleChanged -= Browser_AddressChanged;
            CurrentBrowser.TitleChanged += Browser_AddressChanged;
            //CurrentBrowser.TargetUpdated += Browser_AddressChanged;
            //CurrentBrowser.AddressChanged -= Browser_AddressChanged;
            //CurrentBrowser.AddressChanged += Browser_AddressChanged;
            //CurrentBrowser = currentTab;
        }

      
        public void Browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.Write(e.OldValue);
            Debug.Write(e.NewValue);
            Debug.WriteLine(CurrentBrowser.IsLoaded);
            CheckCanGoBackOrForward();
            //SearchCombo.Text = CurrentBrowser.Address;
            SearchCombo.Items.Add(CurrentBrowser.Address);
            ReverseSearchCombo(CurrentBrowser.Address);

            FileInputOutput.AddLinkToHistory(CurrentBrowser.Address);
        }

        private void ReverseSearchCombo(string url)
        {
            List<string> lista = SearchCombo.Items.Cast<string>().ToList();
            LinkedList<string> linkedList = new LinkedList<string>();
            foreach (string str in lista)
            {
                linkedList.AddFirst(str);
            }
            linkedList.AddFirst(url);

            SearchCombo.Items.Clear();

             
            foreach (string str in linkedList)
            {
                SearchCombo.Items.Add(str);
            }
            SearchCombo.Text = (string)SearchCombo.Items[0];
            
            
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
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowser();
            CurrentBrowser.Stop();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowser();
            CurrentBrowser.LoadUrl(FileInputOutput.GetHomePage());
        }
    }
}
