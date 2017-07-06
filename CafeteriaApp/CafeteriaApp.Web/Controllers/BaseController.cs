using CafeteriaApp.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CafeteriaApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;

            cultureName = CultureHelper.GetImplementedCulture(cultureName);          
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


            return base.BeginExecuteCore(callback, state);
        }

        public string GetUserId()
        {
            var userName = User.Identity.Name;

            if(!string.IsNullOrEmpty(userName))
            {
                var context = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                if(context != null)
                {
                    var user = context.FindByName(userName);
                    return user.Id;
                }
            }

            return null;
        }
    }
}