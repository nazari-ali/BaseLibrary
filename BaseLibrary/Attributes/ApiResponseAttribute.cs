using BaseLibrary.Api;
using BaseLibrary.Exceptions.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BaseLibrary.Attributes
{
    public class ApiResponseAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var ApiResponse = new ApiResponse<object>(true, HttpStatusCode.OK, okObjectResult.Value, LocalExceptionMessage.SuccessdMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = okObjectResult.StatusCode };
            }
            else if (context.Result is OkResult okResult)
            {
                var ApiResponse = new ApiResponse(true, HttpStatusCode.OK, LocalExceptionMessage.SuccessdMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = okResult.StatusCode };
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var ApiResponse = new ApiResponse(false, HttpStatusCode.BadRequest, LocalExceptionMessage.BadRequestMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = badRequestResult.StatusCode };
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var message = badRequestObjectResult.Value.ToString();
                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }
                var ApiResponse = new ApiResponse(false, HttpStatusCode.BadRequest, message);
                context.Result = new JsonResult(ApiResponse) { StatusCode = badRequestObjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                var ApiResponse = new ApiResponse(true, HttpStatusCode.OK, contentResult.Content);
                context.Result = new JsonResult(ApiResponse) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var ApiResponse = new ApiResponse(false, HttpStatusCode.NotFound, LocalExceptionMessage.NotFoundMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = notFoundResult.StatusCode };
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var ApiResponse = new ApiResponse<object>(false, HttpStatusCode.NotFound, notFoundObjectResult.Value, LocalExceptionMessage.NotFoundMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = notFoundObjectResult.StatusCode };
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null && !(objectResult.Value is ApiResponse))
            {
                var ApiResponse = new ApiResponse<object>(true, HttpStatusCode.NotFound, objectResult.Value, LocalExceptionMessage.NotFoundMessage);
                context.Result = new JsonResult(ApiResponse) { StatusCode = objectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}