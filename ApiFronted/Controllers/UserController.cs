using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiAngular.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ApiAngular.Authorization.AuthorizationPolicies;
using ApiAngular.Authorization.AuthorizationPolicies.MyFeature;

namespace ApiAngular.Controllers
{
    //[Route("api/[controller]")]
#if DEBUG
    [Route("api")]
    [AllowAnonymous]
#endif
    //[AuthorizeMyFeature]
    public class UserController : Controller
    {

        IConfiguration configuration;
        HttpHelperRestConections httpHelperRestConections;
        private readonly IAuthorizationService authorizationService;

        public UserController(IConfiguration configuration, IAuthorizationService authorizationService)
        {
            this.configuration = configuration;
            this.authorizationService = authorizationService;
            httpHelperRestConections = new HttpHelperRestConections(configuration.GetSection("BackendeUrl").GetSection("url").Value);
        }


        [HttpGet, Route("user")]
        public JObject GetUser()
        {
            var uri = "api/user/";
            return httpHelperRestConections.restCallGet(uri, this);
        }

 
    }
}
