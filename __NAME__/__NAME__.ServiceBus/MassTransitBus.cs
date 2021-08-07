
namespace __NAME__.ServiceBus
{
    using MassTransit;
    public class MassTransitBus : ITransitBus
    {
        private readonly IBusControl _busControl;
        
        public MassTransitBus(IBusControl busControl)
        {
            _busControl = busControl;
        }
        IBusControl ITransitBus.GetBus => _busControl;
    }
}