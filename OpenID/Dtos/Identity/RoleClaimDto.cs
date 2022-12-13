using System.ComponentModel.DataAnnotations;
using OpenID.Dtos.Identity.Base;
using OpenID.Dtos.Identity.Interfaces;


namespace OpenID.Dtos.Identity
{
    public class RoleClaimDto<TKey> : BaseRoleClaimDto<TKey>, IRoleClaimDto
    {
        [Required]
        public string ClaimType { get; set; }


        [Required]
        public string ClaimValue { get; set; }
    }
}
