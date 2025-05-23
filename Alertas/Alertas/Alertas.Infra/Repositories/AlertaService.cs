using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertas.Domain;
using Alertas.Infra.Interfaces;

namespace Alertas.Infra.Repositories
{
    public class AlertaService : IAlertaService
    {
        public async Task ProcessarAlertaAsync(Alerta alerta)
        {
            if (!alerta.EhValido())
                throw new ArgumentException("Alerta inválido.");

            Console.WriteLine($"Alerta processado: {alerta.Mensagem}");
            await Task.CompletedTask;
        }
    }
}
