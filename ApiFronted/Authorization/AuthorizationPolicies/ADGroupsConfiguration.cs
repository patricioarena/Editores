using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAngular.Authorization.AuthorizationPolicies
{
    /// <summary>
    /// represents ADGroupsConfiguration from appsettings.json
    /// </summary>
    public class ADGroupsConfiguration
    {
        public IEnumerable<string> MyFeature { get; set; }
    }
}
