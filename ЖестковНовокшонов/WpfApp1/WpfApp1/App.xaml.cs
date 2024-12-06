using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string FullName {  get; set; }  
        private readonly char[] _forbiddenChars = { '%','&' };
        private bool ContainsForbiddenChars(string input)
        {
            return input.Any(c => _forbiddenChars.Contains(c));
        }
    }

   
}
