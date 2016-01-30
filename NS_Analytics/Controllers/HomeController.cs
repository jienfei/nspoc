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

        public ActionResult Index(int periodId = 1)
        {
            var userId = 1;
            var answers = db.Answer.Where(a => a.PeriodId == periodId && a.UserId == userId).ToList();
            var questions = db.Question.Where(q => q.PeriodId == periodId).ToList();

            //var missingQuestionIds = new List<int>();

            var missingQuestionIds = questions.Select(q => q.Id).Except(answers.Select(a => a.QuestionId));

            //foreach (var question in questions)
            //{
            //    if (!answers.Select(a => a.QuestionId).ToList().Contains(question.Id))
            //    {
            //        missingQuestionIds.Add(question.Id);
            //    }
            //}

            foreach (var id in missingQuestionIds)
            {
                db.Answer.Add(new Answer{ QuestionId = id, Value = 2, PeriodId = periodId, UserId = userId });
            }
            db.SaveChanges();

            answers = db.Answer.Where(a => a.PeriodId == periodId && a.UserId == userId).ToList();

            var model = new AnswersViewModel();
            model.Answers = answers;

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