using Alertas.Infra;
using Alertas.Infra.Interfaces;
using RabbitMQ.Client;
using System.Text;

public class AlertaPublisher : IAlertaPublisher
{
    private readonly IModel _channel;

    public AlertaPublisher()
    {
        _channel = RabbitMQConnection.CreateChannel();
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
