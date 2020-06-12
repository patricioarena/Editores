using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAngular.Authorization.AuthorizationPolicies
{
    public class ADGroupsRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Groups { get; private set; }

        public ADGroupsRequirement(IEnumerable<string> groups)
        {
            Groups = groups;
        }
    }
}