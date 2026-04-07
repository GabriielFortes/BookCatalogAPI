using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace BookCatalogAPI.SwaggerServerFilter
{
    public class SwaggerServerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>
            {
                new OpenApiServer { Url = "http://localhost:5146"}
            };
        }
    }
}