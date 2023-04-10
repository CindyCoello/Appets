using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string Key, T value) where T : class
        {
            var properties = value.GetType().GetProperties();
            foreach (var item in properties)
            {
                if(item.PropertyType==typeof(IFormFile) || item.PropertyType == typeof(Stream))
                {
                    item.SetValue(value, null);
                }
            }

            tempData[Key] = JsonConvert.SerializeObject(value);
        }


        public static T Get<T>(this ITempDataDictionary tempData, string keyValue) where T : class
        {
            tempData.TryGetValue(keyValue, out object objeto);
            if (objeto == null)
                return null;
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>((string)objeto);
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
