using AutoMapper.Configuration;
using Din.Application.WebAPI.Authentication.Requests;
using Din.Application.WebAPI.Authentication.Responses;
using Din.Domain.Clients.IpStack.Responses;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Authentication.Mapping
{
    public class AuthenticationMappingProfile : MapperConfigurationExpression
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<CredentialRequest, CredentialsDto>();
            CreateMap<TokenDto, TokenResponse>();
            CreateMap<IpStackLocation, LoginLocation>();
        }
    }
}
