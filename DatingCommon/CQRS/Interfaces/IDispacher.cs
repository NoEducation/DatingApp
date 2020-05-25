using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingCommon.CQRS.Interfaces
{
    public interface IDispacher
    {
        Task HandleAsync<T>(T command) where T : ICommand;
        Task<TResult> HandleAsync<TResult>(IQuery<TResult> query);
    }
}
