using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alertas.Infra.Interfaces;
using Alertas.Infra.Repositories;

class Subscriber
{
    static async Task Main()
    {
        IAlertaSubscriberService subscriberService = new AlertaSubscriberService();

        subscriberService.StartListening("fila_servidor", "alerta.servidor", "http://localhost:5154/api/servidor/alerta");
        subscriberService.StartListening("fila_rede", "alerta.rede", "http://localhost:5154/api/rede/alerta");
        subscriberService.StartListening("fila_banco", "alerta.banco", "http://localhost:5154/api/banco/alerta");
        subscriberService.StartListening("fila_seguranca", "alerta.seguranca", "http://localhost:5154/api/seguranca/alerta");

        Console.WriteLine("Aguardando mensagens nas filas...");
        await Task.Delay(-1);
    }
}
