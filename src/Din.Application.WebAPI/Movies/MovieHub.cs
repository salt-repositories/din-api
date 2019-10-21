using System;
using System.Threading.Tasks;
using Din.Application.WebAPI.Movies.HubTasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Din.Application.WebAPI.Movies
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovieHub : Hub
    {
        private readonly MovieQueueTimedTask _task;
        
        public MovieHub(MovieQueueTimedTask task)
        {
            _task = task;
        }

        public async Task GetMovieQueue()
        {
            await _task.StartSendingQueue(Clients.Caller, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _task.StopSendingQueue(Context.ConnectionId);
        }
    }
}