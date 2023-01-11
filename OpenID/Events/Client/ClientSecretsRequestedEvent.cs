using Skoruba.AuditLogging.Events;
using System;
using System.Collections.Generic;

namespace OpenID.Events.Client
{
    public class ClientSecretsRequestedEvent : AuditEvent
    {
        public int ClientId { get; set; }

        public List<(int clientSecretId, string type, DateTime? expiration)> Secrets { get; set; }

        public ClientSecretsRequestedEvent(int clientId, List<(int clientSecretId, string type, DateTime? expiration)> secrets)
        {
            ClientId = clientId;
            Secrets = secrets;
        }
    }
}