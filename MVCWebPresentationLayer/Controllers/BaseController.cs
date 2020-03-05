﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Antes de qualquer execução de ação, este método é rodado ^_^
            HttpCookie cookie = this.Request.Cookies["USERIDENTITY"];

            if (cookie == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Usuario"));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}