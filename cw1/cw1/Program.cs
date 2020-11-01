using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace cw1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(args[0]);

            if (response.IsSuccessStatusCode) {

                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z.]+");

                MatchCollection mailAdresses = regex.Matches(html);

                foreach (var i in mailAdresses) {
                    Console.WriteLine(i);
                }
                Console.WriteLine("koniec");
            }

            
        }
    }
}
