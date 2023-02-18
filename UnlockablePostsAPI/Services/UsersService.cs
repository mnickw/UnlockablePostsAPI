using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace UnlockablePostsAPI.Services
{
    public class UsersService : IUsersService
    {
        public bool ValidateSignatureFromQueryString(IQueryCollection queryParams)
        {
            // TODO: Get secret key from outside
            string secretKey = "LgDJfn7FMdY3m6pD0QTY";

            if (!queryParams.TryGetValue("sign", out var signValues) || signValues.Count != 1)
                return false;

            string sign = signValues.First();

            if (string.IsNullOrEmpty(sign))
                return false;

            var vk_keysAndvalues = queryParams
                .Where(kp => kp.Key.StartsWith("vk_"))
                .OrderBy(kp => kp.Key);

            if (!vk_keysAndvalues.Any())
                return false;

            NameValueCollection queryStringCollection = System.Web.HttpUtility.ParseQueryString(string.Empty);

            foreach (var vk_keyAndvalue in vk_keysAndvalues)
                queryStringCollection.Add(vk_keyAndvalue.Key, vk_keyAndvalue.Value);

            var queryString = queryStringCollection.ToString();

            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(queryString));
                var hashB64 = Convert.ToBase64String(hash);
                hashB64 = hashB64.Replace('+', '-').Replace('/', '_').Trim('=');
                return hashB64 == sign;
            }
        }
    }
}
