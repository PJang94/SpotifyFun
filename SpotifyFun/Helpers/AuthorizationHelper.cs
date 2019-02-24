using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SpotifyFun.Helpers
{
    public class AuthorizationHelper
    {
        public string GetAccessToken()
        {
            string url = "https://accounts.spotify.com/api/token";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string authorizationHeader = "Basic " + Base64Encode(ConfigurationManager.AppSettings["ida:clientID"] + ":" + ConfigurationManager.AppSettings["ida:clientSecret"]);

            request.Headers.Add("Authorization", authorizationHeader);

            string postData = "grant_type=client_credentials";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byte1.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            return json.access_token.ToString();
        }
        
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public void UserLogin()
        {
            string clientID = ConfigurationManager.AppSettings["ida:clientID"].ToString();
            string responseType = "code";
            string redirectURI = HttpContext.Current.Server.HtmlEncode(ConfigurationManager.AppSettings["ida:loginRedirectURI"].ToString());
            string scope = ConfigurationManager.AppSettings["ida:accessScope"];

            string url = string.Format("https://accounts.spotify.com/authorize/?client_id={0}&response_type={1}&redirect_uri={2}&scope={3}",
                                        clientID, responseType, redirectURI, scope);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            WebResponse response = request.GetResponse();

        }
    }
}