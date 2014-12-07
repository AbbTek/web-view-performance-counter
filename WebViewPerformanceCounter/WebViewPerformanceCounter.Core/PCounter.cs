using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebViewPerformanceCounter.Core.Configuration;
using WebViewPerformanceCounter.Core.Services.DTO;

namespace WebViewPerformanceCounter.Core
{
    public class PCounter
    {
        private List<PerformanceCounterGroup> counters = new List<PerformanceCounterGroup>();

        public PCounter()
        {
            foreach (Configuration.Elements.Server server in CManager.PCWebViewer.Servers)
            {
                foreach (Configuration.Elements.PerformanceCounter item in server.PerformanceCounters)
                {
                    try
                    {
                        counters.Add(
                            new PerformanceCounterGroup()
                            {
                                PC = new PerformanceCounter(item.CategoryName, item.CounterName, item.InstanceName, server.MachineName),
                                PCElement = item,
                                ServerName = server.Name,
                                MachineName = server.MachineName,
                                Scale = server.View.Scale,
                                Color = item.Color
                            });
                    }
                    catch (Exception ex)
                    {
                        counters.Add(
                            new PerformanceCounterGroup()
                            {
                                PC = null,
                                PCElement = item,
                                ServerName = server.Name,
                                CounterError = ex.Message
                            });
                    }
                }
            }
        }

        public List<ServerResult> GetResults()
        {
            List<ServerResult> servers = new List<ServerResult>();

            foreach (var item in counters)
            {
                if (servers.Where(s => s.Name == item.ServerName).Count() == 0)
                {
                    servers.Add(new ServerResult()
                    {
                        MachineName = item.MachineName,
                        PerformanceCounters = new List<PCResult>(),
                        Name = item.ServerName,
                        Scale = item.Scale,
                        TimeServer = DateTime.Now
                    });
                }
                var server = servers.Where(s => s.Name == item.ServerName).Single();

                if (item.PC != null)
                {
                    try
                    {
                        server.PerformanceCounters.Add(new PCResult()
                        {
                            CategoryName = item.PC.CategoryName,
                            CounterName = item.PC.CounterName,
                            InstanceName = item.PC.InstanceName,
                            MachineName = item.PC.MachineName,
                            Date = DateTime.Now,
                            Value = item.PC.NextValue(),
                            Scale = item.PCElement.Scale,
                            Label = string.IsNullOrEmpty(item.PCElement.Label) ? item.PC.CounterName : item.PCElement.Label,
                            Color = item.Color
                        });
                    }
                    catch (Exception ex)
                    {
                        server.PerformanceCounters.Add(new PCResult()
                        {
                            Error = ex.Message,
                            Date = DateTime.Now,
                            Label = item.PCElement.Label
                        });
                    }
                }
                else
                {
                    server.PerformanceCounters.Add(new PCResult()
                    {
                        Error = item.CounterError,
                        Date = DateTime.Now,
                        Label = item.PCElement.Label
                    });
                }
            }
            return servers;
        }
    }

}
