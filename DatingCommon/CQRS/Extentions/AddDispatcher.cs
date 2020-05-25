using System;
using System.Collections.Generic;
using System.Text;
using DatingCommon.CQRS.Interfaces;
using DatingCommon.CQRS.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace DatingCommon.CQRS.Extentions
{
    public static class AddDispatcher
    {
        public static IServiceCollection AddDispatcherToService(this IServiceCollection service)
        {
            return service
                .AddScoped<ICommandDispacher, CommandDispacher>()
                .AddScoped<IQueryDispacher, QueryDispacher>()
                .AddScoped<IDispacher, Dispacher>();
        }
            
    }
}
