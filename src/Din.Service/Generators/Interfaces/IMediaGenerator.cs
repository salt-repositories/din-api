using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Dto;

namespace Din.Service.Generators.Interfaces
{
    public interface IMediaGenerator
    {
        Task<MediaDto> GenerateBackgroundImages();
        Task<MediaDto> GenerateGif(GiphyTag tag);
    }
}
