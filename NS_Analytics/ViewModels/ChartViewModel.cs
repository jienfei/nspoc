using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NS_Analytics.ViewModels
{
    public class ChartViewModel
    {
        public List<AnswersResult> AnswersResult { get; set; }
        public List<decimal> Values { get; set; }
        public List<string> XAxis { get; set; }
    }
}