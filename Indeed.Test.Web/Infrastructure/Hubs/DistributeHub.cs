using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Indeed.Test.Web.Infrastructure.Hubs
{
    public class DistributeHub : Hub<ITypedHubClient>
    {
        public async Task SendMessage()
        {
            await Clients.All.SendMessageToClient();
        }

        
    }

    public interface ITypedHubClient
    {
        Task SendMessageToClient();
    }
}
