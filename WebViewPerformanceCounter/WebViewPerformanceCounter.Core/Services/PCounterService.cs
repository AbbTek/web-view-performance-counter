using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using WebViewPerformanceCounter.Core.Services.DTO;

namespace WebViewPerformanceCounter.Core.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PCounterService : IPCounterService
    {
        #region IPCounterService Members

        public List<ServerResult> GetData()
        {
            return new PCounter().GetResults();
        }

        #endregion
    }
}
