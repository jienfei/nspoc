using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public IList<string> Roles { get; set; }

        public IEnumerable<SelectListItem> AllRoles { get; set; }
        public IList<int> SelectedRoles { get; set; }
        
    }
}