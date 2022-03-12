using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.App_Start
{
    public class SessionCofig
    {
        public static User getUser()
        {
            return (User)HttpContext.Current.Session["use"];

        }
        public static void setUser(User UserMail)
        {
            HttpContext.Current.Session["use"] = UserMail;

        }

        public static LoginModel GetLoginInfo()
        {
            return (LoginModel)HttpContext.Current.Session["use"];

        }

        public static void SetUser(LoginModel UserEmail)
        {
            HttpContext.Current.Session["use"] = UserEmail;
        }


    }
}