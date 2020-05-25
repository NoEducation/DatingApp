using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingCommon.CQRS.Interfaces
{
    public interface IQueryHandler<T, TResult> where T : IQuery<TResult>
    {
        Task<TResult> HandleAsync(IQuery<TResult> query);
    }
}
