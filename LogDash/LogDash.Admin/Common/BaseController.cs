using Exceptionless;
using FluentValidation;
using Framework.Platform.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Common
{
    public class BaseController : Controller
    {

        public JsonResult Success(object data = null)
        {
            return new JsonResult(new
            {
                StatusCode = HttpStatusCode.OK,
                ErrorCode = "",
                Message = "",
                Data = data
            });
        }
       
        public JsonResult Success(string Message, object data = null)
        {
            return new JsonResult(new
            {
                StatusCode = HttpStatusCode.OK,
                ErrorCode = "",
                Message = Message,
                Data = data
            });
        }
        public JsonResult Error(string Message, int ErrorCode = 0, object data = null)
        {
            return new JsonResult(new
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorCode = ErrorCode != 0 ? ErrorCode.ToString() : "",
                Message = Message,
                Data = data
            });
        }
        
    }

    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            //if (exception is DomainException)
            //    SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            //else if (exception is MyUnauthorizedException)
            //    SetExceptionResult(context, exception, HttpStatusCode.Unauthorized);
            //else if (exception is MyException)
            //    SetExceptionResult(context, exception, HttpStatusCode.BadRequest);

            if (exception is DomainException)
                SetExceptionResult(context, exception, HttpStatusCode.OK);
            else if (exception is ValidationException)
                SetExceptionResult(context, exception, HttpStatusCode.OK);
            else
                SetExceptionResult(context, exception, HttpStatusCode.InternalServerError);
        }

        private static void SetExceptionResult(ExceptionContext context, Exception exception, HttpStatusCode code)
        {

            if (exception is DomainException &&  context.HttpContext.Request.IsAjaxRequest())
                context.Result = new JsonResult(new
                {
                    StatusCode = code,
                    ErrorCode = ((DomainException)exception).ErroCode.ToString(),
                    Message = exception.Message
                });
            else if (exception is ValidationException && context.HttpContext.Request.IsAjaxRequest())
            {

                var strs = ((ValidationException)exception).Errors.Select(r => r.ErrorMessage);
                context.Result = new JsonResult(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorCode = HttpStatusCode.BadRequest,
                    Message = string.Join("<br/>", strs)
                });
            }
            else
            {

                exception.ToExceptionless().Submit();
                //context.Result = new JsonResult(new
                //{
                //    StatusCode = code,
                //    ErrorCode = "",
                //    Message = exception.Message

                //});
            }
        }
    }
}
