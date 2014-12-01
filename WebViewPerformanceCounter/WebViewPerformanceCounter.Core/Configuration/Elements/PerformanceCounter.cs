using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewPerformanceCounter.Core.Configuration.Elements
{
    public class PerformanceCounter : ConfigurationElement
    {
        private const string categoryName = "categoryName";
        private const string counterName = "counterName";
        private const string instanceName = "instanceName";
        private const string label = "label";
        private const string scale = "scale";
        private const string color = "color";

        [ConfigurationProperty(categoryName, IsRequired = true)]
        public string CategoryName
        {
            get
            {
                return (string)this[categoryName];
            }
        }

        [ConfigurationProperty(counterName, IsRequired = true)]
        public string CounterName
        {
            get
            {
                return (string)this[counterName];
            }
        }

        [ConfigurationProperty(instanceName, IsRequired = true)]
        public string InstanceName
        {
            get
            {
                return (string)this[instanceName];
            }
        }

        [ConfigurationProperty(label, IsRequired = false)]
        public string Label
        {
            get
            {
                return (string)this[label];
            }
        }

        [ConfigurationProperty(scale, IsRequired = false, DefaultValue = 1f)]
        public float Scale
        {
            get
            {
                return (float)this[scale];
            }
        }

        [ConfigurationProperty(color)]
        public string Color
        {
            get
            {
                return (string)this[color];
            }
        }

        public override bool Equals(object compareTo)
        {
            PerformanceCounter pc = compareTo as PerformanceCounter;
            return pc != null && (this.CounterName.Equals(pc.CounterName));
        }

        public override int GetHashCode()
        {
            return this.CounterName.GetHashCode();
        }

    }

}
