using Microsoft.AspNet.Identity;
using NS_Analytics.Models;
using NS_Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class ManagementController : Controller
    {
        private ApplicationDbContext identityDb = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Management
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Projects");
        }

        public ActionResult Projects()
        {
            return RedirectToAction("Index", "Projects");
        }

        public async Task<ActionResult> Users()
        {
            userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));

            var users = identityDb.Users.ToList();
            var models = new List<UserViewModel>();
            foreach (var user in users)
            {
                models.Add(new UserViewModel
                {
                    UserId = user.Id,
                    Roles = await userManager.GetRolesAsync(user.Id),
                    UserName = user.UserName
                });
            }
            return View(models);
        }

        public ActionResult UserEdit(int? id)
        {
            var user = identityDb.Users.Find(id);
            var model = new UserViewModel { UserId = user.Id, UserName = user.UserName, Role = "" };

            var allRoles = identityDb.Roles.ToList();
            model.AllRoles = allRoles.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            model.SelectedRoles = user.Roles.Select(r => r.RoleId).ToList();
            return View(model);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));
                userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));

                var currentUser = identityDb.Users.Find(model.UserId);
                var currentRoles = currentUser.Roles;
                var currentRoleIds = currentRoles.Select(r => r.RoleId);

                var selectedRoles = new HashSet<int>();
                if (model.SelectedRoles != null)
                    selectedRoles = new HashSet<int>(model.SelectedRoles);

                foreach (var role in identityDb.Roles.ToList())
                {
                    if (currentRoleIds.Contains(role.Id) && !selectedRoles.Contains(role.Id))
                    {
                        userManager.RemoveFromRole(currentUser.Id, role.Name);
                    }
                    else if (!currentRoleIds.Contains(role.Id) && selectedRoles.Contains(role.Id))
                    {
                        userManager.AddToRole(currentUser.Id, role.Name);
                    }
                }
                return RedirectToAction("Users");
            }
            return View(model);
        }

        public ActionResult AddUser()
        {
            userManager.AddToRoleAsync(1, "Administrator");

            return View();
        }

        public bool AddUserToRole(ApplicationUserManager userManager, int userId, string roleName)
        {
            var idResult = userManager.AddToRoleAsync(userId, roleName);
            return idResult.IsCompleted;
        }
    }
}