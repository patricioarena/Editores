using ApiBackend.Results;
using Application;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#if DEBUG || PERSONAL
    [AllowAnonymous]
#endif
    public class CustomController : Controller
    {
        public readonly IServiceEscritosTexto _ServiceEscritosTexto;

        public CustomController(IServiceEscritosTexto ServiceEscritosTexto)
        {
            _ServiceEscritosTexto = ServiceEscritosTexto;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ObjectResult CustomErrorStatusCode(Exception e)
        {

            if (e is CustomException)
            {
                var errorCode = ((CustomException)e).errorCode;
                var message = ((CustomException)e).Message;
                return StatusCode((int)HttpStatusCode.PreconditionFailed, new ResponseApi<object>(HttpStatusCode.PreconditionFailed, "ha ocurrido un error", null, e.InnerException != null ? e.InnerException.Message : message, errorCode));
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseApi<object>(HttpStatusCode.InternalServerError, "ha ocurrido un error", null, e.InnerException != null ? e.InnerException.Message : e.Message));
            }
        }
    }
}