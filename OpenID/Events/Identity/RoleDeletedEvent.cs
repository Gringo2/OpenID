using Skoruba.AuditLogging.Events;

namespace OpenID.Events.Identity
{
    public class RoleDeletedEvent<TRoleDto> : AuditEvent
    {
        public TRoleDto Role { get; set; }

        public RoleDeletedEvent(TRoleDto role)
        {
            Role = role;
        }
    }
}