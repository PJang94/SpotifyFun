using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SpotifyFun.Helpers
{
    public class TrackHelper
    {
        private string token;

        public TrackHelper(string token)
        {
            this.token = token;
        }

        /// GetTrackIDFromName
        /// <summary>
        /// Gets the unique spotify id for a track given the title
        /// </summary>
        /// <param name="name">the name of the track</param>
        /// <returns>the id of the track</returns>
        public string GetTrackIDFromName(string name)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}", name, "track");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + this.token);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            if (json.tracks.items.Count == 0)
            {
                return "None";
            }
            else
            {
                return json.tracks.items[0].id;
            }
        }

        /// SearchTrackByID
        /// <summary>
        /// Gets information on a track using its unique spotify id
        /// </summary>
        /// <param name="id">The id of the track</param>
        /// <returns>the return json of the request</returns>
        public dynamic SearchTrackByID(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/{0}/{1}", "tracks", id);

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