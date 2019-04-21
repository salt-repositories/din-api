using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Generators.Interfaces;

namespace Din.Domain.Generators.Concrete
{
    public class MediaGenerator : IMediaGenerator
    {
        private readonly IGiphyClient _giphyClient;
        private readonly IUnsplashClient _unsplashClient;
        private (DateTime DateGenerated, ICollection<UnsplashItem> Collection) _unsplashData;

        public MediaGenerator(IGiphyClient giphyClient, IUnsplashClient unsplashClient)
        {
            _giphyClient = giphyClient;
            _unsplashClient = unsplashClient;
            _unsplashData.DateGenerated = DateTime.Now;
        }

        public async Task<IEnumerable<UnsplashItem>> GenerateBackgroundImages()
        {
            var now = DateTime.Now;

            if (_unsplashData.Collection == null || _unsplashData.DateGenerated.AddDays(1) <= now)
            {
                _unsplashData.DateGenerated = now;
                _unsplashData.Collection = await _unsplashClient.GetBackgroundCollection();
            }

            return _unsplashData.Collection;
        }

        public async Task<GiphyItem> GenerateGif(string query)
        {
            return await _giphyClient.GetRandomGifAsync(query);

        }
    }
}