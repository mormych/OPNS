using System.Text;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;

namespace OPNT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //utworzenie nadawcy i połączenie sie z serwerem 
            var nadawca = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("casper002.kk@gmail.com", "uqvisvrwllonpwyz"),
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587
            });
            //stworzenie treści maila
            StringBuilder wzor = new();
            wzor.AppendLine("<b>Drogi @Model.Tytul </b></br>");
            wzor.AppendLine("Twoja faktura za węgiel czeka w <b>Biurze Obsługi Klienta.</b></br>");
            wzor.AppendLine("Dzięki niej będziesz mógł odebrać swój węgiel.</br></br>");
            wzor.AppendLine("-------------</br></br>");
            wzor.AppendLine("Urząd Miasta Wodzisławia Śląskiego <br> Bogumińska 4 <br> 44-300 Wodzisław Śląski <br> tel. 32 459 04 70</br></br>");
            wzor.AppendLine("Informacja o przetwarzaniu danych <br><br> Ta wiadomość pocztowa i wszelkie załączone do niej pliki są poufne i podlegają ochronie prawnej. Jeśli nie jest Pani/Pan jej prawidłowym adresatem, jakiekolwiek jej ujawnienie, reprodukcja, dystrybucja lub inne rozpowszechnienie, są ściśle zabronione. Jeśli otrzymała Pani/Pan niniejszy przekaz wskutek błędu, proszę o niezwłocznie powiadomienie nadawcy i usunięcie otrzymanych informacji. Nadawca nie jest odpowiedzialny za jakiekolwiek błędy lub zniekształcenia w niniejszej wiadomości, które mogły powstać w wyniku jej elektronicznej transmisji. W razie jakichkolwiek wątpliwości proszę zwrócić się o przesłanie papierowej kopii niniejszej wiadomości do nadawcy.");

            //połączenie nadawcy 
            Email.DefaultSender = nadawca;
            Email.DefaultRenderer = new RazorRenderer();

            //wysyłka maila
            var email = await Email
                .From("casper002.kk@gmail.com", "UM Wodzisław Śląski")
                .To("rkolaczkowski@op.pl")
                .Subject("Powiadomienie Urząd Miasta")
                .UsingTemplate(wzor.ToString(), new { Tytul = "Mieszkańcu" })
                //.Body("Czeka na ciebie faktura za węgiel")
                .SendAsync();

        }

    }
}