using Alertas.Domain;
using Alertas.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertas.Infra.Repositories
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly SqlContext sqlcontext;

        public AlertaRepository()
        {
            sqlcontext = new SqlContext();
        }
        public void Salvar(Alerta alerta)
        {
            if (alerta == null)
            {
                throw new ArgumentNullException(nameof(alerta), "Alerta não pode ser nulo");
            }
            sqlcontext.AddAsync(alerta);
            sqlcontext.SaveChangesAsync();
        }
    }
}
