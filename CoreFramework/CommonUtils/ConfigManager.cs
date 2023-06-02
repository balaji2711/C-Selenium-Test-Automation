using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework.CommonUtils
{
    public static class ConfigManager
    {
        static string startupPath = Directory.GetCurrentDirectory();
        public static dynamic? config;

        public static void InitializeEnvConfig()
        {
            config = Utils.JsonParser(startupPath + "\\Resources\\config.json");
        }
    }
}