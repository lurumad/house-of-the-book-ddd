using System;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration, string key = null)
            where T : new()
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (key == null)
            {
                key = typeof(T).Name;
            }

            var section = new T();
            configuration.GetSection(key).Bind(section);
            return section;
        }
    }
}
