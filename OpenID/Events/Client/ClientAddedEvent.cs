using OpenID.Dtos.Configuration;
using Skoruba.AuditLogging.Events;


namespace OpenID.Events.Client
{
    public class ClientAddedEvent : AuditEvent
    {
        public ClientDto Client { get; set; }

        public ClientAddedEvent(ClientDto client)
        {
            Client = client;
        }
    }
}