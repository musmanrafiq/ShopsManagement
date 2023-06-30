namespace SM.WebApp.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                await context.Response.WriteAsync(message);
            }
        }
    }
}
