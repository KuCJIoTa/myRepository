using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_Method_Post
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string URL = "https://reqres.in/api/users/"; // ssilka na bazu dannih ka ya ponual
            var request = (HttpWebRequest)WebRequest.Create(URL); // Sozdaem zaprost
            request.Method = "POST"; // Oboznachaem metod (dobavlenie)
            request.Proxy.Credentials = new NetworkCredential("student", "student"); // Zadaem znachenia dlya proxy, chtobi ne vidavalo oshibku v sharage
            request.ContentType = "application/json";


            var data = 
        @"{
          ""first_name"": ""Mihail"",
          ""last_name"":""Pegalov""
          }"; // dannie kotorie mi hotim zanesti

            StreamWriter writer = new StreamWriter(request.GetRequestStream()); // stroka dlya zapisi na server chela

            using (writer)
            {
                writer.WriteLine(data);
            }

            var response = (HttpWebResponse)request.GetResponse(); // Poluchenie otveta ot servera

            StreamReader reader = new StreamReader(response.GetResponseStream()); // suda pomeshaetsa rezultat zaprosa

            string Text = reader.ReadToEnd(); // pozvolyaet prochitat zapros, chtobi poluchit dannie iz zaprosa

            Console.WriteLine($"{Text}"); // То какие данные я хочу вывести, to chto vvoditsa v konsol, kak yz ponyal

            Console.ReadLine(); // Komanda dlya prochteniya zaprosa, kotoriy yha vvel v konsol ---------------^
        }
    }
}
