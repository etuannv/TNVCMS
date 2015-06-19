using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;

namespace TNVCMS.Web
{
    public sealed class GlobalConfig
    {
        private static readonly Lazy<GlobalConfig> lazy =
            new Lazy<GlobalConfig>(() => new GlobalConfig());
        public static GlobalConfig Instance { get { return lazy.Value; } }
        IEnumerable<T_Config> ConfigList;
        private GlobalConfig()
        {
            ConfigList = new T_ConfigServices().GetAll();
        }
        public string GetValue(string key)
        {
            string ret = "";
            T_Config data = ConfigList.Where(m => m.Key == key).SingleOrDefault();
            if(data != null)
            {
                ret = data.Value;
            }
            return ret;
        }
    }
}

