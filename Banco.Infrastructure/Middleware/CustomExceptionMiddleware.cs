﻿
using Banco.Domain.Exceptions;
using Banco.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Banco.Infrastructure.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Error() { Description = error.Message, InnerException = error.InnerException?.ToString() };
                //_logger.LogInformation("{ResponseName}: with response {@response}", typeof(Error).Name, response);

                switch (error)
                {
                    case BancoException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}