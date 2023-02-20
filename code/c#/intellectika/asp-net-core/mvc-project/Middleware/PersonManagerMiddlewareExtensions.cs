namespace mvcproject.Middleware
{
    public static class PersonManagerMiddlewareExtensions
    {

        public static IApplicationBuilder UsePersonManager(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PersonManagerMiddleware>();
        }

    }
}
