using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotifyFun.Helpers;

namespace SpotifyFun.Controllers
{
    public class AuthorizationController : Controller
    {
        public ActionResult Login()
        {
            new AuthorizationHelper().UserLogin();
            return RedirectToAction("Index", "Home");
        }
    }
}