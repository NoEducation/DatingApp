using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatingCommon.CQRS.Interfaces;

namespace DatingCommon.CQRS.Logic
{
    public class QueryDispacher : IQueryDispacher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispacher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        {
            var handler =
                this._serviceProvider.GetService(typeof(IQueryHandler<IQuery<TResult>, TResult>)) as
                    IQueryHandler<IQuery<TResult>, TResult>;

            if(handler is null)
                throw new NotImplementedException();

            return await handler.HandleAsync(query);
        }
    }
}
