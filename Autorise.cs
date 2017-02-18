using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using xNet;


namespace KsuwaChecker
{
    internal class Autorise
    {
        private HttpRequest _request;
        public void Autroisation(string login, string password)
        {
            _request = new HttpRequest()
            {
                UserAgent = Http.ChromeUserAgent(),
                Cookies = new CookieDictionary()
            };
            var response =
              _request.Get(
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
            var result = _request.Post("https://login.vk.com/", rp).ToString();
            var testdrive = _request.Get("https://vk.com/").ToString();
            Console.WriteLine("From class autroise logined as: {0} {1}", login, password);
        }
     }
}
