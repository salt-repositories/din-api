using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Movies.Responses;
using Din.Application.WebAPI.Serilization;
using Din.Domain.Queries.Movies;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI.Movies.HubTasks
{
    internal class Connection
    {
        public string ConnectionId { get; set; }
        public IClientProxy Client { get; set; }
    }

    public class MovieQueueTimedTask
    {
        private readonly Container _container;
        private readonly IMediator _bus;
        private readonly IMapper _mapper;
        private readonly ICollection<Connection> _connections;

        private Timer _timer;
        private bool _running;

        public MovieQueueTimedTask(Container container, IMediator bus, IMapper mapper)
        {
            _container = container;
            _bus = bus;
            _mapper = mapper;
            _connections = new List<Connection>();
            _running = false;
        }

        public Task StartSendingQueue(IClientProxy client, string connectionId)
        {
            if (_connections.FirstOrDefault(conn => conn.ConnectionId.Equals(connectionId)) != null)
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
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            }

            return Task.CompletedTask;
        }

        public Task StopSendingQueue(string connectionId)
        {
            var connection = _connections.FirstOrDefault(conn => conn.ConnectionId.Equals(connectionId));

            if (connection == null)
            {
                return Task.CompletedTask;
            }

            _connections.Remove(connection);

            if (_connections.Count != 0)
            {
                return Task.CompletedTask;
            }

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
                if (_running)
                {
                    return;
                }

                _running = true;

                var query = new GetMovieQueueQuery();
                var result = await _bus.Send(query);

                _connections.ToList().ForEach(async conn => await conn.Client.SendAsync("GetMovieQueue",
                    JsonConvert.SerializeObject(_mapper.Map<IEnumerable<MovieQueueResponse>>(result),
                        SerializationSettings.GetSerializerSettings())));

                _running = false;
            }
            catch (Exception)
            {
                _running = false;
                _connections.Clear();
                _timer?.Change(Timeout.Infinite, 0);
                _timer = null;
            }
        }
    }
}