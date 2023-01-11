using Skoruba.AuditLogging.Events;
using System;

namespace OpenID.Events.Client
{
    public class ClientSecretAddedEvent : AuditEvent
    {
        public string Type { get; set; }

        public DateTime? Expiration { get; set; }

        public int ClientId { get; set; }

        public ClientSecretAddedEvent(int clientId, string type, DateTime? expiration)
        {
            ClientId = clientId;
            Type = type;
            Expiration = expiration;
        }
    }
}