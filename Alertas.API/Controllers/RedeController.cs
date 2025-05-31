using Alertas.Domain;
using Alertas.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/rede")]
    public class RedeController : ControllerBase
    {
        private readonly IAlertaSubscriber _alertasSubscriber;

        public RedeController(IAlertaSubscriber alertasSubscriber)
        {
            _alertasSubscriber = alertasSubscriber;
        }

        [HttpPost("Alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido de fila_rede: {alerta.Tipo} - {alerta.Mensagem}");
            _alertasSubscriber.Salvar(alerta);
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}