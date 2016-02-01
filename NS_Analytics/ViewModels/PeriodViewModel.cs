using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.ViewModels
{
    public class PeriodViewModel
    {
        public Period Period { get; set; }
        public IEnumerable<SelectListItem> AllUsers { get; set; }

        private List<int> selectedUsers;
        public List<int> SelectedUsers 
        { 
            get
            {
                if (selectedUsers == null)
                {
                    selectedUsers = Period.User.Select(u => u.Id).ToList();
                }
                return selectedUsers;
            } 
            set
            {
                selectedUsers = value;
            }
        }
    }
}