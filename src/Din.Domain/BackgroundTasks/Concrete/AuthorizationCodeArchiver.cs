using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundTasks.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Domain.BackgroundTasks.Concrete
{
    public class AuthorizationCodeArchiver : IBackgroundTask
    {
        private readonly IAuthorizationCodeRepository _repository;

        public AuthorizationCodeArchiver(IAuthorizationCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            foreach (var code in await _repository.GetAll(cancellationToken))
            {
                if (code.Generated >= DateTime.Now.AddDays(3))
                {
                    code.Active = false;
                }
            }

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
