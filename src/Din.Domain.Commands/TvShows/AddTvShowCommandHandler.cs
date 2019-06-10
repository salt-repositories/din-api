using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Context;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommandHandler : IRequestHandler<AddTvShowCommand, SonarrTvShow>
    {
        private readonly IRequestContext _context;
        private readonly ISonarrClient _client;
        private readonly IAccountRepository _accountRepository;
        private readonly IAddedContentRepository _addedContentRepository;

        public AddTvShowCommandHandler(IRequestContext context, ISonarrClient client, IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository)
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
        }

        public async Task<SonarrTvShow> Handle(AddTvShowCommand request, CancellationToken cancellationToken)
        {
            var tvShow = await _client.AddTvShowAsync(request.TvShow, cancellationToken);

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
