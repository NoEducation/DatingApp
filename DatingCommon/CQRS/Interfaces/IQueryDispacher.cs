using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingCommon.CQRS.Interfaces
{
    public interface IQueryDispacher
    { 
        Task<TResult> HandleAsync<TResult>(IQuery<TResult> query);
    }
}
