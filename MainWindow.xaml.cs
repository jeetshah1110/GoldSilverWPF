using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GoldSilver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!CheckForInternetConnection())
            {
                MessageBoxResult result = MessageBox.Show("Unable to connect to network\nCheck your network connection!",
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Asterisk);
                if(result==MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
         

            string str1 = null;
            WebClient myWebClient1 = new WebClient();

            String html1 = myWebClient1.DownloadString("https://www.google.com/search?source=hp&ei=QrDgW-a6HInjvATA6IsY&q=gold+rate&oq=&gs_l=psy-ab.3.0.35i39k1l6.0.0.0.5639.2.1.0.0.0.0.0.0..1.0....0...1c..64.psy-ab..1.1.219.6...219.DgKTSriCnHI");
         
            MatchCollection m1 = Regex.Matches(html1, @" \d+([\d,]?\d)*(\.\d+)? INR", RegexOptions.Singleline);
            foreach (Match m in m1)
            {
                str1 = m.ToString();
                textbox1.Text = str1+" for 10g of 24K";
            }
            string str2 = null;
            WebClient myWebClient2 = new WebClient();
            String html2 = myWebClient2.DownloadString("https://www.goodreturns.in/silver-rates/pune.html");
         
            MatchCollection m2 = Regex.Matches(html2, @"\d+([\d,]?\d)*(\.\d+)?</strong>", RegexOptions.Singleline);
            foreach (Match m in m2)
            {
                str2 = m.ToString();
               
                textbox2.Text = str2.Substring(0, str2.Length - 9)+ " INR per Gram";
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
