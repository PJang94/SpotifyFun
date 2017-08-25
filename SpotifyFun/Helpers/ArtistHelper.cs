using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SpotifyFun.Helpers
{
    public class ArtistHelper
    {
        private string token;

        public ArtistHelper(string token)
        {
            this.token = token;
        }

        public string GetArtistIDFromName(string name)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}", name, "artist");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            return json.artists.items[0].id;
        }

        public dynamic SearchArtistByID(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/{0}/{1}", "artists", id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);
        }

        public dynamic GetAlbumsFromArtistID(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/artists/{0}/albums", id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);
        }
    }
}