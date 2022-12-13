using OpenID.Dtos.Configuration;
using Skoruba.AuditLogging.Events;


namespace OpenID.Events.Client
{
    public class ClientDeletedEvent : AuditEvent
    {
        public ClientDto Client { get; set; }

        public ClientDeletedEvent(ClientDto client)
        {
            Client = client;
        }
    }
}