using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiFronted.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ApiFronted.Authorization.AuthorizationPolicies;
using ApiFronted.Authorization.AuthorizationPolicies.MyFeature;
using ApiFronted.DTOs;

namespace ApiFronted.Controllers
{
#if DEBUG || PERSONAL
    [Route("api")]
    [AllowAnonymous]
#endif
    public class EscritosTextoController : Controller
    {

        private readonly IConfiguration _Configuration;
        //private readonly IAuthorizationService _AuthorizationService;
        private readonly HttpHelperRestConections _RestHelper;
        public EscritosTextoController(IConfiguration configuration, IAuthorizationService authorizationService)
        {
            _Configuration = configuration;
            //_AuthorizationService = authorizationService;
            _RestHelper = new HttpHelperRestConections(configuration.GetSection("BackendeUrl").GetSection("url").Value);
        }

        [HttpGet]
        [Route("GetAllEscritosTextos")]
        public JObject GetAllEscritosTextos()
        {
            var uri = "api/EscritosTexto/GetAllEscritosTextos";
            return _RestHelper.restCallGet(uri, this);
        }

        [HttpGet]
        [Route("GetEscritosTextoById/{escritoTextoID}")]
        public JObject Get(int escritoTextoID)
        {
            var uri = "api/EscritosTexto/GetEscritosTextoById/" + escritoTextoID;
            return _RestHelper.restCallGet(uri, this);
        }

        [HttpGet]
        [Route("GetUltimoEscritosTexto")]
        public JObject GetUltimoEscritosTexto()
        {
            var uri = "api/EscritosTexto/GetUltimoEscritosTexto";
            return _RestHelper.restCallGet(uri, this);
        }

        [HttpPost]
        [Route("SetEscritoTexto")]
        public JObject SetEscritoTexto([FromBody] EscritosTextoDto aEscritoTexto)
        {
            var uri = "api/EscritosTexto/SetEscritoTexto";
            return _RestHelper.restCallPost(uri, aEscritoTexto, this);
        }
    }
}



