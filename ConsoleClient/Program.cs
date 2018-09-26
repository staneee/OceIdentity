using Common;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;



namespace ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            // http://localhost
            var ids4Host = "http://localhost:10089";
            var msgHost = "http://localhost:10086/msg";
            var phoneHost = "http://localhost:10086/phone";


            var accessToken = await GetMsgToken(ids4Host);  // 请求MsgApi Token
            await RequestData(accessToken, $"{msgHost}/values");       // 请求MsgApi
            await RequestData(accessToken, $"{phoneHost}/values");      // 请求PhoneApi

            accessToken = await GetPhoneToken(ids4Host);//  请求PhoneApi Token
            await RequestData(accessToken, $"{phoneHost}/values");  // 请求PhoneApi 1
            await RequestData(accessToken, $"{phoneHost}/identity");  // 请求PhoneApi 2
            await RequestData(accessToken, $"{msgHost}/values");    // 请求MsgApi

            Console.ReadKey();
        }


        public static async Task<string> GetMsgToken(string url)
        {

            // discover endpoints from metadata
            var client = new DiscoveryClient(url);
            client.Policy.RequireHttps = false;
            var disco = await client.GetAsync();

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return string.Empty;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, IdentityServerConfig.Client_Msg, IdentityServerConfig.Secret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(IdentityServerConfig.Api_Msg);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return string.Empty;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            return tokenResponse.AccessToken;
        }

        public static async Task<string> GetPhoneToken(string url)
        {

            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync(url);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return string.Empty;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, IdentityServerConfig.Client_Phone, IdentityServerConfig.Secret);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("admin", "123", IdentityServerConfig.Api_Phone);//使用用户名密码

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return string.Empty;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            return tokenResponse.AccessToken;
        }

        public static async Task RequestData(string token, string url)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
