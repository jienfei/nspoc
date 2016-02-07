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
            var model = new ResultsViewModel();
            model.AllPeriods = db.Period.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return View(model);

            
        }

        public ActionResult Chart(int id = 0)
        {
            var periodId = id;
            if (periodId == 0)
                periodId = db.Period.First().Id;

            var model = new ChartViewModel();
            var results = db.AnswersResult;

            var usersInPeriod = db.UserPeriod.Count(up => up.PeriodId == periodId);
            var questionsInQuickScan = db.Question.Count(r => r.CategoryId >= 1 && r.CategoryId <= 4);
            var questionsInMaturityScan = db.Question.Count(r => r.CategoryId >= 5 && r.CategoryId <= 10);

            var answersQuickScan = results.Where(r => r.CategoryId >= 1 && r.CategoryId <= 4 && r.PeriodId == periodId);
            var performanceInQuickScan = answersQuickScan.Any() ? answersQuickScan.Sum(r => r.Performance).Value : 0;
            
            var answersMaturityScan = results.Where(r => r.CategoryId >= 5 && r.CategoryId <= 10 && r.PeriodId == periodId);
            var performanceInMaturityScan = answersMaturityScan.Any() ? answersMaturityScan.Sum(r => r.Performance).Value : 0;

            model.Values = new List<decimal>()
            {
                (decimal) performanceInQuickScan / questionsInQuickScan * 100,
                (decimal) performanceInMaturityScan / questionsInMaturityScan * 100
            };

            model.XAxis = new List<string>()
            {
                "QuickScan", "MaturityScan"
            };

            return PartialView("Chart", model);
        }
    }
}