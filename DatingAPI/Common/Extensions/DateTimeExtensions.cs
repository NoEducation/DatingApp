using System;

namespace DatingAPI.Common.Extensions
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
