using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KsuwaChecker
{
    internal class Program
    {
        private static string Mail { get; set; }

        private static string Password { get; set; }

        private static void Main()
        {
            Console.Title = "Ksuwa Cheker";            
            Console.Write(@"Enter your mail: ");
            Mail = Console.ReadLine();
            Console.Write(@"Enter your password: ");
            Password = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetOut(new PrefixedWriter());
            Console.WriteLine(@"Logined with: {0} {1}", Mail, Password);
            Console.ResetColor();          
            var gg = new Autorise();
            gg.Autroisation(Mail, Password);
            Console.ReadKey();
        }
    }
}
