using Skoruba.AuditLogging.Events;

namespace OpenID.Events.Client
{
    public class ClientSecretDeletedEvent : AuditEvent
    {
        public int ClientId { get; set; }

        public int ClientSecretId { get; set; }

        public ClientSecretDeletedEvent(int clientId, int clientSecretId)
        {
            ClientId = clientId;
            ClientSecretId = clientSecretId;
        }
    }
}