using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto;
using Din.Service.Generators.Interfaces;

namespace Din.Service.Generators.Concrete
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

        public async Task<MediaDto> GenerateBackgroundImages()
        {
            var now = DateTime.Now;

            if (_unsplashData.Collection == null || _unsplashData.DateGenerated.AddDays(1) <= now)
            {
                _unsplashData.DateGenerated = now;
                _unsplashData.Collection = await _unsplashClient.GetBackgroundCollection();
            }

            return new MediaDto
            {
                BackgroundImageCollection = _unsplashData.Collection
            };
        }

        public async Task<MediaDto> GenerateGif(GiphyTag tag)
        {
            return new MediaDto
            {
                Gif = await _giphyClient.GetRandomGifAsync(tag)
            };
        }
    }
}