using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Appets.WebUI.Extensions
{
    public static class HtmlExtensions
    {
        public static HtmlString ShowAlert(this IHtmlHelper html)
        {
            var message = html.ViewContext.TempData.Get<AlertMessageExtensions>("ShowAlert");

            if(message == null)
            {
                return new HtmlString("");
            }

            string alert = $"<script>appConfig.alert('{HttpUtility.JavaScriptStringEncode(message.cssClas)}','{HttpUtility.JavaScriptStringEncode(message.Text)}')</script>";
            return new HtmlString(alert);
        }
    }
}
