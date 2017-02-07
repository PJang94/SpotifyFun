using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpotifyFun.Controllers
{
    public class AlbumController : Controller
    {
        public ActionResult SearchAlbumByName(string albumName)
        {
            string id = GetAlbumIDFromName(albumName);
            dynamic albumJSON = SearchAlbumByID(id);

            TempData["albumJSON"] = albumJSON;
            return View("AlbumInfoPage");
        }

        public string GetAlbumIDFromName(string name)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}", name, "album");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            return json.albums.items[0].id;
        }

        public dynamic SearchAlbumByID(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/{0}/{1}", "albums", id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);
        }

        public dynamic SearchAlbumByIDFromArtistPage(string id)
        {
            string url = string.Format("https://api.spotify.com/v1/{0}/{1}", "albums", id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string jsonString = sr.ReadToEnd();

            TempData["albumJSON"] = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);

            return View("AlbumInfoPage");
        }
    }
}