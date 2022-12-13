using System.Collections.Generic;
using OpenID.Configuration.Identity;


namespace Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.Configuration
{
	public class IdentityData
    {
       public List<Role> Roles { get; set; }
       public List<User> Users { get; set; }
    }
}
