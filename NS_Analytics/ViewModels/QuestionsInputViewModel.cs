using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NS_Analytics.ViewModels
{
    public class QuestionsInputViewModel
    {
        public QuestionsInputViewModel()
        {
               
        }

        public List<Question> Questions { get; set; }
    }

}