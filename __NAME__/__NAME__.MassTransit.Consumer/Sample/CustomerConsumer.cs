
using MassTransit;
using System.Threading.Tasks;
using __NAME__.CommandProcessor.Dispatcher;
using __NAME__.Domain.Command;
using __NAME__.Domain.Entities.Sample;

namespace __NAME__.MassTransit.Consumer
{
    public class CustomerConsumer :
         IConsumer<CreateOrUpdateCustomerCommand>,
         IConsumer<CustomerRequest>
    {
        private readonly ICommandBus _commandBus;
        public CustomerConsumer() { }

        public CustomerConsumer(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task Consume(ConsumeContext<CreateOrUpdateCustomerCommand> context)
        {
            await _commandBus.Submit(context.Message);
        }

        public async Task Consume(ConsumeContext<CustomerRequest> context)
        {
            await context.RespondAsync(new CustomerResponse
            {
                CustomerId = context.Message.Id,
                CustomerName = context.Message.Name

            });
        }
    }



}
