using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace LoginAppKatanaAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Get Cookie & Token Facebook Application | #imhiendev";


            AccountModel account = new AccountModel
            {
                Uid = "61566246512910",
                Password = "nuongbach886419"
            };

            LoginService loginService = new LoginService(account);

            await loginService.Login();

            Console.WriteLine("Cookie: " + account.Cookie);
            Console.WriteLine("Token: " + account.Token);

            Console.ReadLine();
        }
        
    }
}
