using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatingCommon.CQRS.Interfaces;

namespace DatingLogic.Base
{
    public class BaseCommandHandler : ICommandHandler<BaseCommand>
    {
        public Task HandleAsync(BaseCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
