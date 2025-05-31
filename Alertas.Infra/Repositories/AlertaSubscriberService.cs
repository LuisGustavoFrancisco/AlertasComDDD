using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertas.Infra.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace Alertas.Infra.Repositories
{
    public class AlertaSubscriberService : IAlertaSubscriberService
    {
        public void StartListening(string queueName, string routingKey, string endpointURL)
        {
            var channel = RabbitMQConnection.CreateChannel();
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
            channel.QueueBind(queueName, "alertas.exchange", routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                using var httpClient = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpointURL, content);
                Console.WriteLine($"Mensagem enviada para {endpointURL}, Status: {response.StatusCode}");

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }
    }
}
