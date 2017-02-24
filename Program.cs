using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using xNet;

namespace KsuwaChecker
{
    internal class Program
    {
        private static void Main()
        {
            Console.Write(@"Enter your mail: ");
            //var login = Console.ReadLine();
 fasdf
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
            var searchedPerson = new SearchedPerson();
            var wrongValue = true;
            {
                while (wrongValue)
                {
                    try
                    {
                        Console.Write(@"Enter person id: vk.com/");
                        searchedPerson.PersonId = Console.ReadLine();
                        if (string.IsNullOrEmpty(searchedPerson.PersonId)) throw new ArgumentNullException(nameof(searchedPerson.PersonId));
                        searchedPerson.PersonName = MainWork.ChekingPersonName(request, searchedPerson.PersonId);
                        Console.WriteLine(@"Person found: {0}", searchedPerson.PersonName);
                        searchedPerson.Online = MainWork.ChekingOnline(request, searchedPerson.PersonId);
                        if (searchedPerson.Online)
                        {
                            Console.WriteLine("{0} is online!", searchedPerson.PersonName);
                            Console.Write("Do you wanna get notified when person come offline? Y/N: ");
                            if (Console.ReadLine() == "Y")
                            {
                                NotifyThenOffline(searchedPerson.PersonId, request, true, searchedPerson.PersonName);
                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("{0} is offline!", searchedPerson.PersonName);
                            Console.WriteLine("{0}", MainWork.GettingLastOnline(request, searchedPerson.PersonId));
                            Console.Write("Do you wanna get notified when person come online? Y/N: ");
                            if (Console.ReadLine() == "Y")
                            {
                                NotifyThenOffline(searchedPerson.PersonId, request, false, searchedPerson.PersonName);
                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }
                        wrongValue = false;
                    }
                    catch (Exception exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.ResetColor();
                    }
                }
            }
            
           
            Console.ReadKey();
        }

        private static void NotifyThenOffline(string searchedPersonPersonId, HttpRequest request, bool check, string searchedPersonName)
        {
            Console.Write("Enter your phone number: +7");
            var phone = Console.ReadLine();
            while (MainWork.ChekingOnline(request, searchedPersonPersonId) == check)
            {
                MainWork.ChekingOnline(request, searchedPersonPersonId);
                Thread.Sleep(5000);
            }
            Console.WriteLine(check == true ? "{0} gone offline" : "{0} come online!", searchedPersonName);
            throw new NotImplementedException();
        }
    }
}
