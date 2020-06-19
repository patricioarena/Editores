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

namespace ApiFronted.Controllers
{
#if DEBUG || PERSONAL
    [Route("api")]
    [AllowAnonymous]
#endif
    public class UserController : Controller
    {
        private readonly IHttpHelperRestConections _HttpHelperRestConections;
        private readonly IAuthorizationService _AuthorizationService;

        public UserController(IAuthorizationService authorizationService, IHttpHelperRestConections httpHelperRestConections)
        {
            _AuthorizationService = authorizationService;
            _HttpHelperRestConections = httpHelperRestConections;
        }


        [HttpGet, Route("user")]
        public JObject GetUser()
        {
            var uri = "api/user/";
            return _HttpHelperRestConections.restCallGet(uri, this);
        }


    }
}
