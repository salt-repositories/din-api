using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class LoginAttemptRepository : BaseRepository, ILoginAttemptRepository
    {
        public LoginAttemptRepository(DinContext context) : base(context)
        {
        }
    }
}
