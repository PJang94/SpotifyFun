using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpotifyFun.Controllers
{
    public class AuthorizationController : Controller
    {

        public ActionResult GoToAuthPage(string html)
        {
            ViewBag.html = html;
            return View("AuthPage");
        }

        // GET: Authorization
        public ActionResult AuthRedirect(string code, string state)
        {
            Session["token"] = code;
            return RedirectToAction("Index", "Home");
        }
    }
}