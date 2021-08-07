
using System;
using MassTransit;
using System.Threading.Tasks;
using System.Threading;
using __NAME__.ServiceBus;
using __NAME__.Domain.Entities.Sample;

namespace __NAME__.MassTransit.Client.Sample
{
    public class CustomerClient : IRequestClient<CustomerRequest, CustomerResponse>
    {
        private ITransitBus _transitBus;
        public CustomerClient(ITransitBus transitBus)
        {
            _transitBus = transitBus;
        }
        public async Task<Response<CustomerResponse>> Request(string url, CustomerRequest requestData, TimeSpan timeSpan,CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var requestClient = _transitBus.GetBus.CreateRequestClient<CustomerRequest>(new Uri(url), timeSpan);
            return await requestClient.GetResponse<CustomerResponse>(requestData);
        }
    }
}
