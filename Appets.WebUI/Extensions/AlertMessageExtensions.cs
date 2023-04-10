using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Extensions
{
    public class AlertMessageExtensions
    {
        public AlertMessageExtensions() { }
        public string Text { get; set; }
        public string cssClas { get; set; }
        private AlertMessageType _Type;

        public AlertMessageType Type {

            get { return _Type; }
            set
            {
                _Type = value;
                cssClas = Type
                switch
                {
                    AlertMessageType.Success => "success",
                    AlertMessageType.Warning => "warning",
                    AlertMessageType.Error => "error",
                    _ => "info",
                };
            }
        }


        public AlertMessageExtensions (string text, AlertMessageType type)
        {
            Text = text;
            Type = type;
        }
    
    }



    public enum AlertMessageType
    {
        Success = 0,
        Info = 1,
        Warning = 2,
        Error = 3

    }

}
