using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Querying.Converters;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Querying;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Mapping.Accounts
{
    public class AccountsMappingProfile : MapperConfigurationExpression
    {
        public AccountsMappingProfile()
        {
            CreateMap<QueryParametersRequest, QueryParameters<Account>>()
                .ConvertUsing<ToQueryParametersConverter<Account>>();
            CreateMap<QueryParametersRequest, QueryParameters<AddedContent>>()
                .ConvertUsing<ToQueryParametersConverter<AddedContent>>();

            CreateMap<AccountRequest, Account>();
        }
    }
}
