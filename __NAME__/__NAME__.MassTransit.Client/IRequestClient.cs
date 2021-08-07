using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace __NAME__.MassTransit.Client
{
    public interface IRequestClient<TRequest, TResponse> where TResponse : class

    {
        Task<Response<TResponse>> Request(string url, TRequest requestData, TimeSpan timeSpan, CancellationToken cancellationToken);
    }
}
