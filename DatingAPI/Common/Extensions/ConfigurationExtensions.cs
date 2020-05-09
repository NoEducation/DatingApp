using Dating.Common.Configurations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Common.Extensions
{   

    /// <summary>
    /// Approach that my teammates (big word) uses, in this project 
    /// is second approach that is showed in udemy course
    /// </summary>
    public  static class ConfigurationExtensions
    {
        public static TokenConfiguration GetTokenConfiguration(this IConfiguration configuration)
        {
            var token = configuration.GetValue<string>("Token");

            return new TokenConfiguration()
            {
                Token = token
            };
        }

        public static CloundinarySettings GetCloundinarySettings(this IConfiguration configuration)
        {
            var settings = configuration.GetSection("CloudinarySettings");
            return new CloundinarySettings()
            {
                CloudName = settings["CloudName"],
                ApiKey = settings["962628257262855"],
                ApiSecret = settings["kyJif9lm4kymU-U1LmiCaWIN4xs"]
            };
        }
    }
}
