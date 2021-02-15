using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AzureFunctions.Extensions.Swashbuckle.SwashBuckle.Filters
{
    internal static class ObjectExtension
    {

        internal static T GetAttributeFrom<T>(this PropertyInfo propertyInfo) where T: System.Attribute
        {
            var attrType = typeof(T);
           
            return (T)propertyInfo.GetCustomAttributes(attrType, false).FirstOrDefault();
        }

        //var name = player.GetAttributeFrom<DisplayAttribute>(nameof(player.PlayerDescription)).Name;
        //var maxLength = player.GetAttributeFrom<MaxLengthAttribute>(nameof(player.PlayerName)).Length;
    }

    internal class QueryStringParameterAttributeFilter : IOperationFilter
    {
        private readonly ISchemaGenerator schemaGenerator;

        public QueryStringParameterAttributeFilter(ISchemaGenerator schemaGenerator)
        {
            this.schemaGenerator = schemaGenerator;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.DeclaringType != null)
            {
                var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<QueryStringParameterAttribute>();

                foreach (var attribute in attributes)
                {
                    var attributeTypeName = new OpenApiSchema {Type = "string"};
                    if (attribute?.DataType?.IsClass ==true)
                    {
                        foreach (var prop in attribute.DataType.GetProperties())
                        {
                            var isRequired = prop.GetAttributeFrom<RequiredAttribute>();
                            var display = prop.GetAttributeFrom<DisplayAttribute>();
                          //  var maxLength = prop.GetAttributeFrom<MaxLengthAttribute>();
                          //  var minLength = prop.GetAttributeFrom<MinLengthAttribute>();

                            //x-www-form-urlencoded 
                            operation.Parameters.Add(new OpenApiParameter
                            {
                                Name = display?.Name ?? prop.Name,
                                Description = $"{display?.Description ?? prop.Name }" ,
                                In = ParameterLocation.Query,
                                Required = isRequired != null,                                   
                                Schema = schemaGenerator.GenerateSchema(prop.PropertyType, new SchemaRepository())
                            });
                        } 

                        continue;
                    }
                     
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = attribute.Name,
                        Description = attribute.Description,
                        In = ParameterLocation.Query,
                        Required = attribute.Required,
                        Schema = schemaGenerator.GenerateSchema(attribute?.DataType, new SchemaRepository())
                    });
                }
            }
        }
    }
}