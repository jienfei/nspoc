using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Projects");
        }

        public ActionResult Projects()
        {
            return RedirectToAction("Index", "Projects");
        }
    }
}