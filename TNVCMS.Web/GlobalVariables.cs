using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNVCMS.Web
{
    public static class GlobalVariables
    {
        public static int PageSize
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            }
        }
        public static string WebTitle
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WebTitle"];
            }
        }
    }
}