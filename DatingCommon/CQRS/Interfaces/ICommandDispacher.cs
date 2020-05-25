using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingCommon.CQRS.Interfaces
{
    public interface ICommandDispacher
    {
        Task HandleAsync<T>(T command) where T : ICommand;
    }
}
