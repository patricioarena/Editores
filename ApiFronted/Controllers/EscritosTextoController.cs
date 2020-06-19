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
using Microsoft.Extensions.Logging;

namespace ApiFronted.Controllers
{
#if DEBUG || PERSONAL
    [Route("api")]
    [AllowAnonymous]
#endif
    public class EscritosTextoController : Controller
    {
        private readonly IAuthorizationService _AuthorizationService;
        private readonly IHttpHelperRestConections _RestHelper;
        private readonly ILogger<EscritosTextoController> _Logger;
        public EscritosTextoController(IConfiguration configuration, IAuthorizationService authorizationService, ILogger<EscritosTextoController> logger , IHttpHelperRestConections helperRestConections)
        {
            _Logger = logger;
            _AuthorizationService = authorizationService;
            _RestHelper = helperRestConections;
        }

        [HttpGet]
        [Route("GetAllEscritosTextos")]
        public JObject GetAllEscritosTextos()
        {
            var uri = "api/EscritosTexto/GetAllEscritosTextos";
            var response =  _RestHelper.restCallGet(uri, this);
            _Logger.LogInformation(response.ToString());
            return response;
        }

        [HttpGet]
        [Route("GetEscritosTextoById/{escritoTextoID}")]
        public JObject Get(int escritoTextoID)
        {
            var uri = "api/EscritosTexto/GetEscritosTextoById/" + escritoTextoID;
            var response =  _RestHelper.restCallGet(uri, this);
            _Logger.LogInformation(response.ToString());
            return response;
        }

        [HttpGet]
        [Route("GetUltimoEscritosTexto")]
        public JObject GetUltimoEscritosTexto()
        {
            var uri = "api/EscritosTexto/GetUltimoEscritosTexto";
            var response =  _RestHelper.restCallGet(uri, this);
            _Logger.LogInformation(response.ToString());
            return response;
        }

        [HttpPost]
        [Route("SetEscritoTexto")]
        public JObject SetEscritoTexto([FromBody] EscritosTextoDto aEscritoTexto)
        {
            var uri = "api/EscritosTexto/SetEscritoTexto";
            var response = _RestHelper.restCallPost(uri, aEscritoTexto, this);

            _Logger.LogInformation(response.ToString());
            return response;
        }
    }
}



