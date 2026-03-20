using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    {
        var hasAuthorize = apiDescription
            .ActionDescriptor
            .GetCustomAttributes<System.Web.Http.AuthorizeAttribute>().Any()
            || apiDescription
            .ActionDescriptor
            .ControllerDescriptor
            .GetCustomAttributes<System.Web.Http.AuthorizeAttribute>().Any();

        if (!hasAuthorize)
            return;

        if (operation.security == null)
            operation.security = new List<IDictionary<string, IEnumerable<string>>>();

        operation.security.Add(new Dictionary<string, IEnumerable<string>>
        {
            { "Bearer", new string[] { } }
        });
    }
}