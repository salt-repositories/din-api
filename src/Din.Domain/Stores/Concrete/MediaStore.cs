using System;
using System.Collections.Generic;
using Din.Domain.Clients.Unsplash.Responses;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Stores.Concrete
{
    public class MediaStore : IMediaStore
    {
        private (DateTime storeDate, IEnumerable<UnsplashImage> backgrounds) _backgroundData;

        public IEnumerable<UnsplashImage> GetBackgrounds()
        {
            var now = DateTime.Now;

            if (_backgroundData.storeDate.AddDays(1) <= now || _backgroundData.backgrounds == null)
            {
                return null;
            }
            
            return _backgroundData.backgrounds;
        }

        public void SetBackgrounds(IEnumerable<UnsplashImage> backgrounds)
        {
            _backgroundData.storeDate = DateTime.Now;
            _backgroundData.backgrounds = backgrounds;
        }
    }
}
