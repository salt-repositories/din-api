using System.Threading.Tasks;

namespace Din.Domain.Validators.Interfaces
{
    public interface IValidator<in T>
    {
        Task ValidateAsync(T obj);
    }
}
