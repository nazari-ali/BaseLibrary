using BaseLibrary.Exceptions.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BaseLibrary.Api
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(bool isSuccess, HttpStatusCode httpStatusCode, string message = null)
        {
            IsSuccess = isSuccess;
            HttpStatusCode = httpStatusCode;
            Message = message;
        }

        #region Implicit Operators

        /// <summary>
        /// Convert data types (Ok)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse(OkResult result)
        {
            return new ApiResponse(true, HttpStatusCode.OK, LocalExceptionMessage.SuccessdMessage);
        }

        /// <summary>
        /// Convert data types (BadRequest)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse(BadRequestResult result)
        {
            return new ApiResponse(false, HttpStatusCode.BadRequest, LocalExceptionMessage.BadRequestMessage);
        }

        /// <summary>
        /// Convert data types (BadRequestObject)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResponse(false, HttpStatusCode.BadRequest, message);
        }

        /// <summary>
        /// Convert data types (Content)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse(ContentResult result)
        {
            return new ApiResponse(true, HttpStatusCode.OK, result.Content);
        }

        /// <summary>
        /// Convert data types (NotFound)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse(NotFoundResult result)
        {
            return new ApiResponse(false, HttpStatusCode.NotFound, LocalExceptionMessage.NotFoundMessage);
        }

        #endregion
    }

    public class ApiResponse<TData> : ApiResponse
        where TData : class
    {
        public TData Data { get; set; }

        public ApiResponse( bool isSuccess, HttpStatusCode httpStatusCode, TData data, string message = null)
            : base(isSuccess, httpStatusCode, message)
        {
            Data = data;
        }

        #region Implicit Operators

        /// <summary>
        /// Convert data (Default)
        /// </summary>
        /// <param name="data"></param>
        public static implicit operator ApiResponse<TData>(TData data)
        {
            return new ApiResponse<TData>(true, HttpStatusCode.OK, data, LocalExceptionMessage.SuccessdMessage);
        }

        /// <summary>
        /// Convert data (Ok)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(OkResult result)
        {
            return new ApiResponse<TData>(true, HttpStatusCode.OK, null, LocalExceptionMessage.SuccessdMessage);
        }

        /// <summary>
        /// Convert data (OkObject)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(OkObjectResult result)
        {
            return new ApiResponse<TData>(true, HttpStatusCode.OK, (TData)result.Value, LocalExceptionMessage.SuccessdMessage);
        }

        /// <summary>
        /// Convert data (BadRequest)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(BadRequestResult result)
        {
            return new ApiResponse<TData>(false, HttpStatusCode.BadRequest, null, LocalExceptionMessage.BadRequestMessage);
        }

        /// <summary>
        /// Convert data (BadRequestObject)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResponse<TData>(false, HttpStatusCode.BadRequest, null, message);
        }

        /// <summary>
        /// Convert data (Content)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(ContentResult result)
        {
            return new ApiResponse<TData>(true, HttpStatusCode.OK, null, result.Content);
        }

        /// <summary>
        /// Convert data (NotFound)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(NotFoundResult result)
        {
            return new ApiResponse<TData>(false, HttpStatusCode.NotFound, null, LocalExceptionMessage.NotFoundMessage);
        }

        /// <summary>
        /// Convert data (NotFoundObject)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResponse<TData>(NotFoundObjectResult result)
        {
            return new ApiResponse<TData>(false, HttpStatusCode.NotFound, (TData)result.Value, LocalExceptionMessage.NotFoundMessage);
        }

        #endregion
    }
}