using System.Threading.Tasks;

namespace Din.Domain.Validators.Interfaces
{
    public interface IValidatorBus<T>
    {
        Task ValidateAsync(T obj);
    }
}
