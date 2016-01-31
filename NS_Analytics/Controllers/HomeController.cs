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
            var categoryId = 1;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        private AnswersViewModel AnswersFilter(int periodId, int userId, int categoryId, int projectId)
        {
            var answers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();
            var questions = db.Question.Where(q => q.CategoryId == categoryId).ToList();

            var missingQuestionIds = questions.Select(q => q.Id).Except(answers.Select(a => a.QuestionId));

            foreach (var id in missingQuestionIds)
            {
                db.Answer.Add(new Answer { QuestionId = id, Value = 0, PeriodId = periodId, UserId = userId, ProjectId = projectId });
            }
            db.SaveChanges();

            answers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();

            var model = new AnswersViewModel();
            model.Answers = answers;
            return model;
        }

        public ActionResult Elicitatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 1;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        public ActionResult Analyse(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 2;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        public ActionResult Specificatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 3;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        public ActionResult Validatie(int periodId = 1)
        {
            var userId = 1;
            var categoryId = 4;
            var projectId = 1;
            var model = AnswersFilter(periodId, userId, categoryId, projectId);

            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}