using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Din.Application.WebAPI.Controller;

[ApiController]
[Produces("application/json")]
[Authorize]
public abstract class ApiController : ControllerBase
{
    protected static OkObjectResult Ok<T>([ActionResultObjectValue] T value) => new(value);
    protected static CreatedResult Created<T>([ActionResultObjectValue] T value) => new(string.Empty, value);

    protected static JsonPatchDocument<TEntity> ConvertPatchDocument<TRequest, TEntity>(
        JsonPatchDocument<TRequest> patchDocument
    ) where TEntity : class where TRequest : class
    {
        return new JsonPatchDocument<TEntity>(
            patchDocument.Operations
                .Select(op => new Operation<TEntity>(op.op, op.path, op.from))
                .ToList(),
            patchDocument.ContractResolver
        );
    }
}