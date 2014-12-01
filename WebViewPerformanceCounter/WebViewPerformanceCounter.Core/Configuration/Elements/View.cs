using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewPerformanceCounter.Core.Configuration.Elements
{
    public class View : ConfigurationElement
    {
        private const string height = "height";
        private const string scale = "scale";

        [ConfigurationProperty(height)]
        public short Height
        {
            get
            {
                return (short)this[height];
            }
        }

        [ConfigurationProperty(scale)]
        public short Scale
        {
            get
            {
                return (short)this[scale];
            }
        }
    }
}
