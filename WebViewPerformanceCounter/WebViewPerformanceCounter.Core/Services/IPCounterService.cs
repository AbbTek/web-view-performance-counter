using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WebViewPerformanceCounter.Core.Services.DTO;

namespace WebViewPerformanceCounter.Core.Services
{
    [ServiceContract]
    public interface IPCounterService
    {
        [OperationContract]
        List<ServerResult> GetData();   
    }
}
