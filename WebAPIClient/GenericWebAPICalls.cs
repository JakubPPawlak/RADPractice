using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace WebAPIClient
{
    public enum AUTHSTATUS { NONE,OK,INVALID,FAILED }

    public static class ClientAuthentication
    {
        static public string baseWebAddress;
        static public string AuthToken = "";
        static public AUTHSTATUS AuthStatus = AUTHSTATUS.NONE;
        static public string IgdbUserToken = "PUT YOUR EXTERNAL WEB API TOKEN HERE";

        static public List<T> getList<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);

                var response = client.GetAsync(baseWebAddress + endpoint).Result;
                var resultContent = response.Content.ReadAsAsync<List<T>>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                return resultContent;
            }
        }

        // Example for this project api/AccountManager/getAccounts
        static public T getItem<T>(string EndPoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
                    var response = client.GetAsync(baseWebAddress + EndPoint).Result;
                    if (response.StatusCode == HttpStatusCode.OK && response.ReasonPhrase.Contains("no orders to manage"))
                    {
                        throw new Exception("Information from getItem call ", new Exception(response.ReasonPhrase));
                    }
                    var resultContent = response.Content.ReadAsAsync<T>(
                        new[] { new JsonMediaTypeFormatter() }).Result;
                    return resultContent;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        static public bool PostItem<T>(T g, string Endpoint)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
                var response = client.PostAsJsonAsync(baseWebAddress + Endpoint, g).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Response Object is " + response.Content.ReadAsAsync<T>(
                        new[] { new JsonMediaTypeFormatter() }).Result.ToString());
                    return true;
                }
                return false;
            }

        }

        static public bool login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                    });
                var result = client.PostAsync(baseWebAddress + "Token", content).Result;
                try
                {
                    var resultContent = result.Content.ReadAsAsync<Token>(
                        new[] { new JsonMediaTypeFormatter() }
                        ).Result;
                    string ServerError = string.Empty;
                    if (!(String.IsNullOrEmpty(resultContent.AccessToken)))
                    {
                        Console.WriteLine(resultContent.AccessToken);
                        AuthToken = resultContent.AccessToken;
                        AuthStatus = AUTHSTATUS.OK;
                        return true;
                    }
                    else
                    {
                        AuthToken = "Invalid Login";
                        AuthStatus = AUTHSTATUS.INVALID;
                        Console.WriteLine("Invalid credentials");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    AuthStatus = AUTHSTATUS.FAILED;
                    AuthToken = "Server Error -> " + ex.Message;
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }
    }
}
