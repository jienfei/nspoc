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
        private readonly IQuestionsRepository questionsRepository;

        public HomeController(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public ActionResult Index()
        {
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