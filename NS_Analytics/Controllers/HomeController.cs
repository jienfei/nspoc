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
    [Authorize]
    public class HomeController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();
        private ApplicationDbContext identityDb = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        private readonly IQuestionRepository questionsRepository;

        public HomeController(IQuestionRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Analist, Admin")]
        public ActionResult Elicitatie()
        {
            var model = AnswersFilter(1);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult Elicitatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Elicitatie");
        }

        [Authorize(Roles = "Analist, Admin")]
        public ActionResult Analyse()
        {
            var model = AnswersFilter(2);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult Analyse(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Analyse");
        }

        [Authorize(Roles = "Analist, Admin")]
        public ActionResult Specificatie()
        {
            var model = AnswersFilter(3);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult Specificatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Specificatie");
        }

        [Authorize(Roles = "Analist, Admin")]
        public ActionResult Validatie()
        {
            var model = AnswersFilter(4);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult Validatie(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("Validatie");
        }

        private AnswersViewModel AnswersFilter(int categoryId)
        {
            userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));
            var user = userManager.FindByName(User.Identity.Name);
            var userId = user.Id;
            if (user.SelectedPeriodId == null)
                return null;

            var periodId = (int) user.SelectedPeriodId;

            var existingAnswers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();
            var questions = db.Question.Where(q => q.CategoryId == categoryId).ToList();

            var missingQuestionIds = questions.Select(q => q.Id).Except(existingAnswers.Select(a => a.QuestionId));

            foreach (var id in missingQuestionIds)
            {
                db.Answer.Add(new Answer { QuestionId = id, Value = 0, PeriodId = periodId, UserId = userId });
            }
            db.SaveChanges();

            existingAnswers = db.Answer.Where(a => a.PeriodId == periodId && a.Question.CategoryId == categoryId && a.UserId == userId).ToList();

            var model = new AnswersViewModel();
            model.Answers = existingAnswers;
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