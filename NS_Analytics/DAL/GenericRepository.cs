using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NS_Analytics.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update();
        T Create();
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDbContext DbContext;

        public GenericRepository(IDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = DbContext.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = DbContext.Set<T>().Where(predicate);
            return query;
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public void Update()
        {
            DbContext.Update();
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().SingleOrDefault(predicate);
        }


        public T Single(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Single(predicate);
        }

        public T Create()
        {
            return DbContext.Set<T>().Create();
        }
    }
}