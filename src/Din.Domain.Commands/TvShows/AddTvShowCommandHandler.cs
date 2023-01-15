using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Context;
using Din.Domain.Mapping;
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
        private readonly ContentPollingQueue _contentPollingQueue;

        public AddTvShowCommandHandler
        (
            IRequestContext context,
            ISonarrClient client,
            IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository,
            ITvShowRepository tvShowRepository,
            ContentPollingQueue contentPollingQueue
        )
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
            _tvShowRepository = tvShowRepository;
            _contentPollingQueue = contentPollingQueue;
        }

        public async Task<TvShow> Handle(AddTvShowCommand request, CancellationToken cancellationToken)
        {
            var response = await _client.AddTvShowAsync(request.TvShow, cancellationToken);
            var tvShow = _tvShowRepository.Insert(response.ToEntity());

            _contentPollingQueue.Enqueue(tvShow);

            _addedContentRepository.Insert(new AddedContent
            {
                ForeignId = tvShow.TvdbId,
                SystemId = tvShow.SystemId,
                Title = tvShow.Title,
                DateAdded = DateTime.Now,
                Status = ContentStatus.Queued,
                Account = await _accountRepository.GetAccountById(_context.GetIdentity(), cancellationToken),
                Type = ContentType.TvShow
            });

            return tvShow;
        }
    }
}
