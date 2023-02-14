using Microsoft.AspNetCore.Http.Features;

namespace emptyproject.MiddlewareLike
{
    public class UploadFileMiddleware
    {

        private readonly RequestDelegate _next;


        public UploadFileMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 1_000_000_000;

            var response = context.Response;
            var request = context.Request;

            response.ContentType = "text/html; charset=utf-8";

            if (request.Path == "/upload" && request.Method == "POST")
            {
                IFormFileCollection files = request.Form.Files;

                var uploadPath = $@"{Directory.GetCurrentDirectory()}/uploads";

                Directory.CreateDirectory(uploadPath);

                foreach (var file in files)
                {
                    string fullPath = $"{uploadPath}/{file.FileName}";

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                await response.WriteAsync("File load successful.");

            }
            else
            {
                await response.SendFileAsync("html/files.html");
            }

            await _next(context);
        }

    }
}
