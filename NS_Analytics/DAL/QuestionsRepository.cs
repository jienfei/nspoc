using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NS_Analytics.DAL
{
    public interface IQuestionsRepository : IGenericRepository<Questions>
    {
    }

    public class QuestionsRepository : GenericRepository<Questions>, IQuestionsRepository
    {
        public QuestionsRepository(IDbContext context) : base(context) { }

        //public GetQuestionsForPeriod(string period)
        //{
            
        //}
    }
}