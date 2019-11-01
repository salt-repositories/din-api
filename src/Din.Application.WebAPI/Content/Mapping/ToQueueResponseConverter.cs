using AutoMapper;
using Din.Application.WebAPI.Content.Responses;
using Din.Domain.Clients.Interfaces;
using ClientContent = Din.Domain.Clients.Abstractions.Content;

namespace Din.Application.WebAPI.Content.Mapping
{
    public class ToQueueResponseConverter<T, K> : ITypeConverter<T, QueueResponse>
        where T : IQueue<K> where K : ClientContent
    {
        public QueueResponse Convert(T source, QueueResponse destination, ResolutionContext context)
        {
            return new QueueResponse
            {
                Id = source.Id,
                Title = source.Title,
                Size = source.Size,
                SizeLeft = source.SizeLeft,
                TimeLeft = source.TimeLeft,
                Eta = source.Eta,
                Status = source.Status,
                DownloadId = source.DownloadId,
                Protocol = source.Protocol,
                Content = new ContentResponse
                {
                    Id = source.Content.SystemId,
                    Title = source.Content.Title,
                    Overview = source.Content.Overview,
                    Genres = source.Content.Genres
                }
            };
        }
    }
}