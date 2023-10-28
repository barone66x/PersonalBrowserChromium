using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PersonalBrowserChromium.Services
{
    public static class UtilitiesClass
    {
        public static void  test (object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("è entrato");

        }
        public static void NewPage(TabControl Tabs)
        {
            ChromiumWebBrowser newBrowser = new ChromiumWebBrowser();
            newBrowser.AddressChanged += test;
            newBrowser.LoadUrl("https://www.google.com");   //cambiare con homepage letta da file
            TabItem newTab = new TabItem();
            newTab.Header = "Nuova Scheda";
            newTab.Content = newBrowser;
            Tabs.Items.Add(newTab);
            GoToLastTab(Tabs);
        }

        private static void GoToLastTab(TabControl Tabs)
        {
            var numberOfTabs = Tabs.Items.Count;
            ((TabItem)Tabs.Items[numberOfTabs - 1]).IsSelected = true;
        }

        public static void SearchSite(TabControl Tabs, string url)
        {
            ChromiumWebBrowser newBrowser = new ChromiumWebBrowser();
            var checkedUrl = CheckUrl(url);
            newBrowser.LoadUrl(checkedUrl);
            TabItem newTab = new TabItem();
            newTab.Content = newBrowser;
            newTab.Header = "Nuova Scheda";

            // var header    = GetHeader(currentBrowser);
            //header.ContinueWith(h => { newTab.Header = (h.Result); });

            Tabs.Items.Add(newTab);
            GoToLastTab(Tabs);
        }

        public static string CheckUrl(string url)
        {
            Regex rx = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (rx.IsMatch(url))
            {
                return url;
            }
            else
            {
                return "https://" + url;
            }
        }
    }
}
