using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Context;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommandHandler : IRequestHandler<AddTvShowCommand, TvShow>
    {
        private readonly IRequestContext _context;
        private readonly ISonarrClient _client;
        private readonly IAccountRepository _accountRepository;
        private readonly IAddedContentRepository _addedContentRepository;
        private readonly ITvShowRepository _tvShowRepository;
        private readonly IMapper _mapper;
        private readonly ContentPollingQueue _contentPollingQueue;

        public AddTvShowCommandHandler
        (
            IRequestContext context,
            ISonarrClient client,
            IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository,
            ITvShowRepository tvShowRepository,
            IMapper mapper,
            ContentPollingQueue contentPollingQueue
        )
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
            _tvShowRepository = tvShowRepository;
            _mapper = mapper;
            _contentPollingQueue = contentPollingQueue;
        }

        public async Task<TvShow> Handle(AddTvShowCommand request, CancellationToken cancellationToken)
        {
            var response = await _client.AddTvShowAsync(request.TvShow, cancellationToken);

            Thread.Sleep(500);

            response = await _client.GetTvShowByIdAsync(response.SystemId, cancellationToken);

            var tvShow = _tvShowRepository.Insert(_mapper.Map<TvShow>(response));

            _contentPollingQueue.Enqueue(tvShow);

            var addedContentEntity = new AddedContent
            {
                ForeignId = tvShow.TvdbId,
                SystemId = tvShow.SystemId,
                Title = tvShow.Title,
                DateAdded = DateTime.Now,
                Status = ContentStatus.Queued,
                Account = await _accountRepository.GetAccountById(_context.GetIdentity(), cancellationToken),
                Type = ContentType.TvShow
            };

            _addedContentRepository.Insert(addedContentEntity);

            return tvShow;
        }
    }
}
