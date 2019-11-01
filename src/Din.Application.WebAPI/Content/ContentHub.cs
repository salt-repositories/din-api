using System;
using System.Threading.Tasks;
using Din.Application.WebAPI.Content.HubTasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Din.Application.WebAPI.Content
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContentHub : Hub
    {
        private readonly CurrentQueueTimedTask _task;
        
        public ContentHub(CurrentQueueTimedTask task)
        {
            _task = task;
        }

        public async Task GetCurrentQueue()
        {
            await _task.StartSendingQueue(Clients.Caller, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _task.StopSendingQueue(Context.ConnectionId);
        }
    }
}