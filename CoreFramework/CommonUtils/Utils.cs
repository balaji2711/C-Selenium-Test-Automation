using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework.CommonUtils
{
    public class Utils
    {
        public Utils()
        {
            ConfigManager.InitializeEnvConfig();
        }

        public static dynamic JsonParser(string filePath)
        {
            dynamic data = JObject.Parse(File.ReadAllText(filePath));
            return data;
        }
    }
}
