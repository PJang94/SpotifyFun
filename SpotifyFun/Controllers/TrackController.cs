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
    public class TrackController : Controller
    {
        public ActionResult SearchTrackByName(string trackName)
        {
            TrackHelper help = new TrackHelper(Session["token"].ToString());

            string id = help.GetTrackIDFromName(trackName);
            dynamic trackJSON = help.SearchTrackByID(id);
            TempData["trackJSON"] = trackJSON;

            return View("TrackInfoPage");
        }
    }
}