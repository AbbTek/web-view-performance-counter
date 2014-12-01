using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebViewPerformanceCounter.Core.Services.DTO
{
    [DataContract]
    public class ServerResult
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public List<PCResult> PerformanceCounters { get; set; }

        [DataMember]
        public short Scale { get; set; }

        [DataMember]
        public string Error { get; set; }

        [DataMember]
        public DateTime TimeServer { get; set; }
    }
}
