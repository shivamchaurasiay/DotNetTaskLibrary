﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetsTasks.Core.DataTable.DataTables
{
    /// <summary>
    /// A common interface for datatables results
    /// </summary>
    public interface IDataTableResult<T>
    {
        /// <summary>
        /// This property sets, gets the encoding used to serialize the .Net object to json.
        /// If this property is not set, the encoding from the Response is used.
        /// </summary>       
        Encoding ContentEncoding { get; set; }

        /// <summary>
        /// This property sets, gets the content Type.
        /// The default value is application/json
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the json request behavior.
        /// The default behaviour is DenyGet requests.
        /// </summary>
        /// <value>
        /// The json request behavior.
        /// </value>
       //  JsonRequestBehavior JsonRequestBehavior { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>        
        int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>        
        int iTotalRecords { get; set; }

        /// <summary>
        /// Optional - this is a string of column names, comma separated (used in combination with sName) 
        /// which will allow DataTables to reorder data on the client-side if required for display
        /// </summary>        
        string sColumns { get; set; }

        /// <summary>
        /// An unaltered copy of sEcho sent from the client side. 
        /// This parameter will change with each draw (it is basically a draw count) - 
        /// so it is important that this is implemented. Note that it strongly recommended 
        /// for security reasons that you 'cast' this parameter to an integer 
        /// in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary> 
        string sEcho { get; }

        /// <summary>
        /// The data in a 2D array
        /// Fill this structure with the plain table data
        /// represented as string.
        /// </summary> 
        T aaData { get; set; }
    }
}
