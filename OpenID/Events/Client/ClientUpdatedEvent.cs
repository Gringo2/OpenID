using OpenID.Dtos.Configuration;
using Skoruba.AuditLogging.Events;

namespace OpenID.Events.Client
{
    public class ClientUpdatedEvent : AuditEvent
    {
        public ClientDto OriginalClient { get; set; }
        public ClientDto Client { get; set; }

        public ClientUpdatedEvent(ClientDto originalClient, ClientDto client)
        {
            OriginalClient = originalClient;
            Client = client;
        }
    }
}