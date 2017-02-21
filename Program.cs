using System;
using xNet;

namespace KsuwaChecker
{
    internal class Program
    {
        private static void Main()
        {
            Console.Write(@"Enter your mail: ");
            //var login = Console.ReadLine();
 
            var request = new HttpRequest()
            {
                UserAgent = Http.ChromeUserAgent(),
                Cookies = new CookieDictionary()
            };
            var response =
                request.Get(
                    "vk.com").ToString();
            var rp = new RequestParams
            {
                ["act"] = "login",
                ["role"] = "al_frame",
                ["ip_h"] = response.Substring("ip_h\" value=\"", "\""),
                ["lg_h"] = response.Substring("lg_h\" value=\"", "\""),
                ["_origin"] = "https://vk.com",
                ["captcha_sid"] = "",
                ["captcha_key"] = "",
                ["email"] = login,
                ["pass"] = password,
                ["expire"] = "0"
            };
            Console.Title = "Ksuwa Cheker";
            Console.SetOut(new PrefixedWriter());
            bool isLoggined;
            do
            {
                isLoggined = MainWork.TryToLogin(request, rp, login, password);
            }
            while (isLoggined == false);
            Console.WriteLine("end");
            bool x;
            var wrongValue = true;
            {
                while (wrongValue)
                {
                    try
                    {
                        x = MainWork.ChekingOnline(request);
                        wrongValue = false;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
