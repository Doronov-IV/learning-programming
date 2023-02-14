namespace emptyproject.MiddlewareLike.FileUpload
{
    public static class UploadFileMiddlewareExtensions
    {

        public static IApplicationBuilder UseUploadFile(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UploadFileMiddleware>();
        }

    }
}
