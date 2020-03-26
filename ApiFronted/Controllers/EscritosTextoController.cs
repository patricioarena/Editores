using ApiFrontend.Controllers;
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

namespace ApiFrontend.Controllers
{
#if DEBUG
    [RoutePrefix("api")]
    [EnableCors(origins: "https://localhost:4200", headers: "*", methods: "*")]
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
        [HttpPost]
        [Route("nuevo")]
        public JObject postEscritosTexto([FromBody]EscritosTexto aEscritoTexto)
        {
            var uri = "api/EscritosTexto/nuevo";
            return restHelper.restCallPost(uri, aEscritoTexto, this);
        }
    }


}


