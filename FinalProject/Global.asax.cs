using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinalProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            Session["user_admin"] = "";
            Session["user_id"] = "";
            Session["user_fullname"] = "";
            Session["action"] = "";
            Session["control"] = "";
            Session["id_product"] = "";

        }
    }
}
