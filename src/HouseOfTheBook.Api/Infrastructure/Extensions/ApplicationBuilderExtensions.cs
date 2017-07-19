using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder builder,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            return condition ? action(builder) : builder;
        }

        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder builder,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            return condition ? ifAction(builder) : elseAction(builder);
        }
    }
}
