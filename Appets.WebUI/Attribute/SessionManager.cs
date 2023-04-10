using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Attribute
{
    public class SessionManager : ActionFilterAttribute
    {

        private readonly string _PantallaNombre;
        public SessionManager() { }
        public SessionManager(string PantallaNombre) 
        {
            _PantallaNombre = PantallaNombre;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var SinAcceso = new RouteValueDictionary(new { action = "SinAcceso", Controller = "Account" });
            var sessionExpirada = new RouteValueDictionary(new { action = "Login", Controller = "Account" }); 
            string pantallas = string.Empty;

            var session = context.HttpContext.Session.GetString("pantallas");

            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToRouteResult(sessionExpirada);
            }
            else
            {
                pantallas = session;
                if(!pantallas.Contains(_PantallaNombre) && _PantallaNombre != "Home")
                {
                    context.Result = new RedirectToRouteResult(SinAcceso);
                }
            }

        }



    }
}
