using Indeed.Test.Web.Infrastructure.Distributors;
using Indeed.Test.Web.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Indeed.Test.Web.Infrastructure.Services
{
    public class RequestChecker : IHostedService
    {
        private Timer _timer;
        private readonly IDistributor requestDistributor;
        public RequestChecker(IDistributor distributor)
        {
            requestDistributor = distributor;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DistributeRequests, null, 0, 1000);
            return Task.CompletedTask;
        }

        void DistributeRequests(object state)
        {
            requestDistributor.Distribute();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }

}
