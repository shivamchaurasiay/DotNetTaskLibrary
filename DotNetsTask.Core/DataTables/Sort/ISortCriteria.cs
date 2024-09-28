﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetsTasks.Core.DataTable.Sort
{
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }

    //-----------------------------------------------------------------------
    /// <summary>
    /// Common interface to the sort implementations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortCriteria<T>
    {
        //-----------------------------------------------------------------------
        SortDirection Direction { get; set; }

        //-----------------------------------------------------------------------
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, Boolean useThenBy);
    }
}
