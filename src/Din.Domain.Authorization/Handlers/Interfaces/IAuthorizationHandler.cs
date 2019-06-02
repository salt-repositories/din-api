using System.Threading.Tasks;

namespace Din.Domain.Authorization.Handlers.Interfaces
{
    public interface IAuthorizationHandler<in TCommand>
    {
        Task Authorize(TCommand command);
    }
}
