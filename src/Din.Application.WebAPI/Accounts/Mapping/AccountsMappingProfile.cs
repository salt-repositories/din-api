using AutoMapper.Configuration;
using Din.Application.WebAPI.Accounts.Requests;
using Din.Application.WebAPI.Querying;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Accounts.Mapping
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
