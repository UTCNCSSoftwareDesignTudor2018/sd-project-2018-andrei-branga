﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.MVC.Controllers
{
  public class ErrorController : Controller
  {
    public ActionResult UnAuthorized()
    {
      return View();
    }

    public ActionResult UnAuthorizedMessage()
    {
      return View();
    }
  }
}