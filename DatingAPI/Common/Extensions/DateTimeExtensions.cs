using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Common.Extensions
{
    public static class DateTimeExtenstions
    {
        public static int CalcuateAge(this DateTime target)
        {
            var result = DateTime.Now.Year - target.Year; ;
            if (target.AddYears(result) > DateTime.Today) ;
                result--;

            return result;
        }
            
    }
}
