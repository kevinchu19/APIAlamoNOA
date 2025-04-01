using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using APIAlamoNoa.Models.Response.Comprobante;

namespace APIAlamoNoa.Exceptions
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {

        public async override void OnException(ExceptionContext context)
        {
            string errorMessage;
            if (context.Exception.InnerException != null)
            {
                errorMessage = $"{context.Exception.InnerException} - {context.Exception.InnerException.StackTrace}";
            }
            else
            {
                errorMessage = $"{context.Exception.Message} - {context.Exception.StackTrace}";
            }


            ComprobanteResponse response = new ComprobanteResponse(new ComprobanteDTO("",
                "500",
                "Error interno",
                errorMessage,
                null));
            context.Result = new ObjectResult(response);

            context.HttpContext.Response.StatusCode =
                        (int)HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;

        }
    }
}
