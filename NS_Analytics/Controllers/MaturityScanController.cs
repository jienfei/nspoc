using Microsoft.AspNet.Identity;
using NS_Analytics.Models;
using NS_Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
    [Authorize(Roles = "Analist, Admin")]
    public class MaturityScanController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();
        private ApplicationDbContext identityDb = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        public MaturityScanController()
        {
        }

        public ActionResult REMS1()
        {
            var model = AnswersFilter(categoryId: 5);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS1(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS1");
        }

        public ActionResult REMS2()
        {
            var model = AnswersFilter(categoryId: 6);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS2(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS2");
        }

        public ActionResult REMS3()
        {
            var model = AnswersFilter(categoryId: 7);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS3(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS3");
        }

        public ActionResult REMS4()
        {
            var model = AnswersFilter(categoryId: 8);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS4(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS4");
        }

        public ActionResult REMS5()
        {
            var model = AnswersFilter(categoryId: 9);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS5(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS5");
        }

        public ActionResult REMS6()
        {
            var model = AnswersFilter(categoryId: 10);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult REMS6(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS6");
        }

        private AnswersViewModel AnswersFilter(int categoryId)
        {
            userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));
            var user = userManager.FindByName(User.Identity.Name);
            var userId = user.Id;
            if (user.SelectedPeriodId == null)
                return null;

            var periodId = (int)user.SelectedPeriodId;

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
    }
}