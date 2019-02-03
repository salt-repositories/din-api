﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Din.Domain.Dtos;
using Din.Domain.Services.Abstractions;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Entities;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Domain.Services.Concrete
{
    /// <inheritdoc cref="ITvShowService" />
    public class TvShowService : ContentService, ITvShowService
    {
        private readonly ITvShowClient _tvShowClient;
        private readonly string _tmdbKey;

        public TvShowService(DinContext context, ITvShowClient tvShowClient, ITMDBClientConfig config, IMapper mapper) : base(context, mapper)
        {
            _tvShowClient = tvShowClient;
            _tmdbKey = config.Key;
        }

        public async Task<IEnumerable<TvShowDto>> GetAllTvShowsAsync()
        {
            return Mapper.Map<IEnumerable<TvShowDto>>(await _tvShowClient.GetCurrentTvShowsAsync());
        }

        public async Task<TvShowDto> GetTvShowByIdAsync(int id)
        {
            return Mapper.Map<TvShowDto>(await _tvShowClient.GetTvShowByIdAsync(id));
        }

        public async Task<IEnumerable<SearchTv>> SearchTvShowAsync(string query)
        {
            return (await new TMDbClient(_tmdbKey).SearchTvShowAsync(query)).Results;
        }

        public async Task<(bool success, SearchTv tvShow)> AddTvShowAsync(SearchTv tvShow, Guid id)
        {
            var tmdbClient = new TMDbClient(_tmdbKey);
            var showDate = Convert.ToDateTime(tvShow.FirstAirDate);

            var seasons = new List<TcRequestSeason>();
            foreach (var s in (await tmdbClient.GetTvShowAsync(tvShow.Id)).Seasons)
                seasons.Add(new TcRequestSeason {SeasonNumber = s.SeasonNumber.ToString(), Monitored = true});

            var requestObj = new TcRequest
            {
                TvShowId = (await tmdbClient.GetTvShowExternalIdsAsync(tvShow.Id)).TvdbId,
                Title = tvShow.Name,
                QualityProfileId = 0,
                ProfileId = "6",
                TitleSlug = GenerateTitleSlug(tvShow.Name, showDate),
                Monitored = true,
                Seasons = seasons
            };

            var response = await _tvShowClient.AddTvShowAsync(requestObj);

            if (response.status)
            {
                await LogContentAdditionAsync(tvShow.Name, id, ContentType.TvShow, Convert.ToInt32(requestObj.TvShowId), response.systemId);

                return (true, tvShow);
            }

            return (false, tvShow);
        }

        public async Task<IEnumerable<CalendarItemDto>> GetTvShowCalendarAsync(DateTime start, DateTime end)
        {
            return Mapper.Map<IEnumerable<CalendarItemDto>>(await _tvShowClient.GetCalendarAsync<TcCalendar>(start, end));
        }

        public async Task<IEnumerable<QueueDto>> GetTvShowQueueAsync()
        {
            return Mapper.Map<IEnumerable<QueueDto>>(await _tvShowClient.GetQueue<TcQueueItem>());
        }
    }
}