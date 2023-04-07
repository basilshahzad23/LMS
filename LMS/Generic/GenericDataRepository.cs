using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace LMS.API.Generic
{
    public interface IEntity
    {
        Guid ID { get; set; }

    }

    public abstract class GenericDataRepository<T, C> : IGenericDataRepository<T> where T : class where C : DbContext, new()
    {
        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public virtual IList<T> Get(Func<T, bool> filter, int? page, int? pageSize, string[] includePaths = null, params SortExpression<T>[] sortExpressions)
        {
            List<T> list;

            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {

                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {

                dbQuery = dbQuery.AsNoTracking().Where(filter).AsQueryable();
            }
            IOrderedEnumerable<T> orderedQuery = null;
            for (var i = 0; i < sortExpressions.Count(); i++)
            {
                if (i == 0)
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = dbQuery.OrderBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = dbQuery.OrderByDescending(sortExpressions[i].SortBy);
                    }
                }
                else
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                    }
                }
                dbQuery = orderedQuery.AsQueryable();
            }

            if (page != 0 && pageSize != 0)
            {
                dbQuery = dbQuery.Skip(((int)page - 1) * (int)pageSize);
                dbQuery = dbQuery.Take((int)pageSize);
            }
            list = dbQuery
           .ToList<T>();


            return list;

        }

        public virtual IList<T> Get(int? page, int? pageSize, string[] includePaths = null, params SortExpression<T>[] sortExpressions)
        {
            List<T> list;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {

                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }




            IOrderedEnumerable<T> orderedQuery = null;
            for (var i = 0; i < sortExpressions.Count(); i++)
            {
                if (i == 0)
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = dbQuery.OrderBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = dbQuery.OrderByDescending(sortExpressions[i].SortBy);
                    }
                }
                else
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                    }
                }
                dbQuery = orderedQuery.AsQueryable();
            }







            if (page != 0 && pageSize != 0)
            {
                dbQuery = dbQuery.Skip(((int)page - 1) * (int)pageSize);
                dbQuery = dbQuery.Take((int)pageSize);
            }
            list = dbQuery
           .ToList<T>();
            return list;
        }

        public IList<T> Get(Func<T, bool> filter, string[] includePaths = null)
        {
            List<T> list;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {
                dbQuery = dbQuery.AsNoTracking().Where(filter).AsQueryable();
            }

            list = dbQuery
           .ToList<T>();
            return list;
        }

        public IList<T> Get(Func<T, bool> filter, string[] includePaths = null, params SortExpression<T>[] sortExpressions)
        {
            List<T> list;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {
                dbQuery = dbQuery.AsNoTracking().Where(filter).AsQueryable();
            }


            IOrderedEnumerable<T> orderedQuery = null;
            for (var i = 0; i < sortExpressions.Count(); i++)
            {
                if (i == 0)
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = dbQuery.OrderBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = dbQuery.OrderByDescending(sortExpressions[i].SortBy);
                    }
                }
                else
                {
                    if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                    }
                }
                dbQuery = orderedQuery.AsQueryable();
            }

            list = dbQuery
           .ToList<T>();
            return list;
        }

        public virtual IList<T> Get()
        {
            List<T> list;

            IQueryable<T> dbQuery = _entities.Set<T>();




            list = dbQuery
               .ToList<T>();

            return list;

        }
        public virtual IList<T> Get(string[] includePaths)
        {
            List<T> list;

            IQueryable<T> dbQuery = _entities.Set<T>();

            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }


            list = dbQuery
               .ToList<T>();

            return list;

        }

        public T GetByID(Func<T, bool> filter, string[] includePaths)
        {
            T Record = null;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            Record = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(filter);
            return Record;
        }


        public IList<T> GeListtByID(Func<T, bool> filter, string[] includePaths)
        {
            List<T> list;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {
                dbQuery = dbQuery.AsNoTracking().Where(filter).AsQueryable();
            }

            list = dbQuery
           .ToList<T>();
            return list;
        }
        public IList<T> GeListtByID(Func<T, bool> filter, int? page, int? pageSize, string[] includePaths)
        {
            List<T> list;
            IQueryable<T> dbQuery = _entities.Set<T>();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    dbQuery = dbQuery.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {
                dbQuery = dbQuery.AsNoTracking().Where(filter).AsQueryable();
            }

            if (page != 0 && pageSize != 0)
            {
                dbQuery = dbQuery.Skip(((int)page - 1) * (int)pageSize);
                dbQuery = dbQuery.Take((int)pageSize);
            }
            list = dbQuery
           .ToList<T>();
            return list;
        }
        public virtual void Add(params T[] items)
        {
            DbSet<T> dbSet = _entities.Set<T>();
            foreach (T item in items)
            {
                dbSet.Add(item);
            }
            
            _entities.SaveChanges();

        }
        public virtual void Add(IEnumerable<string> exclude_include_ColumnNames, params T[] items)
        {
            DbSet<T> dbSet = _entities.Set<T>();
            foreach (T item in items)
            {
                dbSet.Add(item);
                foreach (var cn in exclude_include_ColumnNames)
                {

                    _entities.Entry(cn).State = EntityState.Detached;
                }
            }
            _entities.SaveChanges();
        }
        public virtual int GetNumberOfRecords(Func<T, bool> filter)
        {

            IEnumerable<T> dbQuery = _entities.Set<T>();

            if (filter != null)
            {
                dbQuery = dbQuery.Where(filter).AsQueryable();
            }
            return dbQuery.Count();

        }


        public virtual int GetNumberOfRecords()
        {
            IEnumerable<T> dbQuery = _entities.Set<T>();
            return dbQuery.Count();
        }
        public virtual void Update(params T[] items)
        {

            foreach (T item in items)
            {
                _entities.Entry(item).State = EntityState.Modified;

            }
            _entities.SaveChanges();

        }
        public virtual void Update<P>(Expression<Func<T, P>> excludeColumn, params T[] items)
        {
            foreach (T item in items)
            {


                _entities.Entry(item).State = EntityState.Modified;
                _entities.Entry(item).Property(excludeColumn).IsModified = false;
            }
            _entities.SaveChanges();

        }
        public virtual void Update(IEnumerable<string> exclude_Property, params T[] items)
        {
          

            foreach (var item in items)
            {
                _entities.Entry(item).State = EntityState.Modified;
                foreach (var cn in exclude_Property)
                {
                    _entities.Entry(item).Property(cn).IsModified = false;

                }
            }
            _entities.SaveChanges();
            
        }
        public virtual void Update(IEnumerable<string> exclude_Property, IEnumerable<string> include_NavProperty = null, params T[] items)
        {



            foreach (var item in items)
            {
                _entities.Entry(item).State = EntityState.Modified;
                foreach (var cn in exclude_Property)
                {
                    _entities.Entry(item).Property(cn).IsModified = false;

                }

            }
            _entities.SaveChanges();
        }
        public virtual void Delete(params T[] items)
        {

            if (items == null)
            {
            }
            else
            {
                if (items.Length > 0)
                {
                    foreach (T item in items)
                    {
                        _entities.Entry(item).State = EntityState.Deleted;

                    }
                    _entities.SaveChanges();
                }
            }
        }
        public void CompareWithOrginal(params T[] items)
        {


        }


       


    }
}
