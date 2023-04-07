using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.Generic
{
    public interface IGenericDataRepository<T> where T : class
    {

        int GetNumberOfRecords(Func<T, bool> filter);
        int GetNumberOfRecords();
        IList<T> Get(Func<T, bool> filter, int? page, int? pageSize, string[] includePaths = null, params SortExpression<T>[] sortExpressions);
        IList<T> Get();
        IList<T> Get(string[] includePaths = null);
        IList<T> Get(Func<T, bool> filter, string[] includePaths = null);
        IList<T> Get(Func<T, bool> filter, string[] includePaths = null, params SortExpression<T>[] sortExpressions);
        IList<T> Get(int? page, int? pageSize, string[] includePaths = null, params SortExpression<T>[] sortExpressions);
        T GetByID(Func<T, bool> filterD, string[] includePaths);
        IList<T> GeListtByID(Func<T, bool> filterD, string[] includePaths);
        IList<T> GeListtByID(Func<T, bool> filter, int? page, int? pageSize, string[] includePaths);


        void CompareWithOrginal(params T[] items);
        //void DirectQuery(String query);

        void Add(params T[] items);
        void Add(IEnumerable<string> exclude_include_ColumnNames, params T[] items);
        void Update(params T[] items);
        void Delete(params T[] items);
        void Update<P>(Expression<Func<T, P>> excludeColumn, params T[] items);

        void Update(IEnumerable<string> exclude_Property, params T[] items);
        void Update(IEnumerable<string> exclude_Property, IEnumerable<string> include_NavProperty, params T[] items);

    }
}
