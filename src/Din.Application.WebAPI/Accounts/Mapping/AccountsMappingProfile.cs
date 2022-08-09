using AutoMapper;
using Din.Application.WebAPI.Accounts.Requests;
using Din.Application.WebAPI.Accounts.Responses;
using Din.Application.WebAPI.Querying;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Din.Application.WebAPI.Accounts.Mapping
{
    public class AccountsMappingProfile : MapperConfigurationExpression
    {
        public AccountsMappingProfile()
        {
            CreateMap<QueryResult<Account>, QueryResponse<AccountResponse>>();
            CreateMap<AddedContent, AddedContentResponse>();
            CreateMap<QueryResult<AddedContent>, QueryResponse<AddedContentResponse>>();

            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>();

            CreateMap<JsonPatchDocument<AccountRequest>, JsonPatchDocument<Account>>();
            CreateMap<Operation<AccountRequest>, Operation<Account>>();
        }
    }
}
