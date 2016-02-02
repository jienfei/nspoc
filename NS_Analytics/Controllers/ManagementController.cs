using NS_Analytics.Models;
using NS_Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.Controllers
{
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

        public ActionResult Users()
        {
            //userManager = new ApplicationUserManager(new ApplicationUserStore(identityDb));
            var users = identityDb.Users.ToList();

            var models = users.Select(u => new UserViewModel 
            { 
                UserId = u.Id, 
                Role = string.Join(";", u.Roles.Select(r => r.RoleId)), 
                UserName = u.UserName 
            }).ToList();

            return View(models);
        }

        public ActionResult UserEdit(int? id)
        {
            var user = identityDb.Users.Find(id);

            var model = new UserViewModel { UserId = user.Id, UserName = user.UserName, Role = "" };

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