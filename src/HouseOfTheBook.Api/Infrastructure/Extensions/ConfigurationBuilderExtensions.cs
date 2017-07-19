using System;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddIf(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> action)
        {
            if (condition)
            {
                configurationBuilder = action(configurationBuilder);
            }

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddIfElse(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> ifAction,
            Func<IConfigurationBuilder, IConfigurationBuilder> elseAction)
        {
            configurationBuilder = condition ? ifAction(configurationBuilder) : elseAction(configurationBuilder);

            return configurationBuilder;
        }
    }
}
