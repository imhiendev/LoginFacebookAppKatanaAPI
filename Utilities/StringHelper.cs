using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAppKatanaAPI
{
    internal class StringHelper
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GeneratePWDFormat(string password)
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // Lấy timestamp hiện tại
            return $"#PWD_FB4A:0:{timestamp}:{password}";
        }
    }
}
