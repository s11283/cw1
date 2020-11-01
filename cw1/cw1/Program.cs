using System;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace cw1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            //sprawdzenie, czy args[0] jest puste
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("error");
                throw new System.ArgumentNullException("args[0]", "cannot be null");
            }
            //

            //rzucenie wyjątkiem, jeśli args[0] nie ma poprawnego wzorca adresu URL, zaczynającego się od http lub https
            var regex = new Regex("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$");
            if (!regex.IsMatch(args[0])) {
                throw new System.ArgumentException("args[0]", "http or https adress not valid");
            }
            //

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(args[0]);

            if (response.IsSuccessStatusCode)
            {

                var html = await response.Content.ReadAsStringAsync();
                //dispose dla odpowiedzi 'response'
                response.Dispose();
                //
                var regex2 = new Regex("[a-z0-9]+@[a-z.]+");

                MatchCollection mailAdresses = regex2.Matches(html);
                var uniqueMailAdresses = mailAdresses.OfType<Match>().Select(m => m.Value).Distinct(); //nowa kolekcja, w której każdy element jest unikalny

                if (mailAdresses.Count == 0)
                    Console.WriteLine("Nie znaleziono żadnych adresów email");  //komunikat o braku adresów
                else
                    
                    foreach (var i in uniqueMailAdresses)                       //wypisanie adresów z nowej unikalnej kolekcji, jeżeli istnieją
                    { 
                        Console.WriteLine(i); 
                    }       
            }
            //obsługa błędu przy pobieraniu strony -> wypisanie komunikatu
                else 
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
            //


            
        }
    }
}
