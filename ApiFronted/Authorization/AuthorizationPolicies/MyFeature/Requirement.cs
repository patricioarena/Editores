using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAngular.Authorization.AuthorizationPolicies.MyFeature
{
    public class Requirement : ADGroupsRequirement
    {
        public Requirement(IEnumerable<string> groups) : base(groups)
        {

        }

    }
}