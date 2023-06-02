using CoreFramework.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework.CommonUtils
{
    public class Locator
    {
        public LocatorType Type;
        public string Value;
        public Locator(LocatorType LocatorType, string LocatorValue)
        {
            Type = LocatorType;
            Value = LocatorValue;
        }
    }
}
