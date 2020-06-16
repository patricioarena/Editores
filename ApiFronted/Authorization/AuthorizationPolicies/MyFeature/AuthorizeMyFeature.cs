using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFronted.Authorization;

namespace ApiFronted.Authorization.AuthorizationPolicies
{
    public class AuthorizeMyFeature : AuthorizeAttribute
    {
        public AuthorizeMyFeature() : base(AuthorizationPolicyNames.MyFeature)
        {
        }
    }
}
