using System.ComponentModel.DataAnnotations;
using OpenID.Dtos.Identity.Base;
using OpenID.Dtos.Identity.Interfaces;


namespace OpenID.Dtos.Identity
{
    public class RoleDto<TKey> : BaseRoleDto<TKey>, IRoleDto
    {      
        [Required]
        public string Name { get; set; }
    }
}