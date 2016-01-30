using NS_Analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NS_Analytics.DAL
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
    }

    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbContext context) : base(context) { }

        //public GetQuestionsForPeriod(string period)
        //{
            
        //}
    }
}