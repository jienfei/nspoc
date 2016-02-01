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
            ViewBag.Users = db.User;

            var model = new PeriodViewModel();
            model.Period = period;
            
            var allUsers = db.User.ToList();
            model.AllUsers = allUsers.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            
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
                var periodToUpdate = db.Period.Include(i => i.User).First(i => i.Id == model.Period.Id);

                var newUsers = db.User.Where(u => model.SelectedUsers.Contains(u.Id)).ToList();
                var updatedUsers = new HashSet<int>(model.SelectedUsers);
                foreach (var user in db.User)
                {
                    if (!updatedUsers.Contains(user.Id))
                    {
                        periodToUpdate.User.Remove(user);
                    }
                    else
                    {
                        periodToUpdate.User.Add(user);
                    }
                }

                db.Entry(periodToUpdate).State = EntityState.Modified;
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
