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

            string URL = "https://reqres.in/api/users/3"; // ssilka na bazu dannih ka ya ponual + id chela kotoriy nuzen

            var request = (HttpWebRequest)WebRequest.Create(URL); // Sozdaem zaprost

            request.Method = "GET"; // Oboznachaem metod (vivod infi)

            request.Proxy.Credentials = new NetworkCredential("student","student"); // Zadaem znachenia dlya proxy, chtobi ne vidavalo oshibku v sharage

            var response = (HttpWebResponse)request.GetResponse(); // Poluchenie otveta ot servera 

            StreamReader reader = new StreamReader(response.GetResponseStream()); // suda pomeshaetsa rezultat zaprosa

            string Text = reader.ReadToEnd(); // pozvolyaet prochitat zapros, chtobi poluchit dannie iz zaprosa

            var JsonText = JsonConvert.DeserializeObject<User>(Text); // kak ya ponyal konvertim nekrasiviy kod JSON v obichniy text

            Console.WriteLine($"{JsonText.data.id} {JsonText.data.last_name} {JsonText.data.first_name} {JsonText.data.email}"); // То какие данные я хочу вывести, to chto vvoditsa v konsol, kak yz ponyal

            Console.ReadLine(); // Komanda dlya prochteniya zaprosa, kotoriy yha vvel v konsol ------------------------------------------^
        }
    }
}
