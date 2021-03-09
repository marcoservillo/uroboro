using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Uroboro.Common.Models;

namespace Uroboro.SL.SignalR.Hubs
{
    public class SampleHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetClientMessage()
        {
            await Clients.Caller.SendAsync("GetServerMessage", $"Hi [{Context.ConnectionId}] from SignalR Core 5");
        }

        public async Task GetClientMessageWithParam(string username)
        {
            await Clients.Caller.SendAsync("GetServerMessage", $"Hi {username} [{Context.ConnectionId}] from SignalR Core 5");
        }

        public async Task SendParamsToClient(string name)
        {
            var p1 = true;
            TodoItem item = new() { Id = 1, Name = name, IsCompleted = true };
            await Clients.Caller.SendAsync("GetServerMessageWithParams", p1, item);
        }
    }
}
