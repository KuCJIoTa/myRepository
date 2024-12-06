using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api_Test
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string URL = "https://reqres.in/api/users/2";

            var request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "DELETE";
            request.Proxy.Credentials = new NetworkCredential("student", "student");
            request.ContentType = "application/json";


          

            StreamWriter writer = new StreamWriter(request.GetRequestStream());

            

            

            var response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string Text = reader.ReadToEnd();

            

            Console.WriteLine($"{ Text} Status Code:{((int)response.StatusCode).ToString()}");

            Console.ReadLine();

           
        }
    }
}
