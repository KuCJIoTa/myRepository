using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace API_Demo_Exam_GET_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string FullName { get; set; } // hranenie FIO
        private readonly char[] _forbiddenChars = { '%', '&' }; // hranenie zapreshennih simvolov v fio

        private bool ContainsForbiddenChars(string input) // funkciya dkya validacii polya
        {
            return input.Any(c => _forbiddenChars.Contains(c));
        }

        private void GetFullName_Click(object sender, RoutedEventArgs e)
        {
            string URL = "http://localhost:4444/TransferSimulator/fullName";
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";

            request.Proxy.Credentials = new NetworkCredential("student", "student");

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string text = reader.ReadToEnd();
            var JsonText = JsonConvert.DeserializeObject<FullNameSerializator>(text);

            FullName = JsonText.value;
            TextBoxFullName.Text = FullName;

        }
    }
}
