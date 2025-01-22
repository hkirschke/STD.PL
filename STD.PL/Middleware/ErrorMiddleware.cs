namespace STD.PL.Middleware;

using Microsoft.AspNetCore.Http;
using STD.PL.Consts;
using STD.PL.Exceptions;
using STD.PL.Records;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context?.Response != null && ex != null)
                await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        int code = (int)HttpStatusCode.InternalServerError;

        var messageModel = new Error();

        string msg = string.IsNullOrWhiteSpace(ex.Message) ? "Internal error occurred" : ex.Message;

        if (msg.StartsWith("One or more errors occurred.") && ex.InnerException != null)
        {
            msg = ex.InnerException.Message;
        }

        if (ex is BadHttpRequestException || ex.InnerException is BadHttpRequestException)
        {
            code = (int)HttpStatusCode.BadRequest;
        }

        if (ex is DomainException)
        {
            code = (int)HttpStatusCode.UnprocessableEntity;
            var domainException = ex as DomainException;
            messageModel.Errors = domainException?.exceptionsMessages;
        }

        messageModel.Code = code;
        messageModel.Reason = msg;
        messageModel.Detail = ex.InnerException?.ToString();
        messageModel.StackTrace = ex.StackTrace;

        string result = JsonSerializer.Serialize(messageModel);

        _logger.LogError($"Stack Trace: {ex.StackTrace}");

        httpContext.Response.ContentType = Common.ApplicationJson;
        httpContext.Response.StatusCode = code;

        return httpContext.Response.WriteAsync(result);
    }
}