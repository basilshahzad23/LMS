using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.Generic
{
    public class SortExpression<T> where T : class
    {
        public SortExpression(Func<T, object> sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
        public Func<T, object> SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
