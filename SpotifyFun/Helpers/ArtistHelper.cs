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

        /// GetArtistIDFromName
        /// <summary>
        /// Searches for the unique spotify id of an artist, given their name
        /// </summary>
        /// <param name="name">The name of the artist that is to be searched</param>
        /// <returns>The unique id for this artist</returns>
        public string GetArtistIDFromName(string name)
        {
            // format url query to search artist id from the artist name
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}", name, "artist");

            // create web request from url and add authorization token to header
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);

            // get the response from the request
            WebResponse response = request.GetResponse();

            // read the json result into a string and place into a dynamic object
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            // if the item count is none, no results were found for this artist search
            if (json.artists.items.Count == 0)
            {
                return "none";
            }
            else
            {
                return json.artists.items[0].id;
            }
        }

        /// SearchArtistByID
        /// <summary>
        /// Searches for information on an artist, given the unique spotify id
        /// </summary>
        /// <param name="id">The spotify id tied to the artist</param>
        /// <returns>A json returned by the query</returns>
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