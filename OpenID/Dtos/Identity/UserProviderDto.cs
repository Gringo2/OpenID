using OpenID.Dtos.Identity.Base;
using OpenID.Dtos.Identity.Interfaces;

namespace OpenID.Dtos.Identity
{
    public class UserProviderDto<TKey> : BaseUserProviderDto<TKey>, IUserProviderDto
    {
        public string UserName { get; set; }

        public string ProviderKey { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderDisplayName { get; set; }
    }
}
