using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertas.Infra.Interfaces
{
    public interface IAlertaSubscriberService
    {
        void StartListening(string queueName, string routingKey, string endpointURL);
    }
}
