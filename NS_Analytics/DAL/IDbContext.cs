using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NS_Analytics.DAL
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        int Update();
    }
}