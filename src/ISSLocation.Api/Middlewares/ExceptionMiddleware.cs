using System.Net;

namespace ISSLocation.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string ContentType = "application/json";
        private const int DefaultExceptionStatusCode = (int)HttpStatusCode.InternalServerError;
        private const string DefaultErrorMessage = "Unhandled error occurred.";
        private const string NotFoundErrorMessage = "Requested resource can not be found.";
        private const string BadRequestErrorMessage = "Your browser sent a request that the server could not understand.";
        private const string UnauthorizedErrorMessage = "Unauthorized Access.";
        private const string InternalServerErrorMessage = "Internal Server Error.";
        

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = ContentType;
            context.Response.StatusCode = DefaultExceptionStatusCode;
            var message = string.Empty;

            if (exception is HttpRequestException exp)
            {
                switch (exp.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = BadRequestErrorMessage;
                        break;
                    case HttpStatusCode.NotFound:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = NotFoundErrorMessage;
                        break;
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        message = UnauthorizedErrorMessage;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = InternalServerErrorMessage;
                        break;
                }
            }
            else if (exception is InvalidDataException)
            {
                message = BadRequestErrorMessage;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is ArgumentException)
            {
                message = BadRequestErrorMessage;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                message = DefaultErrorMessage;
                context.Response.StatusCode = DefaultExceptionStatusCode;
            }

            return context.Response.WriteAsync(message);
        }
    }
}