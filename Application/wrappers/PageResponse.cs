﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.wrappers
{
    public class PageResponse<T> : Response<T>
    {
        public int PageNumber {  get; set; }    
        public int PageSize { get; set; }

        public PageResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
