using RabbitMQ.Client;
using System;
using System.Text;

class Publisher
{
    static void Main()
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

        var mensagem = @"{
            ""tipo"": ""falha"",
            ""categoria"": ""servidor"",
            ""mensagem"": ""Servidor de aplicação fora do ar"",
            ""dataHora"": ""2025-05-13T14:05:00Z"",
            ""hostname"": ""app01"",
            ""criticidade"": ""alta""
        }";

        var body = Encoding.UTF8.GetBytes(mensagem);

        channel.BasicPublish(
            exchange: "alertas.exchange",
            routingKey: "alerta.servidor",
            basicProperties: null,
            body: body
        );

        Console.WriteLine("Mensagem publicada para routing key 'alerta.servidor'");
    }
}
