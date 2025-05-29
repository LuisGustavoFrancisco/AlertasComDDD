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

        var tasks = new[]
        {
            StartSubscriber(factory, "fila_servidor", "alerta.servidor", "http://localhost:5154/api/servidor/alerta"),
            StartSubscriber(factory, "fila_rede", "alerta.rede", "http://localhost:5154/api/rede/alerta"),
            StartSubscriber(factory, "fila_banco", "alerta.banco", "http://localhost:5154/api/banco/alerta"),
            StartSubscriber(factory, "fila_seguranca", "alerta.seguranca", "http://localhost:5154/api/seguranca/alerta")
        };

        Console.WriteLine("Aguardando mensagens nas filas...");
        await Task.WhenAll(tasks);
    }

    static async Task StartSubscriber(ConnectionFactory factory, string queueName, string routingKey, string apiUrl)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare("alertas.exchange", ExchangeType.Topic, durable: true);
        channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(queueName, "alertas.exchange", routingKey);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($"Mensagem recebida da {queueName}: {json}");

            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            Console.WriteLine($"Alerta enviado para API ({apiUrl}). Status HTTP: {response.StatusCode}");

            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume(queueName, autoAck: false, consumer: consumer);

        // Mantém o subscriber ativo
        await Task.Delay(-1);
    }
}
