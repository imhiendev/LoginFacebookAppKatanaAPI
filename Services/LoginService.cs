using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAppKatanaAPI
{
    public class LoginService
    {
        public AccountModel _account;
        public LoginService(AccountModel account)
        {
            _account = account;
        }

        public async Task Login()
        {
            var variable = SecurityHelper.GenerateVariable(_account);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://b-graph.facebook.com/graphql");

            #region Headers
            request.Headers.TryAddWithoutValidation("User-Agent", "[FBAN/FB4A;FBAV/498.1.0.64.74;FBBV/692621185;FBDM/{density=1.5,width=540,height=960};FBLC/vi_VN;FBRV/0;FBCR/MobiFone;FBMF/Xiaomi;FBBD/Xiaomi;FBPN/com.facebook.katana;FBDV/2211133C;FBSV/9;FBOP/1;FBCA/x86_64:arm64-v8a;]");
            request.Headers.Add("authorization", "OAuth 350685531728|62f8ce9f74b12f84c123cc23437a4a32");
            request.Headers.Add("x-fb-sim-hni", "45201");
            request.Headers.Add("x-fb-net-hni", "45201");
            request.Headers.Add("x-graphql-client-library", "graphservice");
            request.Headers.Add("x-fb-friendly-name", "FbBloksActionRootQuery-com.bloks.www.bloks.caa.login.async.send_login_request");
            request.Headers.Add("x-tigon-is-retry", "False");
            request.Headers.Add("x-graphql-request-purpose", "fetch");
            request.Headers.Add("x-fb-device-group", "2789");
            request.Headers.Add("Accept-Encoding", "identity");
            request.Headers.Add("x-zero-eh", "error");
            request.Headers.Add("x-fb-connection-type", "WIFI");
            request.Headers.Add("x-fb-http-engine", "Tigon/Liger");
            request.Headers.Add("x-fb-client-ip", "True");
            request.Headers.Add("x-fb-server-cluster", "True");
            #endregion

            #region Content
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("method", "post"));
            collection.Add(new("pretty", "false"));
            collection.Add(new("format", "json"));
            collection.Add(new("server_timestamps", "true"));
            collection.Add(new("locale", "vi_VN"));
            collection.Add(new("purpose", "fetch"));
            collection.Add(new("fb_api_req_friendly_name", "FbBloksActionRootQuery-com.bloks.www.bloks.caa.login.async.send_login_request"));
            collection.Add(new("fb_api_caller_class", "graphservice"));
            collection.Add(new("client_doc_id", "11994080423986492941384902285"));
            collection.Add(new("variables", variable));
            collection.Add(new("fb_api_analytics_tags", "[\"GraphServices\"]"));
            collection.Add(new("client_trace_id", "10bf4b13-5888-41a7-87e8-cf99eeb2c2cd"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            #endregion

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            JObject bloksBundleActionJson = JObject.Parse(result);

            AccountModel newData = Helper.ExtractCookieToken(result.Replace("\\", ""));

            _account.Cookie = newData.Cookie;
            _account.Token = newData.Token;

        }
    }
}
