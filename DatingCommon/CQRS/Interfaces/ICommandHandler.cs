using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingCommon.CQRS.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
