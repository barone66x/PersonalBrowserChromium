using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PersonalBrowserChromium.Services
{
    internal static class FileInputOutput
    {
        private static string _homePath = @"C:\Users\Mattia\Documents\DotNet\PersonalBrowserChromium\PersonalBrowserChromium\Startup_configurations\home_screen.csv";
        private static string _historyPah = @"C:\Users\Mattia\Documents\DotNet\PersonalBrowserChromium\PersonalBrowserChromium\Startup_configurations\history.csv";
        private static string _homePageUrl = "www.google.com";
        public static string GetHomePage() 
        {
             _homePageUrl = "www.google.com"; //in caso non ci sia una homepage la predefinita sarà google
            //string path =  @".\PersonalBrowserChromium\Startup_configurations\home_screen.csv\";
            
            //var path2 = @"home_screen.csv";
            using (StreamReader sr = new StreamReader(_homePath))
            {

                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    _homePageUrl = s;
                }


                return _homePageUrl;
            }
        }

        public static List<string>GetHistory() 
        {
            using (StreamReader sr = new StreamReader(_historyPah))
            {
                List<string> historyList = new List<string>();
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    historyList= s.Split(',').ToList();
                    //historyList.Add(s);
                }

                historyList.Reverse();

                Debug.Write(historyList.Count);
                if (historyList.Count > 5)
                { historyList.RemoveRange(5, historyList.Count - 5); }
               
                return historyList;
            }

        }

        public static void  AddLinkToHistory(string link)
        {
            using (StreamWriter sw = File.AppendText(_historyPah))
            {
                sw.Write($",{link}");
            }

        }
    }
}
