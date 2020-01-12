using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Common.Extensions
{
    public static class ResponseExtensions
    {
        public static void AddApplicationError(this HttpResponse response,string errorMessage)
        {
            response.Headers.Add("Application-Error", errorMessage);
            response.Headers.Add("Access-Control-Expose_Headers", "Application-Error");
            response.Headers.Add("Access-Contorl-Allow-Origin", "*");
        }
    }
}
