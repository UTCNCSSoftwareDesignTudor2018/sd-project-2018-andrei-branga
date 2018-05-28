﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restaurant.MVC.Helpers
{
  public enum ErrorAction
  {
    UnAuthorized,
    UnAuthorizedMessage
  }

  public class DenyAttribute : AuthorizeAttribute
  {
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      return !base.AuthorizeCore(httpContext);
    }
  }

  public class CustomAuthorize : AuthorizeAttribute
  {
    private ErrorAction errorAction = Helpers.ErrorAction.UnAuthorized;

    public ErrorAction ErrorAction
    {
      get { return errorAction; }
      set { errorAction = value; }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
      {
        //if not logged, it will work as normal Authorize and redirect to the Login
        base.HandleUnauthorizedRequest(filterContext);
      }
      else
      {
        //logged and wihout the role to access it - redirect to the custom controller action
        filterContext.Result =
          new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = errorAction }));
      }
    }
  }
}