using __NAME__.CommandProcessor.Command;
using __NAME__.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace __NAME__.CommandProcessor.Dispatcher
{
    public interface ICommandBus
    {
        Task<ICommandResult> Submit<TCommand>(TCommand command) where TCommand: ICommand;
        Task<IEnumerable<ValidationResult>> Validate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

