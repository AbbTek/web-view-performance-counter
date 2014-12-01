using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewPerformanceCounter.Core.Configuration
{
    public static class CManager
    {
        private const string PCWebViewersSettings = "PCWebViewer";
        public static PCWebViewerSettings PCWebViewer = (PCWebViewerSettings)ConfigurationManager.GetSection(PCWebViewersSettings);
    }
}
