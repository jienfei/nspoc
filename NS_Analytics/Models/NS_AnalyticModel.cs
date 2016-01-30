using NS_Analytics.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NS_Analytics.Models
{
    public partial class NS_AnalyticModelContainer : IDbContext
    {
		public new IDbSet<T> Set<T>() where T : class
		{
			return base.Set<T>();
		}

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}