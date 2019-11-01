using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Content.Responses;
using Din.Application.WebAPI.Serilization;
using Din.Domain.Queries.Movies;
using Din.Domain.Queries.TvShows;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI.Content.HubTasks
{
    internal struct Connection
    {
        public string ConnectionId { get; set; }
        public IClientProxy Client { get; set; }
    }

    public class CurrentQueueTimedTask
    {
        private const int TaskInterval = 10;
        private readonly ILogger<CurrentQueueTimedTask> _logger;
        private readonly Container _container;
        private readonly IMediator _bus;
        private readonly IMapper _mapper;
        private readonly ICollection<Connection> _connections;

        private Timer _timer;
        private bool _running;

        public CurrentQueueTimedTask(ILogger<CurrentQueueTimedTask> logger, Container container, IMediator bus, IMapper mapper)
        {
            _logger = logger;
            _container = container;
            _bus = bus;
            _mapper = mapper;
            _connections = new List<Connection>();
            _running = false;
        }

        public Task StartSendingQueue(IClientProxy client, string connectionId)
        {
            _logger.LogInformation($"Start sending current queue to {connectionId}");
            
            if (_connections.Any(conn => conn.ConnectionId.Equals(connectionId)))
            {
                return Task.CompletedTask;
            }

            _connections.Add(new Connection
            {
                ConnectionId = connectionId,
                Client = client
            });

            if (_timer == null)
            {
                _timer = new Timer(async (state) =>
                {
                    using (AsyncScopedLifestyle.BeginScope(_container))
                    {
                        await Execute();
                    }
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(TaskInterval));
            }

            return Task.CompletedTask;
        }

        public Task StopSendingQueue(string connectionId)
        {
            var connection = _connections.FirstOrDefault(conn => conn.ConnectionId.Equals(connectionId));

            if (connection.Equals(default(Connection)))
            {
                return Task.CompletedTask;
            }
            
            _logger.LogInformation($"Stop sending current queue for {connectionId}");

            _connections.Remove(connection);

            if (_connections.Count != 0)
            {
                return Task.CompletedTask;
            }
            
            _logger.LogInformation("Stop fetching current queue");

            while (_running)
            {
                // wait for task to finish
            }

            _timer?.Change(Timeout.Infinite, 0);
            _timer = null;

            return Task.CompletedTask;
        }

        private async Task Execute()
        {
            try
            {
                _logger.LogInformation("Start fetching current queue");
                
                if (_running)
                {
                    return;
                }

                _running = true;

                var movieQuery = new GetMovieQueueQuery();
                var tvShowQuery = new GetTvShowQueueQuery();

                var movieTask = _bus.Send(movieQuery);
                var tvShowTask = _bus.Send(tvShowQuery);
                
                var collection = new List<QueueResponse>();
                collection.AddRange(_mapper.Map<IEnumerable<QueueResponse>>(await movieTask));
                collection.AddRange(_mapper.Map<IEnumerable<QueueResponse>>(await tvShowTask));
                
                var response = JsonConvert.SerializeObject(
                    collection,
                    SerializationSettings.GetSerializerSettings()
                );

                _connections.ToList().ForEach(async conn => await conn.Client.SendAsync("GetCurrentQueue", response));

                _running = false;
                
                _logger.LogInformation("Finished fetching current queue");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                _running = false;
                _connections.Clear();
                _timer?.Change(Timeout.Infinite, 0);
                _timer = null;
            }
        }
    }
}