using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiBackend.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        public CustomController()
        {
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