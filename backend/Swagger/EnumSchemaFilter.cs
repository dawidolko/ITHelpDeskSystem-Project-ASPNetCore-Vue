using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace HelpDeskAPI.Swagger;

/// <summary>
/// Swagger filter to show enum names instead of numbers
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumValues = Enum.GetValues(context.Type);
            
            foreach (var value in enumValues)
            {
                schema.Enum.Add(new OpenApiString(value.ToString()));
            }
            
            schema.Type = "string";
            schema.Format = null;
            
            var values = string.Join(", ", Enum.GetNames(context.Type));
            schema.Description = $"Possible values: {values}";
        }
    }
}
