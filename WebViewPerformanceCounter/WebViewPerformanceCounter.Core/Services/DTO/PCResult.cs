using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebViewPerformanceCounter.Core.Services.DTO
{
    [DataContract]
    public class PCResult
    {
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string CounterName { get; set; }
        [DataMember]
        public string InstanceName { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public float Value { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public float Scale { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}
