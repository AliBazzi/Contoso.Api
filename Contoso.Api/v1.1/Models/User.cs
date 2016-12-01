using System;
using Newtonsoft.Json;

namespace Contoso.Api.v11.Models
{
    public class User : v1.Models.User
    {
        [Obsolete("use TenantId instead", true), JsonIgnore]
        public override int OrganizationId { get; set; }
        
        [Obsolete("use first name and last name instead", true), JsonIgnore]
        public override string FullName { get; set; }

        public new string Id { get; set; }
        
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string TenantId { get; set; }
    }
}