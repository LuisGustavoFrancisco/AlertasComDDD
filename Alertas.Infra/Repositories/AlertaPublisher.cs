using Alertas.Infra.Interfaces;
using RabbitMQ.Client;
using System.Text;

public class AlertaPublisher : IAlertaPublisher
{
    private readonly IModel _channel;

    public AlertaPublisher()
    {
        var factory = new ConnectionFactory
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

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.ExchangeDeclare("alertas.exchange", ExchangeType.Topic, durable: true);
    }

    public void Publicar(string routingKey, string mensagem)
    {
        var body = Encoding.UTF8.GetBytes(mensagem);

        _channel.BasicPublish(
            exchange: "alertas.exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body
        );
    }
}
