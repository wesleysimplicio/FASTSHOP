using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FASTSHOP.Api.Models
{
    public class ErrorItem
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public ErrorItem(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
