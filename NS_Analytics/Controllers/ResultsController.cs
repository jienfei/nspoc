using NS_Analytics.Models;
using NS_Analytics.ViewModels;
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
            var periodId = 1;
            var model = new ResultsViewModel();
            var results = db.AnswersResult;

            var usersInPeriod = db.UserPeriod.Count(up => up.PeriodId == periodId);
            var questionsInQuickScan = db.Question.Count(r => r.CategoryId >= 1 && r.CategoryId <= 4);
            var questionsInMaturityScan = db.Question.Count(r => r.CategoryId >= 5 && r.CategoryId <= 10);

            var performanceInQuickScan = results.Where(r => r.CategoryId >= 1 && r.CategoryId <= 4 && r.PeriodId == periodId).Sum(r => r.Performance).Value;
            var performanceInMaturityScan = results.Where(r => r.CategoryId >= 5 && r.CategoryId <= 10 && r.PeriodId == periodId).Sum(r => r.Performance).Value;

            model.Values = new List<decimal>()
            {
                (decimal) performanceInQuickScan / questionsInQuickScan * 100,
                (decimal) performanceInMaturityScan / questionsInMaturityScan * 100
            };

            model.XAxis = new List<string>()
            {
                "QuickScan", "MaturityScan"
            };

            return View(model);
        }
    }
}