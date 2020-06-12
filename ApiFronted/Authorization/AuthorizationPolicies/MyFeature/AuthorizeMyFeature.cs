using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAngular.Authorization;

namespace ApiAngular.Authorization.AuthorizationPolicies
{
    public class AuthorizeMyFeature : AuthorizeAttribute
    {
        public AuthorizeMyFeature() : base(AuthorizationPolicyNames.MyFeature)
        {
        }
    }
}
