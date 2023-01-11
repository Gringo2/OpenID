using OpenID.Dtos.Identity.Base;
using OpenID.Dtos.Identity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OpenID.Dtos.Identity
{
    public class UserClaimDto<TKey> : BaseUserClaimDto<TKey>, IUserClaimDto
    {
        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }
    }
}