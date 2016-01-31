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
    public class MaturityScanController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();

        public ActionResult REMS1(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 5, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS1(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS1");
        }

        public ActionResult REMS2(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 6, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS2(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS2");
        }

        public ActionResult REMS3(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 7, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS3(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS3");
        }

        public ActionResult REMS4(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 8, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS4(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS4");
        }

        public ActionResult REMS5(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 9, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS5(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS5");
        }

        public ActionResult REMS6(int periodId = 1)
        {
            var model = AnswersFilter(periodId: periodId, userId: 1, categoryId: 10, projectId: 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult REMS6(AnswersViewModel model)
        {
            SaveAnswerChanges(model);
            return RedirectToAction("REMS6");
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