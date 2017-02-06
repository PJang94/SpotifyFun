using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.IO;

namespace SpotifyFun.Controllers
{
    public class HomeController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Auth()
        {
            string clientId = ConfigurationManager.AppSettings["ida:clientId"];
            string clientSecret = ConfigurationManager.AppSettings["ida:clientSecret"];
            string redirectUri = "https://localhost:54876/Authorization/GoToAuthPage";

            string url = string.Format("https://accounts.spotify.com/authorize?client_id={0}&response_type={1}&redirect_uri={2}"
                , clientId, "code", HttpUtility.UrlEncode(redirectUri));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            
            string loginHTML = sr.ReadToEnd();
            TempData["html"] = loginHTML;

            return RedirectToAction("GoToAuthPage", "Authorization");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}