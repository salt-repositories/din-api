using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class LoginAttemptRepository : BaseRepository, ILoginAttemptRepository
    {
        public LoginAttemptRepository(DinContext context) : base(context)
        {
        }

        public async Task<LoginLocation> FindLoginLocationByCoordinates(string latitude, string longitude)
        {
            return await Context.LoginLocation
                .FirstOrDefaultAsync(l => l.Latitude.Equals(latitude) && l.Longitude.Equals(longitude));
        }
    }
}