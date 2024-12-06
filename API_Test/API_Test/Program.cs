using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {


            string URL = "https://reqres.in/api/users/";
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.Proxy.Credentials = new NetworkCredential("student", "student");
            request.ContentType = "application/json";

            var data =
            @"{
                 ""first_name"": ""Misha"",
                 ""last_nam"":""Pigalov""
                }";

            StreamWriter writer = new StreamWriter(request.GetRequestStream());

            using (writer)
            {
                writer.Write(data);
            } 

            var response 
            Console.WriteLine($"{JsonText.data.id} {JsonText.data.last_name} {JsonText.data.first_name} {JsonText.data.email}");

            Console.ReadLine();
        }
    }
}
