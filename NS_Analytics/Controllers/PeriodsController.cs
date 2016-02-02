using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NS_Analytics.Models;
using NS_Analytics.ViewModels;

namespace NS_Analytics.Controllers
{
    public class PeriodsController : Controller
    {
        private NS_AnalyticModelContainer db = new NS_AnalyticModelContainer();
        private ApplicationDbContext identityDb = new ApplicationDbContext();

        // GET: Periods
        public ActionResult Index()
        {
            var period = db.Period.Include(p => p.Project);
            return View(period.ToList());
        }

        // GET: Periods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Period period = db.Period.Find(id);
            if (period == null)
            {
                return HttpNotFound();
            }
            return View(period);
        }

        // GET: Periods/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Active,ProjectId")] Period period)
        {
            if (ModelState.IsValid)
            {
                db.Period.Add(period);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", period.ProjectId);
            return View(period);
        }

        // GET: Periods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Period period = db.Period.Find(id);
            if (period == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", period.ProjectId);
            ViewBag.Users = identityDb.Users.ToList();

            var model = new PeriodViewModel();
            model.Period = period;
            //model.UserPeriods = db.UserPeriod.Where(up => up.PeriodId == period.Id).ToList();

            var allUsers = identityDb.Users.ToList();
            model.AllUsers = allUsers.Select(o => new SelectListItem
            {
                Text = o.UserName,
                Value = o.Id.ToString()
            });

            model.SelectedUsers = db.UserPeriod.Where(up => up.PeriodId == period.Id).Select(up => up.UserId).ToList();

            return View(model);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PeriodViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var periodToUpdate = db.Period.First(i => i.Id == model.Period.Id);

                var currentUserperiods = db.UserPeriod.Where(up => up.PeriodId == model.Period.Id);
                var currentUserIds = currentUserperiods.Select(up => up.UserId).ToList();

                var selectedUsers = new HashSet<int>();
                if (model.SelectedUsers != null)
                    new HashSet<int>(model.SelectedUsers);

                foreach (var user in identityDb.Users)
                {
                    if (currentUserIds.Contains(user.Id) && !selectedUsers.Contains(user.Id))
                    {
                        db.UserPeriod.Remove(currentUserperiods.First(up => up.UserId == user.Id));
                    }
                    else if (!currentUserIds.Contains(user.Id) && selectedUsers.Contains(user.Id))
                    {
                        db.UserPeriod.Add(new UserPeriod { UserId = user.Id, PeriodId = model.Period.Id });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", model.Period.ProjectId);
            return View(model);
        }

        // GET: Periods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Period period = db.Period.Find(id);
            if (period == null)
            {
                return HttpNotFound();
            }
            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Period period = db.Period.Find(id);
            db.Period.Remove(period);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
