﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Common
{
    public class CustomReturnResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class CustomReturnResult<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
