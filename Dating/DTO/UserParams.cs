﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.DTO
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; }


        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize;} set { _pageSize = value > MaxPageSize ? MaxPageSize : value}; }
    }
}
