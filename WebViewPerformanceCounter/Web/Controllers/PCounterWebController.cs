using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebViewPerformanceCounter.Core;
using WebViewPerformanceCounter.Core.Services.DTO;

namespace Web.Controllers
{
    public class PCounterWebController : ApiController
    {
        private static PCounter pcCounter = new PCounter();

        public List<ServerResult> GetData()
        {
            return pcCounter.GetResults();
        }
    }
}
