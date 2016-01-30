using NS_Analytics.Models;
using NS_Analytics.DAL;
using NS_Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
    public class HomeController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();
        private readonly IQuestionRepository questionsRepository;

        public HomeController(IQuestionRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public ActionResult Index(string period = "1")
        {
            var answers = db.Answer.Where(a => a.Period.Name == period);
            //var questions = db.Question.Where(q => q.Period == "1").ToList();

            //db.Period.Add(period);
            //db.SaveChanges();


            var questions = questionsRepository.FindBy(c => c.Period == "1").ToList();

            var model = new QuestionsInputViewModel();
            model.Questions = questions;

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}