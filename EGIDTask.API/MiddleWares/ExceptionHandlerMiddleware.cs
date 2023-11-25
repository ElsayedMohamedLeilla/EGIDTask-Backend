using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Enums;
using EGIDTask.Models.Exceptions;
using EGIDTask.Models.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace EGIDTask.API.MiddleWares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _request;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _request = next;
        }
        public Task Invoke(HttpContext context, IUnitOfWork<ApplicationDBContext> unitOfWork) => InvokeAsync(context, unitOfWork);
        async Task InvokeAsync(HttpContext context, IUnitOfWork<ApplicationDBContext> unitOfWork)
        {

            var response = new ErrorResponse();
            int statusCode = 500;

            try
            {
                await _request.Invoke(context);
            }
            catch (BusinessValidationException ex)
            {
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                response.State = ResponseStatus.ValidationError;
                response.Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message :
                     ex.MessageCode;
                await Return(unitOfWork, context, statusCode, response);
            }
            catch (Exception exception)
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                response.State = ResponseStatus.Error;
                response.Message = exception.Message;
                await Return(unitOfWork, context, statusCode, response);
            }
        }
        private static async Task Return(IUnitOfWork<ApplicationDBContext> unitOfWork, HttpContext context, int statusCode, ErrorResponse response)
        {
            unitOfWork.Rollback();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, settings));
        }
    }
}