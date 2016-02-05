using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
    //[Authorize("Admin", "Manager")]
    public class ResultsController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();
        private ApplicationDbContext identityDb = new ApplicationDbContext();

        // GET: Results
        public ActionResult Index()
        {
            

            return View();
        }
    }
}