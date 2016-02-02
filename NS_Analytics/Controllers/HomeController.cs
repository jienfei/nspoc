using Microsoft.AspNet.Identity;
using NS_Analytics.Models;
using NS_Analytics.DAL;
using NS_Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            var categoryId = 1;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        public ActionResult Elicitatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 1;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Elicitatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Elicitatie");
        }

        public ActionResult Analyse(int periodId = 1)
        {
            var appDb = new ApplicationDbContext();
            var result = appDb.Users.ToList();

            var userId = 1;
            var categoryId = 2;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Analyse(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Analyse");
        }

        public ActionResult Specificatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 3;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Specificatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Specificatie");
        }

        public ActionResult Validatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 4;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Validatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Validatie");
        }

        private AnswersViewModel AnswersFilter(int periodId, int userId, int categoryId, int projectId)
        {
            var answers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();
            var questions = db.Question.Where(q => q.CategoryId == categoryId).ToList();

            var missingQuestionIds = questions.Select(q => q.Id).Except(answers.Select(a => a.QuestionId));

            foreach (var id in missingQuestionIds)
            {
                db.Answer.Add(new Answer { QuestionId = id, Value = 0, PeriodId = periodId, UserId = userId });
            }
            db.SaveChanges();

            answers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();

            var model = new AnswersViewModel();
            model.Answers = answers;
            return model;
        }

        private void SaveAnswerChanges(AnswersViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var answer in model.Answers)
                {
                    db.Entry(answer).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}