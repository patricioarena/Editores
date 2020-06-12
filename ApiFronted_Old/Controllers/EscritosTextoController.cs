using ApiFrontend_Old.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Models;

namespace ApiFrontend_Old.Controllers
{
#if DEBUG
    [RoutePrefix("api")]
#endif
    public class EscritosTextoController : ApiController
    {
        private readonly HttpHelperRestConections restHelper;
        public EscritosTextoController()
        {
            restHelper = new HttpHelperRestConections();
        }

#if DEBUG
        [AllowAnonymous]
#endif
        [HttpGet]
        [Route("GetAllEscritosTextos")]
        public JObject GetAllEscritosTextos()
        {
            var uri = "api/EscritosTexto/GetAllEscritosTextos";
            return restHelper.restCallGet(uri, this);
        }

#if DEBUG
        [AllowAnonymous]
#endif
        [HttpGet]
        [Route("escritoTexto/{escritoTextoID}")]
        public JObject Get(int escritoTextoID)
        {
            var uri = "api/EscritosTexto/escritoTexto/" + escritoTextoID;
            return restHelper.restCallGet(uri, this);
        }

#if DEBUG
        [AllowAnonymous]
#endif
        [HttpGet]
        [Route("GetUltimoEscritosTexto")]
        public JObject GetUltimoEscritosTexto()
        {
            var uri = "api/EscritosTexto/GetUltimoEscritosTexto";
            return restHelper.restCallGet(uri, this);
        }

#if DEBUG
        [AllowAnonymous]
#endif
        [HttpPost]
        [Route("SetEscritoTexto")]
        public JObject SetEscritoTexto([FromBody]EscritosTexto aEscritoTexto)
        {
            var uri = "api/EscritosTexto/SetEscritoTexto";
            return restHelper.restCallPost(uri, aEscritoTexto, this);
        }
    }
}


