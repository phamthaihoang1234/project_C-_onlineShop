using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ictshop.App_Start;

namespace Ictshop.Models
{
    public class AdminAuthorize:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(SessionCofig.getUser() == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginAdmin", action = "Login"}));
            }
            return;
           
        }
    }
}