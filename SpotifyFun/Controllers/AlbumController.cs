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
    public class AlbumController : Controller
    {
        public ActionResult SearchAlbumByName(string albumName)
        {
            AlbumHelper help = new AlbumHelper(Session["token"].ToString());

            string id = help.GetAlbumIDFromName(albumName);

            dynamic albumJSON = help.SearchAlbumByID(id);
            TempData["albumJSON"] = albumJSON;

            return View("AlbumInfoPage");
        }

        public dynamic SearchAlbumByIDFromArtistPage(string id)
        {
            AlbumHelper help = new AlbumHelper(Session["token"].ToString());

            TempData["albumJSON"] = help.SearchAlbumByID(id);

            return View("AlbumInfoPage");
        }
    }
}