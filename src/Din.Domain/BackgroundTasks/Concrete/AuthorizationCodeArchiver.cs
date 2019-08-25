using System;
using System.Linq;
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
            var codes = (await _repository.GetAll(cancellationToken)).ToList();

            if (codes.Count.Equals(0))
            {
                return;
            }

            foreach (var code in codes.Where(code => DateTime.Now.AddDays(3)>= code.Generated))
            {
                code.Active = false;
            }

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
