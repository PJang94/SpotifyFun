﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.IO;
using SpotifyFun.Helpers;

namespace SpotifyFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AuthorizationHelper auth = new AuthorizationHelper();
            Session["token"] = auth.GetAccessToken();
            return View();
        }
    }
}