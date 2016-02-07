using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.ViewModels
{
    public class ResultsViewModel
    {
        public IEnumerable<SelectListItem> AllPeriods { get; set; }
    }
}