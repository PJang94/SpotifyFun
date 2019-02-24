using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SpotifyFun.Helpers
{
    public class AlbumHelper
    {
        private string token;

        public AlbumHelper(string token)
        {
            this.token = token;
        }

        /// GetAlbumIDFromName
        /// <summary>
        /// Takes an album name and searches for its unique spotify id
        /// </summary>
        /// <param name="name">The name of the album</param>
        /// <returns>The unique spotify id for the album</returns>
        public string GetAlbumIDFromName(string name)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}", name, "album");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            if (json.albums.items.Count == 0)
            {
                return "None";
            }
            else
            {
                return json.albums.items[0].id;
            }
        }

        /// SearchAlbumByID
        /// <summary>
        /// Takes an album id and searches for information on the album
        /// </summary>
        /// <param name="id">The unique spotify id of the album</param>
        /// <returns>the json return from the request</returns>
        public dynamic SearchAlbumByID(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/{0}/{1}", "albums", id);

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