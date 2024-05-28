namespace MediaSuggesterAPI.Middlewares
{
    public class CustomCorsMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Append("Access-Control-Allow-Origin", new[] { "*" });
            context.Response.Headers.Append("Access-Control-Allow-Headers", new[] { "Content-Type" });
            context.Response.Headers.Append("Access-Control-Allow-Methods", new[] { "GET", "POST", "PUT", "DELETE" });

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                await context.Response.CompleteAsync();
                return;
            }

            await next(context);
        }
    }
}
