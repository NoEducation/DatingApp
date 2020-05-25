using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatingCommon.CQRS.Interfaces;

namespace DatingCommon.CQRS.Logic
{
    public class Dispacher : IDispacher
    {
        private readonly IQueryDispacher _queryDispacher;
        private readonly ICommandDispacher _commandDispacher;

        public Dispacher(IQueryDispacher queryDispacher,
            ICommandDispacher commandDispacher)
        {
            _queryDispacher = queryDispacher;
            _commandDispacher = commandDispacher;
        }

        public Task HandleAsync<T>(T command) where T : ICommand
        {
            return this._commandDispacher.HandleAsync(command);
        }

        public Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        {
            return this._queryDispacher.HandleAsync(query);
        }
    }
}
