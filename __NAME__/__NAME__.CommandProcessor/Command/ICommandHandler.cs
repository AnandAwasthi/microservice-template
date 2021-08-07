using System.Threading.Tasks;

namespace __NAME__.CommandProcessor.Command
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        Task<ICommandResult> Execute(TCommand command);
    }
}

