﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NS_Analytics.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public List<int> Roles { get; set; }
        
    }
}