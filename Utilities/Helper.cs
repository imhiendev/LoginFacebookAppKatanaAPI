using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoginAppKatanaAPI
{
    public class Helper
    {
        public static AccountModel ExtractCookieToken(string data)
        {
            string accessToken = "";
            List<string> cookieList = new List<string>();
            try
            {
                string accessTokenPattern = "\"access_token\":\"([^\"]+)\"";
                Match accessTokenMatch = Regex.Match(data, accessTokenPattern);
                accessToken = accessTokenMatch.Success ? accessTokenMatch.Groups[1].Value : "Không tìm thấy access_token";

                string cookiesPattern = "\"session_cookies\":\\s*\\[([^\\]]+)\\]";
                Match cookiesMatch = Regex.Match(data, cookiesPattern);
                string cookies = cookiesMatch.Success ? cookiesMatch.Groups[1].Value : "Không tìm thấy cookies";


                if (cookies != "Không tìm thấy cookies")
                {
                    string cookiePattern = "\"name\":\"([^\"]+)\",\"value\":\"([^\"]+)\"";
                    MatchCollection cookieMatches = Regex.Matches(cookies, cookiePattern);
                    foreach (Match match in cookieMatches)
                    {
                        string name = match.Groups[1].Value;
                        string value = match.Groups[2].Value;
                        cookieList.Add($"{name}={value}");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error when extracting cookie and token", ex);
            }


            return new AccountModel
            {
                Cookie = string.Join("; ", cookieList),
                Token = accessToken
            };
        }
    }
}
