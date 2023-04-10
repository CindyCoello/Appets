using Appets.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Controllers
{
    public class BaseController : Controller
    {


        protected void ShowAlert(string text, AlertMessageType type)
        {
            var messaje = new AlertMessageExtensions
            {
                Text = text,
                Type = type
            };
            TempData.Put("ShowAlert", messaje);
        }


        public IActionResult AjaxResult(dynamic model, bool success)
        {
            return Json(new
            {
                item = model,
                success = success
            });
        }
        
    }
}

