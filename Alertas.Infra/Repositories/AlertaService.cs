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
        private readonly SqlContext _context;

        public AlertaService(SqlContext context)
        {
            _context = context;
        }
        public async Task ProcessarAlertaAsync(Alerta alerta)
        {
            if (!alerta.EhValido())
                throw new ArgumentException("Alerta inválido.");

            Console.WriteLine($"Alerta processado: {alerta.Mensagem}");

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
}
