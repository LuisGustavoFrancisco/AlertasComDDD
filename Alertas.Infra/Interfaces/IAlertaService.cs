using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertas.Domain;

namespace Alertas.Infra.Interfaces
{
    public interface IAlertaService
    {
        Task ProcessarAlertaAsync(Alerta alerta);
    }
}
