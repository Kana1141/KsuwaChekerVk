using System;
using xNet;


namespace KsuwaChecker
{
    internal class MainWork
    {
        public static bool TryToLogin(HttpRequest request, RequestParams rp, string login, string password)
        {
            var result = request.Post("https://login.vk.com/", rp).ToString();
            if (result.Contains("parent.onLoginDone"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucssesfully logined as {0} with \r\n login: {1} \r\n password: {2}",
                    result.Substring("parent.onLoginDone(\'/", "\'"), login, password);
                Console.ResetColor();
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cannot autorise with your \r\n login: {0} \r\n passord: {1}", login, password);
            Console.ResetColor();
            return false;
        }   
        internal static string ChekingPersonName(HttpRequest request, string personid)
        {
            var person = new SearchedPerson();
            var response = request.Get("vk.com/" + personid).ToString();
            return response.Substring("<h2 class=\"page_name\">", response.Contains("page_verified") ? "<div class>" : "</h2>");

            //Console.WriteLine("Not logged {0}",response.Substring("id=\"page_current_info\">","</div>"));
        }
        internal static bool ChekingOnline(HttpRequest request, string personid)
        {
            var response = request.Get("vk.com/" + personid).ToString();
            return !response.Contains("display: none;\">Online<");
        }

        internal static string GettingLastOnline(HttpRequest request, string personid)
        {
            var response = request.Get("vk.com/" + personid).ToString();
            return response.Substring("\"profile_time_lv\">", "<");
        }
    }
}
