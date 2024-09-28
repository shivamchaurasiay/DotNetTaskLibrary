using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTasks.Core
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ApiResponseLinks Links { get; set; }
        public bool Success { get; set; }

        public ApiResponse(T data, int statusCode, string message, bool isSuccess)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
    public class ApiResponseLinks
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
