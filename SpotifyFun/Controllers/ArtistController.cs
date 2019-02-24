using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpotifyFun.Helpers;

namespace SpotifyFun.Controllers
{
    public class ArtistController : Controller
    {
        public ActionResult GoToArtistSearchPage()
        {
            return View("ArtistSearchPage");
        }

        public ActionResult SearchArtistByName(string artistName)
        {
            ArtistHelper help = new ArtistHelper(Session["token"].ToString());

            string id = help.GetArtistIDFromName(artistName);

            if (id.Equals("none"))
            {
                TempData["noArtistFound"] = true;
                return View("ArtistSearchPage");
            }

            dynamic artistJSON = help.SearchArtistByID(id);
            TempData["artistJSON"] = artistJSON;
            TempData["artistAlbums"] = help.GetAlbumsFromArtistID(id).items;

            return View("ArtistInfoPage");
        }
    }
}