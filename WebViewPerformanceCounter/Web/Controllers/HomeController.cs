using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;
using WebViewPerformanceCounter.Core.Configuration;
using WebViewPerformanceCounter.Core.Configuration.Elements;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            model.Servers = new List<Server>();

            foreach (Server server in CManager.PCWebViewer.Servers)
            {
                model.Servers.Add(server);
            }

            return View(model);
        }
    }
}
