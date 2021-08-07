
using System.Threading.Tasks;
using __NAME__.CommandProcessor.Command;
using __NAME__.Data.Persistence;
using __NAME__.Domain.Command;
using __NAME__.Mongo.DatabaseFactory;
using __NAME__.ServiceBus;

namespace __NAME__.Domain.Handler.Sample
{
    public class CreateOrUpdateApplicationRegistryHandler : ICommandHandler<CreateOrUpdateCustomerCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly ITransitBus _transitBus;
        public CreateOrUpdateApplicationRegistryHandler(IDbContext dbContext, ITransitBus transitBus)
        {
            _dbContext = dbContext;
            _transitBus = transitBus;
        }

        public async Task<ICommandResult> Execute(CreateOrUpdateCustomerCommand command)
        {
            var collection = _dbContext.GetCollection<CreateOrUpdateCustomerCommand>("SampleCustomer");
            await collection.InsertOneAsync(command).ContinueWith(async (t) =>
            {
                if (_transitBus != null)
                {
                    await _transitBus.Publish(command);
                }
            }); ;
            return new CommandResult(true);
        }
    }
}
