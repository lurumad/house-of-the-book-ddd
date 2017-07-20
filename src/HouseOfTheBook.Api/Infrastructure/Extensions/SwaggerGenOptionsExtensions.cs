using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HouseOfTheBook.Api.Infrastructure.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static SwaggerGenOptions IncludeXmlCommentsIfExists(this SwaggerGenOptions options, Assembly assembly)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var filePath = Path.ChangeExtension(assembly.Location, ".xml");
            if (!IncludeXmlCommentsIfExists(options, filePath) && (assembly.CodeBase != null))
            {
                filePath = Path.ChangeExtension(new Uri(assembly.CodeBase).AbsolutePath, ".xml");
                IncludeXmlCommentsIfExists(options, filePath);
            }

            return options;
        }

        public static bool IncludeXmlCommentsIfExists(this SwaggerGenOptions options, string filePath)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (File.Exists(filePath))
            {
                options.IncludeXmlComments(filePath);
                return true;
            }

            return false;
        }
    }
}
