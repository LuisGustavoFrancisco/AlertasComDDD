using Alertas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertas.Infra.Interfaces
{
    public interface IAlertaSubscriber
    {
        void Salvar (Alerta alerta);
    }
}
