using Alertas.Domain;
using Alertas.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/rede/alerta")]
    public class RedeController : ControllerBase
    {
        private readonly IAlertaSubscriber _alertasSubscriber;

        public RedeController(IAlertaSubscriber alertasSubscriber)
        {
            _alertasSubscriber = alertasSubscriber;
        }

        [HttpPost]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            _alertasSubscriber.Salvar(alerta);
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}