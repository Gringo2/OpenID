using OpenID.Dtos.Configuration;
using Skoruba.AuditLogging.Events;

namespace OpenID.Events.Client
{
    public class ClientRequestedEvent : AuditEvent
    {
        public ClientDto ClientDto { get; set; }

        public ClientRequestedEvent(ClientDto clientDto)
        {
            ClientDto = clientDto;
        }
    }
}