using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vmk.PCWebViewer.Business.Utils;

namespace WebViewPerformanceCounter.Core.Configuration.Elements
{
    public class Server : ConfigurationElement
    {
        private const string name = "name";
        private const string machineName = "machineName";
        private const string performanceCounters = "performanceCounters";
        private const string view = "view";

        [ConfigurationProperty(name, IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this[name];
            }
        }

        [ConfigurationProperty(machineName, IsRequired = true)]
        public string MachineName
        {
            get
            {
                return (string)this[machineName];
            }
        }

        [ConfigurationProperty(performanceCounters)]
        public GenericConfigurationElementCollection<PerformanceCounter> PerformanceCounters
        {
            get
            {
                return (GenericConfigurationElementCollection<PerformanceCounter>)this[performanceCounters];
            }
        }

        [ConfigurationProperty(view)]
        public View View
        {
            get
            {
                return (View)this[view];
            }
        }
    }

}
