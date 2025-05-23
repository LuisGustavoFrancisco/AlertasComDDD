using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Subscriber
{
    static async Task Main()
    {
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

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare("alertas.exchange", ExchangeType.Topic, durable: true);
        channel.QueueDeclare("fila_servidor", durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind("fila_servidor", "alertas.exchange", "alerta.servidor");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($"Mensagem recebida da fila_servidor: {json}");

            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5154/api/servidor/alerta", content);

            Console.WriteLine($"Alerta enviado para API. Status HTTP: {response.StatusCode}");

            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume("fila_servidor", autoAck: false, consumer: consumer);

        Console.WriteLine("Aguardando mensagens na fila_servidor...");
        Console.ReadLine();
    }
}
