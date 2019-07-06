using System;
using System.Collections.Generic;
using System.Linq;
using Din.Domain.Models.Dtos;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Stores.Concrete
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly ICollection<RefreshTokenDto> _tokens;

        public RefreshTokenStore()
        {
            _tokens = new List<RefreshTokenDto>();
        }

        public void Insert(RefreshTokenDto refreshToken)
        {
            _tokens.Add(refreshToken);
        }

        public RefreshTokenDto Get(string token)
        {
            return _tokens.FirstOrDefault(t => t.Token.Equals(token));
        }

        public void Invoke(RefreshTokenDto refreshToken)
        {
            _tokens.Remove(refreshToken);
        }

        public void Invoke(Guid id)
        {
            _tokens.Remove(_tokens.FirstOrDefault(t => t.AccountIdentity.Equals(id)));
        }
    }
}
