using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vmk.PCWebViewer.Business.Utils;
using WebViewPerformanceCounter.Core.Configuration.Elements;

namespace WebViewPerformanceCounter.Core.Configuration
{
    public class PCWebViewerSettings : ConfigurationSection
    {
        private const string servers = "servers";
        private const string webSocketSleep = "webSocketSleep";

        [ConfigurationProperty(webSocketSleep, DefaultValue = 250, IsRequired = false)]
        public int WebSocketSleep
        {
            get
            {
                return (int)this[webSocketSleep];
            }
        }

        [ConfigurationProperty(servers)]
        public GenericConfigurationElementCollection<Server> Servers
        {
            get
            {
                return (GenericConfigurationElementCollection<Server>)this[servers];
            }
        }
    }

}
