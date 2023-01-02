using Skoruba.AuditLogging.Events;

namespace OpenID.Events.Identity
{
    public class UserClaimsRequestedEvent<TUserClaimsDto> : AuditEvent
    {
        public TUserClaimsDto Claims { get; set; }

        public UserClaimsRequestedEvent(TUserClaimsDto claims)
        {
            Claims = claims;
        }
    }
}