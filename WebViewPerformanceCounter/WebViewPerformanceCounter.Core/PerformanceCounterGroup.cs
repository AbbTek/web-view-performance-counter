using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebViewPerformanceCounter.Core.Configuration.Elements;

namespace WebViewPerformanceCounter.Core
{
    public class PerformanceCounterGroup
    {
        public System.Diagnostics.PerformanceCounter PC { get; set; }
        public PerformanceCounter PCElement { get; set; }
        public string ServerName { get; set; }
        public string CounterError { get; set; }
        public string MachineName { get; set; }
        public short Scale { get; set; }
        public string Color { get; set; }
    }
}
