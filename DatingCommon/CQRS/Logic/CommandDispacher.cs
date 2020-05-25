using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatingCommon.CQRS.Interfaces;

namespace DatingCommon.CQRS.Logic
{
    public class CommandDispacher : ICommandDispacher
    {
        private readonly IServiceProvider _servciProvider;

        public CommandDispacher(IServiceProvider servciProvider)
        {
            _servciProvider = servciProvider;
        }

        public async Task HandleAsync<T>(T command) where T : ICommand
        {
            var handler  = this._servciProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;

            if (handler is null)
                throw new NotImplementedException();

            await handler.HandleAsync(command);
        }
    }
}
