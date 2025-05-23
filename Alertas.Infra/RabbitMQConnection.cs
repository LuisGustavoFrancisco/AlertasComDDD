using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Alertas.Infra
{
    public class RabbitMQConnection
    {
        private static IConnection connection;
        private static IModel channel;

        public static IModel CreateChannel()
        {
            if (channel != null && channel.IsOpen)
                return channel;

            var factory = new ConnectionFactory()
            {
                HostName = "jaragua.lmq.cloudamqp.com",
                Port = 5671,
                UserName = "zgindcqr",
                Password = "DVOUL1YUtl03fv2gZ8rM1VGEnS27206l",
                VirtualHost = "zgindcqr",
                Ssl = new SslOption
                {
                    Enabled = true,
                    ServerName = "jaragua.lmq.cloudamqp.com"
                }
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare("alertas.exchange", "topic", durable: true, autoDelete: false);
            return channel;
        }
    }
}
